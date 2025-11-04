using FMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Domain.Entities
{
    public class LocationClass : BaseActiveModel, INamedModel
    {
        public LocationClass() { }

        public LocationClass(string name, string description)
        {
            Name = name;
            Description = description;
        }
        public string Name { get; set; }
        public string Description { get; set; }


    }
}
