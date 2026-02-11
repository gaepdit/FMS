using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Domain.Dto
{
    public enum EventReportType
    {
        Pending,
        Compliance,
        Completed,
        CompletedOutstanding,
        CompletedByCO,
        NoActionTaken
    }
}
