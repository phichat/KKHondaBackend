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
            if (Boolean.Parse(Request.QueryString["formContract"]) == true)
            {

            }

            if (Boolean.Parse(Request.QueryString["formInstalmentTerm"]) == true)
            {
                ExportFormInstalmentTerm(contractId);
            }

            if (Boolean.Parse(Request.QueryString["formTransfer"]) == true)
            {

            }
        }

        private void ExportFormatContract(int contractId)
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

                var file = "./formContract.rpt";
                rptDoc.Load(Server.MapPath(file));
                rptDoc.SetDataSource(dt);
                rptDoc.SetParameterValue("@ContractId", contractId);
                StreamPdfReport(rptDoc);
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

        private void ExportFormInstalmentTerm(int contractId)
        {
            conn = new SqlConnection(conStr);
            var cmd = new SqlCommand();
            var dt = new DataTable();
            var da = new SqlDataAdapter();
            rptDoc = new ReportDocument();

            try
            {
                SqlConnectionStringBuilder connection = new SqlConnectionStringBuilder(conStr);
                var server = connection.DataSource;

               
                var file = "./formInstalmentTerm.rpt";
                rptDoc.Load(Server.MapPath(file));

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

                StreamPdfReport(rptDoc);
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
                StreamPdfReport(rptDoc);
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

        private void StreamPdfReport(ReportDocument rptDoc)
        {
            using (MemoryStream oStream = new MemoryStream())
            {
                rptDoc.ExportToStream(ExportFormatType.PortableDocFormat).CopyTo(oStream);
                Response.Clear();
                Response.Buffer = true;
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