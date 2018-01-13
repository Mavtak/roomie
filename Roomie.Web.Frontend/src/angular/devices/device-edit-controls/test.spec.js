describe('angular roomie.devices device-edit-controls (directive)', function () {
  var $injector;
  var $scope;
  var api;
  var element;

  beforeEach(angular.mock.module('roomie.devices', function ($provide) {
    api = jasmine.createSpy('api');

    $provide.value('api', api);
    $provide.value('deviceTypes', [
      'Really Neat',
      'Kinda Cool',
      'Just Terrible',
    ]);
  }));

  beforeEach(angular.mock.inject(function (_$injector_) {
    $injector = _$injector_;
    $scope = $injector.get('$rootScope').$new();
  }));

  beforeEach(function () {
    $scope.attributes = {
      device: {
        location: {
          name: 'some/place',
        },
        id: 5,
        name: 'Lamp Or Something',
        type: {
          name: 'Kinda Cool',
        }
      }
    };
  });

  beforeEach(function () {
    element = compileDirective('<device-edit-controls device="attributes.device"></device-edit-controls>');
  });

  describe('the rows', function () {

    it('has three of them', function () {
      expect(selectRows().length).toEqual(4);
    });

    describe('the first row', function () {

      describe('the label', function () {

        it('says "Name:"', function () {
          expect(selectLabel(0).text().trim()).toEqual('Name:');
        });

      });

      describe('the input', function () {

        it('exists', function () {
          expect(selectInput(0).length).toEqual(1);
        });

        it('is in the shared form', function () {
          expect($(selectInput(0)).parents('form').length).toEqual(1);
        });

        it('is bound to the value', function () {
          expect(selectInput(0).val()).toEqual('Lamp Or Something');
        });

        it('does not update if the source device\'s name changes', function () {
          $scope.attributes.device.name = 'herp';
          $scope.$digest();

          expect(selectInput(0).val()).toEqual('Lamp Or Something');
        });

      });

    });

    describe('the second row', function () {

      describe('the label', function () {

        it('says "Location:"', function () {
          expect(selectLabel(1).text().trim()).toEqual('Location:');
        });

      });

      describe('the input', function () {

        it('exists', function () {
          expect(selectInput(1).length).toEqual(1);
        });

        it('is in the shared form', function () {
          expect($(selectInput(1)).parents('form').length).toEqual(1);
        });

        it('is bound to the value', function () {
          expect(selectInput(1).val()).toEqual('some/place');
        });

        it('does not update if the source device\'s name changes', function () {
          $scope.attributes.device.location.name = 'herp';
          $scope.$digest();

          expect(selectInput(1).val()).toEqual('some/place');
        });

      });

    });

    describe('the third row', function () {

      describe('the label', function () {

        it('says "Type:"', function () {
          expect(selectLabel(2).text().trim()).toEqual('Type:');
        });

      });

      describe('the input', function () {

        it('exists', function () {
          expect(selectInput(2).length).toEqual(1);
        });

        it('is in the shared form', function () {
          expect($(selectInput(2)).parents('form').length).toEqual(1);
        });

        it('lists options from the deviceType data', function () {
          var values = [];
          selectInput(2).find('option').each(function () {
            values.push($(this).text());
          });

          expect(values).toEqual([
            'Really Neat',
            'Kinda Cool',
            'Just Terrible',
          ]);
        });

        it('is bound to the value', function () {
          expect(selectInput(2).find('option:selected').text()).toEqual('Kinda Cool');
        });

        it('does not update if the source device\'s name changes', function () {
          $scope.attributes.device.type.name = 'herp';
          $scope.$digest();

          expect(selectInput(2).find('option:selected').text()).toEqual('Kinda Cool');
        });

      });

    });

    describe('the fourth row', function () {

      describe('the label', function () {

        it('says nothing', function () {
          expect(selectLabel(3).text().trim()).toEqual('');
        });

      });

      describe('the input (the save button)', function () {

        it('exists', function () {
          expect(selectInput(3).length).toEqual(1);
        });

        it('is in the shared form', function () {
          expect($(selectInput(3)).parents('form').length).toEqual(1);
        });

        it('has text that says "Save"', function () {
          expect(selectInput(3).text().trim()).toEqual('Save');
        });

        describe('clicking', function () {

          afterEach(() => {
            expect(api.calls.count()).toBe(1);
          });

          it('should submit an API request', function () {
            selectInput(3).click();

            expect(api).toHaveBeenCalledWith({
              repository: 'device',
              action: 'update',
              parameters: {
                id: 5,
                name: 'Lamp Or Something',
                location: 'some/place',
                type: 'Kinda Cool',
              }
            })
          });

          it('submits the latest name input', function () {
            angular.element(selectRow(0).find('input')[0])
              .val('Probably a Lamp')
              .triggerHandler('input');

            selectInput(3).click();

            expect(api).toHaveBeenCalledWith({
              repository: 'device',
              action: 'update',
              parameters: {
                id: 5,
                name: 'Probably a Lamp',
                location: 'some/place',
                type: 'Kinda Cool',
              }
            });
          });

          it('submits the latest location input', function () {
            angular.element(selectRow(1).find('input')[0])
              .val('some/place/else')
              .triggerHandler('input');

            selectInput(3).click();

            expect(api).toHaveBeenCalledWith({
              repository: 'device',
              action: 'update',
              parameters: {
                id: 5,
                name: 'Lamp Or Something',
                location: 'some/place/else',
                type: 'Kinda Cool',
              }
            });
          });

          it('submits the latest type input', function () {
            angular.element(selectRow(2).find('select')[0])
              .val(2)
              .triggerHandler('change');

            selectInput(3).click();

            expect(api).toHaveBeenCalledWith({
              repository: 'device',
              action: 'update',
              parameters: {
                id: 5,
                name: 'Lamp Or Something',
                location: 'some/place',
                type: 'Just Terrible',
              }
            });
          });

        });

      });

    });

    function selectInput(index) {
      var selector;

      switch(index) {
        case 0:
        case 1:
          selector ='input[type="text"]';
          break;

        case 2:
          selector = 'select';
          break;

        case 3:
          selector = 'button';
          break;
      }

      return selectRow(index).find(selector);
    }

    function selectLabel(index) {
      return selectRow(index).find('.key');
    }

    function selectRow(index) {
      return selectRows().eq(index);
    }

    function selectRows() {
      return $(element).find('.data .item');
    }

  });

  describe('the shared form', function () {

    it('exists', function () {
      expect($(element).find('form').length).toEqual(1);
    });

  });

  function compileDirective(html) {
    var $compile = $injector.get('$compile');
    var element = $compile(html)($scope);
    $scope.$digest();

    return element;
  }

});
