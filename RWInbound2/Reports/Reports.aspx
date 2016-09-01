<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="RWInbound2.Reports.Reports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="site-title">
        Reports
    </div>
    <asp:Button ID="btnErrorLogReport" runat="server" Text="Error Log" OnClick="btnErrorLogReport_Click" />

</asp:Content>
