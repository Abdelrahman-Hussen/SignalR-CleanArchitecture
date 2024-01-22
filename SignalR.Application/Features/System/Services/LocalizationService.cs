using Microsoft.Extensions.Localization;
using SignalR.Application.Resources;

namespace SignalR.Application.Features
{
    internal class LocalizationService : ILocalizationService
    {
        private readonly IStringLocalizer<ResourceFile> _localizer;

        public LocalizationService(IStringLocalizer<ResourceFile> localizer)
            => _localizer = localizer;

        public string GetMessage(Messages messages)
            => _localizer[messages.ToString()];
    }
}
