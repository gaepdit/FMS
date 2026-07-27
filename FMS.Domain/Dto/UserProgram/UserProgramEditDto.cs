using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class UserProgramEditDto
    {
        public UserProgramEditDto() { }

        public UserProgramEditDto(UserProgram userProgram)
        {
            Id = userProgram.Id;
            Name = userProgram.Name;
            Description = userProgram.Description;
            Active = userProgram.Active;
        }

        public Guid Id { get; set; }

        [Display(Name = "User Program")]
        [Required(ErrorMessage = "User Program Name is required.")]
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
