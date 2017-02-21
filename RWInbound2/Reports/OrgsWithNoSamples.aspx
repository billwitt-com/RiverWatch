<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrgsWithNoSamples.aspx.cs" Inherits="RWInbound2.Reports.OrgsWithNoSamples" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

                <asp:Label ID="Label1" runat="server" CssClass="PageLabel" Text="Organizations With No Samples"></asp:Label>
    

            <br />
            <br />
    <asp:Label ID="lblNoResults" runat="server" ></asp:Label>

    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" ShowPrintButton="False" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="1110px">
        <LocalReport   ReportPath="Reports\OrgsWithoutSamples.rdlc">
            <DataSources>
                <rsweb:ReportDataSource  DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources>
        </LocalReport>
                </rsweb:ReportViewer>


                <asp:SqlDataSource ID="SqlDataSource1" runat="server" OnSelected="SqlDataSource1_Selected" ConnectionString="<%$ ConnectionStrings:RiverWatchDEV %>" SelectCommand="SELECT * FROM [ViewOrgsWithNoSamples]"></asp:SqlDataSource>


            <br />
            <br />


</asp:Content>
