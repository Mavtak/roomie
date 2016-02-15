describe('angular roomie.common widget (directive)', function () {
  var $compile;
  var $rootScope;

  beforeEach(angular.mock.module('roomie.common'));

  beforeEach(angular.mock.inject(function ($injector) {
    $compile = $injector.get('$compile');
    $rootScope = $injector.get('$rootScope');
  }));

  it('works', function () {
    var element = $compile('<widget><div class="thingy">bam</div></widget>')($rootScope);
    $rootScope.$digest();

    expect($(element).find('.widget .content .thingy').html()).toEqual('bam');
  });

});
