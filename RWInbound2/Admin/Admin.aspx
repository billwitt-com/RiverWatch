<%@ Page Title="Admin" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="RWInbound2.Admin.Admin" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Label ID="Label1" CssClass="PageLabel" runat="server" Text="Administration"></asp:Label>  
    <br />
  
        <table style="width: 100%" >
            <tr>
                <td>&nbsp;</td>               
                <td style="width: 262px">
                     <asp:Button ID="btnAddInboundICP" runat="server" Text="Add InboundICP" CssClass="adminButton" Width="176px" OnClick="btnAddInboundICP_Click" />
                </td>
                 <td style="width: 262px">
                    <asp:Button ID="btnControlPermissions" runat="server" Text="Control Permissions" CssClass="adminButton" Width="176px" OnClick="btnControlPermissions_Click" />
                </td>
               <td style="width: 262px">
                    <asp:Button ID="btnExpWater" runat="server" Text="Edit ExpWater" CssClass="adminButton" Width="176px" OnClick="btnExpWaters_Click" />
                </td>                    
                <td>
                    <asp:Button ID="btnEditIncoming" runat="server" Text="Edit Inbound Field Data" CssClass="adminButton" Width="176px" OnClick="btnEditIncoming_Click" />
                </td> 
            </tr>
            <tr>
                <td>&nbsp;</td>               
                <td style="width: 262px">  
                    <asp:Button ID="btnEditMetalBarcode"   CssClass="adminButton" Width="176px" runat="server" Text="Edit Metal Barcodes" OnClick="btnEditMetalBarcode_Click" />                    
                </td>
                 <td style="width: 262px">                   
                     <asp:Button ID="btnEditNutrients" CssClass="adminButton" Width="176px" runat="server" Text="Edit Nutrients" OnClick="btnEditNutrients_Click"  /></td>
               <td style="width: 262px">                    
                    <asp:Button ID="btnEditNutrientBarcodes" CssClass="adminButton" Width="176px" runat="server" Text="Edit Nutrient Barcodes" OnClick="btnEditNutrientBarcodes_Click" />                   
                </td>                    
                <td>    
                     <asp:Button ID="btnNutrientLimits" runat="server" Text="Edit Nutrient Limits" CssClass="adminButton" Width="176px" OnClick="btnNutrientLimits_Click" />
                </td> 
            </tr> 
            <tr>
                <td>&nbsp;</td>               
                <td style="width: 262px">  
                    <asp:Button ID="btnEditWaterCodes" runat="server" Text="Edit Water Codes" CssClass="adminButton" Width="176px" OnClick="btnEditWaterCodes_Click"  />                
                </td>
                 <td style="width: 262px">  
                    <asp:Button ID="btnEditUnknowns" runat="server" CssClass="adminButton"  Text="Edit Unknowns" Width="176px" OnClick="Button7_Click" />                   
                </td>
               <td style="width: 262px">  
                     <asp:Button ID="btnManageOrgs"  CssClass="adminButton" runat="server" Text="Manage Organizations" OnClick="btnManageOrgs_Click" Width="176px"  />                                   
</td>                    
                <td>  
               <asp:Button ID="btnManageOrgStatus"  CssClass="adminButton" runat="server" Text="Manage Org Status"  Width="176px" OnClick="btnManageOrgStatus_Click"  />                                   
                </td> 
            </tr>  
            <tr>
                <td>&nbsp;</td>               
                <td style="width: 262px">      
                    <asp:Button ID="btnManageParticipants" CssClass="adminButton"  runat="server" Text="Manage Participants" Width="176px" OnClick="btnManageParticipants_Click" />                
                </td>
                 <td style="width: 262px">  
                     <asp:Button ID="btnPermissions" CssClass="adminButton"  runat="server" Enabled="False" Text="Manage Permissions"  Width="176px" OnClick="btnPermissions_Click" />                 
                </td>
               <td style="width: 262px">   
                    <asp:Button ID="btnManageStations" runat="server" Text="Manage Stations" CssClass="adminButton" Width="176px" OnClick="btnManageStations_Click" />               
                </td>                    
                <td>     
                     <asp:Button ID="btnUsers" CssClass="adminButton"  runat="server"  Text="Manage Users"  Width="176px" OnClick="btnUsers_Click" />                 
                </td> 
            </tr> 
            <tr>
                <td>&nbsp;</td>               
                <td style="width: 262px">    
                   <asp:Button ID="btnProjectStations" runat="server" Text="Project Stations" CssClass="adminButton" Width="176px" OnClick="btnProjectStations_Click" />                 
                </td>
                 <td style="width: 262px">                   
                    <asp:Button ID="btnManagePublicUsers" runat="server" Text="Public User Access" CssClass="adminButton" Width="176px" OnClick="btnManagePublicUsers_Click" />                  
                </td>
               <td style="width: 262px">                    
                    <asp:Button ID="btnRoles" runat="server" Text="Roles" CssClass="adminButton" Width="176px" OnClick="btnRoles_Click" />               
                </td>                    
               <td style="width: 262px">                    
                    &nbsp;</td>     
            </tr>   

            <tr>
                <td>&nbsp;</td>               
                <td style="width: 262px">    
                                   
                </td>
                 <td style="width: 262px">                   
                
                </td>
               <td style="width: 262px">                    
            
                </td>                    
               <td style="width: 262px">                    
                    &nbsp;</td>     
            </tr>

            <tr>
                <td>&nbsp;</td>               
                <td style="width: 262px">    
                                   
                    &nbsp;</td>
                 <td style="width: 262px">                   
                
                     &nbsp;</td>
               <td style="width: 262px">                    
            
                    <asp:Button ID="btnBulkEditOrgActive" runat="server" Text="OrgActiveBulkEdit" CssClass="adminButton" Width="176px" OnClick="btnBulkEditOrgActive_Click"  />               
            
                </td>                    
               <td style="width: 262px">                    
                    <asp:Button ID="btnResetAllOrgActive" BorderColor="Red" BorderStyle="Solid" BorderWidth="4px" runat="server" Text="Set All Orgs INACTIVE" CssClass="adminButton" Width="183px" ForeColor="Red" OnClick="btnResetAllOrgActive_Click" Height="26px"   />               
                </td>     
            </tr>

            <tr>
                <td>&nbsp;</td>               
                <td style="width: 262px">    
                                   
                    &nbsp;</td>
                 <td style="width: 262px">                   
                
                     &nbsp;</td>
               <td style="width: 262px">                    
            
                    <asp:Button ID="btnBulkEditStnActive" runat="server" Text="StnActiveBulkEdit" CssClass="adminButton" Width="176px" OnClick="btnBulkEditStnActive_Click"   />               
            
                </td>                    
               <td style="width: 262px">                    
                    <asp:Button ID="btnResetAllStnActive" BorderColor="Red" BorderStyle="Solid" BorderWidth="4px" runat="server" Text="Set All Stations INACTIVE" CssClass="adminButton" Width="183px" ForeColor="Red" Height="26px" OnClick="btnResetAllStnActive_Click"   />               
                </td>     
            </tr>
        </table>  
</asp:Content>
