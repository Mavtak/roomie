function devices(
  AutomaticPollingDeviceListing
) {

  var result = new AutomaticPollingDeviceListing();

  result.run();

  //TODO: clear data on logout

  return result;

}

export default devices;
