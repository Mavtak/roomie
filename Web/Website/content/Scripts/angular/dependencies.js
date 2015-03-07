angular.module('roomie.app', [
  'ui.router',
  'roomie.devices',
  'roomie.tasks'
]);

angular.module('roomie.common', [
]);

angular.module('roomie.devices', [
  'roomie.common'
]);
angular.module('roomie.tasks', [
  'roomie.common'
]);
