<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="QAQCReport.aspx.cs" Inherits="RWInbound2.Reports.QAQCReport" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Label ID="Label1" runat="server"  CssClass="PageLabel" Text="QAQC Report"></asp:Label>



    <br />
    <br />
    <table style="width: 100%">
        <tr>
            <td class="rowId-charid-dsorder-textbox" style="width: 32px">&nbsp;</td>
            <td style="width: 88px">
                <asp:Label ID="Label2" runat="server" Text="Org Name: "></asp:Label>
            </td>
            <td style="width: 260px">
                <asp:TextBox ID="tbOrgName" runat="server"  Width="240px"></asp:TextBox>
                
            <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                TargetControlID="tbOrgName"
                CompletionSetCount="10"
                ServiceMethod="SearchOrgs"
                MinimumPrefixLength="2"
                CompletionInterval="100">
            </ajaxToolkit:AutoCompleteExtender>
            </td>
            <td style="width: 75px">
                <asp:Button ID="btnOrgName"  CssClass="adminButton" runat="server" Text="Select" OnClick="btnOrgName_Click" />
            </td>
            <td>
                <asp:Label ID="lblOrgNameMsg" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="rowId-charid-dsorder-textbox" style="width: 32px">&nbsp;</td>
            <td style="width: 88px">
                <asp:Label ID="Label3" runat="server" Text="Kit Number: "></asp:Label>
            </td>
            <td style="width: 260px">
                <asp:TextBox ID="tbKitNumber" runat="server"></asp:TextBox>
            </td>
            <td style="width: 75px">
                <asp:Button ID="btnKitNumber"  CssClass="adminButton" runat="server" Text="Select" OnClick="btnKitNumber_Click" />
            </td>
            <td>
                <asp:Label ID="lblKitNumMsg" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="rowId-charid-dsorder-textbox" style="width: 32px">&nbsp;</td>
            <td style="width: 88px">
                <asp:Label ID="Label4" runat="server" Text="Staton Num: "></asp:Label>
            </td>
            <td style="width: 260px">
                <asp:TextBox ID="tbStnNumber" runat="server"></asp:TextBox>
            </td>
            <td style="width: 75px">
                <asp:Button ID="btnStnNumber"  CssClass="adminButton" runat="server" Text="Select" OnClick="btnStnNumber_Click" />
            </td>
            <td>
                <asp:Label ID="lblStnNumMsg" runat="server"></asp:Label>
            </td>
        </tr>
    </table>



    <br />

<%--      SelectCommand="SELECT top 1 * FROM [QAQCView]">--%>
   <%-- <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RiverwatchDEV %>" SelectCommand="SELECT * FROM [QAQCView]"></asp:SqlDataSource>
   --%> <br />
    <rsweb:ReportViewer ID="ReportViewer1"   runat="server" Font-Names="Verdana" BackColor="White" Font-Size="12px" WaitMessageFont-Names="Verdana" Height="1200px" WaitMessageFont-Size="14pt" Width="953px">
        <LocalReport ReportPath="Reports\QAQCReport.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="QAQCDataSource" Name="DataSet1" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    <asp:SqlDataSource ID="QAQCDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:RiverwatchDEV %>"
        SelectCommand="SELECT top 0 * FROM [QAQCView]">
    </asp:SqlDataSource>
      
    <br />
    <br />
    <asp:GridView ID="GridView1" runat="server">
    </asp:GridView>
<%--    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server"></asp:ObjectDataSource>--%>
    <br />
    <br />
    <br />
    <br />



</asp:Content>
