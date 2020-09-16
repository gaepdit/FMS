using FMS.Domain.Dto;
using FMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestHelpers
{
    public static partial class DataHelpers
    {
        public static List<CabinetFile> GetCabinetFileJoinsForCabinet(Guid cabinetId) =>
            CabinetFiles.Where(e => e.CabinetId == cabinetId).ToList();

        public static List<CabinetFile> GetCabinetFileJoinsForFile(Guid fileId) =>
            CabinetFiles.Where(e => e.FileId == fileId).ToList();

        public static List<string> GetCabinetsForFile(Guid fileId) =>
            GetCabinetFileJoinsForFile(fileId)
            .Select(c => GetCabinetSummary(c.CabinetId).Name)
            .ToList();

        public static CabinetSummaryDto GetCabinetSummary(Guid id) =>
            new CabinetSummaryDto(Cabinets.Find(e => e.Id == id));

        public static CabinetDetailDto GetCabinetDetail(Guid id)
        {
            var cabinet = Cabinets.Find(e => e.Id == id);

            var cabinetDetail = new CabinetDetailDto(cabinet);

            foreach (var cf in GetCabinetFileJoinsForCabinet(id))
            {
                cabinetDetail.Files.Add(new FileSummaryDto(GetFile(cf.FileId)));
            }

            return cabinetDetail;
        }
    }
}
