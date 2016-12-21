<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StationsByProject.aspx.cs" Inherits="RWInbound2.Reports.StationsByProject" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div class="site-title">
        <asp:Label ID="Label1" runat="server" CssClass =" PageLabel" Text="Stations By Project"></asp:Label>
    </div>
        <div class="label-placement">
        <asp:Label ID="ErrorLabel" CssClass="label-error" runat="server" />               
    </div>
    <div class="label-placement">
            <asp:Label ID="SuccessLabel" CssClass="label-success" runat="server" />
    </div>
    Select By:<asp:DropDownList ID="ddlProjects" runat="server">
        </asp:DropDownList>
        <asp:Button ID="btnSelect" runat="server" Text="Select" OnClick="btnSelect_Click" />
        <br />
        <br />
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" ShowPrintButton="False" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="1109px">
            <LocalReport ReportPath="Reports\StationsByProject.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>

<%--    SelectCommand="SELECT * FROM [ViewStationsByProject]"--%>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RiverWatchDEV %>" 
            ></asp:SqlDataSource>
        <br />

</asp:Content>
