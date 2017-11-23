let webpackConfig = require('./webpack.config.js');

module.exports = function (config) {
  config.set({
    frameworks: [
      'jasmine',
    ],
    preprocessors: {
      'src/**/*.js': ['webpack']
    },
    webpack: webpackConfig,
    files: [
      'node_modules/angular/angular.js',
      'node_modules/angular-mocks/angular-mocks.js',
      'node_modules/angular-ui-router/release/angular-ui-router.js',
      'node_modules/jquery/dist/jquery.js',
      'node_modules/lodash/index.js',
      'src/index.js',
      {
        pattern: 'src/**/*.spec.js',
        watched: false,
      },
    ],
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
      moduleName: 'roomie.templates',
      cacheIdFromPath: function(filepath) {
        return filepath.replace(/^src\/angular\//g, '');
      },
    }
  });
};
