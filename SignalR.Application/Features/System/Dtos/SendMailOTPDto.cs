namespace SignalR.Application.Features.System
{
    public class SendMailOTPDto
    {
        [Required]
        public string Email { get; init; }
    }
}
