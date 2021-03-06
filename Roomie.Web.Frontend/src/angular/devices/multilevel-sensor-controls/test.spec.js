describe('angular roomie.devices multilevel-sensor-controls (directive)', function () {
  var $injector;
  var $scope;
  var attributes;
  var element;

  beforeEach(angular.mock.module('roomie.devices'));

  beforeEach(angular.mock.inject(function (_$injector_) {
    $injector = _$injector_;
    $scope = $injector.get('$rootScope').$new();
  }));

  beforeEach(function () {
    attributes = {
      sensor: {
        poll: jasmine.createSpy(),
        value: {
          value: 123,
          units: 'McDerps'
        },
        timeStamp: new Date(2015, 4, 22, 22, 28, 17)
      }
    };

    $scope.attributes = attributes;

    element = compileDirective('<multilevel-sensor-controls label="derp!" sensor="attributes.sensor"></multilevel-sensor-controls>');
  });

  describe('the text', function () {
    var parts;
    var text;

    beforeEach(function () {
      text = $(element).text().trim();
      parts = text.split(' ');
    });

    it('has the label first', function () {
      expect(parts[0]).toEqual('derp!:');
    });

    it('has the measurement value second', function () {
      expect(parts[1]).toEqual('123');
    });

    it('has the measurement units third', function () {
      expect(parts[2]).toEqual('McDerps');
    });

    it('has the timestamp last', function () {
      expect(parts[3]).toEqual('(at');
      expect(parts[4]).toEqual('5/22/2015,');
      expect(parts[5]).toEqual('10:28:17');
      expect(parts[6]).toEqual('PM)');
    });

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

  function compileDirective(html) {
    var $compile = $injector.get('$compile');
    var element = $compile(html)($scope);
    $scope.$digest();

    return element;
  }

});
