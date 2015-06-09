using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        User user = null;
        user = DBHandler.User_Authenticate(tbUsername.Text, tbPassword.Text);
        if(user == null)
        {
            lblNotification.Text = "Invalid login details!";
            return;
        }
        Session["Logged_In"] = user;
        Response.Redirect("~/Frontpage.aspx");
        //lblNotification.Text = "User:" + user.Username + " PW:" + user.Password + " EMAIL:" + user.Email;
    }
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        if(tbRegEmail.Text == "" || tbRegUsername.Text == "" || tbRegPassword.Text == "")
        {
            lblRegNotification.Text = "Please fill in all required fields!";
            return;
        }

        if(DBHandler.User_Exists(tbRegUsername.Text))
        {
            lblRegNotification.Text = "Username already exists!";
            return;
        }
        if(DBHandler.User_Exists_M(tbRegEmail.Text))
        {
            lblRegNotification.Text = "E-mailadres already exists!";
            return;
        }
        User user = DBHandler.User_Register(tbRegUsername.Text, tbRegPassword.Text, tbRegEmail.Text);
        if(user != null)
        {
            Session["Logged_In"] = user;
            Response.Write("<script>alert('Registering succesfull! Redirecting to frontpage..')</script>");
            Server.Transfer("~/Frontpage.aspx");
        }
        else
        {
            lblRegNotification.Text = "ERROR";
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Frontpage.aspx");
    }
}