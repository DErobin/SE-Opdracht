<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadContent.aspx.cs" Inherits="UploadContent" %>

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

        <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />

    </div>
    <div>
        <h1>Upload content: </h1>
        <p>Titel:
            <asp:TextBox ID="tbContentTitel" runat="server"></asp:TextBox>
        </p>
        <p>Beschrijving:
            <asp:TextBox ID="tbContentDescription" runat="server"></asp:TextBox>
        </p>
    </div>
    <div><h2>Customize headers:</h2></div>
    <div>

        Titel: <asp:TextBox ID="tbHeaderTitel" runat="server"></asp:TextBox>
        <br />
        Mediatype:
        <asp:DropDownList ID="ddlMediaType" runat="server" AutoPostBack="True">
            <asp:ListItem>Text</asp:ListItem>
            <asp:ListItem>Picture</asp:ListItem>
            <asp:ListItem>Video</asp:ListItem>
        </asp:DropDownList>
        <br />
        <asp:TextBox ID="tbHeaderInput" runat="server"></asp:TextBox>

    </div>
    <div>
        <h2>Customize Tags:</h2>
        <p>
            <asp:ListBox ID="lbTags" runat="server" OnSelectedIndexChanged="lbTags_SelectedIndexChanged"></asp:ListBox>
            <asp:Button ID="btnAddTag" runat="server" Text="Add" OnClick="btnAddTag_Click" />
            <asp:Button ID="btnRemove" runat="server" OnClick="btnRemove_Click" Text="Remove" />
            
        </p>
        <p><asp:TextBox ID="tbNewTag" runat="server">New Tag</asp:TextBox>
            <asp:Button ID="btnCreateTag" runat="server" OnClick="btnCreateTag_Click" Text="Create" />
        </p>
    </div>
    <div>

        <asp:Panel ID="pnlTags" runat="server">
        </asp:Panel>

    </div>
    </form>
</body>
</html>
