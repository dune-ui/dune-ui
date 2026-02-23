export default (show, dismissable) => ({
    // Properties
    show: show,
    dismissable: dismissable,
    _resolve: null,

    // Initialization
    init() {
        this.$el.close = () => {
            this.close();
        };
        
        this.$el.open = () => {
            this.open();

            return new Promise((resolve) => {
                this._resolve = resolve;
            });
        };
    },

    // Methods
    close() {
        this.show = false;
        
        if (this._resolve) {
            this._resolve('Hello there!!');
        }
        
        this._resolve = null;
    },
    open() {
        this.show = true;
    },

    // Directive bindings
    root: {
        ['x-id']() {
            return ['dialog'];
        },
        ['x-on:keydown.esc.window.stop']() {
            if (this.dismissable) {
                return this.close();
            }
        },
    },
    trigger: {
        ['@click']() {
            return this.open();
        },
        [':id']() {
            return this.$id('dialog') + '-trigger';
        },
    },
    overlay: {
        ['@click']() {
            if (this.dismissable) {
                return this.close();
            }
        },
        ['x-show']() {
            return this.show;
        },
        ['x-cloak']() {
            return true;
        },
        ['x-trap.noscroll.inert']() {
            return this.show;
        },
        ['x-transition.opacity.duration.150ms']() {
            return true;
        },
    },
    dialog: {
        ['@click.stop']() {
            return true;
        },
        [':aria-labelledby']() {
            return this.$id('dialog') + '-title';
        },
        [':aria-describedby']() {
            return this.$id('dialog') + '-description';
        },
        [':aria-modal']() {
            return this.show;
        },
    },
    title: {
        [':id']() {
            return this.$id('dialog') + '-title';
        },
    },
    description: {
        [':id']() {
            return this.$id('dialog') + '-description';
        },
    },
    closeButton: {
        ['@click']() {
            return this.close();
        },
    }
})