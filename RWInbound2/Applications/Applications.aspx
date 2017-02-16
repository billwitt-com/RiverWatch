<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Applications.aspx.cs" Inherits="RWInbound2.Applications.Applications" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    
    <asp:Label ID="Label1" runat="server" CssClass="PageLabel" Text="Applications"></asp:Label>
    <br />


    <table style="width: 100%">
        <tr>
            <td style="width: 328px">&nbsp;</td>
            <td style="width: 296px">&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 328px">
                <asp:Button ID="btnAWQMSChems" runat="server" Text="AWQMS Chemicals Download" CssClass="adminButton" OnClick="btnAWQMSChems_Click" />
            </td>
            <td style="width: 296px">
                <asp:Button ID="btnAWQMSStations" runat="server" CssClass="adminButton" Text="AWQMS Stations Download" Enabled="False" />
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 328px">
                <asp:Button ID="btnAWQMSPhysHab" runat="server" CssClass="adminButton" Text="AWQMS PhysHab Download"  Enabled="False" />
            </td>
            <td style="width: 296px">
                <asp:Button ID="btnAWQMSBugs" runat="server" Text="AWQMS Bugs Download " CssClass="adminButton"   Enabled="False"  />
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>


</asp:Content>
