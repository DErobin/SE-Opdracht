using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MyAccount : System.Web.UI.Page
{
    User user;
    User logd;
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

        user = DBHandler.User_FetchDetails(username);
        logd = Session["Logged_In"] as User;
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

        if(logd != null && (logd.Rightslevel == "M" || logd.Rightslevel == "A"))
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
    protected void btnChangeRightsLevel_Click(object sender, EventArgs e)
    {
        //0n, 1m, 2a
        if (user == null || logd == null)
            return;
        else
        {
            switch(ddlRightsLevel.SelectedIndex)
            {
                case 0: //Normal
                    if(user.Rightslevel == "N")
                    {
                        Response.Write("<script>alert('User is already a normal user!')</script>");
                        return;
                    }
                    DBHandler.User_SetRightsLevel(user, "N");
                    lblUserPermissions.Text = "N";
                    break;
                case 1: //Mod
                    if (user.Rightslevel == "M")
                    {
                        Response.Write("<script>alert('User is already a normal user!')</script>");
                        return;
                    }
                    DBHandler.User_SetRightsLevel(user, "M");
                    lblUserPermissions.Text = "M";
                    break;
                case 2: //Admin
                    if (user.Rightslevel == "A")
                    {
                        Response.Write("<script>alert('User is already a normal user!')</script>");
                        return;
                    }
                    DBHandler.User_SetRightsLevel(user, "A");
                    lblUserPermissions.Text = "A";
                    break;
            }
            Response.Write("<script>alert('Rights updated!')</script>");
            string redir = "~/Profile.aspx?Username="+user.Username;
            Server.Transfer(redir);
        }
    }
}