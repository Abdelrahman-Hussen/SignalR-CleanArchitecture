using SignalR.Domain.Models;
using SignalR.Domain.Specifications;

namespace SignalR.Application.Features.System
{
    internal class OTPSpecification : BaseSpecification<OTP>
    {
        public static OTPSpecification GetByEmail(string email)
        {
            var spec = new OTPSpecification();

            spec.AddCriteria(x => x.Email == email);

            return spec;
        }
    }
}
