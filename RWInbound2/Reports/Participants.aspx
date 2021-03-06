﻿<%@ Page Title="Partcipants" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Participants.aspx.cs" Inherits="RWInbound2.Reports.Participants" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <br />

    <asp:Label ID="Label1" runat="server" CssClass="PageLabel" Text="Participants Table"></asp:Label>

    <br />

    <div class="label-placement">
        <asp:Label ID="ErrorLabel" CssClass="label-error" runat="server" />               
    </div>
    <div class="label-placement">
            <asp:Label ID="SuccessLabel" CssClass="label-success" runat="server" />
    </div>
    <br />
<rsweb:ReportViewer ID="ReportViewer1" runat="server" PageCountMode="Actual" Font-Names="Verdana" ShowPrintButton="False" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="1072px" Height="551px">
    <LocalReport ReportPath="Reports\Participants.rdlc">
        <DataSources>
            <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
        </DataSources>
    </LocalReport>
</rsweb:ReportViewer>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RiverWatchDEV %>" 
    SelectCommand="SELECT [OrganizationName], [LastName], [FirstName], [Title], [YearSignedOn], [Phone], [Email], [Active], [MailPreference], [HomeEmail], [HomePhone], [Zip], [State], [City], [Address2], [Address1], [PrimaryContact], [Training], [DateCreated], [UserCreated], [DateLastModified], [UserLastModified], [Valid] FROM [ParticipantsView] ORDER BY [OrganizationName],[LastName]"></asp:SqlDataSource>
</asp:Content>
