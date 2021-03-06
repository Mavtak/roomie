﻿describe('angular roomie.task received (filter)', function () {
  var filter;

  beforeEach(angular.mock.module('roomie.tasks'));

  beforeEach(inject(function ($filter) {
    filter = $filter('received');
  }));

  it('works when given {receivedTimestamp: some date, expired: false}', function () {
    var result = filter({
      receivedTimestamp: new Date(2015, 1, 5, 3, 39, 25),
      expired: false
    });

    expect(result).toEqual('2/5/2015, 3:39:25 AM');
  });

  it('works when given {receivedTimestamp: undefined, expired: true}', function () {
    var result = filter({
      expired: true
    });

    expect(result).toEqual('Expired');
  });

  describe('cases that should not ever happen', function () {

    it('works when given {receivedTimestamp: some date, expired: true}', function () {
      var result = filter({
        receivedTimestamp: new Date(2015, 1, 5, 3, 39, 25),
        expired: false
      });

      expect(result).toEqual('2/5/2015, 3:39:25 AM');
    });

    it('works when given {receivedTimestamp: undefined, expired: false}', function () {
      var result = filter({
        expired: false
      });

      expect(result).toEqual('');
    });

  });

});
