using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;

namespace FMS.Domain.Entities
{
    public class UserProgram : BaseActiveModel, INamedModel
    {
        public UserProgram() { }

        public UserProgram(UserProgramCreateDto userProgram)
        {
            Name = userProgram.Name;
            Description = userProgram.Description;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
            Description = Description?.Trim();
        }
    }
}
