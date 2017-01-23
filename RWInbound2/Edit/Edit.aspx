<%@ Page Title="Edit" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="RWInbound2.Edit.Edit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <br />
    <asp:Label ID="Label1" CssClass="PageLabel" runat="server" Text="Edit Data Tables:"></asp:Label>
    <br />
    <table class="EditTabTable" >
        <tr >
            <td>
                <asp:Button ID="btnActivityCategories" runat="server" OnClick="btnActivityCategories_Click" Text="Activity Categories"
                            CssClass="adminButton" />

            </td>
            <td>                <asp:Button ID="btnActivityTypes" runat="server" OnClick="btnActivityTypes_Click" Text="Activity Types" 
                            CssClass="adminButton"/></td>
            <td>



                <asp:Button ID="btnBenthics" runat="server" OnClick="btnBenthics_Click" Text="Benthics" 
                            CssClass="adminButton"/>

                </td>
            <td>

                <asp:Button ID="btnBioResultsTypes" runat="server" OnClick="btnBioResultsTypes_Click" Text="Bio Results Types" 
                            CssClass="adminButton"/>

                </td>
            <td>

                <asp:Button ID="btnCommunities" runat="server" OnClick="btnCommunities_Click" Text="Communities" 
                            CssClass="adminButton"/>


                </td>
        </tr>
        <tr>
            <td>                


                <asp:Button ID="btnCounties" runat="server" OnClick="btnCounties_Click" Text="Counties" 
                            CssClass="adminButton"/>

                </td>
            <td>



                <asp:Button ID="btnEcoRegions" runat="server" OnClick="btnEcoRegions_Click" Text="Eco Regions" 
                            CssClass="adminButton"/>

                </td>
            <td>

                <asp:Button ID="btnEquipment" runat="server" OnClick="btnEquipment_Click" Text="Equipment" 
                            CssClass="adminButton"/>

                </td>
            <td>

                <asp:Button ID="btnEquipCategories" runat="server" OnClick="btnEquipCategories_Click" Text="Equipment Categories" 
                            CssClass="adminButton"/>

                </td>
            <td>

                <asp:Button ID="btnEquipItem" runat="server" OnClick="btnEquipItems_Click" Text="Equipment Items" 
                            CssClass="adminButton"/>

        

                </td>
        </tr>
        <tr>
            <td>

        

                <asp:Button ID="btnFieldGear" runat="server" OnClick="btnFieldGear_Click" Text="Field Gear" 
                            CssClass="adminButton"/>

                </td>
            <td>

                <asp:Button ID="btnFieldProcedure" runat="server" OnClick="btnFieldProcedure_Click" Text="Field Procedures"
                            CssClass="adminButton"/>

                </td>
            <td>

                <asp:Button ID="btnGear" runat="server" OnClick="btnGear_Click" Text="Gear" 
                            CssClass="adminButton"/> 

                </td>
            <td> 

                <asp:Button ID="btnGrid" runat="server" OnClick="btnGrid_Click" Text="Grids"
                            CssClass="adminButton"/>

                </td>
            <td>


                <asp:Button ID="btnHydroUnit" runat="server" OnClick="btnHydroUnit_Click" Text="Hydro Units" 
                            CssClass="adminButton"/>



                </td>
        </tr>
        <tr>
            <td>



                <asp:Button ID="brnLimits" runat="server" OnClick="brnLimits_Click" Text="Measurement Limits" 
                            CssClass="adminButton"/> 

                </td>
            <td> 

                <asp:Button ID="btnMetalBarCodeType" runat="server" OnClick="btnMetalBarCodeType_Click" Text="Metal Bar Code Types" 
                            CssClass="adminButton"/>

                </td>
            <td>

                <asp:Button ID="btnMetalLimits" runat="server" Text="Metal Val Limits" CssClass="adminButton" OnClick="btnMetalLimits_Click"/>
            </td>
            <td>

                <asp:Button ID="btnNutrientBarCodeType" runat="server" OnClick="btnNutrientBarCodeType_Click" Text="Nutrient Barcode Types" 
                            CssClass="adminButton" Width="195px"/>

                </td>
            <td>

                <asp:Button ID="btnNutrientLimits" runat="server" Text="Nutrient Val Limits" CssClass="adminButton" OnClick="btnNutrientLimits_Click"/>
  
                </td>
        </tr>
        <tr>
            <td>
  
                &nbsp;</td>
            <td>              

                <asp:Button ID="btnOrganizationType" runat="server" OnClick="btnOrganizationType_Click" Text="Organization Types" 
                            CssClass="adminButton"/>
  
            </td>
            <td>
  
                <asp:Button ID="btnPhysHabPara" runat="server" OnClick="btnPhysHabPara_Click" Text="Phys Hab Para" 
                            CssClass="adminButton"/>              

                </td>
            <td>

                <asp:Button ID="btnEditPhysHab" runat="server" OnClick="btnEditPhysHab_Click" Text="Phys Hab" 
                            CssClass="adminButton"/>

              </td>
            <td>

                <asp:Button ID="btnProject" runat="server" OnClick="btnProject_Click" Text="Projects" 
                            CssClass="adminButton"/>

                </td>
        </tr>
        <tr>
            <td>

                <asp:Button ID="btnQUADI" runat="server" OnClick="btnQUADI_Click" Text="QUADI" 
                            CssClass="adminButton"/>

              </td>
            <td>

              <asp:Button ID="btnQuarterSection" runat="server" OnClick="btnQuarterSection_Click" Text="Quarter Sections" 
                            CssClass="adminButton"/>

                </td>
            <td>

                <asp:Button ID="btnRange" runat="server" OnClick="btnRange_Click" Text="Ranges" 
                            CssClass="adminButton"/>

              </td>
            <td>

              <asp:Button ID="btnRegion" runat="server" OnClick="btnRegion_Click" Text="Regions" 
                            CssClass="adminButton"/>

                </td>
            <td>    

                <asp:Button ID="btnRiverWatchWaterShed" runat="server" OnClick="btnRiverWatchWaterShed_Click" Text="RW Water Sheds" 
                            CssClass="adminButton"/>

              </td>
        </tr>
        <tr>
            <td>

                <asp:Button ID="btnSampleType" runat="server" OnClick="btnSampleType_Click" Text="Sample Types" 
                            CssClass="adminButton"/>    

               </td>
            <td>
 
                <asp:Button ID="btnSection" runat="server" OnClick="btnSection_Click" Text="Sections" 
                            CssClass="adminButton"/>

                </td>
            <td>

              <asp:Button ID="btnState" runat="server" OnClick="btnState_Click" Text="States" 
                            CssClass="adminButton"/>
 
                </td>
            <td>

               <asp:Button ID="btnStationQUAD" runat="server" OnClick="btnStationQUAD_Click" Text="Station QUADs" 
                            CssClass="adminButton"/>

                </td>
            <td>  

                <asp:Button ID="btnStationTypes" runat="server" OnClick="btnStationTypes_Click" Text="Station Types" 
                            CssClass="adminButton"/>

                </td>
        </tr>
        <tr>
            <td>

                <asp:Button ID="btnStationStatus" runat="server" OnClick="btnStationStatus_Click" Text="Station Statuses" 
                            CssClass="adminButton"/>  

                </td>
            <td>

                <asp:Button ID="btnSubPara" runat="server" OnClick="btnSubPara_Click" Text="Sub Paras" 
                            CssClass="adminButton"/>

                </td>
            <td>

                <asp:Button ID="btnEditSubSamps" runat="server" OnClick="btnEditSubSamps_Click" Text="Sub Samps" 
                            CssClass="adminButton"/>

                </td>
            <td>

                <asp:Button ID="btnTownship" runat="server" OnClick="btnTownship_Click" Text="Townships" 
                            CssClass="adminButton"/>

                </td>
            <td>

                <asp:Button ID="btntblWBKey" runat="server" OnClick="btntblWBKey_Click" Text="Water Body IDs" 
                            CssClass="adminButton"/>

                </td>
        </tr>
        <tr>
            <td>

                <asp:Button ID="btnWaterCodeDrainage" runat="server" OnClick="btnWaterCodeDrainage_Click" Text="Water Code Drainage" 
                            CssClass="adminButton"/>

                </td>
            <td>
               
                <asp:Button ID="btnWQCCWaterShed" runat="server" OnClick="btnWQCCWaterShed_Click" Text="WQCC Water Sheds" 
                            CssClass="adminButton"/>

            </td>
            <td>

                <asp:Button ID="btnWSG" runat="server" OnClick="btnWSG_Click" Text="WSGs" 
                            CssClass="adminButton"/>
               
                </td>
            <td>
               
                <asp:Button ID="btnWSR" runat="server" OnClick="btnWSR_Click" Text="WSRs" 
                            CssClass="adminButton"/>

            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>



                &nbsp;

</asp:Content>
