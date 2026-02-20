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
        public enum ReportType
        {
            None,
            Normal,
            Pending,
            Map,
            Assignment,
            Delisted,
            DelistedByRange,
            Event,
            EventPending,
            EventCompliance,
            EventCompleted,
            EventCompletedOutstanding,
            EventActivityCompleted,
            EventNoActionTaken,
            PAF
        }

        /// <summary>
        /// Takes in a list of generic T, input it into the XLWorkbook, convert it to a byte array.
        /// </summary>
        /// <param name="list">A list of FacilityDetailDtoScalar or FacilityMapSummaryDtoScalar or EventSummaryDtoScalar or FacilityPendingDtoScalar</param>
        /// <typeparam name="T">FacilityDetailDtoScalar or FacilityMapSummaryDtoScalar or EventSummaryDtoScalar or FacilityPendingDtoScalar</typeparam>
        /// <returns>A byte array to use in File()</returns>
        public static byte[] ExportExcelAsByteArray<T>(this IEnumerable<T> list, 
            ReportType reportType, 
            DateOnly? startDate = null, 
            DateOnly? endDate = null,
            IEnumerable<T> vrpList = null,
            IEnumerable<T> brnList = null,
            int hsiCompCount = 0,
            int hsiRecCount = 0,
            int hsiCompAvg = 0,
            int vrpCompCount = 0,
            int vrpRecCount = 0,
            int vrpCompAvg = 0,
            int brnCompCount = 0,
            int brnRecCount = 0,
            int brnCompAvg = 0)
        {
            var ms = new MemoryStream();
            var wb = new XLWorkbook();
            
            IXLWorksheet ws;
            // insert the enumerable data
            IXLTable table; 

            if (reportType == ReportType.Pending)
            {
                ws = wb.AddWorksheet("Pending RNs");
                table = ws.Cell(1, 1).InsertTable(list);
                table.ShowHeaderRow = true;
                ws.Columns().AdjustToContents(1, 10000);
                ws.Column("E").Style.NumberFormat.NumberFormatId = (int)XLPredefinedFormat.DateTime.DayMonthYear4WithSlashes;
            }

            if (reportType == ReportType.Event)
            {
                ws = wb.AddWorksheet("Events");
                table = ws.Cell(1, 1).InsertTable(list);
                table.ShowHeaderRow = true;
                ws.Columns().AdjustToContents(1, 10000);
                ws.Column("E").Style.NumberFormat.NumberFormatId = (int)XLPredefinedFormat.DateTime.DayMonthYear4WithSlashes;
                ws.Column("F").Style.NumberFormat.NumberFormatId = (int)XLPredefinedFormat.DateTime.DayMonthYear4WithSlashes;
                ws.Column("G").Style.NumberFormat.NumberFormatId = (int)XLPredefinedFormat.DateTime.DayMonthYear4WithSlashes;
            }

            if (reportType == ReportType.Normal)
            {
                ws = wb.AddWorksheet("Results");
                table = ws.Cell(1, 1).InsertTable(list);
                table.ShowHeaderRow = true;
                ws.Columns().AdjustToContents(1, 10000);
                ws.Column(16).Style.NumberFormat.NumberFormatId = (int)XLPredefinedFormat.DateTime.DayMonthYear4WithSlashes;
                ws.Column(17).Style.NumberFormat.NumberFormatId = (int)XLPredefinedFormat.DateTime.DayMonthYear4WithSlashes;
            }

            if (reportType == ReportType.Map)
            {
                ws = wb.AddWorksheet("Map Results");
                table = ws.Cell(1, 1).InsertTable(list);
                table.ShowHeaderRow = true;
                ws.Columns().AdjustToContents(1, 10000);
            }

            if (reportType == ReportType.Assignment)
            {
                ws = wb.AddWorksheet("HSI Assignment List");
                table = ws.Cell(1, 1).InsertTable(list);
                table.ShowHeaderRow = true;
                ws.Columns().AdjustToContents(1, 10000);
            }

            if (reportType == ReportType.Delisted)
            {
                ws = wb.AddWorksheet("Delisted");
                table = ws.Cell(1, 1).InsertTable(list);
                table.ShowHeaderRow = true;
                ws.Columns().AdjustToContents(1, 10000);
                ws.Column("A").Style.NumberFormat.NumberFormatId = (int)XLPredefinedFormat.DateTime.DayMonthYear4WithSlashes;
            }

            if (reportType == ReportType.DelistedByRange)
            {
                ws = wb.AddWorksheet("Delisted By Date Range");
                table = ws.Cell(3, 1).InsertTable(list);
                table.ShowHeaderRow = true;
                ws.Columns().AdjustToContents(1, 10000);
                ws.Cell("A1").Value = "Delist Start Date";
                ws.Cell("B1").SetValue(startDate?.ToString("MM/dd/yyyy") ?? "");
                ws.Cell("A2").Value = "Delist End Date";
                ws.Cell("B2").SetValue(endDate?.ToString("MM/dd/yyyy") ?? "");
                ws.Column("E").Style.NumberFormat.NumberFormatId = (int)XLPredefinedFormat.DateTime.DayMonthYear4WithSlashes;
                ws.Column("F").Style.NumberFormat.NumberFormatId = (int)XLPredefinedFormat.DateTime.DayMonthYear4WithSlashes;

                table.ShowTotalsRow = true;
                table.Field("Acres").TotalsRowFunction = XLTotalsRowFunction.Sum;
            }

            if (reportType == ReportType.EventPending) 
            {
                ws = wb.AddWorksheet("Events Pending");

                table = ws.Cell(3, 1).InsertTable(list);
                table.ShowHeaderRow = true;
                ws.Columns().AdjustToContents(1, 10000);
                ws.Cell("D1").Style.Font.Bold = true;
                ws.Cell("D1").Style.Font.FontSize = 14;
                ws.Cell("D1").Value = "Events Pending";
                ws.Column(8).Style.NumberFormat.NumberFormatId = (int)XLPredefinedFormat.DateTime.DayMonthYear4WithSlashes;
                ws.Column(9).Style.NumberFormat.NumberFormatId = (int)XLPredefinedFormat.DateTime.DayMonthYear4WithSlashes;
            }

            if (reportType == ReportType.EventCompleted)
            {
                ws = wb.AddWorksheet("Events Completed");
                table = ws.Cell(3, 1).InsertTable(list);
                table.ShowHeaderRow = true;
                ws.Columns().AdjustToContents(1, 10000);
                ws.Cell("A1").Value = "Start Date";
                ws.Cell("B1").SetValue(startDate?.ToString("MM/dd/yyyy") ?? "");
                ws.Cell("A2").Value = "End Date";
                ws.Cell("B2").SetValue(endDate?.ToString("MM/dd/yyyy") ?? "");
                ws.Column(8).Style.NumberFormat.NumberFormatId = (int)XLPredefinedFormat.DateTime.DayMonthYear4WithSlashes;
                ws.Column(9).Style.NumberFormat.NumberFormatId = (int)XLPredefinedFormat.DateTime.DayMonthYear4WithSlashes;
            }

            if (reportType == ReportType.EventCompliance) 
            {
                ws = wb.AddWorksheet("Event Compliance");
                table = ws.Cell(3, 1).InsertTable(list);
                table.ShowHeaderRow = true;
                ws.Columns().AdjustToContents(1, 10000);

                ws.Cell("A1").Value = "Start Date";
                ws.Cell("B1").SetValue(startDate?.ToString("MM/dd/yyyy") ?? "");
                ws.Cell("A2").Value = "End Date";
                ws.Cell("B2").SetValue(endDate?.ToString("MM/dd/yyyy") ?? "");
                ws.Column(5).Style.NumberFormat.NumberFormatId = (int)XLPredefinedFormat.DateTime.DayMonthYear4WithSlashes;
                ws.Column(6).Style.NumberFormat.NumberFormatId = (int)XLPredefinedFormat.DateTime.DayMonthYear4WithSlashes;
                ws.Column(7).Style.NumberFormat.NumberFormatId = (int)XLPredefinedFormat.Number.Precision2;
            }

            if (reportType == ReportType.EventActivityCompleted)
            {
                ws = wb.AddWorksheet("Events Completed By CO");
                table = ws.Cell(3, 1).InsertTable(list);

                table.ShowHeaderRow = true;
                ws.Columns().AdjustToContents(1, 10000);
                ws.Column(6).Style.NumberFormat.NumberFormatId = (int)XLPredefinedFormat.DateTime.DayMonthYear4WithSlashes;
                ws.Column(7).Style.NumberFormat.NumberFormatId = (int)XLPredefinedFormat.DateTime.DayMonthYear4WithSlashes;
            }

            if (reportType == ReportType.EventNoActionTaken)
            {
                ws = wb.AddWorksheet("Events No Action Taken");
                table = ws.Cell(1, 1).InsertTable(list);
                table.ShowHeaderRow = true;
                ws.Columns().AdjustToContents(1, 10000);
                ws.Column(3).Style.NumberFormat.NumberFormatId = (int)XLPredefinedFormat.DateTime.DayMonthYear4WithSlashes;
            }

            if (reportType == ReportType.EventCompletedOutstanding)
            {
                

                // ******************** HSI Worksheet ******************
                ws = wb.AddWorksheet("HSI");
                table = ws.Cell(3, 1).InsertTable(list);
                table.ShowHeaderRow = true;
                
                // First Row formatting
                ws.Cell("A1").Style.Font.Bold = true;
                ws.Cell("A1").Style.Font.FontSize = 14;
                ws.Cell("A1").Value = "HSI";

                ws.Cell("C1").Style.Font.Bold = true;
                ws.Cell("C1").Style.Font.FontSize = 14;
                ws.Cell("C1").Value = "Start Date";
                ws.Cell("D1").Style.Font.Bold = true;
                ws.Cell("D1").Style.Font.FontSize = 14;
                ws.Cell("D1").SetValue(startDate?.ToString("MM/dd/yyyy") ?? "");

                ws.Cell("F1").Style.Font.Bold = true;
                ws.Cell("F1").Style.Font.FontSize = 14;
                ws.Cell("F1").Value = "End Date";
                ws.Cell("G1").Style.Font.Bold = true;
                ws.Cell("G1").Style.Font.FontSize = 14;
                ws.Cell("G1").SetValue(endDate?.ToString("MM/dd/yyyy") ?? "");

                //Second Row formatting
                ws.Cell("A2").Style.Font.Bold = true;
                ws.Cell("A2").Style.Font.FontSize = 12;
                ws.Cell("A2").Value = "Reports Received";
                ws.Cell("B2").Style.Font.Bold = true;
                ws.Cell("B2").Style.Font.FontSize = 12;
                ws.Cell("B2").SetValue(hsiRecCount);

                ws.Cell("C2").Style.Font.Bold = true;
                ws.Cell("C2").Style.Font.FontSize = 12;
                ws.Cell("C2").Value = "Reports Completed";
                ws.Cell("D2").Style.Font.Bold = true;
                ws.Cell("D2").Style.Font.FontSize = 12;
                ws.Cell("D2").SetValue(hsiCompCount);

                ws.Cell("E2").Style.Font.Bold = true;
                ws.Cell("E2").Style.Font.FontSize = 12;
                ws.Cell("E2").Value = "Average Days to Complete";
                ws.Cell("F2").Style.Font.Bold = true;
                ws.Cell("F2").Style.Font.FontSize = 12;
                ws.Cell("F2").SetValue(hsiCompAvg);

                ws.Columns().AdjustToContents(1, 10000);

                ws.Column(6).Style.NumberFormat.NumberFormatId = (int)XLPredefinedFormat.DateTime.DayMonthYear4WithSlashes;
                ws.Column(7).Style.NumberFormat.NumberFormatId = (int)XLPredefinedFormat.DateTime.DayMonthYear4WithSlashes;

                // ******************** VRP Worksheet ******************
                ws = wb.AddWorksheet("VRP");
                table = ws.Cell(3, 1).InsertTable(vrpList);
                table.ShowHeaderRow = true;

                // First Row formatting
                ws.Cell("A1").Style.Font.Bold = true;
                ws.Cell("A1").Style.Font.FontSize = 14;
                ws.Cell("A1").Value = "VRP";

                ws.Cell("C1").Style.Font.Bold = true;
                ws.Cell("C1").Style.Font.FontSize = 14;
                ws.Cell("C1").Value = "Start Date";
                ws.Cell("D1").Style.Font.Bold = true;
                ws.Cell("D1").Style.Font.FontSize = 14;
                ws.Cell("D1").SetValue(startDate?.ToString("MM/dd/yyyy") ?? "");

                ws.Cell("F1").Style.Font.Bold = true;
                ws.Cell("F1").Style.Font.FontSize = 14;
                ws.Cell("F1").Value = "End Date";
                ws.Cell("G1").Style.Font.Bold = true;
                ws.Cell("G1").Style.Font.FontSize = 14;
                ws.Cell("G1").SetValue(endDate?.ToString("MM/dd/yyyy") ?? "");

                //Second Row formatting
                ws.Cell("A2").Style.Font.Bold = true;
                ws.Cell("A2").Style.Font.FontSize = 12;
                ws.Cell("A2").Value = "Reports Received";
                ws.Cell("B2").Style.Font.Bold = true;
                ws.Cell("B2").Style.Font.FontSize = 12;
                ws.Cell("B2").SetValue(vrpRecCount);

                ws.Cell("C2").Style.Font.Bold = true;
                ws.Cell("C2").Style.Font.FontSize = 12;
                ws.Cell("C2").Value = "Reports Completed";
                ws.Cell("D2").Style.Font.Bold = true;
                ws.Cell("D2").Style.Font.FontSize = 12;
                ws.Cell("D2").SetValue(vrpCompCount);

                ws.Cell("E2").Style.Font.Bold = true;
                ws.Cell("E2").Style.Font.FontSize = 12;
                ws.Cell("E2").Value = "Average Days to Complete";
                ws.Cell("F2").Style.Font.Bold = true;
                ws.Cell("F2").Style.Font.FontSize = 12;
                ws.Cell("F2").SetValue(vrpCompAvg);

                ws.Columns().AdjustToContents(1, 10000);

                ws.Column(6).Style.NumberFormat.NumberFormatId = (int)XLPredefinedFormat.DateTime.DayMonthYear4WithSlashes;
                ws.Column(7).Style.NumberFormat.NumberFormatId = (int)XLPredefinedFormat.DateTime.DayMonthYear4WithSlashes;

                // ******************** Brownfield Worksheet ******************
                ws = wb.AddWorksheet("Brownfield");
                table = ws.Cell(3, 1).InsertTable(brnList);
                table.ShowHeaderRow = true;

                // First Row formatting
                ws.Cell("A1").Style.Font.Bold = true;
                ws.Cell("A1").Style.Font.FontSize = 14;
                ws.Cell("A1").Value = "BROWN";

                ws.Cell("C1").Style.Font.Bold = true;
                ws.Cell("C1").Style.Font.FontSize = 14;
                ws.Cell("C1").Value = "Start Date";
                ws.Cell("D1").Style.Font.Bold = true;
                ws.Cell("D1").Style.Font.FontSize = 14;
                ws.Cell("D1").SetValue(startDate?.ToString("MM/dd/yyyy") ?? "");

                ws.Cell("F1").Style.Font.Bold = true;
                ws.Cell("F1").Style.Font.FontSize = 14;
                ws.Cell("F1").Value = "End Date";
                ws.Cell("G1").Style.Font.Bold = true;
                ws.Cell("G1").Style.Font.FontSize = 14;
                ws.Cell("G1").SetValue(endDate?.ToString("MM/dd/yyyy") ?? "");

                //Second Row formatting
                ws.Cell("A2").Style.Font.Bold = true;
                ws.Cell("A2").Style.Font.FontSize = 12;
                ws.Cell("A2").Value = "Reports Received";
                ws.Cell("B2").Style.Font.Bold = true;
                ws.Cell("B2").Style.Font.FontSize = 12;
                ws.Cell("B2").SetValue(brnRecCount);

                ws.Cell("C2").Style.Font.Bold = true;
                ws.Cell("C2").Style.Font.FontSize = 12;
                ws.Cell("C2").Value = "Reports Completed";
                ws.Cell("D2").Style.Font.Bold = true;
                ws.Cell("D2").Style.Font.FontSize = 12;
                ws.Cell("D2").SetValue(brnCompCount);

                ws.Cell("E2").Style.Font.Bold = true;
                ws.Cell("E2").Style.Font.FontSize = 12;
                ws.Cell("E2").Value = "Average Days to Complete";
                ws.Cell("F2").Style.Font.Bold = true;
                ws.Cell("F2").Style.Font.FontSize = 12;
                ws.Cell("F2").SetValue(brnCompAvg);

                ws.Columns().AdjustToContents(1, 10000);
                ws.Column(6).Style.NumberFormat.NumberFormatId = (int)XLPredefinedFormat.DateTime.DayMonthYear4WithSlashes;
                ws.Column(7).Style.NumberFormat.NumberFormatId = (int)XLPredefinedFormat.DateTime.DayMonthYear4WithSlashes;
            }

            if(reportType == ReportType.PAF)
            {
                ws = wb.AddWorksheet("PAF");
                table = ws.Cell(2, 1).InsertTable(list);
                table.ShowHeaderRow = true;

                ws.Columns().AdjustToContents(1, 10000);
                ws.Cell("D1").Style.Font.Bold = true;
                ws.Cell("D1").Style.Font.FontSize = 14;
                ws.Cell("D1").Value = "PAF Report";
                ws.Column("C").Style.NumberFormat.NumberFormatId = (int)XLPredefinedFormat.DateTime.DayMonthYear4WithSlashes;
                ws.Column("D").Style.NumberFormat.NumberFormatId = (int)XLPredefinedFormat.Number.Precision2;
                ws.Column("G").Style.NumberFormat.NumberFormatId = (int)XLPredefinedFormat.DateTime.DayMonthYear4WithSlashes;
                ws.Column("H").Style.NumberFormat.NumberFormatId = (int)XLPredefinedFormat.DateTime.DayMonthYear4WithSlashes;
                ws.Column("I").Style.NumberFormat.NumberFormatId = (int)XLPredefinedFormat.DateTime.DayMonthYear4WithSlashes;
                ws.Column("J").Style.NumberFormat.NumberFormatId = (int)XLPredefinedFormat.DateTime.DayMonthYear4WithSlashes;
                ws.Column("K").Style.NumberFormat.NumberFormatId = (int)XLPredefinedFormat.DateTime.DayMonthYear4WithSlashes;
                ws.Column("L").Style.NumberFormat.NumberFormatId = (int)XLPredefinedFormat.DateTime.DayMonthYear4WithSlashes;
                ws.Column("M").Style.NumberFormat.NumberFormatId = (int)XLPredefinedFormat.DateTime.DayMonthYear4WithSlashes;

                table.ShowAutoFilter = true;
                // Must enable the filter
                table.AutoFilter.IsEnabled = true;
            }

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
            string blankFilePath="./Helpers/BlankRequestForm.pdf")
        {
            PdfDocument mainDoc = new();
            // break the list into chunks of 18 elements
            var smallerLists = list.Chunk(maxCol);
            // need to create a list to keep all object references.
            // declare a variable inside the scope will not work
            List<PdfDocument> pdfDocuments = new();
            foreach (var smallerList in smallerLists)
            {
                PdfDocument currDocument = GeneratePdfPage(smallerList, currentUser, maxCol, blankFilePath);
                pdfDocuments.Add(currDocument);
            }
            // add all of the pages to the main document
            foreach (var pdfDocument in pdfDocuments)
                mainDoc.AppendPage(pdfDocument);
            
            // Creates a new Memory stream
            MemoryStream stream = new();
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
            PdfDocument currDocument = new();
            currDocument.LoadFromFile(blankFilePath);
            Dictionary<string, string> dictionaryTextbox = GenerateRetentionRecordDictionary(smallerList, currentUser);
            // iterate through all user fill-able widget
            PdfFormWidget formWidget = (PdfFormWidget)currDocument.Form;
            foreach (PdfField field in formWidget.FieldsWidget.List)
            {
                //Fill the data for Text Box field
                if (field is PdfTextBoxFieldWidget textBoxField && dictionaryTextbox.ContainsKey(textBoxField.Name))
                    textBoxField.Text = dictionaryTextbox[textBoxField.Name];
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
            Dictionary<string, string> dictionaryTextbox = new();
            // start from 1 to name the key
            int i = 1;
            foreach (var retentionRecord in list)
            {
                dictionaryTextbox.Add("Consignment Number" + i, retentionRecord.ConsignmentNumber ?? "");
                dictionaryTextbox.Add("Item" + i, retentionRecord.BoxNumber ?? "");
                dictionaryTextbox.Add("Location number from transmittal" + i, retentionRecord.ShelfNumber ?? "");
                i++;
            }
            // add key-value pair for the date
            dictionaryTextbox.Add("Date of Request", DateTime.Now.ToString("MM/dd/yyyy"));
            // add key-value pair for the user information
            dictionaryTextbox.Add("Name of Requestor", currentUser.DisplayName ?? "");
            dictionaryTextbox.Add("EMail", currentUser.Email ?? "");
            return dictionaryTextbox;
        }
    }
}