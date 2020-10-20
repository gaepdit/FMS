﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class CabinetDetailDto
    {
        public CabinetDetailDto(Cabinet cabinet)
        {
            Id = cabinet.Id;
            Active = cabinet.Active;
            CabinetNumber = cabinet.CabinetNumber;
            Name = cabinet.Name;
            FirstFileLabel = cabinet.FirstFileLabel;
            Files = cabinet.CabinetFiles?
                    .Select(c => new FileSummaryDto(c.File)).ToList()
                ?? new List<FileSummaryDto>();
        }

        public Guid Id { get; }
        public bool Active { get; }
        public int CabinetNumber { get; }

        [Display(Name = "Cabinet Number")]
        public string Name { get; }

        [Display(Name = "First File Label")]
        public string FirstFileLabel { get; }

        public List<FileSummaryDto> Files { get; }
    }
}