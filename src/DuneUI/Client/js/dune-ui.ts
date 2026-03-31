import "./web-components/del-collapsible";
import "./web-components/del-dialog";

import { dialog } from "./wrappers/dialog";

import "interestfor";

const duneui = { dialog };

// augment the Window type so TS knows about window.duneui
declare global {
  interface Window {
    duneui: typeof duneui;
  }
}

if (window.duneui) {
  console.warn("duneui: window.duneui is already defined and will be overwritten.");
}

window.duneui = duneui;
