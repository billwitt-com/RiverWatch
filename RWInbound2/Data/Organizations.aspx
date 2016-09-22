<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Organizations.aspx.cs" Inherits="RWInbound2.Data.Organizations" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Organizations"></asp:Label>

    <rsweb:ReportViewer ID="ReportViewer1"  runat="server" ExportContentDisposition="AlwaysInline" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="887px">
        <LocalReport ReportPath="Data\Org.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSetOrgs" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RiverwatchDEV %>" SelectCommand="SELECT * FROM [organization]"></asp:SqlDataSource>
    <asp:EntityDataSource ID="EntityDataSourceOrgs" runat="server">
    </asp:EntityDataSource>
</asp:Content>
