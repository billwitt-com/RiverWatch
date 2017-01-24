<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Data.aspx.cs" Inherits="RWInbound2.Data.Data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

        <br /> 
    &nbsp; 
    <asp:Label ID="Label1"  CssClass = "PageLabel" runat="server" Text=" River Watch Data"></asp:Label>
            <br />
            <br /> 
    <asp:Button ID="btnFieldData" runat="server" Text="Enter Field Data"  CssClass="adminButton" Width="218px" OnClick="btnFieldData_Click" />

    <br /> 
        <asp:Button ID="btnUnknownSample" runat="server"  CssClass="adminButton" Text="Enter an Unknown Sample"  OnClick="btnUnknownSample_Click" Width="218px" Height="23px" />

    <br />

    <asp:Button ID="btnUploadLatchat" runat="server" Text="Upload Latchat" CssClass="adminButton" Width="218px" OnClick="btnUploadLatchat_Click" />

   

</asp:Content>
