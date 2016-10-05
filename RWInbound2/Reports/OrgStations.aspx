﻿<%@ Page Title="Organization Stations" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrgStations.aspx.cs" Inherits="RWInbound2.Reports.OrgStations" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <hgroup>
        <h3><%: Page.Title %></h3>
    </hgroup>

    <div class="label-placement">
        <asp:Label ID="ErrorLabel" CssClass="label-error" runat="server" />               
    </div>
    <div class="label-placement">
            <asp:Label ID="SuccessLabel" CssClass="label-success" runat="server" />
    </div>
    <br />
    <p>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                Search By Organization Name:
                <asp:TextBox ID="orgNameSearch" 
                    AutoPostBack="true"
                    runat="server"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="Search" Height="31px" OnClick="btnSearch_Click" />
                <asp:Button ID="btnSearchRefresh" runat="server" Text="Reset Search" Height="31px" OnClick="btnSearchRefresh_Click" />
                <ajaxToolkit:AutoCompleteExtender 
                    ID="tbSearch_AutoCompleteExtender" 
                    runat="server" 
                    BehaviorID="tbSearch_AutoCompleteExtender" 
                    DelimiterCharacters=""  
                    ServiceMethod="SearchForOrgName"             
                    TargetControlID="orgNameSearch"
                    MinimumPrefixLength="2"
                    CompletionInterval="100" 
                    EnableCaching="false" 
                    CompletionSetCount="10">
                </ajaxToolkit:AutoCompleteExtender>                
            </ContentTemplate>
        </asp:UpdatePanel>         
    </p>
<br />
<rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="475px" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="957px">
    <LocalReport ReportPath="Reports\OrgStations.rdlc">
        <DataSources>
            <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
        </DataSources>
    </LocalReport>
</rsweb:ReportViewer>
<asp:SqlDataSource ID="SqlDataSource1" 
                    runat="server" 
                    ConnectionString="<%$ ConnectionStrings:RiverWatchDEV %>" 
                    SelectCommand="SELECT [OrganizationName], [StationNumber], [StationName], [River] FROM [OrgStationsView] ORDER BY [OrganizationName], [StationName]"></asp:SqlDataSource>
</asp:Content>