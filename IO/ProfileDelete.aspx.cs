using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ProfileDelete : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["Logged_In"] == null)
        {
            Response.Redirect("~/Frontpage.aspx");
        }
        User user = Session["Logged_In"] as User;


    }
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        User user = DBHandler.User_Authenticate(tbUsername.Text, tbPassword.Text);
        if(user == null)
        {
            lblNotification.Text = "Invalid login details!";
            return;
        }
        DBHandler.User_Delete(user);
        Session["Logged_In"] = null;
        Response.Write("<script>alert('You have deleted your account! Redirecting to frontpage..')</script>");
        Server.Transfer("~/Frontpage.aspx");
    }
    protected void btmCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Frontpage.aspx");
    }
}