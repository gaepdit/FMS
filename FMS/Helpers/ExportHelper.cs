using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClosedXML.Excel;
using FMS.Domain.Dto;
using FMS.Domain.Services;
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
            // insert the enumerable data
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
        /// <param name="currentUser">The current user of the program</param>
        /// <param name="maxCol">The maximum number of rows the current blank pdf doc can support</param>
        /// <param name="blankFilePath">The path to the blank Pdf document</param>
        /// <param name="freeTierLimit">The max number of pages the program can generate</param>
        /// <returns>A byte array to use in File()</returns>
        public static byte[] ExportPdfAsByteArray(
            IEnumerable<RetentionRecordDetailDto> list, UserView currentUser, int maxCol=18,
            string blankFilePath="~/wwwroot/BlankRequestForm.pdf", int freeTierLimit=10)
        {
            PdfDocument mainDoc = new PdfDocument();
            // break the list into chunks of 18 elements
            var smallerLists = list.Chunk(maxCol);
            // need to create a list to keep all object references.
            // declare a variable inside the scope will not work
            List<PdfDocument> pdfDocuments = new List<PdfDocument>();
            foreach (var smallerList in smallerLists)
            {
                PdfDocument currDocument = GeneratePdfPage(smallerList, currentUser, maxCol, blankFilePath);
                pdfDocuments.Add(currDocument);
                // limit for the free tier
                if (pdfDocuments.Count >= freeTierLimit)
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
        /// <param name="currentUser">The current user of the program</param>
        /// <param name="maxCol">The maximum number of rows the current blank pdf doc can support</param>
        /// <param name="blankFilePath">The path to the blank Pdf document</param>
        /// <returns>A Pdf page</returns>
        /// <exception cref="ArgumentException">A page can have at most 18 rows</exception>
        private static PdfDocument GeneratePdfPage(RetentionRecordDetailDto[] smallerList,
            UserView currentUser, int maxCol, string blankFilePath)
        {
            if (smallerList.Length > maxCol)
                throw new ArgumentException("The input list's length should not exceed " + maxCol);
            PdfDocument currDocument = new PdfDocument();
            currDocument.LoadFromFile(blankFilePath);
            Dictionary<string, string> dictionaryTextbox = GenerateRetentionRecordDictionary(smallerList, currentUser);
            // iterate through all user fill-able widget
            PdfFormWidget formWidget = (PdfFormWidget) currDocument.Form;
            for (int i = 0; i < formWidget.FieldsWidget.List.Count; i++)
            {
                //Fill the data for Text Box field
                PdfField field = (PdfField) formWidget.FieldsWidget.List[i];
                PdfTextBoxFieldWidget textBoxField = field as PdfTextBoxFieldWidget;
                if (textBoxField != null && dictionaryTextbox.ContainsKey(textBoxField.Name))
                {
                    textBoxField.Text = dictionaryTextbox[textBoxField.Name];
                }
            }
            return currDocument;
        }
        
        /// <summary>
        /// A helper method to convert a list of RetentionRecordDetailDto to a dictionary.
        /// The key of the dictionary represents the cell name in the pdf, and the value represents the value
        /// to be filled in in the pdf.
        /// </summary>
        /// <param name="list">A list of RetentionRecordDetailDto</param>
        /// <param name="currentUser">The current user of the program</param>
        /// <returns>A dictionary with the key represents the cell name and the value represents the value</returns>
        private static Dictionary<string, string> GenerateRetentionRecordDictionary(
            RetentionRecordDetailDto[] list, UserView currentUser)
        {
            Dictionary<string, string> dictionaryTextbox = new Dictionary<string, string>();
            // start from 1 to name the key
            int i = 1;
            foreach (var retentionRecord in list)
            {
                dictionaryTextbox.Add("Consignment Number" + i,
                    retentionRecord.ConsignmentNumber == null ? "" : retentionRecord.ConsignmentNumber);
                dictionaryTextbox.Add("Item" + i,
                    retentionRecord.BoxNumber == null ? "" : retentionRecord.BoxNumber);
                dictionaryTextbox.Add("Location number from transmittal" + i,
                    retentionRecord.ShelfNumber == null ? "" : retentionRecord.ShelfNumber);
                i++;
            }
            // add key-value pair for the date
            dictionaryTextbox.Add("Date of Request", DateTime.Now.ToString("MM/dd/yyyy"));
            // add key-value pair for the user information
            dictionaryTextbox.Add("Name of Requestor",
                currentUser.DisplayName == null ? "" : currentUser.DisplayName);
            dictionaryTextbox.Add("EMail",
                currentUser.Email == null ? "" : currentUser.Email);
            return dictionaryTextbox;
        }
    }
}