using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Content
/// </summary>
public class Content
{
    public int ContentID { get; set; }
    public int UploaderID { get; set; }
    public string Titel { get; set; }
    public string Beschrijving { get; set; }
    public int Views { get; set; }
    public string Datum { get; set; }

    //Additional queries needed:
    public string UploaderUsername { get; set; }
    public int Thumbs { get; set; }
    public int Favorites { get; set; }

    public List<Header> Headers {get; set;}
    public List<Comment> Comments { get; set; }
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

    public void SetDetails(List<string> Details)
    {

    }
}