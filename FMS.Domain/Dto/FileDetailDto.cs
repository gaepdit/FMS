using FMS.Domain.Entities;
using System;

namespace FMS.Domain.Dto
{
    public class FileDetailDto
    {
        public FileDetailDto(File file)
        {
            Id = file.Id;
            Active = file.Active;
            FileLabel = file.FileLabel;
        }

        public Guid Id;

        public bool Active { get; set; }

        public string FileLabel { get; set; }
    }
}
