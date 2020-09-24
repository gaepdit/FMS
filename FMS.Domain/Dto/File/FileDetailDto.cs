﻿using FMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FMS.Domain.Dto
{
    public class FileDetailDto
    {
        public FileDetailDto(File file)
        {
            Id = file.Id;
            Active = file.Active;
            FileLabel = file.FileLabel;
            Facilities = file.Facilities?
                .Where(e => e.Active)
                .Select(e => new FacilitySummaryDto(e)).ToList()
                ?? new List<FacilitySummaryDto>();
            Cabinets = file.CabinetFiles?
                .Select(c => new CabinetSummaryDto(c.Cabinet)).ToList()
                ?? new List<CabinetSummaryDto>();
        }

        public Guid Id { get; }
        public bool Active { get; }

        [Display(Name = "File Label")]
        public string FileLabel { get; }

        public List<CabinetSummaryDto> Cabinets { get; set;  }
        public List<FacilitySummaryDto> Facilities { get; set;  }
    }
}
