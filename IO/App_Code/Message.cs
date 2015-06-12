using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Contains all the information of a message a user has sent to another user
/// </summary>
public class Message
{
    public int MessageID { get; set; } //The ID of the message
    public int SenderID { get; set;} //The ID of the sender of the message
    public int RecieverID { get; set; } //The ID of the reciever of the message
    public string Titel { get; set;} //The titel of the message
    public string MessageText { get; set; } //The text of the message
    public string Date { get; set; } // The date the message was posted in string format

	public Message(int messageid, int senderid, int recieverid, string titel, string message, string date)
	{
        MessageID= messageid;
        SenderID= senderid;
        RecieverID= recieverid;
        Titel= titel;
        MessageText= message;
        Date = date;
		//
		// TODO: Add constructor logic here
		//
	}
}