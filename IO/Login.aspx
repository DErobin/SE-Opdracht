<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="height: 131px">
    
        Username:
        <asp:TextBox ID="tbUsername" runat="server"></asp:TextBox>
        <br />
        Password:
    
        <asp:TextBox ID="tbPassword" runat="server" TextMode="Password"></asp:TextBox>
    
        <br />
        <asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" Text="Login" />
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
        <p>
            <asp:Label ID="lblNotification" runat="server"></asp:Label>
        </p>
    
    </div>
        <div>
            E-Mail: <asp:TextBox ID="tbRegEmail" runat="server" TextMode="Email"></asp:TextBox>
            <br />
            Username:
            <asp:TextBox ID="tbRegUsername" runat="server"></asp:TextBox>
            <br />
            Password:
            <asp:TextBox ID="tbRegPassword" runat="server" TextMode="Password"></asp:TextBox>
            <br />
            <asp:Button ID="btnRegister" runat="server" OnClick="btnRegister_Click" Text="Register" />
            <br />
            <br />
            <asp:Label ID="lblRegNotification" runat="server"></asp:Label>
            <br />
        </div>
    </form>
</body>
</html>
