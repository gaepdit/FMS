namespace FMS.Domain.Dto
{
    public class UserProgramCreateDto
    {
        [Display(Name = "User Program")]
        [Required(ErrorMessage = "User Program Name is required.")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
            Description = Description?.Trim();
        }
    }
}
