var gulp = require('gulp'),
    sass = require('gulp-ruby-sass'),
    sourcemaps = require('gulp-sourcemaps'),
    uglify = require('gulp-uglify'),
    concat = require('gulp-concat'),
    plumber = require('gulp-plumber'),
    browserSync = require('browser-sync').create(),
    minifyCss = require("gulp-minify-css"),
    rename = require("gulp-rename"),
    rimraf = require("rimraf");

var cfg = {
    stylesPath: './Theme/devel',
    webRoot: "./wwwroot"
};

gulp.task("clean:css", done => rimraf(cfg.webRoot + '/css/main.min.css', done));
gulp.task("clean", gulp.series(["clean:css"]));

gulp.task('compile-styles', function () {
    return sass(cfg.stylesPath + '/main.scss', {
        style: 'compressed', // expanded, compressed
        loadPath: [
            cfg.stylesPath
        ],
        sourcemap: false
    })
        .on('error', sass.logError)
        .pipe(sourcemaps.write('maps', {
            includeContent: false,
            sourceRoot: cfg.stylesPath
        }))
        .pipe(rename({
            suffix: '.min'
        }))
        .pipe(minifyCss())
        .pipe(gulp.dest(cfg.webRoot + '/css'))
        .pipe(browserSync.stream({
            match: '**/*.css'
        }));
});

gulp.task('watch', function () {
//    gulp.watch([cfg.stylesPath + '/**/*.scss', '!' + cfg.stylesPath + '/**/main.scss', '!' + cfg.stylesPath + '/**/index.scss'], gulp.series(['compile-styles']));
    gulp.watch([cfg.stylesPath + '/**/*.scss', '!' + cfg.stylesPath + '/**/main.scss'], gulp.series(['compile-styles']));
});

gulp.task("default", gulp.series(["watch"]));