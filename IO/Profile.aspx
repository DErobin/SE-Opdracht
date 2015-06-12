<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Profile.aspx.cs" Inherits="MyAccount" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>

        <asp:Button ID="btnHome" runat="server" Text="Home" OnClick="btnHome_Click" />
        <br />
        <asp:Button ID="btnEditAccountDetails" runat="server" OnClick="btnEditAccountDetails_Click" Text="Edit Account Details" />
        <br />
        <asp:Button ID="btnWarnAccount" runat="server" Text="Issue warning" />
        <br />
        <asp:DropDownList ID="ddlRightsLevel" runat="server">
            <asp:ListItem>Normal</asp:ListItem>
            <asp:ListItem>Moderator</asp:ListItem>
            <asp:ListItem>Administrator</asp:ListItem>
        </asp:DropDownList>
        <asp:Button ID="btnChangeRightsLevel" runat="server" Text="Edit" OnClick="btnChangeRightsLevel_Click" />
        <br />
        <asp:Button ID="btnDeleteAccount" runat="server" OnClick="btnDeleteAccount_Click" Text="Delete Account" />

    </div>
    <div>
        <h1>Account Details</h1>
    </div>
    <div>
        <h1>
            <asp:Label ID="lblUsermame" runat="server" Text="USERNAME"></asp:Label>
        </h1>
        <h2>Personal info</h2>
        <p>Account created on: <asp:Label ID="lblAccountCreatedOn" runat="server" Text="DATUM"></asp:Label>
        </p>
        <p>Last Login: 
            <asp:Label ID="lblLastLogin" runat="server" Text="DATUM"></asp:Label>
        </p>
        <h2>FunnyJunk career stats</h2>
        <p>Content Thumbs: 
            <asp:Label ID="lblContentThumbs" runat="server" Text="###"></asp:Label>
        </p>
        <p>Comment Thumbs: 
            <asp:Label ID="lblCommentThumbs" runat="server" Text="###"></asp:Label>
        </p>
        <p>Level: 
            <asp:Label ID="lblLevel" runat="server" Text="###"></asp:Label>
        </p>
        <p>User permissions: 
            <asp:Label ID="lblUserPermissions" runat="server" Text="CHAR"></asp:Label>
        </p>
        <p>Subscribers: 
            <asp:Label ID="lblSubscribers" runat="server" Text="###"></asp:Label>
        </p>
        <p>Content views: 
            <asp:Label ID="lblContentViews" runat="server" Text="###"></asp:Label>
        </p>
        <p># of favorites: 
            <asp:Label ID="lblFavorites" runat="server" Text="###"></asp:Label>
        </p>
        <p>Total comments made: 
            <asp:Label ID="lblComments" runat="server" Text="###"></asp:Label>
        </p>
    </div>
    <div>
        <h1>
            <asp:Label ID="Label1" runat="server" Text="Funny Pictures"></asp:Label>
        </h1>
        <p>meer shit hier</p>
    </div>
    </form>
    </body>
</html>
