using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Infrastructure.SeedData;
using System;

namespace TestHelpers
{
    public static class DataHelpers
    {
        public static Facility[] Facilities = DevSeedData.GetFacilities();
        public static County[] Counties = ProdSeedData.GetCounties();
        public static FacilityStatus[] FacilityStatuses = DevSeedData.GetFacilityStatuses();

        public static FacilityDetailDto GetFacilityDetail(Guid id)
        {
            var facility = Array.Find(Facilities, e => e.Id == id);

            return new FacilityDetailDto(facility)
            {
                County = GetCounty(facility.CountyId),
                FacilityStatus = GetFacilityStatus(facility.FacilityStatusId)
            };
        }

        public static County GetCounty(int id) => Array.Find(Counties, e => e.Id == id);
        public static FacilityStatus GetFacilityStatus(Guid id) => Array.Find(FacilityStatuses, e => e.Id == id);
    }
}
