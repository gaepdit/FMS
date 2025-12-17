using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;
using System;

namespace FMS.Domain.Entities
{
    public class SourceStatus : BaseActiveModel, INamedModel
    {
        public SourceStatus() { }

        public SourceStatus(SourceStatusCreateDto sourceStatus)
        {
            Name = sourceStatus.Name;
            Description = sourceStatus.Description;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
            Description = Description?.Trim();
        }

        public string DisplayName => $"{Name} ({Description})";
    }
}
