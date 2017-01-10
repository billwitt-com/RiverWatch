<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ResetOrgActive.aspx.cs" Inherits="RWInbound2.Admin.ResetOrgActive" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    
        <br />

        <asp:Label ID="Label1" CssClass="PageLabel" ForeColor="Red" Font-Size="X-Large" runat="server" Text="RESET ALL ORGS TO INACTIVE Bulk Update"></asp:Label>
            <br />
                <br />
    <asp:Label ID="Label2" ForeColor="Red" Font-Size="X-Large" runat="server" Text="Danger this will reset ALL Orgs to INACTIVE"></asp:Label>

                <br />
        <br />

                <br />
                <br />

    <asp:Button ID="Button1" runat="server" CssClass="adminButton" Text="CAUTION: RESET ALL ORGS TO INACTIVE !!!!" OnClick="Button1_Click" />



        <ajaxToolkit:ConfirmButtonExtender ID="Button1_ConfirmButtonExtender"   ConfirmOnFormSubmit="true" ConfirmText="ARE YOU SURE YOU WANT TO SET ALL ORGS TO NOT ACTIVE?" runat="server"  TargetControlID="Button1" />





        <br />
        <br />
        <asp:Label ID="LblResults" runat="server" ></asp:Label>





</asp:Content>
