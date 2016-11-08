<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Data.aspx.cs" Inherits="RWInbound2.Data.Data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p >
        <span class="PageLabel">Data</span></p>

    <asp:Button ID="btnFieldData" runat="server" Text="Enter Field Data"  CssClass="adminButton" Width="175px" OnClick="btnFieldData_Click" />

    <br /> 
        <asp:Button ID="btnUnknownSample" runat="server"  CssClass="adminButton" Text="Enter an Unknown Sample"  OnClick="btnUnknownSample_Click" Width="175px" />

    <br />

    <asp:Button ID="btnUploadLatchat" runat="server" Text="Upload Latchat" CssClass="adminButton" Width="175px" OnClick="btnUploadLatchat_Click" />

   

</asp:Content>
