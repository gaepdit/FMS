using FMS.Domain.Entities.Base;
using FMS.Domain.Utils;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
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

        // Readonly properties
        public string Name => FileLabel;

        // Static methods
        public static string CountyString(int countyNum)
        {
            if (Counties.TrueForAll(e => e.Id != countyNum))
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
            Regex.IsMatch(fileLabel, FileLabelPattern, RegexOptions.None, TimeSpan.FromMilliseconds(100));
    }
}
