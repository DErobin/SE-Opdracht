using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for User
/// </summary>
public class User
{
    private string aco;
    private string lio;
    private string rlevel;

    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password {get; set;}
    public int Age {get; set;}
    public string AccountCreatedOn {get; set;}
    public string LastLogin {get; set;}
    public int ProfilePrivacy {get; set;}
    public int MessagePrivacy {get; set;}
    public int MassMessagePrivacy {get; set;}
    public int Level {get; set;}
    public string Rightslevel {get; set;}

	public User(int id, string username, string email, string password, int age, string aco, string lio, 
        int ppriv, int mpriv, int mmpriv, int level, string rlevel)
    {
        Id = id;
        Username = username;
        Email = email;
        Password = password;
        Age = age;
        AccountCreatedOn = aco;
        LastLogin = lio;
        ProfilePrivacy = ppriv;
        MessagePrivacy = mpriv;
        MassMessagePrivacy = mmpriv;
        Level = level;
        Rightslevel = rlevel;
	}

    public User(int id, string username, string email, int age, string aco, string lio, int level, string rlevel)
    {
        this.Id = id;
        this.Username = username;
        this.Email = email;
        this.Age = age;
        this.AccountCreatedOn = aco;
        this.LastLogin = lio;
        this.Level = level;
        this.Rightslevel = rlevel;
    }

    public User(User user)
    {
        this.Id = user.Id;
        this.Username = user.Username;
        this.Email = user.Email;
        this.Age = user.Age;
        this.AccountCreatedOn = user.AccountCreatedOn;
        this.LastLogin = user.LastLogin;
        this.Level = user.Level;
        this.Rightslevel = user.Rightslevel;
    }
}