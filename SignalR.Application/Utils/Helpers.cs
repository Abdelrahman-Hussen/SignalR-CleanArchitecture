using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace SingalR.Application.Utils
{
    public static class Helpers
    {
        public static string ArrangeValidationErrors(List<ValidationFailure> validationFailures)
        {
            StringBuilder errors = new StringBuilder();

            foreach (var error in validationFailures)
                errors.Append($"{error}\n");

            return errors.ToString();
        }

        public static string ArrangeIdentityErrors(IEnumerable<IdentityError> identityError)
        {
            var errors = new StringBuilder();

            foreach (var error in identityError)
                errors.Append($"{error.Description}\n");

            return errors.ToString();
        }

        public static string BaseUrl()
        {
            var httpContextAccessor = new HttpContextAccessor();
            var request = httpContextAccessor.HttpContext!.Request;
            return $"{request.Scheme}://{request.Host}{request.PathBase}/";
        }
    }
}
