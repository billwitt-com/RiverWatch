<%@ Page Title="Participants Primary Contacts" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ParticipantsPrimaryContacts.aspx.cs" Inherits="RWInbound2.Reports.ParticipantsPrimaryContacts" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
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
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="1072px" Height="551px" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
        <LocalReport ReportPath="Reports\ParticipantsPrimaryContacts.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="Data Source=tcp:corw.database.windows.net,1433;Initial Catalog=RiverWatch;Persist Security Info=False;User ID=riverwatchadmin;Password=XkYZk2ul6fp4%;MultipleActiveResultSets=False;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False" 
        SelectCommand="SELECT * FROM [ParticipantsView] WHERE Active=1 AND PrimaryContact=1 ORDER BY FirstName"
        ProviderName="System.Data.SqlClient" ></asp:SqlDataSource>
</asp:Content>
