using FMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static FMS.Domain.Entities.Facility;

namespace FMS.Infrastructure.SeedData
{
    public static partial class DevSeedData
    {
        public static Facility[] GetFacilities()
        {
            return new List<Facility>
            {
                new Facility
                {

                },
                new Facility
                {

                }
            }.ToArray();
        }
    }
}
