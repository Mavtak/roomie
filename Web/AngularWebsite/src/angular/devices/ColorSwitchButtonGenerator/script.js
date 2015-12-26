angular.module('roomie.devices').factory('ColorSwitchButtonGenerator', function (
  RainbowColorsGenerator
  ) {

  return function ColorSwitchButtonGenerator() {
    var rainbowColorsGenerator = new RainbowColorsGenerator();

    this.generate = function() {
      var result = [];
      var colors = rainbowColorsGenerator.generate();

      for (var i = 0; i < colors.length; i++) {
        var label = '\xA0';
        var color = colors[i];
        var activated = false;

        result.push({
          label: label,
          color: color,
          activated: activated
        });
      }

      return result;
    };
  };

});
