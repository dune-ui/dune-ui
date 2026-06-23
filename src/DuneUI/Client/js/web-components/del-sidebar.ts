import { LitElement } from "lit";
import { customElement } from "lit/decorators.js";

/**
 * Sidebar provider web component (the server-rendered equivalent of shadcn's
 * <SidebarProvider>). Rendered by the `dui-sidebar-wrapper` tag helper.
 *
 * It owns two independent pieces of state and syncs them onto the nested
 * `[data-slot="sidebar"]` element as data attributes, which the Tailwind
 * `group-data-[...]` variants react to:
 *   - open       -> data-state="expanded|collapsed"  (desktop, >= md)
 *   - openMobile -> data-mobile="open|closed"        (mobile drawer, < md)
 *
 * Keeping the two states separate means crossing the breakpoint never leaves
 * the sidebar in a half-open state.
 */
@customElement("del-sidebar")
export class Sidebar extends LitElement {
  override createRenderRoot() {
    return this;
  }

  #open = true; // desktop: expanded by default
  #openMobile = false; // mobile drawer: closed by default
  #sidebarEl: HTMLElement | null = null;
  #mql = window.matchMedia("(min-width: 768px)");

  override connectedCallback() {
    super.connectedCallback();
    this.#sidebarEl = this.querySelector<HTMLElement>('[data-slot="sidebar"]');

    this.addEventListener("command", this.#onCommand);
    this.#mql.addEventListener("change", this.#onBreakpointChange);

    this.#syncState();
  }

  override disconnectedCallback() {
    super.disconnectedCallback();
    this.removeEventListener("command", this.#onCommand);
    this.#mql.removeEventListener("change", this.#onBreakpointChange);
  }

  get isMobile() {
    return !this.#mql.matches;
  }

  /** Toggle whichever state is relevant for the current breakpoint. */
  toggleSidebar() {
    if (this.isMobile) {
      this.#openMobile = !this.#openMobile;
    } else {
      this.#open = !this.#open;
    }
    this.#syncState();
  }

  /** Set the mobile drawer open/closed state. */
  setOpenMobile(open: boolean) {
    this.#openMobile = open;
    this.#syncState();
  }

  closeMobile() {
    this.setOpenMobile(false);
  }

  #syncState() {
    if (!this.#sidebarEl) return;
    const collapsed = !this.#open;
    const mode = this.#sidebarEl.dataset.collapsibleConfig || "offcanvas";

    this.#sidebarEl.dataset.state = this.#open ? "expanded" : "collapsed";
    this.#sidebarEl.dataset.mobile = this.#openMobile ? "open" : "closed";
    
    // data-collapsible carries the collapse mode ONLY while collapsed on desktop.
    // It must be empty when expanded (so icon-rail menu styles don't fire) and on
    // mobile (the drawer always shows the full-width menu).
    this.#sidebarEl.dataset.collapsible = !this.isMobile && collapsed ? mode : "";
    
    this.#syncTriggers();
  }

  #syncTriggers() {
    const expanded = String(this.isMobile ? this.#openMobile : this.#open);
    for (const trigger of this.querySelectorAll('[data-slot="sidebar-trigger"]')) {
      trigger.setAttribute("aria-expanded", expanded);
    }
  }

  #onCommand = (event: Event) => {
    switch ((event as Event & { command: string }).command) {
      case "--toggle-sidebar":
        this.toggleSidebar();
        break;
      case "--open-mobile":
        this.setOpenMobile(true);
        break;
      case "--close-mobile":
        this.closeMobile();
        break;
    }
  };

  #onBreakpointChange = () => {
    // When entering desktop, make sure the mobile drawer is never left stuck open.
    if (!this.isMobile) {
      this.#openMobile = false;
    }

    this.#syncState();
  };
}
