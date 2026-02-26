import Alpine from 'alpinejs'
import anchor from '@alpinejs/anchor'
import collapse from '@alpinejs/collapse'

import collapsible from "./collapsible";
import dialog from "./dialog";
import popover from "./popover";
import sheet from "./sheet";
import tooltip from './tooltip';

window.Alpine = Alpine;

Alpine.plugin(anchor);
Alpine.plugin(collapse);

Alpine.data('collapsible', collapsible);
Alpine.data('dialog', dialog);
Alpine.data('popover', popover);
Alpine.data('sheet', sheet);
Alpine.data('tooltip', tooltip);

Alpine.start();