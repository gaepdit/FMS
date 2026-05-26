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
        }

        public string Name { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
        }
    }
}
