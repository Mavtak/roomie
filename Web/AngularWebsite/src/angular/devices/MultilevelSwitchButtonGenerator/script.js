angular.module('roomie.devices').factory('MultilevelSwitchButtonGenerator', function () {

  return MultilevelSwitchButtonGenerator;

  function MultilevelSwitchButtonGenerator(multilevelSwitch) {
    this.multilevelSwitch = multilevelSwitch;

    this.generate = function(count) {
      var result = [];

      var actualPower = this.multilevelSwitch.power;
      var min = this.multilevelSwitch.min || this.multilevelSwitch.minPower || 0;
      var max = this.multilevelSwitch.max || this.multilevelSwitch.maxPower || 100;

      for (var i = 0; i < count; i++) {
        var label = '';
        var power = getPower(min, max, i, count);
        var lowerBound = (getPower(min, max, i - 1, count) + power) / 2;
        var upperBound = (getPower(min, max, i + 1, count) + power) / 2;
        var activated = false;

        if (i === 0) {
          label = 'Off';
          lowerBound = 0;

          if (actualPower == min) {
            activated = true;
          }
        } else if (i == count - 1) {
          label = 'On';
          upperBound = max;
        }

        if (!activated) {
          activated = actualPower > lowerBound && actualPower <= upperBound;
        }

        power = Math.round(power);

        result.push({
          label: label,
          power: power,
          activated: activated
        });
      }

      return result;
    };

    function getPower(min, max, i, count) {
      return min + (i / (count - 1)) * (max - min);
    }
  }

});
