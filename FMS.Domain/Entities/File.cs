using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using FMS.Domain.Entities.Base;
using FMS.Domain.Utils;
using static FMS.Domain.Data.Data;

namespace FMS.Domain.Entities
{
    public class File : BaseActiveModel, INamedModel
    {
        public File() { }

        public File(int countyNum, int sequence) =>
            FileLabel = string.Concat(CountyString(countyNum), "-", SequenceString(sequence));

        /// <summary>
        /// Internal ID from the Programs, consisting of the 3-digit county number (with possible leading zero)
        /// and a 4-digit system-generated sequential number for each county (xxx-xxxx)
        /// </summary>
        [StringLength(9)]
        public string FileLabel { get; set; }

        public ICollection<Facility> Facilities { get; set; }

        // Files and Cabinets have a many-to-many relationship
        public ICollection<CabinetFile> CabinetFiles { get; set; }

        public string Name => FileLabel;
        public int CountyNumber => int.Parse(FileLabel.Substring(0, 3));
        public int SequenceNumber => int.Parse(FileLabel.Substring(4, 4));

        public static string CountyString(int countyNum)
        {
            if (Counties.All(e => e.Id != countyNum))
            {
                throw new ArgumentException($"County ID {countyNum} does not exist.", nameof(countyNum));
            }

            return countyNum.ToString().PadLeft(3, '0');
        }

        public static string SequenceString(int sequence)
        {
            Prevent.OutOfRange(sequence, nameof(sequence), 1, 9999);
            return sequence.ToString().PadLeft(4, '0');
        }

        public const string FileLabelPattern = @"^\d{3}-\d{4}$";

        public static bool IsValidFileLabelFormat(string fileLabel) =>
            Regex.IsMatch(fileLabel, FileLabelPattern);
    }
}