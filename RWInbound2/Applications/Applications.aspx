<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Applications.aspx.cs" Inherits="RWInbound2.Applications" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <p class="site-title">
    Applications
        </p>
    <p>
        <asp:Button ID="btnLatchet" runat="server" Text="STORET" />
    </p>
    <p>
        <asp:Button ID="Button1" runat="server" Text="AWQMS" />
    </p>
    <p>
        <asp:Button ID="Button2" runat="server" Enabled="False" Text="Gap Exports" />
    </p>
    <p>
        <asp:Button ID="Button3" runat="server" Enabled="False" Text="EDAS" />
    </p>
    <p>
        <asp:Button ID="Button4" runat="server" Text="More" />
    </p>
    <p>
        &nbsp;</p>


</asp:Content>
