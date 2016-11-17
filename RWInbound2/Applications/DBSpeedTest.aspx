<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DBSpeedTest.aspx.cs" Inherits="RWInbound2.DBSpeedTest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Label ID="lblHeader" runat="server" CssClass="PageLabel" Text="Simple Speed test for Data Base response times"></asp:Label>
    <br />
    <br />
    <asp:Label ID="lblLoopCount" runat="server" Text="Enter the number of loops:  "></asp:Label>
    <asp:TextBox ID="tbLoopCount" Text ="20" runat="server"></asp:TextBox>

    <asp:Button ID="btnGo" runat="server" OnClick="btnGo_Click" Text="Start Test" />
    <br />

    <asp:Label ID="Label1" runat="server" Text="Test: Linq query to count entries in view ViewSoloNutrientDups using Azure"></asp:Label>
    <asp:Panel ID="pnlStuff" runat="server">
        <table style="width: 100%">
            <tr>
                <td style="width: 181px">
                    <asp:Label ID="lblAzLinqStart" runat="server" Text="Label"></asp:Label>
                </td>
                <td style="width: 140px">
                    <asp:Label ID="lblAZLinqEnd" runat="server" Text="Label"></asp:Label>
                </td>
                <td style="width: 234px">
                    <asp:Label ID="lblAzLinqTotal" runat="server" Text="Label"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblAzLinqCount" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
        </table>

    <asp:Label ID="Label2" runat="server" Text="Test: SQL query inboundsamples using Azure"></asp:Label>

        <table style="width: 100%">
            <tr>
                <td style="width: 181px">
                    <asp:Label ID="lblAzSqlStart" runat="server" Text="Label"></asp:Label>
                </td>
                <td style="width: 140px">
                    <asp:Label ID="lblAzSqlEnd" runat="server" Text="Label"></asp:Label>
                </td>
                <td style="width: 237px">
                    <asp:Label ID="lblAzSqlTotal" runat="server" Text="Label"></asp:Label>
                </td>
                                <td>
                    <asp:Label ID="lblAzSqlCount" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
        </table>

        <asp:Label ID="Label6" runat="server" Text="Test: Linq query to count entries in view ViewSoloNutrientDups using Remote SQL DB"></asp:Label>

    <table style="width: 100%">
        <tr>
            <td style="width: 181px">
                <asp:Label ID="lblReLinqStart" runat="server" Text="Label"></asp:Label>
            </td>
            <td style="width: 140px">
                <asp:Label ID="lblReLinqEnd" runat="server" Text="Label"></asp:Label>
            </td>
            <td style="width: 239px">
                <asp:Label ID="lblReLinqTotal" runat="server" Text="Label"></asp:Label>
            </td>

            <td>
                <asp:Label ID="lblReLinqCount" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
    </table>

    <asp:Label ID="Label13" runat="server" Text="Test: SQL query inboundsamples using Remote SQL DB"></asp:Label>
    <table style="width: 100%">
        <tr>
            <td style="width: 181px">
                <asp:Label ID="lblReSqlStart" runat="server" Text="Label"></asp:Label>
            </td>
            <td style="width: 140px">
                <asp:Label ID="lblReSqlEnd" runat="server" Text="Label"></asp:Label>
            </td>
            <td style="width: 242px">
                <asp:Label ID="lblReSqlTotal" runat="server" Text="Label"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblReSqlCount" runat="server" Text="Label"></asp:Label>
            </td>

        </tr>
    </table>

<%--        ** now do the local instance... --%>
           <%--     <asp:Label ID="Label3" runat="server" Text="Test: Linq query to count entries in view ViewSoloNutrientDups using Local SQL DB"></asp:Label>
        <table style="width: 100%">
        <tr>
            <td style="width: 181px">
                <asp:Label ID="lblLoLinqStart" runat="server" Text="Label"></asp:Label>
            </td>
            <td style="width: 140px">
                <asp:Label ID="lblLoLinqEnd" runat="server" Text="Label"></asp:Label>
            </td>
            <td style="width: 239px">
                <asp:Label ID="lblLoLinqTotal" runat="server" Text="Label"></asp:Label>
            </td>

            <td>
                <asp:Label ID="lblLoLinqCount" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
    </table>

    <asp:Label ID="Label8" runat="server" Text="Test: SQL query to count entries in view ViewSoloNutrientDups using LocaL SQL DB"></asp:Label>
    <table style="width: 100%">
        <tr>
            <td style="width: 181px">
                <asp:Label ID="lblLoSqlStart" runat="server" Text="Label"></asp:Label>
            </td>
            <td style="width: 140px">
                <asp:Label ID="lblLoSqlEnd" runat="server" Text="Label"></asp:Label>
            </td>
            <td style="width: 242px">
                <asp:Label ID="lblLoSqlTotal" runat="server" Text="Label"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblLoSqlCount" runat="server" Text="Label"></asp:Label>
            </td>

        </tr>
    </table>--%>
    </asp:Panel>


</asp:Content>
