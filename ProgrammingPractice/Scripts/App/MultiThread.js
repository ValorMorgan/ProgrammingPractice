/*===============================================
                MultiThread
===============================================*/

/*=============================
           Variables
=============================*/
var timersInitiated = false;
var gauagSingleThread = null;
var gaugeMultiThread = null;

/*=============================
           Methods
=============================*/
function SetTimers(reset) {
    if (timersInitiated && reset) {
        $('.multiThread-timer').timer('remove');
    }

    $('.multiThread-timer').timer({
        seconds: 0,
        format: '%M:%S'
    });

    timersInitiated = true;
}

function SetFillGauges() {
    var fillGaugeConfig = liquidFillGaugeDefaultSettings();
    fillGaugeConfig.circleColor = '#2c9c41';
    fillGaugeConfig.textColor = '#2c9c41';
    fillGaugeConfig.waveTextColor = '#2c9c41';
    fillGaugeConfig.waveColor = '#1c6c2b';
    fillGaugeConfig.circleThickness = 0.1;
    fillGaugeConfig.circleFillGap = 0.05;
    fillGaugeConfig.waveAnimate = true;
    fillGaugeConfig.waveAnimateTime = 2000;

    if (!Exists(gauagSingleThread)) {
        gauagSingleThread = loadLiquidFillGauge('multiThread-fillGauge-singleThread', 0, fillGaugeConfig);
    } else {
        gauagSingleThread.update(0);
    }

    if (!Exists(gaugeMultiThread)) {
        gaugeMultiThread = loadLiquidFillGauge('multiThread-fillGauge-multiThread', 0, fillGaugeConfig);
    } else {
        gaugeMultiThread.update(0);
    }
}

function StartSingleThread() {
    var fillgauge = $('#multiThread-compare-multiThread');

    $.ajax({
        type: 'POST',
        dataType: 'json',
        url: '/MultiThreadAjax/SingleThreadAsync',
        async: true
    }).fail(function (xhr) {
        $(fillgauge).empty();
        $(fillgauge).prepend('<img class="fillGauge-Error" src="Content/Images/FillGaugeError.png" />');
        gauagSingleThread = null;
        $('#multiThread-compare-singleThread-timer').timer('pause');
    }).done(function () {
        gauagSingleThread.update(100);
        $('#multiThread-compare-singleThread-timer').timer('pause');
    });
}

function StartMultiThread() {
    var fillgauge = $('#multiThread-compare-multiThread');

    $.ajax({
        type: 'POST',
        dataType: 'json',
        url: '/MultiThreadAjax/MultiThreadAsync',
        async: true
    }).fail(function (xhr) {
        $(fillgauge).empty();
        $(fillgauge).prepend('<img class="fillGauge-Error" src="Content/Images/FillGaugeError.png" />');
        gaugeMultiThread = null;
        $('#multiThread-compare-multiThread-timer').timer('pause');
    }).done(function () {
        gaugeMultiThread.update(100);
        $('#multiThread-compare-multiThread-timer').timer('pause');
    });
}

/*=============================
           Events
=============================*/
$(document).ready(function () {
    SetFillGauges();
    SetTimers();
    StartSingleThread();
    StartMultiThread();
});

$('#multiThread-startOver').click(function () {
    SetFillGauges();
    SetTimers(reset = true);
    StartSingleThread();
    StartMultiThread();
});