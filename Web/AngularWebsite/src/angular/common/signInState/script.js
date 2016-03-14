angular.module('roomie.common').factory('signInState', function () {
  return new SignInState();

  function SignInState() {
    var _validStates = ['signed-in', 'signed-out'];
    var _state = 'unknown';

    this.get = get;
    this.set = set;

    function get(state) {
      return _state;
    }

    function set(state) {
      if (!_.contains(_validStates, state)) {
        throw new Error('invalid state "' + state + '"');
      }

      _state = state;
    }
  }

});
