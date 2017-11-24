import defineModule from '../defineModule.js';
import appContent from './app-content/index.js';
import appContentLoadingIndicator from './app-content-loading-indicator/index.js';
import appFooter from './app-footer/index.js';
import appHeader from './app-header/index.js';
import appHorizontalSection from './app-horizontal-section/index.js';
import dock from './dock/index.js';
import keyValue from './key-value/index.js';
import pageMenuItems from './pageMenuItems/index.js';
import roomieApp from './roomie-app/index.js';
import roomieBot from './roomie-bot/index.js';
import sideMenu from './side-menu/index.js';
import sideMenuButton from './side-menu-button/index.js';
import sideMenuItem from './side-menu-item/index.js';
import sideMenuSet from './side-menu-set/index.js';
import signInState from './signInState/index.js';
import wholePageStatus from './wholePageStatus/index.js';
import widget from './widget/index.js';
import widgetButton from './widget-button/index.js';
import widgetButtonGroup from './widget-button-group/index.js';
import widgetDataSection from './widget-data-section/index.js';
import widgetDisconnectedIcon from './widget-disconnected-icon/index.js';
import widgetHeader from './widget-header/index.js';

let module = defineModule({
  name: 'roomie.common', 
  definitions: [
    appContent,
    appContentLoadingIndicator,
    appFooter,
    appHeader,
    appHorizontalSection,
    dock,
    keyValue,
    pageMenuItems,
    roomieApp,
    roomieBot,
    sideMenu,
    sideMenuButton,
    sideMenuItem,
    sideMenuSet,
    signInState,
    wholePageStatus,
    widget,
    widgetButton,
    widgetButtonGroup,
    widgetDataSection,
    widgetDisconnectedIcon,
    widgetHeader,
  ],
});

export default module;
