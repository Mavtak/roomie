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
  'ui.router',
  'roomie.common',
  'roomie.data'
]);

angular.module('roomie.tasks', [
  'ui.router',
  'roomie.common'
]);

angular.module('roomie.users', [
  'ui.router'
]);
