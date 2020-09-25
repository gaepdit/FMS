using FMS.Domain.Dto;
using FMS.Domain.Entities;
using System;
using System.Linq;

namespace TestHelpers
{
    public static partial class DataHelpers
    {
        private static File GetFile(Guid id) => Files.Find(e => e.Id == id);

        public static FileDetailDto GetFileDetail(Guid id)
        {
            var file = Files.Find(e => e.Id == id);

            var fileDetail = new FileDetailDto(file);
            fileDetail.Facilities.AddRange(Facilities
                .Where(e => e.Active)
                .Where(e => e.FileId == file.Id)
                .Select(e => GetFacilitySummary(e.Id)).ToList());
            fileDetail.Cabinets.AddRange(GetCabinetSummariesForFile(id));

            return fileDetail; 
        }
    }
}