using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class UserPositionEditDto
    {
        public UserPositionEditDto() { }

        public UserPositionEditDto(UserPosition userPosition)
        {
            Id = userPosition.Id;
            Name = userPosition.Name;
            Description = userPosition.Description;
            Active = userPosition.Active;
        }

        public Guid Id { get; set; }

        [Display(Name = "User Position")]
        [Required(ErrorMessage = "User Position Name is required.")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        [Display(Name = "Is Active")]
        public bool Active { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
            Description = Description?.Trim();
        }
    }
}
