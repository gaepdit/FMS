using System;
using System.Collections.Generic;
using System.Linq;
using FMS.Domain.Entities;
using FMS.Domain.Utils;

namespace FMS.Domain.Dto
{
    public static class CabinetExtensions
    {
        public static List<string> GetCabinetsForFile(
            this IEnumerable<CabinetSummaryDto> cabinets, string fileLabel) =>
            cabinets.GetCabinetSummariesForFile(fileLabel).Select(e => e.Name).ToList();

        private static IEnumerable<CabinetSummaryDto> GetCabinetSummariesForFile(
            this IEnumerable<CabinetSummaryDto> cabinets, string fileLabel)
        {
            Prevent.NullOrEmpty(fileLabel, nameof(fileLabel));
            if (!File.IsValidFileLabelFormat(fileLabel))
            {
                throw new ArgumentException($"File label '{fileLabel}' is invalid.", nameof(fileLabel));
            }

            return cabinets.Where(e =>
                string.CompareOrdinal(e.FirstFileLabel, fileLabel) < 0 &&
                string.CompareOrdinal(fileLabel, e.LastFileLabel) < 0 ||
                fileLabel == e.FirstFileLabel
            );
        }
    }
}