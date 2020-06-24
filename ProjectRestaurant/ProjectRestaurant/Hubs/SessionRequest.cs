using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectRestaurant.Hubs
{
    public class SessionRequest : Hub
    {
        private string RestaurantConnectionId;
        private string CostumerConID;
        public async Task BroadcastMessage(string tableId,string CostumerConnectionId)
        {
            CostumerConID = CostumerConnectionId;
            
            await Clients.All.SendAsync("receive", tableId, CostumerConnectionId);

        }
        public Task SendSessionRequestMessage(string connectionId, string message)
        {
            return Clients.Client(connectionId).SendAsync("Request", message);
        }
        public Task SendActivationLink(string message,string conId)
        {
            return Clients.Client(conId).SendAsync("SendLink", message);
        }
        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("UserConnected", Context.ConnectionId);
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Clients.All.SendAsync("UserDisonnected", Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }

        public string getRestaurantConnectionId()
        {
            return Context.ConnectionId;
        }
    }
}
