using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Contains all the information of a tag, including id and name.
/// </summary>
public class Tag
{
    public int TagID { get; set; } //ID of the tag
    public string TagName { get; set; } //Name of the tag
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