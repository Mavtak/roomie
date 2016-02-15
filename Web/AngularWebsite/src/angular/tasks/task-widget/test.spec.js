describe('angular roomie.task task-widget (directive)', function() {
  var $compile;
  var $rootScope;
  var element;

  beforeEach(angular.mock.module('roomie.tasks'));

  beforeEach(angular.mock.inject(function(_$compile_, _$rootScope_) {
    $compile = _$compile_;
    $rootScope = _$rootScope_;
  }));

  beforeEach(function() {
    $rootScope.task = {};

    element = $compile('<task-widget task="task"></task-widget>')($rootScope);
  });

  describe('the structure', function() {

    describe('the header', function() {

      it('has one', function() {
        $rootScope.$digest();

        expect($(element).find('.widget widget-header .header').length).toEqual(1);
      });

      it('has a title of "Task"', function() {
        $rootScope.$digest();

        expect($(element).find('.widget widget-header .header .name').html().trim()).toEqual('Task');
      });

      it('does not link to anywhere', function() {
        $rootScope.$digest();

        expect($(element).find('.widget widget-header .header').attr('href')).not.toBeDefined();
      });

      it('has no subtitle', function() {
        $rootScope.$digest();

        expect($(element).find('.widget widget-header .header .location').html().trim()).toEqual('');
      });

    });

    describe('the key-value entries', function() {

      it('has 4', function() {
        $rootScope.$digest();

        expect($(element).find('.widget widget-data-section').find('key-value').length).toEqual(4);
      });

      it('has the first one as Origin"', function() {
        $rootScope.task.origin = 'derp';

        $rootScope.$digest();

        expectKeyValue(element, 0, 'Origin', 'derp', undefined);
      });

      it('has the second one as "Created"', function() {
        $rootScope.task.script = {
          creationTimestamp: new Date(2015, 2, 22, 5, 30, 15)
        };

        $rootScope.$digest();

        expectKeyValue(element, 1, 'Created', '3/22/2015, 5:30:15 AM', undefined);
      });

      it('has the third one as "Target"', function() {
        $rootScope.task.target = {
          id: '123',
          name: 'derp'
        };

        $rootScope.$digest();

        expectKeyValue(element, 2, 'Target', 'derp', '/computer/123/derp');
      });

      it('has the fourth one as "recieved"', function() {
        $rootScope.task = {
          expired: true
        };

        $rootScope.$digest();

        expectKeyValue(element, 3, 'Received', 'Expired', undefined);
      });

      function expectKeyValue(element, index, key, value, href) {
        var keyValues = $(element).find('.widget widget-data-section').find('key-value');

        expect(keyValues.eq(index).attr('key')).toEqual(key);
        expect(keyValues.eq(index).attr('value')).toEqual(value);
        expect(keyValues.eq(index).attr('href')).toEqual(href);
      }

    });

    describe('the script block', function() {

      it('has one', function() {
        $rootScope.$digest();

        expect($(element).find('.widget textarea.code').length).toEqual(1);
      });

      it('contains the script', function() {
        $rootScope.task.script = {
          text: '\nCore.Print Text="hi!"\n'
        };

        $rootScope.$digest();

        expect($(element).find('.widget textarea.code').val()).toEqual('\nCore.Print Text="hi!"\n');

      });

    });

  });

});
