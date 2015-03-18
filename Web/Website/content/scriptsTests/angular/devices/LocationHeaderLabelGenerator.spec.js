/// <reference path="../../../Scripts/Libraries/jquery-1.5.1.min.js"/>
/// <reference path="../../../Scripts/Libraries/angular-1.3.13.min.js"/>
/// <reference path="../angular-mocks.js"/>
/// <reference path="../../../Scripts/angular/dependencies.js"/>
/// <reference path="../../../Scripts/angular/devices/LocationHeaderLabelGenerator.js"/>

describe('roomie.devices.LocationHeaderLabelGenerator', function() {
  var LocationHeaderLabelGenerator;

  beforeEach(angular.mock.module('roomie.devices'));

  beforeEach(inject(function(_LocationHeaderLabelGenerator_) {
    LocationHeaderLabelGenerator = _LocationHeaderLabelGenerator_;
  }));

  describe('getParts', function() {

    describe('with no previous location', function() {

      it('returns an array of parts with labels', function() {
        var generator = new LocationHeaderLabelGenerator(undefined, 'a/b/c');

        var parts = generator.getParts();

        expect(parts.length).toEqual(3);
        expect(parts[0].label).toEqual('a');
        expect(parts[1].label).toEqual('b');
        expect(parts[2].label).toEqual('c');
      });

      it('returns an array of parts with depths', function() {
        var generator = new LocationHeaderLabelGenerator(undefined, 'a/b/c');

        var parts = generator.getParts();

        expect(parts.length).toEqual(3);
        expect(parts[0].depth).toEqual(0);
        expect(parts[1].depth).toEqual(1);
        expect(parts[2].depth).toEqual(2);
      });

    });

    describe('with a previous location that has a different root', function() {

      it('returns an array of parts with labels', function() {
        var generator = new LocationHeaderLabelGenerator('z/b/c', 'a/b/c');

        var parts = generator.getParts();

        expect(parts.length).toEqual(3);
        expect(parts[0].label).toEqual('a');
        expect(parts[1].label).toEqual('b');
        expect(parts[2].label).toEqual('c');
      });

      it('returns an array of parts with depths', function() {
        var generator = new LocationHeaderLabelGenerator('z/b/c', 'a/b/c');

        var parts = generator.getParts();

        expect(parts.length).toEqual(3);
        expect(parts[0].depth).toEqual(0);
        expect(parts[1].depth).toEqual(1);
        expect(parts[2].depth).toEqual(2);
      });

    });

    describe('with a previous location that has a common root', function() {

      it('returns an array of parts with labels', function() {
        var generator = new LocationHeaderLabelGenerator('a/b/z', 'a/b/c/d/e');

        var parts = generator.getParts();

        expect(parts.length).toEqual(3);
        expect(parts[0].label).toEqual('c');
        expect(parts[1].label).toEqual('d');
        expect(parts[2].label).toEqual('e');
      });

      it('returns an array of parts with depths', function() {
        var generator = new LocationHeaderLabelGenerator('a/b/z', 'a/b/c/d/e');

        var parts = generator.getParts();

        expect(parts.length).toEqual(3);
        expect(parts[0].depth).toEqual(2);
        expect(parts[1].depth).toEqual(3);
        expect(parts[2].depth).toEqual(4);
      });

    });

    describe('with a previous location that is the same', function() {

      it('returns an empty array', function() {
        var generator = new LocationHeaderLabelGenerator('a/b/c', 'a/b/c');

        var parts = generator.getParts();

        expect(parts.length).toEqual(0);
      });

    });

    describe('with a previous location that is sorted after the current location (invalid)', function() {

      it('returns an empty array', function() {
        var generator = new LocationHeaderLabelGenerator('a/b/c/d', 'a/b/c');

        var parts = generator.getParts();

        expect(parts.length).toEqual(0);
      });

    });
  });

});
