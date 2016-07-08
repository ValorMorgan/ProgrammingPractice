/*===============================================
                MultiThread
===============================================*/

/*=============================
           Variables
=============================*/
var timersInitiated = false;
var gaugeLeft = null;
var gaugeRight = null;

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

var setFillGauges = function () {
    var fillGaugeConfig = liquidFillGaugeDefaultSettings();
    fillGaugeConfig.circleColor = '#2c9c41';
    fillGaugeConfig.textColor = '#2c9c41';
    fillGaugeConfig.waveTextColor = '#2c9c41';
    fillGaugeConfig.waveColor = '#1c6c2b';
    fillGaugeConfig.circleThickness = 0.1;
    fillGaugeConfig.circleFillGap = 0.05;
    fillGaugeConfig.waveAnimate = true;
    fillGaugeConfig.waveAnimateTime = 2000;

    if (gaugeLeft == null || gaugeLeft == '' || gaugeLeft.length <= 0) {
        gaugeLeft = loadLiquidFillGauge('multiThread-fillGauge-left', 0, fillGaugeConfig);
    } else {
        gaugeLeft.update(0);
    }

    if (gaugeRight == null || gaugeRight == '' || gaugeRight.length <= 0) {
        gaugeRight = loadLiquidFillGauge('multiThread-fillGauge-right', 0, fillGaugeConfig);
    } else {
        gaugeRight.update(0);
    }
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
        gaugeLeft.update(100);
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
        gaugeRight.update(100);
        $('#multiThread-compare-right-timer').timer('pause');
    });
}

/*=============================
           Events
=============================*/
$(document).ready(function () {
    setFillGauges();
    setTimers();
    startLeft();
    startRight();
});

$('#multiThread-startOver').click(function () {
    setFillGauges();
    setTimers(true);
    startLeft();
    startRight();
});