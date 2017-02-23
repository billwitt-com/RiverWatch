<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UploadICP.aspx.cs" Inherits="RWInbound2.Data.UploadICP" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <asp:Label ID="Label2" runat="server" Text="Upload Inbound ICP Data from CSV file"  CssClass="PageLabel"></asp:Label>


    <br />
    <br />
    <br />
    <asp:Panel ID="pnlFileControl" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Choose File"></asp:Label>

    <asp:FileUpload ID="FileUpload1" OnLoad="FileUpload1_Load"    runat="server" />

    <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
        <br />
        <br />
       
    <br />
    <asp:Label ID="lblStatus" runat="server"></asp:Label>


    <br />
    <br />
        </asp:Panel>

    <asp:Panel ID="pnlUploadComplete" runat="server">

        <asp:Label ID="lblUploadComplete" runat="server" Text=""></asp:Label>


        <br />
        <br />
        <asp:Label ID="lblErrorList" runat="server"></asp:Label>


    </asp:Panel>
</asp:Content>
