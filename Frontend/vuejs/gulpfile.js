const gulp = require('gulp');
const s3 = require('gulp-s3-upload')({
  useIAM: true
});

gulp.task('deploy', done =>
  gulp
  .src([
    '**/*',
    '!node_modules/**/*',
    '!gulpfile.js',
    '!package.json'
  ])
  .pipe(s3({
    Bucket: '',
    ACL: 'public-read'
  }), done);
);
