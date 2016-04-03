angular.module('roomie.help').controller('CommandDocumentationController', function (
  $http,
  wholePageStatus
) {

  var self = this;
  wholePageStatus.set('loading');

  $http.get('/api/commandDocumentation')
    .then(function (response) {
      self.commands = response.data;

      wholePageStatus.set('ready');
    });

});
