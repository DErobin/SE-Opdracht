<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SQLtest.aspx.cs" Inherits="SQLtest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Button ID="btnConnect" runat="server" OnClick="btnConnect_Click" Text="Connect" />
    
    </div>
        <asp:Label ID="lblTest" runat="server" Text="TEST"></asp:Label>
    </form>
</body>
</html>
