function ComputerDetailController(
  $state,
  api,
  pageMenuItems,
  signInState,
  wholePageStatus
) {

  var controller = this;

  wholePageStatus.set('loading');
  pageMenuItems.reset();

  api({
    repository: 'computer',
    action: 'read',
    parameters: {
      id: +$state.params.id
    }
  })
    .then(function (result) {
      if (result.data) {
        controller.computer = result.data;

        signInState.set('signed-in');
        wholePageStatus.set('ready');
      }

      if (result.error) {
        var error = result.error;
        
        var signInError = _.isArray(error.types) && _.includes(error.types, 'must-sign-in');
  
        if (signInError) {
          signInState.set('signed-out');
        }
  
        wholePageStatus.set('ready');
  
        //TODO: handle other errors
      }
    });
      
}

export default ComputerDetailController;
