angular.module('roomie.app', [
  'ui.router',
  'roomie.devices',
  'roomie.tasks',
  'roomie.users'
]);

angular.module('roomie.common', [
   'roomie.templates'
]);

angular.module('roomie.data', [
]);

angular.module('roomie.devices', [
  'ui.router',
  'roomie.common',
  'roomie.data',
  'roomie.templates'
]);

angular.module('roomie.tasks', [
  'ui.router',
  'roomie.common',
  'roomie.templates'
]);

angular.module('roomie.users', [
  'ui.router',
  'roomie.templates'
]);
