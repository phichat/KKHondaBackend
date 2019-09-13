using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KKHondaReport.RIS
{
    public partial class index : System.Web.UI.Page
    {
        private string conStr = ConfigurationManager.ConnectionStrings["KKMssql"].ConnectionString;
        private SqlConnection conn = null;
        private ReportDocument rptDoc;

        protected void Page_Load(object sender, EventArgs e)
        {
            var sedNo = string.Empty;
            var clNo = string.Empty;
            var alNo = string.Empty;
            var userId = string.Empty;

            if (Request.QueryString["sedNo"] != null)
            {
                sedNo = Request.QueryString["sedNo"];
            }
            if (Request.QueryString["userId"] != null)
            {
                userId = Request.QueryString["userId"];
            }
            if (Request.QueryString["clNo"] != null)
            {
                clNo = Request.QueryString["clNo"];
            }
            if (Request.QueryString["alNo"] != null)
            {
                alNo = Request.QueryString["alNo"];
            }

            if (Request.QueryString["formSed"] != null)
            {
                ExportSED(sedNo);
            }

            if (Request.QueryString["formCl"] != null)
            {
                ExportCL(clNo, userId);
            }

            if (Request.QueryString["formAl"] != null)
            {
                ExportAL(alNo, userId);
            }

            if (Request.QueryString["formRegisTag"] != null)
            {
                var sdate = Request.QueryString["sDate"].ToString();
                var edate = Request.QueryString["eDate"].ToString();
                ExportRegisTag(DateTime.Parse(sdate), DateTime.Parse(edate));
            }

            if (Request.QueryString["formRegisVehicleTax"] != null)
            {
                var sdate = Request.QueryString["sDate"].ToString();
                var edate = Request.QueryString["eDate"].ToString();
                ExportRegisVehicleTax(DateTime.Parse(sdate), DateTime.Parse(edate));
            }
            
        }

        private void ExportSED(string sedNo)
        {
            try
            {
                rptDoc = new ReportDocument();
                SqlConnectionStringBuilder connection = new SqlConnectionStringBuilder(conStr);
                var server = connection.DataSource;
                var userid = connection.UserID;
                var pass = connection.Password;
                var database = connection.InitialCatalog;

                var file = "./SED.rpt";
                rptDoc.Load(Server.MapPath(file));
                rptDoc.Refresh();

                TableLogOnInfo L1 = rptDoc.Database.Tables[0].LogOnInfo;
                GetLoginfo(L1, server, userid, pass, database);
                rptDoc.SetParameterValue("@sed_no", sedNo);
                rptDoc.Database.Tables[0].ApplyLogOnInfo(L1);

                StreamPdfReport(rptDoc, "sed-doc.pdf");
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        private void ExportCL(string clNo, string userId)
        {
            try
            {
                //RIS/index.aspx?alNo=AL026209/0001&userId=4111&formAl=true
                rptDoc = new ReportDocument();
                SqlConnectionStringBuilder connection = new SqlConnectionStringBuilder(conStr);
                var server = connection.DataSource;
                var userid = connection.UserID;
                var pass = connection.Password;
                var database = connection.InitialCatalog;

                var file = "./RegisCL.rpt";
                rptDoc.Load(Server.MapPath(file));
                rptDoc.Refresh();

                TableLogOnInfo L1 = rptDoc.Database.Tables[0].LogOnInfo;
                GetLoginfo(L1, server, userid, pass, database);
                rptDoc.SetParameterValue("@user_id", userId);
                rptDoc.SetParameterValue("@cl_no", clNo);
                rptDoc.Database.Tables[0].ApplyLogOnInfo(L1);

                StreamPdfReport(rptDoc, "cl-doc.pdf");
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        private void ExportAL(string alNo, string userId)
        {
            try
            {
                //RIS/index.aspx?clNo=CL016209/0001&userId=4111&formCl=true
                rptDoc = new ReportDocument();
                SqlConnectionStringBuilder connection = new SqlConnectionStringBuilder(conStr);
                var server = connection.DataSource;
                var userid = connection.UserID;
                var pass = connection.Password;
                var database = connection.InitialCatalog;

                var file = "./RegisAL.rpt";
                rptDoc.Load(Server.MapPath(file));
                rptDoc.Refresh();

                TableLogOnInfo L1 = rptDoc.Database.Tables[0].LogOnInfo;

                GetLoginfo(L1, server, userid, pass, database);
                rptDoc.SetParameterValue("@user_id", userId);
                rptDoc.SetParameterValue("@al_no", alNo);
                rptDoc.Database.Tables[0].ApplyLogOnInfo(L1);

                StreamPdfReport(rptDoc, "al-doc.pdf");
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        private void ExportRegisTag(DateTime sdate, DateTime edate)
        {
            try
            {
                //RIS/index.aspx?sDate=2019-09-11&eDate=2019-09-12&formRegisTag=true
                rptDoc = new ReportDocument();
                SqlConnectionStringBuilder connection = new SqlConnectionStringBuilder(conStr);
                var server = connection.DataSource;
                var userid = connection.UserID;
                var pass = connection.Password;
                var database = connection.InitialCatalog;

                var file = "./RegisTag.rpt";
                rptDoc.Load(Server.MapPath(file));
                rptDoc.Refresh();

                TableLogOnInfo L1 = rptDoc.Database.Tables[0].LogOnInfo;
                GetLoginfo(L1, server, userid, pass, database);
                rptDoc.SetParameterValue("@start_sell_date", sdate.ToString("yyyy-MM-dd"));
                rptDoc.SetParameterValue("@end_sell_date", edate.ToString("yyyy-MM-dd"));
                rptDoc.Database.Tables[0].ApplyLogOnInfo(L1);

                StreamXlsReport(rptDoc, "regis-tag-doc.xls");
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        private void ExportRegisVehicleTax(DateTime sdate, DateTime edate)
        {
            try
            {
                //RIS/index.aspx?sDate=2019-09-11&eDate=2019-09-12&formRegisVehicleTax=true
                rptDoc = new ReportDocument();
                SqlConnectionStringBuilder connection = new SqlConnectionStringBuilder(conStr);
                var server = connection.DataSource;
                var userid = connection.UserID;
                var pass = connection.Password;
                var database = connection.InitialCatalog;

                var file = "./RegisVehicleTax.rpt";
                rptDoc.Load(Server.MapPath(file));
                rptDoc.Refresh();

                TableLogOnInfo L1 = rptDoc.Database.Tables[0].LogOnInfo;
                GetLoginfo(L1, server, userid, pass, database);
                rptDoc.SetParameterValue("@start_receive_date", sdate.ToString("yyyy-MM-dd"));
                rptDoc.SetParameterValue("@end_receive_date", edate.ToString("yyyy-MM-dd"));
                rptDoc.Database.Tables[0].ApplyLogOnInfo(L1);

                StreamXlsReport(rptDoc, "RegisVehicleTax.xls");
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        private void StreamXlsReport(ReportDocument rptDoc, string fileName)
        {
            using (MemoryStream oStream = new MemoryStream())
            {
                rptDoc.ExportToStream(ExportFormatType.Excel).CopyTo(oStream);
                Response.Clear();
                Response.Buffer = true;
                Response.AppendHeader("Content-Disposition", $"attachment; filename={fileName}");
                //Response.ContentType = "application/pdf";
                Response.BinaryWrite(oStream.ToArray());
                Response.End();
                rptDoc.Close();
                rptDoc.Dispose();
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

        private void GetLoginfo(TableLogOnInfo Log, string server, string userId, string password, string database)
        {
            Log.ConnectionInfo.ServerName = server;
            Log.ConnectionInfo.UserID = userId;
            Log.ConnectionInfo.Password = password;
            Log.ConnectionInfo.DatabaseName = database;
        }
    }
}