describe('roomie.devices.ColorSwitchButtonGenerator', function () {
  var ColorSwitchButtonGenerator;
  var colors;

  beforeEach(angular.mock.module('roomie.devices', function ($provide) {
    $provide.factory('RainbowColorsGenerator', function () {
      return MockRainbowColorsGenerator;
    });
  }));

  beforeEach(angular.mock.inject(function ($injector) {
    ColorSwitchButtonGenerator = $injector.get('ColorSwitchButtonGenerator');
  }));

  beforeEach(function () {
    colors = ['red', 'yellow', 'blue'];
  });

  describe('generate', function() {

    it('returns a button for each color from the rainbow generator', function () {
      var generator = new ColorSwitchButtonGenerator({});

      var result = generator.generate();

      expect(result.length).toEqual(3);
    });

    describe('color values for each button', function() {

      it('corrosponds to each color from the rainbow generator', function() {
        var generator = new ColorSwitchButtonGenerator({});

        var result = generator.generate();

        expect(result[0].color).toEqual('red');
        expect(result[1].color).toEqual('yellow');
        expect(result[2].color).toEqual('blue');
      });

    });

    describe('label values for each button', function() {

      it('are nonbreaking spaces', function() {
        var generator = new ColorSwitchButtonGenerator({});

        var result = generator.generate();

        expect(result[0].label).toEqual('\xA0');
        expect(result[1].label).toEqual('\xA0');
        expect(result[2].label).toEqual('\xA0');
      });

    });

    describe('activated values for each button', function() {

      it('are always false', function () {
        var generator = new ColorSwitchButtonGenerator({});

        var result = generator.generate();

        expect(result[0].activated).toEqual(false);
        expect(result[1].activated).toEqual(false);
        expect(result[2].activated).toEqual(false);
      });

    });

  });

  function MockRainbowColorsGenerator() {

    this.generate = function () {
      return colors;
    };
  }

});
