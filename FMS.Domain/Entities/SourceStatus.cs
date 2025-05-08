using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;
using System;

namespace FMS.Domain.Entities
{
    public class SourceStatus : BaseActiveModel
    {
        public SourceStatus() { }

        public SourceStatus(SourceStatusCreateDto sourceStatus)
        {
            Name = sourceStatus.Name;
        }

        public string Name { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
        }
    }
}
