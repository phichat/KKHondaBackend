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
using System.Globalization;

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

                    if(branchType == "")
                    {
                        branchId = "0";
                    }

                    if(brandType == "")
                    {
                        brandTypeId = "0";
                        version = "0";
                        design = "0";
                        color = "0";
                    }

                    if(bookingName == "")
                    {
                        strBookingName = "";
                        strRegisName = "";
                    }

                    if(bookingStatus == "")
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
                    //if (bookingDate == "2")
                    //{
                    //if(sDate != "")
                    //{
                    //DateTime dtStartDate = DateTime.ParseExact(sDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    //sDate = dtStartDate.ToString("yyyy-MM-dd");
                    //string[] sa = sDate.Split('/');
                    //sDate = sa[2] + "-" + sa[1] + "-" + sa[0];
                    //}

                    //if (eDate != "")
                    //{
                    //DateTime dtEndDate = DateTime.ParseExact(eDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    //eDate = dtEndDate.ToString("yyyy-MM-dd");

                    //    string[] sa = eDate.Split('/');
                    //    eDate = sa[2] + "-" + sa[1] + "-" + sa[0];
                    //}

                    //}

                    //if(bookingReceiveDate == "2")
                    //{
                    //if (sBookingReceiveDate != "")
                    //{
                    //DateTime dtBookingReceiveStartDate = DateTime.ParseExact(sBookingReceiveDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    //sBookingReceiveDate = dtBookingReceiveStartDate.ToString("yyyy-MM-dd");

                    //    string[] sa = sBookingReceiveDate.Split('/');
                    //    sBookingReceiveDate = sa[2] + "-" + sa[1] + "-" + sa[0];
                    //}

                    //if (eBookingReceiveDate != "")
                    //{
                    //DateTime dtBookingReceiveEndDate = DateTime.ParseExact(eBookingReceiveDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    //eBookingReceiveDate = dtBookingReceiveEndDate.ToString("yyyy-MM-dd");

                    //    string[] sa = eBookingReceiveDate.Split('/');
                    //    eBookingReceiveDate = sa[2] + "-" + sa[1] + "-" + sa[0];
                    //}
                    //}
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
                    if(branchType == "")
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

                    if(isSellName == "")
                    {
                        SellId = "0";
                    }

                    if(isPaymentType == "")
                    {
                        paymentTypeId = "0";
                    }
                    
                    if(isSellDate == "")
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
        }

        private void ExportFormatBookingReport(string branchType = "", string branchId = "", string brandType = "",string brandTypeId = "",string version = "",string design = "",string color = "",string bookingName = "",string strBookingName = "", string strRegisName = "", string bookingStatus = "", string bookingDate = "", string sDate = "", string eDate = "", string bookingReceiveDate = "", string sBookingReceiveDate = "",string eBookingReceiveDate = "")
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
                var sDate = DateTime.ParseExact("2018-06-05",
                                  "yyyy-MM-dd",
                                   CultureInfo.InvariantCulture);
                var eDate = DateTime.ParseExact("2018-09-15",
                                  "yyyy-MM-dd",
                                   CultureInfo.InvariantCulture);

                //var sDate = DateTime.ParseExact(strStartDate,
                //                  "yyyy-MM-dd",
                //                   CultureInfo.InvariantCulture);
                //var eDate = DateTime.ParseExact(strEndDate,
                //                  "yyyy-MM-dd",
                //                   CultureInfo.InvariantCulture);

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

                var startReceiveDate = DateTime.ParseExact(strStartDate,
                                  "yyyy-MM-dd",
                                   CultureInfo.InvariantCulture);
                var EndReceiveDate = DateTime.ParseExact(strEndDate,
                                 "yyyy-MM-dd",
                                  CultureInfo.InvariantCulture);

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

        private void ExportCreditNote(string branch_ids = "",string sdate = "",string edate = "", string book_person = "")
        {
            try
            {
                var displayStartDate = "";
                if(sdate != "")
                {
                    var strStartDate = sdate.Split('T')[0];
                    DateTime dateTimeStartDate = DateTime.ParseExact(strStartDate,
                                  "yyyy-MM-dd",
                                   CultureInfo.InvariantCulture);
                    displayStartDate = dateTimeStartDate.ToString("dd/MM/yyyy");
                }

                var displayEndDate = "";
                if (edate != "")
                {
                    var strEndDate = edate.Split('T')[0];
                    DateTime dateTimeEndDate = DateTime.ParseExact(strEndDate,
                                  "yyyy-MM-dd",
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

        public string getResult()
        {
            return "aaaa";
        }

        //private void ExportTestReport()
        //{
        //    try
        //    {
        //        rptDoc = new ReportDocument();
        //        SqlConnectionStringBuilder connection = new SqlConnectionStringBuilder(conStr);
        //        var server = connection.DataSource;

        //        var file = "./testReport.rpt";
        //        rptDoc.Load(Server.MapPath(file));
        //        rptDoc.Refresh();

        //        TableLogOnInfo L1 = rptDoc.Database.Tables[0].LogOnInfo;
        //        GetLoginfo(L1, server);
        //        //rptDoc.SetParameterValue("@branch_id", 1);
        //        //rptDoc.SetParameterValue("strBookingType", "test string Booking Type");
        //        //rptDoc.Database.Tables[0].ApplyLogOnInfo(L1);

        //        //rptDoc.SetParameterValue("@booking_id", 2, "Subreport3"); 
        //        //rptDoc.Subreports["Subreport1"].Database.Tables[0].ApplyLogOnInfo(L1);

        //        //test query
        //        DataTable table = new DataTable();
        //        using (SqlConnection conn = new SqlConnection(conStr))
        //        {
        //            using (SqlCommand cmd = new SqlCommand("SELECT * FROM _branch where branch_id = 1", conn))
        //            {
        //                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
        //                {
        //                    adapter.Fill(table);
        //                }
        //            }
        //        }
        //        rptDoc.Database.Tables[0].SetDataSource(table);

        //        StreamPdfReport(rptDoc, "test");
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write(ex.Message);
        //    }
        //}
        private void GetLoginfo(TableLogOnInfo Log, string server)
        {
            Log.ConnectionInfo.ServerName = server;
            Log.ConnectionInfo.UserID = "sa";
            Log.ConnectionInfo.Password = "Krirkkai@2012";
            //Log.ConnectionInfo.UserID = "sa";
            //Log.ConnectionInfo.Password = "sql@1234";
            Log.ConnectionInfo.DatabaseName = "";
        }
    }
}