/// <reference path="../../../Scripts/Libraries/jquery-1.5.1.min.js"/>
/// <reference path="../../../Scripts/Libraries/angular-1.3.13.min.js"/>
/// <reference path="../angular-mocks.js"/>
/// <reference path="../../../Scripts/angular/dependencies.js"/>
/// <reference path="../../../Scripts/angular/devices/RainbowColorsGenerator.js"/>

describe('roomie.devices.RainbowColorsGenerator', function () {
  var RainbowColorsGenerator;
  
  beforeEach(angular.mock.module('roomie.devices'));

  beforeEach(angular.mock.inject(function ($injector) {
    RainbowColorsGenerator = $injector.get('RainbowColorsGenerator');
  }));

  describe('generate', function() {

    it('returns a specific array of hex colors', function() {
      var generator = new RainbowColorsGenerator({});

      var result = generator.generate();

      expect(result).toEqual([
        '#FF0000',
        '#FF2900',
        '#FF5200',
        '#FF7B00',
        '#FFA500',
        '#FFBB00',
        '#FFD200',
        '#FFE800',
        '#FFFF00',
        '#BFDF00',
        '#7FBF00',
        '#3F9F00',
        '#008000',
        '#00603F',
        '#00407F',
        '#0020BF',
        '#0000FF',
        '#2000DF',
        '#4000BF',
        '#60009F',
        '#800080',
        '#9F0060',
        '#BF0040',
        '#DF0020'
      ]);
    });
  });

});
