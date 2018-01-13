function CommandDocumentationController(
  api,
  wholePageStatus
) {

  var controller = this;

  wholePageStatus.set('loading');

  api({
    repository: 'commandDocumentation',
    action: 'list',
  })
    .then(function (response) {
      controller.commands = response.data;

      wholePageStatus.set('ready');
    });

}

export default CommandDocumentationController;
