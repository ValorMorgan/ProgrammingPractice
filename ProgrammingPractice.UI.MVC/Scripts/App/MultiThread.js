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
/** Sets the timers to begin counting up from 0.
 *  Parameters:
 *    reset: true/false if the timers should be reset to 0.
 */
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

/** Sets the fill gauges with their default style as well as
 *  resetting their value to 0.
 */
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

/** Begins the SingleThread fill gauge by
 *  calling the server to perform an operation.
 *  Also adds a window.unload event to cancel
 *  the operation if it is still going.
 */
function StartSingleThread() {
    var fillgauge = $('#multiThread-compare-multiThread');

    var ajax = $.ajax({
        type: 'POST',
        dataType: 'json',
        url: '/MultiThreadAjax/SingleThread',
        data: {
            clientId: clientId
        },
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

    $(window).unload(function () {
        ajax.abort();
        $.ajax({
            type: 'POST',
            dataType: 'json',
            url: '/MultiThreadAjax/CancelSingleThread',
            data: {
                clientId: clientId
            },
            async: true
        });
    });
}

/** Begins the MultiThread fill gauge by
 *  calling the server to perform an operation.
 *  Also adds a window.unload event to cancel
 *  the operation if it is still going.
 */
function StartMultiThread() {
    var fillgauge = $('#multiThread-compare-multiThread');

    var ajax = $.ajax({
        type: 'POST',
        dataType: 'json',
        url: '/MultiThreadAjax/MultiThread',
        data: {
            clientId: clientId
        },
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

    $(window).unload(function () {
        ajax.abort();
        $.ajax({
            type: 'POST',
            dataType: 'json',
            url: '/MultiThreadAjax/CancelMultiThread',
            data: {
                clientId: clientId
            },
            async: true
        });
    });
}

/*=============================
           Events
=============================*/
/** Initial load event to initialize gauages and timers
 */
$(window).load(function () {
    SetFillGauges();
    SetTimers();
    StartSingleThread();
    StartMultiThread();
});