namespace SignalR.Domain.Models
{
    public class OTP : EntityWithId
    {
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? OTPCode { get; set; }
        public DateTime? OTPExpirationDate { get; set; }
    }
}
