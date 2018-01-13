function TasksController(
  $state,
  ManualPollingUpdater,
  pageMenuItems,
  signInState,
  wholePageStatus
  ) {

  var controller = this;

  wholePageStatus.set('loading');
  pageMenuItems.reset();
  initializeScope();
  connectData();

  function initializeScope() {
    controller.page = {
      items: []
    };
  }

  function connectData() {
    //TODO: move paging into ManualPoller
    var start = 0;
    var count = 10;

    if (typeof $state.params.start !== 'undefined') {
      start = $state.params.start;
    }

    if (typeof $state.params.count !== "undefined") {
      count = $state.params.count;
    }

    var data = new ManualPollingUpdater({
      repository: 'task',
      filter: {
        start: start,
        count: count,
      },
      originals: controller.page.items,
      ammendOriginal: processTask,
      processErrors: processErrors
    });

    data.run();
  }

  function processErrors(errors) {
    wholePageStatus.set('ready');

    var signInError = _.isArray(errors) && _.some(errors, {
      type: 'must-sign-in'
    });

    if (signInError) {
      signInState.set('signed-out');
    }

    //TODO: handle other errors
  }

  function processTask(task) {
    wholePageStatus.set('ready');
    signInState.set('signed-in');

    if (task.expiration) {
      task.expiration = new Date(task.expiration);
    }

    if (task.receivedTimestamp) {
      task.receivedTimestamp = new Date(task.receivedTimestamp);
    }

    if (task.script) {
      if (task.script.creationTimestamp) {
        task.script.creationTimestamp = new Date(task.script.creationTimestamp);
      }
    }
  }

}

export default TasksController;
