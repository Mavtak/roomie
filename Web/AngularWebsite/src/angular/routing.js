angular.module('roomie.app').run(['$rootScope', '$state', function ($rootScope, $state) {
  $rootScope.$state = $state;
}]);
