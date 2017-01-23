<%@ Page Title="Organizations" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Organizations.aspx.cs" Inherits="RWInbound2.Reports.Organizations" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Label ID="Label1" runat="server" CssClass="PageLabel" Text="All Organizations"></asp:Label>
   
     <div class="label-placement">
        <asp:Label ID="ErrorLabel" CssClass="label-error" runat="server" />               
    </div>
    <div class="label-placement">
            <asp:Label ID="SuccessLabel" CssClass="label-success" runat="server" />
    </div>
    <br />
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" ShowPrintButton="False" Font-Names="Verdana" Font-Size="8pt" Width="1072px" Height="551px" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
        <LocalReport ReportPath="Public\AllOrgs.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSourceAllOrgs" Name="DataSet1" />
            </DataSources>
        </LocalReport>
 
</rsweb:ReportViewer>
    <asp:SqlDataSource ID="SqlDataSourceAllOrgs" runat="server" 
        ConnectionString="<%$ ConnectionStrings:RiverWatchDEV %>" 
        SelectCommand="SELECT  [OrganizationName], [OrganizationType], [City], [State], [Zip], [WaterShed] FROM [organization] order by OrganizationName"></asp:SqlDataSource>
</asp:Content>
