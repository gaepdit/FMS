using FMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace FMS.Domain.Entities
{
    public class SiteSummaryLog : BaseActiveModel
    {
        public SiteSummaryLog() { }

        public SiteSummaryLog(Guid id, string fileName, string filePath, string fileType)
        {
            Id = id;
            PdfFileName = fileName;
            PdfFilePath = filePath;
            PdfFileType = fileType;
        }

        public string PdfFileName { get; set; }

        public string PdfFilePath { get; set; }

        public string PdfFileType { get; set; }
    }
}
