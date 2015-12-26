angular.module('roomie.data').factory('ManualUpdater', function () {

  return function ManualUpdater(options) {
    var originals = options.originals;
    var ammendOriginal = options.ammendOriginal;
    var processUpdate = options.processUpdate;
    var updateComplete = options.updateComplete;

    this.run = function(updates) {
      for (var i = 0; i < updates.length; i++) {
        var update = updates[i];

        if (typeof processUpdate !== 'undefined') {
          processUpdate(update);
        }

        var original = getOriginal(update);

        if (original === update) {
          addOriginal(original);
        } else {
          applyUpdate(original, update);
        }
      }

      if (typeof updateComplete !== 'undefined') {
        updateComplete();
      }
    };

    function getOriginal(update) {
      return _.find(originals, { id: update.id }) || update;
    }

    function addOriginal(original) {
      if (typeof ammendOriginal !== 'undefined') {
        ammendOriginal(original);
      }

      originals.push(original);
    }

    function applyUpdate(original, update) {
      //TODO: delete data from original when it makes sense (functions on original should be preserved)
      //TODO: decide array behavior (which might be context dependent)

      _.merge(original, update);
    }
  };

});
