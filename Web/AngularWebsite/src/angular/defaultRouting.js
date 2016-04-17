angular.module('roomie.app').config(function (
  $stateProvider
) {

  $stateProvider.state('default', {
    url: '',
    controller: function ($state) {
      $state.go('help/about');
    }
  });

});
