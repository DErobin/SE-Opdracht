using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Channels : System.Web.UI.Page
{
    private List<Channel> channels;
    protected void Page_Load(object sender, EventArgs e)
    {
        channels = new List<Channel>();
        channels = DBHandler.Content_FetchChannels();
        lbChannels.DataSource = channels;
        lbChannels.DataTextField = "Name";
        lbChannels.DataValueField = "ChannelID";
        lbChannels.DataBind();

    }
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        if(tbChannelName.Text == "")
        {
            Response.Write("<script>alert('Please fill in a name for the channel!')</script>");
            return;
        }
        if(Session["Logged_In"] as User == null)
        {
            Response.Write("<script>alert('Please log in before attempting to create a channel!')</script>");
            return;
        }
        if(DBHandler.Content_ChannelExists(tbChannelName.Text))
        {
            Response.Write("<script>alert('This channel already exists!')</script>");
            return;
        }
        if(DBHandler.Content_CreateChannel(tbChannelName.Text))
        {
            Response.Write("<script>alert('Channel created!')</script>");
            Server.Transfer("~/FrontPage.aspx");
        }
        else
        {
            Response.Write("<script>alert('Error while creating channel!')</script>");
            return;
        }
    }
    protected void btnHome_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/FrontPage.aspx");
    }
}