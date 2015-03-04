/// <reference path="../../../Scripts/Libraries/jquery-1.5.1.min.js"/>
/// <reference path="../../../Scripts/Libraries/angular-1.3.13.min.js"/>
/// <reference path="../angular-mocks.js"/>
/// <reference path="../../../Scripts/angular/dependencies.js"/>
/// <reference path="../../../Scripts/angular/tasks/received.js"/>

describe('roomie.task.received', function() {
  var filter;
  
  beforeEach(angular.mock.module('roomie.tasks'));

  beforeEach(inject(function($filter) {
    filter = $filter('received');
  }));

  it('works when given {receivedTimestamp: some date, expired: false}', function () {
    var result = filter({
      receivedTimestamp: new Date('2015-02-05T03:39:25.682'),
      expired: false
    });

    expect(result).toEqual('2/4/2015, 10:39:25 PM');
  });

  it('works when given {receivedTimestamp: undefined, expired: true}', function () {
    var result = filter({
      expired: true
    });

    expect(result).toEqual('Expired');
  });

  describe('cases that should not ever happen', function() {

    it('works when given {receivedTimestamp: some date, expired: true}', function() {
      var result = filter({
        receivedTimestamp: new Date('2015-02-05T03:39:25.682'),
        expired: false
      });

      expect(result).toEqual('2/4/2015, 10:39:25 PM');
    });

    it('works when given {receivedTimestamp: undefined, expired: false}', function() {
      var result = filter({
        expired: false
      });

      expect(result).toEqual('');
    });

  });

});