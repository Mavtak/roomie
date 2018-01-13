function AutomaticPollingUpdater(
  $timeout,
  ManualPollingUpdater
) {

  return AutomaticPollingUpdater;

  function AutomaticPollingUpdater(options) {
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

    this.stop = function () {
      if (!running) {
        throw {
          message: "this instance of AutomaticPollingUpdater is already stopped."
        };
      }

      running = false;
    };

    function forever(promiseFactory) {
      var promise = promiseFactory();

      if (typeof promise === 'undefined') {
        return;
      }

      return promise.then(function () {
          return forever(promiseFactory);
      }, function () {
          return forever(promiseFactory);
      });
    }

    function wait() {
      return $timeout(function () {
      }, 1000);
    }
  }

}

export default AutomaticPollingUpdater;
