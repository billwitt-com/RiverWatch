<%@ Page Title="Metal Bar Codes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MetalBarCodes.aspx.cs" Inherits="RWInbound2.Reports.MetalBarCodes" %>

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
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" ShowPrintButton="False" Font-Names="Verdana" Font-Size="8pt" Width="1072px" Height="551px" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
        <LocalReport ReportPath="Reports\MetalBarCodes.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RiverWatchDEV %>" SelectCommand="SELECT [SampleNumber], [NumberSample], [LabID], [Code], [Type], [Filtered], [ContainMetal], [SampleID], [UserLastModified], [DateLastModified], [UserCreated], [DateCreated], [AnalyzeDate], [LogDate], [Verified], [BoxNumber] FROM [MetalBarCode] ORDER BY [LabID]"></asp:SqlDataSource>
</asp:Content>
