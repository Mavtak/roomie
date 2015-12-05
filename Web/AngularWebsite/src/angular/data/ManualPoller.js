﻿var module = angular.module('roomie.data');

module.factory('ManualPoller', ['$http', function($http) {

  return function ManualPoller(options) {
    var url = options.url;
    var selectItems = options.itemSelector || defaultItemSelector;

    this.run = function () {
      //TODO: handle failures
      return $http.get(url)
        .then(selectHttpBody)
        .then(selectItems);
    };

    function selectHttpBody(response) {
      return response.data;
    }

    function defaultItemSelector(page) {
      return page.items;
    }
  };

}]);