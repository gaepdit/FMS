using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;

namespace FMS.Domain.Entities
{
    /// <summary>
    /// Type/Environmental Interest
    /// </summary>
    public class FacilityType : BaseActiveModel, INamedModel
    {
        public FacilityType() { }

        public FacilityType(FacilityTypeCreateDto newFacilityType)
        {
            Name = newFacilityType.Name;
            Description = newFacilityType.Description;
        }

        [StringLength(20)]
        public string Name { get; set; }

        public string Description { get; set; }

        public string DisplayName => $"{Name} ({Description})";

        public const string FacilityTypeNamePattern = @"^\w*$";

        public static bool IsValidFacilityTypeName(string facilityTypeName) =>
            Regex.IsMatch(facilityTypeName, FacilityTypeNamePattern, RegexOptions.None, TimeSpan.FromMilliseconds(100));
    }
}
