using System;
using System.Collections.Generic;
using System.Linq;
using FMS.Domain.Entities;
using FMS.Domain.Utils;

namespace FMS.Domain.Dto
{
    public static class CabinetExtensions
    {
        public static List<string> GetCabinetsForFile(this IEnumerable<CabinetSummaryDto> cabinets, string fileLabel, bool testValidity = true)
        {
            if (testValidity) 
            {
                Prevent.NullOrEmpty(fileLabel, nameof(fileLabel));
                if (!File.IsValidFileLabelFormat(fileLabel))
                {
                    throw new ArgumentException($"File label '{fileLabel}' is invalid.", nameof(fileLabel));
                }
            }

            return cabinets.Where(e =>
                string.CompareOrdinal(e.FirstFileLabel, fileLabel) < 0 &&
                string.CompareOrdinal(fileLabel, e.LastFileLabel) < 0 ||
                fileLabel == e.FirstFileLabel
            ).Select(e => e.Name).ToList();
        }
    }
}