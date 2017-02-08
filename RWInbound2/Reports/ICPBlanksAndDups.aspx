<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ICPBlanksAndDups.aspx.cs" Inherits="RWInbound2.Reports.ICPBlanksAndDups" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <asp:Label ID="Label1" runat="server" CssClass="PageLabel" Text="ALL ICP Blanks And Dups Report"></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" ></asp:Label>
        <br />
        <br />
    <br />

    <asp:Button ID="btnRun" CssClass="adminButton " runat="server" Text="RUN" Height="26px" OnClick="Button1_Click" Width="77px" />
        <br />
        <br />
      <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="908px"  Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" 
                    WaitMessageFont-Size="14pt" ShowPrintButton="False" Height="497px">
                    <LocalReport ReportPath="Public\RawSamplesAll.rdlc">
                        <DataSources>
                            <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
                        </DataSources>
                    </LocalReport>
                </rsweb:ReportViewer>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server"  ConnectionString="<%$ ConnectionStrings:RiverWatchDEV %>" >

            
        </asp:SqlDataSource>
</asp:Content>
