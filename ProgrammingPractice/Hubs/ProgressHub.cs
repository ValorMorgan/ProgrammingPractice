using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace ProgrammingPractice.Hubs
{
    public class ProgressHub : Hub
    {
        public string message = "Initializing and Preparing...";
        public int count = 1;

        public static void SendMessage(string message, int count)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ProgressHub>();
            hubContext.Clients.All.sendMessage(string.Format("Process completed for {0}", message), count);
        }

        public void GetCountAndMessage()
        {
            Clients.Caller.sendMessage(string.Format(message), count);
        }
    }
}