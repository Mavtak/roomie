var paths = module.exports = {};

var roots = {};
roots.in = 'src/';
roots.out = 'app/';

paths.angularTemplates = {};
paths.angularTemplates.in = [
  roots.in + 'angular/**/*.html',
];
paths.angularTemplates.out = roots.out;

paths.gulp = {};
paths.gulp.in = [
  'gulpfile.js',
  'gulp/**/*.js',
];

paths.images = {};
paths.images.in = [
  roots.in + '**/*.jpg',
  roots.in + '**/*.JPG',
];
paths.images.out = roots.out;

paths.karma = {};
paths.karma.config = 'karma.conf.js';

paths.markup = {};
paths.markup.in = [
  roots.in + '**/*.html',
  '!' + roots.in + 'angular/**/*.html',
];
paths.markup.out = roots.out;

paths.scripts = {};
paths.scripts.in = [
  roots.in + 'angular/modules.js',
  roots.in + '**/*.js',
  '!' + roots.in + '**/*.spec.js',
];
paths.scripts.out = roots.out;

paths.tests = {};
paths.tests.in = [
  'node_modules/angular/angular.js',
  'node_modules/angular-mocks/angular-mocks.js',
  roots.in + 'angular/modules.js',
  roots.in + '**/*.js',
  roots.in + '**/*.spec.js',
  'src/angular/**/*.html',
];
paths.tests.justSpecs = roots.in + '**/*.spec.js';

paths.styles = {};
paths.styles.in = [
  roots.in + '**/*.css',
];
paths.styles.out = roots.out;
