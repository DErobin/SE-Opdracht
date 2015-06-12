using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// A class containing information needed to display a comment under content.
/// </summary>
public class Comment
{
    public int CommentId { get; set; } //ID of the comment
    public int ContentId { get; set; } //ID of the content the comment is placed under
    public int UploaderId { get; set; } //ID of the person who uploaded the comment
    public string Uploader { get; set; } //Username of the person who uploaded the comment
    public int CommentOn { get; set; } //ID of the comment this may be a comment on
    public int Thumbs { get; set; } //The amount of thumbs the comment has
    public string Picture { get; set; } //The path in text format of where a possible image that is attached to the comment is located.
    public string Description { get; set; } //The text of the comment
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