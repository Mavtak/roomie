describe('angular roomie.users sign-in-form (directive)', function () {
  var $injector;
  var $scope;
  var element;

  beforeEach(angular.mock.module('roomie.users'));

  beforeEach(angular.mock.inject(function (_$injector_) {
    $injector = _$injector_;
    $scope = $injector.get('$rootScope').$new();
  }));

  beforeEach(function () {
    $scope.attributes = {};
  });

  beforeEach(function () {
    element = compileDirective('<sign-in-form password="attributes.password" submit="attributes.submit" username="attributes.username" ></sign-in-form>');
  });

  describe('the username input', function () {

    it('exists', function () {
      expect(selectUsernameInput().length).toEqual(1);
    });

    it('is bound to the username attribute', function () {
      angular.element(selectUsernameInput())
        .val('some new value')
        .triggerHandler('change');

      expect($scope.attributes.username).toEqual('some new value');
    });

    function selectUsernameInput() {
      return $(element).find('input[type=text]');
    }

  });

  describe('the password input', function () {

    it('exists', function () {
      expect(selectPasswordInput().length).toEqual(1);
    });

    it('is bound to the username attribute', function () {
      angular.element(selectPasswordInput())
        .val('some new value')
        .triggerHandler('change');

      expect($scope.attributes.password).toEqual('some new value');
    });

    function selectPasswordInput() {
      return $(element).find('input[type=password]');
    }

  });

  describe('the submit button', function () {

    it('exists', function () {
      expect(selectSubmitButton().length).toEqual(1);
    });

    it('calls the save attribute as a function', function () {
      $scope.attributes.submit = jasmine.createSpy('submit function');
      $scope.$digest();

      selectSubmitButton().click();

      expect($scope.attributes.submit.calls.count()).toEqual(1);
    });

    function selectSubmitButton() {
      return $(element).find('button[type=submit]');
    }

  });

  function compileDirective(html) {
    var $compile = $injector.get('$compile');
    var element = $compile(html)($scope);
    $scope.$digest();

    return element;
  }

});
