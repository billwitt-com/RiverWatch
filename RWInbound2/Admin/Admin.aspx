<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="RWInbound2.Admin.Admin" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Label ID="Label1" CssClass="PageLabel" runat="server" Text="Administration"></asp:Label>  
  <%--  
    <asp:Panel ID="pnlQuickview"   runat="server" BackColor="#FFFFCC"  CssClass="panelgrouping"
          Width="468px" ForeColor="Black"    >
       <div  style="text-align:center">
       
        <asp:Label ID="Label3" runat="server" Font-Bold="True" Text="Quick View"></asp:Label>
             <br />
             </div>--%>
       <%-- <table style="width: 90%">
            <tr>
                <td style="width: 145px; height: 29px;">
                    <asp:Label ID="Label1" runat="server" Text="Kit Number :"></asp:Label>
                </td>
                <td style="height: 28px; width: 185px">
                    <asp:TextBox ID="TextBox1" runat="server" Width="95px"></asp:TextBox>
                </td>
                <td style="height: 28px">
                    <asp:Button ID="btnSearchKitNumber" runat="server" CssClass="smallButton" Height="23px"  Text="Search" />
                </td>
            </tr>
            <tr>
                <td style="width: 145px; height: 29px;">
                    <asp:Label ID="Label4" runat="server" Text="Station Name :"></asp:Label>
                </td>
                <td style="width: 185px; height: 29px;">
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                </td>
                <td style="height: 29px">
                    <asp:Button ID="btnSearchStationName" runat="server" CssClass="smallButton"  Text="Search" Height="23px" OnClick="btnSearchStationName_Click" />
                </td>
            </tr>
            <tr>
                <td style="width: 145px">
                    <asp:Label ID="Label2" runat="server" Text="Org Name :"></asp:Label>
                </td>
                <td style="width: 185px">
                    <asp:TextBox ID="tbOrgName" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnSearchOrgName" runat="server" CssClass="smallButton" Text="Search" Height="23px" OnClick="btnSearchOrgName_Click" />
                </td>
            </tr>
        </table>

    </asp:Panel>
    <ajaxToolkit:DropShadowExtender ID="pnlQuickview_DropShadowExtender" runat="server" 
        BehaviorID="pnlQuickview_DropShadowExtender"  TargetControlID="pnlQuickview">
    </ajaxToolkit:DropShadowExtender>
--%>

        <table style="width: 100%" >
               <tr>
                <td style="height: 13px"></td>
                   </tr>
            <tr>
                <td style="height: 18px"></td>
                <td style="width: 262px; height: 18px;">        
                    <asp:Button ID="btnManageOrgs"  CssClass="adminButton" runat="server" Text="Manage Organizations" OnClick="btnManageOrgs_Click" Width="176px"  /></td>
                <td style="height: 18px">
                <asp:Button ID="btnManageStations" runat="server" Text="Manage Stations" CssClass="adminButton" Width="176px" OnClick="btnManageStations_Click" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td style="width: 262px">
        <asp:Button ID="btnEditUnknowns" runat="server" CssClass="adminButton"  Text="Edit Unknowns" Width="176px" OnClick="Button7_Click" />
                </td>
                <td>
                     <asp:Button ID="btnNutrientLimits" runat="server" Text="Nutrient Limits" CssClass="adminButton" Width="176px" OnClick="btnNutrientLimits_Click" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td style="width: 262px">
        <asp:Button ID="Button4" CssClass="adminButton"  runat="server" Text="Manage Participants" Width="176px" />
                </td>
                <td>
                    <asp:Button ID="btnEditMetalBarcode"   CssClass="adminButton" Width="176px" runat="server" Text="Edit Metal Barcodes" OnClick="btnEditMetalBarcode_Click" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td style="width: 262px">
                    <asp:Button ID="btnUsers" CssClass="adminButton"  runat="server"  Text="Manage Users"  Width="176px" OnClick="btnUsers_Click" />
                </td>
                <td>
                    <asp:Button ID="Button2" CssClass="adminButton"  runat="server" Enabled="False" Text="Manage Permissions"  Width="176px" />
                </td>
            </tr>
             <tr>
                <td>&nbsp;</td>               
                <td style="width: 262px">
                    <asp:Button ID="btnProjectStations" runat="server" Text="Project Stations" CssClass="adminButton" Width="176px" OnClick="btnProjectStations_Click" />
                </td>
                 <td>
                    <asp:Button ID="btnControlPermissions" runat="server" Text="Control Permissions" CssClass="adminButton" Width="176px" OnClick="btnControlPermissions_Click" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>               
                <td style="width: 262px">
                    <asp:Button ID="btnRoles" runat="server" Text="Roles" CssClass="adminButton" Width="176px" OnClick="btnRoles_Click" />
                </td>
                 <td>
                    <asp:Button ID="btnAddInboundICP" runat="server" Text="Add InboundICP" CssClass="adminButton" Width="176px" OnClick="btnAddInboundICP_Click" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td style="width: 262px">
                    <asp:Button ID="btnExpWater" runat="server" Text="Edit ExpWater" CssClass="adminButton" Width="176px" OnClick="btnExpWaters_Click" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td style="width: 262px">
                    <asp:Button ID="btnEditIncoming" runat="server" Text="Edit Field Data" CssClass="adminButton" Width="176px" OnClick="btnEditIncoming_Click" />
                </td>
                <td>
                    <asp:Button ID="btnManagePublicUsers" runat="server" Text="Manage Public User Access" CssClass="adminButton" Width="176px" OnClick="btnManagePublicUsers_Click" />
                </td>
            </tr>
        </table>
  
</asp:Content>
