angular.module('roomie.devices.data').factory('AutomaticPollingDeviceListing', function (
  AutomaticPollingUpdater,
  deviceUtilities,
  signInState,
  wholePageStatus
) {

  return AutomaticPollingDeviceListing;

  function AutomaticPollingDeviceListing(options) {
    var _options = options || {};

    var _data;
    var _page = {
      items: []
    };
    var _locations;
    var _ready = false;

    Object.defineProperty(this, 'page', {
      get: function () { return _page; }
    });
    Object.defineProperty(this, 'ready', {
      get: function () { return _ready; }
    });

    this.run = function () { _data.run(); };
    this.stop = function () { _data.stop(); };

    init();

    function calculateOptions() {
      var path = '/api/device';

      if (_options.examples) {
        path += "?examples=true";
      }

      var options = {
        url: path,
        originals: _page.items,
        ammendOriginal: function (x) {
          deviceUtilities.setActions(x);
        },
        processErrors: function (x) {
          wholePageStatus.set('ready');

          var signInError = _.isArray(x) && _.some(x, {
            type: 'must-sign-in'
          });

          if (signInError) {
            signInState.set('signed-out');
          }

          //TODO: handle other errors
        },
        processUpdate: function (x) {
          deviceUtilities.parseTimestamps(x);
        },
        updateComplete: function () {
          signInState.set('signed-in');

          _ready = true;
        }
      };

      options.itemSelector = function (x) { return x; };

      return options;
    }

    function init() {
      var options = calculateOptions();
      _data = new AutomaticPollingUpdater(options);
    }
  }

});
