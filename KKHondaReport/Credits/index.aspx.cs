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

namespace KKHondaReport.Contracts
{
    public partial class index : System.Web.UI.Page
    {
        private string conStr = ConfigurationManager.ConnectionStrings["KKMssql"].ConnectionString;
        private SqlConnection conn = null;
        private ReportDocument rptDoc;

        protected void Page_Load(object sender, EventArgs e)
        {
            var contractId = int.Parse(Request.QueryString["contractId"]);
            if (Request.QueryString["formContract"] != null)
            {
                if (Boolean.Parse(Request.QueryString["formContract"]) == true)
                    ExportFormatContract(contractId);
            }

            if (Request.QueryString["formInstalmentTerm"] != null)
            {
                if (Boolean.Parse(Request.QueryString["formInstalmentTerm"]) == true)
                    ExportFormInstalmentTerm(contractId);
            }

            if (Request.QueryString["formTransfer"] != null)
            {
                //if (Boolean.Parse(Request.QueryString["formTransfer"]) == true)
                    //ExportFormTransfer(contractId);
            }

            // -------- Report Sum -------- //

            if (Request.QueryString["sumCutOffCash"] != null)
            {
                if (Boolean.Parse(Request.QueryString["sumCutOffCash"]) == true)
                    ExportSum(contractId, "./sumCutOffCash.rpt", "cut-off-sale-doc.pdf");
            }

            if (Request.QueryString["sumDiscountByTerm"] != null)
            {
                if (Boolean.Parse(Request.QueryString["sumDiscountByTerm"]) == true)
                    ExportSum(contractId, "./sumDiscountByTerm.rpt", "cut-off-sale-doc.pdf");
            }

            if (Request.QueryString["sumInterestDelay"] != null)
            {
                if (Boolean.Parse(Request.QueryString["sumInterestDelay"]) == true)
                    ExportSum(contractId, "./sumInterestDelay.rpt", "interest-delayed-doc.pdf");
            }

            if (Request.QueryString["sumOutstanding"] != null)
            {
                if (Boolean.Parse(Request.QueryString["sumOutstanding"]) == true)
                    ExportSum(contractId, "./sumOutstanding.rpt", "outstanding-doc.pdf");
            }

            if (Request.QueryString["sumPaymentHistory"] != null)
            {
                if (Boolean.Parse(Request.QueryString["sumPaymentHistory"]) == true)
                    ExportSum(contractId, "./sumPaymentHistory.rpt", "history-payment-doc.pdf");
            }
        }

        private void ExportFormatContract(int contractId)
        {
            try
            {
                rptDoc = new ReportDocument();
                SqlConnectionStringBuilder connection = new SqlConnectionStringBuilder(conStr);
                var server = connection.DataSource;

                var file = "./formContract.rpt";
                rptDoc.Load(Server.MapPath(file));
                rptDoc.Refresh();

                TableLogOnInfo L1 = rptDoc.Database.Tables[0].LogOnInfo;
                GetLoginfo(L1, server);
                rptDoc.SetParameterValue("@ContractId", contractId);
                rptDoc.Database.Tables[0].ApplyLogOnInfo(L1);

                StreamPdfReport(rptDoc, "contract-doc.pdf");
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        private void ExportFormInstalmentTerm(int contractId)
        {
            rptDoc = new ReportDocument();

            try
            {
                SqlConnectionStringBuilder connection = new SqlConnectionStringBuilder(conStr);
                var server = connection.DataSource;
                
                var file = "./formInstalmentTerm.rpt";
                rptDoc.Load(Server.MapPath(file));
                rptDoc.Refresh();

                TableLogOnInfo L1 = rptDoc.Subreports["section1"].Database.Tables[0].LogOnInfo;
                GetLoginfo(L1, server);
                rptDoc.SetParameterValue("@ContractId", contractId, "section1");
                rptDoc.SetParameterValue("@section_group", 1, "section1");
                rptDoc.Subreports["section1"].Database.Tables[0].ApplyLogOnInfo(L1);

                TableLogOnInfo L2 = rptDoc.Subreports["section1"].Database.Tables[0].LogOnInfo;
                GetLoginfo(L2, server);
                rptDoc.SetParameterValue("@ContractId", contractId, "section2");
                rptDoc.SetParameterValue("@section_group", 2, "section2");
                rptDoc.Subreports["section2"].Database.Tables[0].ApplyLogOnInfo(L2);

                TableLogOnInfo L3 = rptDoc.Subreports["section1"].Database.Tables[0].LogOnInfo;
                GetLoginfo(L3, server);
                rptDoc.SetParameterValue("@ContractId", contractId, "section3(customer)");
                rptDoc.Subreports["section3(customer)"].Database.Tables[0].ApplyLogOnInfo(L3);

                StreamPdfReport(rptDoc, "instalment-card.pdf");
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        private void ExportFormTransfer(int contractId)
        {
            conn = new SqlConnection(conStr);
            var cmd = new SqlCommand();
            var dt = new DataTable();
            var da = new SqlDataAdapter();
            rptDoc = new ReportDocument();

            try
            {
                conn.Open();

                cmd.CommandText = "";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ContractId", contractId);
                cmd.Connection = conn;
                cmd.CommandTimeout = 120;

                da.SelectCommand = cmd;
                da.Fill(dt);

                var file = "./formTransfer.rpt";
                rptDoc.Load(Server.MapPath(file));
                rptDoc.SetDataSource(dt);
                rptDoc.SetParameterValue("@ContractId", contractId);
                //StreamPdfReport(rptDoc);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void ExportSum(int contractId, string file, string fileName)
        {
            try
            {
                rptDoc = new ReportDocument();
                SqlConnectionStringBuilder connection = new SqlConnectionStringBuilder(conStr);
                var server = connection.DataSource;
                
                rptDoc.Load(Server.MapPath(file));
                rptDoc.Refresh();

                TableLogOnInfo L1 = rptDoc.Database.Tables[0].LogOnInfo;
                GetLoginfo(L1, server);
                rptDoc.SetParameterValue("@ContractId", contractId);
                rptDoc.Database.Tables[0].ApplyLogOnInfo(L1);

                StreamPdfReport(rptDoc, fileName);
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

        private void GetLoginfo(TableLogOnInfo Log, string server)
        {
            Log.ConnectionInfo.ServerName = server;
            Log.ConnectionInfo.UserID = "sa";
            Log.ConnectionInfo.Password = "sql@2012";
            Log.ConnectionInfo.DatabaseName = "";
        }

    }
}