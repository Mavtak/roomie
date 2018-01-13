import commonModule from '../common/module.js';
import dataModule from '../data/module.js';
import defineModule from '../defineModule.js';
import binarySensorControls from './binary-sensor-controls/index.js';
import binarySwitchControls from './binary-switch-controls/index.js';
import colorSwitchControls from './color-switch-controls/index.js';
import ColorSwitchButtonGenerator from './ColorSwitchButtonGenerator/index.js';
import currentActionControls from './current-action-controls/index.js';
import deviceEditControls from './device-edit-controls/index.js';
import deviceList from './device-list/index.js';
import deviceWidget from './device-widget/index.js';
import getNewModeToToggleSetpoint from './getNewModeToToggleSetpoint/index.js';
import locationHeaderGroup from './location-header-group/index.js';
import LocationHeaderLabelGenerator from './LocationHeaderLabelGenerator/index.js';
import multilevelSensoControls from './multilevel-sensor-controls/index.js';
import multilevelSwitchControls from './multilevel-switch-controls/index.js';
import MultilevelSwitchButtonGenerator from './MultilevelSwitchButtonGenerator/index.js';
import RainbowColorsGenerator from './RainbowColorsGenerator/index.js';
import thermostatControls from './thermostat-controls/index.js';
import thermostatModeControls from './thermostat-mode-controls/index.js';
import thermostatSingleTemperatureControls from './thermostat-single-temperature-controls/index.js';
import thermostatTemperatureControls from './thermostat-temperature-controls/index.js';


let module = defineModule({
  name: 'roomie.devices', 
  dependencies: [
    commonModule,
    dataModule,
    'ui.router',
  ],
  definitions: [
    binarySensorControls,
    binarySwitchControls,
    colorSwitchControls,
    ColorSwitchButtonGenerator,
    currentActionControls,
    deviceEditControls,
    deviceList,
    deviceWidget,
    getNewModeToToggleSetpoint,
    locationHeaderGroup,
    LocationHeaderLabelGenerator,
    multilevelSensoControls,
    multilevelSwitchControls,
    MultilevelSwitchButtonGenerator,
    RainbowColorsGenerator,
    thermostatControls,
    thermostatModeControls,
    thermostatSingleTemperatureControls,
    thermostatTemperatureControls,
  ],
});

export default module;
