angular.module('roomie.devices.data').factory('devices', function (
  AutomaticPollingDeviceListing
) {

  var result = new AutomaticPollingDeviceListing();

  result.run();

  //TODO: clear data on logout

  return result;

});
