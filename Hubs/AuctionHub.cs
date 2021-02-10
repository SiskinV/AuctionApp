using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace AuctionApp.Hubs
{
    public class AuctionHub : Hub
    {

        public async Task AddToGroup(string groupName)
        {
            await base.Groups.AddToGroupAsync(base.Context.ConnectionId, groupName);
        }

        public async Task UpdateAuctions(string groupName)
        {
            await base.Clients.Group(groupName).SendAsync("updateAuctions");
        }

    }
}