using FMS.Domain.Entities;
using System.ComponentModel;

namespace FMS.Domain.Dto
{
    public class UserProgramSummaryDto
    {
        public UserProgramSummaryDto(UserProgram userProgram)
        {
            Id = userProgram.Id;
            Active = userProgram.Active;
            Name = userProgram.Name;
            Description = userProgram.Description;
        }

        public Guid Id { get; }

        public bool Active { get; }

        [DisplayName("User Program")]
        public string Name { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }
    }
}
