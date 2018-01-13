describe('angular roomie.devices thermostat-single-temperature-controls (directive)', function () {
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
      set: jasmine.createSpy(),
      temperature: {
        value: 12.55,
        units: 'McDerps'
      },
      toggle: jasmine.createSpy('toggle')
    };

    $scope.attributes = attributes;

    element = compileDirective('<thermostat-single-temperature-controls label="thingy" set="attributes.set" temperature="attributes.temperature" toggle="attributes.toggle" active="attributes.active"></thermostat-single-temperature-controls>');
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
      $scope.$digest();

      return $(element).find('.temperature .button.setpoint-button').filter(function () {
        return $(this).text().trim() === '-';
      });
    }

  });

  describe('the main display', function () {

    it('exists', function () {
      expect(selectDisplay().length).toEqual(1);
    });

    describe('the temperature', function () {

      it('exists', function () {
        expect(selectDisplay().find('.value').length).toEqual(1);
      });

      it('formats the temperature', function () {
        expect(selectDisplay().find('.value').text().trim()).toEqual('12.55°M');
      });

    });

    describe('the label', function () {

      it('exists', function () {
        expect(selectDisplay().find('.description').length).toEqual(1);
      });

      it('includes the passed label', function () {
        expect(selectDisplay().find('.description').text().trim()).toEqual('thingy');
      });

    });

    describe('the button', function () {

      describe('when toggle function is not supplied', function () {

        beforeEach(function () {
          delete attributes.toggle;

          $scope.$digest();
        });

        it('does not have a button class', function () {
          expect(selectDisplay().children().hasClass('button')).toEqual(false);
        });

        it('does not have a toggle class', function () {
          expect(selectDisplay().children().hasClass('toggle')).toEqual(false);
        });

      });

      describe('when toggle function is supplied', function () {

        it('does have a button class', function () {
          expect(selectDisplay().children().hasClass('button')).toEqual(true);
        });

        it('does have a toggle class', function () {
          expect(selectDisplay().children().hasClass('toggle')).toEqual(true);
        });

        describe('when it is clicked', function () {

          it('calls the toggle function', function () {
            expect(attributes.toggle).not.toHaveBeenCalled();

            selectDisplay().children().click();

            expect(attributes.toggle).toHaveBeenCalled();
          });

        });

      });

      it('reflects the active value', function () {
        delete attributes.active;
        $scope.$digest();
        expect(selectDisplay().children().hasClass('active')).toEqual(false);


        attributes.active = true;
        $scope.$digest();
        expect(selectDisplay().children().hasClass('active')).toEqual(true);

        attributes.active = false;
        $scope.$digest();
        expect(selectDisplay().children().hasClass('active')).toEqual(false);
      });

    });

    function selectDisplay() {
      return $(element).find('.temperature .section.main-display');
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
      $scope.$digest();

      return $(element).find('.temperature .button.setpoint-button').filter(function () {
        return $(this).text().trim() === '+';
      });
    }

  });

  function compileDirective(html) {
    var $compile = $injector.get('$compile');
    var element = $compile(html)($scope);
    $scope.$digest();

    return element;
  }

});
