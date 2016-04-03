angular.module('roomie.tasks').filter('received', function () {

  return function (task) {
    if (task.receivedTimestamp) {
      return task.receivedTimestamp.toLocaleString();
    }

    if (task.expired) {
      return 'Expired';
    }

    return '';
  };

});
