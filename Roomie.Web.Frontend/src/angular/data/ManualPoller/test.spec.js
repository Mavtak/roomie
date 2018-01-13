describe('angular roomie.data ManualPoller (factory)', function () {

  var $q;
  var $timeout;
  var api;
  var ManualPoller;
  var items;

  beforeEach(angular.mock.module('roomie.data'));
  
  beforeEach(angular.mock.module(function ($provide) {
    api = jasmine.createSpy('api');

    $provide.value('api', api);
  }));

  beforeEach(angular.mock.inject(function ($injector) {
    $q = $injector.get('$q');
    $timeout = $injector.get('$timeout');
    ManualPoller = $injector.get('ManualPoller');
  }));

  beforeEach(function () {
    api.and.returnValue($q.when({}));

    items = [];
  });

  it('submits the API request', function () {
    var manualPoller = new ManualPoller({
      repository: 'derp'
    });

    manualPoller.run();

    expect(api).toHaveBeenCalledWith({
      repository: 'derp',
      action: 'list',
      parameters: undefined,
    })
  });

  describe('item selection', function () {

    it('selects the items property by default.', function () {
      var actual;
      var manualPoller = new ManualPoller({
        repository: 'derp'
      });

      api.and.returnValue($q.when({
        data: {
          items: [{ id: 'a' }, { id: 'b' }],
        }
      }));

      manualPoller.run().then(function (x) {
        actual = x;
      });

      $timeout.flush();

      expect(actual).toEqual([{ id: 'a' }, { id: 'b' }]);      
    });

    it('selects the items property with an optional override when provided.', function () {
      var actual;
      var manualPoller = new ManualPoller({
        repository: 'derp',
        itemSelector: function (response) {
          return response;
        }
      });

      api.and.returnValue($q.when({
        data: [{ id: 'a' }, { id: 'b' }],
      }));

      manualPoller.run().then(function (x) {
        actual = x;
      });

      $timeout.flush();

      expect(actual).toEqual([{ id: 'a' }, { id: 'b' }]);
    });

  });

  describe('error handling', function () {
    var theError;

    beforeEach(function () {
      api.and.returnValue($q.when({
        error: {
          something: 'a message maybe'
        }
      }));
    });

    describe('when the processErrors option is not set', function () {

      it('does not break', function () {
          var manualPoller = new ManualPoller({
            repository: 'derp',
          });

          manualPoller.run()

          $timeout.flush();
      });

    });

    describe('when the processErrors option is set', function () {

      it('calls it with the error data', function () {
        var processErrors = jasmine.createSpy('processErrors');

        var manualPoller = new ManualPoller({
          repository: 'derp',
          processErrors: processErrors
        });

        manualPoller.run();

        $timeout.flush();

        expect(processErrors.calls.count()).toEqual(1);
        expect(processErrors.calls.mostRecent().args[0]).toEqual({
          something: 'a message maybe'
        });
      });

    });

  });

});
