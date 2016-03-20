angular.module('roomie.common').directive('roomieBot', function () {

  return {
    restrict: 'E',
    scope: {
      mood: '=mood',
    },
    templateUrl: 'common/roomie-bot/template.html',
  };

});
