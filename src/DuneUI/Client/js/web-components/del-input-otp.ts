import { LitElement } from "lit";
import { customElement } from "lit/decorators.js";

const SELECTION_STYLE_ID = "del-input-otp-selection-style";

/**
 * Hide the field's native selection highlight (once per document). The transparent text would
 * otherwise become visible when selected (e.g. Ctrl+A); the slots reflect the selection via their
 * `data-active` state instead. This can't be expressed as an inline style, so it lives here.
 */
function ensureSelectionStyle() {
  if (document.getElementById(SELECTION_STYLE_ID)) return;
  const style = document.createElement("style");
  style.id = SELECTION_STYLE_ID;
  style.textContent =
    'del-input-otp [data-slot="input-otp"]::selection{background:transparent;color:transparent;}';
  document.head.appendChild(style);
}

/**
 * Input OTP web component (the server-rendered equivalent of shadcn's Input OTP). Rendered by
 * the `dui-input-otp` tag helper.
 *
 * It renders in light DOM and operates on the server-rendered children: a single real
 * <input data-slot="input-otp"> that holds the whole code (and posts it with the form),
 * overlaid transparently over the presentational slot cells. Typing, paste, backspace, and
 * caret movement are all handled by the native input; this component keeps the cells in sync
 * with the value, reflects the active cell (and a blinking fake caret) via `data-active`, and
 * enforces the `data-pattern` regex by rejecting any input that would make the value fail it.
 */
@customElement("del-input-otp")
export class InputOtp extends LitElement {
  override createRenderRoot() {
    return this;
  }

  #field: HTMLInputElement | null = null;
  #slots: HTMLElement[] = [];
  #maxLength = 0;
  #pattern: RegExp | null = null;
  #lastValid = "";
  #caretClass = "pointer-events-none absolute inset-0 flex items-center justify-center";
  #caretLineClass = "animate-caret-blink bg-foreground h-4 w-px duration-1000";
  #resizeObserver: ResizeObserver | null = null;

  override connectedCallback() {
    super.connectedCallback();

    ensureSelectionStyle();

    this.#field = this.querySelector<HTMLInputElement>('[data-slot="input-otp"]');
    this.#slots = Array.from(this.querySelectorAll<HTMLElement>('[data-slot="input-otp-slot"]'));
    this.#maxLength = Number(this.getAttribute("maxlength")) || this.#slots.length;

    // --root-height drives the field's font-size so the transparent text lines up with the slots.
    // The server seeds a 32px fallback; measure the real slot height and keep it in sync for themes
    // that size slots differently (mirrors guilhermerodz/input-otp, which measures the same way).
    this.#measureRootHeight();
    if (this.#slots.length > 0 && typeof ResizeObserver !== "undefined") {
      this.#resizeObserver = new ResizeObserver(() => this.#measureRootHeight());
      this.#resizeObserver.observe(this.#slots[0]);
    }

    // The caret classes are resolved server-side (theme token + statics) and handed over here,
    // since the web component can't resolve themepack tokens itself.
    this.#caretClass = this.getAttribute("data-caret-class") ?? this.#caretClass;
    this.#caretLineClass = this.getAttribute("data-caret-line-class") ?? this.#caretLineClass;

    const pattern = this.getAttribute("data-pattern");
    if (pattern) {
      try {
        this.#pattern = new RegExp(pattern);
      } catch {
        this.#pattern = null;
      }
    }

    if (this.#field) {
      this.#lastValid = this.#field.value;
      this.#field.addEventListener("input", this.#onInput);
      this.#field.addEventListener("focus", this.#onFocus);
      this.#field.addEventListener("blur", this.#onSync);
      this.#field.addEventListener("click", this.#onSync);
      document.addEventListener("selectionchange", this.#onSelectionChange);
    }

    this.#sync();
  }

  override disconnectedCallback() {
    super.disconnectedCallback();
    this.#resizeObserver?.disconnect();
    this.#resizeObserver = null;
    if (this.#field) {
      this.#field.removeEventListener("input", this.#onInput);
      this.#field.removeEventListener("focus", this.#onFocus);
      this.#field.removeEventListener("blur", this.#onSync);
      this.#field.removeEventListener("click", this.#onSync);
    }
    document.removeEventListener("selectionchange", this.#onSelectionChange);
  }

  #onInput = () => {
    if (!this.#field) return;

    let value = this.#field.value;
    if (value.length > this.#maxLength) {
      value = value.slice(0, this.#maxLength);
    }

    if (this.#pattern && !this.#pattern.test(value)) {
      // Reject the change: restore the last value that matched the pattern.
      const caret = Math.min(this.#lastValid.length, this.#maxLength);
      this.#field.value = this.#lastValid;
      this.#field.setSelectionRange(caret, caret);
    } else {
      if (this.#field.value !== value) {
        this.#field.value = value;
      }
      this.#lastValid = value;
    }

    this.#sync();
  };

  #onFocus = () => {
    if (!this.#field) return;
    // Standard OTP UX: drop the caret at the first empty slot rather than mapping a click
    // position onto an invisible character.
    const length = this.#field.value.length;
    if (length < this.#maxLength) {
      this.#field.setSelectionRange(length, length);
    }
    this.#sync();
  };

  #onSelectionChange = () => {
    if (document.activeElement === this.#field) {
      this.#sync();
    }
  };

  #onSync = () => {
    this.#sync();
  };

  /** Distribute the value into the cells and reflect the active cell + fake caret. */
  #sync() {
    if (!this.#field) return;

    const value = this.#field.value;
    const focused = document.activeElement === this.#field;
    const selectionStart = this.#field.selectionStart ?? value.length;
    const selectionEnd = this.#field.selectionEnd ?? selectionStart;
    const isRange = focused && selectionStart !== selectionEnd;
    const caretIndex = Math.min(selectionStart, this.#maxLength - 1);

    for (let index = 0; index < this.#slots.length; index++) {
      const slot = this.#slots[index];
      const char = value[index] ?? "";

      const active = focused
        ? isRange
          ? index >= selectionStart && index < selectionEnd
          : index === caretIndex
        : false;
      slot.dataset.active = active ? "true" : "false";

      slot.textContent = char;
      if (active && char === "" && !isRange) {
        slot.appendChild(this.#createCaret());
      }
    }
  }

  /** Measure the rendered slot height and publish it as --root-height for the field's font-size. */
  #measureRootHeight() {
    const height = this.#slots[0]?.getBoundingClientRect().height;
    if (height) {
      this.style.setProperty("--root-height", `${height}px`);
    }
  }

  #createCaret(): HTMLElement {
    const caret = document.createElement("div");
    caret.className = this.#caretClass;
    const line = document.createElement("div");
    line.className = this.#caretLineClass;
    caret.appendChild(line);
    return caret;
  }
}
