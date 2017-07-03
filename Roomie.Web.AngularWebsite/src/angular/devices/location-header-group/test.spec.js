describe('angular roomie.devices location-header-group (directive)', function () {
  var $injector;
  var $scope;
  var givenCurrentLocation;
  var givenPreviousLocation;
  var parts;

  beforeEach(angular.mock.module('roomie.devices', function ($provide) {
    $provide.factory('LocationHeaderLabelGenerator', function () {
      return MockLocationHeaderLabelGenerator;
    });
  }));

  beforeEach(angular.mock.inject(function (_$injector_) {
    $injector = _$injector_;
    $scope = $injector.get('$rootScope').$new();
  }));

  beforeEach(function () {
    givenCurrentLocation = null;
    givenPreviousLocation = null;
    parts = null;
  });

  it('gives the previous and current locations to the LocationHeaderLabelGenerator', function () {
    $scope.previous = { name: 'herp' };
    $scope.current = { name: 'derp' };

    compileDirective('<location-header-group previous-location="previous" current-location="current"></location-header-group>');

    expect(givenPreviousLocation).toBe($scope.previous);
    expect(givenCurrentLocation).toBe($scope.current);
  });

  describe('when getParts returns an empty array', function () {

    it('renders no labels', function () {
      parts = [];

      var element = compileDirective('<location-header-group></location-header-group>');

      expect(getHeaders(element).length).toEqual(0);
    });

  });

  describe('when getParts returns a single entry', function () {

    it('renders the label', function () {
      parts = [{
        label: 'Derp',
        depth: 0
      }];

      var element = compileDirective('<location-header-group></location-header-group>');

      expect(getHeaders(element).length).toEqual(1);
    });

    it('renders depth = 0 as an H2', function () {
      parts = [{ depth: 0 }];

      var element = compileDirective('<location-header-group></location-header-group>');

      expect(getHeader(element, 0).get(0).tagName).toEqual('H2');
    });

    it('renders depth = 1 as an H3', function () {
      parts = [{ depth: 1 }];

      var element = compileDirective('<location-header-group></location-header-group>');

      expect(getHeader(element, 0).get(0).tagName).toEqual('H3');
    });

    it('renders depth = 2 as an H4', function () {
      parts = [{ depth: 2 }];

      var element = compileDirective('<location-header-group></location-header-group>');

      expect(getHeader(element, 0).get(0).tagName).toEqual('H4');
    });

    it('renders depth = 3 as an H5', function () {
      parts = [{ depth: 3 }];

      var element = compileDirective('<location-header-group></location-header-group>');

      expect(getHeader(element, 0).get(0).tagName).toEqual('H5');
    });

    it('renders depth = 4 as an H6', function () {
      parts = [{ depth: 4 }];

      var element = compileDirective('<location-header-group></location-header-group>');

      expect(getHeader(element, 0).get(0).tagName).toEqual('H6');
    });

    it('renders depth = 5 as an H6', function () {
      parts = [{ depth: 5 }];

      var element = compileDirective('<location-header-group></location-header-group>');

      expect(getHeader(element, 0).get(0).tagName).toEqual('H6');
    });

    it('renders a higher depth as an H6', function () {
      parts = [{ depth: 123 }];

      var element = compileDirective('<location-header-group></location-header-group>');

      expect(getHeader(element, 0).get(0).tagName).toEqual('H6');
    });

  });

  describe('when getParts returns an multiple entries', function () {

    it('renders them', function () {
      parts = [{
          label: 'Derp',
          depth: 2
        }, {
          label: 'Merp',
          depth: 3
        }, {
          label: 'Berp',
          depth: 4
        }];

      var element = compileDirective('<location-header-group></location-header-group>');

      expect(getHeaders(element).length).toEqual(3);

      expect(getHeader(element, 0).text()).toEqual('Derp');
      expect(getHeader(element, 0).get(0).tagName).toEqual('H4');

      expect(getHeader(element, 1).text()).toEqual('Merp');
      expect(getHeader(element, 1).get(0).tagName).toEqual('H5');

      expect(getHeader(element, 2).text()).toEqual('Berp');
      expect(getHeader(element, 2).get(0).tagName).toEqual('H6');
    });

  });

  function compileDirective(html) {
    var $compile = $injector.get('$compile');
    var element = $compile(html)($scope);
    $scope.$digest();

    return element;
  }

  function getHeaders(element) {
    return $(element).children();
  }

  function getHeader(element, index) {
    return getHeaders(element).eq(index).children();
  }

  function MockLocationHeaderLabelGenerator(previous, current) {
    givenPreviousLocation = previous;
    givenCurrentLocation = current;

    this.getParts = function () {
      return parts;
    };
  }

});
