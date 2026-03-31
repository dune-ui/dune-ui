import { LitElement } from "lit";
import { customElement } from "lit/decorators.js";

@customElement("del-dialog")
class Dialog extends LitElement {
  createRenderRoot() {
    return this;
  }

  connectedCallback() {
    super.connectedCallback();
    const dialog = this.querySelector("dialog");
    if (!dialog) return;

    const sync = () => {
      const isOpen = dialog.open;
      const isModal = dialog.matches(":modal");
      const scrollbarWidth = window.innerWidth - document.documentElement.clientWidth;

      document.body.style.overflow = isOpen && isModal ? "hidden" : "";
      document.body.style.paddingRight = isOpen && isModal ? `${scrollbarWidth}px` : "";

      dialog.toggleAttribute("data-open", isOpen);
      dialog.toggleAttribute("data-closed", !isOpen);
    };

    new MutationObserver(sync).observe(dialog, { attributes: true, attributeFilter: ["open"] });
    sync();
  }
}
