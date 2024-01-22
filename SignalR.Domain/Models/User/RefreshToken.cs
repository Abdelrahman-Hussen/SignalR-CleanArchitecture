namespace SignalR.Domain.Models
{
    public class RefreshToken : Entity
    {
        public string Token { get; set; }
        public DateTime ExpiresOn { get; set; }
        public bool IsExpired => DateTime.Now >= ExpiresOn;
        public DateTime? RevokedOn { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; }
        public string UserId { get; set; }

        public void Revoke()
        {
            RevokedOn = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
    }
}
