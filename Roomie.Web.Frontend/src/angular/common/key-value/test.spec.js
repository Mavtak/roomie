describe('angular roomie.common key-value (directive)', function () {
  var $injector;
  var $scope;

  beforeEach(angular.mock.module('roomie.common'));

  beforeEach(angular.mock.inject(function (_$injector_) {
    $injector = _$injector_;
    $scope = $injector.get('$rootScope').$new();
  }));

  it('works given a key and value', function () {
    var element = compileDirective('<key-value key="herp" value="derp"></key-value>');

    expect($(element).find('.item')[0]).toBeDefined();
    expect($(element).find('.item .key').html()).toEqual('herp:');
    expect($(element).find('.item .value').html()).toEqual('derp');
    expect($(element).find('.item .value').attr('href')).toEqual(undefined);
  });

  it('works given a key and value and href', function () {
    var element = compileDirective('<key-value key="herp" value="derp" href="http://localhost/bam"></key-value>');

    expect($(element).find('.item')[0]).toBeDefined();
    expect($(element).find('.item .key').html()).toEqual('herp:');
    expect($(element).find('.item .value').html()).toEqual('derp');
    expect($(element).find('.item .value').attr('href')).toEqual('http://localhost/bam');
  });

  function compileDirective(html) {
    var $compile = $injector.get('$compile');
    var element = $compile(html)($scope);
    $scope.$digest();

    return element;
  }

});
