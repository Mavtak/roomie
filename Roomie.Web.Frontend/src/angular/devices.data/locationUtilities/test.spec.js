describe('angular roomie.devices.data locationUtilities (factory)', function () {
  var subject;

  beforeEach(angular.mock.module('roomie.devices.data'));

  beforeEach(angular.mock.inject(function ($injector) {
    subject = $injector.get('locationUtilities');
  }));

  describe('the functions', function () {

    it('has these', function () {
      expect(_.functions(subject)).toEqual([
        'calculatePageMenuItems',
        'extractFromDevices',
      ]);
    });

    describe('calculatePageMenuItems', function () {

      it('works', function () {
        var locations = [
          '',
          '',
          'a',
          'a/b',
          'a/b',
          'c/d/e',
        ];

        var result = subject.calculatePageMenuItems(locations);

        expect(result).toEqual([{
          indent: 0,
          label: 'a',
          target: '#/devices?location=a',
        }, {
          indent: 1,
          label: 'b',
          target: '#/devices?location=a/b',
        }, {
          indent: 0,
          label: 'c',
          target: '#/devices?location=c',
        }, {
          indent: 1,
          label: 'd',
          target: '#/devices?location=c/d',
        }, {
          indent: 2,
          label: 'e',
          target: '#/devices?location=c/d/e',
        }]);
      });

    });

    describe('extractFromDevices', function () {

      it('works', function () {
        var devices = [{
          location: {
            name: 'a',
          },
        }, {
          location: {
          name: 'a/b'
          },
        }, {
          location: {
            name: 'a/b'
          },
        }, {
          location: {
            name: 'c/d/e'
          },
        }, {
          location: {}
        }, {
        }];

        var result = subject.extractFromDevices(devices);

        expect(result).toEqual([
          'a',
          'a/b',
          'a/b',
          'c/d/e',
          '',
          '',
        ]);
      });

    });

  });

});
