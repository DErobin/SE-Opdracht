using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Contains some more detailed stats of a user for display in his profile
/// </summary>
public class UserStats : User
{
    public int ConThumbs; //Amount of thumbs a piece of content has
    public int ComThumbs; //Amount of thumbs the comments of a user have
    public int Subscribers; //Amount of subscribers the user has
    public int ConViews; //Amount of total content views the user has
    public int Favorites; //Amount of favorites the user has
    public int CommentsMade; //Amount of comments the user has made
    /*public UserStats(int id, string username, string email, int age, string aco, string lio, int level, string rlevel, int conthumbs, int comthumbs, int subscribers, int conviews, int favorites, int comsmade) : base(id, username, email, age, aco, lio, level, rlevel)
	{
        ConThumbs = conthumbs;
        ComThumbs = comthumbs;
        Subscribers = subscribers;
        ConViews = conviews;
        Favorites = favorites;
        CommentsMade = comsmade;
	}*/

    //Sets the stats of the object
    public void SetStats(int conthumbs, int comthumbs, int subscribers, int conviews, int favorites, int comsmade)
    {
        ConThumbs = conthumbs;
        ComThumbs = comthumbs;
        Subscribers = subscribers;
        ConViews = conviews;
        Favorites = favorites;
        CommentsMade = comsmade;
    }

    public UserStats(User user) : base (user)
    {

    }
}