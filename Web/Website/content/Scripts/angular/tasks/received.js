var module = angular.module('roomie.tasks');

module.filter('received', function() {
  return function(task) {
    if (task.receivedTimestamp) {
      return task.receivedTimestamp.toLocaleString();
    }

    if (task.expired) {
      return 'Expired';
    }

    return '';
  };
});
