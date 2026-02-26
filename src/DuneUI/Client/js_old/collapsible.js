export default (isOpen) => ({
    isOpen: isOpen,
    root: {
        [':data-state']() {
            return this.$data.isOpen ? 'open' : 'closed';
        },
    },
    trigger: {
        ['@click']() {
            return this.toggle();
        },
    },
    content: {
        ['x-show']() {
            return this.$data.isOpen;
        },
    },
    toggle() {
        this.$data.isOpen = !this.$data.isOpen;
    }
})