﻿<%@ Page Title="Edit" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="RWInbound2.Edit.Edit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <br />
    <asp:Label ID="Label1" CssClass="PageLabel" runat="server" Text="Edit Lookup Tables"></asp:Label>
    <br />
    <br />
    <table class="button-list-table-editpage" >
        <tr>                     
            <td class="button-list-td">
                <asp:Button ID="btnActivityCategories" runat="server" OnClick="btnActivityCategories_Click" Text="Activity Categories"
                            CssClass="button-list-button" />
            </td>
            <td class="button-list-td">
                <asp:Button ID="btnActivityTypes" runat="server" OnClick="btnActivityTypes_Click" Text="Activity Types" 
                            CssClass="button-list-button"/></td>            
            <td class="button-list-td">
                <asp:Button ID="btnBioResultsTypes" runat="server" OnClick="btnBioResultsTypes_Click" Text="Bio Results Types" 
                            CssClass="button-list-button"/>
            </td>
            <td class="button-list-td">
                <asp:Button ID="btnCommunities" runat="server" OnClick="btnCommunities_Click" Text="Communities" 
                            CssClass="button-list-button"/>
            </td>
            <td class="button-list-td">  
                <asp:Button ID="btnCounties" runat="server" OnClick="btnCounties_Click" Text="Counties" 
                            CssClass="button-list-button"/>
            </td>
        </tr>
        <tr>           
            <td class="button-list-td">
                <asp:Button ID="btnEcoRegions" runat="server" OnClick="btnEcoRegions_Click" Text="Eco Regions" 
                            CssClass="button-list-button"/>
            </td>
           <td class="button-list-td">
                <asp:Button ID="btnEquipment" runat="server" OnClick="btnEquipment_Click" Text="Equipment" 
                            CssClass="button-list-button"/>
            </td>
            <td class="button-list-td">
                <asp:Button ID="btnEquipCategories" runat="server" OnClick="btnEquipCategories_Click" Text="Equipment Categories" 
                            CssClass="button-list-button"/>
            </td>
            <td class="button-list-td">
                <asp:Button ID="btnEquipItem" runat="server" OnClick="btnEquipItems_Click" Text="Equipment Items" 
                            CssClass="button-list-button"/>
            </td>
            <td class="button-list-td"> 
                <asp:Button ID="btnFieldGear" runat="server" OnClick="btnFieldGear_Click" Text="Field Gear" 
                            CssClass="button-list-button"/>
            </td>
        </tr>
        <tr>      
            <td class="button-list-td"> 
                <asp:Button ID="btnFieldProcedure" runat="server" OnClick="btnFieldProcedure_Click" Text="Field Procedures"
                            CssClass="button-list-button"/>
            </td>
            <td class="button-list-td"> 
                <asp:Button ID="btnGear" runat="server" OnClick="btnGear_Click" Text="Gear" 
                            CssClass="button-list-button"/> 
            </td>
            <td class="button-list-td"> 
                <asp:Button ID="btnGrid" runat="server" OnClick="btnGrid_Click" Text="Grids"
                            CssClass="button-list-button"/>
            </td>
            <td class="button-list-td"> 
                <asp:Button ID="btnHydroUnit" runat="server" OnClick="btnHydroUnit_Click" Text="Hydro Units" 
                            CssClass="button-list-button"/>
            </td>
            <td class="button-list-td"> 
                <asp:Button ID="btnItent" runat="server" OnClick="btnItent_Click" Text="Intent" 
                            CssClass="button-list-button"/> 
            </td>
        </tr>
        <tr>
            <td class="button-list-td"> 
                <asp:Button ID="btnMedium" runat="server" OnClick="btnMedium_Click" Text="Medium" 
                            CssClass="button-list-button"/>
            </td>
            <td class="button-list-td"> 
                <asp:Button ID="brnLimits" runat="server" OnClick="brnLimits_Click" Text="Metals Val Limits" 
                            CssClass="button-list-button"/> 
            </td>
            <td class="button-list-td"> 
                <asp:Button ID="btnMetalBarCodeType" runat="server" OnClick="btnMetalBarCodeType_Click" Text="Metal Bar Code Types" 
                            CssClass="button-list-button"/>
            </td>          
            <td class="button-list-td"> 
                <asp:Button ID="btnNutrientBarCodeType" runat="server" OnClick="btnNutrientBarCodeType_Click" Text="Nutrient Barcode Types" 
                            CssClass="button-list-button"/>
            </td>
            <td class="button-list-td"> 
                <asp:Button ID="btnNutrientLimits" runat="server" Text="Nutrient Val Limits" CssClass="button-list-button" OnClick="btnNutrientLimits_Click"/>  
            </td>
        </tr>
        <tr>            
            <td class="button-list-td"> 
                <asp:Button ID="btnOrganizationType" runat="server" OnClick="btnOrganizationType_Click" Text="Organization Types" 
                            CssClass="button-list-button"/>  
            </td>
            <td class="button-list-td">  
                <asp:Button ID="btnPhysHabPara" runat="server" OnClick="btnPhysHabPara_Click" Text="Phys Hab Para" 
                            CssClass="button-list-button"/>    
            </td>
            <td class="button-list-td"> 
                <asp:Button ID="btnEditPhysHab" runat="server" OnClick="btnEditPhysHab_Click" Text="Phys Hab" 
                            CssClass="button-list-button"/>
            </td>
            <td class="button-list-td"> 
                <asp:Button ID="btnProject" runat="server" OnClick="btnProject_Click" Text="Projects" 
                            CssClass="button-list-button"/>
            </td>
            <td class="button-list-td"> 
                <asp:Button ID="btnQUADI" runat="server" OnClick="btnQUADI_Click" Text="QUADI" 
                            CssClass="button-list-button"/>
            </td>
        </tr>
        <tr>
            <td class="button-list-td"> 
                <asp:Button ID="btnQuarterSection" runat="server" OnClick="btnQuarterSection_Click" Text="Quarter Sections" 
                            CssClass="button-list-button"/>
            </td>
            <td class="button-list-td"> 
                <asp:Button ID="btnRange" runat="server" OnClick="btnRange_Click" Text="Ranges" 
                            CssClass="button-list-button"/>
            </td>
            <td class="button-list-td"> 
                <asp:Button ID="btnRegion" runat="server" OnClick="btnRegion_Click" Text="Regions" 
                            CssClass="button-list-button"/>
            </td>
            <td class="button-list-td">  
                <asp:Button ID="btnRiverWatchWaterShed" runat="server" OnClick="btnRiverWatchWaterShed_Click" Text="RW Water Sheds" 
                            CssClass="button-list-button"/>
            </td>
            <td class="button-list-td"> 
                <asp:Button ID="btnSampleType" runat="server" OnClick="btnSampleType_Click" Text="Sample Unknown Types" 
                            CssClass="button-list-button"/>  
            </td>
        </tr>
        <tr>
            <td class="button-list-td">  
                <asp:Button ID="btnSection" runat="server" OnClick="btnSection_Click" Text="Sections" 
                            CssClass="button-list-button"/>
            </td>
            <td class="button-list-td"> 
                <asp:Button ID="btnState" runat="server" OnClick="btnState_Click" Text="States" 
                            CssClass="button-list-button"/> 
            </td>
            <td class="button-list-td"> 
               <asp:Button ID="btnStationQUAD" runat="server" OnClick="btnStationQUAD_Click" Text="Station QUADs" 
                            CssClass="button-list-button"/>
            </td>
            <td class="button-list-td"> 
                <asp:Button ID="btnStationTypes" runat="server" OnClick="btnStationTypes_Click" Text="Station Types" 
                            CssClass="button-list-button"/>
            </td>
            <td class="button-list-td"> 
                <asp:Button ID="btnStationStatus" runat="server" OnClick="btnStationStatus_Click" Text="Station Statuses" 
                            CssClass="button-list-button"/>  
            </td>
        </tr>
        <tr>
            <td class="button-list-td"> 
                <asp:Button ID="btnSubPara" runat="server" OnClick="btnSubPara_Click" Text="Sub Paras" 
                            CssClass="button-list-button"/>
            </td>
            <td class="button-list-td"> 
                <asp:Button ID="btnEditSubSamps" runat="server" OnClick="btnEditSubSamps_Click" Text="Sub Samps" 
                            CssClass="button-list-button"/>
            </td>
            <td class="button-list-td"> 
                <asp:Button ID="btnTownship" runat="server" OnClick="btnTownship_Click" Text="Townships" 
                            CssClass="button-list-button"/>
            </td>
            <td class="button-list-td"> 
                <asp:Button ID="btntblWBKey" runat="server" OnClick="btntblWBKey_Click" Text="Water Body IDs" 
                            CssClass="button-list-button"/>
             </td>
            <td class="button-list-td"> 
                <asp:Button ID="btnWaterCodeDrainage" runat="server" OnClick="btnWaterCodeDrainage_Click" Text="Water Code Drainage" 
                            CssClass="button-list-button"/>
            </td>
        </tr>
        <tr>
            <td class="button-list-td"> 
                <asp:Button ID="btnWQCCWaterShed" runat="server" OnClick="btnWQCCWaterShed_Click" Text="WQCC Water Sheds" 
                            CssClass="button-list-button"/>
            </td>
            <td class="button-list-td"> 
                <asp:Button ID="btnEditWaterCodes" runat="server" Text="Water Codes" CssClass="button-list-button" OnClick="btnEditWaterCodes_Click" />                
            </td>
            <td class="button-list-td">                
                <asp:Button ID="btnWSG" runat="server" OnClick="btnWSG_Click" Text="WSGs" 
                            CssClass="button-list-button"/>               
            </td>
            <td class="button-list-td">                
                <asp:Button ID="btnWSR" runat="server" OnClick="btnWSR_Click" Text="WSRs" 
                            CssClass="button-list-button"/>
            </td>  
            <td class="button-list-td">&nbsp;</td>          
        </tr>
        <tr>
            <td class="button-list-td">&nbsp;</td>
            <td class="button-list-td">&nbsp;</td>
            <td class="button-list-td">&nbsp;</td>
            <td class="button-list-td">&nbsp;</td>
            <td class="button-list-td">&nbsp;</td>
        </tr>            
    </table>  
</asp:Content>
