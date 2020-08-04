using FMS.Domain.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Entities.Base
{
    public abstract class BaseActiveModel : BaseModel, IActive
    {
        [Display(Name = "Active Site")]
        public bool Active { get; set; } = true;
    }
}
