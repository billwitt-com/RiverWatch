<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Validation.aspx.cs" Inherits="RWInbound2.Validation.Validation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <p class="pageHeader">Incoming Validation </p>
    <div >
    <table  style="width: 100%">
        <%--<tr>
            <td style="width: 7px">&nbsp;</td>
            <td style="width: 201px">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>--%>
        <tr>
            <td >&nbsp;</td>
            <td style="width: 209px" >
    <asp:Button ID="btnICPBlanks"   runat="server" OnClick="btnICPBlanks_Click" Text="Step #1 ICP Blanks" Width="190px" Font-Bold="True" Font-Names="Calibri" Font-Size="16px"  />
            </td>
            <td>
                <asp:Label ID="lblICPBlanks" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td >&nbsp;</td>
            <td style="width: 209px" >
        <asp:Button ID="btnICPDups"  runat="server" Text="Step #2 ICP Duplicates" OnClick="btnICPDups_Click"  Width="190px" Font-Bold="True" Font-Names="Calibri" Font-Size="16px"  />
            </td>
            <td>
                <asp:Label ID="lblICPDups" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 27px">&nbsp;</td>
            <td style="width: 209px">
        <asp:Button ID="btnICPSamples"   runat="server" Text="Step #3 ICP Normals" OnClick="btnICPSamples_Click"  Width="190px" Font-Bold="True" Font-Names="Calibri" Font-Size="16px"   />
            </td>
            <td>
                <asp:Label ID="lblICPSamples" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 27px">&nbsp;</td>
            <td style="width: 209px">
                <asp:Button ID="btnLachet" runat="server"  Text="Step #1 Lachat Normals"  Width="190px" Font-Bold="True" Font-Names="Calibri" Font-Size="16px" OnClick="btnLachet_Click"   />
            </td>
            <td>
                <asp:Label ID="lblLachet" runat="server"></asp:Label>
            </td>
        </tr>
         <tr>
            <td style="width: 27px">&nbsp;</td>
            <td style="width: 209px">
                <asp:Button ID="btnLachatDups" runat="server"  Text="Step #2 Lachat Dups"  Width="190px" Font-Bold="True" Font-Names="Calibri" Font-Size="16px" OnClick="btnLachatDups_Click"   />
            </td>
            <td>
                <asp:Label ID="lblLachatDups" runat="server"></asp:Label>
            </td>
        </tr>

         <tr>
            <td style="width: 27px">&nbsp;</td>
            <td style="width: 209px">
               
            </td>
            <td>
                <asp:Label ID="lblLachatMessage" runat="server"></asp:Label>
            </td>
        </tr>

         <tr>
            <td style="width: 27px">&nbsp;</td>
            <td style="width: 209px">
                <asp:Button ID="btnLachatHangingDups" runat="server"  Text="Step #3 Lachat Solo Dups"  Width="190px" Font-Bold="True" Font-Names="Calibri" Font-Size="16px" OnClick="btnLachatDups_Click"   />
            </td>
            <td>
                <asp:Label ID="lblSoloLachatDups" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 27px">&nbsp;</td>
            <td style="width: 209px">
    <asp:Button ID="btnField" runat="server"   Text="Field"  Width="190px" Font-Bold="True" Font-Names="Calibri" Font-Size="16px" OnClick="btnField_Click"  />
            </td>
            <td>
                <asp:Label ID="lblFieldSamples" runat="server" ></asp:Label>
           </td>
        </tr>

        <tr>
            <td style="width: 27px">&nbsp;</td>
            <td style="width: 209px">
    
            </td>
            <td>
                 <asp:Label ID="lblFieldNotRecorded" runat="server" ></asp:Label> 
           </td>
        </tr>

        <tr>
            <td style="width: 27px">&nbsp;</td>
            <td style="width: 209px">
    <asp:Button ID="btnUnknown" runat="server"   Text="Unknowns"  Width="190px" Font-Bold="True" Font-Names="Calibri" Font-Size="16px" OnClick="btnUnknown_Click"  />
            </td>
            <td>
                <asp:Label ID="lblUnknowns" runat="server" Text="There are XX unknown samples"></asp:Label>         </td>
        </tr>

            
    </table>

        </div>

    <p>
        &nbsp;</p>
<p>
    &nbsp;</p>


</asp:Content>
