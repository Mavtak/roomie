angular.module('roomie.common').factory('wholePageStatus', function () {

  return new WholePageLoaderControls();

  function WholePageLoaderControls() {
    var currentStatus = 'ready';
    var validStatuses = ['loading', 'ready'];

    this.get = function () {
      return currentStatus;
    };

    this.set = function (status) {
      if (!_.contains(validStatuses, status)) {
        throw new Error('invalid status "' + status + '"');
      }

      currentStatus = status;
    };
  }

});
