using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace SingalR.Application.Utils
{
    public static class TokenExtractor
    {
        public static string? GetId()
        {
            var userIdentity = GetHttpContext().User.Identity as ClaimsIdentity;

            var userId = userIdentity?.FindFirst(ClaimTypes.Sid)?.Value;

            return userId;
        }

        public static string? GetEmail()
        {
            var userIdentity = GetHttpContext().User.Identity as ClaimsIdentity;

            var userEmail = userIdentity!.FindFirst(ClaimTypes.Email)!.Value;

            return userEmail;
        }
        public static List<Roles> GetRoles()
        {
            var userIdentity = GetHttpContext().User.Identity as ClaimsIdentity;

            var userRoles = userIdentity!.FindAll(ClaimTypes.Role).Select(roleClaim => roleClaim.Value).ToList();

            if (userRoles.Count == 1)
            {
                var singleRole = userRoles[0];
                Enum.TryParse(singleRole, ignoreCase: true, out Roles role);
                return new List<Roles> { role };
            }

            var roles = new List<Roles>();
            foreach (var role in userRoles)
            {
                Enum.TryParse(role, ignoreCase: true, out Roles parsedRole);
                roles.Add(parsedRole);
            }

            return roles;
        }
        public static string? GetUserName()
        {
            var userIdentity = GetHttpContext().User.Identity as ClaimsIdentity;

            var userName = userIdentity!.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            return userName;
        }
        public static string? GetPhoneNumber()
        {
            var userIdentity = GetHttpContext().User.Identity as ClaimsIdentity;

            var phoneNumber = userIdentity!.FindFirst(ClaimTypes.MobilePhone)!.Value;

            return phoneNumber;
        }
        public static HttpContext GetHttpContext()
        {
            var httpContextAccessor = new HttpContextAccessor();
            return httpContextAccessor.HttpContext;
        }
    }
}
