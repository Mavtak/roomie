var paths = require('./paths');

module.exports = function (config) {
  config.set({
    frameworks: [
      'jasmine',
    ],
    preprocessors: {
      'src/angular/**/*.html': [
        'ng-html2js',
      ],
    },
    files: paths.tests.in,
    reports: [
      'spec',
    ],
    colors: true,
    autoWatch: true,
    browsers: [
      'Chrome',
    ],
    singleRun: false,
    ngHtml2JsPreprocessor: {
      stripPrefix: 'src/angular/',
      moduleName: 'kabrich.templates',
      cacheIdFromPath: function(filepath) {
        return filepath.replace(/^src\/angular\//g, '');
      },
    }
  });
};
