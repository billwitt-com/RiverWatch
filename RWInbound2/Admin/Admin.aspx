<%@ Page Title="Admin" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="RWInbound2.Admin.Admin" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label1" CssClass="PageLabel" runat="server" Text="Administration"></asp:Label>  
    <br />  
    <br />
    <table class="button-list-table-adminpage" >
        <tr>                      
            <td class="button-list-td">
                <asp:Button ID="btnAddInboundICP" runat="server" Text="Add InboundICP" CssClass="button-list-button" OnClick="btnAddInboundICP_Click" />
            </td>
            <td class="button-list-td">
                <asp:Button ID="btnControlPermissions" runat="server" Text="Control Permissions" CssClass="button-list-button" OnClick="btnControlPermissions_Click" />
            </td>
            <td class="button-list-td">
                <asp:Button ID="btnExpWater" runat="server" Text="Edit ExpWater" CssClass="button-list-button" OnClick="btnExpWaters_Click" />
            </td>                    
            <td>
                <asp:Button ID="btnEditIncoming" runat="server" Text="Edit Inbound Field Data" CssClass="button-list-button" OnClick="btnEditIncoming_Click" />
            </td> 
        </tr>
        <tr>                        
            <td class="button-list-td">  
                <asp:Button ID="btnEditMetalBarcode" CssClass="button-list-button" runat="server" Text="Edit Metal Barcodes" OnClick="btnEditMetalBarcode_Click" />                    
            </td>
            <td class="button-list-td">                   
                <asp:Button ID="btnEditNutrients" CssClass="button-list-button" runat="server" Text="Edit Nutrients" OnClick="btnEditNutrients_Click"  />
            </td>
            <td class="button-list-td">                    
                <asp:Button ID="btnEditNutrientBarcodes" CssClass="button-list-button" runat="server" Text="Edit Nutrient Barcodes" OnClick="btnEditNutrientBarcodes_Click" />                   
            </td>                    
            <td>    
                <asp:Button ID="btnEditUnknowns" runat="server" CssClass="adminButton"  Text="Edit Unknowns" Width="176px" OnClick="Button7_Click" />                   
            </td> 
        </tr> 
        <tr>                      
            <td class="button-list-td">  
                <asp:Button ID="btnManageOrgs"  CssClass="adminButton" runat="server" Text="Manage Organizations" OnClick="btnManageOrgs_Click" Width="176px"  />                                   
            </td>
            <td class="button-list-td">  
                <asp:Button ID="btnManageOrgStatus"  CssClass="adminButton" runat="server" Text="Manage Org Status"  Width="176px" OnClick="btnManageOrgStatus_Click"  />                                   
            </td>
            <td class="button-list-td">  
                <asp:Button ID="btnManageParticipants" CssClass="adminButton"  runat="server" Text="Manage Participants" Width="176px" OnClick="btnManageParticipants_Click" />                
            </td>                    
            <td>  
                <asp:Button ID="btnPermissions" CssClass="adminButton"  runat="server" Enabled="False" Text="Manage Permissions"  Width="176px" OnClick="btnPermissions_Click" />                 
            </td> 
        </tr>  
        <tr>                    
            <td class="button-list-td">      
                <asp:Button ID="btnManageStations" runat="server" Text="Manage Stations" CssClass="button-list-button" OnClick="btnManageStations_Click" />               
            </td>
                <td class="button-list-td">  
                    <asp:Button ID="btnUsers" CssClass="adminButton"  runat="server"  Text="Manage Users"  Width="176px" OnClick="btnUsers_Click" />                 
            </td>
            <td class="button-list-td">   
                <asp:Button ID="btnProjectStations" runat="server" Text="Project Stations" CssClass="button-list-button" OnClick="btnProjectStations_Click" />                 
            </td>                    
            <td>     
                <asp:Button ID="btnManagePublicUsers" runat="server" Text="Public User Access" CssClass="button-list-button" OnClick="btnManagePublicUsers_Click" />                  
            </td> 
        </tr> 
        <tr>                       
            <td class="button-list-td">    
                <asp:Button ID="btnRoles" runat="server" Text="Roles" CssClass="button-list-button" OnClick="btnRoles_Click" />               
            </td>
            <td class="button-list-td">                   
                <asp:Button ID="btnStationImages" runat="server" Text="Station Images" CssClass="button-list-button" OnClick="btnStationImages_Click" />     
            </td>
           <td class="button-list-td">&nbsp;</td>                
           <td class="button-list-td">&nbsp;</td> 
        </tr>  
        <tr>
            <td>&nbsp;</td>               
            <td class="button-list-td"></td>    
            <td class="button-list-td"></td>    
            <td class="button-list-td"></td>    
            <td class="button-list-td">&nbsp;</td>    
        </tr>
        <tr>                   
            <td class="button-list-td">&nbsp;</td>    
            <td class="button-list-td">&nbsp;</td>    
            <td class="button-list-td"> 
                <asp:Button ID="btnBulkEditOrgActive" runat="server" Text="OrgActiveBulkEdit" CssClass="button-list-button" OnClick="btnBulkEditOrgActive_Click"  />   
            </td>                    
            <td class="button-list-td">                    
                <asp:Button ID="btnResetAllOrgActive" BorderColor="Red" BorderStyle="Solid" BorderWidth="4px" runat="server" Text="Set All Orgs INACTIVE" CssClass="adminButton" Width="183px" ForeColor="Red" OnClick="btnResetAllOrgActive_Click" Height="26px"   />               
            </td>     
        </tr>
        <tr>         
            <td class="button-list-td">&nbsp;</td>        
            <td class="button-list-td">&nbsp;</td>    
            <td class="button-list-td"> 
                <asp:Button ID="btnBulkEditStnActive" runat="server" Text="StnActiveBulkEdit" CssClass="button-list-button" OnClick="btnBulkEditStnActive_Click"   />               
            </td>                    
            <td class="button-list-td">                    
                <asp:Button ID="btnResetAllStnActive" BorderColor="Red" BorderStyle="Solid" BorderWidth="4px" runat="server" Text="Set All Stations INACTIVE" CssClass="adminButton" Width="183px" ForeColor="Red" Height="26px" OnClick="btnResetAllStnActive_Click"   />               
            </td>     
        </tr>
    </table>  
</asp:Content>
