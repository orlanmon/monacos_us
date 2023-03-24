/// <binding AfterBuild='default' />
// include plug-ins
var gulp = require('gulp');
var concat = require('gulp-concat');
var uglify = require('gulp-uglify');
var del = require('del');
var debug = require('gulp-debug');






var config = {
    //Include all js files but exclude any min.js files
    src: ['angular_app/**/*.js', '!angular_app/**/*.min.js']
}

//delete the output file(s)
gulp.task('clean', function () {
    //del is an async function and not a gulp plugin (just standard nodejs)
    //It returns a promise, so make sure you return that from this task function
    //  so gulp knows when the delete is complete
    return del(['angular_app/all.min.js']);
});

// Combine and minify all files from the app folder
// This tasks depends on the clean task which means gulp will ensure that the 
// Clean task is completed before running the scripts task.
gulp.task('scripts', ['clean'], function () {


    console.log('Node version: ' + process.version);

    return gulp.src(config.src)
        .pipe(debug({ title: 'Debug:' }))
        //.pipe(uglify()).on('error', function (e) {
        //    console.log(e);
        //})
        .pipe(concat('all.min.js'))
        .pipe(gulp.dest('angular_app/'));
   
});

//Set a default tasks
gulp.task('default', ['scripts'], function () { });