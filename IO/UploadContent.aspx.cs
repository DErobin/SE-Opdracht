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
            lbTags.DataSource = Tags;
            lbTags.DataTextField = "TagName";
            lbTags.DataValueField = "TagID";
            lbTags.DataBind();
            //Session["Upload_TagList"] = Tags;
            Session["AddedTags"] = AddedTags;
        }
        else
        {
            Tags = DBHandler.Content_FetchAllTags();
            AddedTags = Session["AddedTags"] as List<Tag>;
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
                Content content = DBHandler.Content_Fetch(contentid);
                DBHandler.Content_UploadTags(AddedTags, content);
            }
            Response.Write("<script>alert('Content uploaded!')</script>");
            string redir = "~/ViewContent.aspx?ContentID=" + contentid;
            Response.Redirect(redir);
        }

    }
    protected void btnAddTag_Click(object sender, EventArgs e)
    {
        bool exists = false;
        for (int j = 0; j < AddedTags.Count; j++ )
        {
            if (AddedTags[j].TagID == Tags[lbTags.SelectedIndex].TagID)
                exists = true;
        }

        if (!exists)
            AddedTags.Add(Tags[lbTags.SelectedIndex]);
        
        pnlTags.Controls.Clear();
        for(int i=0; i<AddedTags.Count; i++)
        {
            Label label = new Label();
            label.Text = AddedTags[i].TagName + ", ";
            pnlTags.Controls.Add(label);
        }
        Session["AddedTags"] = AddedTags;
    }
    protected void ddlTag_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    protected void btnHome_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/Frontpage.aspx");
    }
    protected void lbTags_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
    protected void btnRemove_Click(object sender, EventArgs e)
    {
        for(int i=0; i<AddedTags.Count; i++)
        {
            if (AddedTags[i].TagID == Tags[lbTags.SelectedIndex].TagID)
                AddedTags.Remove(AddedTags[i]);
        }
        pnlTags.Controls.Clear();
        for (int i = 0; i < AddedTags.Count; i++)
        {
            Label label = new Label();
            label.Text = AddedTags[i].TagName + ", ";
            pnlTags.Controls.Add(label);
        }
        Session["AddedTags"] = AddedTags;
    }
    protected void btnCreateTag_Click(object sender, EventArgs e)
    {
        if(tbNewTag.Text == null)
        {
            Response.Write("<script>alert('Please enter a tag!')</script>");
            return;
        }
        if(DBHandler.Content_TagExists(tbNewTag.Text))
        {
            Response.Write("<script>alert('Tag already exists!')</script>");
            return;
        }
        DBHandler.Content_CreateNewTag(tbNewTag.Text);
        Response.Write("<script>alert('Tag created!')</script>");
        Tags = DBHandler.Content_FetchAllTags();
        lbTags.DataSource = Tags;
        lbTags.DataTextField = "TagName";
        lbTags.DataValueField = "TagID";
        lbTags.DataBind();

        pnlTags.Controls.Clear();
        for (int i = 0; i < AddedTags.Count; i++)
        {
            Label label = new Label();
            label.Text = AddedTags[i].TagName + ", ";
            pnlTags.Controls.Add(label);
        }
        Session["AddedTags"] = AddedTags;

    }
}