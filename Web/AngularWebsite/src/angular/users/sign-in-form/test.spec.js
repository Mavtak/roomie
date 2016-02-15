describe('angular roomie.users sign-in-form (directive)', function () {
  var $compile;
  var $rootScope;
  var element;

  beforeEach(angular.mock.module('roomie.users'));

  beforeEach(angular.mock.inject(function ($injector) {
    $compile = $injector.get('$compile');
    $rootScope = $injector.get('$rootScope');
  }));

  beforeEach(function () {
    $rootScope.attributes = {};
  });

  beforeEach(function () {
    element = $compile('<sign-in-form password="attributes.password" submit="attributes.submit" username="attributes.username" ></sign-in-form>')($rootScope);
    $rootScope.$digest();
  });

  describe('the username input', function () {

    it('exists', function () {
      expect(selectUsernameInput().length).toEqual(1);
    });

    it('is bound to the username attribute', function () {
      angular.element(selectUsernameInput())
        .val('some new value')
        .triggerHandler('change');

      expect($rootScope.attributes.username).toEqual('some new value');
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

      expect($rootScope.attributes.password).toEqual('some new value');
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
      $rootScope.attributes.submit = jasmine.createSpy('submit function');
      $rootScope.$digest();

      selectSubmitButton().click();

      expect($rootScope.attributes.submit.calls.count()).toEqual(1);
    });

    function selectSubmitButton() {
      return $(element).find('button[type=submit]');
    }
  });

});
