function getNewModeToToggleSetpoint() {

  return function getNewModeToToggleSetpoint(currentMode, toggledSetpoint) {
    if (currentMode === undefined || toggledSetpoint === undefined) {
      return;
    }

    currentMode = currentMode.toLowerCase();
    toggledSetpoint = toggledSetpoint.toLowerCase();

    switch (currentMode) {
      case 'off':
        switch (toggledSetpoint) {
          case 'heat':
            return 'heat';

          case 'cool':
            return 'cool';
        }
        break;

      case 'heat':
        switch (toggledSetpoint) {
          case 'heat':
            return 'off';

          case 'cool':
            return 'auto';
        }
        break;

      case 'cool':
        switch (toggledSetpoint) {
          case 'heat':
            return 'auto';

          case 'cool':
            return 'off';
        }
        break;

      case 'auto':
        switch (toggledSetpoint) {
          case 'heat':
            return 'cool';

          case 'cool':
            return 'heat';
        }
        break;
    }
  };
}

export default getNewModeToToggleSetpoint;
