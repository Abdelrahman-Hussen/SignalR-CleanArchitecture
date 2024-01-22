namespace SignalR.Application.Features
{
    public interface ILocalizationService
    {
        string GetMessage(Messages messages);
    }
}