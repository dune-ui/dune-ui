export default (delayDuration, closeDelayDuration, defaultOpen, position, offset) => ({
  delayDuration: delayDuration,
  closeDelayDuration: closeDelayDuration,
  tooltipOpened: defaultOpen,
  debounceTimeout: null,
  trigger: {
    ["@focusin"]() {
      clearTimeout(this.mouseoutTimeout);
      clearTimeout(this.debounceTimeout);

      this.debounceTimeout = setTimeout(() => {
        this.open();
      }, this.delayDuration);
    },
    ["@focusout"]() {
      clearTimeout(this.mouseoutTimeout);
      this.mouseoutTimeout = setTimeout(() => {
        clearTimeout(this.debounceTimeout);
        this.close();
      }, this.closeDelayDuration);
    },
    ["@mouseover"]() {
      clearTimeout(this.mouseoutTimeout);
      clearTimeout(this.debounceTimeout);

      this.debounceTimeout = setTimeout(() => {
        this.open();
      }, this.delayDuration);
    },
    ["@mouseout"]() {
      clearTimeout(this.mouseoutTimeout);
      this.mouseoutTimeout = setTimeout(() => {
        clearTimeout(this.debounceTimeout);
        this.close();
      }, this.closeDelayDuration);
    },
  },
  content: {
    ["x-show"]() {
      return this.tooltipOpened;
    },
    [`x-anchor.${position}.offset.${offset}`]() {
      return this.$refs.trigger;
    },
    [":data-state"]() {
      return this.tooltipOpened ? "open" : "closed";
    },
  },
  open() {
    this.tooltipOpened = true;
  },
  close() {
    this.tooltipOpened = false;
  },
});
