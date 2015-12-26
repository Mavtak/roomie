var module = angular.module('roomie.data');

module.factory('AutomaticPollingUpdater', ['$timeout', 'ManualPollingUpdater', function($timeout, ManualPollingUpdater) {

  return function AutomaticPollingUpdater(options) {
    var pollingUpdater = new ManualPollingUpdater(options);

    var running = false;

    this.run = function () {
      if (running) {
        throw {
          message: "this instance of AutomaticPollingUpdater is already running."
        };
      }

      running = true;

      forever(function () {
        if (running) {
          return pollingUpdater.run()
          .then(wait);
        }
      });
    };

    this.stop = function() {
      if (!running) {
        throw {
          message: "this instance of AutomaticPollingUpdater is already stopped."
        };
      }

      running = false;
    };

    function forever(promiseFactory) {
      return promiseFactory().then(function() {
        return forever(promiseFactory);
      });
    }

    function wait() {
      return $timeout(function() {
      }, 500);
    }
  };

}]);
