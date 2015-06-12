<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewContent.aspx.cs" Inherits="ViewContent" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btnHome" runat="server" Text="Home" OnClick="btnHome_Click" style="height: 26px" />
    </div>
    <div>
        <h1>
            <asp:Label ID="lblTitel" runat="server" Text="Titel"></asp:Label>
        </h1>
        <p>By:
            <asp:HyperLink ID="hlUsername" runat="server">Username</asp:HyperLink>
        </p>
    </div>
    <div>
        <asp:Button ID="btnEditContent" runat="server" Text="Edit content" />
        <asp:Button ID="btnRemoveContent" runat="server" Text="Remove content" />
        </div>
    <div>
        <asp:Panel ID="pnlContentControls" runat="server">
        </asp:Panel>
    </div>
    <div>
        <h3>Description: </h3>
        <asp:Label ID="lblConDescription" runat="server" Text="Description"></asp:Label>

    </div>
    <div>
        <p>Thumbs: <asp:Button ID="btnPosThumb" runat="server" Text="THU" OnClick="btnPosThumb_Click" />
            <asp:Label ID="lblThumbs" runat="server" Text="###"></asp:Label>
            <asp:Button ID="btnNegThumb" runat="server" Text="THD" OnClick="btnNegThumb_Click" />
        </p>
        <p>Views: <asp:Label ID="lblViews" runat="server" Text="###"></asp:Label>
        </p>
        <p>Favorited: 
            <asp:Label ID="lblFavorites" runat="server" Text="###"></asp:Label>
        </p>
        <p>Submitted: <asp:Label ID="lblDatum" runat="server" Text="##/##/##"></asp:Label>
        </p>
    </div>
    <div> Tags: 
        <asp:Panel ID="pnlTags" runat="server">
        </asp:Panel>
        </div>
    <div> Channel: </div>
    <div>
        <h1>Comments(<asp:Label ID="lblComAmount" runat="server" Text="###"></asp:Label>):
        </h1>
        <asp:Button ID="btnComSubmit" runat="server" Text="Submit" OnClick="btnComSubmit_Click" />
        <br />
        <br />
        <asp:TextBox ID="tbComText" runat="server" Height="112px" TextMode="MultiLine" Width="300px"></asp:TextBox>
        <br />
        <asp:TextBox ID="tbComPath" runat="server" Width="300px">PATH_OF_PICTURE</asp:TextBox>
    </div>
    <div>

        <asp:Panel ID="pnlComments" runat="server">
        </asp:Panel>

    </div>
    </form>
</body>
</html>
