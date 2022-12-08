using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using CsvHelper;
using CsvHelper.Configuration;

namespace FMS
{
    public static class CsvHelper
    {
        public static async Task<byte[]> GetCsvByteArrayAsync<T>(this IEnumerable list)
            where T : ClassMap
        {
            MemoryStream ms = null;
            StreamWriter writer = null;
            CsvWriter csv = null;

            try
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture) {SanitizeForInjection = true};

                ms = new MemoryStream();
                writer = new StreamWriter(ms, Encoding.UTF8);
                csv = new CsvWriter(writer,config);

                csv.Context.RegisterClassMap<T>();
                await csv.WriteRecordsAsync(list);

                await csv.FlushAsync();
                await writer.FlushAsync();
                await ms.FlushAsync();

                return ms.ToArray();
            }
            finally
            {
                if (csv != null) await csv.DisposeAsync();
                if (writer != null) await writer.DisposeAsync();
                if (ms != null) await ms.DisposeAsync();
            }
        }
        public static byte[] ExportExcelAsByteArray<T>(this IEnumerable<T> list)
        {
            var ms = new MemoryStream();
            var wb = new XLWorkbook();
            var ws = wb.AddWorksheet("FMS_Search_Results");
            // insert the IEnumberable data
            ws.Cell(1, 1).InsertTable(list);
            ws.Columns().AdjustToContents();

            wb.SaveAs(ms);
            return ms.ToArray();
        }
    }
}