using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using Spire.Pdf;

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
        
        public static byte[] ExportPdfAsByteArray<T>(this IEnumerable<T> list, int maxCol=18,
            string blankFilePath="../FMS/wwwroot/BlankRequestForm.pdf")
        {
            PdfDocument mainDoc = new PdfDocument();
            // break the list into chunks of 18 elements
            var smallerLists = list.Chunk(maxCol);
            Console.WriteLine("The size of the original list: " + list.Count());
            Console.WriteLine("The size of smaller lists: " + smallerLists.Count());
            // need to create a list to keep all object references.
            // declare a variable inside the scope will not work
            List<PdfDocument> pdfDocuments = new List<PdfDocument>();
            foreach (var smallerList in smallerLists)
            {
                PdfDocument currDocument = new PdfDocument();
                currDocument.LoadFromFile(blankFilePath);
                for (int i = 1; i <= smallerList.Length; i++)
                {
                    var currRetentionRecordDetail = smallerList[i - 1];
                }
                pdfDocuments.Add(currDocument);
                // 10 is the limit for the free tier
                if (pdfDocuments.Count >= 10)
                {
                    break;
                }
            }
            
            // add all of the pages to the main document
            foreach (var pdfDocument in pdfDocuments)
            {
                mainDoc.AppendPage(pdfDocument);
            }
            
            // Creates a new Memory stream
            MemoryStream stream = new MemoryStream();
            // Saves the document as stream
            mainDoc.SaveToStream(stream, FileFormat.PDF);
            // dispose all of the pages
            foreach (var pdfDocument in pdfDocuments)
            {
                pdfDocument.Close();
            }
            mainDoc.Close();
            // Converts the PdfDocument object to byte form.
            return stream.ToArray();
        }
    }
}