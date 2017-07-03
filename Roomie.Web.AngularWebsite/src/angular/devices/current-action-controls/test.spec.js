describe('angular roomie.devices current-action-controls (directive)', function () {
  var $injector;
  var $scope;
  var attributes;
  var element;

  beforeEach(angular.mock.module('roomie.devices'));

  beforeEach(angular.mock.inject(function (_$injector_) {
    $injector = _$injector_;
    $scope = $injector.get('$rootScope').$new();
  }));

  beforeEach(function () {
    attributes = {
      currentAction: 'idle or something'
    };

    $scope.attributes = attributes;

    element = compileDirective('<current-action-controls current-action="attributes.currentAction"></current-action-controls>');
  });

  describe('the text', function () {
    var text;

    beforeEach(readText);

    it('is bound to the current-action attribute', function () {
      expect(text).toEqual('idle or something');

      attributes.currentAction = 'running or whatever else';
      readText();
      expect(text).toEqual('running or whatever else');
    });

    function readText() {
      $scope.$digest();
      text = $(element).find('.group').text().trim();
    }

  });

  function compileDirective(html) {
    var $compile = $injector.get('$compile');
    var element = $compile(html)($scope);
    $scope.$digest();

    return element;
  }

});
