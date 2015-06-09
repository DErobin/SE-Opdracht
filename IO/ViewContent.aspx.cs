using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class ViewContent : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string querystring = Request.QueryString["ContentID"];

        if(querystring == null)
        {
            Response.Redirect("~/Frontpage.aspx");
        }
        int ContentID = Convert.ToInt32(querystring);

        if(!DBHandler.Content_Exists(ContentID))
        {
            Response.Write("<script>alert('Content does not exist! Redirecting to frontpage..')</script>");
            Server.Transfer("~/Frontpage.aspx");
        }

        Content content = DBHandler.Content_Fetch(ContentID);
        content.Headers = DBHandler.Content_FetchHeaders(content);
        content.Comments = DBHandler.Content_FetchComments(content);
        if(content.Headers != null)
        {
            for (int i = 0; i < content.Headers.Count;i++ )
            {
                Label lblTitel_ = new Label();
                lblTitel_.Text = content.Headers[i].Titel;
                pnlContentControls.Controls.Add(lblTitel_);
                pnlContentControls.Controls.Add(new LiteralControl("<br /"));
                /*
                if(content.Headers[i].Titel != "")
                {
                    string control = "<h3>" + content.Headers[i].Titel + "</h2>";
                    pnlContentControls.Controls.Add(new LiteralControl(control));
                    pnlContentControls.Controls.Add(new LiteralControl("<br /"));
                }
                */
                switch(content.Headers[i].SoortContent)
                {
                    case 0: //Text
                        Label lblText = new Label();
                        lblText.Text = content.Headers[i].Text;
                        pnlContentControls.Controls.Add(lblText);
                        pnlContentControls.Controls.Add(new LiteralControl("<br /"));
                        break;
                    case 1: //Picture
                        Label lblPicture = new Label();
                        lblPicture.Text = "[[PICTURE]]: " + content.Headers[i].Path;
                        pnlContentControls.Controls.Add(lblPicture);
                        pnlContentControls.Controls.Add(new LiteralControl("<br /"));
                        break;
                    case 2: //Video
                        Label lblVideo = new Label();
                        lblVideo.Text = "[[VIDEO]]: " + content.Headers[i].Path;
                        pnlContentControls.Controls.Add(lblVideo);
                        pnlContentControls.Controls.Add(new LiteralControl("<br /"));
                        break;
                }
            }
        }

        if (content.Comments != null)
        {
            lblComAmount.Text = Convert.ToString(content.Comments.Count);
            for (int i = 0; i < content.Comments.Count; i++)
            {
                /*
                string control = "<h2>" + content.Comments[i].Uploader + "</h2>";
                pnlComments.Controls.Add(new LiteralControl(control));
                pnlCommentsControls.Controls.Add(new LiteralControl("<br /"));
                 */
                pnlComments.Controls.Add(new LiteralControl("<br /"));
                Label lblCommentID = new Label();
                lblCommentID.Text = content.Comments[i].CommentId + "- ";
                pnlComments.Controls.Add(lblCommentID);

                HyperLink hlProfile = new HyperLink();
                hlProfile.Text = content.Comments[i].Uploader;
                string redire = "~/Profile.aspx?Username=" + content.Comments[i].Uploader;
                hlProfile.NavigateUrl = redire;
                pnlComments.Controls.Add(hlProfile);

                Label lblThumbsD = new Label();
                lblThumbsD.Text = " Thumbs: " + content.Comments[i].Thumbs;
                pnlComments.Controls.Add(lblThumbsD);
                pnlComments.Controls.Add(new LiteralControl("<br /"));

                Label lblPicture = new Label();
                lblPicture.Text = "[[PICTURE]]: " + content.Comments[i].Picture;
                pnlComments.Controls.Add(lblPicture);
                pnlComments.Controls.Add(new LiteralControl("<br /"));

                Label lblDescription = new Label();
                lblDescription.Text = content.Comments[i].Description;
                pnlComments.Controls.Add(lblDescription);
                pnlComments.Controls.Add(new LiteralControl("<br /"));

            }
        }
        else lblComAmount.Text = "0";


        lblTitel.Text = content.Titel;
        lblConDescription.Text = content.Beschrijving;
        //lblUsername.Text = content.UploaderUsername;
        hlUsername.Text = content.UploaderUsername;
        string redir = "~/Profile.aspx?Username=" + content.UploaderUsername;
        hlUsername.NavigateUrl = redir;
        lblThumbs.Text = " " + Convert.ToString(content.Thumbs);
        lblViews.Text = Convert.ToString(content.Views);
        lblFavorites.Text = Convert.ToString(content.Favorites);
        lblDatum.Text = content.Datum;
    }
}