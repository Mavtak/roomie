var module = angular.module('roomie.data');

module.factory('AutomaticPollingUpdater', ['$http', '$timeout', function($http, $timeout) {

  return function AutomaticPollingUpdater(options) {
    var self = this;

    self.url = options.url;
    self.originals = options.originals;
    self.setFunctions = options.setFunctions;

    self.start = function() {
      forever(function() {
        return getData()
          .success(applyUpdates)
          .then(wait);
      });
    };

    self.stop = function() {
      //TODO: write me!
    };

    function forever(promiseFactory) {
      return promiseFactory().then(function() {
        forever(promiseFactory);
      });
    }

    function getData() {
      return $http.get(self.url);
    }

    function applyUpdates(updates) {
      var originals = self.originals;

      for (var i = 0; i < updates.length; i++) {
        var update = updates[i];
        var original = getOriginal(originals, update);

        if (original === update) {
          addOriginal(originals, original);
        } else {
          applyUpdate(original, update);
        }
      }
    }

    function wait() {
      return $timeout(function() {
      }, 500);
    }

    function getOriginal(originals, update) {
      return _.find(originals, { id: update.id }) || update;
    }

    function addOriginal(originals, item) {
      self.setFunctions(item);
      originals.push(item);
    }

    function applyUpdate(original, update) {
      //TODO: delete data from original when it makes sense (functions on original should be preserved)
      //TODO: decide array behavior (which might be context dependent)

      _.merge(original, update, function(a, b) {
        if (typeof a === 'object' && typeof b === 'object') {
          return _.merge(a, b);
        }
      });
    }
  }
}]);
