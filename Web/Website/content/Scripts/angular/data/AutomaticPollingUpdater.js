var module = angular.module('roomie.data');

module.factory('AutomaticPollingUpdater', ['$timeout', 'ManualPollingUpdater', function($timeout, ManualPollingUpdater) {

  return function AutomaticPollingUpdater(options) {
    var pollingUpdater = new ManualPollingUpdater(options);

    this.run = function() {
      forever(function() {
        return pollingUpdater.run()
          .then(wait);
      });
    };

    this.stop = function() {
      //TODO: write me!
    };

    function forever(promiseFactory) {
      return promiseFactory().then(function() {
        forever(promiseFactory);
      });
    }

    function wait() {
      return $timeout(function() {
      }, 500);
    }
  };

}]);
