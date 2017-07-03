angular.module('roomie.help.pages').controller('CommandDocumentationController', function (
  $http,
  wholePageStatus
) {

  var controller = this;

  wholePageStatus.set('loading');

  $http.get('/api/commandDocumentation')
    .then(function (response) {
      controller.commands = response.data;

      wholePageStatus.set('ready');
    });

});
