<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Validation.aspx.cs" Inherits="RWInbound2.Validation.Validation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <p class="pageHeader">Incoming Validation </p>
    <div >
    <table  style="width: 100%">
        <%--<tr>
            <td style="width: 7px">&nbsp;</td>
            <td style="width: 201px">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>--%>
        <tr>
            <td >&nbsp;</td>
            <td >
    <asp:Button ID="btnICPBlanks"   runat="server" OnClick="btnICPBlanks_Click" Text="Step #1 ICP Blanks" Width="170px" Font-Bold="True" Font-Names="Calibri" Font-Size="16px"  />
            </td>
            <td>
                <asp:Label ID="lblICPBlanks" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td >&nbsp;</td>
            <td >
        <asp:Button ID="btnICPDups"  runat="server" Text="Step #2 ICP Duplicates" OnClick="btnICPDups_Click"  Width="170px"  Height="23px" Font-Bold="True" Font-Names="Calibri" Font-Size="16px"  />
            </td>
            <td>
                <asp:Label ID="lblICPDups" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 27px">&nbsp;</td>
            <td style="width: 189px">
        <asp:Button ID="btnICPSamples"   runat="server" Text="Step #3 ICP Normals" OnClick="btnICPSamples_Click"  Width="170px" Font-Bold="True" Font-Names="Calibri" Font-Size="16px"   />
            </td>
            <td>
                <asp:Label ID="lblICPSamples" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 27px">&nbsp;</td>
            <td style="width: 189px">
    <asp:Button ID="btnField" runat="server"   Text="Field"  Width="170px" Font-Bold="True" Font-Names="Calibri" Font-Size="16px"  />
            </td>
            <td>
                <asp:Label ID="lblFieldSamples" runat="server" Text="There are XX inbound samples"></asp:Label>
            &nbsp;- Example</td>
        </tr>
        <tr>
            <td style="width: 27px">&nbsp;</td>
            <td style="width: 189px">
    <asp:Button ID="btnLachet" runat="server"  Text="Lachet"  Width="170px" Font-Bold="True" Font-Names="Calibri" Font-Size="16px"   />
            </td>
            <td>
                <asp:Label ID="lblLachet" runat="server" Text="There are XX inbound samples"></asp:Label>
            &nbsp;- Example</td>
        </tr>
    </table>

        </div>

    <p>
        &nbsp;</p>
<p>
    &nbsp;</p>


</asp:Content>
