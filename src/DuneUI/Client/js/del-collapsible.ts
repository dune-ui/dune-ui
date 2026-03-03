import { LitElement, html } from 'lit';
import { customElement } from 'lit/decorators.js';

@customElement('del-collapsible')
export class Collapsible extends LitElement {

    static override get observedAttributes() {
        return [...super.observedAttributes, 'hidden'];
    }

    #invokers: Element[] = [];

    override connectedCallback() {
        super.connectedCallback();
        this.addEventListener('command', this.#onCommand);
        setTimeout(() => { this.#cacheInvokers(); this.#syncState(); });
    }

    override disconnectedCallback() {
        super.disconnectedCallback();
        this.removeEventListener('command', this.#onCommand);
        this.#invokers = [];
    }

    override attributeChangedCallback(name: string, old: string | null, value: string | null) {
        super.attributeChangedCallback(name, old, value);
        if (name === 'hidden') this.#syncState();
    }

    #cacheInvokers() {
        if (!this.id) return;
        this.#invokers = Array.from(document.querySelectorAll(`[commandfor="${this.id}"]`));
    }

    #syncState() {
        this.dataset.state = this.hidden ? 'closed' : 'open';
        this.#syncInvokers();
    }

    #syncInvokers() {
        const expanded = String(!this.hidden);
        for (const invoker of this.#invokers) {
            invoker.setAttribute('aria-expanded', expanded);
        }
    }

    #onCommand = (event: Event) => {
        switch ((event as Event & { command: string }).command) {
            case '--show': this.show(); break;
            case '--hide': this.hide(); break;
            case '--toggle': this.toggle(); break;
        }
    }

    toggle() {
        this.hidden = !this.hidden;
    }

    show() {
        this.hidden = false;
    }

    hide() {
        this.hidden = true;
    }

    override render() {
        return html`<slot></slot>`;
    }
}
