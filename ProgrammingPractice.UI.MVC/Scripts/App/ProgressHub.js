/*===============================================
                ProgressHub
===============================================*/

/*=============================
           Variables
=============================*/

/*=============================
           Methods
=============================*/
function StartInvoicing() {
    var progressNotifier = $.connection.progressHub;

    // client-side sendMessage function that will be called from the server-side
    progressNotifier.client.sendMessage = function (message, count) {
        // update progress
        UpdateProgress(message, count);
    };

    // establish the connection to the server and start server-side operation
    $.connection.hub.start().done(function () {
        // call the method CallLongOperation defined in the Hub
        progressNotifier.server.getCountAndMessage();
    });
}