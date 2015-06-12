using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for User
/// </summary>
public class User
{
    private string aco; //Date the account was created on in string format
    private string lio; //Last date the user logged in in string format
    private string rlevel; //Rights of the user in string format

    public int Id { get; set; } //ID of the user
    public string Username { get; set; } //Username of the user
    public string Email { get; set; } //Emailadres of the user
    public string Password {get; set;} //Users password
    public int Age {get; set;} //Users age
    public string AccountCreatedOn {get; set;} //Date the account was created on in string format
    public string LastLogin {get; set;} //Date the account was last logged in
    public int ProfilePrivacy {get; set;} //The level of profileprivacy the user has set
    public int MessagePrivacy {get; set;} //The level of message privacy the user has set
    public int MassMessagePrivacy {get; set;} //The level of massmessage privacy the user has set
    public int Level {get; set;} //The level of the user
    public string Rightslevel {get; set;} //The rights of a user in string format

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