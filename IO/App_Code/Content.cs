using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// This class contains information regarding a piece of uploaded content. It also contains the headers
/// linked to the content, the comments and the tags.
/// </summary>
public class Content
{
    public int ContentID { get; set; } //ID of the content in the DB
    public int UploaderID { get; set; } //ID of the uploader
    public string Titel { get; set; } //Titel of the content
    public string Beschrijving { get; set; } //Description of the content
    public int Views { get; set; } //Amount of views the content has
    public string Datum { get; set; } //The date in string format

    //Additional queries needed:
    public string UploaderUsername { get; set; } //The username of the uploader of the content
    public int Thumbs { get; set; } //The amount of thumbs the content has gotten
    public int Favorites { get; set; } //The amount of favorites the content has gotten

    public List<Header> Headers {get; set;} //The headers belonging to a content
    public List<Comment> Comments { get; set; } //The comments that appear under a piece of content
    public List<Tag> Tags { get; set; } //The list of tags that are linked to the content
	public Content(int contentid, int uploaderid, string titel, string beschrijving, int views, string datum, string uploaderusername, int thumbs, int favorites)
	{
        ContentID = contentid;
        UploaderID = uploaderid;
        Titel = titel;
        Beschrijving = beschrijving;
        Views = views;
        Datum = datum;

        //AQN:
        UploaderUsername = uploaderusername;
        Thumbs = thumbs;
        Favorites = favorites;
	}

}