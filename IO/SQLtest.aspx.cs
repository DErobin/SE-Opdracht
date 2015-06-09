using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

public partial class SQLtest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnConnect_Click(object sender, EventArgs e)
    {

        string TestCMD = "select * from Comment_";
        OracleDataReader dbr = DBManager.ExecuteQuery(TestCMD);
        string ide = "";
        while(dbr.Read())
        {
            ide = dbr["beschrijving"].ToString();
        }

        lblTest.Text = ide;
    }
}