angular.module('roomie.data').factory('ManualPollingUpdater', function (
  ManualPoller,
  ManualUpdater
) {

  return ManualPollingUpdater;

  function ManualPollingUpdater(options) {
    var poller = new ManualPoller(options);
    var updater = new ManualUpdater(options);

    this.run = function() {
      return poller.run()
        .then(updater.run);
    };
  }

});
