describe('angular roomie.devices MultilevelSwitchButtonGenerator (directive)', function() {
  var MultilevelSwitchButtonGenerator;

  beforeEach(angular.mock.module('roomie.devices'));

  beforeEach(angular.mock.inject(function ($injector) {
    MultilevelSwitchButtonGenerator = $injector.get('MultilevelSwitchButtonGenerator');
  }));

  describe('generate', function() {

    it('returns an array of {count} items', function() {
      var generator = new MultilevelSwitchButtonGenerator({});

      var result = generator.generate(17);

      expect(result.length).toEqual(17);
    });

    describe('power values for each button', function() {

      it('works for 0 to 12, 5 buttons', function() {
        var generator = new MultilevelSwitchButtonGenerator({
          min: 0,
          max: 12
        });

        var result = generator.generate(5);

        expect(result[0].power).toEqual(0);
        expect(result[1].power).toEqual(3);
        expect(result[2].power).toEqual(6);
        expect(result[3].power).toEqual(9);
        expect(result[4].power).toEqual(12);
      });

      it('works for 0 to 10, 4 buttons (with rounding)', function () {
        var generator = new MultilevelSwitchButtonGenerator({
          min: 0,
          max: 10
        });

        var result = generator.generate(4);

        expect(result[0].power).toEqual(0);
        expect(result[1].power).toEqual(3);
        expect(result[2].power).toEqual(7);
        expect(result[3].power).toEqual(10);
      });

      it('works for 100 to 199, 4 buttons', function() {
        var generator = new MultilevelSwitchButtonGenerator({
          min: 100,
          max: 199
        });

        var result = generator.generate(4);

        expect(result[0].power).toEqual(100);
        expect(result[1].power).toEqual(133);
        expect(result[2].power).toEqual(166);
        expect(result[3].power).toEqual(199);
      });

    });

    describe('label values for each button', function() {

      it('works for 0 to 12, 5 buttons', function() {
        var generator = new MultilevelSwitchButtonGenerator({
          min: 0,
          max: 12
        });

        var result = generator.generate(5);

        expect(result[0].label).toEqual('Off');
        expect(result[1].label).toEqual('');
        expect(result[2].label).toEqual('');
        expect(result[3].label).toEqual('');
        expect(result[4].label).toEqual('On');
      });

    });

    describe('activated values for each button', function() {

      it('selects the nearest value', function() {
        var multilevelSwitch = {
          min: 0,
          max: 12
        };

        var generator = new MultilevelSwitchButtonGenerator(multilevelSwitch);

        multilevelSwitch.power = 1;
        var result = generator.generate(5);

        expect(result[0].activated).toEqual(true);
        expect(result[1].activated).toEqual(false);
        expect(result[2].activated).toEqual(false);
        expect(result[3].activated).toEqual(false);
        expect(result[4].activated).toEqual(false);

        multilevelSwitch.power = 1.5;
        result = generator.generate(5);

        expect(result[0].activated).toEqual(true);
        expect(result[1].activated).toEqual(false);
        expect(result[2].activated).toEqual(false);
        expect(result[3].activated).toEqual(false);
        expect(result[4].activated).toEqual(false);

        multilevelSwitch.power = 1.6;
        result = generator.generate(5);

        expect(result[0].activated).toEqual(false);
        expect(result[1].activated).toEqual(true);
        expect(result[2].activated).toEqual(false);
        expect(result[3].activated).toEqual(false);
        expect(result[4].activated).toEqual(false);
      });

      it('selects the first button power is min', function() {
        var multilevelSwitch = {
          min: 0,
          max: 12,
          power: 0
        };

        var generator = new MultilevelSwitchButtonGenerator(multilevelSwitch);
        var result = generator.generate(5);

        expect(result[0].activated).toEqual(true);
        expect(result[1].activated).toEqual(false);
        expect(result[2].activated).toEqual(false);
        expect(result[3].activated).toEqual(false);
        expect(result[4].activated).toEqual(false);
      });

      it('selects the last button power is max', function() {
        var multilevelSwitch = {
          min: 0,
          max: 12,
          power: 12
        };

        var generator = new MultilevelSwitchButtonGenerator(multilevelSwitch);
        var result = generator.generate(5);

        expect(result[0].activated).toEqual(false);
        expect(result[1].activated).toEqual(false);
        expect(result[2].activated).toEqual(false);
        expect(result[3].activated).toEqual(false);
        expect(result[4].activated).toEqual(true);
      });

      it('selects no value when power is undefined', function() {
        var multilevelSwitch = {
          min: 0,
          max: 12
        };

        var generator = new MultilevelSwitchButtonGenerator(multilevelSwitch);
        var result = generator.generate(5);

        expect(result[0].activated).toEqual(false);
        expect(result[1].activated).toEqual(false);
        expect(result[2].activated).toEqual(false);
        expect(result[3].activated).toEqual(false);
        expect(result[4].activated).toEqual(false);
      });

      it('selects no value when power is too lower', function() {
        var multilevelSwitch = {
          min: 0,
          max: 12,
          power: -1
        };

        var generator = new MultilevelSwitchButtonGenerator(multilevelSwitch);
        var result = generator.generate(5);

        expect(result[0].activated).toEqual(false);
        expect(result[1].activated).toEqual(false);
        expect(result[2].activated).toEqual(false);
        expect(result[3].activated).toEqual(false);
        expect(result[4].activated).toEqual(false);
      });

      it('selects no value when power is too high', function() {
        var multilevelSwitch = {
          min: 0,
          max: 12,
          power: 13
        };

        var generator = new MultilevelSwitchButtonGenerator(multilevelSwitch);
        var result = generator.generate(5);

        expect(result[0].activated).toEqual(false);
        expect(result[1].activated).toEqual(false);
        expect(result[2].activated).toEqual(false);
        expect(result[3].activated).toEqual(false);
        expect(result[4].activated).toEqual(false);
      });

    });

  });

});
