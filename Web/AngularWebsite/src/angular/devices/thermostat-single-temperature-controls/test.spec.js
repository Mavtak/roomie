describe('angular roomie.devices thermostat-single-temperature-controls (directive)', function () {
  var $compile;
  var $rootScope;
  var attributes;
  var element;

  beforeEach(angular.mock.module('roomie.devices'));

  beforeEach(angular.mock.inject(function ($injector) {
    $compile = $injector.get('$compile');
    $rootScope = $injector.get('$rootScope');
  }));

  beforeEach(function () {
    element = $compile('<thermostat-single-temperature-controls label="thingy" set="attributes.set" temperature="attributes.temperature"></thermostat-single-temperature-controls>')($rootScope);

    attributes = {
      set: jasmine.createSpy(),
      temperature: {
        value: 12.55,
        units: 'McDerps'
      }
    };

    $rootScope.attributes = attributes;

    $rootScope.$digest();
  });

  describe('the cooler button', function () {

    describe('existence criteria', function () {

      it('the "set" attribute is a function and "temperature" attribute exists and has a "value" property', function () {
        var button = selectButton();

        expect(button.length).toEqual(1);
      });

      it('even if the "temperature" attribute does not have a "units" property', function () {
        delete attributes.temperature.units;
        var button = selectButton();

        expect(button.length).toEqual(1);
      });

    });

    describe('nonexistence criteria', function () {

      it('the "set" attribute is not a function', function () {
        attributes.set = 'nope!';
        var button = selectButton();

        expect(button.length).toEqual(0);
      });

      it('the "temperature" attribute does not exist', function () {
        delete attributes.temperature;
        var button = selectButton();

        expect(button.length).toEqual(0);
      });

      it('the "temperature" attribute does not have a "value" attribute', function () {
        delete attributes.temperature.value;
        var button = selectButton();

        expect(button.length).toEqual(0);
      });

    });

    describe('clicking behavior', function () {

      it('calls the function specified by the "set" callback', function () {
        var button = selectButton();

        expect(attributes.set).not.toHaveBeenCalled();

        button.click();

        expect(attributes.set).toHaveBeenCalledWith({
          value: 11.55,
          units: 'McDerps'
        });
      });

    });

    function selectButton() {
      $rootScope.$digest();

      return $(element).find('.temperature .button.setpoint-button').filter(function () {
        return $(this).text().trim() === '-';
      });
    }

  });

  describe('the temperature display', function () {

    it('exists', function () {
      var display = selectDisplay();

      expect(display.length).toEqual(1);
    });

    it('formats the temperature', function () {
      var display = selectDisplay();

      expect(display.text().trim()).toEqual('12.55°M');
    });

    function selectDisplay() {
      return $(element).find('.temperature .value');
    }

  });

  describe('the hotter button', function () {

    describe('existence criteria', function () {

      it('the "set" attribute is a function and "temperature" attribute exists and has a "value" property', function () {
        var button = selectButton();

        expect(button.length).toEqual(1);
      });

      it('even if the "temperature" attribute does not have a "units" property', function () {
        delete attributes.temperature.units;
        var button = selectButton();

        expect(button.length).toEqual(1);
      });

    });

    describe('nonexistence criteria', function () {

      it('the "set" attribute is not a function', function () {
        attributes.set = 'nope!';
        var button = selectButton();

        expect(button.length).toEqual(0);
      });

      it('the "temperature" attribute does not exist', function () {
        delete attributes.temperature;
        var button = selectButton();

        expect(button.length).toEqual(0);
      });

      it('the "temperature" attribute does not have a "value" attribute', function () {
        delete attributes.temperature.value;
        var button = selectButton();

        expect(button.length).toEqual(0);
      });

    });

    describe('clicking behavior', function () {

      it('calls the function specified by the "set" callback', function () {
        var button = selectButton();

        expect(attributes.set).not.toHaveBeenCalled();

        button.click();

        expect(attributes.set).toHaveBeenCalledWith({
          value: 13.55,
          units: 'McDerps'
        });
      });

    });

    function selectButton() {
      $rootScope.$digest();

      return $(element).find('.temperature .button.setpoint-button').filter(function () {
        return $(this).text().trim() === '+';
      });
    }

  });

  describe('the label', function () {

    it('exists', function () {
      var actual = $(element).find('.temperature .description').text().trim();

      expect(actual).toEqual('thingy');
    });

  });

});
