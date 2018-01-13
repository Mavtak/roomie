function ManualPoller(
  api
) {

  return ManualPoller;

  function ManualPoller(options) {
    var repository = options.repository;
    var filter = options.filter;
    var processErrors = options.processErrors;
    var selectItems = options.itemSelector || defaultItemSelector;

    this.run = function () {
      var result = api({
        repository: repository,
        action: 'list',
        parameters: filter,
      }).then(function (response) {
        if (response.error) {
          if (processErrors) {
            return processErrors(response.error);
          } else {
            return;
          }
        }

        var body = selectHttpBody(response);
        var items = selectItems(body);

        return items;
      });      

      return result;
    };

    function selectHttpBody(response) {
      return response.data;
    }

    function defaultItemSelector(page) {
      return page.items;
    }
  }

}

export default ManualPoller;
