<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="RWInbound2.index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
            
        <table style="width: 100%">
            <tr>
                <td style="width: 754px">
                    <p class="site-title"> Welcome to River Watch Internal Web Site </p>
       
                    </td>
                <td>&nbsp;</td>
                <td>
                    <asp:Button ID="btnLogin" runat="server" Text="Log In" OnClick="btnLogin_Click" /></td>
            </tr>
        </table>
   
    <p>
        <asp:Label ID="lblWelcome" runat="server" Text="Welcome Default"></asp:Label>
        </p>
    <p>
        Links to associated sites?</p>
    <p>
        Perhaps some news here?&nbsp;</p>
    <p>
        Some pictures?&nbsp;</p>
    <p>
        Some quotes?</p>
</asp:Content>
