using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class UserProgramSummaryDto
    {
        public UserProgramSummaryDto(UserProgram userProgram)
        {
            Id = userProgram.Id;
            Active = userProgram.Active;
            Name = userProgram.Name;
        }

        public Guid Id { get; }

        public bool Active { get; }

        public string Name { get; set; }
    }
}
