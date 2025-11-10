using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Domain.Entities
{
    public class EventContractor : BaseActiveModel, INamedModel
    {
        public EventContractor() { }
        public EventContractor(EventContractorCreateDto eventContractor)
        {
            Name = eventContractor.Name;
            Description = eventContractor.Description;
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public void TrimAll()
        {
            Name = Name?.Trim();
            Description = Description?.Trim();
        }
    }
}
