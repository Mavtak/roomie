angular.module('roomie.tasks').controller('TasksController', ['$http', '$scope', 'ManualPollingUpdater', 'pageMenuItems', 'wholePageStatus', function ($http, $scope, ManualPollingUpdater, pageMenuItems, wholePageStatus) {

  wholePageStatus.set('loading');
  pageMenuItems.reset();
  initializeScope();
  connectData();

  function initializeScope() {
    $scope.page = {
      items: []
    };
  }

  function connectData() {
    //TODO: move paging into ManualPoller
    var start = 0;
    var count = 10;

    if (typeof $scope.$state.params.start !== 'undefined') {
      start = $scope.$state.params.start;
    }

    if (typeof $scope.$state.params.count !== "undefined") {
      count = $scope.$state.params.count;
    }

    var data = new ManualPollingUpdater({
      url: '/api/task?start=' + start + '&count=' + count,
      originals: $scope.page.items,
      ammendOriginal: processTask
    });

    data.run();
  }

  function processTask(task) {
    wholePageStatus.set('ready');

    if (task.expiration) {
      task.expiration = new Date(task.expiration);
    }

    if (task.receivedTimestamp) {
      task.receivedTimestamp = new Date(task.receivedTimestamp);
    }

    if (task.script) {
      if (task.script.creationTimestamp) {
        task.script.creationTimestamp = new Date(task.script.creationTimestamp);
      }
    }
  }
}]);
