using FMS.Domain.Entities.Interfaces;

namespace FMS.Domain.Entities.Base
{
    public abstract class BaseActiveModel : BaseModel, IActive
    {
        public bool Active { get; set; } = true;
    }
}
