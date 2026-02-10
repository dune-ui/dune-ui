export default (openOnHover, defaultOpen, position, offset) => ({
    openOnHover: openOnHover,
    isOpen: defaultOpen,
    isOpenedViaHover: false,
    debounceTimeout: null,
    root: {
        ['x-id']() {
            return ['popover'];
        },
        [':data-state']() {
            return this.isOpen ? 'open' : 'closed';
        },
    },
    trigger: {
        ['@click']() {
            return this.toggle();
        },
        ['@mouseover']() {
            if (this.openOnHover) {
                clearTimeout(this.debounceTimeout);
                return this.open(true);
            }
        },
        ['@mouseout']() {
            if (this.isOpenedViaHover) {
                this.debounceTimeout = setTimeout(() => {
                    clearTimeout(this.debounceTimeout);
                    this.close();
                }, 300);
            }
        },
        [':id']() {
            return this.$id('popover') + '-trigger';
        },
        [':aria-controls']() {
            return this.$id('popover') + '-content';
        },
        ['@keydown.esc.window']() {
            return this.close();
        },
        [':data-state']() {
            return this.isOpen ? 'open' : 'closed';
        },
    },
    content: {
        ['@click.outside.capture']() {
            if (!this.$refs.trigger.contains(this.$event.target)) {
                return this.close();
            }
        },
        ['@mouseover']() {
            clearTimeout(this.debounceTimeout);
        },
        ['@mouseout']() {
            if (this.isOpenedViaHover) {
                this.debounceTimeout = setTimeout(() => {
                    clearTimeout(this.debounceTimeout);
                    this.close();
                }, 300);
            }
        },
        [`x-anchor.${position}.offset.${offset}`]() {
            return this.$refs.trigger;
        },
        ['x-trap']() {
            return this.isOpen;
        },
        ['x-show']() {
            return this.isOpen;
        },
        ['x-transition']() {
            return true;
        },
        [':data-state']() {
            return this.isOpen ? 'open' : 'closed';
        },
        [':id']() {
            return this.$id('popover') + '-content';
        },
        [':aria-labelledby']() {
            return this.$id('popover-menu') + '-trigger';
        },
    },
    close() {
        this.isOpen = false;
        this.isOpenedViaHover = false;
    },
    open(viaHover = false) {
        this.isOpen = true;
        this.isOpenedViaHover = viaHover;
    },
    toggle() {
        this.isOpen === true ? this.close() : this.open();
        this.isOpenedViaHover = false;
    },
})