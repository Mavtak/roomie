angular.module('roomie.app', [
  'roomie.computers.pages',
  'roomie.devices',
  'roomie.devices.pages',
  'roomie.help.pages',
  'roomie.networks',
  'roomie.networks.pages',
  'roomie.tasks',
  'roomie.tasks.pages',
  'roomie.users',
  'roomie.users.pages',
]);

angular.module('roomie.common', [
   'roomie.templates',
]);

angular.module('roomie.computers.pages', [
  'ui.router',
]);

angular.module('roomie.data', [
]);

angular.module('roomie.devices', [
  'ui.router',
  'roomie.common',
  'roomie.data',
  'roomie.templates',
]);

angular.module('roomie.devices.data', [
  'roomie.data',
  'roomie.devices',
]);

angular.module('roomie.devices.pages', [
  'ui.router',
  'roomie.devices',
  'roomie.devices.data',
]);

angular.module('roomie.help.pages', [
  'roomie.common',
  'roomie.templates',
]);

angular.module('roomie.networks', [
  'roomie.common',
  'roomie.templates',
]);

angular.module('roomie.networks.pages', [
  'roomie.networks',
  'ui.router',
]);

angular.module('roomie.tasks', [
  'roomie.common',
  'roomie.templates',
]);

angular.module('roomie.tasks.pages', [
  'ui.router',
  'roomie.tasks',
]);

angular.module('roomie.users', [
  'roomie.templates',
]);

angular.module('roomie.users.pages', [
  'ui.router',
  'roomie.templates',
]);
