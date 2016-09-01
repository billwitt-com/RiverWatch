<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Data.aspx.cs" Inherits="RWInbound2.Data.Data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p class="site-title">
        Data</p>

    <asp:Button ID="btnFieldData" runat="server" Text="Enter Field Data" OnClick="btnFieldData_Click" />

    <br />
    <br />

    <asp:Button ID="btnUploadLatchat" runat="server" Text="Upload Latchat" OnClick="btnUploadLatchat_Click" />
</asp:Content>
