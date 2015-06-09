using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UploadContent : System.Web.UI.Page
{
    List<Tag> Tags;
    List<Tag> AddedTags;
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            Tags = new List<Tag>();
            AddedTags = new List<Tag>();
            Tags = DBHandler.Content_FetchAllTags();
            ddlTag.DataSource = Tags;
            ddlTag.DataTextField = "TagName";
            ddlTag.DataValueField = "TagID";
            ddlTag.DataBind();
            Session["Upload_TagList"] = Tags;
            Session["TagPanel"] = AddedTags;
            Session["DDLItem"] = ddlTag.SelectedIndex;
        }
        else
        {
            
            Tags = Session["Upload_TagList"] as List<Tag>;
            ddlTag.DataSource = Tags;
            ddlTag.DataTextField = "TagName";
            ddlTag.DataValueField = "TagID";
            ddlTag.DataBind();
            ddlTag.SelectedIndex = Convert.ToInt32(Session["DDLItem"].ToString());


            pnlTags.Controls.Clear();
            AddedTags = Session["TagPanel"] as List<Tag>;
            if (AddedTags != null)
                for (int i = 0; i < AddedTags.Count; i++ )
                {
                    Label lbltagadd = new Label();
                    lbltagadd.Text = AddedTags[i].TagName + ", ";
                    pnlTags.Controls.Add(lbltagadd);
                }


        }

        switch(ddlMediaType.SelectedIndex)
        {
            case 0: //Text:
                tbHeaderInput.TextMode = TextBoxMode.MultiLine;
                break;
            case 1: //Picture:
                tbHeaderInput.TextMode = TextBoxMode.SingleLine;
                break;
            case 2: //Video:
                tbHeaderInput.TextMode = TextBoxMode.SingleLine;
                break;
        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (tbContentTitel.Text == "" || tbHeaderInput.Text == "")
        {
            Response.Write("<script>alert('Please fill in the Titel and media input!')</script>");
            return;
        }
        else
        {
            User user = Session["Logged_In"] as User;

            int contentid = DBHandler.Content_Upload(user.Id, tbContentTitel.Text, tbContentDescription.Text);

            if(contentid != -1)
            {
                DBHandler.Content_UploadHeaders(contentid, tbHeaderTitel.Text, ddlMediaType.SelectedIndex, tbHeaderInput.Text, tbHeaderInput.Text);
            }
            Response.Write("<script>alert('Content uploaded!')</script>");
            string redir = "~/ViewContent.aspx?ContentID=" + contentid;
            Response.Redirect(redir);
        }

    }
    protected void btnAddTag_Click(object sender, EventArgs e)
    {
        Tag tag = Tags[ddlTag.SelectedIndex];
        AddedTags.Add(tag);
        Session["TagPanel"] = AddedTags;
    }
    protected void ddlTag_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["DDLItem"] = ddlTag.SelectedIndex;
    }
    protected void btnHome_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/Frontpage.aspx");
    }
}