var module = angular.module('roomie.data');

module.factory('ManualPoller', ['$http', function($http) {

  return function ManualPoller(options) {
    var url = options.url;

    this.run = function() {
      return $http.get(url);
    };
  };

}]);
