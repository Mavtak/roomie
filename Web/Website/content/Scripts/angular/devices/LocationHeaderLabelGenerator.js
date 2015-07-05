var module = angular.module('roomie.devices');

module.factory('LocationHeaderLabelGenerator', function() {

  return function LocationHeaderLabelGenerator(previousLocation, currentLocation) {
    this.getParts = function() {
      var previous = getParts(previousLocation);
      var current = getParts(currentLocation);

      var commonBeginnings = eatCommonBeginnings(previous, current);

      var result = [];

      for (var i = 0; i < current.length; i++) {
        var location = commonBeginnings.concat(current.slice(0, i + 1)).join('/');

        result.push({
          depth: commonBeginnings.length + i,
          label: current[i],
          location: location
        });
      }

      return result;
    };
  };

  function getParts(location) {
    if (typeof location === 'undefined') {
      return [];
    }

    if (typeof location !== 'string') {
      console.error(typeof location);
    }

    return location.split('/');
  }

  function eatCommonBeginnings(previous, current) {
    var common = [];

    while (previous[0] === current[0] && typeof previous[0] !== 'undefined') {
      common.push(previous[0]);
      previous.shift();
      current.shift();
    }

    return common;
  }

});
