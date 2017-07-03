var gulp = require('gulp');
var paths = require('../paths');

gulp.task('watch-lint', [
  'lint'
], function () {
  gulp.watch(paths.gulp.in, ['lint-gulp']);
  gulp.watch(paths.karma.config, ['lint-karma']);
  gulp.watch(paths.scripts.in, ['lint-scripts']);
  gulp.watch(paths.tests.justSpecs, ['lint-tests']);
});
