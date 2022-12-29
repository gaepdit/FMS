using ClosedXML.Excel;
using System.Collections.Generic;
using System.IO;

namespace FMS
{
    public static class ExportHelper
    {
        public static byte[] ExportExcelAsByteArray<T>(this IEnumerable<T> list)
        {
            var ms = new MemoryStream();
            var wb = new XLWorkbook();
            var ws = wb.AddWorksheet("Search_Results");
            // insert the IEnumerable data
            ws.Cell(1, 1).InsertTable(list);
            ws.Columns().AdjustToContents(1, 100);

            wb.SaveAs(ms);
            return ms.ToArray();
        }
    }
}