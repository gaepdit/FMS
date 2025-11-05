using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;
using Microsoft.Graph.Models.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Domain.Entities
{
    public class LocationClass : BaseActiveModel, INamedModel
    {
        public LocationClass() { }

        public LocationClass(LocationClassCreateDto locClass)
        {
            Name = locClass.Name;
            Description = locClass.Description;
        }

        [Display(Name = "Class Name")]
        public string Name { get; set; }

        [Display(Name = "Class Description")]
        public string Description { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
            Description = Description?.Trim();
        }
    }
}
