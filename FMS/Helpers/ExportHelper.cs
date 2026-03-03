using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using FMS.Domain.Dto;
using FMS.Domain.Services;
using Spire.Pdf;
using Spire.Pdf.Fields;
using Spire.Pdf.Widget;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
            EventOutstanding,
            EventNoActionTaken,
            PAF,
            HSIListByNumber,
            HSIListByName,
            HSIListByCounty,
            HSIListByClass,
            AbndInacStatusTracker,
            AbndCostEstimateReport
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
            decimal hsiCompAvg = 0.0m,
            int vrpCompCount = 0,
            int vrpRecCount = 0,
            decimal vrpCompAvg = 0.0m,
            int brnCompCount = 0,
            int brnRecCount = 0,
            decimal brnCompAvg = 0.0m)
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

                // First Row formatting
                ws.Cell("A1").Style.Font.Bold = true;
                ws.Cell("A1").Style.Font.FontSize = 14;
                ws.Cell("A1").Value = "Delisted By Date Range";

                ws.Cell("A2").Style.Font.Bold = true;
                ws.Cell("A2").Style.Font.FontSize = 14;
                ws.Cell("A2").SetValue("Start Date: " + startDate?.ToString("MM/dd/yyyy") ?? "");

                ws.Cell("C2").Style.Font.Bold = true;
                ws.Cell("C2").Style.Font.FontSize = 14;
                ws.Cell("C2").SetValue("End Date: " + endDate?.ToString("MM/dd/yyyy") ?? "");

                ws.Column("E").Style.NumberFormat.NumberFormatId = (int)XLPredefinedFormat.DateTime.DayMonthYear4WithSlashes;
                ws.Column("F").Style.NumberFormat.NumberFormatId = (int)XLPredefinedFormat.DateTime.DayMonthYear4WithSlashes;

                ws.Columns().AdjustToContents(1, 10000);
                table.ShowTotalsRow = true;
                table.Field("Acres").TotalsRowFunction = XLTotalsRowFunction.Sum;
            }

            if (reportType == ReportType.EventPending) 
            {
                ws = wb.AddWorksheet("Events Pending");

                table = ws.Cell(2, 1).InsertTable(list);
                table.ShowHeaderRow = true;
                ws.Columns().AdjustToContents(1, 10000);
                ws.Cell("A1").Style.Font.Bold = true;
                ws.Cell("A1").Style.Font.FontSize = 14;
                ws.Cell("A1").Value = "Events Pending";

                ws.Column(8).Style.NumberFormat.NumberFormatId = (int)XLPredefinedFormat.DateTime.DayMonthYear4WithSlashes;
                ws.Column(9).Style.NumberFormat.NumberFormatId = (int)XLPredefinedFormat.DateTime.DayMonthYear4WithSlashes;
            }

            if (reportType == ReportType.EventCompleted)
            {
                ws = wb.AddWorksheet("Events Completed");
                table = ws.Cell(3, 1).InsertTable(list);
                table.ShowHeaderRow = true;

                // First Row formatting
                ws.Cell("A1").Style.Font.Bold = true;
                ws.Cell("A1").Style.Font.FontSize = 14;
                ws.Cell("A1").Value = "Completed Events";

                // Second Row Formatting
                ws.Cell("A2").Style.Font.Bold = true;
                ws.Cell("A2").Style.Font.FontSize = 14;
                ws.Cell("A2").SetValue("Start Date: " + startDate?.ToString("MM/dd/yyyy") ?? "");
                ws.Cell("C2").Style.Font.Bold = true;
                ws.Cell("C2").Style.Font.FontSize = 14;
                ws.Cell("C2").SetValue("End Date: " + endDate?.ToString("MM/dd/yyyy") ?? "");

                ws.Column(8).Style.NumberFormat.NumberFormatId = (int)XLPredefinedFormat.DateTime.DayMonthYear4WithSlashes;
                ws.Column(9).Style.NumberFormat.NumberFormatId = (int)XLPredefinedFormat.DateTime.DayMonthYear4WithSlashes;

                ws.Columns().AdjustToContents(1, 10000);
            }

            if (reportType == ReportType.EventCompliance) 
            {
                ws = wb.AddWorksheet("Event Compliance");
                table = ws.Cell(3, 1).InsertTable(list);
                table.ShowHeaderRow = true;

                // First Row formatting
                ws.Cell("A1").Style.Font.Bold = true;
                ws.Cell("A1").Style.Font.FontSize = 14;
                ws.Cell("A1").Value = "Compliance Orders";

                // Second Row Formatting
                ws.Cell("A2").Style.Font.Bold = true;
                ws.Cell("A2").Style.Font.FontSize = 14;
                ws.Cell("A2").SetValue("Start Date: " + startDate?.ToString("MM/dd/yyyy") ?? "");
                ws.Cell("C2").Style.Font.Bold = true;
                ws.Cell("C2").Style.Font.FontSize = 14;
                ws.Cell("C2").SetValue("End Date: " + endDate?.ToString("MM/dd/yyyy") ?? "");

                ws.Column(5).Style.NumberFormat.NumberFormatId = (int)XLPredefinedFormat.DateTime.DayMonthYear4WithSlashes;
                ws.Column(6).Style.NumberFormat.NumberFormatId = (int)XLPredefinedFormat.DateTime.DayMonthYear4WithSlashes;
                ws.Column(7).Style.NumberFormat.NumberFormatId = (int)XLPredefinedFormat.Number.Precision2;

                ws.Columns().AdjustToContents(1, 10000);
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

                ws.Cell("B1").Style.Font.Bold = true;
                ws.Cell("B1").Style.Font.FontSize = 14;
                ws.Cell("B1").SetValue("Start Date: " + startDate?.ToString("MM/dd/yyyy") ?? "");

                ws.Cell("C1").Style.Font.Bold = true;
                ws.Cell("C1").Style.Font.FontSize = 14;
                ws.Cell("C1").SetValue("End Date: " + endDate?.ToString("MM/dd/yyyy") ?? "");

                //Second Row formatting
                ws.Cell("A2").Style.Font.Bold = true;
                ws.Cell("A2").Style.Font.FontSize = 12;
                ws.Cell("A2").SetValue("Reports Received: " + hsiRecCount);

                ws.Cell("B2").Style.Font.Bold = true;
                ws.Cell("B2").Style.Font.FontSize = 12;
                ws.Cell("B2").SetValue("Reports Completed: " + hsiCompCount);

                ws.Cell("C2").Style.Font.Bold = true;
                ws.Cell("C2").Style.Font.FontSize = 12;
                ws.Cell("C2").SetValue("Average Days to Complete: " + hsiCompAvg);

                ws.Columns().AdjustToContents(1, 10000);

                // ******************** VRP Worksheet ******************
                ws = wb.AddWorksheet("VRP");
                table = ws.Cell(3, 1).InsertTable(vrpList);
                table.ShowHeaderRow = true;

                // First Row formatting
                ws.Cell("A1").Style.Font.Bold = true;
                ws.Cell("A1").Style.Font.FontSize = 14;
                ws.Cell("A1").Value = "VRP";

                ws.Cell("B1").Style.Font.Bold = true;
                ws.Cell("B1").Style.Font.FontSize = 14;
                ws.Cell("B1").SetValue("Start Date: " + startDate?.ToString("MM/dd/yyyy") ?? "");

                ws.Cell("C1").Style.Font.Bold = true;
                ws.Cell("C1").Style.Font.FontSize = 14;
                ws.Cell("C1").SetValue("End Date: " + endDate?.ToString("MM/dd/yyyy") ?? "");

                //Second Row formatting
                ws.Cell("A2").Style.Font.Bold = true;
                ws.Cell("A2").Style.Font.FontSize = 12;
                ws.Cell("A2").SetValue("Reports Received: " + vrpRecCount);

                ws.Cell("B2").Style.Font.Bold = true;
                ws.Cell("B2").Style.Font.FontSize = 12;
                ws.Cell("B2").SetValue("Reports Completed: " + vrpCompCount);

                ws.Cell("C2").Style.Font.Bold = true;
                ws.Cell("C2").Style.Font.FontSize = 12;
                ws.Cell("C2").SetValue("Average Days to Complete: " + vrpCompAvg);

                ws.Columns().AdjustToContents(1, 10000);

                // ******************** Brownfield Worksheet ******************
                ws = wb.AddWorksheet("Brownfield");
                table = ws.Cell(3, 1).InsertTable(brnList);
                table.ShowHeaderRow = true;

                // First Row formatting
                ws.Cell("A1").Style.Font.Bold = true;
                ws.Cell("A1").Style.Font.FontSize = 14;
                ws.Cell("A1").Value = "BROWN";

                ws.Cell("B1").Style.Font.Bold = true;
                ws.Cell("B1").Style.Font.FontSize = 14;
                ws.Cell("B1").SetValue("Start Date: " + startDate?.ToString("MM/dd/yyyy") ?? "");

                ws.Cell("C1").Style.Font.Bold = true;
                ws.Cell("C1").Style.Font.FontSize = 14;
                ws.Cell("C1").SetValue("End Date: " + endDate?.ToString("MM/dd/yyyy") ?? "");

                //Second Row formatting
                ws.Cell("A2").Style.Font.Bold = true;
                ws.Cell("A2").Style.Font.FontSize = 12;
                ws.Cell("A2").SetValue("Reports Received: " + brnRecCount);

                ws.Cell("B2").Style.Font.Bold = true;
                ws.Cell("B2").Style.Font.FontSize = 12;
                ws.Cell("B2").SetValue("Reports Completed: " + brnCompCount);

                ws.Cell("C2").Style.Font.Bold = true;
                ws.Cell("C2").Style.Font.FontSize = 12;
                ws.Cell("C2").SetValue("Average Days to Complete: " + brnCompAvg);

                ws.Columns().AdjustToContents(1, 10000);
            }

            if (reportType == ReportType.EventOutstanding)
            {
                // ******************** HSI Worksheet ******************
                ws = wb.AddWorksheet("HSI");
                table = ws.Cell(3, 1).InsertTable(list);
                table.ShowHeaderRow = true;

                // First Row formatting
                ws.Cell("A1").Style.Font.Bold = true;
                ws.Cell("A1").Style.Font.FontSize = 14;
                ws.Cell("A1").Value = "HSI";

                //Second Row formatting
                ws.Cell("A2").Style.Font.Bold = true;
                ws.Cell("A2").Style.Font.FontSize = 12;
                ws.Cell("A2").SetValue("Reports Received: " + hsiRecCount);

                ws.Columns().AdjustToContents(1, 10000);

                // ******************** VRP Worksheet ******************
                ws = wb.AddWorksheet("VRP");
                table = ws.Cell(3, 1).InsertTable(vrpList);
                table.ShowHeaderRow = true;

                // First Row formatting
                ws.Cell("A1").Style.Font.Bold = true;
                ws.Cell("A1").Style.Font.FontSize = 14;
                ws.Cell("A1").Value = "VRP";

                //Second Row formatting
                ws.Cell("A2").Style.Font.Bold = true;
                ws.Cell("A2").Style.Font.FontSize = 12;
                ws.Cell("A2").SetValue("Reports Received: " + vrpRecCount);

                ws.Columns().AdjustToContents(1, 10000);

                // ******************** Brownfield Worksheet ******************
                ws = wb.AddWorksheet("Brownfield");
                table = ws.Cell(3, 1).InsertTable(brnList);
                table.ShowHeaderRow = true;

                // First Row formatting
                ws.Cell("A1").Style.Font.Bold = true;
                ws.Cell("A1").Style.Font.FontSize = 14;
                ws.Cell("A1").Value = "BROWN";

                //Second Row formatting
                ws.Cell("A2").Style.Font.Bold = true;
                ws.Cell("A2").Style.Font.FontSize = 12;
                ws.Cell("A2").SetValue("Reports Received: " + brnRecCount);

                ws.Columns().AdjustToContents(1, 10000);
            }

            if (reportType == ReportType.PAF)
            {
                ws = wb.AddWorksheet("PAF");
                table = ws.Cell(2, 1).InsertTable(list);
                table.ShowHeaderRow = true;

                ws.Cell("D1").Style.Font.Bold = true;
                ws.Cell("D1").Style.Font.FontSize = 14;
                ws.Cell("D1").Value = "PAF Report";
                ws.Column("C").Style.DateFormat.Format = "MM/dd/yyyy";
                ws.Column("D").Style.NumberFormat.NumberFormatId = (int)XLPredefinedFormat.Number.Precision2;
                ws.Column("G").Style.DateFormat.Format = "MM/dd/yyyy";
                ws.Column("H").Style.DateFormat.Format = "MM/dd/yyyy";
                ws.Column("I").Style.DateFormat.Format = "MM/dd/yyyy";
                ws.Column("J").Style.DateFormat.Format = "MM/dd/yyyy";
                ws.Column("K").Style.DateFormat.Format = "MM/dd/yyyy";
                ws.Column("L").Style.DateFormat.Format = "MM/dd/yyyy";
                ws.Column("M").Style.DateFormat.Format = "MM/dd/yyyy";
                table.ShowAutoFilter = true;
                // Must enable the filter
                table.AutoFilter.IsEnabled = true;
                table.ShowTotalsRow = true;

                // *************** Report Totals ***************
                table.Field("PAF Issue Date").TotalsCell.Value = "Total PAF Amt -->";
                // PAF Amount Sum
                table.Field("PAF Amount").TotalsRowFunction = XLTotalsRowFunction.Sum;

                //// Number of Unique Compliance Officer Rows
                table.Field("Project Officer").TotalsRowFunction = XLTotalsRowFunction.Count;

                //// Number of Unique Contractor Rows
                table.Field("Contractor").TotalsRowFunction = XLTotalsRowFunction.Count;

                ws.Columns().AdjustToContents(1, 10000);
            }

            if(reportType == ReportType.HSIListByNumber)
            {
                ws = wb.AddWorksheet("HSI List");
                table = ws.Cell(2, 1).InsertTable(list);
                table.ShowHeaderRow = true;

                ws.Columns().AdjustToContents(1, 10000);
                ws.Cell("B1").Style.Font.Bold = true;
                ws.Cell("B1").Style.Font.FontSize = 14;
                ws.Cell("B1").Value = "HSI List By Facility Number";
            }

            if (reportType == ReportType.HSIListByName)
            {
                ws = wb.AddWorksheet("HSI List");
                table = ws.Cell(2, 1).InsertTable(list);
                table.ShowHeaderRow = true;

                ws.Columns().AdjustToContents(1, 10000);
                ws.Cell("B1").Style.Font.Bold = true;
                ws.Cell("B1").Style.Font.FontSize = 14;
                ws.Cell("B1").Value = "HSI List By Facility Name";
            }

            if (reportType == ReportType.HSIListByCounty)
            {
                ws = wb.AddWorksheet("HSI List");
                table = ws.Cell(2, 1).InsertTable(list);
                table.ShowHeaderRow = true;

                ws.Columns().AdjustToContents(1, 10000);
                ws.Cell("B1").Style.Font.Bold = true;
                ws.Cell("B1").Style.Font.FontSize = 14;
                ws.Cell("B1").Value = "HSI List By County";
            }

            if (reportType == ReportType.HSIListByClass)
            {
                ws = wb.AddWorksheet("HSI List");
                table = ws.Cell(2, 1).InsertTable(list);
                table.ShowHeaderRow = true;

                ws.Columns().AdjustToContents(1, 10000);
                ws.Cell("B1").Style.Font.Bold = true;
                ws.Cell("B1").Style.Font.FontSize = 14;
                ws.Cell("B1").Value = "HSI List By Class";
            }

            if (reportType == ReportType.AbndInacStatusTracker)
            {
                ws = wb.AddWorksheet("Abnd Inac Status Tracker");
                table = ws.Cell(2, 1).InsertTable(list);
                table.ShowHeaderRow = true;

                ws.Columns().AdjustToContents(1, 10000);
                ws.Cell("B1").Style.Font.Bold = true;
                ws.Cell("B1").Style.Font.FontSize = 14;
                ws.Cell("B1").Value = "Abandoned Inactive Status Tracker";
            }

            if (reportType == ReportType.AbndCostEstimateReport)
            {
                ws = wb.AddWorksheet("Abnd Inac Cost Estimate");
                table = ws.Cell(2, 1).InsertTable(list);
                table.ShowHeaderRow = true;

                ws.Columns().AdjustToContents(1, 10000);
                ws.Cell("B1").Style.Font.Bold = true;
                ws.Cell("B1").Style.Font.FontSize = 14;
                ws.Cell("B1").Value = "Abandoned Sites Cost Estimate Report";
                ws.Column("A").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Column("F").Style.NumberFormat.NumberFormatId = (int)XLPredefinedFormat.Number.Integer;
                ws.Column("F").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Column("G").Style.NumberFormat.NumberFormatId = (int)XLPredefinedFormat.DateTime.DayMonthYear4WithSlashes;
                ws.Column("G").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Column("H").Style.NumberFormat.NumberFormatId = (int)XLPredefinedFormat.Number.Integer;
                ws.Column("H").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Column("J").Style.NumberFormat.NumberFormatId = (int)XLPredefinedFormat.Number.Precision2;
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