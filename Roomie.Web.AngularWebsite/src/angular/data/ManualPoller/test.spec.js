describe('angular roomie.data ManualPoller (factory)', function () {

  var $httpBackend;
  var ManualPoller;
  var items;

  beforeEach(angular.mock.module('roomie.data'));

  beforeEach(angular.mock.inject(function ($injector) {
    $httpBackend = $injector.get('$httpBackend');
    ManualPoller = $injector.get('ManualPoller');
  }));

  beforeEach(function () {
    items = [];
  });

  it('POSTs the provided resource', function () {
    var manualPoller = new ManualPoller({
      repository: 'derp'
    });

    $httpBackend.when('POST', '/api/derp')
      .respond({
        data: {}
      });

    $httpBackend.expectPOST('/api/derp');

    manualPoller.run();

    $httpBackend.flush();
  });

  describe('item selection', function () {

    it('selects the items property by default.', function () {
      var actual;
      var manualPoller = new ManualPoller({
        repository: 'derp'
      });

      $httpBackend.when('POST', '/api/derp')
        .respond({
          data: {
            items: [{ id: 'a' }, { id: 'b' }],
          }
        });

      manualPoller.run().then(function (x) {
        actual = x;
      });

      $httpBackend.flush();

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

      $httpBackend.when('POST', '/api/derp')
        .respond({
          data: [{ id: 'a' }, { id: 'b' }],
        });

      manualPoller.run().then(function (x) {
        actual = x;
      });

      $httpBackend.flush();

      expect(actual).toEqual([{ id: 'a' }, { id: 'b' }]);
    });

  });

  describe('error handling', function () {
    var theError;

    beforeEach(function () {
      $httpBackend.when('POST', '/api/derp')
        .respond(200, {
            error: {
              something: 'a message maybe'
            }
        });
    });

    describe('when the processErrors option is not set', function () {

      it('does not break', function (done) {
          var manualPoller = new ManualPoller({
            repository: 'derp',
          });

          manualPoller.run()
            .catch(done);

          $httpBackend.flush();
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

        $httpBackend.flush();

        expect(processErrors.calls.count()).toEqual(1);
        expect(processErrors.calls.mostRecent().args[0]).toEqual({
          something: 'a message maybe'
        });
      });

    });

  });

});
