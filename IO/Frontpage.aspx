<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frontpage.aspx.cs" Inherits="Frontpage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Button ID="btnHome" runat="server" OnClick="btnHome_Click" Text="Home" />
        <br />
        <asp:Button ID="btnChannels" runat="server" Text="Channels" OnClick="btnChannels_Click" />
        <br />
        <asp:Button ID="btnUploadContent" runat="server" OnClick="btnUploadContent_Click" Text="Upload content" />
        <br />
        <asp:Button ID="btnRegLog" runat="server" OnClick="btnRegLog_Click" Text="Register/ Login" />
    
        <br />
        <asp:Button ID="btnMyAccount" runat="server" Text="My Account" OnClick="btnMyAccount_Click" />
        <br />
        <asp:Button ID="btnInbox" runat="server" Text="Inbox" OnClick="btnInbox_Click" />
        <br />
        <asp:Label ID="lblLoggedIn" runat="server" Text="Currently logged in: -"></asp:Label>
    
    </div>
        <div>
        <asp:GridView ID="gvTopContent" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField ="ContentID" HeaderText ="ContentID" Visible="false" />
                <asp:TemplateField HeaderText ="Titel">
                    <ItemTemplate>
                        <asp:HyperLink runat="server" NavigateUrl='<%# Eval("ContentID", "~/ViewContent.aspx?ContentID={0}" ) %>'
                            Text='<%# Eval("Titel") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField ="Thumbs" HeaderText ="Thumbs" />
                <asp:BoundField DataField ="Comments" HeaderText ="Comments" />
            </Columns>
        </asp:GridView>
            <br />
        </div>
        <div>
        <div>
            <asp:Label ID="Label1" runat="server" Text="Sort by: "></asp:Label>
            <asp:DropDownList ID="ddlSortBy" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True">
                <asp:ListItem>Thumbs</asp:ListItem>
                <asp:ListItem>Views</asp:ListItem>
                <asp:ListItem>Comments</asp:ListItem>
            </asp:DropDownList>
            <asp:Label ID="Label2" runat="server" Text="Order by: "></asp:Label>
            <asp:DropDownList ID="ddlOrderBY" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True">
                <asp:ListItem>DESC</asp:ListItem>
                <asp:ListItem>ASC</asp:ListItem>
            </asp:DropDownList>
            <asp:GridView ID="gvMainContent" runat="server" OnSorting="gvMainContent_Sorting" AllowPaging="True" OnPageIndexChanging="gvMainContent_PageIndexChanging" PageSize="2" AutoGenerateColumns="true">
            </asp:GridView>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="kutzooi" />
        </div>
        </div>
    </form>
</body>
</html>
