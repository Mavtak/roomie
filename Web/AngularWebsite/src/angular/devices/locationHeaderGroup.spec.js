describe('roomie.devices.locationHeaderGroup', function() {
  var $compile;
  var $rootScope;
  var givenCurrentLocation;
  var givenPreviousLocation;
  var parts;

  beforeEach(angular.mock.module('roomie.devices', function($provide) {
    $provide.factory('LocationHeaderLabelGenerator', function() {
      return MockLocationHeaderLabelGenerator;
    });
  }));

  beforeEach(angular.mock.inject(function ($injector) {
    $compile = $injector.get('$compile');
    $rootScope = $injector.get('$rootScope');
  }));

  beforeEach(function() {
    givenCurrentLocation = null;
    givenPreviousLocation = null;
    parts = null;
  });

  it('gives the previous and current locations to the LocationHeaderLabelGenerator', function() {
    $rootScope.previous = { name: 'herp' };
    $rootScope.current = { name: 'derp' };

    $compile('<location-header-group previous-location="previous" current-location="current"></location-header-group>')($rootScope);

    expect(givenPreviousLocation).toBe($rootScope.previous);
    expect(givenCurrentLocation).toBe($rootScope.current);
  });

  describe('when getParts returns an empty array', function() {

    it('renders no labels', function() {
      parts = [];

      var element = $compile('<location-header-group></location-header-group>')($rootScope);
      $rootScope.$digest();

      expect(getHeaders(element).length).toEqual(0);
    });

  });

  describe('when getParts returns a single entry', function() {

    it('renders the label', function() {
      parts = [{
        label: 'Derp',
        depth: 0
      }];

      var element = $compile('<location-header-group></location-header-group>')($rootScope);
      $rootScope.$digest();

      expect(getHeaders(element).length).toEqual(1);
    });

    it('renders depth = 0 as an H2', function() {
      parts = [{ depth: 0 }];

      var element = $compile('<location-header-group></location-header-group>')($rootScope);
      $rootScope.$digest();

      expect(getHeader(element, 0).get(0).tagName).toEqual('H2');
    });

    it('renders depth = 1 as an H3', function() {
      parts = [{ depth: 1 }];

      var element = $compile('<location-header-group></location-header-group>')($rootScope);
      $rootScope.$digest();

      expect(getHeader(element, 0).get(0).tagName).toEqual('H3');
    });

    it('renders depth = 2 as an H4', function() {
      parts = [{ depth: 2 }];

      var element = $compile('<location-header-group></location-header-group>')($rootScope);
      $rootScope.$digest();

      expect(getHeader(element, 0).get(0).tagName).toEqual('H4');
    });

    it('renders depth = 3 as an H5', function() {
      parts = [{ depth: 3 }];

      var element = $compile('<location-header-group></location-header-group>')($rootScope);
      $rootScope.$digest();

      expect(getHeader(element, 0).get(0).tagName).toEqual('H5');
    });

    it('renders depth = 4 as an H6', function() {
      parts = [{ depth: 4 }];

      var element = $compile('<location-header-group></location-header-group>')($rootScope);
      $rootScope.$digest();

      expect(getHeader(element, 0).get(0).tagName).toEqual('H6');
    });

    it('renders depth = 5 as an H6', function() {
      parts = [{ depth: 5 }];

      var element = $compile('<location-header-group></location-header-group>')($rootScope);
      $rootScope.$digest();

      expect(getHeader(element, 0).get(0).tagName).toEqual('H6');
    });

    it('renders a higher depth as an H6', function() {
      parts = [{ depth: 123 }];

      var element = $compile('<location-header-group></location-header-group>')($rootScope);
      $rootScope.$digest();

      expect(getHeader(element, 0).get(0).tagName).toEqual('H6');
    });

  });

  describe('when getParts returns an multiple entries', function() {

    it('renders them', function() {
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

      var element = $compile('<location-header-group></location-header-group>')($rootScope);
      $rootScope.$digest();

      expect(getHeaders(element).length).toEqual(3);

      expect(getHeader(element, 0).text()).toEqual('Derp');
      expect(getHeader(element, 0).get(0).tagName).toEqual('H4');

      expect(getHeader(element, 1).text()).toEqual('Merp');
      expect(getHeader(element, 1).get(0).tagName).toEqual('H5');

      expect(getHeader(element, 2).text()).toEqual('Berp');
      expect(getHeader(element, 2).get(0).tagName).toEqual('H6');
    });

  });

  function getHeaders(element) {
    return $(element).children();
  }

  function getHeader(element, index) {
    return getHeaders(element).eq(index).children();
  }

  function MockLocationHeaderLabelGenerator(previous, current) {
    givenPreviousLocation = previous;
    givenCurrentLocation = current;

    this.getParts = function() {
      return parts;
    };
  }
});
