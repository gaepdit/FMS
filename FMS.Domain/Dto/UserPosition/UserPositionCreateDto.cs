namespace FMS.Domain.Dto
{
    public class UserPositionCreateDto
    {
        [Display(Name = "User Position")]
        [Required(ErrorMessage = "User Position Name is required.")]
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
