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
      'src/index.js',
      'node_modules/angular-mocks/angular-mocks.js',
      'node_modules/jquery/dist/jquery.js',
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
