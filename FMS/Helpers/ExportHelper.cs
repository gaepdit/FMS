using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;

namespace FMS
{
    public static class ExportHelper
    {
        public static byte[] ExportExcelAsByteArray<T>(this IEnumerable<T> list)
        {
            var ms = new MemoryStream();
            var wb = new XLWorkbook();
            var ws = wb.AddWorksheet("Search_Results");
            // insert the IEnumberable data
            ws.Cell(1, 1).InsertTable(list);
            ws.Columns().AdjustToContents(1, 100);

            wb.SaveAs(ms);
            return ms.ToArray();
        }
    }
}