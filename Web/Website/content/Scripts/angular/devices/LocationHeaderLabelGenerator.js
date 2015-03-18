var module = angular.module('roomie.devices');

module.factory('LocationHeaderLabelGenerator', function() {

  return function LocationHeaderLabelGenerator(previousLocation, currentLocation) {
    this.getParts = function() {
      var previous = getParts(previousLocation);
      var current = getParts(currentLocation);

      var depth = eatCommonBeginnings(previous, current);

      var result = [];

      for (var i = 0; i < current.length; i++) {
        result.push({
          depth: depth + i,
          label: current[i]
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
    var depth = 0;

    while (previous[0] === current[0] && typeof previous[0] !== 'undefined') {
      depth++;
      previous.shift();
      current.shift();
    }

    return depth;
  }

});
