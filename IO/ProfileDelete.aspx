<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProfileDelete.aspx.cs" Inherits="ProfileDelete" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h1>Delete Account: 
        <asp:Label ID="lblUsername" runat="server" Text="USERNAME "></asp:Label>
        Confirmation.
        </h1>
    <p>Are you sure you want to delete your accounnt? Enter your login details below to confirm.</p>
    <p>Username: <asp:TextBox ID="tbUsername" runat="server">Username</asp:TextBox>
        </p>
        <p>Password: 
            <asp:TextBox ID="tbPassword" runat="server" TextMode="Password">password</asp:TextBox>
        </p>
        <p>
            <asp:Button ID="btnConfirm" runat="server" Text="Confirm" OnClick="btnConfirm_Click" />
            <asp:Button ID="btmCancel" runat="server" Text="Cancel" OnClick="btmCancel_Click" />
        </p>
        <p>
            <asp:Label ID="lblNotification" runat="server" Text=" "></asp:Label>
        </p>
    </div>
    </form>
</body>
</html>
