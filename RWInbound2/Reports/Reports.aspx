﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="RWInbound2.Reports.Reports" %>
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
    <br />
    <asp:Button ID="btnOrgStations" runat="server" Text="OrgStations" OnClick="btnOrgStations_Click" />
    <br />
    <asp:Button ID="btnOrganizations" runat="server" Text="All Organizations" OnClick="btnOrganizations_Click" />
    <br />
    <asp:Button ID="btnParticipants" runat="server" Text="All Participants" OnClick="btnParticipants_Click" />
    <br />
    <asp:Button ID="btnStations" runat="server" Text="All Stations" OnClick="btnStations_Click" />
</asp:Content>
