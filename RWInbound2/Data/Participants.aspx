<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Participants.aspx.cs" Inherits="RWInbound2.Data.Participants" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

        <asp:Label ID="Label1" runat="server" Text="Participants"></asp:Label>

    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Tahoma" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="952px">
        <LocalReport ReportPath="Data\Participants.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources>
        </LocalReport>
        </rsweb:ReportViewer>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RiverwatchDEV %>" 
            SelectCommand="SELECT * FROM [ParticipantswithOrgName] ORDER BY [LastName], [FirstName]"></asp:SqlDataSource>
</asp:Content>
