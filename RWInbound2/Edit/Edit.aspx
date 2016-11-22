<%@ Page Title="Edit" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="RWInbound2.Edit.Edit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p class="site-title">
        Edit Data Tables:
    </p>
    <asp:Table ID="Table1" 
                runat="server" 
                CssClass="edit-table" >
        <asp:TableRow CssClass="edit-table-row">
            <asp:TableCell CssClass="edit-table-cell">
                <asp:Button ID="btnActivityCategories" runat="server" OnClick="btnActivityCategories_Click" Text="Activity Categories"
                            CssClass="adminButton" />
            </asp:TableCell>  
            <asp:TableCell CssClass="edit-table-cell">
                <asp:Button ID="btnActivityTypes" runat="server" OnClick="btnActivityTypes_Click" Text="Activity Types" 
                            CssClass="adminButton"/>
            </asp:TableCell> 
            <asp:TableCell CssClass="edit-table-cell">
                <asp:Button ID="btnBenthics" runat="server" OnClick="btnBenthics_Click" Text="Benthics" 
                            CssClass="adminButton"/>
            </asp:TableCell>           
             <asp:TableCell CssClass="edit-table-cell">
                <asp:Button ID="btnBioResultsTypes" runat="server" OnClick="btnBioResultsTypes_Click" Text="Bio Results Types" 
                            CssClass="adminButton"/>
            </asp:TableCell>
            <asp:TableCell CssClass="edit-table-cell">
                <asp:Button ID="btnCommunities" runat="server" OnClick="btnCommunities_Click" Text="Communities" 
                            CssClass="adminButton"/>
            </asp:TableCell>  
        </asp:TableRow>

        <asp:TableRow CssClass="edit-table-row">            
            <asp:TableCell CssClass="edit-table-cell">
                <asp:Button ID="btnCounties" runat="server" OnClick="btnCounties_Click" Text="Counties" 
                            CssClass="adminButton"/>
            </asp:TableCell>     
            <asp:TableCell CssClass="edit-table-cell">
                <asp:Button ID="btnEcoRegions" runat="server" OnClick="btnEcoRegions_Click" Text="Eco Regions" 
                            CssClass="adminButton"/>
            </asp:TableCell> 
            <asp:TableCell CssClass="edit-table-cell">
                <asp:Button ID="btnEquipCategories" runat="server" OnClick="btnEquipCategories_Click" Text="Equipment Categories" 
                            CssClass="adminButton"/>
            </asp:TableCell> 
            <asp:TableCell CssClass="edit-table-cell">
                <asp:Button ID="btnEquipItem" runat="server" OnClick="btnEquipItems_Click" Text="Equipment Items" 
                            CssClass="adminButton"/>
            </asp:TableCell>           
            <asp:TableCell CssClass="edit-table-cell">
                <asp:Button ID="btnFieldGear" runat="server" OnClick="btnFieldGear_Click" Text="Field Gear" 
                            CssClass="adminButton"/>
            </asp:TableCell>   
        </asp:TableRow>  
        
        <asp:TableRow CssClass="edit-table-row">
            <asp:TableCell CssClass="edit-table-cell">
                <asp:Button ID="btnFieldProcedure" runat="server" OnClick="btnFieldProcedure_Click" Text="Field Procedures"
                            CssClass="adminButton"/>
            </asp:TableCell> 
            <asp:TableCell CssClass="edit-table-cell">
                <asp:Button ID="btnGear" runat="server" OnClick="btnGear_Click" Text="Gears" 
                            CssClass="adminButton"/> 
            </asp:TableCell>
            <asp:TableCell CssClass="edit-table-cell">
                <asp:Button ID="btnGrid" runat="server" OnClick="btnGrid_Click" Text="Grids"
                            CssClass="adminButton"/>
            </asp:TableCell>                            
            <asp:TableCell CssClass="edit-table-cell">
                <asp:Button ID="btnHydroUnit" runat="server" OnClick="btnHydroUnit_Click" Text="Hydro Units" 
                            CssClass="adminButton"/>
            </asp:TableCell> 
            <asp:TableCell CssClass="edit-table-cell">
                <asp:Button ID="brnLimits" runat="server" OnClick="brnLimits_Click" Text="Measurement Limits" 
                            CssClass="adminButton"/> 
            </asp:TableCell> 
        </asp:TableRow>

        <asp:TableRow CssClass="edit-table-row">
            <asp:TableCell CssClass="edit-table-cell">
                <asp:Button ID="btnMetalBarCodeType" runat="server" OnClick="btnMetalBarCodeType_Click" Text="Metal Bar Code Types" 
                            CssClass="adminButton"/>
            </asp:TableCell>   
            <asp:TableCell CssClass="edit-table-cell">
                <asp:Button ID="btnNutrientBarCodeType" runat="server" OnClick="btnNutrientBarCodeType_Click" Text="Nutrient Bar Code Types" 
                            CssClass="adminButton"/>
            </asp:TableCell>                                   
            <asp:TableCell CssClass="edit-table-cell">
                <asp:Button ID="btnOrganizations" runat="server" OnClick="btnOrganizations_Click" Text="Organizations" 
                            CssClass="adminButton"/>
            </asp:TableCell> 
            <asp:TableCell CssClass="edit-table-cell">
                <asp:Button ID="btnOrganizationType" runat="server" OnClick="btnOrganizationType_Click" Text="Organization Types" 
                            CssClass="adminButton"/>
            </asp:TableCell> 
            <asp:TableCell CssClass="edit-table-cell">  
                <asp:Button ID="btnPhysHabPara" runat="server" OnClick="btnPhysHabPara_Click" Text="Phys Hab Para" 
                            CssClass="adminButton"/>              
            </asp:TableCell>                                         
        </asp:TableRow>

        <asp:TableRow CssClass="edit-table-row">
            <asp:TableCell CssClass="edit-table-cell">
                <asp:Button ID="btnEditPhysHab" runat="server" OnClick="btnEditPhysHab_Click" Text="Phys Hab" 
                            CssClass="adminButton"/>
            </asp:TableCell>  
            <asp:TableCell CssClass="edit-table-cell">
                <asp:Button ID="btnProject" runat="server" OnClick="btnProject_Click" Text="Projects" 
                            CssClass="adminButton"/>
            </asp:TableCell> 
            <asp:TableCell CssClass="edit-table-cell">
                <asp:Button ID="btnQUADI" runat="server" OnClick="btnQUADI_Click" Text="QUADI" 
                            CssClass="adminButton"/>
            </asp:TableCell>                                                    
            <asp:TableCell CssClass="edit-table-cell">
              <asp:Button ID="btnQuarterSection" runat="server" OnClick="btnQuarterSection_Click" Text="Quarter Sections" 
                            CssClass="adminButton"/>
            </asp:TableCell> 
            <asp:TableCell CssClass="edit-table-cell">
                <asp:Button ID="btnRange" runat="server" OnClick="btnRange_Click" Text="Ranges" 
                            CssClass="adminButton"/>
            </asp:TableCell> 
        </asp:TableRow>

        <asp:TableRow CssClass="edit-table-row">
            <asp:TableCell CssClass="edit-table-cell">
              <asp:Button ID="btnRegion" runat="server" OnClick="btnRegion_Click" Text="Regions" 
                            CssClass="adminButton"/>
            </asp:TableCell>
            <asp:TableCell CssClass="edit-table-cell">
                <asp:Button ID="btnRiverWatchWaterShed" runat="server" OnClick="btnRiverWatchWaterShed_Click" Text="River Watch Water Sheds" 
                            CssClass="adminButton"/>
            </asp:TableCell>                                                     
            <asp:TableCell CssClass="edit-table-cell">
                <asp:Button ID="btnSampleType" runat="server" OnClick="btnSampleType_Click" Text="Sample Types" 
                            CssClass="adminButton"/>    
            </asp:TableCell> 
            <asp:TableCell CssClass="edit-table-cell">
                <asp:Button ID="btnSection" runat="server" OnClick="btnSection_Click" Text="Sections" 
                            CssClass="adminButton"/>
            </asp:TableCell> 
            <asp:TableCell CssClass="edit-table-cell">
              <asp:Button ID="btnState" runat="server" OnClick="btnState_Click" Text="States" 
                            CssClass="adminButton"/>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow CssClass="edit-table-row">
            <asp:TableCell CssClass="edit-table-cell">
               <asp:Button ID="btnStationQUAD" runat="server" OnClick="btnStationQUAD_Click" Text="Station QUADs" 
                            CssClass="adminButton"/>
            </asp:TableCell>                                                     
            <asp:TableCell CssClass="edit-table-cell">
                <asp:Button ID="btnStationTypes" runat="server" OnClick="btnStationTypes_Click" Text="Station Types" 
                            CssClass="adminButton"/>
            </asp:TableCell>
            <asp:TableCell CssClass="edit-table-cell">
                <asp:Button ID="btnStationStatus" runat="server" OnClick="btnStationStatus_Click" Text="Station Statuses" 
                            CssClass="adminButton"/>  
            </asp:TableCell> 
            <asp:TableCell CssClass="edit-table-cell">
                <asp:Button ID="btnSubPara" runat="server" OnClick="btnSubPara_Click" Text="Sub Paras" 
                            CssClass="adminButton"/>
            </asp:TableCell> 
            <asp:TableCell CssClass="edit-table-cell">
                <asp:Button ID="btnEditSubSamps" runat="server" OnClick="btnEditSubSamps_Click" Text="Sub Samps" 
                            CssClass="adminButton"/>
            </asp:TableCell> 
        </asp:TableRow>

        <asp:TableRow CssClass="edit-table-row">
            <asp:TableCell CssClass="edit-table-cell">
                <asp:Button ID="btnTownship" runat="server" OnClick="btnTownship_Click" Text="Townships" 
                            CssClass="adminButton"/>
            </asp:TableCell> 
            <asp:TableCell CssClass="edit-table-cell">
                <asp:Button ID="btntblWBKey" runat="server" OnClick="btntblWBKey_Click" Text="Water Body IDs" 
                            CssClass="adminButton"/>
            </asp:TableCell>
            <asp:TableCell CssClass="edit-table-cell">
                <asp:Button ID="btnWQCCWaterShed" runat="server" OnClick="btnWQCCWaterShed_Click" Text="WQCC Water Sheds" 
                            CssClass="adminButton"/>
            </asp:TableCell>
            <asp:TableCell CssClass="edit-table-cell">
                <asp:Button ID="btnWSG" runat="server" OnClick="btnWSG_Click" Text="WSGs" 
                            CssClass="adminButton"/>
            </asp:TableCell>                                      
            <asp:TableCell CssClass="edit-table-cell">
                <asp:Button ID="btnWSR" runat="server" OnClick="btnWSR_Click" Text="WSRs" 
                            CssClass="adminButton"/>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
