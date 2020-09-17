using FMS.Domain.Dto;
using FMS.Domain.Entities;
using System;
using System.Linq;

namespace TestHelpers
{
    public static partial class DataHelpers
    {
        public static File GetFile(Guid id) => Files.Find(e => e.Id == id);

        public static FileDetailDto GetFileDetail(Guid id)
        {
            var file = Files.Find(e => e.Id == id);
            return new FileDetailDto(file)
            {
                Facilities = Facilities
                    .Where(e => e.Active)
                    .Where(e => e.FileId == file.Id)
                    .Select(e => GetFacilitySummary(e.Id)).ToList(),
                Cabinets = GetCabinetSummariesForFile(id)
            };
        }
    }
}
