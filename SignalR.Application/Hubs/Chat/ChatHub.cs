using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Application.Hubs.Chat
{
    [Authorize]
    public sealed class ChatHub : Hub<IChatHub>
    {
        public override async Task OnConnectedAsync()
            => await Clients.All.SendMessage($"\n My name is {Context.UserIdentifier} and my ConnectionId : {Context.ConnectionId} \n");
        
        public async Task WhoAmI()
            => await Clients.User(Context.UserIdentifier).SendMessage($"\n My name is {Context.UserIdentifier} and my ConnectionId : {Context.ConnectionId} \n");
          
        public async Task SendMessage(string Message)
            => await Clients.All.SendMessage(Message);

        public async Task SendToUser(string UserID, string message)
            => await Clients.User(UserID).SendMessage(message);
           
        public async Task JoinToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"{groupName}");
            await Clients.Group($"{groupName}").SendMessage($"{Context.UserIdentifier} joined {groupName}");
        }

        public async Task SendToGroup(string groupName, string message)
            => await Clients.Group($"{groupName}").SendMessage(message);
    }
}
