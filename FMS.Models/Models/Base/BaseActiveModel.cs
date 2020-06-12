using FMS.Models.Models.Interfaces;

namespace FMS.Models.Models
{
    public abstract class BaseActiveModel : BaseModel, IActive
    {
        public bool Active { get; set; } = true;
    }
}
