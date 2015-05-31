/// <reference path="../../../Scripts/Libraries/jquery-1.5.1.min.js"/>
/// <reference path="../../../Scripts/Libraries/angular-1.3.13.min.js"/>
/// <reference path="../angular-mocks.js"/>
/// <reference path="../../../Scripts/angular/dependencies.js"/>
/// <reference path="../../../Scripts/angular/devices/currentActionControls.js"/>

describe('roomie.devices.currentActionControls', function() {
  var $compile;
  var $rootScope;
  var attributes;
  var element;

  beforeEach(angular.mock.module('roomie.devices'));

  beforeEach(angular.mock.inject(function($injector) {
    $compile = $injector.get('$compile');
    $rootScope = $injector.get('$rootScope');
  }));

  beforeEach(function() {
    element = $compile('<current-action-controls current-action="attributes.currentAction"></current-action-controls>')($rootScope);

    attributes = {
      currentAction: 'idle or something'
    };

    $rootScope.attributes = attributes;
    $rootScope.$digest();
  });

  describe('the text', function() {
    var text;

    beforeEach(readText);

    it('is bound to the current-action attribute', function() {
      expect(text).toEqual('idle or something');

      attributes.currentAction = 'running or whatever else';
      readText();
      expect(text).toEqual('running or whatever else');
    });

    function readText() {
      $rootScope.$digest();
      text = $(element).find('.group').text();
    }
  });

});
