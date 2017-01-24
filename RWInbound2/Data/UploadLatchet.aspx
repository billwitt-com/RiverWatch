<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UploadLatchet.aspx.cs"  Inherits="RWInbound2.Data.UploadLatchet" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label2" runat="server" Text="Upload Latchet Data from CSV file"  CssClass="PageLabel"></asp:Label>
    <br />
    <br />
    <asp:Label ID="Label3" runat="server" Text="Note: This also updates nutrient data in the data base"></asp:Label>
    <br />
    <asp:Label ID="Label4" runat="server" Text="And may take a minute or two - Please be patient"></asp:Label>

    <br />
    <br />
    <br />
    <asp:Panel ID="pnlFileControl" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Choose File"></asp:Label>

    <asp:FileUpload ID="FileUpload1" OnLoad="FileUpload1_Load"    runat="server" />

    <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
        <br />
        <br />
        View Existing Batch Numbers
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="View" />
        <asp:DropDownList ID="ddlBatches" runat="server">
        </asp:DropDownList>
        <br />
    <br />
    <asp:Label ID="lblStatus" runat="server"></asp:Label>


    <br />
    <br />
        </asp:Panel>

    <asp:Panel ID="pnlUploadComplete" runat="server">

        <asp:Label ID="lblUploadComplete" runat="server" Text=""></asp:Label>


    </asp:Panel>
</asp:Content>
