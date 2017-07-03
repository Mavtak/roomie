var gulp = require('gulp');

gulp.task('watch', [
  'watch-build',
  'watch-lint',
  'watch-test',
]);

require('./watch-build.js');
require('./watch-lint.js');
require('./watch-test.js');
