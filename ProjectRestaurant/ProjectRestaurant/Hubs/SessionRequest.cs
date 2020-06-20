using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectRestaurant.Hubs
{
    public class SessionRequest:Hub
    {
        public async Task BroadcastMessage(string message) 
        {
            await Clients.All.SendAsync("receive", message);
            
        }

    }
}
