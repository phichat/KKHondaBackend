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
                conn.Open();

                cmd.CommandText = "dbo.sp_RptFormInstalmentTerm";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ContractId", contractId);
                cmd.Connection = conn;
                cmd.CommandTimeout = 120;

                da.SelectCommand = cmd;
                da.Fill(dt);
                
                var file = "./formInstalmentTerm.rpt";
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
    }
}