describe('angular roomie.common widget-button-group (directive)', function () {
  var $injector;
  var $scope;

  beforeEach(angular.mock.module('roomie.common'));

  beforeEach(angular.mock.inject(function (_$injector_) {
    $injector = _$injector_;
    $scope = $injector.get('$rootScope').$new();
  }));

  it('works', function () {
    var element = compileDirective('<widget-button-group><div class="thingy">bam</div></widget-button-group>');

    expect($(element).find('.buttonGroup .thingy').html()).toEqual('bam');
  });

  function compileDirective(html) {
    var $compile = $injector.get('$compile');
    var element = $compile(html)($scope);
    $scope.$digest();

    return element;
  }

});
