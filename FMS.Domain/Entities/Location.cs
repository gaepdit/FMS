﻿using System;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;

namespace FMS.Domain.Entities
{
    public class Location : BaseActiveModel
    {
        public Location() { }

        public Location(Guid id, LocationCreateDto location)
        {
            FacilityId = id;
            Score = location.Score;
        }

        public Guid FacilityId { get; set; }
        public string Score { get; set; }
    }
}
