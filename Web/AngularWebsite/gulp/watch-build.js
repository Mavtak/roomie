var gulp = require('gulp');
var paths = require('../paths');

gulp.task('watch-build', [
  'build',
  ], function () {
  gulp.watch(paths.angularTemplates.in, ['build-angular-templates']);
  gulp.watch(paths.images.in, ['build-images']);
  gulp.watch(paths.markup.in, ['build-markup']);
  gulp.watch(paths.scripts.in, ['build-scripts']);
  gulp.watch(paths.styles.in, ['build-styles']);
});
