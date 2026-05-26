namespace FMS.Domain.Dto
{
    public class UserProgramCreateDto
    {
        [Display(Name = "User Position")]
        [Required(ErrorMessage = "User Position Name is required.")]
        public string Name { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
        }
    }
}
