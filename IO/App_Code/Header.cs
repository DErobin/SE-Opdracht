using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Header
/// </summary>
public class Header
{
    public int Id { get; set; }
    public int ContentId { get; set;}
    public string Titel { get; set; }
    public int SoortContent { get; set; }
    public string Text { get; set; }
    public string Path { get; set; }
    public Header(int id, int contentid, string titel, int soortcontent, string tekst, string mediapath)
    {
        Id = id;
        ContentId = contentid;
        Titel = titel;
        SoortContent = soortcontent;
        Text = tekst;
        Path = mediapath;
    }
}