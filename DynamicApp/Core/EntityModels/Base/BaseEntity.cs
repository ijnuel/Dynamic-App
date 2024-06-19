using Core.EntityInterfaces.Base;
using System.ComponentModel.DataAnnotations;

namespace Core.EntityModels.Base
{
    public class BaseEntity : IBaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string CreatedById { get; set; } = default!;
        public DateTime CreatedDate { get; set; }
        public string? LastModifiedById { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsArchived { get; set; }
    }
}
