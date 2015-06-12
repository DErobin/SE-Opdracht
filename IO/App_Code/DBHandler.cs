using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Data;
using System.Web.UI.WebControls;



/// <summary>
/// This class handles communication with the database. It does this by formatting queries and handling errors before
/// submitting these queries to the DBManager class, which opens the connection and executes them.
/// </summary>
public class DBHandler
{
	public DBHandler()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    //User related:

    //Check if username and password are correct, if correct, return user details. Else, return null.
    static public User User_Authenticate(string username, string password)
    {
        User user=null;

        string query = "Select * from Gebruiker where gebruikersnaam = '" + username + "' and wachtwoord = '" +password + "'";
        //Console.WriteLine(query);
        OracleDataReader dbr = DBManager.ExecuteQuery(query);
        if(!dbr.HasRows)
        {
            return null;
        }
        else while(dbr.Read())
        {
            user = new User(Convert.ToInt32(dbr["gebruikersid"].ToString()), dbr["gebruikersnaam"].ToString(), dbr["e_mailadres"].ToString(), dbr["wachtwoord"].ToString(), Convert.ToInt32(dbr["leeftijd"].ToString()), dbr["accountgemaaktop"].ToString(), dbr["laatstingelogdop"].ToString(), Convert.ToInt32(dbr["profielprivacy"].ToString()), Convert.ToInt32(dbr["berichtprivacy"].ToString()), Convert.ToInt32(dbr["massaberichtprivacy"].ToString()), Convert.ToInt32(dbr["level_"].ToString()), dbr["rechtsniveau"].ToString());
        }
        return user;
    }

    //Grab the details of a user without sensitive information like passwords or privacy settings.
    //returns null if the user doesnt exist.
    static public User User_FetchDetails(string username)
    {
        User user = null;
        string query = "Select * from Gebruiker where gebruikersnaam = '" + username + "'";
        OracleDataReader dbr = DBManager.ExecuteQuery(query);
        if (!dbr.HasRows)
        {
            return null;
        }
        else while (dbr.Read())
        {
            user = new User(Convert.ToInt32(dbr["gebruikersid"].ToString()), dbr["gebruikersnaam"].ToString(), dbr["e_mailadres"].ToString(), Convert.ToInt32(dbr["leeftijd"].ToString()), dbr["accountgemaaktop"].ToString(), dbr["laatstingelogdop"].ToString(), Convert.ToInt32(dbr["level_"].ToString()), dbr["rechtsniveau"].ToString());
        }
        return user;
    }

    //See above, but fetch from userid instead of username.
    static public User User_FetchDetails(int userid)
    {
        User user = null;
        string query = "Select * from Gebruiker where GEBRUIKERSID = " + userid;
        OracleDataReader dbr = DBManager.ExecuteQuery(query);
        if (!dbr.HasRows)
        {
            return null;
        }
        else while (dbr.Read())
            {
                user = new User(Convert.ToInt32(dbr["gebruikersid"].ToString()), dbr["gebruikersnaam"].ToString(), dbr["e_mailadres"].ToString(), Convert.ToInt32(dbr["leeftijd"].ToString()), dbr["accountgemaaktop"].ToString(), dbr["laatstingelogdop"].ToString(), Convert.ToInt32(dbr["level_"].ToString()), dbr["rechtsniveau"].ToString());
            }
        return user;
    }

    //Fetch some additional user related information and stats, for display on the profile page.
    //returns null if user doesnt exist
    static public UserStats User_FetchStats(User user)
    {
        if (user == null)
            return null;
        UserStats ustats = new UserStats(user); 
        //OracleDataReader dbr;

        int conthumbs= 0;
        int comthumbs= 0;
        int subscribers= 0;
        int conviews=0;
        int favorites=0;
        int comsmade=0;

        string query = "select g.Gebruikersnaam, COALESCE(SUM(ct.Rating), 0) as conthumbs from Gebruiker g left join Content_ co on co.UploaderID = g.GebruikersID left join ContentThumb ct on ct.ContentID= co.ContentID where g.GebruikersID = " + ustats.Id + " group by g.Gebruikersnaam";
        OracleDataReader dbr = DBManager.ExecuteQuery(query);
        while(dbr.Read())
        {
            conthumbs = Convert.ToInt32(dbr["conthumbs"].ToString());
        }

        query = "select g.Gebruikersnaam, COALESCE(SUM(cot.Rating), 0) as comthumbs from Gebruiker g left join Comment_ com on com.Plaatserid = g.Gebruikersid left join CommentThumb cot on cot.CommentID = com.CommentID where g.GebruikersID = " + ustats.Id + " group by g.Gebruikersnaam";
        dbr = DBManager.ExecuteQuery(query);
        while (dbr.Read())
        {
            comthumbs = Convert.ToInt32(dbr["comthumbs"].ToString());
        }
            
        query = "select g.Gebruikersnaam, COALESCE(COUNT(s.SubscriberID),0) as subscribers from Gebruiker g left join Subscriber s on g.GebruikersID = s.SubscriberID where g.GebruikersID = " + ustats.Id + " group by g.Gebruikersnaam";
        dbr = DBManager.ExecuteQuery(query);
        while (dbr.Read())
        {
            subscribers = Convert.ToInt32(dbr["subscribers"].ToString());
        }

        query = "select g.Gebruikersnaam, COALESCE(SUM(co.Views_),0) as conviews from Gebruiker g left join content_ co on co.UploaderID = g.GebruikersID where g.GebruikersID = " + ustats.Id + " group by g.Gebruikersnaam";
        dbr = DBManager.ExecuteQuery(query);
        while (dbr.Read())
        {
            conviews = Convert.ToInt32(dbr["conviews"].ToString());
        }

        query = "select g.Gebruikersnaam, COALESCE(COUNT(f.gebruikersID),0) as favorites from Gebruiker g left join Favorite f on g.GebruikersID = f.Gebruikersid where g.GebruikersID = " + ustats.Id + " group by g.Gebruikersnaam";
        dbr = DBManager.ExecuteQuery(query);
        while (dbr.Read())
        {
            favorites = Convert.ToInt32(dbr["favorites"].ToString());
        }

        query = "select g.Gebruikersnaam, COALESCE(COUNT(com.plaatserid), 0) as comsmade from Gebruiker g left join Comment_ com on g.GebruikersID = com.PlaatserID where g.GebruikersID = " + ustats.Id + " group by g.Gebruikersnaam";
        dbr = DBManager.ExecuteQuery(query);
        while (dbr.Read())
        {
            comsmade = Convert.ToInt32(dbr["comsmade"].ToString());
        } 

        //ustats.SetStats(conthumbs, comthumbs, subscribers, 0, 0, 0);
        ustats.SetStats(conthumbs, comthumbs, subscribers, conviews, favorites, comsmade);
        return ustats;
    }
    /*
    static bool User_UpdateRecords(User user)
    {

        /*
        string query = "Update GEBRUIKER set 
        if (user == null)
            return false;
        string query = "Update GEBRUIKER set 
        GEBRUIKERSID = "" + user.Id + "',;
        GEBRUIKERSNAAM = 
        E_MAILADRES
        WACHTWOORD
        LEEFTIJD
        ACCOUNTGEMAAKTOP
        LAATSTINGELOGDOP
        PROFIELPRIVACY
        BERICHTPRIVACY
        MASSABERICHTPRIVACY
        LEVEL_
        RECHTSNIVEAU"
        return false;
    }*/

    //Registers a new user and returns the new User. if the registration fails, the user will be null.
    static public User User_Register(string username, string password, string email)
    {
        User user = null;

        OracleCommand cmd = DBManager.ExecuteProcedure("User_Register");

        cmd.Parameters.Add("username", OracleDbType.NVarchar2).Value = username;
        cmd.Parameters.Add("email", OracleDbType.NVarchar2).Value = email;
        cmd.Parameters.Add("password", OracleDbType.NVarchar2).Value = password;

        OracleDataAdapter oda = new OracleDataAdapter(cmd);
        cmd.ExecuteNonQuery();

        user = User_Authenticate(username, password);
        return user;
    }

    //Changes the rights of a specificn user. Returns null if user doesnt exists or if the richtslevel is invalid.
    static public void User_SetRightsLevel(User user, string rlevel)
    {
        if (user == null || rlevel == null || rlevel == "")
            return;

        //Correcting issues with datetime format (DB wont accept it) takes too much time, so a static date has been chosen.
        string query = "Update Gebruiker set RECHTSNIVEAU= '" + rlevel + "' where GEBRUIKERSID = " + user.Id;
        OracleDataReader dbr = DBManager.ExecuteQuery(query);
    }

    //Deletes the specified user.
    static public void User_Delete(User user)
    {
        OracleCommand cmd = DBManager.ExecuteProcedure("User_Delete");
        cmd.Parameters.Add("userid", OracleDbType.Int32).Value = user.Id;
        OracleDataAdapter oda = new OracleDataAdapter(cmd);
        cmd.ExecuteNonQuery();

    }

    //Checks if the specified user exists and returns this as a bool.
    static public bool User_Exists(string username)
    {
        string query = "select * from Gebruiker where gebruikersnaam = '" + username + "'";
        OracleDataReader dbr = DBManager.ExecuteQuery(query);
        if (!dbr.HasRows)
        {
            return false;
        }
        else
            return true;
    }
    //Same as above, but by email.
    static public bool User_Exists_M(string email)
    {
        string query = "select * from Gebruiker where e_mailadres = '" + email + "'";
        OracleDataReader dbr = DBManager.ExecuteQuery(query);
        if (!dbr.HasRows)
        {
            return false;
        }
        else
            return true;
    }
    
    //Fetches a list of messages from the database that the specified user has recieved in his inbox.
    //returns null if nothing is found or the user doesnt exist
    static public List<Message> User_FetchInbox(User user)
    {
        if (!User_Exists(user.Username))
            return null;
        string query = "select * from Bericht where ontvangerid = " + user.Id;
        OracleDataReader dbr = DBManager.ExecuteQuery(query);
        if (!dbr.HasRows)
            return null;
        List<Message> inbox = new List<Message>();

        while(dbr.Read())
        {
            inbox.Add(new Message(Convert.ToInt32(dbr["berichtid"].ToString()), Convert.ToInt32(dbr["zenderid"].ToString()), Convert.ToInt32(dbr["ontvangerid"].ToString()), dbr["titel"].ToString(), dbr["tekst"].ToString(), dbr["datum"].ToString()));
        }
        return inbox;
        
    }

    //Fetches the outbox of a user. Returns null if the user doesnt exist.
    static public List<Message> User_FetchOutbox(User user)
    {
        if (!User_Exists(user.Username))
            return null;
        string query = "select * from Bericht where zenderid = " + user.Id;
        OracleDataReader dbr = DBManager.ExecuteQuery(query);
        if (!dbr.HasRows)
            return null;
        List<Message> outbox = new List<Message>();

        while (dbr.Read())
        {
            outbox.Add(new Message(Convert.ToInt32(dbr["berichtid"].ToString()), Convert.ToInt32(dbr["zenderid"].ToString()), Convert.ToInt32(dbr["ontvangerid"].ToString()), dbr["titel"].ToString(), dbr["tekst"].ToString(), dbr["datum"].ToString()));
        }
        return outbox;
    }

    //Create a new message
    static public void User_CreateNewMessage(User sender, User reciever, string titel, string tekst)
    {
        if(sender == null || reciever == null)
            return;

        //Correcting issues with datetime format (DB wont accept it) takes too much time, so a static date has been chosen.
        string query = "insert into Bericht(ZENDERID, ONTVANGERID, TITEL, TEKST, DATUM) values(" + sender.Id + ", " + reciever.Id + ", '" + titel + "', '" + tekst + "', " + "date '2420-04-20'"/*DateTime.Now*/ + ")";
        OracleDataReader dbr = DBManager.ExecuteQuery(query);
    }

    //Checks if content has already been thumbed by a specific user and returns this as a bool
    static public bool User_HasThumbed(Content content, User user)
    {
        if (content == null)
            return false;
        string query = "Select * from ContentThumb where gebruikersid= " + user.Id + " and contentid = " + content.ContentID;
        OracleDataReader dbr = DBManager.ExecuteQuery(query);
        if (dbr.HasRows)
            return true;
        else
            return false;
    }
    //Content:

    //Add a thumb to content
    static public void Content_Thumb(Content content, User user, bool positive)
    {
        if (user == null || content == null)
            return;
        string query;
        if(positive)
            query = "insert into ContentThumb(GEBRUIKERSID,CONTENTID,RATING) values(" + user.Id + ", " + content.ContentID +  ", 1)";
        else
            query = "insert into ContentThumb(GEBRUIKERSID,CONTENTID,RATING) values(" + user.Id + ", " + content.ContentID +  ", -1)";
        OracleDataReader dbr = DBManager.ExecuteQuery(query);
    }

    //Checks if content exists and returns this as a bool
    static public bool Content_Exists(int contentid)
    {
        string query = "Select * from Content_ where ContentID = '" + contentid + "'";
        OracleDataReader dbr = DBManager.ExecuteQuery(query);
        if (!dbr.HasRows)
        {
            return false;
        }
        else
            return true;
    }

    //Fetch a list of all the channels for display
    static public List<Channel> Content_FetchChannels()
    {
        List<Channel> channels = new List<Channel>();
        string query = "Select * from Channel";
        OracleDataReader dbr = DBManager.ExecuteQuery(query);
        if(!dbr.HasRows)
        {
            return channels;
        }
        else while(dbr.Read())
        {
            channels.Add(new Channel(Convert.ToInt32(dbr["channelid"].ToString()), dbr["naam"].ToString()));
        }
        return channels;
    }

    //Create a new channel returns true if the channel creation passes, false if the channel already exists.
    static public bool Content_CreateChannel(string name)
    {
        if (Content_ChannelExists(name))
            return false;
        string query = "insert into Channel(naam) values('" + name + "')";
        OracleDataReader dbr = DBManager.ExecuteQuery(query);
        return true;
    }

    //Checks if a piece of content exists and returns this as a bool
    static public bool Content_ChannelExists(string name)
    {
        string query = "Select * from Channel where naam = '" + name + "'";
        OracleDataReader dbr = DBManager.ExecuteQuery(query);
        if (!dbr.HasRows)
        {
            return false;
        }
        else
            return true;
    }

    //Upload a new piece of content, and returns the new contents id. -1 if content creation fails.
    static public int Content_Upload(int uploaderid, string titel, string description)
    {
        OracleCommand cmd = DBManager.ExecuteProcedure("Content_Upload");

        cmd.Parameters.Add("Uploaderid", OracleDbType.Int32).Value = uploaderid;
        cmd.Parameters.Add("Titel", OracleDbType.NVarchar2).Value = titel;
        cmd.Parameters.Add("Beschrijving", OracleDbType.NVarchar2).Value = description;

        OracleDataAdapter oda = new OracleDataAdapter(cmd);
        cmd.ExecuteNonQuery();

        string query = "Select * from Content_ where Uploaderid = " + uploaderid + " and Titel = '" + titel + "' and Beschrijving = '" + description + "'";
        OracleDataReader dbr = DBManager.ExecuteQuery(query);
        if (!dbr.HasRows)
        {
            return -1;
        }
        else while(dbr.Read())
        {
            return Convert.ToInt32(dbr["ContentID"].ToString());
        }
        return -1;
    }
    //Uploads a piece of contents corresponding headers
    static public void Content_UploadHeaders(int contentid, string titel, int soortcontent, string text, string path)
    {
        OracleCommand cmd = DBManager.ExecuteProcedure("Content_CreateHeader");

        cmd.Parameters.Add("contentid", OracleDbType.Int32).Value = contentid;
        cmd.Parameters.Add("Titel", OracleDbType.NVarchar2).Value = titel;
        cmd.Parameters.Add("SOORTCONTENT", OracleDbType.Int32).Value = soortcontent;
        cmd.Parameters.Add("tekst", OracleDbType.NVarchar2).Value = text;
        cmd.Parameters.Add("path", OracleDbType.NVarchar2).Value = path;

        OracleDataAdapter oda = new OracleDataAdapter(cmd);
        cmd.ExecuteNonQuery();
    }
    //Creates a new comment under a piece of content
    static public void Content_CreateComment(int contentid, int plaatserid, int commentonid, string plaatje, string beschrijving)
    {
        string qprt = contentid + ", " + plaatserid + ", " + commentonid + ", '" + plaatje + "', '" + beschrijving;
        if(commentonid == -1)
            qprt = contentid + ", " + plaatserid + ", null, '" + plaatje + "', '" + beschrijving;
        string query = "insert into comment_(contentid, plaatserid, commentonid, plaatje, beschrijving) values(" + qprt + "')";
        OracleDataReader dbr = DBManager.ExecuteQuery(query);
    }
    //Uploads the tags linked to a specific piece of content
    static public void Content_UploadTags(List<Tag> tags, Content content)
    {
        if(tags == null || content == null)
            return;
        for(int i=0; i<tags.Count; i++)
        {
            string query = "insert into TagLink(tagid, contentid) values(" + tags[i].TagID + ", " + content.ContentID + ")";
            OracleDataReader dbr = DBManager.ExecuteQuery(query);
        }
    }
    //Fetches a piece of content by the content id
    static public Content Content_Fetch(int contentid)
    {
        if (!Content_Exists(contentid))
            return null;
        Content content = null;

        string query = "Select * from Content_ where ContentID = '" + contentid + "'";
        OracleDataReader dbr = DBManager.ExecuteQuery(query);
        if (!dbr.HasRows)
        {
            return null;
        }
        string username = "";
        int thumbs = 0;
        int favorites = 0;
        string query2 = "select g.Gebruikersnaam, COALESCE(SUM(ct.Rating), 0) as conthumbs from Gebruiker g left join Content_ co on co.UploaderID = g.GebruikersID left join ContentThumb ct on ct.ContentID= co.ContentID where co.ContentID = " + contentid + " group by g.Gebruikersnaam";
        OracleDataReader dbr2 = DBManager.ExecuteQuery(query2);
        if(dbr2.HasRows)
        {
            while(dbr2.Read())
            {
                username = dbr2["Gebruikersnaam"].ToString();
                thumbs = Convert.ToInt32(dbr2["conthumbs"].ToString());
            }
        }
        query2 = "select COALESCE(Count(gebruikersid), 0) as Favorites from Favorite where ContentID = " + contentid;
        dbr2 = DBManager.ExecuteQuery(query2);
        if(dbr2.HasRows)
        {
            while(dbr2.Read())
            {
                favorites = Convert.ToInt32(dbr2["Favorites"].ToString());
            }
        }

        while( dbr.Read())
        {
            content = new Content(Convert.ToInt32(dbr["contentid"].ToString()), Convert.ToInt32(dbr["uploaderid"].ToString()), dbr["titel"].ToString(), dbr["beschrijving"].ToString(), Convert.ToInt32(dbr["views_"].ToString()), dbr["datum"].ToString(), username, thumbs, favorites);
        }
        return content;
    }

    //Fetches the headers belonging to a piece of content
    static public List<Header> Content_FetchHeaders(Content content)
    {
        if (!Content_Exists(content.ContentID))
            return null;

        string query = "select * from Header_ where contentid = " + content.ContentID + " order by headerid";
        OracleDataReader dbr = DBManager.ExecuteQuery(query);
        if (!dbr.HasRows)
        {
            return null;
        }

        List<Header> headers = new List<Header>();

        while(dbr.Read())
        {
            headers.Add(new Header(Convert.ToInt32(dbr["headerid"].ToString()), Convert.ToInt32(dbr["headerid"].ToString()), dbr["titel"].ToString(), Convert.ToInt32(dbr["soortcontent"].ToString()), dbr["tekst"].ToString(), dbr["path"].ToString()));
        }
        return headers;
    }
    //Fetches the comments belonging to a piece of content
    static public List<Comment> Content_FetchComments(Content content)
    {
        if (!Content_Exists(content.ContentID))
            return null;
        string query = "select cm.commentid, co.contentid, cm.plaatserid, g.Gebruikersnaam, cm.commentonid, COALESCE(SUM(ct.Rating), 0) as Thumbs, cm.Plaatje, cm.Beschrijving from CommentThumb ct, Content_ co, Comment_ cm, Gebruiker g where ct.CommentID(+) = cm.CommentID and cm.PlaatserID = g.GebruikersID and co.ContentID = cm.ContentID and co.ContentID = " + content.ContentID + " group by cm.commentid, co.contentid, cm.plaatserid, g.Gebruikersnaam, cm.commentonid, cm.Plaatje, cm.Beschrijving";
        OracleDataReader dbr = DBManager.ExecuteQuery(query);

        if (!dbr.HasRows)
        {
            return null;
        }
        List<Comment> comments = new List<Comment>();

        while(dbr.Read())
        {
            comments.Add(new Comment(dbr["commentid"].ToString(), dbr["contentid"].ToString(), dbr["plaatserid"].ToString(), dbr["gebruikersnaam"].ToString(), dbr["commentonid"].ToString(), dbr["thumbs"].ToString(), dbr["plaatje"].ToString(), dbr["beschrijving"].ToString()));
        }
        return comments;
    }
    //Fetches a list of tags belonging to a piece of content
    static public List<Tag> Content_FetchTags(Content content)
    {
        if (!DBHandler.Content_Exists(content.ContentID))
            return null;
        string query = "select t.tagid, t.naam from Tag t join TagLink tl on t.tagid = tl.tagid where tl.contentid= " + content.ContentID;
        OracleDataReader dbr = DBManager.ExecuteQuery(query);
        if (!dbr.HasRows)
            return null;
        List<Tag> tags = new List<Tag>();
        while(dbr.Read())
        {
            tags.Add(new Tag(Convert.ToInt32(dbr["tagid"].ToString()), dbr["naam"].ToString()));
        }
        return tags;
    }
    //Creates a new tag
    static public void Content_CreateNewTag(string name)
    {
        if(name == null)
            return;
        string query = "insert into Tag(naam) values ('" + name + "')";
        OracleDataReader dbr = DBManager.ExecuteQuery(query);
    }
    //Checks if a tag alreadt exists
    static public bool Content_TagExists(string name)
    {
        string query = "select * from tag where naam = '" + name + "'";
        OracleDataReader dbr = DBManager.ExecuteQuery(query);
        if (dbr.HasRows)
            return true;
        else return false;
    }
    //Fetches a list of all the tags currently in the database
    static public List<Tag> Content_FetchAllTags()
    {
        string query = "select * from Tag";
        OracleDataReader dbr = DBManager.ExecuteQuery(query);
        if (!dbr.HasRows)
            return null;
        List<Tag> tags = new List<Tag>();
        while(dbr.Read())
        {
            tags.Add(new Tag(Convert.ToInt32(dbr["tagid"].ToString()), dbr["naam"].ToString()));
        }
        return tags;
    }
    //Frontpage related:
    //Returns the datatable that is linked to the top gridview on the frontpage
    static public DataTable Content_FrontpageDataSource()
    {
        string query = "select co.ContentID, co.Titel, COALESCE(SUM(ct.Rating),0) as Thumbs, COALESCE(COUNT(com.ContentID),0) as Comments from Content_ co left join ContentThumb ct on co.ContentID = ct.ContentID left join Comment_ com on co.ContentID = com.ContentID group by co.ContentID, co.Titel order by Thumbs desc";
        OracleDataAdapter oda = new OracleDataAdapter(query, DBManager.Connection);
        DataTable ds = new DataTable();
        oda.Fill(ds);
        return ds;
    }
    //Returns the datatable that is linked to the top gridview on the frontpage
    static public DataTable Content_FrontpageMainDataSource()
    {
        string query = "select co.Titel, COALESCE(SUM(ct.Rating),0) as Thumbs, COALESCE(COUNT(com.ContentID),0) as Comments, Views_ as Views from Content_ co left join ContentThumb ct on co.ContentID = ct.ContentID left join Comment_ com on co.ContentID = com.ContentID group by co.Titel, co.Views_ order by Thumbs desc";
        OracleDataAdapter oda = new OracleDataAdapter(query, DBManager.Connection);
        DataTable ds = new DataTable();
        oda.Fill(ds);
        return ds;
    }
}