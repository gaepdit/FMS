using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Entities
{
    public class County
    {
        // This list will not change, so no need for "BaseActiveModel"
        public int Id { get; set; }

        [Display(Name = "County")]
        [StringLength(20)]
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
