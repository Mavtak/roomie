angular.module('roomie.data').factory('ManualPoller', function (
  $http
) {

  return ManualPoller;

  function ManualPoller(options) {
    var repository = options.repository;
    var filter = options.filter;
    var processErrors = options.processErrors;
    var selectItems = options.itemSelector || defaultItemSelector;

    this.run = function () {
      var result = $http.post('/api/' + repository, {
        action: 'list',
        parameters: filter,
      }).then(function (response) {
        if (processErrors && response.data.error) {
          return processErrors(response.data.error);
        }

        var body = selectHttpBody(response);
        var items = selectItems(body);

        return items;
      });      

      return result;
    };

    function selectHttpBody(response) {
      return response.data.data;
    }

    function defaultItemSelector(page) {
      return page.items;
    }
  }

});
