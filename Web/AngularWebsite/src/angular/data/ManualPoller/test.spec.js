﻿describe('angular roomie.data ManualPoller (factory)', function () {

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

  it('GETs the provided URL', function () {
    var manualPoller = new ManualPoller({
      url: '/herp/derp.json'
    });

    $httpBackend.when('GET', '/herp/derp.json')
      .respond({});

    $httpBackend.expectGET('/herp/derp.json');

    manualPoller.run();

    $httpBackend.flush();

  });

  describe('item selection', function () {

    it('selects the items property by default.', function () {
      var actual;
      var manualPoller = new ManualPoller({
        url: '/herp/derp.json'
      });

      $httpBackend.when('GET', '/herp/derp.json')
        .respond({
          items: [{ id: 'a' }, { id: 'b' }]
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
        url: '/herp/derp.json',
        itemSelector: function (response) {
          return response;
        }
      });

      $httpBackend.when('GET', '/herp/derp.json')
        .respond([{ id: 'a' }, { id: 'b' }]);

      manualPoller.run().then(function (x) {
        actual = x;
      });

      $httpBackend.flush();

      expect(actual).toEqual([{ id: 'a' }, { id: 'b' }]);

    });

  });

});
