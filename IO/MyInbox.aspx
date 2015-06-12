<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyInbox.aspx.cs" Inherits="MyInbox" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btnHome" runat="server" Text="Home" OnClick="btnHome_Click" />
        <h1>Compose new message:</h1>
        Titel: <asp:TextBox ID="tbTitel" runat="server"></asp:TextBox>
        <br />
        Reciever: <asp:TextBox ID="tbReciever" runat="server"></asp:TextBox>
        <br />
        <asp:TextBox ID="tbText" runat="server" Height="100px" TextMode="MultiLine"></asp:TextBox><asp:Button ID="btnSend" runat="server" Text="Send message" OnClick="btnSend_Click" />
    </div>
    <div>
        <h1>Inbox:</h1>
    </div>
    <div>
        <h2>Sent messages:</h2>
        <asp:Panel ID="pnlRecieved" runat="server"></asp:Panel>
        <h2>Recieved messages:</h2>
        <asp:Panel ID="pnlSent" runat="server"></asp:Panel>
    </div>
    </form>
</body>
</html>
