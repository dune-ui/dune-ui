import { LitElement } from "lit";
import { customElement } from "lit/decorators.js";

/**
 * Menu popup web component for the Dropdown Menu family. Wraps a native `popover`
 * element (rendered by the `dui-dropdown-menu-content` / `dui-dropdown-menu-sub-content`
 * tag helpers); the browser handles open/close, light-dismiss, Escape, and CSS-anchor
 * positioning. This component adds the menu *semantics* on top:
 *
 *   - roving focus with Arrow keys / Home / End / type-ahead
 *   - Enter/Space to activate, close-on-select for ordinary items
 *   - checkbox/radio items toggle in place (the menu stays open)
 *   - submenu open (ArrowRight / hover via `interestfor`) and close (ArrowLeft)
 *
 * Rendered in light DOM (like `del-sidebar`) so the server-rendered items stay
 * styleable by Tailwind and participate in layout.
 */
@customElement("del-dropdown-menu")
export class DropdownMenu extends LitElement {
  override createRenderRoot() {
    return this;
  }

  #typeBuffer = "";
  #typeTimer = 0;

  override connectedCallback() {
    super.connectedCallback();
    this.addEventListener("toggle", this.#onToggle as EventListener);
    this.addEventListener("keydown", this.#onKeydown);
    this.addEventListener("click", this.#onClick);
  }

  override disconnectedCallback() {
    super.disconnectedCallback();
    this.removeEventListener("toggle", this.#onToggle as EventListener);
    this.removeEventListener("keydown", this.#onKeydown);
    this.removeEventListener("click", this.#onClick);
  }

  /** Focus the first enabled item — used on open and when entering a submenu. */
  focusFirstItem() {
    this.#items()[0]?.focus();
  }

  get #isSub() {
    return this.hasAttribute("data-sub");
  }

  /** The invoker (trigger / sub-trigger) that targets this popover. */
  #trigger(): HTMLElement | null {
    return this.id ? document.querySelector<HTMLElement>(`[popovertarget="${this.id}"]`) : null;
  }

  /** This menu level's enabled items, excluding any nested submenu's items. */
  #items(): HTMLElement[] {
    const all = this.querySelectorAll<HTMLElement>(
      '[role="menuitem"],[role="menuitemcheckbox"],[role="menuitemradio"]',
    );
    return Array.from(all).filter(
      (el) =>
        el.closest("del-dropdown-menu") === this && el.getAttribute("aria-disabled") !== "true",
    );
  }

  #onToggle = (event: ToggleEvent) => {
    const open = event.newState === "open";

    if (this.#isSub) {
      // Keep the parent sub-trigger highlighted while the submenu is open. The base
      // token keys off data-open; the Vega translucent theme keys off aria-expanded.
      const trigger = this.#trigger();
      trigger?.toggleAttribute("data-open", open);
      trigger?.setAttribute("aria-expanded", String(open));

      // On close (Esc light-dismiss / ArrowLeft) the <div> sub-trigger has no native
      // invoker association, so the browser drops focus to <body> and the parent menu
      // loses keyboard control. Return focus to the sub-trigger — but only when the
      // parent menu is still open, so we don't fight close-on-select / full dismiss.
      if (
        !open &&
        trigger &&
        this.parentElement?.closest("del-dropdown-menu")?.matches(":popover-open")
      ) {
        trigger.focus();
      }
      // Hover-opening shouldn't steal focus; ArrowRight focuses explicitly.
      return;
    }

    if (open) {
      this.focusFirstItem();
    }
  };

  #onKeydown = (event: KeyboardEvent) => {
    // Only the menu level that owns the focused item handles the key — submenu
    // events bubble through the parent popover, so guard against double handling.
    if ((event.target as Element)?.closest("del-dropdown-menu") !== this) {
      return;
    }

    const items = this.#items();
    const current = document.activeElement as HTMLElement | null;
    const index = current ? items.indexOf(current) : -1;

    switch (event.key) {
      case "ArrowDown":
        event.preventDefault();
        this.#focusAt(index < 0 ? 0 : index + 1);
        break;
      case "ArrowUp":
        event.preventDefault();
        this.#focusAt(index < 0 ? items.length - 1 : index - 1);
        break;
      case "Home":
        event.preventDefault();
        this.#focusAt(0);
        break;
      case "End":
        event.preventDefault();
        this.#focusAt(items.length - 1);
        break;
      case "ArrowRight":
        if (current?.getAttribute("aria-haspopup") === "menu") {
          event.preventDefault();
          this.#openSubmenu(current);
        }
        break;
      case "ArrowLeft":
        if (this.#isSub) {
          event.preventDefault();
          this.#closeSelf();
        }
        break;
      case "Escape":
        // Native Esc light-dismiss closes the whole popover stack at once here (the
        // <div> sub-trigger never set up a proper popover-ancestor relationship), losing
        // the parent menu. Handle it ourselves: cancel the native dismiss and close only
        // this submenu, returning focus to its sub-trigger.
        if (this.#isSub) {
          event.preventDefault();
          event.stopPropagation();
          this.#closeSelf();
        }
        break;
      case "Enter":
      case " ":
        if (current && items.includes(current)) {
          event.preventDefault();
          if (current.getAttribute("aria-haspopup") === "menu") {
            this.#openSubmenu(current);
          } else {
            current.click();
          }
        }
        break;
      default:
        if (event.key.length === 1 && !event.ctrlKey && !event.metaKey && !event.altKey) {
          this.#typeahead(event.key, items, index);
        }
    }
  };

  #onClick = (event: MouseEvent) => {
    const item = (event.target as Element)?.closest<HTMLElement>(
      '[role="menuitem"],[role="menuitemcheckbox"],[role="menuitemradio"]',
    );
    if (!item || item.closest("del-dropdown-menu") !== this) {
      return;
    }

    if (item.getAttribute("aria-disabled") === "true") {
      event.preventDefault();
      event.stopPropagation();
      return;
    }

    // Sub-trigger: open its submenu on click (popovertarget is inert on a <div>),
    // and never close the parent.
    if (item.getAttribute("aria-haspopup") === "menu") {
      this.#openSubmenu(item);
      return;
    }

    const role = item.getAttribute("role");
    if (role === "menuitemcheckbox") {
      this.#toggleCheckbox(item);
      return;
    }
    if (role === "menuitemradio") {
      this.#selectRadio(item);
      return;
    }

    this.#closeChain();
  };

  #focusAt(index: number) {
    const items = this.#items();
    if (items.length === 0) {
      return;
    }
    items[(index + items.length) % items.length].focus();
  }

  #openSubmenu(trigger: HTMLElement) {
    const id = trigger.getAttribute("popovertarget");
    const sub = id ? document.getElementById(id) : null;
    if (!(sub instanceof DropdownMenu)) {
      return;
    }
    if (!sub.matches(":popover-open")) {
      sub.showPopover();
    }
    sub.focusFirstItem();
  }

  #closeSelf() {
    if (this.matches(":popover-open")) {
      this.hidePopover();
    }
    this.#trigger()?.focus();
  }

  /** Close this menu and every ancestor menu (close-on-select). */
  #closeChain() {
    let top: DropdownMenu = this;
    let parent = this.parentElement?.closest("del-dropdown-menu");
    while (parent instanceof DropdownMenu) {
      top = parent;
      parent = parent.parentElement?.closest("del-dropdown-menu");
    }
    if (top.matches(":popover-open")) {
      top.hidePopover();
    }
  }

  #setItemState(item: HTMLElement, checked: boolean) {
    item.setAttribute("aria-checked", String(checked));
    item.setAttribute("data-state", checked ? "checked" : "unchecked");
    item.querySelector('[data-slot$="-indicator"]')?.classList.toggle("hidden", !checked);
  }

  #toggleCheckbox(item: HTMLElement) {
    const checked = item.getAttribute("aria-checked") !== "true";
    this.#setItemState(item, checked);
    item.dispatchEvent(new CustomEvent("checkedchange", { detail: { checked }, bubbles: true }));
  }

  #selectRadio(item: HTMLElement) {
    const group = item.getAttribute("data-radio-group");
    const siblings = group
      ? this.querySelectorAll<HTMLElement>(`[role="menuitemradio"][data-radio-group="${group}"]`)
      : [item];
    for (const radio of siblings) {
      this.#setItemState(radio, radio === item);
    }
    item.dispatchEvent(
      new CustomEvent("valuechange", {
        detail: { value: item.getAttribute("data-value") },
        bubbles: true,
      }),
    );
  }

  #typeahead(char: string, items: HTMLElement[], currentIndex: number) {
    clearTimeout(this.#typeTimer);
    this.#typeBuffer += char.toLowerCase();
    this.#typeTimer = window.setTimeout(() => {
      this.#typeBuffer = "";
    }, 500);

    const start = currentIndex < 0 ? 0 : currentIndex;
    for (let offset = 1; offset <= items.length; offset++) {
      const candidate = items[(start + offset) % items.length];
      const text = (candidate.textContent ?? "").trim().toLowerCase();
      if (text.startsWith(this.#typeBuffer)) {
        candidate.focus();
        break;
      }
    }
  }
}
