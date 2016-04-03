describe('angular roomie.devices binary-sensor-controls (directive)', function () {
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
    element = $compile('<binary-sensor-controls sensor="attributes.sensor"></binary-sensor-controls>')($rootScope);

    attributes = {
      sensor: {
        poll: jasmine.createSpy(),
        value: true,
        timeStamp: new Date(2015, 3, 26, 13, 47, 23)
      }
    };

    $rootScope.attributes = attributes;
    $rootScope.$digest();
  });

  describe('the text', function () {
    var parts;
    var text;

    beforeEach(readText);

    describe('the label', function () {

      it('is "Binary Sensor" when sensor.type is undefined', function () {
        expect(text).toMatch(/^Binary Sensor: /);
      });

      //TODO: account for different values of sensor.type

    });

    describe('the measurement', function () {

      it('is "Unknown" when device.value is undefined', function () {
        delete attributes.sensor.value;
        readText();

        expect(text).toMatch(/: Unknown \(at/);
      });

      it('is "True" when device.value == true', function () {
        attributes.sensor.value = true;
        readText();

        expect(text).toMatch(/: True \(at/);

      });

      it('is "False" when device.value == false', function () {
        attributes.sensor.value = false;
        readText();

        expect(text).toMatch(/: False \(at/);
      });

      //TODO: account for different values of sensor.type

    });

    it('has the timestamp last', function () {
      expect(text).toMatch(/\(at 4\/26\/2015, 1:47:23 PM\)$/);
    });

    function readText() {
      $rootScope.$digest();
      text = $(element).text().trim();
      parts = text.split(' ');
    }

  });

  describe('the button', function () {
    var button;

    beforeEach(function () {
      button = $(element).find('button');
    });

    it('exists', function () {
      expect(button.length).toEqual(1);
    });

    describe('clicking', function () {

      it('calls the sensor.poll()', function () {
        expect(attributes.sensor.poll).not.toHaveBeenCalled();

        button.click();

        expect(attributes.sensor.poll).toHaveBeenCalled();
      });

    });

  });

});
