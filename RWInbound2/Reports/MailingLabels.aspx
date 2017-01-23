<%@ Page Title="Mailing Labels Report" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MailingLabels.aspx.cs" Inherits="RWInbound2.Reports.MailingLabels" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <br />

    <asp:Label ID="Label1" runat="server" CssClass="PageLabel" Text="Mail Labels"></asp:Label>
    <br />

    <asp:RadioButtonList ID="RadioButtonList1"   runat="server">
         <asp:ListItem   Selected="True">All Participants</asp:ListItem>
        <asp:ListItem>Active Orgs and Their Active Primary Contacts</asp:ListItem>
        <asp:ListItem>Participants - Active Only</asp:ListItem>         
    </asp:RadioButtonList>


    <asp:Button ID="btnSelect" runat="server" CssClass="adminButton" Text="Select" OnClick="btnSelect_Click" />


    <br />


    <br />
    <rsweb:ReportViewer ID="ReportViewer1" PageCountMode="Actual" runat="server" Width="918px" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
        <LocalReport ReportPath="Reports\Mlables.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources>
        </LocalReport>

    </rsweb:ReportViewer>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RiverWatchDEV %>" >
        </asp:SqlDataSource>
</asp:Content>

<%--SelectCommand="SELECT * FROM [ParticipantswithOrgName] WHERE Active=1 AND PrimaryContact=1 ">--%>
