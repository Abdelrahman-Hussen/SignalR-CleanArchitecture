using Microsoft.AspNetCore.Http;

namespace SingalR.Application.Utils
{
    internal static class Localizer
    {
        public static Language GetLanguage()
        {
            var httpContextAccessor = new HttpContextAccessor();

            var httpContext = httpContextAccessor.HttpContext;

            string acceptLanguage = httpContext!.Request.Headers["Accept-Language"];

            string lang = acceptLanguage.Split(',').FirstOrDefault()!.Trim().Split(';').FirstOrDefault()!;

            if (string.IsNullOrEmpty(lang))
                lang = "EN";

            Language language;

            Enum.TryParse(lang, out language);

            return language;
        }
    }
}
