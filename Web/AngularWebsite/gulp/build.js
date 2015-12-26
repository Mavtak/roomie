var angularTemplateCache = require('gulp-angular-templatecache');
var concat = require('gulp-concat');
var gulp = require('gulp');
var minifyHtml = require('gulp-minify-html');
var paths = require('../paths');
var rev = require('gulp-rev-easy');
var sourcemaps = require('gulp-sourcemaps');

gulp.task('build', [
  'build-angular-templates',
  'build-images',
  'build-markup',
  'build-scripts',
  'build-styles',
]);

gulp.task('build-angular-templates', function () {
  return gulp.src(paths.angularTemplates.in)
    .pipe(minifyHtml({
      empty: true
    }))
    .pipe(angularTemplateCache({
      module: 'roomie.templates',
      standalone: true,
      templateBody: '  $templateCache.put("<%= url %>","<%= contents %>");',
      templateFooter: '\n});',
      templateHeader: 'angular.module("<%= module %>"<%= standalone %>).run(function($templateCache) {\n',
    }))
    .pipe(gulp.dest(paths.angularTemplates.out));
});

gulp.task('build-images', function () {
  return gulp.src(paths.images.in)
    .pipe(gulp.dest(paths.images.out));
});

gulp.task('build-markup', [
  'build-angular-templates',
  'build-images',
  'build-scripts',
  'build-styles',
], function () {
  return gulp.src(paths.markup.in)
    .pipe(rev({
      base: paths.markup.out + '/..',
    }))
    .pipe(minifyHtml())
    .pipe(gulp.dest(paths.markup.out));
});

gulp.task('build-scripts', function () {
  return gulp.src(paths.scripts.in)
    .pipe(sourcemaps.init())
    .pipe(concat('script.js'))
    .pipe(sourcemaps.write('.'))
    .pipe(gulp.dest(paths.scripts.out));
});

gulp.task('build-styles', function () {
  return gulp.src(paths.styles.in)
    .pipe(concat('style.css'))
    .pipe(gulp.dest(paths.styles.out));
});
