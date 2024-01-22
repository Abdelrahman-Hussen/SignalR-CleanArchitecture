using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.Application.Hubs.Chat
{
    public interface IChatHub
    {
        Task SendMessage(ChatDto chat);
        Task SendMessage(string message);
    }
}
