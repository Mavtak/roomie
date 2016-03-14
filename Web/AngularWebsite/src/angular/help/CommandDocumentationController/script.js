angular.module('roomie.help').controller('CommandDocumentationController', function (
  $http,
  wholePageStatus
) {

  var self = this;
  wholePageStatus.set('loading');

  $http.get('/api/commandDocumentation')
    .then(function (response) {
      console.log('derp');
      self.commands = response.data;

      wholePageStatus.set('ready');
    });

});
