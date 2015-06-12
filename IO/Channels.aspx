<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Channels.aspx.cs" Inherits="Channels" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btnHome" runat="server" Text="Home" OnClick="btnHome_Click" />
    </div>
    <div>
    <h1>Channels:</h1>
    </div>
    <div>Add new channel
        <br />
        Name:
        <asp:TextBox ID="tbChannelName" runat="server"></asp:TextBox>
        <asp:Button ID="btnCreate" runat="server" Text="Create" OnClick="btnCreate_Click" />
    </div>
    <div>
        <asp:ListBox ID="lbChannels" runat="server"></asp:ListBox>
    </div>
    </form>
</body>
</html>
