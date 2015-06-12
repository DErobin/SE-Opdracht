using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// A class containing the id and the name of the channel
/// </summary>
public class Channel
{
    public int ChannelID { get; set; }
    public string Name { get; set; }
	public Channel(int id, string name)
	{
        ChannelID = id;
        Name = name;

		//
		// TODO: Add constructor logic here
		//
	}
}