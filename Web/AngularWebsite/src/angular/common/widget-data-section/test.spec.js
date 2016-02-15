describe('angular roomie.common widget-data-section (directive)', function () {
  var $compile;
  var $rootScope;

  beforeEach(angular.mock.module('roomie.common'));

  beforeEach(angular.mock.inject(function ($injector) {
    $compile = $injector.get('$compile');
    $rootScope = $injector.get('$rootScope');
  }));

  it('works', function () {
    var element = $compile('<widget-data-section><div class="thingy">bam</div></widget-data-section>')($rootScope);
    $rootScope.$digest();

    expect($(element).find('.data .thingy').html()).toEqual('bam');
  });

});
