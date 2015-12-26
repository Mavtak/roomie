describe('roomie.common.widgetButtonGroup', function () {
  var $compile;
  var $rootScope;

  beforeEach(angular.mock.module('roomie.common'));

  beforeEach(angular.mock.inject(function ($injector) {
    $compile = $injector.get('$compile');
    $rootScope = $injector.get('$rootScope');
  }));

  it('works', function () {
    var element = $compile('<widget-button-group><div class="thingy">bam</div></widget-button-group>')($rootScope);
    $rootScope.$digest();

    expect($(element).find('.buttonGroup .thingy').html()).toEqual('bam');
  });

});
