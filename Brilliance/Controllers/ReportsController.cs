using Brilliance.Infrastructure.DataProvider;
using Brilliance.Models.ViewModel;
using Brilliance.Infrastructure;
using Brilliance.Models;
using System;
using System.Web.Mvc;
using System.Data;
using System.Web;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.SS.Util;
using ICSharpCode.SharpZipLib.Zip;

namespace Brilliance.Controllers
{
    public class ReportsController : BaseController
    {
        private IReportDataProvider _ireportDataProvider;
        // GET: Reports
        #region Level3Reports
        public void GetLevel3ComplaintExcelReportwithNotes(string Type, string From, string To)
        {
            string clientid = "";
            DataSet ds = new DataSet();
            //MonthYearStr = "01 " + MonthYearStr;
            _ireportDataProvider = new ReportDataProvider();

            //int monthInDigit = Convert.ToDateTime(MonthYearStr).Month;
            //int year = Convert.ToDateTime(MonthYearStr).Year;
            DateTime datefrom = Convert.ToDateTime(From);
            DateTime dateto = Convert.ToDateTime(To);
            string dfrom = (datefrom).ToString("dd MMMM yyyy");
            string dto = (dateto).ToString("dd MMMM yyyy");
            Guid ClientID = Common.CheckIdNullOrEmptyNonEncrypt(clientid);
            IWorkbook workbook;
            workbook = new HSSFWorkbook();
            //string CompanyID = "CA484A56-94D4-4AD5-9C5A-0ACA865A059E";
            ds = _ireportDataProvider.GetComplaintExcelReport(Type);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {

                for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
                {
                    string Tier3 = Convert.ToString(ds.Tables[0].Rows[k]["Tier3"]);
                    //string AccNo = Convert.ToString(ds.Tables[0].Rows[k]["Email"]);
                    //Guid CompanyID = new Guid(Convert.ToString(ds.Tables[0].Rows[k]["CompanyID"]));
                    //DataTable dt = ds.Tables[0].Select("CompanyId='CA484A56-94D4-4AD5-9C5A-0ACA865A059E'").CopyToDataTable();
                    DataTable dt = ds.Tables[0];


                    //workbook = new XSSFWorkbook();

                    ISheet sheet1 = workbook.CreateSheet("Sheet " + k);

                    IRow row1 = sheet1.CreateRow(0);
                    ICell cellr1 = row1.CreateCell(0);
                    cellr1.SetCellValue("Complaint Notes Report");
                    ICellStyle style = workbook.CreateCellStyle();
                    style.Alignment = HorizontalAlignment.Left;
                    row1.GetCell(0).CellStyle = style;

                    var font = workbook.CreateFont();
                    font.FontHeightInPoints = 11;
                    font.FontName = "Calibri";
                    font.Boldweight = (short)FontBoldWeight.Bold;
                    //font.Color = IndexedColors.Red.Index;
                    cellr1.CellStyle.SetFont(font);
                    sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, 6));

                    IRow row2 = sheet1.CreateRow(1);
                    ICell cellr2 = row2.CreateCell(0);
                    cellr2.SetCellValue(Tier3);
                    //ICellStyle styler2 = workbook.CreateCellStyle();
                    //styler2.Alignment = HorizontalAlignment.Left;
                    row1.GetCell(0).CellStyle = style;

                    var fontr2 = workbook.CreateFont();
                    fontr2.FontHeightInPoints = 9;
                    fontr2.FontName = "Calibri";
                    fontr2.Boldweight = (short)FontBoldWeight.Bold;
                    //fontr2.Color = IndexedColors.Blue.Index;
                    cellr2.CellStyle.SetFont(fontr2);
                    sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 0, 0));


                    IRow row3 = sheet1.CreateRow(2);
                    ICell cellr3 = row3.CreateCell(0);
                    // DateTime date = DateTime.Now;

                    //cellr2.SetCellValue("Date : " + date.ToString("dd/MM/yyyy"));
                    cellr3.SetCellValue(dfrom + " - " + dto);
                    //ICellStyle styler2 = workbook.CreateCellStyle();
                    //styler2.Alignment = HorizontalAlignment.Left;
                    row3.GetCell(0).CellStyle = style;

                    var fontr3 = workbook.CreateFont();
                    fontr3.FontHeightInPoints = 9;
                    fontr3.FontName = "Calibri";
                    fontr3.Boldweight = (short)FontBoldWeight.Bold;
                    //fontr3.Color = IndexedColors.Grey80Percent.Index;
                    cellr3.CellStyle.SetFont(fontr3);
                    sheet1.AddMergedRegion(new CellRangeAddress(2, 2, 0, 6));

                    //IRow row4 = sheet1.CreateRow(4);
                    //row4.CreateCell(0).SetCellValue("We request you to credit the accounts of the persons with the amount mentioned against each, as per the statement given below and debit the same to our account no. " + AccNo + " with you.");
                    //ICellStyle styler4 = workbook.CreateCellStyle();
                    //styler4.Alignment = HorizontalAlignment.Center;
                    //styler4.VerticalAlignment = VerticalAlignment.Center;
                    //styler4.WrapText = true;
                    //row4.GetCell(0).CellStyle = styler4;
                    //row4.HeightInPoints = 40;
                    //sheet1.AddMergedRegion(new CellRangeAddress(4, 4, 0, 6));

                    IRow row4 = sheet1.CreateRow(4);

                    for (int j = 0; j < dt.Columns.Count - 1; j++)
                    {
                        ICell cell = row4.CreateCell(j);

                        String columnName = dt.Columns[j].ToString();
                        cell.SetCellValue(columnName);
                    }

                    //decimal TotalSalary = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        IRow row = sheet1.CreateRow(i + 5);
                        for (int j = 0; j < dt.Columns.Count - 1; j++)
                        {
                            ICell cell = row.CreateCell(j);
                            String columnName = dt.Columns[j].ToString();
                            cell.SetCellValue(dt.Rows[i][columnName].ToString());

                            //if (j == 4 && !string.IsNullOrEmpty(Convert.ToString(dt.Rows[i][columnName])))
                            //{
                            //    TotalSalary = TotalSalary + Convert.ToDecimal(dt.Rows[i][columnName].ToString());
                            //}
                        }
                    }

                    //ICellStyle styler3 = workbook.CreateCellStyle();
                    //styler3.Alignment = HorizontalAlignment.Right;

                    //IRow rowe1 = sheet1.CreateRow(dt.Rows.Count + 7 + 5);
                    //ICell celle1 = rowe1.CreateCell(5);
                    //ICell celle6 = rowe1.CreateCell(6);
                    //celle1.SetCellValue("Total Amount of Debit");
                    //celle6.SetCellValue("NIL");
                    //rowe1.GetCell(6).CellStyle = styler3;

                    //IRow rowe2 = sheet1.CreateRow(dt.Rows.Count + 7 + 6);
                    //ICell celle2 = rowe2.CreateCell(5);
                    //ICell celle7 = rowe2.CreateCell(6);
                    //celle2.SetCellValue("Total Number of Credit Entries");
                    //celle7.SetCellValue(dt.Rows.Count);
                    //rowe2.GetCell(6).CellStyle = styler3;

                    //IRow rowe3 = sheet1.CreateRow(dt.Rows.Count + 7 + 7);
                    //ICell celle3 = rowe3.CreateCell(5);
                    //ICell celle8 = rowe3.CreateCell(6);
                    //celle3.SetCellValue("Total Amount of Credit");
                    //celle8.SetCellValue(Convert.ToString(TotalSalary));
                    //rowe3.GetCell(6).CellStyle = styler3;

                    //IRow rowe5 = sheet1.CreateRow(dt.Rows.Count + 7 + 9);
                    //ICell celle5 = rowe5.CreateCell(5);
                    //celle5.SetCellValue("Authorised Representative(s)");

                    sheet1.AutoSizeColumn(0);
                    sheet1.AutoSizeColumn(1);
                    sheet1.AutoSizeColumn(2);
                    sheet1.AutoSizeColumn(3);
                    sheet1.AutoSizeColumn(4);
                    sheet1.AutoSizeColumn(5);
                    sheet1.AutoSizeColumn(6);
                    sheet1.AutoSizeColumn(7);
                    sheet1.AutoSizeColumn(8);
                    sheet1.AutoSizeColumn(9);
                    sheet1.AutoSizeColumn(10);
                    sheet1.AutoSizeColumn(11);
                    sheet1.AutoSizeColumn(12);
                    sheet1.AutoSizeColumn(13);
                    sheet1.AutoSizeColumn(14);
                    sheet1.AutoSizeColumn(15);
                    sheet1.AutoSizeColumn(16);
                    sheet1.AutoSizeColumn(17);
                    sheet1.AutoSizeColumn(18);
                    sheet1.AutoSizeColumn(19);
                    sheet1.AutoSizeColumn(20);
                }
            }


            using (var exportData = new MemoryStream())
            {
                Response.Clear();
                workbook.Write(exportData);

                ///-----Direct Excel Export Start----///
                ///
                string saveAsFileName = string.Format("Complaints_Notes_Report.xls", DateTime.Now);
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", saveAsFileName));
                Response.Clear();
                Response.BinaryWrite(exportData.GetBuffer());
                Response.End();

                ////----Direct Excel Export END----///

                ///For save excel File start

                //if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath("~/Excel/")))
                //{
                //    Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath("~/Excel/"));
                //}

                //if (System.IO.File.Exists(Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Excel/" + CompanyID + ".xls"))))

                //{
                //    System.IO.File.Delete(Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Excel/" + CompanyID + ".xls")));
                //}

                //string FilePath = System.Web.HttpContext.Current.Server.MapPath("~/Excel/" + CompanyID + ".xls");

                // Write to file using file stream

                // FileStream file = new FileStream(FilePath, FileMode.CreateNew, FileAccess.Write);
                // exportData.WriteTo(file);
                // file.Close();
                // exportData.Close();
                // Response.Clear();
                // Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                // Response.AddHeader("content-disposition", "attachment: filename=" + "Report.xls");
                ////Response.BinaryWrite(Ep.GetAsByteArray());
                // Response.End();

                ///For save excel File End
            }

            ///Zip
            //using (ZipFile zip = new ZipFile())
            //{
            //    zip.AlternateEncodingUsage = ZipOption.AsNecessary;
            //    zip.AddDirectory(Server.MapPath("~/Areas/Account/Excel/"));
            //    Response.Clear();
            //    Response.BufferOutput = false;
            //    string zipName = "SalarySheetSentTobank_" + monthInDigit + "_" + year + ".zip";
            //    Response.ContentType = "application/zip";
            //    Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
            //    zip.Save(Response.OutputStream);
            //    Response.End();
            //}

            //string[] files = Directory.GetFiles(System.Web.HttpContext.Current.Server.MapPath("~/Excel/"));
            //foreach (string filePath in files)
            //    System.IO.File.Delete(filePath);
        }
        public void GetLevel3ComplaintExcelReportwithoutNotes(string Type, string From, string To)
        {
            string clientid = "";
            DataSet ds = new DataSet();
            _ireportDataProvider = new ReportDataProvider();
            DateTime datefrom = Convert.ToDateTime(From);
            DateTime dateto = Convert.ToDateTime(To);
            string dfrom = (datefrom).ToString("dd MMMM yyyy");
            string dto = (dateto).ToString("dd MMMM yyyy");
            Guid ClientID = Common.CheckIdNullOrEmptyNonEncrypt(clientid);
            IWorkbook workbook;
            workbook = new HSSFWorkbook();
            ds = _ireportDataProvider.GetComplaintExcelReport(Type);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
                {
                    string Tier3 = Convert.ToString(ds.Tables[0].Rows[k]["Tier3"]);
                    DataTable dt = ds.Tables[0];

                    ISheet sheet1 = workbook.CreateSheet("Sheet " + k);

                    IRow row1 = sheet1.CreateRow(0);
                    ICell cellr1 = row1.CreateCell(0);
                    cellr1.SetCellValue("Complaint Data Without Notes");
                    ICellStyle style = workbook.CreateCellStyle();
                    style.Alignment = HorizontalAlignment.Left;
                    row1.GetCell(0).CellStyle = style;

                    var font = workbook.CreateFont();
                    font.FontHeightInPoints = 11;
                    font.FontName = "Calibri";
                    font.Boldweight = (short)FontBoldWeight.Bold;
                    cellr1.CellStyle.SetFont(font);
                    sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, 6));

                    IRow row2 = sheet1.CreateRow(1);
                    ICell cellr2 = row2.CreateCell(0);
                    cellr2.SetCellValue(Tier3);
                    row1.GetCell(0).CellStyle = style;

                    var fontr2 = workbook.CreateFont();
                    fontr2.FontHeightInPoints = 9;
                    fontr2.FontName = "Calibri";
                    fontr2.Boldweight = (short)FontBoldWeight.Bold;
                    //fontr2.Color = IndexedColors.Blue.Index;
                    cellr2.CellStyle.SetFont(fontr2);
                    sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 0, 0));


                    IRow row3 = sheet1.CreateRow(2);
                    ICell cellr3 = row3.CreateCell(0);
                    cellr3.SetCellValue(dfrom + " - " + dto);
                    row3.GetCell(0).CellStyle = style;

                    var fontr3 = workbook.CreateFont();
                    fontr3.FontHeightInPoints = 9;
                    fontr3.FontName = "Calibri";
                    fontr3.Boldweight = (short)FontBoldWeight.Bold;
                    cellr3.CellStyle.SetFont(fontr3);
                    sheet1.AddMergedRegion(new CellRangeAddress(2, 2, 0, 6));

                    IRow row4 = sheet1.CreateRow(4);

                    for (int j = 0; j < dt.Columns.Count - 1; j++)
                    {
                        ICell cell = row4.CreateCell(j);

                        String columnName = dt.Columns[j].ToString();
                        cell.SetCellValue(columnName);
                    }

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        IRow row = sheet1.CreateRow(i + 5);
                        for (int j = 0; j < dt.Columns.Count - 1; j++)
                        {
                            ICell cell = row.CreateCell(j);
                            String columnName = dt.Columns[j].ToString();
                            cell.SetCellValue(dt.Rows[i][columnName].ToString());

                        }
                    }

                    sheet1.AutoSizeColumn(0);
                    sheet1.AutoSizeColumn(1);
                    sheet1.AutoSizeColumn(2);
                    sheet1.AutoSizeColumn(3);
                    sheet1.AutoSizeColumn(4);
                    sheet1.AutoSizeColumn(5);
                    sheet1.AutoSizeColumn(6);
                    sheet1.AutoSizeColumn(7);
                    sheet1.AutoSizeColumn(8);
                    sheet1.AutoSizeColumn(9);
                    sheet1.AutoSizeColumn(10);
                    sheet1.AutoSizeColumn(11);
                    sheet1.AutoSizeColumn(12);
                    sheet1.AutoSizeColumn(13);
                    sheet1.AutoSizeColumn(14);
                    sheet1.AutoSizeColumn(15);
                    sheet1.AutoSizeColumn(16);
                    sheet1.AutoSizeColumn(17);
                    sheet1.AutoSizeColumn(18);
                    sheet1.AutoSizeColumn(19);
                    sheet1.AutoSizeColumn(20);
                }
            }


            using (var exportData = new MemoryStream())
            {
                Response.Clear();
                workbook.Write(exportData);

                ///-----Direct Excel Export Start----///
                ///
                string saveAsFileName = string.Format("Complaintsdata_WithoutNotes_Report.xls", DateTime.Now);
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", saveAsFileName));
                Response.Clear();
                Response.BinaryWrite(exportData.GetBuffer());
                Response.End();

                ////----Direct Excel Export END----///  
            }
        }
        public void GetLevel3ComplaintTypesExcelReport(string Type, string From, string To)
        {
            string clientid = "";
            DataSet ds = new DataSet();
            _ireportDataProvider = new ReportDataProvider();
            DateTime datefrom = Convert.ToDateTime(From);
            DateTime dateto = Convert.ToDateTime(To);
            string dfrom = (datefrom).ToString("dd MMMM yyyy");
            string dto = (dateto).ToString("dd MMMM yyyy");
            Guid ClientID = Common.CheckIdNullOrEmptyNonEncrypt(clientid);
            IWorkbook workbook;
            workbook = new HSSFWorkbook();
            ds = _ireportDataProvider.GetComplaintExcelReport(Type);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
                {
                    string Tier3 = Convert.ToString(ds.Tables[0].Rows[k]["Tier3"]);
                    DataTable dt = ds.Tables[0];

                    ISheet sheet1 = workbook.CreateSheet("Sheet " + k);

                    IRow row1 = sheet1.CreateRow(0);
                    ICell cellr1 = row1.CreateCell(0);
                    cellr1.SetCellValue("Complaint Types Report");
                    ICellStyle style = workbook.CreateCellStyle();
                    style.Alignment = HorizontalAlignment.Left;
                    row1.GetCell(0).CellStyle = style;

                    var font = workbook.CreateFont();
                    font.FontHeightInPoints = 11;
                    font.FontName = "Calibri";
                    font.Boldweight = (short)FontBoldWeight.Bold;
                    cellr1.CellStyle.SetFont(font);
                    sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, 6));

                    IRow row2 = sheet1.CreateRow(1);
                    ICell cellr2 = row2.CreateCell(0);
                    cellr2.SetCellValue(Tier3);
                    row1.GetCell(0).CellStyle = style;

                    var fontr2 = workbook.CreateFont();
                    fontr2.FontHeightInPoints = 9;
                    fontr2.FontName = "Calibri";
                    fontr2.Boldweight = (short)FontBoldWeight.Bold;
                    //fontr2.Color = IndexedColors.Blue.Index;
                    cellr2.CellStyle.SetFont(fontr2);
                    sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 0, 0));


                    IRow row3 = sheet1.CreateRow(2);
                    ICell cellr3 = row3.CreateCell(0);
                    cellr3.SetCellValue(dfrom + " - " + dto);
                    row3.GetCell(0).CellStyle = style;

                    var fontr3 = workbook.CreateFont();
                    fontr3.FontHeightInPoints = 9;
                    fontr3.FontName = "Calibri";
                    fontr3.Boldweight = (short)FontBoldWeight.Bold;
                    cellr3.CellStyle.SetFont(fontr3);
                    sheet1.AddMergedRegion(new CellRangeAddress(2, 2, 0, 6));

                    IRow row4 = sheet1.CreateRow(4);

                    for (int j = 0; j < dt.Columns.Count - 1; j++)
                    {
                        ICell cell = row4.CreateCell(j);

                        String columnName = dt.Columns[j].ToString();
                        cell.SetCellValue(columnName);
                    }

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        IRow row = sheet1.CreateRow(i + 5);
                        for (int j = 0; j < dt.Columns.Count - 1; j++)
                        {
                            ICell cell = row.CreateCell(j);
                            String columnName = dt.Columns[j].ToString();
                            cell.SetCellValue(dt.Rows[i][columnName].ToString());

                        }
                    }

                    sheet1.AutoSizeColumn(0);
                    sheet1.AutoSizeColumn(1);
                    sheet1.AutoSizeColumn(2);
                    sheet1.AutoSizeColumn(3);
                    sheet1.AutoSizeColumn(4);
                    sheet1.AutoSizeColumn(5);
                    sheet1.AutoSizeColumn(6);
                    sheet1.AutoSizeColumn(7);
                    sheet1.AutoSizeColumn(8);
                    sheet1.AutoSizeColumn(9);
                    sheet1.AutoSizeColumn(10);
                    sheet1.AutoSizeColumn(11);
                    sheet1.AutoSizeColumn(12);
                    sheet1.AutoSizeColumn(13);
                    sheet1.AutoSizeColumn(14);
                    sheet1.AutoSizeColumn(15);
                    sheet1.AutoSizeColumn(16);
                    sheet1.AutoSizeColumn(17);
                    sheet1.AutoSizeColumn(18);
                    sheet1.AutoSizeColumn(19);
                    sheet1.AutoSizeColumn(20);
                }
            }


            using (var exportData = new MemoryStream())
            {
                Response.Clear();
                workbook.Write(exportData);

                ///-----Direct Excel Export Start----///
                ///
                string saveAsFileName = string.Format("Complaints_Types_Report.xls", DateTime.Now);
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", saveAsFileName));
                Response.Clear();
                Response.BinaryWrite(exportData.GetBuffer());
                Response.End();

                ////----Direct Excel Export END----///  
            }
        }
        public void GetLevel3ComplaintTurnaroundTimeExcelReport(string Type, string From, string To)
        {
            string clientid = "";
            DataSet ds = new DataSet();
            _ireportDataProvider = new ReportDataProvider();
            DateTime datefrom = Convert.ToDateTime(From);
            DateTime dateto = Convert.ToDateTime(To);
            string dfrom = (datefrom).ToString("dd MMMM yyyy");
            string dto = (dateto).ToString("dd MMMM yyyy");
            Guid ClientID = Common.CheckIdNullOrEmptyNonEncrypt(clientid);
            IWorkbook workbook;
            workbook = new HSSFWorkbook();
            ds = _ireportDataProvider.GetComplaintExcelReport(Type);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
                {
                    string Tier3 = Convert.ToString(ds.Tables[0].Rows[k]["Tier3"]);
                    DataTable dt = ds.Tables[0];

                    ISheet sheet1 = workbook.CreateSheet("Sheet " + k);

                    IRow row1 = sheet1.CreateRow(0);
                    ICell cellr1 = row1.CreateCell(0);
                    cellr1.SetCellValue("Complaints Turnsround Time Report");
                    ICellStyle style = workbook.CreateCellStyle();
                    style.Alignment = HorizontalAlignment.Left;
                    row1.GetCell(0).CellStyle = style;

                    var font = workbook.CreateFont();
                    font.FontHeightInPoints = 11;
                    font.FontName = "Calibri";
                    font.Boldweight = (short)FontBoldWeight.Bold;
                    cellr1.CellStyle.SetFont(font);
                    sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, 6));

                    IRow row2 = sheet1.CreateRow(1);
                    ICell cellr2 = row2.CreateCell(0);
                    cellr2.SetCellValue(Tier3);
                    row1.GetCell(0).CellStyle = style;

                    var fontr2 = workbook.CreateFont();
                    fontr2.FontHeightInPoints = 9;
                    fontr2.FontName = "Calibri";
                    fontr2.Boldweight = (short)FontBoldWeight.Bold;
                    //fontr2.Color = IndexedColors.Blue.Index;
                    cellr2.CellStyle.SetFont(fontr2);
                    sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 0, 0));


                    IRow row3 = sheet1.CreateRow(2);
                    ICell cellr3 = row3.CreateCell(0);
                    cellr3.SetCellValue(dfrom + " - " + dto);
                    row3.GetCell(0).CellStyle = style;

                    var fontr3 = workbook.CreateFont();
                    fontr3.FontHeightInPoints = 9;
                    fontr3.FontName = "Calibri";
                    fontr3.Boldweight = (short)FontBoldWeight.Bold;
                    cellr3.CellStyle.SetFont(fontr3);
                    sheet1.AddMergedRegion(new CellRangeAddress(2, 2, 0, 6));

                    IRow row4 = sheet1.CreateRow(4);

                    for (int j = 0; j < dt.Columns.Count - 1; j++)
                    {
                        ICell cell = row4.CreateCell(j);

                        String columnName = dt.Columns[j].ToString();
                        cell.SetCellValue(columnName);
                    }

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        IRow row = sheet1.CreateRow(i + 5);
                        for (int j = 0; j < dt.Columns.Count - 1; j++)
                        {
                            ICell cell = row.CreateCell(j);
                            String columnName = dt.Columns[j].ToString();
                            cell.SetCellValue(dt.Rows[i][columnName].ToString());

                        }
                    }

                    sheet1.AutoSizeColumn(0);
                    sheet1.AutoSizeColumn(1);
                    sheet1.AutoSizeColumn(2);
                    sheet1.AutoSizeColumn(3);
                    sheet1.AutoSizeColumn(4);
                    sheet1.AutoSizeColumn(5);
                    sheet1.AutoSizeColumn(6);
                    sheet1.AutoSizeColumn(7);
                    sheet1.AutoSizeColumn(8);
                    sheet1.AutoSizeColumn(9);
                    sheet1.AutoSizeColumn(10);
                    sheet1.AutoSizeColumn(11);
                    sheet1.AutoSizeColumn(12);
                    sheet1.AutoSizeColumn(13);
                    sheet1.AutoSizeColumn(14);
                    sheet1.AutoSizeColumn(15);
                    sheet1.AutoSizeColumn(16);
                    sheet1.AutoSizeColumn(17);
                    sheet1.AutoSizeColumn(18);
                    sheet1.AutoSizeColumn(19);
                    sheet1.AutoSizeColumn(20);
                    sheet1.AutoSizeColumn(21);
                    sheet1.AutoSizeColumn(22);
                    sheet1.AutoSizeColumn(23);
                    sheet1.AutoSizeColumn(24);
                    sheet1.AutoSizeColumn(25);
                    sheet1.AutoSizeColumn(26);
                }
            }


            using (var exportData = new MemoryStream())
            {
                Response.Clear();
                workbook.Write(exportData);

                ///-----Direct Excel Export Start----///
                ///
                string saveAsFileName = string.Format("Complaints_Types_Report.xls", DateTime.Now);
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", saveAsFileName));
                Response.Clear();
                Response.BinaryWrite(exportData.GetBuffer());
                Response.End();

                ////----Direct Excel Export END----///  
            }
        }
        #endregion
        #region Level2Reports
        public void GetLevel2ComplaintsTypesRollupReport(string Type, string From, string To)
        {
            string clientid = "";
            DataSet ds = new DataSet();
            //MonthYearStr = "01 " + MonthYearStr;
            _ireportDataProvider = new ReportDataProvider();
            DateTime datefrom = Convert.ToDateTime(From);
            DateTime dateto = Convert.ToDateTime(To);
            string dfrom = (datefrom).ToString("dd MMMM yyyy");
            string dto = (dateto).ToString("dd MMMM yyyy");
            
            //int monthInDigit = Convert.ToDateTime(MonthYearStr).Month;
            //int year = Convert.ToDateTime(MonthYearStr).Year;
            Guid ClientID = Common.CheckIdNullOrEmptyNonEncrypt(clientid);
            IWorkbook workbook;
            workbook = new HSSFWorkbook();
            ds = _ireportDataProvider.GetLevel2ComplaintExcelReport(Type);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {

                for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
                {
                    string Tier2 = Convert.ToString(ds.Tables[0].Rows[k]["Tier2"]);
                    DataTable dt = ds.Tables[0];


                    //workbook = new XSSFWorkbook();

                    ISheet sheet1 = workbook.CreateSheet("Sheet " + k);

                    IRow row1 = sheet1.CreateRow(0);
                    ICell cellr1 = row1.CreateCell(0);
                    cellr1.SetCellValue("Complaints Types Roll up Report");
                    ICellStyle style = workbook.CreateCellStyle();
                    style.Alignment = HorizontalAlignment.Left;
                    row1.GetCell(0).CellStyle = style;

                    var font = workbook.CreateFont();
                    font.FontHeightInPoints = 11;
                    font.FontName = "Calibri";
                    font.Boldweight = (short)FontBoldWeight.Bold;
                    //font.Color = IndexedColors.Red.Index;
                    cellr1.CellStyle.SetFont(font);
                    sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, 6));

                    IRow row2 = sheet1.CreateRow(1);
                    ICell cellr2 = row2.CreateCell(0);
                    cellr2.SetCellValue(Tier2);
                    //ICellStyle styler2 = workbook.CreateCellStyle();
                    //styler2.Alignment = HorizontalAlignment.Left;
                    row1.GetCell(0).CellStyle = style;

                    var fontr2 = workbook.CreateFont();
                    fontr2.FontHeightInPoints = 9;
                    fontr2.FontName = "Calibri";
                    fontr2.Boldweight = (short)FontBoldWeight.Bold;
                    //fontr2.Color = IndexedColors.Blue.Index;
                    cellr2.CellStyle.SetFont(fontr2);
                    sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 0, 0));


                    IRow row3 = sheet1.CreateRow(2);
                    ICell cellr3 = row3.CreateCell(0);
                    // DateTime date = DateTime.Now;

                    //cellr2.SetCellValue("Date : " + date.ToString("dd/MM/yyyy"));
                    cellr3.SetCellValue(dfrom + " - " + dto);
                    //ICellStyle styler2 = workbook.CreateCellStyle();
                    //styler2.Alignment = HorizontalAlignment.Left;
                    row3.GetCell(0).CellStyle = style;

                    var fontr3 = workbook.CreateFont();
                    fontr3.FontHeightInPoints = 9;
                    fontr3.FontName = "Calibri";
                    fontr3.Boldweight = (short)FontBoldWeight.Bold;
                    //fontr3.Color = IndexedColors.Grey80Percent.Index;
                    cellr3.CellStyle.SetFont(fontr3);
                    sheet1.AddMergedRegion(new CellRangeAddress(2, 2, 0, 6));

                    IRow row4 = sheet1.CreateRow(4);

                    for (int j = 0; j < dt.Columns.Count - 1; j++)
                    {
                        ICell cell = row4.CreateCell(j);

                        String columnName = dt.Columns[j].ToString();
                        cell.SetCellValue(columnName);
                    }

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        IRow row = sheet1.CreateRow(i + 5);
                        for (int j = 0; j < dt.Columns.Count - 1; j++)
                        {
                            ICell cell = row.CreateCell(j);
                            String columnName = dt.Columns[j].ToString();
                            cell.SetCellValue(dt.Rows[i][columnName].ToString());

                        }
                    }

                    sheet1.AutoSizeColumn(0);
                    sheet1.AutoSizeColumn(1);
                    sheet1.AutoSizeColumn(2);
                    sheet1.AutoSizeColumn(3);
                    sheet1.AutoSizeColumn(4);
                    sheet1.AutoSizeColumn(5);
                    sheet1.AutoSizeColumn(6);
                    sheet1.AutoSizeColumn(7);
                    sheet1.AutoSizeColumn(8);
                    sheet1.AutoSizeColumn(9);
                    sheet1.AutoSizeColumn(10);
                    sheet1.AutoSizeColumn(11);
                    sheet1.AutoSizeColumn(12);
                    sheet1.AutoSizeColumn(13);
                    sheet1.AutoSizeColumn(14);
                    sheet1.AutoSizeColumn(15);
                    sheet1.AutoSizeColumn(16);
                    sheet1.AutoSizeColumn(17);
                    sheet1.AutoSizeColumn(18);
                    sheet1.AutoSizeColumn(19);
                    sheet1.AutoSizeColumn(20);
                }
            }


            using (var exportData = new MemoryStream())
            {
                Response.Clear();
                workbook.Write(exportData);

                ///-----Direct Excel Export Start----///
                ///
                string saveAsFileName = string.Format("ComplaintsTypes_Rollup_Report.xls", DateTime.Now);
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", saveAsFileName));
                Response.Clear();
                Response.BinaryWrite(exportData.GetBuffer());
                Response.End();

            }

        }
        public void GetLevel2ComplaintsDatasRollupReport(string Type, string From, string To)
        {
            string clientid = "";
            DataSet ds = new DataSet();
            //MonthYearStr = "01 " + MonthYearStr;
            _ireportDataProvider = new ReportDataProvider();

            //int monthInDigit = Convert.ToDateTime(MonthYearStr).Month;
            //int year = Convert.ToDateTime(MonthYearStr).Year;
            DateTime datefrom = Convert.ToDateTime(From);
            DateTime dateto = Convert.ToDateTime(To);
            string dfrom = (datefrom).ToString("dd MMMM yyyy");
            string dto = (dateto).ToString("dd MMMM yyyy");
            Guid ClientID = Common.CheckIdNullOrEmptyNonEncrypt(clientid);
            IWorkbook workbook;
            workbook = new HSSFWorkbook();
            //string CompanyID = "CA484A56-94D4-4AD5-9C5A-0ACA865A059E";
            ds = _ireportDataProvider.GetLevel2ComplaintExcelReport(Type);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {

                for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
                {
                    string Tier2 = Convert.ToString(ds.Tables[0].Rows[k]["Tier2"]);
                    DataTable dt = ds.Tables[0];

                    ISheet sheet1 = workbook.CreateSheet("Sheet " + k);

                    IRow row1 = sheet1.CreateRow(0);
                    ICell cellr1 = row1.CreateCell(0);
                    cellr1.SetCellValue("Complaints Data Roll up Report");
                    ICellStyle style = workbook.CreateCellStyle();
                    style.Alignment = HorizontalAlignment.Left;
                    row1.GetCell(0).CellStyle = style;

                    var font = workbook.CreateFont();
                    font.FontHeightInPoints = 11;
                    font.FontName = "Calibri";
                    font.Boldweight = (short)FontBoldWeight.Bold;
                    cellr1.CellStyle.SetFont(font);
                    sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, 6));

                    IRow row2 = sheet1.CreateRow(1);
                    ICell cellr2 = row2.CreateCell(0);
                    cellr2.SetCellValue(Tier2);
                    row1.GetCell(0).CellStyle = style;

                    var fontr2 = workbook.CreateFont();
                    fontr2.FontHeightInPoints = 9;
                    fontr2.FontName = "Calibri";
                    fontr2.Boldweight = (short)FontBoldWeight.Bold;
                    cellr2.CellStyle.SetFont(fontr2);
                    sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 0, 0));


                    IRow row3 = sheet1.CreateRow(2);
                    ICell cellr3 = row3.CreateCell(0);

                    cellr3.SetCellValue(dfrom + " - " + dto);
                    row3.GetCell(0).CellStyle = style;

                    var fontr3 = workbook.CreateFont();
                    fontr3.FontHeightInPoints = 9;
                    fontr3.FontName = "Calibri";
                    fontr3.Boldweight = (short)FontBoldWeight.Bold;
                    cellr3.CellStyle.SetFont(fontr3);
                    sheet1.AddMergedRegion(new CellRangeAddress(2, 2, 0, 6));


                    IRow row4 = sheet1.CreateRow(4);

                    for (int j = 0; j < dt.Columns.Count - 1; j++)
                    {
                        ICell cell = row4.CreateCell(j);

                        String columnName = dt.Columns[j].ToString();
                        cell.SetCellValue(columnName);
                    }

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        IRow row = sheet1.CreateRow(i + 5);
                        for (int j = 0; j < dt.Columns.Count - 1; j++)
                        {
                            ICell cell = row.CreateCell(j);
                            String columnName = dt.Columns[j].ToString();
                            cell.SetCellValue(dt.Rows[i][columnName].ToString());

                        }
                    }

                    sheet1.AutoSizeColumn(0);
                    sheet1.AutoSizeColumn(1);
                    sheet1.AutoSizeColumn(2);
                    sheet1.AutoSizeColumn(3);
                    sheet1.AutoSizeColumn(4);
                    sheet1.AutoSizeColumn(5);
                    sheet1.AutoSizeColumn(6);
                    sheet1.AutoSizeColumn(7);
                    sheet1.AutoSizeColumn(8);
                    sheet1.AutoSizeColumn(9);
                    sheet1.AutoSizeColumn(10);
                    sheet1.AutoSizeColumn(11);
                    sheet1.AutoSizeColumn(12);
                    sheet1.AutoSizeColumn(13);
                    sheet1.AutoSizeColumn(14);
                    sheet1.AutoSizeColumn(15);
                    sheet1.AutoSizeColumn(16);
                    sheet1.AutoSizeColumn(17);
                    sheet1.AutoSizeColumn(18);
                    sheet1.AutoSizeColumn(19);
                    sheet1.AutoSizeColumn(20);
                }
            }


            using (var exportData = new MemoryStream())
            {
                Response.Clear();
                workbook.Write(exportData);

                ///-----Direct Excel Export Start----///
                ///
                string saveAsFileName = string.Format("Complaints_DataRollup_Report.xls", DateTime.Now);
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", saveAsFileName));
                Response.Clear();
                Response.BinaryWrite(exportData.GetBuffer());
                Response.End();
            }
        }
        public void GetLevel2ComplaintTurnaroundTimerollup(string Type, string From, string To)
        {
            string clientid = "";
            DataSet ds = new DataSet();
            //MonthYearStr = "01 " + MonthYearStr;
            _ireportDataProvider = new ReportDataProvider();
            //int monthInDigit = Convert.ToDateTime(MonthYearStr).Month;
            //int year = Convert.ToDateTime(MonthYearStr).Year;
            DateTime datefrom = Convert.ToDateTime(From);
            DateTime dateto = Convert.ToDateTime(To);
            string dfrom = (datefrom).ToString("dd MMMM yyyy");
            string dto = (dateto).ToString("dd MMMM yyyy");
            Guid ClientID = Common.CheckIdNullOrEmptyNonEncrypt(clientid);
            IWorkbook workbook;
            workbook = new HSSFWorkbook();
            //string CompanyID = "CA484A56-94D4-4AD5-9C5A-0ACA865A059E";
            ds = _ireportDataProvider.GetLevel2ComplaintExcelReport(Type);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {

                for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
                {
                    string Tier2 = Convert.ToString(ds.Tables[0].Rows[k]["Tier2"]);
                    DataTable dt = ds.Tables[0];

                    ISheet sheet1 = workbook.CreateSheet("Sheet " + k);

                    IRow row1 = sheet1.CreateRow(0);
                    ICell cellr1 = row1.CreateCell(0);
                    cellr1.SetCellValue("Complaints Turnaround Time Roll up Report");
                    ICellStyle style = workbook.CreateCellStyle();
                    style.Alignment = HorizontalAlignment.Left;
                    row1.GetCell(0).CellStyle = style;

                    var font = workbook.CreateFont();
                    font.FontHeightInPoints = 11;
                    font.FontName = "Calibri";
                    font.Boldweight = (short)FontBoldWeight.Bold;
                    cellr1.CellStyle.SetFont(font);
                    sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, 6));

                    IRow row2 = sheet1.CreateRow(1);
                    ICell cellr2 = row2.CreateCell(0);
                    cellr2.SetCellValue(Tier2);
                    row1.GetCell(0).CellStyle = style;

                    var fontr2 = workbook.CreateFont();
                    fontr2.FontHeightInPoints = 9;
                    fontr2.FontName = "Calibri";
                    fontr2.Boldweight = (short)FontBoldWeight.Bold;
                    cellr2.CellStyle.SetFont(fontr2);
                    sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 0, 0));


                    IRow row3 = sheet1.CreateRow(2);
                    ICell cellr3 = row3.CreateCell(0);

                    cellr3.SetCellValue(dfrom + " - "+ dto);
                    row3.GetCell(0).CellStyle = style;

                    var fontr3 = workbook.CreateFont();
                    fontr3.FontHeightInPoints = 9;
                    fontr3.FontName = "Calibri";
                    fontr3.Boldweight = (short)FontBoldWeight.Bold;
                    cellr3.CellStyle.SetFont(fontr3);
                    sheet1.AddMergedRegion(new CellRangeAddress(2, 2, 0, 6));


                    IRow row4 = sheet1.CreateRow(4);

                    for (int j = 0; j < dt.Columns.Count - 1; j++)
                    {
                        ICell cell = row4.CreateCell(j);

                        String columnName = dt.Columns[j].ToString();
                        cell.SetCellValue(columnName);
                    }

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        IRow row = sheet1.CreateRow(i + 5);
                        for (int j = 0; j < dt.Columns.Count - 1; j++)
                        {
                            ICell cell = row.CreateCell(j);
                            String columnName = dt.Columns[j].ToString();
                            cell.SetCellValue(dt.Rows[i][columnName].ToString());

                        }
                    }

                    sheet1.AutoSizeColumn(0);
                    sheet1.AutoSizeColumn(1);
                    sheet1.AutoSizeColumn(2);
                    sheet1.AutoSizeColumn(3);
                    sheet1.AutoSizeColumn(4);
                    sheet1.AutoSizeColumn(5);
                    sheet1.AutoSizeColumn(6);
                    sheet1.AutoSizeColumn(7);
                    sheet1.AutoSizeColumn(8);
                    sheet1.AutoSizeColumn(9);
                    sheet1.AutoSizeColumn(10);
                    sheet1.AutoSizeColumn(11);
                    sheet1.AutoSizeColumn(12);
                    sheet1.AutoSizeColumn(13);
                    sheet1.AutoSizeColumn(14);
                    sheet1.AutoSizeColumn(15);
                    sheet1.AutoSizeColumn(16);
                    sheet1.AutoSizeColumn(17);
                    sheet1.AutoSizeColumn(18);
                    sheet1.AutoSizeColumn(19);
                    sheet1.AutoSizeColumn(20);
                }
            }


            using (var exportData = new MemoryStream())
            {
                Response.Clear();
                workbook.Write(exportData);

                ///-----Direct Excel Export Start----///
                ///
                string saveAsFileName = string.Format("Complaints_TurnaroundTime_Rollup Report.xls", DateTime.Now);
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", saveAsFileName));
                Response.Clear();
                Response.BinaryWrite(exportData.GetBuffer());
                Response.End();
            }
        }
        #endregion
        #region Level1Reports
        public void GetLevel1ComplaintsTypesRollupReport(string Type, string From, string To)
        {
            string clientid = "";
            DataSet ds = new DataSet();
            //MonthYearStr = "01 " + MonthYearStr;
            _ireportDataProvider = new ReportDataProvider();
            DateTime datefrom = Convert.ToDateTime(From);
            DateTime dateto = Convert.ToDateTime(To);
            string dfrom = (datefrom).ToString("dd MMMM yyyy");
            string dto = (dateto).ToString("dd MMMM yyyy");
            //int monthInDigit = Convert.ToDateTime(MonthYearStr).Month;
            //int year = Convert.ToDateTime(MonthYearStr).Year;
            Guid ClientID = Common.CheckIdNullOrEmptyNonEncrypt(clientid);
            IWorkbook workbook;
            workbook = new HSSFWorkbook();
            ds = _ireportDataProvider.GetLevel1ComplaintExcelReport(Type);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {

                for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
                {
                    string Organisation = Convert.ToString(ds.Tables[0].Rows[k]["Organisation"]);
                    DataTable dt = ds.Tables[0];


                    //workbook = new XSSFWorkbook();

                    ISheet sheet1 = workbook.CreateSheet("Sheet " + k);

                    IRow row1 = sheet1.CreateRow(0);
                    ICell cellr1 = row1.CreateCell(0);
                    cellr1.SetCellValue("Complaints Types Roll up Report");
                    ICellStyle style = workbook.CreateCellStyle();
                    style.Alignment = HorizontalAlignment.Left;
                    row1.GetCell(0).CellStyle = style;

                    var font = workbook.CreateFont();
                    font.FontHeightInPoints = 11;
                    font.FontName = "Calibri";
                    font.Boldweight = (short)FontBoldWeight.Bold;
                    //font.Color = IndexedColors.Red.Index;
                    cellr1.CellStyle.SetFont(font);
                    sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, 6));

                    IRow row2 = sheet1.CreateRow(1);
                    ICell cellr2 = row2.CreateCell(0);
                    cellr2.SetCellValue(Organisation);
                    //ICellStyle styler2 = workbook.CreateCellStyle();
                    //styler2.Alignment = HorizontalAlignment.Left;
                    row1.GetCell(0).CellStyle = style;

                    var fontr2 = workbook.CreateFont();
                    fontr2.FontHeightInPoints = 9;
                    fontr2.FontName = "Calibri";
                    fontr2.Boldweight = (short)FontBoldWeight.Bold;
                    //fontr2.Color = IndexedColors.Blue.Index;
                    cellr2.CellStyle.SetFont(fontr2);
                    sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 0, 0));


                    IRow row3 = sheet1.CreateRow(2);
                    ICell cellr3 = row3.CreateCell(0);
                    // DateTime date = DateTime.Now;

                    //cellr2.SetCellValue("Date : " + date.ToString("dd/MM/yyyy"));
                    cellr3.SetCellValue(dfrom + " - " + dto);
                    //ICellStyle styler2 = workbook.CreateCellStyle();
                    //styler2.Alignment = HorizontalAlignment.Left;
                    row3.GetCell(0).CellStyle = style;

                    var fontr3 = workbook.CreateFont();
                    fontr3.FontHeightInPoints = 9;
                    fontr3.FontName = "Calibri";
                    fontr3.Boldweight = (short)FontBoldWeight.Bold;
                    //fontr3.Color = IndexedColors.Grey80Percent.Index;
                    cellr3.CellStyle.SetFont(fontr3);
                    sheet1.AddMergedRegion(new CellRangeAddress(2, 2, 0, 6));

                    IRow row4 = sheet1.CreateRow(4);

                    for (int j = 0; j < dt.Columns.Count - 1; j++)
                    {
                        ICell cell = row4.CreateCell(j);

                        String columnName = dt.Columns[j].ToString();
                        cell.SetCellValue(columnName);
                    }

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        IRow row = sheet1.CreateRow(i + 5);
                        for (int j = 0; j < dt.Columns.Count - 1; j++)
                        {
                            ICell cell = row.CreateCell(j);
                            String columnName = dt.Columns[j].ToString();
                            cell.SetCellValue(dt.Rows[i][columnName].ToString());

                        }
                    }

                    sheet1.AutoSizeColumn(0);
                    sheet1.AutoSizeColumn(1);
                    sheet1.AutoSizeColumn(2);
                    sheet1.AutoSizeColumn(3);
                    sheet1.AutoSizeColumn(4);
                    sheet1.AutoSizeColumn(5);
                    sheet1.AutoSizeColumn(6);
                    sheet1.AutoSizeColumn(7);
                    sheet1.AutoSizeColumn(8);
                    sheet1.AutoSizeColumn(9);
                    sheet1.AutoSizeColumn(10);
                    sheet1.AutoSizeColumn(11);
                    sheet1.AutoSizeColumn(12);
                    sheet1.AutoSizeColumn(13);
                    sheet1.AutoSizeColumn(14);
                    sheet1.AutoSizeColumn(15);
                    sheet1.AutoSizeColumn(16);
                    sheet1.AutoSizeColumn(17);
                    sheet1.AutoSizeColumn(18);
                    sheet1.AutoSizeColumn(19);
                    sheet1.AutoSizeColumn(20);
                }
            }


            using (var exportData = new MemoryStream())
            {
                Response.Clear();
                workbook.Write(exportData);

                ///-----Direct Excel Export Start----///
                ///
                string saveAsFileName = string.Format("Level1_ComplaintsTypes_Rollup_Report.xls", DateTime.Now);
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", saveAsFileName));
                Response.Clear();
                Response.BinaryWrite(exportData.GetBuffer());
                Response.End();

            }

        }
        public void GetLevel1ComplaintsDatasRollupReport(string Type, string From, string To)
        {
            string clientid = "";
            DataSet ds = new DataSet();
            //MonthYearStr = "01 " + MonthYearStr;
            _ireportDataProvider = new ReportDataProvider();

            //int monthInDigit = Convert.ToDateTime(MonthYearStr).Month;
            //int year = Convert.ToDateTime(MonthYearStr).Year;
            DateTime datefrom = Convert.ToDateTime(From);
            DateTime dateto = Convert.ToDateTime(To);
            string dfrom = (datefrom).ToString("dd MMMM yyyy");
            string dto = (dateto).ToString("dd MMMM yyyy");
            Guid ClientID = Common.CheckIdNullOrEmptyNonEncrypt(clientid);
            IWorkbook workbook;
            workbook = new HSSFWorkbook();
            //string CompanyID = "CA484A56-94D4-4AD5-9C5A-0ACA865A059E";
            ds = _ireportDataProvider.GetLevel1ComplaintExcelReport(Type);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {

                for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
                {
                    string Organisation = Convert.ToString(ds.Tables[0].Rows[k]["Organisation"]);
                    DataTable dt = ds.Tables[0];

                    ISheet sheet1 = workbook.CreateSheet("Sheet " + k);

                    IRow row1 = sheet1.CreateRow(0);
                    ICell cellr1 = row1.CreateCell(0);
                    cellr1.SetCellValue("Complaints Data Roll up Report");
                    ICellStyle style = workbook.CreateCellStyle();
                    style.Alignment = HorizontalAlignment.Left;
                    row1.GetCell(0).CellStyle = style;

                    var font = workbook.CreateFont();
                    font.FontHeightInPoints = 11;
                    font.FontName = "Calibri";
                    font.Boldweight = (short)FontBoldWeight.Bold;
                    cellr1.CellStyle.SetFont(font);
                    sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, 6));

                    IRow row2 = sheet1.CreateRow(1);
                    ICell cellr2 = row2.CreateCell(0);
                    cellr2.SetCellValue(Organisation);
                    row1.GetCell(0).CellStyle = style;

                    var fontr2 = workbook.CreateFont();
                    fontr2.FontHeightInPoints = 9;
                    fontr2.FontName = "Calibri";
                    fontr2.Boldweight = (short)FontBoldWeight.Bold;
                    cellr2.CellStyle.SetFont(fontr2);
                    sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 0, 0));


                    IRow row3 = sheet1.CreateRow(2);
                    ICell cellr3 = row3.CreateCell(0);

                    cellr3.SetCellValue(dfrom + " - " + dto);
                    row3.GetCell(0).CellStyle = style;

                    var fontr3 = workbook.CreateFont();
                    fontr3.FontHeightInPoints = 9;
                    fontr3.FontName = "Calibri";
                    fontr3.Boldweight = (short)FontBoldWeight.Bold;
                    cellr3.CellStyle.SetFont(fontr3);
                    sheet1.AddMergedRegion(new CellRangeAddress(2, 2, 0, 6));


                    IRow row4 = sheet1.CreateRow(4);

                    for (int j = 0; j < dt.Columns.Count - 1; j++)
                    {
                        ICell cell = row4.CreateCell(j);

                        String columnName = dt.Columns[j].ToString();
                        cell.SetCellValue(columnName);
                    }

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        IRow row = sheet1.CreateRow(i + 5);
                        for (int j = 0; j < dt.Columns.Count - 1; j++)
                        {
                            ICell cell = row.CreateCell(j);
                            String columnName = dt.Columns[j].ToString();
                            cell.SetCellValue(dt.Rows[i][columnName].ToString());

                        }
                    }

                    sheet1.AutoSizeColumn(0);
                    sheet1.AutoSizeColumn(1);
                    sheet1.AutoSizeColumn(2);
                    sheet1.AutoSizeColumn(3);
                    sheet1.AutoSizeColumn(4);
                    sheet1.AutoSizeColumn(5);
                    sheet1.AutoSizeColumn(6);
                    sheet1.AutoSizeColumn(7);
                    sheet1.AutoSizeColumn(8);
                    sheet1.AutoSizeColumn(9);
                    sheet1.AutoSizeColumn(10);
                    sheet1.AutoSizeColumn(11);
                    sheet1.AutoSizeColumn(12);
                    sheet1.AutoSizeColumn(13);
                    sheet1.AutoSizeColumn(14);
                    sheet1.AutoSizeColumn(15);
                    sheet1.AutoSizeColumn(16);
                    sheet1.AutoSizeColumn(17);
                    sheet1.AutoSizeColumn(18);
                    sheet1.AutoSizeColumn(19);
                    sheet1.AutoSizeColumn(20);
                }
            }


            using (var exportData = new MemoryStream())
            {
                Response.Clear();
                workbook.Write(exportData);

                ///-----Direct Excel Export Start----///
                ///
                string saveAsFileName = string.Format("Level1_Complaints_DataRollup_Report.xls", DateTime.Now);
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", saveAsFileName));
                Response.Clear();
                Response.BinaryWrite(exportData.GetBuffer());
                Response.End();
            }
        }

        public void GetLevel1ComplaintTurnaroundTimerollup(string Type, string From, string To)
        {
            string clientid = "";
            DataSet ds = new DataSet();
            //MonthYearStr = "01 " + MonthYearStr;
            _ireportDataProvider = new ReportDataProvider();

            //int monthInDigit = Convert.ToDateTime(MonthYearStr).Month;
            //int year = Convert.ToDateTime(MonthYearStr).Year;
            DateTime datefrom = Convert.ToDateTime(From);
            DateTime dateto = Convert.ToDateTime(To);
            string dfrom = (datefrom).ToString("dd MMMM yyyy");
            string dto = (dateto).ToString("dd MMMM yyyy");
            Guid ClientID = Common.CheckIdNullOrEmptyNonEncrypt(clientid);
            IWorkbook workbook;
            workbook = new HSSFWorkbook();
            //string CompanyID = "CA484A56-94D4-4AD5-9C5A-0ACA865A059E";
            ds = _ireportDataProvider.GetLevel1ComplaintExcelReport(Type);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {

                for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
                {
                    string Organisation = Convert.ToString(ds.Tables[0].Rows[k]["Organisation"]);
                    DataTable dt = ds.Tables[0];

                    ISheet sheet1 = workbook.CreateSheet("Sheet " + k);

                    IRow row1 = sheet1.CreateRow(0);
                    ICell cellr1 = row1.CreateCell(0);
                    cellr1.SetCellValue("Complaints Turnaround Time Roll up Report");
                    ICellStyle style = workbook.CreateCellStyle();
                    style.Alignment = HorizontalAlignment.Left;
                    row1.GetCell(0).CellStyle = style;

                    var font = workbook.CreateFont();
                    font.FontHeightInPoints = 11;
                    font.FontName = "Calibri";
                    font.Boldweight = (short)FontBoldWeight.Bold;
                    cellr1.CellStyle.SetFont(font);
                    sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, 6));

                    IRow row2 = sheet1.CreateRow(1);
                    ICell cellr2 = row2.CreateCell(0);
                    cellr2.SetCellValue(Organisation);
                    row1.GetCell(0).CellStyle = style;

                    var fontr2 = workbook.CreateFont();
                    fontr2.FontHeightInPoints = 9;
                    fontr2.FontName = "Calibri";
                    fontr2.Boldweight = (short)FontBoldWeight.Bold;
                    cellr2.CellStyle.SetFont(fontr2);
                    sheet1.AddMergedRegion(new CellRangeAddress(1, 1, 0, 0));


                    IRow row3 = sheet1.CreateRow(2);
                    ICell cellr3 = row3.CreateCell(0);

                    cellr3.SetCellValue(dfrom + " - " + dto);
                    row3.GetCell(0).CellStyle = style;

                    var fontr3 = workbook.CreateFont();
                    fontr3.FontHeightInPoints = 9;
                    fontr3.FontName = "Calibri";
                    fontr3.Boldweight = (short)FontBoldWeight.Bold;
                    cellr3.CellStyle.SetFont(fontr3);
                    sheet1.AddMergedRegion(new CellRangeAddress(2, 2, 0, 6));


                    IRow row4 = sheet1.CreateRow(4);

                    for (int j = 0; j < dt.Columns.Count - 1; j++)
                    {
                        ICell cell = row4.CreateCell(j);

                        String columnName = dt.Columns[j].ToString();
                        cell.SetCellValue(columnName);
                    }

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        IRow row = sheet1.CreateRow(i + 5);
                        for (int j = 0; j < dt.Columns.Count - 1; j++)
                        {
                            ICell cell = row.CreateCell(j);
                            String columnName = dt.Columns[j].ToString();
                            cell.SetCellValue(dt.Rows[i][columnName].ToString());

                        }
                    }

                    sheet1.AutoSizeColumn(0);
                    sheet1.AutoSizeColumn(1);
                    sheet1.AutoSizeColumn(2);
                    sheet1.AutoSizeColumn(3);
                    sheet1.AutoSizeColumn(4);
                    sheet1.AutoSizeColumn(5);
                    sheet1.AutoSizeColumn(6);
                    sheet1.AutoSizeColumn(7);
                    sheet1.AutoSizeColumn(8);
                    sheet1.AutoSizeColumn(9);
                    sheet1.AutoSizeColumn(10);
                    sheet1.AutoSizeColumn(11);
                    sheet1.AutoSizeColumn(12);
                    sheet1.AutoSizeColumn(13);
                    sheet1.AutoSizeColumn(14);
                    sheet1.AutoSizeColumn(15);
                    sheet1.AutoSizeColumn(16);
                    sheet1.AutoSizeColumn(17);
                    sheet1.AutoSizeColumn(18);
                    sheet1.AutoSizeColumn(19);
                    sheet1.AutoSizeColumn(20);
                }
            }

            using (var exportData = new MemoryStream())
            {
                Response.Clear();
                workbook.Write(exportData);

                ///-----Direct Excel Export Start----///
                ///
                string saveAsFileName = string.Format("Level1_Complaints_TurnaroundTime_Rollup Report.xls", DateTime.Now);
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", saveAsFileName));
                Response.Clear();
                Response.BinaryWrite(exportData.GetBuffer());
                Response.End();
            }
        }
        #endregion
    }
}