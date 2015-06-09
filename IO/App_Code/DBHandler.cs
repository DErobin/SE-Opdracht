using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Data;
using System.Web.UI.WebControls;



/// <summary>
/// Summary description for DBHandler
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
        RECHTSNIVEAU"*/
        return false;
    }

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

    static public void User_Delete(User user)
    {
        OracleCommand cmd = DBManager.ExecuteProcedure("User_Delete");
        cmd.Parameters.Add("userid", OracleDbType.Int32).Value = user.Id;
        OracleDataAdapter oda = new OracleDataAdapter(cmd);
        cmd.ExecuteNonQuery();

    }
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

    //Content:

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
        string query2 = "select g.Gebruikersnaam, COALESCE(SUM(ct.Rating), 0) as conthumbs from Gebruiker g left join Content_ co on co.UploaderID = g.GebruikersID left join ContentThumb ct on ct.ContentID= co.ContentID where co.UploaderID = " + contentid + " group by g.Gebruikersnaam";
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
    //Frontpage related:
    static public DataTable Content_FrontpageDataSource()
    {
        string query = "select co.ContentID, co.Titel, COALESCE(SUM(ct.Rating),0) as Thumbs, COALESCE(COUNT(com.ContentID),0) as Comments from Content_ co left join ContentThumb ct on co.ContentID = ct.ContentID left join Comment_ com on co.ContentID = com.ContentID group by co.ContentID, co.Titel order by Thumbs desc";
        OracleDataAdapter oda = new OracleDataAdapter(query, DBManager.Connection);
        DataTable ds = new DataTable();
        oda.Fill(ds);
        return ds;
    }
    static public DataTable Content_FrontpageMainDataSource()
    {
        string query = "select co.Titel, COALESCE(SUM(ct.Rating),0) as Thumbs, COALESCE(COUNT(com.ContentID),0) as Comments, Views_ as Views from Content_ co left join ContentThumb ct on co.ContentID = ct.ContentID left join Comment_ com on co.ContentID = com.ContentID group by co.Titel, co.Views_ order by Thumbs desc";
        OracleDataAdapter oda = new OracleDataAdapter(query, DBManager.Connection);
        DataTable ds = new DataTable();
        oda.Fill(ds);
        return ds;
    }
}