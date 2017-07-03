var gulp = require('gulp');
var karma = require('gulp-karma');
var paths = require('../paths');

gulp.task('test', [
  'test-scripts'
]);

gulp.task('test-scripts', function () {
  return gulp.src(paths.tests.in)
    .pipe(karma({
      configFile: paths.karma.config,
      action: 'run'
    }))
    .on('error', function(err) {
      throw err;
    });
});
