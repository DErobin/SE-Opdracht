using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// A header that belongs to a piece of content
/// </summary>
public class Header
{
    public int Id { get; set; } //Id of the header
    public int ContentId { get; set;} //ContentID the header is linked to
    public string Titel { get; set; } //Titel of the header
    public int SoortContent { get; set; } //The type of subcontent the header contains
    public string Text { get; set; } //The text in a header
    public string Path { get; set; } //The path of possible media
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