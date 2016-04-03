angular.module('roomie.devices').factory('RainbowColorsGenerator', function () {

  return RainbowColorsGenerator;

  function RainbowColorsGenerator() {
    this.generate = function () {
      var result = [
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
      ];

      return result;
    };
  }

});
