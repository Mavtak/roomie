angular.module('roomie.app').run(function (
  $rootScope,
  $state
  ) {
  $rootScope.$state = $state;
});
