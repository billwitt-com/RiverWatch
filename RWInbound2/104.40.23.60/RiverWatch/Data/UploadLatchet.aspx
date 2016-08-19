<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UploadLatchet.aspx.cs" Inherits="RWInbound2.Data.UploadLatchet" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    Upload Latchet Data from CSV file

    <br />
    Please place the file to be processed in your <a href="file:///c:/temp">c:\temp</a> directory
    <br />
    <br />
    <asp:Label ID="Label1" runat="server" Text="Choose File"></asp:Label>

    <asp:FileUpload ID="FileUpload1" runat="server" />

    <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
    <ajaxToolkit:AjaxFileUpload ID="AjaxFileUpload1" runat="server" />
    <br />
    <br />
</asp:Content>
