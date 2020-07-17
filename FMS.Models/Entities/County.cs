using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Entities
{
    public class County
    {
        // This list will not change, so no need for "BaseActiveModel"
        public int Id { get; set; }

        [StringLength(20)]
        public string Name { get; set; }
    }
}
