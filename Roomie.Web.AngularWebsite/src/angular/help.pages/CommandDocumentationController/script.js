angular.module('roomie.help.pages').controller('CommandDocumentationController', function (
  $http,
  wholePageStatus
) {

  var controller = this;

  wholePageStatus.set('loading');

  $http.post('/api/commandDocumentation', {
    action: 'list',
  })
    .then(function (response) {
      controller.commands = response.data.data;

      wholePageStatus.set('ready');
    });

});
