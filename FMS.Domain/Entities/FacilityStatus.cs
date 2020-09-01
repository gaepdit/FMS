using FMS.Domain.Entities.Base;
using System;

namespace FMS.Domain.Entities
{
    public class FacilityStatus : BaseActiveModel
    {
        public string Status { get; set; }

        //public Guid EnvironmentalInterestId { get; set; }
        public  EnvironmentalInterest EnvironmentalInterest { get; set; }  //
    }
}