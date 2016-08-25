<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="RWInbound2.Edit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p class="site-title">
    Edit Data Tables:</p>
<p>
    <asp:Label ID="lblWelcome" Text="Welcome Default" runat="server"></asp:Label>
</p>
    <p>
    <asp:Button ID="btnOrganizations" runat="server" OnClick="btnOrganizations_Click" Text="Organizations" />
</p>
    <p>
        <asp:Button ID="brnLimits" runat="server" OnClick="brnLimits_Click" Text="Measurement Limits" />
</p>
    <p>
        <asp:Button ID="btnGear" runat="server" OnClick="btnGear_Click" Text="Gear" />
</p>
    <p>
        <asp:Button ID="btnActivityCategories" runat="server" OnClick="btnActivityCategories_Click" Text="Activity Categories" />
</p>
    <p>
        <asp:Button ID="btnActivityTypes" runat="server" OnClick="btnActivityTypes_Click" Text="Activity Types" />
</p>
</asp:Content>
