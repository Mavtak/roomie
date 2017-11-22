function computerWidget() {

  return {
    restrict: 'E',
    scope: {
      computer: '=computer',
      showEdit: '=showEdit'
    },
    templateUrl: 'computers/computer-widget/template.html'
  };

}

export default computerWidget;
