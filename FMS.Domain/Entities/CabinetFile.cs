using System;

namespace FMS.Domain.Entities
{
    /// <summary>
    /// Join table representing the many-to-many relationship between Cabinets and Files
    /// </summary>
    public class CabinetFile
    {
        public Guid CabinetId { get; set; }
        public Cabinet Cabinet { get; set; }

        public Guid FileId { get; set; }
        public File File { get; set; }
    }
}
