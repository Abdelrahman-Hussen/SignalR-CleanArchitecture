namespace SignalR.Application.Features.Auth
{
    public record LoginDto
    {
        [Required]
        public string Email { get; init; }
        [Required]
        public string Password { get; init; }
        public string? RefreshToken { get; init; }
    }
}
