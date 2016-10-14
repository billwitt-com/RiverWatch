<%@ Page Title="Stations" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Stations.aspx.cs" Inherits="RWInbound2.Reports.Stations" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
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
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="551px" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="1044px">
        <LocalReport ReportPath="Reports\Stations.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources>
        </LocalReport>
</rsweb:ReportViewer>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RiverWatchDEV %>" SelectCommand="SELECT [StationNumber], [River], [AquaticModelIndex], [WaterCode], [WaterBodyID], [StationType], [RWWaterShed], [StationQUAD], [Grid], [QuaterSection], [Section], [Range], [Township], [QUADI], [WQCCWaterShed], [HydroUnit], [EcoRegion], [StationName], [StationStatus], [Elevation], [WaterShedRegion], [Longtitude], [Latitude], [County], [State], [NearCity], [Move], [Description], [UTMX], [UTMY], [UserLastModified], [DateLastModified], [UserCreated], [DateCreated], [StateEngineering], [USGS], [Comments], [Region], [StoretUploaded] FROM [Station] ORDER BY [StationName]"></asp:SqlDataSource>
</asp:Content>
