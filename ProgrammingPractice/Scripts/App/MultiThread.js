/*===============================================
                MultiThread
===============================================*/

/*=============================
           Variables
=============================*/
var timersInitiated = false;

/*=============================
           Methods
=============================*/
var setTimers = function (reset) {
    if (timersInitiated && reset) {
        $('.multiThread-timer').timer('remove');
    }

    $('.multiThread-timer').timer({
        seconds: 0,
        format: '%M:%S'
    });

    timersInitiated = true;
}

var startLeft = function () {
    var element = $('#multiThread-compare-left');

    $.ajax({
        type: 'POST',
        dataType: 'json',
        url: '/MultiThreadAjax/SingleThread',
        async: true
    }).fail(function (xhr) {
        $(element).css('color', 'red');
        $(element).text('Failed');
        $('#multiThread-compare-left-timer').timer('pause');
    }).done(function (status) {
        $(element).css('color', 'green');
        $(element).text('Done');
        $('#multiThread-compare-left-timer').timer('pause');
    });
}

var startRight = function () {
    var element = $('#multiThread-compare-right');

    $.ajax({
        type: 'POST',
        dataType: 'json',
        url: '/MultiThreadAjax/MultiThread',
        async: true
    }).fail(function (xhr) {
        $(element).css('color', 'red');
        $(element).text('Failed');
        $('#multiThread-compare-right-timer').timer('pause');
    }).done(function (status) {
        $(element).css('color', 'green');
        $(element).text('Done');
        $('#multiThread-compare-right-timer').timer('pause');
    });
}

/*=============================
           Events
=============================*/
$(document).ready(function () {
    setTimers();
    startLeft();
    startRight();
});

$('#multiThread-startOver').click(function () {
    setTimers(true);
    startLeft();
    startRight();
});