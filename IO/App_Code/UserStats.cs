using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UserStats
/// </summary>
public class UserStats : User
{
    public int ConThumbs;
    public int ComThumbs;
    public int Subscribers;
    public int ConViews;
    public int Favorites;
    public int CommentsMade;
    public UserStats(int id, string username, string email, int age, string aco, string lio, int level, string rlevel, int conthumbs, int comthumbs, int subscribers, int conviews, int favorites, int comsmade) : base(id, username, email, age, aco, lio, level, rlevel)
	{
        ConThumbs = conthumbs;
        ComThumbs = comthumbs;
        Subscribers = subscribers;
        ConViews = conviews;
        Favorites = favorites;
        CommentsMade = comsmade;
	}

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