<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Data.aspx.cs" Inherits="RWInbound2.Data.Data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p >
        <span class="PageLabel">Data</span></p>

    <asp:Button ID="btnFieldData" runat="server" Text="Enter Field Data" OnClick="btnFieldData_Click" />

    <br />
    <br />

    <asp:Button ID="btnUploadLatchat" runat="server" Text="Upload Latchat" OnClick="btnUploadLatchat_Click" />

    <asp:Panel ID="pnlDownloadData" runat="server">
        <br />
        <asp:Label ID="Label1"  CssClass="PageLabel" runat="server" Text="Download Data"></asp:Label>
        <br />
        <table style="width: 100%">
            <tr>
                <td>
                    <asp:Button ID="btnOrganization" runat="server" Text="Organizations" OnClick="btnOrganization_Click" />
                </td>
                <td>
                   <asp:Button ID="btnInboundICP" runat="server" Text="InboundICP" />
                </td>
                <td>
                    <asp:Button ID="Button7" runat="server" Text="Button" />
                </td>
            </tr>
            <tr>
                <td>
                     <asp:Button ID="btnParticipants" runat="server" Text="Participants" />
                </td>
                <td>
                    <asp:Button ID="Button5" runat="server" Text="Button" />
                </td>
                <td>
                    <asp:Button ID="Button6" runat="server" Text="Button" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnStation" runat="server" Text="Stations" OnClick="btnStation_Click" />
                </td>
                <td>
                    <asp:Button ID="Button8" runat="server" Text="Button" />
                </td>
                <td>
                    <asp:Button ID="Button9" runat="server" Text="Button" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnSamples" runat="server" Text="Samples" />
                </td>
                <td>
                    <asp:Button ID="Button3" runat="server" Text="Button" />
                </td>
                <td>
                    <asp:Button ID="Button4" runat="server" Text="Button" />
                </td>
            </tr>
           <tr>
                <td>
                    <asp:Button ID="btnUnknownSample" runat="server" Text="UnknownSamples" />
                </td>
                <td>
                    <asp:Button ID="Button11" runat="server" Text="Button" />
                </td>
                <td>
                    <asp:Button ID="Button12" runat="server" Text="Button" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="Button13" runat="server" Text="Button" />
                </td>
                <td>
                    <asp:Button ID="Button14" runat="server" Text="Button" />
                </td>
                <td>
                    <asp:Button ID="Button15" runat="server" Text="Button" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </asp:Panel>



</asp:Content>
