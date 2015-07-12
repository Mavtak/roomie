angular.module('roomie.app', [
  'ui.router',
  'roomie.devices',
  'roomie.tasks',
  'roomie.users'
]);

angular.module('roomie.common', [
]);

angular.module('roomie.data', [
]);

angular.module('roomie.devices', [
  'roomie.common',
  'roomie.data'
]);

angular.module('roomie.tasks', [
  'roomie.common'
]);

angular.module('roomie.users', [
]);
