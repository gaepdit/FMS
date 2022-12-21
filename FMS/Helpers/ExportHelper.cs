using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using ClosedXML.Excel;
using Spire.Pdf;
using Spire.Pdf.Fields;
using Spire.Pdf.Widget;

namespace FMS
{
    public static class ExportHelper
    {
        /// <summary>
        /// Takes in a list of generic T, input it into the XLWorkbook, convert it to a byte array.
        /// </summary>
        /// <param name="list">A list of FacilityDetailDtoScalar or FacilityMapSummaryDtoScalar</param>
        /// <typeparam name="T">FacilityDetailDtoScalar or FacilityMapSummaryDtoScalar</typeparam>
        /// <returns>A byte array to use in File()</returns>
        public static byte[] ExportExcelAsByteArray<T>(this IEnumerable<T> list)
        {
            var ms = new MemoryStream();
            var wb = new XLWorkbook();
            var ws = wb.AddWorksheet("Search_Results");
            // insert the Enumberable data
            ws.Cell(1, 1).InsertTable(list);
            ws.Columns().AdjustToContents(1, 100);
            wb.SaveAs(ms);
            return ms.ToArray();
        }
        
        /// <summary>
        /// Takes in a list of RetentionRecordDetailDto, fill the information into the blank pdf form
        /// accordingly, and convert it to a byte array.
        /// </summary>
        /// <param name="list">A list of RetentionRecordDetailDto</param>
        /// <param name="maxCol">The maximum number of rows the current blank pdf doc can support</param>
        /// <param name="blankFilePath">The path to the blank Pdf document</param>
        /// <typeparam name="T">RetentionRecordDetailDto</typeparam>
        /// <returns></returns>
        public static byte[] ExportPdfAsByteArray<T>(this IEnumerable<T> list, int maxCol=18,
            string blankFilePath="../FMS/wwwroot/BlankRequestForm.pdf")
        {
            PdfDocument mainDoc = new PdfDocument();
            // break the list into chunks of 18 elements
            var smallerLists = list.Chunk(maxCol);
            // need to create a list to keep all object references.
            // declare a variable inside the scope will not work
            List<PdfDocument> pdfDocuments = new List<PdfDocument>();
            foreach (var smallerList in smallerLists)
            {
                PdfDocument currDocument = smallerList.GeneratePdfPage(maxCol, blankFilePath);
                pdfDocuments.Add(currDocument);
                // 10 is the limit for the free tier
                if (pdfDocuments.Count >= 10)
                    break;
            }
            
            // add all of the pages to the main document
            foreach (var pdfDocument in pdfDocuments)
                mainDoc.AppendPage(pdfDocument);
            
            // Creates a new Memory stream
            MemoryStream stream = new MemoryStream();
            // Saves the document as stream
            mainDoc.SaveToStream(stream, FileFormat.PDF);
            // dispose all of the pages
            foreach (var pdfDocument in pdfDocuments)
                pdfDocument.Close();
            mainDoc.Close();
            // Converts the PdfDocument object to byte form.
            return stream.ToArray();
        }
        
        /// <summary>
        /// Takes in a chunked list of RetentionRecordDetailDto and blank form path, fill the
        /// information from the list into the Pdf page and returns it.
        /// </summary>
        /// <param name="smallerList">The chunked/smaller list of RetentionRecordDetailDto</param>
        /// <param name="maxCol">The maximum number of rows the current blank pdf doc can support</param>
        /// <param name="blankFilePath">The path to the blank Pdf document</param>
        /// <typeparam name="T">RetentionRecordDetailDto</typeparam>
        /// <returns>A Pdf page</returns>
        /// <exception cref="ArgumentException">A page can have at most 18 rows</exception>
        private static PdfDocument GeneratePdfPage<T>(this T[] smallerList, int maxCol, string blankFilePath)
        {
            if (smallerList.Length > maxCol)
                throw new ArgumentException("The input list's length should not exceed " + maxCol);
            PdfDocument currDocument = new PdfDocument();
            currDocument.LoadFromFile(blankFilePath);
            for (int index = 1; index <= smallerList.Length; index++)
            {
                var currRetentionRecordDetail = smallerList[index - 1];
                    
                PdfFormWidget formWidget = (PdfFormWidget) currDocument.Form;
                for (int i = 0; i < formWidget.FieldsWidget.List.Count; i++)
                {
                    //Fill the data for Text Box field
                    PdfField field = (PdfField) formWidget.FieldsWidget.List[i];
                    if (field is PdfTextBoxFieldWidget)
                    {
                        PdfTextBoxFieldWidget textBoxField = (PdfTextBoxFieldWidget) field;
                        // if (sampleDataTextbox.ContainsKey(textBoxField.Name))
                        // {
                        //     textBoxField.Text = sampleDataTextbox[textBoxField.Name];
                        // }
                    }
                }
            }
            return currDocument;
        }
    }
}