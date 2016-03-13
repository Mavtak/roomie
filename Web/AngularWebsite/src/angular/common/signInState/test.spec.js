describe('angular roomie.common signInState (factory)', function () {
  var instance;

  beforeEach(angular.mock.module('roomie.common'));

  beforeEach(angular.mock.inject(function($injector) {
    instance = $injector.get('signInState');
  }));

  describe('its functions', function () {

    it('has these', function () {
      expect(_.functions(instance)).toEqual([
        'get',
        'set',
      ]);
    });

    describe('get', function () {

      it('initially returns "unknown"', function () {
        expect(instance.get()).toEqual('unknown');
      });

      it('returns whatever returns what is sent to set', function () {
        instance.set('signed-in');

        expect(instance.get()).toEqual('signed-in');
        expect(instance.get()).toEqual('signed-in');
        expect(instance.get()).toEqual('signed-in');

        instance.set('signed-out');

        expect(instance.get()).toEqual('signed-out');
        expect(instance.get()).toEqual('signed-out');
        expect(instance.get()).toEqual('signed-out');
      });

    });

    describe('set', function () {

      it('accepts "signed-in"', function () {
        instance.set('signed-in');
      });

      it('accepts "signed-out"', function () {
        instance.set('signed-out');
      });

      it('does not accept anything else', function () {
        var call = function () {
          instance.set('anything-else');
        };

        expect(call).toThrowError('invalid state "anything-else"');
      });

    });

  });

});
