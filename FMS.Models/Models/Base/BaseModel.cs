using System;

namespace FMS.Models.Models
{
    public abstract class BaseModel
    {
        // Unique Identifier for object instance
        public Guid Id { get; set; }
    }
}
