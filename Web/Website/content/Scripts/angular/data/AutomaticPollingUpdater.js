var module = angular.module('roomie.data');

module.factory('AutomaticPollingUpdater', ['$timeout', 'ManualPollingUpdater', function($timeout, ManualPollingUpdater) {

  return function AutomaticPollingUpdater(options) {
    var pollingUpdater = new ManualPollingUpdater(options);

    var running;

    this.run = function () {
      running = true;

      forever(function () {
        if (running) {
          return pollingUpdater.run()
          .then(wait);
        }
      });
    };

    this.stop = function() {
      running = false;
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
