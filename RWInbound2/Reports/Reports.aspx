<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="RWInbound2.Reports.Reports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="site-title">
        Reports
    </div>
    <asp:Button ID="btnErrorLogReport" runat="server" Text="Error Log" OnClick="btnErrorLogReport_Click" />
    <br />
    <asp:Button ID="btnStationsWithGauges" runat="server" Text="StationsWithGauges" OnClick="btnStationsWithGauges_Click" />
    <br />
    <asp:Button ID="btnOrgPerformance" runat="server" Text="OrgPerformance" />
        <br />
    <asp:Button ID="btnLachatNoBC" runat="server" Text="Lachat BC not Entered" OnClick="btnLachatNoBC_Click" />
    <br />
    <asp:Button ID="btnOrgStatus" runat="server" Text="OrgStatus" OnClick="btnOrgStatus_Click" />
</asp:Content>
