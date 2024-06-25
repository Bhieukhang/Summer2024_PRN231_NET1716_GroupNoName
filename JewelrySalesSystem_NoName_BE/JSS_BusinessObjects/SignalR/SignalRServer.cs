using JSS_BusinessObjects.Payload.Response;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS_BusinessObjects.SignalR
{
    public class SignalRServer : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendDiscountNotification(DiscountResponse discount)
        {
            await Clients.All.SendAsync("ReceiveDiscountNotification", discount);
        }
    }
}
