var gulp = require('gulp');

gulp.task('default', [
  'build',
  'lint',
  'test',
]);

require('./gulp/build.js');
require('./gulp/lint.js');
require('./gulp/test.js');
require('./gulp/watch.js');
