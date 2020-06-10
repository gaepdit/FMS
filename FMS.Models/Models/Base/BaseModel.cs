using System;

namespace FMS.Models.Models.Base
{
    public abstract class BaseModel
    {
        // Unique Identifier for object instance
        public Guid Id { get; set; }

        // number to determine which Security levels have access to add, delete, change records
        public int AccessLevel { get; set; }

        public DateTimeOffset? UpdatedDate { get; set; }
        public Guid? UpdatedById { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
        public Guid? CreatedById { get; set; }
    }
}
