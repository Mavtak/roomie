angular.module('roomie.app', [
  'roomie.devices',
  'roomie.devices.pages',
  'roomie.help.pages',
  'roomie.tasks',
  'roomie.tasks.pages',
  'roomie.users',
  'roomie.users.pages',
]);

angular.module('roomie.common', [
   'roomie.templates',
]);

angular.module('roomie.data', [
]);

angular.module('roomie.devices', [
  'ui.router',
  'roomie.common',
  'roomie.data',
  'roomie.templates',
]);

angular.module('roomie.devices.pages', [
  'ui.router',
]);

angular.module('roomie.help.pages', [
  'roomie.common',
  'roomie.templates',
]);

angular.module('roomie.tasks', [
  'roomie.common',
  'roomie.templates',
]);

angular.module('roomie.tasks.pages', [
  'ui.router',
]);

angular.module('roomie.users', [
  'roomie.templates',
]);

angular.module('roomie.users.pages', [
  'ui.router',
  'roomie.templates',
]);
