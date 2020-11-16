using System;
using System.ComponentModel.DataAnnotations;
using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class CabinetSummaryDto
    {
        public CabinetSummaryDto(Cabinet cabinet)
        {
            Id = cabinet.Id;
            Active = cabinet.Active;
            Name = cabinet.Name;
            FirstFileLabel = cabinet.FirstFileLabel;
        }

        public Guid Id { get; }
        public bool Active { get; }

        [Display(Name = "Cabinet Number")]
        public string Name { get; }

        [Display(Name = "First File Label")]
        public string FirstFileLabel { get; }

        [Display(Name = "Last File Label")]
        public string LastFileLabel
        {
            get => _lastFileLabel ?? "999-9999";
            set => _lastFileLabel = value;
        }

        private string _lastFileLabel;
    }
}