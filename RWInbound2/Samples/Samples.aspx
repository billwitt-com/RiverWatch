<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Samples.aspx.cs" Inherits="RWInbound2.Samples.SamplesPage" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <p class="site-title">
        Samples</p>
    <table style="width: 100%">
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnNewSample" runat="server"  CssClass="smallButton" Text="New Sample" OnClick="btnNewSample_Click" />
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnAddMetalBarcode" runat="server" CssClass="smallButton" Text="Add Metals Barcode" OnClick="btnAddMetalBarcode_Click" />
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnAddNutrientBarcode" runat="server"  CssClass="smallButton" Text="Add Nutrient Barcode" OnClick="btnAddNutrientBarcode_Click"  />
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnAddBugs" runat="server" CssClass="smallButton" Text="Add Bugs" />
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>


    
</asp:Content>
