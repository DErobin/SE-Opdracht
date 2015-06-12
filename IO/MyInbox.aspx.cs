using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MyInbox : System.Web.UI.Page
{
    List<Message> RecievedMessages;
    List<Message> SentMessages;
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["Logged_In"] == null)
            Server.Transfer("~/Frontpage.aspx");
        User user = Session["Logged_In"] as User;
        if (!Page.IsPostBack)
        {

        }
        RecievedMessages = DBHandler.User_FetchInbox(user);
        if (RecievedMessages == null)
            RecievedMessages = new List<Message>();
        for (int i = 0; i < RecievedMessages.Count; i++) //Draw all the recieved messages in pnlRecieved
        {
            User usender = DBHandler.User_FetchDetails(RecievedMessages[i].SenderID); //Fetch the messages
            if (usender != null)
            {

                Label titel = new Label();
                titel.Text = "TITEL: " + RecievedMessages[i].Titel;

                Label text = new Label();
                text.Text = "MESSAGE: " + RecievedMessages[i].MessageText;

                Label date = new Label();
                date.Text = "DATE: " + RecievedMessages[i].Date;

                Label breakl = new Label();
                breakl.Text = "--------------------------------------------------------";

                pnlSent.Controls.Add(breakl);
                pnlSent.Controls.Add(new LiteralControl("<br /"));

                //Hyperlink does not seem to work here.
                HyperLink hlSender = new HyperLink();
                hlSender.Text = "BY: " + usender.Username;
                string redire = "~/Profile.aspx?Username=" + usender.Username;
                hlSender.NavigateUrl = redire;
                pnlSent.Controls.Add(hlSender);
                pnlSent.Controls.Add(new LiteralControl("<br /"));
                pnlSent.Controls.Add(titel);
                pnlSent.Controls.Add(new LiteralControl("<br /"));
                pnlSent.Controls.Add(text);
                pnlSent.Controls.Add(new LiteralControl("<br /"));
                pnlSent.Controls.Add(date);
                pnlSent.Controls.Add(new LiteralControl("<br /"));

            }
        }
        SentMessages = DBHandler.User_FetchOutbox(user);
        if (SentMessages == null)
            SentMessages = new List<Message>();
        for (int i = 0; i < SentMessages.Count; i++)
        {
            User reciever = DBHandler.User_FetchDetails(SentMessages[i].RecieverID);
            if (reciever != null)
            {

                Label titel = new Label();
                titel.Text = "TITEL: " + SentMessages[i].Titel;

                Label text = new Label();
                text.Text = "MESSAGE: " + SentMessages[i].MessageText;

                Label date = new Label();
                date.Text = "DATE: " + SentMessages[i].Date;

                Label breakl = new Label();
                breakl.Text = "--------------------------------------------------------";

                pnlRecieved.Controls.Add(breakl);
                pnlRecieved.Controls.Add(new LiteralControl("<br /"));

                //Hyperlink does not seem to work here.
                HyperLink hlSender = new HyperLink();
                hlSender.Text = "SENT TO: " + reciever.Username;
                string redire = "~/Profile.aspx?Username=" + reciever.Username;
                hlSender.NavigateUrl = redire;
                pnlRecieved.Controls.Add(hlSender);
                pnlRecieved.Controls.Add(new LiteralControl("<br /"));
                pnlRecieved.Controls.Add(titel);
                pnlRecieved.Controls.Add(new LiteralControl("<br /"));
                pnlRecieved.Controls.Add(text);
                pnlRecieved.Controls.Add(new LiteralControl("<br /"));
                pnlRecieved.Controls.Add(date);
                pnlRecieved.Controls.Add(new LiteralControl("<br /"));

            }
        }
    }
    protected void btnSend_Click(object sender, EventArgs e)
    {
        if(tbTitel.Text == "" || tbReciever.Text == "" || tbText.Text == "")
        {
            Response.Write("<script>alert('Please fill in a Titel, the reciever and the message before submitting!')</script>");
            //Server.Transfer("~/Frontpage.aspx");
            return;
        }
        if(!DBHandler.User_Exists(tbReciever.Text))
        {
            Response.Write("<script>alert('The reciever of your message does not appear to exist! Please check if you picked a correct username.')</script>");
            return;
        }
        User reciever = DBHandler.User_FetchDetails(tbReciever.Text);
        DBHandler.User_CreateNewMessage(Session["Logged_In"] as User, reciever, tbTitel.Text, tbText.Text);

        Response.Write("<script>alert('Message sent!.')</script>");
    }
    protected void btnHome_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Frontpage.aspx");
    }
}