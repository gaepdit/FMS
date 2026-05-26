using FMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace FMS.Domain.Entities.Users
{
    public class UserProgram : BaseActiveModel
    {
        public UserProgram() { }

        public UserProgram(int id) { }

        public Guid UserId { get; set; }

        public OrganizationalUnit OrgUnit { get; set; }

        public Guid PositionId { get; set; }

        public IXmlSerializable Settings { get; set; }
    }
}
