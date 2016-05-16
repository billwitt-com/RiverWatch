<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Validation.aspx.cs" Inherits="RWInbound2.Validation.Validation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <p class="site-title">
    Validation</p>
<p>
    <asp:Button ID="btnICP" runat="server" OnClick="btnICP_Click" Text="ICP" />
</p>
<p>
    <asp:Button ID="btnField" runat="server" Text="Field" Enabled="False" />
</p>
<p>
    <asp:Button ID="btnLachet" runat="server" Text="Lachet" Enabled="False" />
</p>


</asp:Content>
