using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Tag
/// </summary>
public class Tag
{
    public int TagID { get; set; }
    public string TagName { get; set; }
	public Tag(int tagid, string tagname)
	{
        TagID = tagid;
        TagName = tagname;
		//
		// TODO: Add constructor logic here
		//
	}

    public override string ToString()
    {
        return TagName;
    }
}