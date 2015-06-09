using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MyAccount : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["Username"] == null) //1e x
        {
            Response.Redirect("~/Frontpage.aspx");
        }
        string username = Request.QueryString["Username"];
        if(!DBHandler.User_Exists(username))
        {
            Response.Write("<script>alert('User does not exist! Redirecting to frontpage..')</script>");
            Server.Transfer("~/Frontpage.aspx");
        }

        User user = DBHandler.User_FetchDetails(username);
        User logd = Session["Logged_In"] as User;
        if (logd != null && user.Id == logd.Id)
        {
            btnDeleteAccount.Enabled = true;
            btnEditAccountDetails.Enabled = true;
        }
        else
        {
            btnDeleteAccount.Enabled = false;
            btnEditAccountDetails.Enabled = false;
        }

        if(user.Rightslevel == "M" || user.Rightslevel == "A")
        {
            btnChangeRightsLevel.Enabled = true;
            btnWarnAccount.Enabled = true;
            ddlRightsLevel.Enabled = true;
        }
        else
        {
            btnChangeRightsLevel.Enabled = false;
            btnWarnAccount.Enabled = false;
            ddlRightsLevel.Enabled = false;
        }
        lblUsermame.Text = user.Username;
        lblAccountCreatedOn.Text = user.AccountCreatedOn;
        lblLastLogin.Text = user.LastLogin;
        lblLevel.Text = Convert.ToString(user.Level);
        lblUserPermissions.Text = Convert.ToString(user.Rightslevel);


        UserStats ustats = DBHandler.User_FetchStats(user);

        lblContentThumbs.Text = Convert.ToString(ustats.ConThumbs);
        lblCommentThumbs.Text = Convert.ToString(ustats.ComThumbs);
        lblSubscribers.Text = Convert.ToString(ustats.Subscribers);
        lblContentViews.Text = Convert.ToString(ustats.ConViews);
        lblFavorites.Text = Convert.ToString(ustats.Favorites);
        lblCommentThumbs.Text = Convert.ToString(ustats.ComThumbs);
        lblComments.Text = Convert.ToString(ustats.ComThumbs);
    }
    protected void btnDeleteAccount_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ProfileDelete.aspx");
    }
    protected void btnEditAccountDetails_Click(object sender, EventArgs e)
    {

    }
    protected void btnHome_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Frontpage.aspx");
    }
}