namespace SignalR.Application.Features.Auth
{
    public record RegisterDto
    {
        [Required]
        public string FullName { get; init; }
        [Required]
        public string UserName { get; init; }
        [Required]
        public string Email { get; init; }
        [Required]
        public string PhoneNumber { get; init; }
        [Required]
        public string Password { get; init; }
    }
}
