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
using KKHondaReport.Models;
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
            //ExportFormatBookingReport();

            //report 4. รายงานยอดขายรวม
            //ExportSummarySaleReport();

            //report 7.รายงานสรุปยอดขายแยกประเภท
            //ExportSummarySaleReportByType();

            //report 8.รายงานสินค้าคงเหลือ
            //ExportSummaryStockBalance();

            //report 2. รายงานการรับคืนสินค้า (แบบละเอียด)
            //ExportCreditNote();


            //report 17. รายงานยอดขายสินค้า - แสดงกำไร
            //ExportProductSellingProfit();

            //report 28. รายงานการลดหนี้
            //ExportCreditNoteDetailsReport();


            //report 31. รายงานสินค้าคงเหลือ
            //ExportStockBalanceMain();


            //report 19. รายงานการับคืนเงินมัดจำ
            //ExportSummaryDepositReport();
            if (Request.QueryString["BookingReport"] != null)
            {
                if (Boolean.Parse(Request.QueryString["BookingReport"]) == true)
                {
                    string branchType = Request.QueryString["branchType"];
                    string branchId = Request.QueryString["branchId"];
                    string brandType = Request.QueryString["brandType"];
                    string brandTypeId = Request.QueryString["brandTypeId"];
                    string version = Request.QueryString["version"];
                    string design = Request.QueryString["design"];
                    string color = Request.QueryString["color"];
                    string bookingName = Request.QueryString["bookingName"];
                    string strBookingName = Request.QueryString["bookingNameId"];
                    string strRegisName = Request.QueryString["regisNameId"];
                    string bookingStatus = Request.QueryString["bookingStatus"];
                    string bookingStatusId = Request.QueryString["bookingStatusId"];
                    string bookingDate = Request.QueryString["bookingDate"];
                    string sDate = Request.QueryString["sDate"];
                    string eDate = Request.QueryString["eDate"];
                    string bookingReceiveDate = Request.QueryString["bookingReceiveDate"];
                    string sBookingReceiveDate = Request.QueryString["sBookingReceiveDate"];
                    string eBookingReceiveDate = Request.QueryString["eBookingReceiveDate"];
                    if (bookingDate == "2")
                    {
                        if(sDate != "")
                        {
                            //DateTime dtStartDate = DateTime.ParseExact(sDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            //sDate = dtStartDate.ToString("yyyy-MM-dd");
                            string[] sa = sDate.Split('/');
                            sDate = sa[2] + "-" + sa[1] + "-" + sa[0];
                        }

                        if (eDate != "")
                        {
                            //DateTime dtEndDate = DateTime.ParseExact(eDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            //eDate = dtEndDate.ToString("yyyy-MM-dd");

                            string[] sa = eDate.Split('/');
                            eDate = sa[2] + "-" + sa[1] + "-" + sa[0];
                        }

                    }

                    if(bookingReceiveDate == "2")
                    {
                        if (sBookingReceiveDate != "")
                        {
                            //DateTime dtBookingReceiveStartDate = DateTime.ParseExact(sBookingReceiveDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            //sBookingReceiveDate = dtBookingReceiveStartDate.ToString("yyyy-MM-dd");

                            string[] sa = sBookingReceiveDate.Split('/');
                            sBookingReceiveDate = sa[2] + "-" + sa[1] + "-" + sa[0];
                        }

                        if (eBookingReceiveDate != "")
                        {
                            //DateTime dtBookingReceiveEndDate = DateTime.ParseExact(eBookingReceiveDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            //eBookingReceiveDate = dtBookingReceiveEndDate.ToString("yyyy-MM-dd");

                            string[] sa = eBookingReceiveDate.Split('/');
                            eBookingReceiveDate = sa[2] + "-" + sa[1] + "-" + sa[0];
                        }
                    }
                    ExportFormatBookingReport(branchType, branchId, brandType, brandTypeId, version, design, color, bookingName, strBookingName, strRegisName, bookingStatus, bookingDate, sDate, eDate, bookingReceiveDate, sBookingReceiveDate, eBookingReceiveDate);
                }
                   
            }

            if (Request.QueryString["SaleReport"] != null)
            {
                if (Boolean.Parse(Request.QueryString["SaleReport"]) == true)
                    ExportSummarySaleReport();
            }

            if (Request.QueryString["SaleReportByType"] != null)
            {
                if (Boolean.Parse(Request.QueryString["SaleReportByType"]) == true)
                {
                    string strStartDate = Request.QueryString["strStartDate"];
                    string strEndDate = Request.QueryString["strEndDate"];
                    ExportSummarySaleReportByType(strStartDate, strEndDate);
                }

            }

            if (Request.QueryString["StockBalance"] != null)
            {
                if (Boolean.Parse(Request.QueryString["StockBalance"]) == true)
                {
                    string strStartDate = Request.QueryString["strStartDate"];
                    string strEndDate = Request.QueryString["strEndDate"];

                    ExportSummaryStockBalance(strStartDate, strEndDate);
                }

            }

            if (Request.QueryString["CreditNote"] != null)
            {
                if (Boolean.Parse(Request.QueryString["CreditNote"]) == true)
                    ExportCreditNote();
            }

            if (Request.QueryString["ProductSellingProfit"] != null)
            {
                if (Boolean.Parse(Request.QueryString["ProductSellingProfit"]) == true)
                    ExportProductSellingProfit();
            }

            if (Request.QueryString["ServiceCheckReport"] != null)
            {
                if (Boolean.Parse(Request.QueryString["ServiceCheckReport"]) == true)
                    ExportServiceCheckReport();
            }

            if (Request.QueryString["StockBalanceMain"] != null)
            {
                if (Boolean.Parse(Request.QueryString["StockBalanceMain"]) == true)
                    ExportStockBalanceMain();
            }


            if (Request.QueryString["SummaryDepositReport"] != null)
            {
                if (Boolean.Parse(Request.QueryString["SummaryDepositReport"]) == true)
                    ExportSummaryDepositReport();
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

        private void ExportSummarySaleReport()
        {
            try
            {
                rptDoc = new ReportDocument();
                SqlConnectionStringBuilder connection = new SqlConnectionStringBuilder(conStr);
                var server = connection.DataSource;

                var file = "./formSummarySaleReport.rpt";
                rptDoc.Load(Server.MapPath(file));
                rptDoc.Refresh();

                TableLogOnInfo L1 = rptDoc.Database.Tables[0].LogOnInfo;
                GetLoginfo(L1, server);
                //rptDoc.SetParameterValue("@branch_id", 1);
                rptDoc.SetParameterValue("strBookingType", "test ExportSummarySaleReport");
                rptDoc.Database.Tables[0].ApplyLogOnInfo(L1);

                //rptDoc.SetParameterValue("@booking_id", 2, "Subreport3"); 
                rptDoc.Subreports["Subreport1"].Database.Tables[0].ApplyLogOnInfo(L1);

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

                var sDate = DateTime.ParseExact(strStartDate,
                                  "yyyy-MM-dd",
                                   CultureInfo.InvariantCulture);
                var eDate = DateTime.ParseExact(strEndDate,
                                  "yyyy-MM-dd",
                                   CultureInfo.InvariantCulture);

                rptDoc = new ReportDocument();
                SqlConnectionStringBuilder connection = new SqlConnectionStringBuilder(conStr);
                var server = connection.DataSource;

                var file = "../ALL/formSummarySaleReportByType.rpt";
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
                var startReceiveDate = DateTime.ParseExact(strStartDate,
                                  "yyyy-MM-dd",
                                   CultureInfo.InvariantCulture);
                var EndReceiveDate = DateTime.ParseExact(strEndDate,
                                 "yyyy-MM-dd",
                                  CultureInfo.InvariantCulture);

                rptDoc = new ReportDocument();
                SqlConnectionStringBuilder connection = new SqlConnectionStringBuilder(conStr);
                var server = connection.DataSource;

                var file = "./formSummaryStockBalance.rpt";
                rptDoc.Load(Server.MapPath(file));
                rptDoc.Refresh();

                TableLogOnInfo L1 = rptDoc.Database.Tables[0].LogOnInfo;
                GetLoginfo(L1, server);
                //rptDoc.SetParameterValue("@branch_id", 1);
                rptDoc.SetParameterValue("strReceiveDate", startReceiveDate); 
                rptDoc.Database.Tables[0].ApplyLogOnInfo(L1);

                rptDoc.SetParameterValue("@start_receive_date", startReceiveDate, "Subreport1");
                rptDoc.SetParameterValue("@end_receive_date", EndReceiveDate, "Subreport1");
                rptDoc.Subreports["Subreport1"].Database.Tables[0].ApplyLogOnInfo(L1);

                StreamPdfReport(rptDoc, "test");
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        private void ExportCreditNote()
        {
            try
            {
                var startCreditNoteDate = DateTime.ParseExact("2018-06-09",
                                  "yyyy-MM-dd",
                                   CultureInfo.InvariantCulture);
                var EndCreditNoteDate = DateTime.ParseExact("2018-12-09",
                                 "yyyy-MM-dd",
                                  CultureInfo.InvariantCulture).AddTicks(-1).AddDays(1);

                var DisplayEndCreditNoteDate = DateTime.ParseExact("2018-12-09",
                                 "yyyy-MM-dd",
                                  CultureInfo.InvariantCulture);



                rptDoc = new ReportDocument();
                SqlConnectionStringBuilder connection = new SqlConnectionStringBuilder(conStr);
                var server = connection.DataSource;

                var file = "./formCreditNote.rpt";
                rptDoc.Load(Server.MapPath(file));
                rptDoc.Refresh();

                TableLogOnInfo L1 = rptDoc.Database.Tables[0].LogOnInfo;
                GetLoginfo(L1, server);
                //rptDoc.SetParameterValue("@branch_id", 1);
                rptDoc.SetParameterValue("strStartCreditNoteDate", startCreditNoteDate);
                rptDoc.SetParameterValue("strEndCreditNoteDate", DisplayEndCreditNoteDate);
                rptDoc.Database.Tables[0].ApplyLogOnInfo(L1);

                rptDoc.SetParameterValue("@sDate", startCreditNoteDate, "Subreport1");
                rptDoc.SetParameterValue("@eDate", EndCreditNoteDate, "Subreport1");
                rptDoc.Subreports["Subreport1"].Database.Tables[0].ApplyLogOnInfo(L1);

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

                var file = "../PSS/formProductSellingProfitReport.rpt";
                rptDoc.Load(Server.MapPath(file));
                rptDoc.Refresh();

                TableLogOnInfo L1 = rptDoc.Database.Tables[0].LogOnInfo;
                GetLoginfo(L1, server);
                //rptDoc.SetParameterValue("@branch_id", 1);
                rptDoc.SetParameterValue("strBookingType", "test string Booking Type");
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

                var file = "../PSS/formServiceCheckReport.rpt";
                rptDoc.Load(Server.MapPath(file));
                rptDoc.Refresh();

                TableLogOnInfo L1 = rptDoc.Database.Tables[0].LogOnInfo;
                GetLoginfo(L1, server);
                //rptDoc.SetParameterValue("@branch_id", 1);
                rptDoc.SetParameterValue("strBookingType", "test string Booking Type");
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
            Log.ConnectionInfo.UserID = "sa_report";
            Log.ConnectionInfo.Password = "1234";
            //Log.ConnectionInfo.UserID = "sa";
            //Log.ConnectionInfo.Password = "sql@1234";
            Log.ConnectionInfo.DatabaseName = "";
        }
    }
}