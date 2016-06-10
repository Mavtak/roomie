fdescribe('angular roomie.devices.data deviceUtilities (factory)', function () {
  var $http;
  var subject;

  beforeEach(angular.mock.module('roomie.devices.data'));

  beforeEach(angular.mock.module(function ($provide) {
    $http = jasmine.createSpyObj('$http', ['post']);

    $provide.value('$http', $http);
  }));

  beforeEach(angular.mock.inject(function ($injector) {
    subject = $injector.get('deviceUtilities');
  }));

  describe('the functions', function () {

    it('has these', function () {
      expect(_.functions(subject)).toEqual([
        'parseTimestamps',
        'setActions',
      ]);
    });

    describe('parseTimestamps', function () {

      it('parses all of the timeStamp values', function () {
        var device = {
          binarySensor: {
              timeStamp: '2016-04-14T12:57:22.8413439Z',
          },
          humiditySensor: {
              timeStamp: '2016-04-1Z',
          },
          illuminanceSensor: {
              timeStamp: '2016-04-2Z',
          },
          powerSensor: {
              timeStamp: '2016-04-3Z',
          },
          temperatureSensor: {
              timeStamp: '2016-04-4Z',
          },
        };

        subject.parseTimestamps(device);

        expect(device).toEqual({
          binarySensor: {
              timeStamp: new Date(Date.UTC(2016, 3, 14, 12, 57, 22, 841)),
          },
          humiditySensor: {
              timeStamp: new Date(Date.UTC(2016, 3, 1)),
          },
          illuminanceSensor: {
              timeStamp: new Date(Date.UTC(2016, 3, 2)),
          },
          powerSensor: {
              timeStamp: new Date(Date.UTC(2016, 3, 3)),
          },
          temperatureSensor: {
              timeStamp: new Date(Date.UTC(2016, 3, 4)),
          },
        });

      });

      it('is fine with missing parts', function () {
        var device = {};

        subject.parseTimestamps(device);

        expect(device).toEqual({});
      });

      it('is fine with missing timestamps', function () {
        var device = {
          binarySensor: {},
          humiditySensor: {},
          illuminanceSensor: {},
          powerSensor: {},
          temperatureSensor: {},
        };

        subject.parseTimestamps(device);

        expect(device).toEqual({
          binarySensor: {},
          humiditySensor: {},
          illuminanceSensor: {},
          powerSensor: {},
          temperatureSensor: {},
        });

      });

    });

    describe('setActions', function () {
      var device;

      beforeEach(function () {
        device = {
          binarySensor: {},
          binarySwitch: {},
          colorSwitch: {},
          humiditySensor: {},
          id: 'the-device-id',
          illuminanceSensor: {},
          multilevelSwitch: {},
          powerSensor: {},
          temperatureSensor: {},
          thermostat: {
            core: {},
            fan: {},
            setpoints: {},
          },
        };
      });

      it('is fine with missing parts', function () {
        device = {};

        subject.setActions(device);

        expect(device).toEqual({
          hasThermostat: jasmine.any(Function),
        });
      });

      describe('adding device action functions', function () {

        it('adds binarySensor.poll', function () {
          subject.setActions(device);

          device.binarySensor.poll();

          expectApiCall('/api/device/the-device-id?action=PollBinarySensor');
        });

        it('adds binarySwitch.setPower', function () {
          var onOrOff = 'On';

          subject.setActions(device);

          device.binarySwitch.setPower(onOrOff);

          expectApiCall('/api/device/the-device-id?action=PowerOn');
        });

        it('adds colorSwitch.setValue', function () {
          subject.setActions(device);

          device.colorSwitch.setValue('some color');

          expectApiCall('/api/device/the-device-id?action=SetColor&color=some%20color');
        });

        it('adds humiditySensor.poll', function () {
          subject.setActions(device);

          device.humiditySensor.poll();

          expectApiCall('/api/device/the-device-id?action=PollHumiditySensor');
        });

        it('adds illuminanceSensor.poll', function () {
          subject.setActions(device);

          device.illuminanceSensor.poll();

          expectApiCall('/api/device/the-device-id?action=PollIlluminanceSensor');
        });

        it('adds multilevelSwitch.setPower', function () {
          subject.setActions(device);

          device.multilevelSwitch.setPower('bright or something');

          expectApiCall('/api/device/the-device-id?action=Dim&power=bright%20or%20something');
        });

        it('adds powerSensor.poll', function () {
          subject.setActions(device);

          device.powerSensor.poll();

          expectApiCall('/api/device/the-device-id?action=PollPowerSensor');
        });

        it('adds temperatureSensor.poll', function () {
          subject.setActions(device);

          device.temperatureSensor.poll();

          expectApiCall('/api/device/the-device-id?action=PollTemperatureSensor');
        });

        it('adds thermostat.core.set', function () {
          subject.setActions(device);

          device.thermostat.core.set('some mode');

          expectApiCall('/api/device/the-device-id?action=SetThermostatMode&mode=some%20mode');
        });

        it('adds thermostat.fan.set', function () {
          subject.setActions(device);

          device.thermostat.fan.set('some mode');

          expectApiCall('/api/device/the-device-id?action=SetThermostatFanMode&mode=some%20mode');
        });

        it('adds thermostat.setpoints.set', function () {
          var temperature = {
            value: 70,
            units: 'Derps',
          };

          subject.setActions(device);

          device.thermostat.setpoints.set('some-type', temperature);

          expectApiCall('/api/device/the-device-id?action=SetThermostatSetpoint&type=some-type&temperature=70%20Derps');
        });

        function expectApiCall(path) {
          expect($http.post).toHaveBeenCalledWith(path);
          expect($http.post.calls.count()).toEqual(1);
        }

      });

      describe('adding the hasThermostat function', function () {

        beforeEach(function () {
          subject.setActions(device);
        });

        it('adds the function', function () {
          expect(typeof device.hasThermostat).toEqual('function');
        });

        it('returns false when there is no thermostat object', function () {
          delete device.thermostat;

          expect(device.hasThermostat()).toEqual(false);
        });

        it('returns false when there is an empty thermostat object', function () {
          device.thermostat = {};

          expect(device.hasThermostat()).toEqual(false);
        });

        it('returns false when there is a thermostat object with empty components', function () {
          device.thermostat = {
            core: {
              supportedModes: [],
            },
            fan: {
              supportedModes: [],},
            setpoints: {},
          };

          expect(device.hasThermostat()).toEqual(false);
        });

        it('returns true if only the thermostat core currentAction is set', function () {
          device.thermostat = {
            core: {
              currentAction: 'some-derp',
            },
          };

          expect(device.hasThermostat()).toEqual(true);
        });

        it('returns true if only the thermostat core mode is set', function () {
          device.thermostat = {
            core: {
              mode: 'some-derp',
            },
          };

          expect(device.hasThermostat()).toEqual(true);
        });

        it('returns true if only the thermostat supported is set', function () {
          device.thermostat = {
            core: {
              supportedModes: ['some-derp'],
            },
          };

          expect(device.hasThermostat()).toEqual(true);
        });

        it('returns true if only the thermostat fan currentAction is set', function () {
          device.thermostat = {
            fan: {
              currentAction: 'some-derp',
            },
          };

          expect(device.hasThermostat()).toEqual(true);
        });

        it('returns true if only the thermostat fan mode is set', function () {
          device.thermostat = {
            fan: {
              mode: 'some-derp',
            },
          };

          expect(device.hasThermostat()).toEqual(true);
        });

        it('returns true if only the thermostat fan is set', function () {
          device.thermostat = {
            fan: {
              supportedModes: ['some-derp'],
            },
          };

          expect(device.hasThermostat()).toEqual(true);
        });

        it('returns true if only a thermostat setpoint (cool) is set', function () {
          device.thermostat = {
            setpoints: {
              cool: {}
            }
          };

          expect(device.hasThermostat()).toEqual(true);
        });

        it('returns true if only a thermostat setpoint (heat) is set', function () {
          device.thermostat = {
            setpoints: {
              heat: {}
            }
          };

          expect(device.hasThermostat()).toEqual(true);
        });

      });

    });

  });

});
