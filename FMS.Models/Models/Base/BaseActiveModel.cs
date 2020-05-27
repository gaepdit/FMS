using FMS.Models.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FMS.Models.Models.Base
{
    public abstract class BaseActiveModel : BaseModel, IActive
    {
        public bool Active { get; set; } = true;
    }
}
