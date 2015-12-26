var gulp = require('gulp');
var paths = require('../paths');

gulp.task('watch-build', [
  'build',
  ], function () {
  gulp.watch(paths.angularTemplates.in, ['build-angular-templates', 'build-markup']);
  gulp.watch(paths.images.in, ['build-images', 'build-markup']);
  gulp.watch(paths.markup.in, ['build-markup']);
  gulp.watch(paths.scripts.in, ['build-scripts', 'build-markup']);
  gulp.watch(paths.styles.in, ['build-styles', 'build-markup']);
});
