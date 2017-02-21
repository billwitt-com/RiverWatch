<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AWQMSStations.aspx.cs" Inherits="RWInbound2.Applications.AWQMSStations" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

   
 
    <asp:Label ID="Label2" runat="server" CssClass="PageLabel" Text="AWQMS Stations Download"></asp:Label>
    <br />
        <br />

    <asp:Button ID="btnDownload" runat="server" Text=" RUN " CssClass="adminButton" OnClick="btnDownload_Click" />
        <br />    <br />    <br />

     <asp:Label ID="lblDownload" runat="server" Text="Label"></asp:Label>
        <br />

                <asp:Button ID="btnSendFile"  CssClass="adminButton"  Text = "Download" runat="server" OnClick="btnSendFile_Click"  />

</asp:Content>
