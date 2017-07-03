describe('angular roomie.devices thermostat-temperature-controls (directive)', function () {
  var $injector;
  var $scope;
  var attributes;
  var element;
  var getNewModeToToggleSetpoint;

  beforeEach(angular.mock.module('roomie.devices', function ($provide) {
    getNewModeToToggleSetpoint = jasmine.createSpy('getNewModeToToggleSetpoint');

    getNewModeToToggleSetpoint.and.returnValue('some-new-mode');

    $provide.value('getNewModeToToggleSetpoint', getNewModeToToggleSetpoint);
  }));

  beforeEach(angular.mock.inject(function (_$injector_) {
    $injector = _$injector_;
    $scope = $injector.get('$rootScope').$new();
  }));

  beforeEach(function () {

    attributes = {
      setpoints: {
        cool: {
          value: 50,
          units: 'McDerps'
        },
        heat: {
          value: 25,
          units: 'McDerps'
        },
        set: jasmine.createSpy()
      },
      temperature: {
        value: 30,
        units: 'McDerps'
      },
      core: {
        mode: 'some-current-mode',
        set: jasmine.createSpy()
      }
    };

    $scope.attributes = attributes;

    element = compileDirective('<thermostat-temperature-controls setpoints="attributes.setpoints" temperature="attributes.temperature" core="attributes.core"></thermostat-temperature-controls>');
  });

  describe('the temperature displays', function () {

    it('has 3', function () {
      var displays = selectTemperatureDisplays();

      expect(displays.length).toEqual(3);
    });

    describe('the first one', function () {

      it('is the heat setpoint', function () {
        var display = selectTemperatureDisplay(0);

        expect(display.find('.value').text()).toContain('25');
        expect(display.find('.description').text()).toContain('Heat');
      });

      describe('the main button', function () {

        it('toggles the setpoint by changing the mode', function () {
            var button = selectTemperatureDisplay(0).find('.button');

            button.click();

            expect(getNewModeToToggleSetpoint).toHaveBeenCalledWith('some-current-mode', 'heat');
            expect(attributes.core.set).toHaveBeenCalledWith('some-new-mode');
        });

        describe('active styling', function () {

          it('shows as active when code.mode = "heat"', function () {
            attributes.core.mode = 'heat';
            var button = selectTemperatureDisplayButton(0);

            $scope.$digest();

            expect(button.hasClass('active')).toEqual(true);
          });

          it('shows as inactive when code.mode = "cool"', function () {
            attributes.core.mode = 'cool';
            var button = selectTemperatureDisplayButton(0);

            $scope.$digest();

            expect(button.hasClass('active')).toEqual(false);
          });

          it('shows as active when code.mode = "auto"', function () {
            attributes.core.mode = 'auto';
            var button = selectTemperatureDisplayButton(0);

            $scope.$digest();

            expect(button.hasClass('active')).toEqual(true);
          });

        });

      });

      describe('the cooler button', function () {

        it('sets the heat setpoint to cooler', function () {
          var button = selectSetpointButton(0, '-');

          expect(attributes.setpoints.set).not.toHaveBeenCalled();

          button.click();

          expect(attributes.setpoints.set).toHaveBeenCalled();

          var args = attributes.setpoints.set.calls.mostRecent().args;

          expect(args[0]).toEqual('heat');
          expect(args[1]).toEqual({
            value: 24,
            units: 'McDerps'
          });
        });

      });

      describe('the hotter button', function () {

        it('sets the heat setpoint to hotter', function () {
          var button = selectSetpointButton(0, '+');

          expect(attributes.setpoints.set).not.toHaveBeenCalled();

          button.click();

          expect(attributes.setpoints.set).toHaveBeenCalled();

          var args = attributes.setpoints.set.calls.mostRecent().args;

          expect(args[0]).toEqual('heat');
          expect(args[1]).toEqual({
            value: 26,
            units: 'McDerps'
          });
        });

      });

    });

    describe('the second one', function () {

      it('is the current temperature', function () {
        var display = selectTemperatureDisplay(1);

        expect(display.find('.value').text()).toContain('30');
        expect(display.find('.description').text()).toContain('Current');
      });

      describe('the cooler button', function () {

        it('does not exist', function () {
          var button = selectSetpointButton(1, '-');

          expect(button.length).toEqual(0);
        });

      });

      describe('the hotter button', function () {

        it('does not exist', function () {
          var button = selectSetpointButton(1, '+');

          expect(button.length).toEqual(0);
        });

      });

    });

    describe('the third one', function () {

      it('is the cool setpoint', function () {
        var display = selectTemperatureDisplay(2);

        expect(display.find('.value').text()).toContain('50');
        expect(display.find('.description').text()).toContain('Cool');
      });

      describe('the main button', function () {

        it('toggles the setpoint by changing the mode', function () {
            var button = selectTemperatureDisplayButton(2);

            button.click();

            expect(getNewModeToToggleSetpoint).toHaveBeenCalledWith('some-current-mode', 'cool');
            expect(attributes.core.set).toHaveBeenCalledWith('some-new-mode');
        });

        describe('active styling', function () {

          it('shows as inactive when code.mode = "heat"', function () {
            attributes.core.mode = 'heat';
            var button = selectTemperatureDisplayButton(2);

            $scope.$digest();

            expect(button.hasClass('active')).toEqual(false);
          });

          it('shows as active when code.mode = "cool"', function () {
            attributes.core.mode = 'cool';
            var button = selectTemperatureDisplayButton(2);

            $scope.$digest();

            expect(button.hasClass('active')).toEqual(true);
          });

          it('shows as active when code.mode = "auto"', function () {
            attributes.core.mode = 'auto';
            var button = selectTemperatureDisplayButton(2);

            $scope.$digest();

            expect(button.hasClass('active')).toEqual(true);
          });

        });

      });

      describe('the cooler button', function () {

        it('sets the heat setpoint to cooler', function () {
          var button = selectSetpointButton(2, '-');

          expect(attributes.setpoints.set).not.toHaveBeenCalled();

          button.click();

          expect(attributes.setpoints.set).toHaveBeenCalled();

          var args = attributes.setpoints.set.calls.mostRecent().args;

          expect(args[0]).toEqual('cool');
          expect(args[1]).toEqual({
            value: 49,
            units: 'McDerps'
          });
        });

      });

      describe('the hotter button', function () {

        it('sets the heat setpoint to hotter', function () {
          var button = selectSetpointButton(2, '+');

          expect(attributes.setpoints.set).not.toHaveBeenCalled();

          button.click();

          expect(attributes.setpoints.set).toHaveBeenCalled();

          var args = attributes.setpoints.set.calls.mostRecent().args;

          expect(args[0]).toEqual('cool');
          expect(args[1]).toEqual({
            value: 51,
            units: 'McDerps'
          });
        });

      });

    });

  });

  function compileDirective(html) {
    var $compile = $injector.get('$compile');
    var element = $compile(html)($scope);
    $scope.$digest();

    return element;
  }

  function selectTemperatureDisplays() {
    return $(element).find('.thermostat-controls thermostat-single-temperature-controls');
  }

  function selectTemperatureDisplay(index) {
    return selectTemperatureDisplays().eq(index);
  }

  function selectTemperatureDisplayButton(index) {
    return selectTemperatureDisplay(index).find('.button');
  }

  function selectSetpointButton(index, label) {
    return selectTemperatureDisplay(index).find('.setpoint-button').filter(function () {
      return $(this).text().trim() === label;
    });
  }

});
