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

        public Guid OrgUnitId { get; set; }

        public string Position { get; set; }

        public IXmlSerializable Settings { get; set; }
    }
}
