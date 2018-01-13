describe('roomie.common.wholePageStatus', function () {

  var wholePageLoaderControls;

  beforeEach(angular.mock.module('roomie.common'));

  beforeEach(angular.mock.inject(function ($injector) {
    wholePageLoaderControls = $injector.get('wholePageStatus');
  }));

  describe('get', function () {

    it('is a function', function () {
      expect(typeof wholePageLoaderControls.get).toEqual('function');
    });

    it('initially returns "ready"', function () {
      expect(wholePageLoaderControls.get()).toEqual('ready');
    });

    it('returns whatever was sent to set', function () {
      wholePageLoaderControls.set('loading');
      expect(wholePageLoaderControls.get()).toEqual('loading');
      expect(wholePageLoaderControls.get()).toEqual('loading');
      expect(wholePageLoaderControls.get()).toEqual('loading');

      wholePageLoaderControls.set('loading');
      expect(wholePageLoaderControls.get()).toEqual('loading');
      expect(wholePageLoaderControls.get()).toEqual('loading');
      expect(wholePageLoaderControls.get()).toEqual('loading');

      wholePageLoaderControls.set('ready');
      expect(wholePageLoaderControls.get()).toEqual('ready');
      expect(wholePageLoaderControls.get()).toEqual('ready');
      expect(wholePageLoaderControls.get()).toEqual('ready');
    });

  });

  describe('set', function () {

    it('is a function', function () {
      expect(typeof wholePageLoaderControls.set).toEqual('function');
    });

    it('accepts "loading"', function () {
      wholePageLoaderControls.set('loading');
    });

    it('accepts "ready"', function () {
      wholePageLoaderControls.set('ready');
    });

    it('does not accept "anything-else"', function () {
      var call = function () {
        wholePageLoaderControls.set('anything-else');
      };

      expect(call).toThrowError('invalid status "anything-else"');
    });

  });

});
