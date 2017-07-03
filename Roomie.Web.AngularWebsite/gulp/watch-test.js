var gulp = require('gulp');
var karma = require('gulp-karma');
var paths = require('../paths');

gulp.task('watch-test', [
  'watch-test-scripts'
]);

gulp.task('watch-test-scripts', function () {
  return gulp.src(paths.tests.in)
    .pipe(karma({
      configFile: paths.karma.config,
      action: 'start'
    }))
    .on('error', function(err) {
      throw err;
    });
});
