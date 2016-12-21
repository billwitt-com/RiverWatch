<%@ Page Title="Reports" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="RWInbound2.Reports.Reports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="site-title">
        <asp:Label ID="Label1" runat="server" CssClass =" PageLabel" Text="  Admin Reports"></asp:Label>
        <br />
        <br />
        <table style="width: 100%">
            <tr>
                <td class="rowId-charid-dsorder-textbox" style="width: 15px">&nbsp;</td>
                <td>                <asp:Button ID="btnErrorLogReport" runat="server" Text="Error Log" OnClick="btnErrorLogReport_Click"
                            CssClass="adminButton" /></td>
                <td style="width: 408px">                <asp:Button ID="btnLachatNoBC" runat="server" Text="Lachat BC not Entered" OnClick="btnLachatNoBC_Click"
                            CssClass="adminButton" /></td>
                <td>                <asp:Button ID="btnMailingList" runat="server" Text="Mailing List" OnClick="btnMailingList_Click" 
                            CssClass="adminButton"/>   </td>
                <td>                <asp:Button ID="btnMetalBarCodes" runat="server" Text="Metal Bar Codes" OnClick="btnMetalBarCodes_Click" 
                            CssClass="adminButton"/></td>
            </tr>
            <tr>
                <td class="rowId-charid-dsorder-textbox" style="width: 15px">&nbsp;</td>
                <td>                <asp:Button ID="btnParticipants" runat="server" Text="Participants" OnClick="btnParticipants_Click" 
                            CssClass="adminButton"/></td>
                <td style="width: 408px">                <asp:Button ID="btnParticipantsPrimaryContacts" runat="server" Text="Participants - Primary Contacts" OnClick="btnParticipantsPrimaryContacts_Click"
                            CssClass="adminButton"/></td>
                <td>                <asp:Button ID="btnPublicUsers" runat="server" Text="Public Users" OnClick="btnPublicUsers_Click" 
                            CssClass="adminButton"/></td>
                <td>                <asp:Button ID="btnSamples" runat="server" Text="Samples" OnClick="btnSamples_Click" 
                            CssClass="adminButton"/></td>
            </tr>
            <tr>
                <td class="rowId-charid-dsorder-textbox" style="width: 15px">&nbsp;</td>
                <td>&nbsp;</td>
                <td style="width: 408px">&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            
        </table>
    </div>



 
</asp:Content>
