angular.module('roomie.data').factory('ManualPoller', function (
  $http
) {

  return ManualPoller;

  function ManualPoller(options) {
    var url = options.url;
    var processErrors = options.processErrors;
    var selectItems = options.itemSelector || defaultItemSelector;

    this.run = function () {
      var result = $http.get(url);

      if (processErrors) {
        result = result.error(processErrors);
      }

      result = result
        .then(selectHttpBody)
        .then(selectItems);

      return result;
    };

    function selectHttpBody(response) {
      return response.data;
    }

    function defaultItemSelector(page) {
      return page.items;
    }
  }

});
