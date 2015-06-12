using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Data;



public partial class Frontpage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack) //1e x
        {
            if(Session["Logged_In"] == null)
            {
                btnMyAccount.Enabled = false;
                btnUploadContent.Enabled = false;
                btnInbox.Enabled = false;
                lblLoggedIn.Text = "Currently logged in: -";
            }
            else
            {
                User user = Session["Logged_In"] as User;
                btnMyAccount.Enabled = true;
                btnUploadContent.Enabled = true;
                btnInbox.Enabled = true;
                lblLoggedIn.Text = "Currently logged in: " + user.Username;
                btnRegLog.Text = "Log Out";
            }
            DataTable TopContent = DBHandler.Content_FrontpageDataSource();
            gvTopContent.DataSource = TopContent;
            gvTopContent.DataBind();

            DataTable MainContent = DBHandler.Content_FrontpageMainDataSource();
            gvMainContent.DataSource = MainContent;
            gvMainContent.DataBind();
            Session["Main_Content"] = MainContent;
            gvMainContent.Sort("THUMBS", SortDirection.Descending);
        }
        else //Reloads
        {
            gvMainContent.DataSource = Session["Main_Content"];
            //if (Session["MAINC_PN"] != null)
                //gvMainContent.PageIndex = Convert.ToInt32(Session["MAINC_PN"]);
        }

    }

    public void SortContentGrid()
    {

    }

    protected void btnHome_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Frontpage.aspx");
    }
    protected void btnRegLog_Click(object sender, EventArgs e)
    {
        if(Session["Logged_In"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }
        else
        {
            Session["Logged_In"] = null;
            Response.Redirect("~/Frontpage.aspx");
        }
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch(ddlSortBy.SelectedIndex)
        {
            case 0: //Thumbs vws cms
                if(ddlOrderBY.SelectedIndex == 0)
                    gvMainContent.Sort("THUMBS", SortDirection.Descending);
                else
                    gvMainContent.Sort("THUMBS", SortDirection.Ascending);
                break;
            case 1: //Views
                if (ddlOrderBY.SelectedIndex == 0)
                    gvMainContent.Sort("VIEWS", SortDirection.Descending);
                else
                    gvMainContent.Sort("VIEWS", SortDirection.Ascending);
                break;
            case 2: //Comments
                if (ddlOrderBY.SelectedIndex == 0)
                    gvMainContent.Sort("COMMENTS", SortDirection.Descending);
                else
                    gvMainContent.Sort("COMMENTS", SortDirection.Ascending);
                break;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //gvMainContent.Sort("COMMENTS", SortDirection.Ascending);
        //ViewState["Main_Content"] = gvMainContent.DataSource;
    }
    protected void gvMainContent_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dt = new DataTable();
        dt = gvMainContent.DataSource as DataTable;
        if(e.SortDirection == SortDirection.Ascending)
        {
            dt.DefaultView.Sort = e.SortExpression + " ASC";
        }
        else if(e.SortDirection == SortDirection.Descending)
        {
            dt.DefaultView.Sort = e.SortExpression + " DESC";
        }
        gvMainContent.DataSource = dt;
        gvMainContent.DataBind();
        Session["Main_Content"] = gvMainContent.DataSource;
    }
    protected void gvMainContent_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMainContent.PageIndex = e.NewPageIndex;
        gvMainContent.DataBind();
    }
    protected void btnMyAccount_Click(object sender, EventArgs e)
    {
        User user = Session["Logged_In"] as User;
        string redir = "~/Profile.aspx?Username=" + user.Username;
        Response.Redirect(redir);
    }
    protected void btnUploadContent_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/UploadContent.aspx");
    }
    protected void btnInbox_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/MyInbox.aspx");
    }
    protected void btnChannels_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Channels.aspx");
    }
}