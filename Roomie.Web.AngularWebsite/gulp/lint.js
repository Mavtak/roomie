var gulp = require('gulp');
var jshint = require('gulp-jshint');
var paths = require('../paths');

gulp.task('lint', [
  'lint-gulp',
  'lint-scripts',
  'lint-tests',
]);

gulp.task('lint-gulp', function () {
  return gulp.src(paths.gulp.in)
    .pipe(jshint())
    .pipe(jshint.reporter());
});

gulp.task('lint-karma', function () {
  return gulp.src(paths.karma.config)
    .pipe(jshint())
    .pipe(jshint.reporter());
});

gulp.task('lint-scripts', function () {
  return gulp.src(paths.scripts.in)
    .pipe(jshint())
    .pipe(jshint.reporter());
});

gulp.task('lint-tests', function () {
  return gulp.src(paths.tests.justSpecs)
    .pipe(jshint())
    .pipe(jshint.reporter());
});
