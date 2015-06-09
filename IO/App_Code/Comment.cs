using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Comment
/// </summary>
public class Comment
{
    public int CommentId { get; set; }
    public int ContentId { get; set; }
    public int UploaderId { get; set; }
    public string Uploader { get; set; }
    public int CommentOn { get; set; }
    public int Thumbs { get; set; }
    public string Picture { get; set; }
    public string Description { get; set; }
    public Comment(string commentid, string contentid, string uploaderid, string uploader, string commenton, string thumbs, string picture, string description)
	{
        CommentId = Convert.ToInt32(commentid);
        ContentId = Convert.ToInt32(contentid);
        UploaderId = Convert.ToInt32(uploaderid);
        Uploader = uploader;
        if (commenton != "")
        {
            CommentOn = Convert.ToInt32(commenton);
        }
        else CommentOn = -1;
        Picture = picture;
        Description = description;
	    Thumbs = Convert.ToInt32(thumbs);	
	}
}