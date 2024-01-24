namespace SignalR.Application.Hubs.Chat
{
    public interface IChatHub
    {
        Task SendMessage(ChatDto chat);
        Task SendMessage(string message);
    }
}
