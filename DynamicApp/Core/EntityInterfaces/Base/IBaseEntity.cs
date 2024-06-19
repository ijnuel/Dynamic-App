using Core.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.EntityInterfaces.Base
{
    public interface IBaseEntity
    {
        public Guid Id { get; set; }
        public string CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? LastModifiedById { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsArchived { get; set; }
    }
}
