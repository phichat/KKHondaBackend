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

namespace KKHondaReport.HPS
{
    public partial class CollectionLetter1 : System.Web.UI.Page
    {
        private string conStr = ConfigurationManager.ConnectionStrings["KKMssql"].ConnectionString;
        private SqlConnection conn = null;
        private ReportDocument rptDoc;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["CollectionLetter"] != null)
            {
                if (Boolean.Parse(Request.QueryString["CollectionLetter"]) == true)
                {
                    string cusType = Request.QueryString["cusType"];
                    string bookNo = Request.QueryString["bookNo"];

                    ExportCollectionLetter(cusType, bookNo);
                }
            }
        }

        private void ExportCollectionLetter(string cusType = "", string bookNo = "")
        {
            try
            {
                try
                {
                    rptDoc = new ReportDocument();
                    var file = "";
                    if (cusType == "1")
                    {
                        file = "./CollectionLetter.rpt";
                    }
                    else
                    {
                        file = "./CollectionLetter2.rpt";
                    }
                    rptDoc.Load(Server.MapPath(file));
                    rptDoc.Refresh();

                    TableLogOnInfo L1 = rptDoc.Database.Tables[0].LogOnInfo;
                    GetLoginfo(L1);
                    rptDoc.SetParameterValue("@bookNo", bookNo);
                    rptDoc.Database.Tables[0].ApplyLogOnInfo(L1);

                    StreamPdfReport(rptDoc, "CollectionLetter");
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
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

        private void GetLoginfo(TableLogOnInfo Log)
        {
            SqlConnectionStringBuilder connection = new SqlConnectionStringBuilder(conStr);

            Log.ConnectionInfo.ServerName = connection.DataSource;
            Log.ConnectionInfo.UserID = connection.UserID;
            Log.ConnectionInfo.Password = connection.Password;
            Log.ConnectionInfo.DatabaseName = "";
        }
    }
}