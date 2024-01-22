
namespace SignalR.Domain.Primitives
{
    public class EntityWithId : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
    }
}
