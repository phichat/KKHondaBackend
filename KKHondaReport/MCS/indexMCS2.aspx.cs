using CrystalDecisions.CrystalReports.Engine;
using System;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.Shared;
//using KKHondaReport.Models;
using System.Globalization;
using ClosedXML.Excel;

namespace KKHondaReport.MCS
{
    public partial class indexMSC2 : System.Web.UI.Page
    {
        private string conStr = ConfigurationManager.ConnectionStrings["KKMssql"].ConnectionString;
        private SqlConnection conn = null;
        private ReportDocument rptDoc;
        protected void Page_Load(object sender, EventArgs e)
        { 

            //report 1
            if (Request.QueryString["BookingReport"] != null)
            {
                if (Boolean.Parse(Request.QueryString["BookingReport"]) == true)
                {
                    string branchType = (Request.QueryString["branchType"] == null ? "" : Request.QueryString["branchType"]);
                    string branchId = (Request.QueryString["branchId"] == null ? "" : Request.QueryString["branchId"]);
                    string brandType = (Request.QueryString["brandType"] == null ? "" : Request.QueryString["brandType"]);
                    string brandTypeId = (Request.QueryString["brandTypeId"] == null ? "" : Request.QueryString["brandTypeId"]);
                    string version = (Request.QueryString["version"] == null ? "" : Request.QueryString["version"]);
                    string design = (Request.QueryString["design"] == null ? "" : Request.QueryString["design"]);
                    string color = (Request.QueryString["color"] == null ? "" : Request.QueryString["color"]);
                    string bookingName = (Request.QueryString["bookingName"] == null ? "" : Request.QueryString["bookingName"]);
                    string strBookingName = (Request.QueryString["bookingNameId"] == null ? "" : Request.QueryString["bookingNameId"]);
                    string strRegisName = (Request.QueryString["regisNameId"] == null ? "" : Request.QueryString["regisNameId"]);
                    string bookingStatus = (Request.QueryString["bookingStatus"] == null ? "" : Request.QueryString["bookingStatus"]);
                    string bookingStatusId = (Request.QueryString["bookingStatusId"] == null ? "" : Request.QueryString["bookingStatusId"]);
                    string bookingDate = (Request.QueryString["bookingDate"] == null ? "" : Request.QueryString["bookingDate"]);
                    string sDate = (Request.QueryString["sDate"] == null ? "" : Request.QueryString["sDate"]);
                    string eDate = (Request.QueryString["eDate"] == null ? "" : Request.QueryString["eDate"]);
                    string bookingReceiveDate = (Request.QueryString["bookingReceiveDate"] == null ? "" : Request.QueryString["bookingReceiveDate"]);
                    string sBookingReceiveDate = (Request.QueryString["sBookingReceiveDate"] == null ? "" : Request.QueryString["sBookingReceiveDate"]);
                    string eBookingReceiveDate = (Request.QueryString["eBookingReceiveDate"] == null ? "" : Request.QueryString["eBookingReceiveDate"]);

                    if (branchType == "")
                    {
                        branchId = "0";
                    }

                    if (brandType == "")
                    {
                        brandTypeId = "0";
                        version = "0";
                        design = "0";
                        color = "0";
                    }

                    if (bookingName == "")
                    {
                        strBookingName = "";
                        strRegisName = "";
                    }

                    if (bookingStatus == "")
                    {
                        bookingStatusId = "0";
                    }

                    if (bookingDate == "")
                    {
                        sDate = "";
                        eDate = "";
                    }


                    if (bookingReceiveDate == "")
                    {
                        sBookingReceiveDate = "";
                        eBookingReceiveDate = "";
                    } 
                    ExportFormatBookingReport(branchType, branchId, brandType, brandTypeId, version, design, color, bookingName, strBookingName, strRegisName, bookingStatus, bookingDate, sDate, eDate, bookingReceiveDate, sBookingReceiveDate, eBookingReceiveDate);
                }

            }

            //report 4. รายงานยอดขายรวม
            //ExportSummarySaleReport();
            if (Request.QueryString["SaleReport"] != null)
            {
                if (Boolean.Parse(Request.QueryString["SaleReport"]) == true)
                {
                    string branchType = (Request.QueryString["branchType"] == null ? "" : Request.QueryString["branchType"]);
                    string branchId = (Request.QueryString["branchId"] == null ? "" : Request.QueryString["branchId"]);
                    string brandType = (Request.QueryString["brandType"] == null ? "" : Request.QueryString["brandType"]);
                    string brandTypeId = (Request.QueryString["brandTypeId"] == null ? "" : Request.QueryString["brandTypeId"]);
                    string version = (Request.QueryString["version"] == null ? "" : Request.QueryString["version"]);
                    string design = (Request.QueryString["design"] == null ? "" : Request.QueryString["design"]);
                    string color = (Request.QueryString["color"] == null ? "" : Request.QueryString["color"]);
                    string isSellName = (Request.QueryString["isSellName"] == null ? "" : Request.QueryString["isSellName"]);
                    string SellId = (Request.QueryString["SellId"] == null ? "" : Request.QueryString["SellId"]);
                    string isPaymentType = (Request.QueryString["isPaymentType"] == null ? "" : Request.QueryString["isPaymentType"]);
                    string paymentTypeId = (Request.QueryString["paymentTypeId"] == null ? "" : Request.QueryString["paymentTypeId"]);
                    string isSellDate = (Request.QueryString["isSellDate"] == null ? "" : Request.QueryString["isSellDate"]);
                    string sDate = (Request.QueryString["sDate"] == null ? "" : Request.QueryString["sDate"]);
                    string eDate = (Request.QueryString["eDate"] == null ? "" : Request.QueryString["eDate"]);
                    if (branchType == "")
                    {
                        branchId = "0";
                    }

                    if (brandType == "")
                    {
                        brandTypeId = "0";
                        version = "0";
                        design = "0";
                        color = "0";
                    }

                    if (isSellName == "")
                    {
                        SellId = "0";
                    }

                    if (isPaymentType == "")
                    {
                        paymentTypeId = "0";
                    }

                    if (isSellDate == "")
                    {
                        sDate = "";
                        eDate = "";
                    }

                    ExportSummarySaleReport(branchId, brandTypeId, version, design, color, SellId, paymentTypeId, sDate, eDate);
                }

            }

            //report 7.รายงานสรุปยอดขายแยกประเภท
            //ExportSummarySaleReportByType();
            if (Request.QueryString["SaleReportByType"] != null)
            {
                if (Boolean.Parse(Request.QueryString["SaleReportByType"]) == true)
                {
                    string strStartDate = Request.QueryString["strStartDate"];
                    string strEndDate = Request.QueryString["strEndDate"];
                    ExportSummarySaleReportByType(strStartDate, strEndDate);
                }

            }

            //report 8.รายงานสินค้าคงเหลือ
            //ExportSummaryStockBalance();
            if (Request.QueryString["StockBalance"] != null)
            {
                if (Boolean.Parse(Request.QueryString["StockBalance"]) == true)
                {
                    string strStartDate = Request.QueryString["strStartDate"];
                    string strEndDate = Request.QueryString["strEndDate"];

                    ExportSummaryStockBalance(strStartDate, strEndDate);
                }

            }

            //report 2. รายงานการรับคืนสินค้า (แบบละเอียด)
            //ExportCreditNote();
            if (Request.QueryString["CreditNote"] != null)
            {
                if (Boolean.Parse(Request.QueryString["CreditNote"]) == true)
                {
                    string branch_ids = Request.QueryString["branchId"];
                    string sdate = Request.QueryString["sdate"];
                    string edate = Request.QueryString["edate"];
                    string book_person = (Request.QueryString["bookingNameId"] == null ? "" : Request.QueryString["bookingNameId"]);
                    ExportCreditNote(branch_ids, sdate, edate, book_person);
                }
            }

            //report 17. รายงานยอดขายสินค้า - แสดงกำไร
            //ExportProductSellingProfit();
            if (Request.QueryString["ProductSellingProfit"] != null)
            {
                if (Boolean.Parse(Request.QueryString["ProductSellingProfit"]) == true)
                    ExportProductSellingProfit();
            }

            //report 21, รายงานการซ่อมที่ยังดำเนินการ
            if (Request.QueryString["ServiceCheckReport"] != null)
            {
                if (Boolean.Parse(Request.QueryString["ServiceCheckReport"]) == true)
                    ExportServiceCheckReport();
            }

            //report 31. รายงานสินค้าคงเหลือ
            //ExportStockBalanceMain();
            if (Request.QueryString["StockBalanceMain"] != null)
            {
                if (Boolean.Parse(Request.QueryString["StockBalanceMain"]) == true)
                    ExportStockBalanceMain();
            }

            //report 19. รายงานการับคืนเงินมัดจำ
            //ExportSummaryDepositReport();

            if (Request.QueryString["SummaryDepositReport"] != null)
            {
                if (Boolean.Parse(Request.QueryString["SummaryDepositReport"]) == true)
                    ExportSummaryDepositReport();
            }

            //report 28. รายงานการลดหนี้
            //ExportCreditNoteDetailsReport();

            if (Request.QueryString["CreditNoteDetailsReport"] != null)
            {
                if (Boolean.Parse(Request.QueryString["CreditNoteDetailsReport"]) == true)
                    ExportCreditNoteDetailsReport();
            }

            //report 44    
            //ExportSaleQtyDaily(); 
            if (Request.QueryString["ExportSaleQtyDaily"] != null)
            {
                if (Boolean.Parse(Request.QueryString["ExportSaleQtyDaily"]) == true)
                {
                    string sDate = Request.QueryString["sdate"];
                    //string sDate = "2018-06-09T00:00:00.000Z";
                    var dateTime = DateTime.ParseExact(sDate,
                                  "yyyy-MM-ddTHH:mm:ss.fffZ",
                                  CultureInfo.InvariantCulture);
                    //DateTimeFormatInfo usDtfi = new CultureInfo("en-US", false).DateTimeFormat;
                    //DateTimeFormatInfo ukDtfi = new CultureInfo("en-GB", false).DateTimeFormat;
                    //var dateTime = Convert.ToDateTime(sDate, usDtfi);

                    //string strFullFormat = dateTime.ToString("yyyy-MM-dd");
                    string strFullFormat = dateTime.Year + "-" + string.Format("{0:00}", dateTime.Month) + "-" + string.Format("{0:00}", dateTime.Day);
                    int iMonth = dateTime.Month;
                    int iYear = dateTime.Year;
                    string strFullText = dateTime.ToString("dddd, dd MMMM yyyy");

                    ExportSaleQtyDaily(strFullFormat, iMonth, iYear, strFullText);
                }   
            }

            //report 44_2
            //ExportSaleQtyDaily2();
            if (Request.QueryString["ExportSaleQtyDaily2"] != null)
            {
                if (Boolean.Parse(Request.QueryString["ExportSaleQtyDaily2"]) == true)
                {
                    string sDate = Request.QueryString["sdate"];
                    //string sDate = "2018-06-09";
                    var dateTime = DateTime.ParseExact(sDate,
                                 "yyyy-MM-ddTHH:mm:ss.fffZ",
                                  CultureInfo.InvariantCulture);
                    //string strFullFormat = dateTime.ToString("yyyy-MM-dd");
                    string strFullFormat = dateTime.Year + "-" + string.Format("{0:00}", dateTime.Month) + "-" + string.Format("{0:00}", dateTime.Day);
                    int iMonth = dateTime.Month;
                    int iYear = dateTime.Year;
                    string strFullText = dateTime.ToString("dddd, dd MMMM yyyy");

                    ExportSaleQtyDaily2(strFullFormat, iMonth, iYear, strFullText); 
                }
            }

            //report 15 รายงานการรับ-คืนเงินมัดจำลูกค้า (แยกประเภท)
            //ExportSummaryDepositByDate();
            if (Request.QueryString["SummaryDepositByDate"] != null)
            {
                if (Boolean.Parse(Request.QueryString["SummaryDepositByDate"]) == true)
                {
                    string strStartDate = Request.QueryString["strStartDate"];
                    string strEndDate = Request.QueryString["strEndDate"];
                    ExportSummaryDepositByDate(strStartDate, strEndDate);
                }

            }

            //report 14 รายงานประวัติการชำระและเกรดสัญญา
            //ExportContractGradePayment();
            if (Request.QueryString["ExportContractGradePayment"] != null)
            {
                if (Boolean.Parse(Request.QueryString["ExportContractGradePayment"]) == true)
                {
                    ExportContractGradePayment();
                }

            }
        }

        private void ExportFormatBookingReport(string branchType = "", string branchId = "", string brandType = "", string brandTypeId = "", string version = "", string design = "", string color = "", string bookingName = "", string strBookingName = "", string strRegisName = "", string bookingStatus = "", string bookingDate = "", string sDate = "", string eDate = "", string bookingReceiveDate = "", string sBookingReceiveDate = "", string eBookingReceiveDate = "")
        {
            try
            {
                rptDoc = new ReportDocument();
                SqlConnectionStringBuilder connection = new SqlConnectionStringBuilder(conStr);
                var server = connection.DataSource;

                var file = "./BookingReport.rpt";
                rptDoc.Load(Server.MapPath(file));
                rptDoc.Refresh();

                TableLogOnInfo L1 = rptDoc.Database.Tables[0].LogOnInfo;
                GetLoginfo(L1, server);
                //rptDoc.SetParameterValue("@branch_id", "1,2,3,4,5,6");
                //rptDoc.SetParameterValue("strBookingType", "test string Booking Type");
                rptDoc.SetParameterValue("@branch_ids", branchId);
                rptDoc.SetParameterValue("@brandId", brandTypeId);
                rptDoc.SetParameterValue("@modelId", version);
                rptDoc.SetParameterValue("@typeId", design);
                rptDoc.SetParameterValue("@colorId", color);
                rptDoc.SetParameterValue("@book_preson", strBookingName);
                rptDoc.SetParameterValue("@regist_person", strRegisName);
                rptDoc.SetParameterValue("@book_status_id", bookingStatus);
                rptDoc.SetParameterValue("@booking_date_start", sDate);
                rptDoc.SetParameterValue("@booking_date_end", eDate);
                rptDoc.SetParameterValue("@book_receive_date_start", sBookingReceiveDate);
                rptDoc.SetParameterValue("@book_receive_date_end", eBookingReceiveDate);
                rptDoc.Database.Tables[0].ApplyLogOnInfo(L1);

                //rptDoc.SetParameterValue("@branch_ids", branchId, "Subreport1");
                //rptDoc.SetParameterValue("@brand_id", 0, "Subreport1");
                //rptDoc.SetParameterValue("@model_id", 0, "Subreport1");
                //rptDoc.SetParameterValue("@type_id", 0, "Subreport1");
                //rptDoc.SetParameterValue("@type_id", 0, "Subreport1");
                //rptDoc.SetParameterValue("@color_id", 0, "Subreport1");
                //rptDoc.SetParameterValue("@color_id", 0, "Subreport1");
                //rptDoc.SetParameterValue("@book_preson", "", "Subreport1");
                //rptDoc.SetParameterValue("@regist_person", "", "Subreport1");
                //rptDoc.SetParameterValue("@book_status_id", "0", "Subreport1");
                //rptDoc.SetParameterValue("@booking_date_start", "", "Subreport1");
                //rptDoc.SetParameterValue("@booking_date_end", "", "Subreport1");
                //rptDoc.SetParameterValue("@book_receive_date_start", "", "Subreport1");
                //rptDoc.SetParameterValue("@book_receive_date_end", "", "Subreport1");


                //rptDoc.Subreports["Subreport1"].Database.Tables[0].ApplyLogOnInfo(L1);

                StreamPdfReport(rptDoc, "test");
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        private void ExportSummarySaleReport(string branchId = "", string brandTypeId = "", string version = "", string design = "", string color = "", string SellId = "", string paymentTypeId = "", string sDate = "", string eDate = "")
        {
            try
            {
                rptDoc = new ReportDocument();
                SqlConnectionStringBuilder connection = new SqlConnectionStringBuilder(conStr);
                var server = connection.DataSource;

                var file = "./SaleReport.rpt";
                rptDoc.Load(Server.MapPath(file));
                rptDoc.Refresh();

                TableLogOnInfo L1 = rptDoc.Database.Tables[0].LogOnInfo;
                GetLoginfo(L1, server);
                //rptDoc.SetParameterValue("@branch_id", 1);
                //rptDoc.SetParameterValue("strBookingType", "test ExportSummarySaleReport");
                rptDoc.SetParameterValue("@branch_ids", branchId);
                rptDoc.SetParameterValue("@brandId", brandTypeId);
                rptDoc.SetParameterValue("@modelId", version);
                rptDoc.SetParameterValue("@typeId", design);
                rptDoc.SetParameterValue("@colorId", color);
                rptDoc.SetParameterValue("@sale_preson_id", SellId);
                rptDoc.SetParameterValue("@payment_type_id", paymentTypeId);
                rptDoc.SetParameterValue("@sale_date_start", sDate);
                rptDoc.SetParameterValue("@sale_date_end", eDate);
                rptDoc.Database.Tables[0].ApplyLogOnInfo(L1);

                //rptDoc.SetParameterValue("@booking_id", 2, "Subreport3"); 
                //rptDoc.Subreports["Subreport1"].Database.Tables[0].ApplyLogOnInfo(L1);

                StreamPdfReport(rptDoc, "test");
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        private void ExportSummarySaleReportByType(string strStartDate = "", string strEndDate = "")
        {
            try
            {
                //var sDate = DateTime.ParseExact("2018-06-05",
                //                  "yyyy-MM-dd",
                //                   CultureInfo.InvariantCulture);
                //var eDate = DateTime.ParseExact("2018-09-15",
                //                  "yyyy-MM-dd",
                //                   CultureInfo.InvariantCulture);

                var startDateTime = DateTime.ParseExact(strStartDate,
                                  "yyyy-MM-ddTHH:mm:ss.fffZ",
                                   CultureInfo.InvariantCulture);
                var endDateTime = DateTime.ParseExact(strEndDate,
                                  "yyyy-MM-ddTHH:mm:ss.fffZ",
                                   CultureInfo.InvariantCulture);

                string sDate = startDateTime.Year + "-" + string.Format("{0:00}", startDateTime.Month) + "-" + string.Format("{0:00}", startDateTime.Day);

                string eDate = endDateTime.Year + "-" + string.Format("{0:00}", endDateTime.Month) + "-" + string.Format("{0:00}", endDateTime.Day);


                rptDoc = new ReportDocument();
                SqlConnectionStringBuilder connection = new SqlConnectionStringBuilder(conStr);
                var server = connection.DataSource;

                var file = "../ALL/SummarySaleReportByType.rpt";
                rptDoc.Load(Server.MapPath(file));
                rptDoc.Refresh();

                TableLogOnInfo L1 = rptDoc.Database.Tables[0].LogOnInfo;
                GetLoginfo(L1, server);
                //rptDoc.SetParameterValue("@branch_id", 1);
                rptDoc.SetParameterValue("@sDate", sDate);
                rptDoc.SetParameterValue("@eDate", eDate);
                rptDoc.Database.Tables[0].ApplyLogOnInfo(L1);

                //rptDoc.SetParameterValue("@booking_id", 2, "Subreport3"); 
                //rptDoc.Subreports["Subreport1"].Database.Tables[0].ApplyLogOnInfo(L1);

                StreamPdfReport(rptDoc, "test");
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        private void ExportSummaryStockBalance(string strStartDate = "", string strEndDate = "")
        {
            try
            {
                //var startReceiveDate = DateTime.ParseExact("2018-06-05",
                //                  "yyyy-MM-dd",
                //                   CultureInfo.InvariantCulture);
                //var EndReceiveDate = DateTime.ParseExact("2018-09-15",
                //                  "yyyy-MM-dd",
                //                   CultureInfo.InvariantCulture);

                var startDateTime = DateTime.ParseExact(strStartDate,
                                  "yyyy-MM-ddTHH:mm:ss.fffZ",
                                   CultureInfo.InvariantCulture);
                var endDateTime = DateTime.ParseExact(strEndDate,
                                 "yyyy-MM-ddTHH:mm:ss.fffZ",
                                  CultureInfo.InvariantCulture);

                string startReceiveDate = startDateTime.Year + "-" + string.Format("{0:00}", startDateTime.Month) + "-" + string.Format("{0:00}", startDateTime.Day);

                string EndReceiveDate = endDateTime.Year + "-" + string.Format("{0:00}", endDateTime.Month) + "-" + string.Format("{0:00}", endDateTime.Day);

                rptDoc = new ReportDocument();
                SqlConnectionStringBuilder connection = new SqlConnectionStringBuilder(conStr);
                var server = connection.DataSource;

                var file = "./SummaryStockBalance.rpt";
                rptDoc.Load(Server.MapPath(file));
                rptDoc.Refresh();

                TableLogOnInfo L1 = rptDoc.Database.Tables[0].LogOnInfo;
                GetLoginfo(L1, server);
                //rptDoc.SetParameterValue("@branch_id", 1);
                rptDoc.SetParameterValue("strReceiveDate", startReceiveDate);
                rptDoc.SetParameterValue("@start_receive_date", startReceiveDate);
                rptDoc.SetParameterValue("@end_receive_date", EndReceiveDate);
                rptDoc.Database.Tables[0].ApplyLogOnInfo(L1);

                //rptDoc.SetParameterValue("@start_receive_date", startReceiveDate, "Subreport1");
                //rptDoc.SetParameterValue("@end_receive_date", EndReceiveDate, "Subreport1");
                //rptDoc.Subreports["Subreport1"].Database.Tables[0].ApplyLogOnInfo(L1);

                StreamPdfReport(rptDoc, "test");
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        private void ExportCreditNote(string branch_ids = "", string sdate = "", string edate = "", string book_person = "")
        {
            try
            {
                var displayStartDate = "";
                if (sdate != "")
                {
                    var strStartDate = sdate.Split('T')[0];
                    DateTime dateTimeStartDate = DateTime.ParseExact(strStartDate,
                                  "yyyy-MM-ddTHH:mm:ss.fffZ",
                                   CultureInfo.InvariantCulture);
                    displayStartDate = dateTimeStartDate.ToString("dd/MM/yyyy");
                }

                var displayEndDate = "";
                if (edate != "")
                {
                    var strEndDate = edate.Split('T')[0];
                    DateTime dateTimeEndDate = DateTime.ParseExact(strEndDate,
                                  "yyyy-MM-ddTHH:mm:ss.fffZ",
                                   CultureInfo.InvariantCulture);
                    displayEndDate = dateTimeEndDate.ToString("dd/MM/yyyy");
                }


                rptDoc = new ReportDocument();
                SqlConnectionStringBuilder connection = new SqlConnectionStringBuilder(conStr);
                var server = connection.DataSource;

                var file = "./CreditNote.rpt";
                rptDoc.Load(Server.MapPath(file));
                rptDoc.Refresh();

                TableLogOnInfo L1 = rptDoc.Database.Tables[0].LogOnInfo;
                GetLoginfo(L1, server);
                //rptDoc.SetParameterValue("@branch_id", 1);
                rptDoc.SetParameterValue("strStartCreditNoteDate", displayStartDate);
                rptDoc.SetParameterValue("strEndCreditNoteDate", displayEndDate);
                rptDoc.SetParameterValue("@sDate", sdate);
                rptDoc.SetParameterValue("@eDate", edate);
                rptDoc.SetParameterValue("@branch_ids", branch_ids);
                rptDoc.SetParameterValue("@book_person", book_person);
                rptDoc.Database.Tables[0].ApplyLogOnInfo(L1);

                //rptDoc.SetParameterValue("@sDate", startCreditNoteDate, "Subreport1");
                //rptDoc.SetParameterValue("@eDate", EndCreditNoteDate, "Subreport1");
                //rptDoc.Subreports["Subreport1"].Database.Tables[0].ApplyLogOnInfo(L1);

                StreamPdfReport(rptDoc, "test");
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        private void StreamPdfReport(ReportDocument rptDoc, string fileName)
        {
            using (MemoryStream oStream = new MemoryStream())
            {
                rptDoc.ExportToStream(ExportFormatType.PortableDocFormat).CopyTo(oStream);
                Response.Clear();
                Response.Buffer = true;
                Response.AppendHeader("Content-Disposition", $"inline; filename={fileName}");
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(oStream.ToArray());
                Response.End();
                rptDoc.Close();
                rptDoc.Dispose();
            }
        }

        private void ExportProductSellingProfit()
        {
            try
            {
                rptDoc = new ReportDocument();
                SqlConnectionStringBuilder connection = new SqlConnectionStringBuilder(conStr);
                var server = connection.DataSource;

                var file = "../PSS/ProductSellingProfitReport.rpt";
                rptDoc.Load(Server.MapPath(file));
                rptDoc.Refresh();

                TableLogOnInfo L1 = rptDoc.Database.Tables[0].LogOnInfo;
                GetLoginfo(L1, server);
                //rptDoc.SetParameterValue("@branch_id", 1);
                //rptDoc.SetParameterValue("strBookingType", "test string Booking Type");
                rptDoc.Database.Tables[0].ApplyLogOnInfo(L1);

                //rptDoc.SetParameterValue("@booking_id", 2, "Subreport3"); 
                //rptDoc.Subreports["Subreport1"].Database.Tables[0].ApplyLogOnInfo(L1);

                StreamPdfReport(rptDoc, "test");
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        private void ExportServiceCheckReport()
        {
            try
            {
                rptDoc = new ReportDocument();
                SqlConnectionStringBuilder connection = new SqlConnectionStringBuilder(conStr);
                var server = connection.DataSource;

                var file = "../PSS/ServiceCheckReport.rpt";
                rptDoc.Load(Server.MapPath(file));
                rptDoc.Refresh();

                TableLogOnInfo L1 = rptDoc.Database.Tables[0].LogOnInfo;
                GetLoginfo(L1, server);
                //rptDoc.SetParameterValue("@branch_id", 1);
                //rptDoc.SetParameterValue("strBookingType", "test string Booking Type");
                rptDoc.Database.Tables[0].ApplyLogOnInfo(L1);

                //rptDoc.SetParameterValue("@booking_id", 2, "Subreport3"); 
                //rptDoc.Subreports["Subreport1"].Database.Tables[0].ApplyLogOnInfo(L1);

                StreamPdfReport(rptDoc, "test");
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        private void ExportCreditNoteDetailsReport()
        {
            try
            {
                rptDoc = new ReportDocument();
                SqlConnectionStringBuilder connection = new SqlConnectionStringBuilder(conStr);
                var server = connection.DataSource;

                var file = "../OTH/CreditNoteDetailsReport.rpt";
                rptDoc.Load(Server.MapPath(file));
                rptDoc.Refresh();

                TableLogOnInfo L1 = rptDoc.Database.Tables[0].LogOnInfo;
                GetLoginfo(L1, server);
                //rptDoc.SetParameterValue("@branch_id", "1,2,3,4,5,6");
                //rptDoc.SetParameterValue("strBookingType", "test string Booking Type");
                rptDoc.Database.Tables[0].ApplyLogOnInfo(L1);

                //rptDoc.SetParameterValue("@booking_id", 2, "Subreport3"); 
                //rptDoc.Subreports["Subreport1"].Database.Tables[0].ApplyLogOnInfo(L1);

                StreamPdfReport(rptDoc, "test");
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        private void ExportStockBalanceMain()
        {
            try
            {
                rptDoc = new ReportDocument();
                SqlConnectionStringBuilder connection = new SqlConnectionStringBuilder(conStr);
                var server = connection.DataSource;

                var file = "../OTH/StockBalanceMainReport.rpt";
                rptDoc.Load(Server.MapPath(file));
                rptDoc.Refresh();

                TableLogOnInfo L1 = rptDoc.Database.Tables[0].LogOnInfo;
                GetLoginfo(L1, server);
                //rptDoc.SetParameterValue("@branch_id", 1);
                //rptDoc.SetParameterValue("strBookingType", "test ExportSummarySaleReport");
                rptDoc.Database.Tables[0].ApplyLogOnInfo(L1);

                //rptDoc.SetParameterValue("@booking_id", 2, "Subreport3"); 
                //rptDoc.Subreports["Subreport1"].Database.Tables[0].ApplyLogOnInfo(L1);

                StreamPdfReport(rptDoc, "test");
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        private void ExportSummaryDepositReport()
        {
            try
            {
                rptDoc = new ReportDocument();
                SqlConnectionStringBuilder connection = new SqlConnectionStringBuilder(conStr);
                var server = connection.DataSource;

                var file = "../PSS/SummaryDepositReport.rpt";
                rptDoc.Load(Server.MapPath(file));
                rptDoc.Refresh();

                TableLogOnInfo L1 = rptDoc.Database.Tables[0].LogOnInfo;
                GetLoginfo(L1, server);
                //rptDoc.SetParameterValue("@branch_id", 1);
                //rptDoc.SetParameterValue("strBookingType", "test ExportSummarySaleReport");
                rptDoc.Database.Tables[0].ApplyLogOnInfo(L1);

                //rptDoc.SetParameterValue("@booking_id", 2, "Subreport3"); 
                //rptDoc.Subreports["Subreport1"].Database.Tables[0].ApplyLogOnInfo(L1);

                StreamPdfReport(rptDoc, "test");
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        public string ExportSaleQtyDaily(string strFullFormat = "", int iMonth =0, int iYear = 0, string strFullText = "")
        {

            //call SP
            //SP_SaleQtyDailyByPaymentType
            DataTable dtPaymentType = new DataTable();
            using (var con = new SqlConnection(conStr))
            {
                using (var cmd = new SqlCommand("SP_SaleQtyDailyByPaymentType", con))
                {
                    cmd.Parameters.Add("@booking_date", SqlDbType.VarChar);
                    cmd.Parameters["@booking_date"].Value = strFullFormat;

                    cmd.Parameters.Add("@month", SqlDbType.Int);
                    cmd.Parameters["@month"].Value = iMonth;

                    cmd.Parameters.Add("@year", SqlDbType.Int);
                    cmd.Parameters["@year"].Value = iYear;
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        da.Fill(dtPaymentType);
                    }
                }
            }

            //SP_SaleQtyDailyByFinance
            DataTable dtFinance = new DataTable();
            using (var con = new SqlConnection(conStr))
            {
                using (var cmd = new SqlCommand("SP_SaleQtyDailyByFinance", con))
                {
                    cmd.Parameters.Add("@booking_date", SqlDbType.VarChar);
                    cmd.Parameters["@booking_date"].Value = strFullFormat;

                    cmd.Parameters.Add("@month", SqlDbType.Int);
                    cmd.Parameters["@month"].Value = iMonth;

                    cmd.Parameters.Add("@year", SqlDbType.Int);
                    cmd.Parameters["@year"].Value = iYear;
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        da.Fill(dtFinance);
                    }
                }
            }

            //listZone
            DataView vZone = new DataView(dtPaymentType);
            DataTable dtZone = vZone.ToTable(true, "zone_id", "zone_name");

            //listFinance
            DataView vFinance = new DataView(dtFinance);
            DataTable dtListFinance = vFinance.ToTable(true, "fi_id", "fi_name");


            List<string> listHeader = new List<string>();
            listHeader.Add("เช่าซื้อ KKI");
            listHeader.Add("เงินสด");
            listHeader.Add("ขายส่ง");


            //add finance to listHeader and dtPaymentType
            foreach (DataRow rowListFinance in dtListFinance.Rows)
            {
                var fi_id = rowListFinance["fi_id"].ToString();
                var fi_name = rowListFinance["fi_name"].ToString();

                //check col if not exist then add it.
                DataColumnCollection columns = dtPaymentType.Columns;
                if ((!columns.Contains(fi_id)) && (fi_id != ""))
                {
                    //dtPaymentType.Columns.Add(financeId, typeof(System.Int32));
                    DataColumn colFinance = new DataColumn(fi_id, typeof(System.String));
                    colFinance.DefaultValue = 0;
                    dtPaymentType.Columns.Add(colFinance);
                    listHeader.Add(fi_name);
                }

            }

            List<string> listSubHeader = new List<string>();
            //listSubHeader.Add("ลำดับ");
            //listSubHeader.Add("สาขา");

            // Data of list of string arrays 
            var listOfDataArr = new List<string[]>();
            int lastZoneRow = 0;

            // list row zone not include to sum
            List<int> listZoneRow = new List<int>();

            //list summary
            List<string> listSummary = new List<string>();
            ////add zone name
            listSummary.Add("รวมทั้งสิ้น");
            listSummary.Add("xx");

            foreach (DataRow rowZone in dtZone.Rows)
            {
                var zone_id = rowZone["zone_id"].ToString();
                var zone_name = rowZone["zone_name"].ToString();

                List<string> listDataZoneRow = new List<string>();
                ////add zone name
                listDataZoneRow.Add(zone_name);
                listDataZoneRow.Add("xx");

                //ลำดับ
                int row = 1;
                foreach (DataRow rowPaymentType in dtPaymentType.Rows)
                {
                    var booking_date = rowPaymentType["booking_date"].ToString();
                    int branch_id = Convert.ToInt32(rowPaymentType["branch_id"].ToString());
                    var zoneName = rowPaymentType["zone_name"].ToString();
                    var zoneId = rowPaymentType["zone_id"].ToString();
                    var branchName = rowPaymentType["branch_name"].ToString();
                    var cash_qty = Convert.ToInt32(rowPaymentType["cash_qty"].ToString());
                    var installment_qty = Convert.ToInt32(rowPaymentType["installment_qty"].ToString());
                    var wholesale_qty = Convert.ToInt32(rowPaymentType["wholesale_qty"].ToString());
                    var cash_qty_acc = Convert.ToInt32(rowPaymentType["cash_qty_acc"].ToString());
                    var installment_qty_acc = Convert.ToInt32(rowPaymentType["installment_qty_acc"].ToString());
                    var wholesale_qty_acc = Convert.ToInt32(rowPaymentType["wholesale_qty_acc"].ToString());
                    //check zone
                    if (zone_id == zoneId)
                    {
                        //total by rows วันนี้
                        int sumTotalByRowsToday = 0;
                        //total by rows สะสม
                        int sumTotalByRows = 0;

                        //Data Each Row
                        List<string> listDataRow = new List<string>();

                        listDataRow.Add(row.ToString());
                        listDataRow.Add(branchName);

                        //data เช่าซื้อ KKI
                        listDataRow.Add(installment_qty.ToString()); 
                        listDataRow.Add(installment_qty_acc.ToString());

                        //data เงินสด
                        listDataRow.Add(cash_qty.ToString());
                        listDataRow.Add(cash_qty_acc.ToString());


                        //data ขายส่ง
                        listDataRow.Add(wholesale_qty.ToString());
                        listDataRow.Add(wholesale_qty_acc.ToString());

                        sumTotalByRowsToday += installment_qty + cash_qty + wholesale_qty;
                        sumTotalByRows += installment_qty_acc + cash_qty_acc + wholesale_qty_acc;

                        foreach (DataRow rowListFinance in dtListFinance.Rows)
                        {
                            int fi_id = Convert.ToInt32(rowListFinance["fi_id"].ToString());
                            var fi_name = rowListFinance["fi_name"].ToString();

                            var queryModel = from dt in dtFinance.AsEnumerable()
                                             where dt.Field<int>("fi_id") == fi_id &&
                                             dt.Field<int>("branch_id") == branch_id
                                             select dt;
                            if (!queryModel.Any())
                            {
                                listDataRow.Add("0");
                                listDataRow.Add("0");
                            }
                            else
                            {
                                string saleQty = queryModel.First().Field<int>("sale_qty").ToString();
                                string saleQtyAcc = queryModel.First().Field<int>("sale_qty_acc").ToString();
                                listDataRow.Add(saleQty);
                                listDataRow.Add(saleQtyAcc);

                                int qty = Convert.ToInt32(saleQty);
                                sumTotalByRowsToday += qty;

                                int qtyAcc = Convert.ToInt32(saleQtyAcc);
                                sumTotalByRows += qtyAcc;
                            }

                        }

                        //add sum total by rows to list
                        listDataRow.Add(sumTotalByRowsToday.ToString());
                        listDataRow.Add(sumTotalByRows.ToString());

                        string[] arrDataRow = listDataRow.ToArray();
                        listOfDataArr.Add(arrDataRow);
                        row++;
                    } 
                }

                //รวมแต่ละ zone
                if (listOfDataArr.Count() > 0)
                {
                    //sum zone by each col

                    for (int col = 2; col < listOfDataArr[0].Count(); col++)
                    {
                        var sumQty = 0;
                        for (int listRow = lastZoneRow; listRow < listOfDataArr.Count; listRow++)
                        {
                            var qty = listOfDataArr[listRow][col];
                            sumQty += Convert.ToInt32(qty);
                        }
                        listDataZoneRow.Add(sumQty.ToString());
                    }
                    string[] arrDataZoneRow = listDataZoneRow.ToArray();
                    listOfDataArr.Add(arrDataZoneRow);

                    lastZoneRow = listOfDataArr.Count;

                    //store last zone row index
                    listZoneRow.Add(lastZoneRow - 1);
                }

            }

            //Summary
            //รวทั้งสิ้น
            if (listOfDataArr.Count() > 0)
            {
                //sum zone by each col

                for (int col = 2; col < listOfDataArr[0].Count(); col++)
                {
                    var sumQty = 0;
                    foreach (var listRow in listZoneRow)
                    {
                        var qty = listOfDataArr[listRow][col];
                        sumQty += Convert.ToInt32(qty);
                    }

                    listSummary.Add(sumQty.ToString());
                }
                string[] arrDataSummaryZoneRow = listSummary.ToArray();
                listOfDataArr.Add(arrDataSummaryZoneRow);
            }

            //Total By Rows
            listHeader.Add("Total By Rows");

            //add วันนี้ / สะสม
            for (int i = 0; i < listHeader.Count; i++)
            {
                listSubHeader.Add("วันนี้");
                listSubHeader.Add("สะสม");
            }

            //DateTime dateTime = DateTime.ParseExact("2018-06-09", "yyyy-MM-dd", CultureInfo.InvariantCulture);
            //string strDate = dateTime.ToString("dddd, dd MMMM yyyy");
            genExportExcelSaleQtyDaily("test", listHeader, listSubHeader, listOfDataArr, listZoneRow, strFullText);
            return "aaaa";
        }

        public void genExportExcelSaleQtyDaily(string fileName, List<string> headData, List<string> subHeader, List<string[]> bodyData, List<int> listZoneRow, string strDate)
        {

            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("Inserting Data");

                ws.Cell(3, 1).Value = "ลำดับ";
                ws.Cell(3, 2).Value = "สาขา";

                int col = 3;
                for (int i = 0; i < headData.Count; i++)
                {
                    ws.Cell(2, col).Value = headData[i];
                    ws.Range(2, col, 2, col + 1).Merge().AddToNamed("Titles");

                    col += 2;
                }

                for (int j = 0; j < subHeader.Count; j++)
                {
                    ws.Cell(3, j + 3).Value = subHeader[j];
                }

                ws.Cell(1, 1).Value = "รายงานยอดขายประจำวันที่ " + strDate;
                ws.Range(1, 1, 1, subHeader.Count + 2).Merge().AddToNamed("Header");

                var rangeWithArrays = ws.Cell(4, 1).InsertData(bodyData);
                var totalRows = ws.RowsUsed().Count();
                var totalCols = ws.ColumnsUsed().Count();

                //merge zone
                foreach (var item in listZoneRow)
                {
                    ws.Range(item + 4, 1, item + 4, 2).Merge();
                }
                ws.Range(totalRows, 1, totalRows, 2).Merge();

                ws.Range(1, 1, totalRows, totalCols).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Columns("B").AdjustToContents();

                //var rangeWithArrays = ws.Cell(2, 3).InsertData(listOfArr);
                //wb.Worksheets.Add(dt); 
                //wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //wb.Style.Font.Bold = true;
                //wb.Worksheets.FirstOrDefault().Tables.FirstOrDefault().ShowAutoFilter = false;

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment; filename=" + fileName + ".xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }

            }
        }

        public string ExportSaleQtyDaily2(string strFullFormat="", int iMonth = 0, int iYear = 0, string strFullText = "")
        {

            //call SP
            //SP_SaleQtyDailyByPaymentType
            DataTable dtPaymentType = new DataTable();
            using (var con = new SqlConnection(conStr))
            {
                using (var cmd = new SqlCommand("SP_SaleQtyDaily2ByPaymentType", con))
                {
                    cmd.Parameters.Add("@booking_date", SqlDbType.VarChar);
                    cmd.Parameters["@booking_date"].Value = strFullFormat;

                    cmd.Parameters.Add("@month", SqlDbType.Int);
                    cmd.Parameters["@month"].Value = iMonth;

                    cmd.Parameters.Add("@year", SqlDbType.Int);
                    cmd.Parameters["@year"].Value = iYear;
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        da.Fill(dtPaymentType);
                    }
                }
            }
             

            //listZone
            DataView vZone = new DataView(dtPaymentType);
            DataTable dtZone = vZone.ToTable(true, "zone_id", "zone_name");

            List<string> listHeader1 = new List<string>();
            listHeader1.Add("ลำดับ");
            listHeader1.Add("สาขา");
            listHeader1.Add("เป้ารวม");
            listHeader1.Add("ขายรถ");
            listHeader1.Add("คาดการณ์");

            List<string> listHeader2 = new List<string>();
            listHeader2.Add("เช่าซื้อ");
            listHeader2.Add("สด,เชื่อ");
            listHeader2.Add("รวม");
            listHeader2.Add("ยอดขาย");

            List<string> listHeader3 = new List<string>();
            listHeader3.Add("ชื่อ");
            listHeader3.Add("เป้า1");
            listHeader3.Add("เป้า2");
            listHeader3.Add("วันนี้");
            listHeader3.Add("สะสม");
            listHeader3.Add("วันนี้");
            listHeader3.Add("สะสม");
            listHeader3.Add("วันนี้");
            listHeader3.Add("สะสม");
            listHeader3.Add("ภายในเดือนนี้");
             
            
            // Data of list of string arrays 
            var listOfDataArr = new List<string[]>();
            int lastZoneRow = 0;

            // list row zone not include to sum
            List<int> listZoneRow = new List<int>();

            //list summary
            List<string> listSummary = new List<string>();
            ////add zone name
            listSummary.Add("รวมทั้งสิ้น");
            listSummary.Add("xx");

            foreach (DataRow rowZone in dtZone.Rows)
            {
                var zone_id = rowZone["zone_id"].ToString();
                var zone_name = rowZone["zone_name"].ToString();

                List<string> listDataZoneRow = new List<string>();
                ////add zone name
                listDataZoneRow.Add(zone_name);
                listDataZoneRow.Add("xx");

                //ลำดับ
                int row = 1;
                foreach (DataRow rowPaymentType in dtPaymentType.Rows)
                {
                    var booking_date = rowPaymentType["booking_date"].ToString();
                    int branch_id = Convert.ToInt32(rowPaymentType["branch_id"].ToString());
                    var zoneName = rowPaymentType["zone_name"].ToString();
                    var zoneId = rowPaymentType["zone_id"].ToString();
                    var branchName = rowPaymentType["branch_name"].ToString();
                    var othersell_qty = Convert.ToInt32(rowPaymentType["othersell_qty"].ToString());
                    var othersell_qty_acc = Convert.ToInt32(rowPaymentType["othersell_qty_acc"].ToString());
                    var installment_qty = Convert.ToInt32(rowPaymentType["installment_qty"].ToString());
                    var installment_qty_acc = Convert.ToInt32(rowPaymentType["installment_qty_acc"].ToString()); 
                    //check zone
                    if (zone_id == zoneId)
                    {  

                        //Data Each Row
                        List<string> listDataRow = new List<string>();

                        listDataRow.Add(row.ToString());
                        listDataRow.Add(branchName);

                        //data เป้า1
                        listDataRow.Add("0");
                        //data เป้า2
                        listDataRow.Add("0");

                        //data installment_qty, installment_qty_acc
                        listDataRow.Add(installment_qty.ToString());
                        listDataRow.Add(installment_qty_acc.ToString());

                        //data othersell_qty
                        listDataRow.Add(othersell_qty.ToString());
                        listDataRow.Add(othersell_qty_acc.ToString());

                        //data รวม วันนี้
                        int sum_qty = installment_qty + othersell_qty;
                        listDataRow.Add(sum_qty.ToString());

                        //data รวม สะสม
                        int sum_qty_acc = installment_qty_acc + othersell_qty_acc;
                        listDataRow.Add(sum_qty_acc.ToString());

                        //คาดการณ์ยอดขายภายในเดือนนี้
                        listDataRow.Add("0");
                        //sumTotalByRows += installment_qty + installment_qty_acc + othersell_qty + othersell_qty_acc; 
                         
                        string[] arrDataRow = listDataRow.ToArray();
                        listOfDataArr.Add(arrDataRow);
                        row++;
                    }
                }

                //รวมแต่ละ zone
                if (listOfDataArr.Count() > 0)
                {
                    //sum zone by each col

                    for (int col = 2; col < listOfDataArr[0].Count(); col++)
                    {
                        var sumQty = 0;
                        for (int listRow = lastZoneRow; listRow < listOfDataArr.Count; listRow++)
                        {
                            var qty = listOfDataArr[listRow][col];
                            sumQty += Convert.ToInt32(qty);
                        }
                        listDataZoneRow.Add(sumQty.ToString());
                    }
                    string[] arrDataZoneRow = listDataZoneRow.ToArray();
                    listOfDataArr.Add(arrDataZoneRow);

                    lastZoneRow = listOfDataArr.Count;

                    //store last zone row index
                    listZoneRow.Add(lastZoneRow - 1);
                }

            }

            //Summary
            //รวทั้งสิ้น
            if (listOfDataArr.Count() > 0)
            {
                //sum zone by each col

                for (int col = 2; col < listOfDataArr[0].Count(); col++)
                {
                    var sumQty = 0;
                    foreach (var listRow in listZoneRow)
                    {
                        var qty = listOfDataArr[listRow][col];
                        sumQty += Convert.ToInt32(qty);
                    }

                    listSummary.Add(sumQty.ToString());
                }
                string[] arrDataSummaryZoneRow = listSummary.ToArray();
                listOfDataArr.Add(arrDataSummaryZoneRow);
            } 

            //DateTime dateTime = DateTime.ParseExact("2018-06-09", "yyyy-MM-dd", CultureInfo.InvariantCulture);
            //string strDate = dateTime.ToString("dddd, dd MMMM yyyy");

            string title = "บริษัท เกริกไกรเอ็นเทอร์ไพรส์ จำกัด";
            string subTitle = "รายงานรยอดขายประจำวัน DAILY SALE REPORT "+ strFullText;

            genExportExcelSaleQtyDaily2("test", listHeader1, listHeader2, listHeader3, listOfDataArr, listZoneRow, title, subTitle);
            return "aaaa";
        }

        public void genExportExcelSaleQtyDaily2(string fileName, List<string> listHeader1, List<string> listHeader2, List<string> listHeader3, List<string[]> bodyData, List<int> listZoneRow, string title, string subTitle)
        {

            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("Inserting Data");
                //add title
                ws.Cell(1, 1).Value = title;
                ws.Range(1, 1, 1, 11).Merge();

                //add subTitle
                ws.Cell(2, 1).Value = subTitle;
                ws.Range(2, 1, 2, 11).Merge();

                ////col start
                int colHeader1 = 1;
                for (int i = 0; i < listHeader1.Count; i++)
                {
                    ws.Cell(3, colHeader1).Value = listHeader1[i];
                    //merge cell
                    if (listHeader1[i] == "ลำดับ")
                    {
                        ws.Range(3, 1, 5, 1).Merge();
                    }
                    else if (listHeader1[i] == "สาขา")
                    {
                        ws.Range(3, 2, 4, 2).Merge();
                    }
                    else if (listHeader1[i] == "เป้ารวม")
                    {
                        ws.Range(3, 3, 4, 4).Merge();
                        colHeader1 = 4;
                    }
                    else if (listHeader1[i] == "ขายรถ")
                    {
                        ws.Range(3, 5, 3, 10).Merge();
                        colHeader1 = 10;
                    }
                    colHeader1++;
                }

                //col start
                int colHeader2 = 5;
                for (int i = 0; i < listHeader2.Count; i++)
                {
                    ws.Cell(4, colHeader2).Value = listHeader2[i];
                    if (listHeader2[i] != "ยอดขาย")
                    {
                        ws.Range(4, colHeader2, 4, colHeader2 + 1).Merge();
                    }
                    colHeader2 += 2;
                }

                //col start
                int colHeader3 = 2;
                for (int i = 0; i < listHeader3.Count; i++)
                {
                    ws.Cell(5, colHeader3).Value = listHeader3[i];
                    colHeader3++;
                }


                var rangeWithArrays = ws.Cell(6, 1).InsertData(bodyData);
                var totalRows = ws.RowsUsed().Count();
                var totalCols = ws.ColumnsUsed().Count();

                //merge zone
                foreach (var item in listZoneRow)
                {
                    ws.Range(item + 6, 1, item + 6, 2).Merge();
                }
                ws.Range(totalRows, 1, totalRows, 2).Merge();

                ws.Range(1, 1, totalRows, totalCols).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Columns("B").AdjustToContents();
                ws.Columns("K").AdjustToContents();
                
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment; filename=" + fileName + ".xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }

            }
        }

        private void ExportSummaryDepositByDate(string strStartDate = "", string strEndDate = "")
        {
            try
            {
                //var sDate = DateTime.ParseExact(strStartDate,
                //                  "yyyy-MM-ddTHH:mm:ss.fffZ",
                //                   CultureInfo.InvariantCulture);
                //var eDate = DateTime.ParseExact(strEndDate,
                //                  "yyyy-MM-ddTHH:mm:ss.fffZ",
                //                   CultureInfo.InvariantCulture);

                var startDateTime = DateTime.ParseExact(strStartDate,
                                 "yyyy-MM-ddTHH:mm:ss.fffZ",
                                  CultureInfo.InvariantCulture);
                var endDateTime = DateTime.ParseExact(strEndDate,
                                 "yyyy-MM-ddTHH:mm:ss.fffZ",
                                  CultureInfo.InvariantCulture);

                string startDepositDate = startDateTime.Year + "-" + string.Format("{0:00}", startDateTime.Month) + "-" + string.Format("{0:00}", startDateTime.Day);

                string EndDepositDate = endDateTime.Year + "-" + string.Format("{0:00}", endDateTime.Month) + "-" + string.Format("{0:00}", endDateTime.Day);

                rptDoc = new ReportDocument();
                SqlConnectionStringBuilder connection = new SqlConnectionStringBuilder(conStr);
                var server = connection.DataSource;

                var file = "../ALL/SummaryDepositByDate.rpt";
                rptDoc.Load(Server.MapPath(file));
                rptDoc.Refresh();

                TableLogOnInfo L1 = rptDoc.Database.Tables[0].LogOnInfo;
                GetLoginfo(L1, server); 
                //rptDoc.SetParameterValue("@sDate", sDate);
                //rptDoc.SetParameterValue("@eDate", eDate);
                rptDoc.Database.Tables[0].ApplyLogOnInfo(L1);

                //rptDoc.SetParameterValue("@booking_id", 2, "Subreport3"); 
                //rptDoc.Subreports["Subreport1"].Database.Tables[0].ApplyLogOnInfo(L1);

                StreamPdfReport(rptDoc, "test");
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        private void ExportContractGradePayment()
        {
            try
            { 
                rptDoc = new ReportDocument();
                SqlConnectionStringBuilder connection = new SqlConnectionStringBuilder(conStr);
                var server = connection.DataSource;

                var file = "../HPS/ContractGradePayment.rpt";
                rptDoc.Load(Server.MapPath(file));
                rptDoc.Refresh();

                TableLogOnInfo L1 = rptDoc.Database.Tables[0].LogOnInfo;
                GetLoginfo(L1, server);
                //rptDoc.SetParameterValue("@sDate", sDate);
                //rptDoc.SetParameterValue("@eDate", eDate);
                rptDoc.Database.Tables[0].ApplyLogOnInfo(L1); 

                StreamPdfReport(rptDoc, "test");
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        //private void GetLoginfo(TableLogOnInfo Log, string server)
        //{
        //    Log.ConnectionInfo.ServerName = server;
        //    Log.ConnectionInfo.UserID = "sa_report";
        //    Log.ConnectionInfo.Password = "1234";
        //    //Log.ConnectionInfo.UserID = "sa";
        //    //Log.ConnectionInfo.Password = "sql@1234";
        //    Log.ConnectionInfo.DatabaseName = "";
        //}

        private void GetLoginfo(TableLogOnInfo Log, string server)
        {
            SqlConnectionStringBuilder connection = new SqlConnectionStringBuilder(conStr);

            Log.ConnectionInfo.ServerName = connection.DataSource;
            Log.ConnectionInfo.UserID = connection.UserID;
            Log.ConnectionInfo.Password = connection.Password;
            Log.ConnectionInfo.DatabaseName = "";
        }

    }
}