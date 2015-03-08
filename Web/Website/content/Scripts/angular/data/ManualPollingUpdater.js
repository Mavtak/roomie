var module = angular.module('roomie.data');

module.factory('ManualPollingUpdater', ['ManualPoller', 'ManualUpdater', function(ManualPoller, ManualUpdater) {

  return function ManualPollingUpdater(options) {
    var poller = new ManualPoller(options);
    var updater = new ManualUpdater(options);

    this.run = function() {
      return poller.run()
        .success(updater.run);
    };
  };

}]);
