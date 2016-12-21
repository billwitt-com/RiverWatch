<%@ Page Title="View Error Log" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewErrorLog.aspx.cs" Inherits="RWInbound2.Reports.ViewErrorLog" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   <hgroup>
        <h3><%: Page.Title %></h3>
    </hgroup>
    <br />
<rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" ShowPrintButton="False" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="1072px" Height="551px">
    <LocalReport ReportPath="Reports\ErrorLog.rdlc">
        <DataSources>
            <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
        </DataSources>
    </LocalReport>



</rsweb:ReportViewer>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RiverwatchDEV %>" 
        SelectCommand="SELECT * FROM [ErrorLog] ORDER BY [Date] DESC"></asp:SqlDataSource>
<p>
</p>
</asp:Content>
