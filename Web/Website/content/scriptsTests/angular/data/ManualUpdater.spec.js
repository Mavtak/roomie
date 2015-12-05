/// <reference path="../../../Scripts/Libraries/angular-1.3.13.min.js"/>
/// <reference path="../../../Scripts/Libraries/jquery-1.5.1.min.js"/>
/// <reference path="../../../Scripts/Libraries/lodash-3.4.0.min.js"/>
/// <reference path="../angular-mocks.js"/>
/// <reference path="../../../Scripts/angular/dependencies.js"/>
/// <reference path="../../../Scripts/angular/data/ManualUpdater.js"/>

describe('roomie.data.ManualUpdater', function() {

  var ManualUpdater;
  var items;

  beforeEach(angular.mock.module('roomie.data'));

  beforeEach(angular.mock.inject(function ($injector) {
    ManualUpdater = $injector.get('ManualUpdater');
  }));

  beforeEach(function() {
    items = [];
  });

  describe('adding new entries', function() {

    it('adds new items when originals is empty', function() {
      var manualUpdater = new ManualUpdater({
        originals: items
      });

      manualUpdater.run([{ id: 'a' }, { id: 'b' }, { id: 'c' }]);

      expect(items).toEqual([{ id: 'a' }, { id: 'b' }, { id: 'c' }]);
    });

    it('adds new items to existing items', function() {
      var manualUpdater = new ManualUpdater({
        originals: items
      });

      manualUpdater.run([{ id: 'a' }, { id: 'b' }, { id: 'c' }]);
      manualUpdater.run([{ id: 'a' }, { id: 'b' }, { id: 'c' }, { id: 'd' }, { id: 'e' }, { id: 'f' }]);

      expect(items).toEqual([{ id: 'a' }, { id: 'b' }, { id: 'c' }, { id: 'd' }, { id: 'e' }, { id: 'f' }]);
    });

    it('uses the input object instances as the results', function() {
      var manualUpdater = new ManualUpdater({
        originals: items
      });

      var a = { id: 'a' };
      var b = { id: 'b' };
      var c = { id: 'c' };
      manualUpdater.run([a, b, c]);

      expect(items[0]).toBe(a);
      expect(items[1]).toBe(b);
      expect(items[2]).toBe(c);
    });

    it('runs the optional processUpdate function on new items', function () {
      var counter = 0;
      var manualUpdater = new ManualUpdater({
        originals: items,
        processUpdate: function (item) {
          item.customThingy = counter;
          counter++;
        }
      });

      manualUpdater.run([{ id: 'a' }, { id: 'b' }, { id: 'c' }]);

      expect(items).toEqual([{ id: 'a', customThingy: 0 }, { id: 'b', customThingy: 1 }, { id: 'c', customThingy: 2 }]);
    });

    it('runs the optional ammendOriginal function on new items', function () {
      var counter = 0;
      var manualUpdater = new ManualUpdater({
        originals: items,
        ammendOriginal: function (item) {
          item.customThingy = counter;
          counter++;
        }
      });

      manualUpdater.run([{ id: 'a' }, { id: 'b' }, { id: 'c' }]);

      expect(items).toEqual([{ id: 'a', customThingy: 0 }, { id: 'b', customThingy: 1 }, { id: 'c', customThingy: 2 }]);
    });

  });

  describe('updating existing entries', function() {

    describe('adding new properties', function() {

      var original;
      var update;
      var result;
      var newDirectObject;
      var newNestedObject;

      beforeEach(function() {

        original = {
          id: 'abc-123',
          directLiteral1: 'first value',
          directObject: {
            indirectLiteral2: 'second value',
            nestedObject: {

            }
          },
          aFunction: function() {
          }
        };

        update = {
          id: 'abc-123',
          directLiteral2: 'second value, added',
          directObject: {
            indirectLiteral1: 'first value, added',
            nestedObject: {
              nestedLiteral1: 'first value, added',
              superNestedObject: {
                value: 'yes'
              }
            }
          },
          directObject2: {

          }
        };

        newDirectObject = update.directObject2;
        newNestedObject = update.directObject.nestedObject.superNestedObject;

        var manualUpdater = new ManualUpdater({
          originals: items
        });

        manualUpdater.run([original]);
        manualUpdater.run([update]);
        result = items[0];
      });

      it('maintains the original object instance', function() {
        expect(result).toBe(original);
      });

      it('adds new literal properties on the base object', function() {
        expect(result.directLiteral2).toEqual('second value, added');
      });

      it('copies added object property instances on the base object', function() {
        expect(result.directObject2).not.toBe(newDirectObject);
      });

      it('updates literal properties on direct child objects', function() {
        expect(result.directObject.indirectLiteral1).toEqual('first value, added');
      });

      it('copies added nested object instances', function() {
        expect(result.directObject.nestedObject.superNestedObject).not.toBe(newNestedObject);
      });

      it('updates literal properties on nested objects', function() {
        expect(result.directObject.nestedObject.nestedLiteral1).toEqual('first value, added');
      });
    });

    describe('updating existing properties', function() {

      var original;
      var update;
      var result;
      var originalDirectObject;
      var originalNestedObject;

      beforeEach(function() {

        original = {
          id: 'abc-123',
          directLiteral1: 'first value',
          directLiteral2: 'second value',
          directObject: {
            indirectLiteral1: 'first value',
            indirectLiteral2: 'second value',
            nestedObject: {
              nestedLiteral1: 'fist value'
            }
          },
          aFunction: function() {
          }
        };

        update = {
          id: 'abc-123',
          directLiteral2: 'second value, updated',
          directObject: {
            indirectLiteral1: 'first value, updated',
            nestedObject: {
              nestedLiteral1: 'first value, updated'
            }
          }
        };

        originalDirectObject = original.directObject;
        originalNestedObject = original.directObject.nestedObject;

        var manualUpdater = new ManualUpdater({
          originals: items
        });

        manualUpdater.run([original]);
        manualUpdater.run([update]);
        result = items[0];
      });

      it('maintains the original object instance', function() {
        expect(result).toBe(original);
      });

      it('updates literal properties on the base object', function() {
        expect(result.directLiteral2).toEqual('second value, updated');
      });

      it('maintains object property instances on the base object', function() {
        expect(result.directObject).toBe(originalDirectObject);
      });

      it('updates literal properties on direct child objects', function() {
        expect(result.directObject.indirectLiteral1).toEqual('first value, updated');
      });

      it('maintains added nested object instances', function() {
        expect(result.directObject.nestedObject).toBe(originalNestedObject);
      });

      it('updates literal properties on nested objects', function() {
        expect(result.directObject.nestedObject.nestedLiteral1).toEqual('first value, updated');
      });

    });

    describe('removing properties', function() {

      it('does not remove properties', function() {
        var manualUpdater = new ManualUpdater({
          originals: items
        });

        var original = {
          id: 'abc-123',
          baseLiteral: 'hi there!',
          baseObject: {
            childObjectProperty: true,
            nestedObject: {
              nestedObjectProperty: 1234
            }
          }
        };

        var originalBaseObject = original.baseObject;
        var originalNestedObject = original.baseObject.nestedObject;

        manualUpdater.run([original]);

        manualUpdater.run([{
          id: 'abc-123'
        }]);

        var result = items[0];

        expect(result).toBe(original);
        expect(result.baseLiteral).toEqual('hi there!');
        expect(result.baseObject).toBe(originalBaseObject);
        expect(result.baseObject.childObjectProperty).toEqual(true);
        expect(result.baseObject.nestedObject).toBe(originalNestedObject);
        expect(result.baseObject.nestedObject.nestedObjectProperty).toEqual(1234);
      });

    });

    it('runs the optional processUpdate function on updated items', function () {
      var counter = 0;
      var manualUpdater = new ManualUpdater({
        originals: items,
        processUpdate: function (item) {
          item.customThingy = counter;
          counter++;
        }
      });

      manualUpdater.run([{ id: 'a' }, { id: 'b' }, { id: 'c' }]);
      manualUpdater.run([{ id: 'a' }, { id: 'b' }, { id: 'c' }]);

      expect(items).toEqual([{ id: 'a', customThingy: 3 }, { id: 'b', customThingy: 4 }, { id: 'c', customThingy: 5 }]);
    });

    it('does not run the optional ammendOriginal function on updated items', function () {
      var counter = 0;
      var manualUpdater = new ManualUpdater({
        originals: items,
        ammendOriginal: function (item) {
          item.customThingy = counter;
          counter++;
        }
      });

      manualUpdater.run([{ id: 'a' }, { id: 'b' }, { id: 'c' }]);
      manualUpdater.run([{ id: 'a' }, { id: 'b' }, { id: 'c' }]);

      expect(items).toEqual([{ id: 'a', customThingy: 0 }, { id: 'b', customThingy: 1 }, { id: 'c', customThingy: 2 }]);
    });

  });

  describe('removing entries', function() {

    it('does not remove entries', function() {
      var manualUpdater = new ManualUpdater({
        originals: items
      });

      manualUpdater.run([{ id: 'a' }, { id: 'b' }, { id: 'c' }]);
      manualUpdater.run([{ id: 'd' }, { id: 'e' }, { id: 'f' }]);

      expect(items).toEqual([{ id: 'a' }, { id: 'b' }, { id: 'c' }, { id: 'd' }, { id: 'e' }, { id: 'f' }]);
    });

  });

  describe('when the update is complete', function() {

    it('runs the optional updateComplete function', function () {
      var counter = 0;
      var manualUpdater = new ManualUpdater({
        originals: items,
        updateComplete: function () {
          counter++;
        }
      });

      manualUpdater.run([{ id: 'a' }, { id: 'b' }, { id: 'c' }]);

      expect(counter).toEqual(1);
    });
  });

});
