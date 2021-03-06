﻿<%@ Page Title="Edit ExpWater" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="EditExpWater.aspx.cs" Inherits="RWInbound2.Admin.EditExpWater" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" ClientIDMode="Static" runat="server">
    <div>
        <hgroup>
            <h3><%: Page.Title %></h3>
        </hgroup>

        <div class="label-placement">
            <asp:Label ID="ErrorLabel" CssClass="label-error" runat="server" />               
        </div>
        <div class="label-placement">
             <asp:Label ID="SuccessLabel" CssClass="label-success" runat="server" />
        </div>
        <br />            
    </div> 
    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    Search By Sample Number:
                    <asp:TextBox ID="sampleNumberSearch"
                        onkeydown = "return (event.keyCode!=13);"                         
                        runat="server"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" Height="31px" OnClick="btnSearch_Click" CssClass="adminButton"/>
                    <asp:Button ID="btnSearchRefresh" runat="server" Text="Reset Search" Height="31px" 
                                OnClick="btnSearchRefresh_Click" CssClass="adminButton" />
                    <ajaxToolkit:AutoCompleteExtender 
                        ID="tbSearch_AutoCompleteExtender" 
                        runat="server" 
                        BehaviorID="tbSearch_AutoCompleteExtender" 
                        DelimiterCharacters=""  
                        ServiceMethod="SearchForExpWaterSampleNumber"             
                        TargetControlID="sampleNumberSearch"
                        MinimumPrefixLength="2"
                        CompletionInterval="100" 
                        EnableCaching="false" 
                        CompletionSetCount="10">
                    </ajaxToolkit:AutoCompleteExtender> 
            </ContentTemplate>
        </asp:UpdatePanel>  
    </div>       
    <asp:FormView ID="ExpWaterFormView" runat="server" 
        DataKeyNames="ID"
        ItemType="RWInbound2.NEWexpWater" 
        SelectMethod="GetExpWater"
        UpdateMethod="UpdateExpWater"
        DefaultMode="Edit"
        AllowPaging="true"
        EmptyDataText="No results found."
        PagerSettings-Position="TopAndBottom"
        OnPageIndexChanging="ExpWaterFormView_PageIndexChanging"
        CssClass="edit-exp-water-formview-td" >

        <EditItemTemplate> 
            <asp:Button ID="UpdateButton" runat="server" Text="Update" CommandName="Update" CssClass="adminButton" />
            <button class="adminButton" type="reset" value="Reset">Reset</button>
            <br />   
            <div class="edit-exp-water-div">
                <label>Event:</label>
                <asp:Label ID="Event" runat="server" Text='<%# Bind("Event") %>'></asp:Label> 
                <label>Sample Number:</label>
                <asp:Label ID="SampleNumber" runat="server" Text='<%# Bind("SampleNumber") %>'></asp:Label>  
                <label>Water Shed:</label>
                <asp:Label ID="WaterShed" runat="server" Text='<%# Bind("WaterShed") %>'></asp:Label>
                <label>River Name:</label>
                <asp:Label ID="RiverName" runat="server" Text='<%# Bind("RiverName") %>'></asp:Label>
            </div>   
            <hr class="edit-exp-water-hr" />  
            <div class="edit-exp-water-div">
                <label>Kit Number:</label>
                <asp:Label ID="KitNumber" runat="server" Text='<%# Bind("KitNumber") %>'></asp:Label>
                <label>Organization Name:</label>
                <asp:Label ID="OrganizationName" runat="server" Text='<%# Bind("OrganizationName") %>'></asp:Label>                
                <label>Station Number:</label>
                <asp:Label ID="StationNumber" runat="server" Text='<%# Bind("StationNumber") %>'></asp:Label>
            </div>             
            <hr class="edit-exp-water-hr" /> 
            <div class="edit-exp-water-div">
                <label>Type Code:</label>
                <asp:Label ID="TextBoxTypeCode" runat="server" Text='<%# Bind("TypeCode") %>'></asp:Label>  
                <label>Metals BarCode:</label>
                <asp:Label ID="TextBoxBarCode" runat="server" Text='<%# Bind("MetalsBarCode") %>'></asp:Label>   
                <label>Nutrient BarCode:</label>
                <asp:Label ID="TextBoxNutrientBarCode" runat="server" Text='<%# Bind("NutrientBarCode") %>'></asp:Label>                  
                <label>Sample Date:</label> 
                <asp:Label ID="TextBoxSampleDate" runat="server" Text='<%# Bind("SampleDate") %>'></asp:Label>                
            </div>  
            <hr class="edit-exp-water-hr" />  
            <br />  
            <table>
                <tr>
                    <th class="edit-exp-water-th">Field Data</th>
                    <th class="edit-exp-water-th">Metal Data</th>
                    <th class="edit-exp-water-th">Nutrient Data</th>
                </tr>
                <tr>
                    <td>
                        <div class="edit-exp-water-td-div">
                            <label>USGS_Flow:</label>
                            <asp:TextBox ID="TextBoxUSGS_Flow" runat="server" Text='<%# Bind("USGS_Flow") %>'></asp:TextBox>                            
                        </div>                        
                        <div class="edit-exp-water-td-div">
                            <label>PH:</label>
                            <asp:TextBox ID="TextBoxPH" runat="server" Text='<%# Bind("PH") %>'></asp:TextBox>                            
                        </div>
                        <div class="edit-exp-water-td-div">
                            <label>TempC:</label>
                            <asp:TextBox ID="TextBoxTempC" runat="server" Text='<%# Bind("TempC") %>'></asp:TextBox>                            
                        </div>
                        <div class="edit-exp-water-td-div">
                            <label>PHEN_ALK:</label>
                            <asp:TextBox ID="TextBoxPHEN_ALK" runat="server" Text='<%# Bind("PHEN_ALK") %>'></asp:TextBox>                            
                        </div>
                        <div class="edit-exp-water-td-div">
                            <label>TOTAL_ALK:</label>
                            <asp:TextBox ID="TextBoxTOTAL_ALK" runat="server" Text='<%# Bind("TOTAL_ALK") %>'></asp:TextBox>                            
                        </div>
                        <div class="edit-exp-water-td-div">
                            <label>TOTAL_HARD:</label>
                            <asp:TextBox ID="TextBoxTOTAL_HARD" runat="server" Text='<%# Bind("TOTAL_HARD") %>'></asp:TextBox>                            
                        </div>
                        <div class="edit-exp-water-td-div">
                            <label>DO_ GL:</label>
                            <asp:TextBox ID="TextBoxDO_MGL" runat="server" Text='<%# Bind("DO_MGL") %>'></asp:TextBox>                            
                        </div>
                        <div class="edit-exp-water-td-div">
                            <label>DOSAT:</label>
                            <asp:TextBox ID="TextBoxDOSAT" runat="server" TextMode="Number" Text='<%# Bind("DOSAT") %>'></asp:TextBox> 
                        </div>                               
                    </td>
                    <td >
                        <div class="edit-exp-water-td-div">
                            <label>AL_D:</label>
                            <asp:TextBox ID="TextBoxAL_D" runat="server" Text='<%# Bind("AL_D") %>'></asp:TextBox>                            
                            <label>AL_T:</label>
                            <asp:TextBox ID="TextBoxAL_T" runat="server" Text='<%# Bind("AL_T") %>'></asp:TextBox>                            
                        </div>
                        <div class="edit-exp-water-td-div">
                            <label>AS_D:</label>
                            <asp:TextBox ID="TextBoxAS_D" runat="server" Text='<%# Bind("AS_D") %>'></asp:TextBox>                           
                            <label>AS_T:</label>
                            <asp:TextBox ID="TextBoxAS_T" runat="server" Text='<%# Bind("AS_T") %>'></asp:TextBox>                            
                        </div>
                        <div class="edit-exp-water-td-div">
                            <label>CA_D:</label>
                            <asp:TextBox ID="TextBoxCA_D" runat="server" Text='<%# Bind("CA_D") %>'></asp:TextBox>                            
                            <label>CA_T:</label>
                            <asp:TextBox ID="TextBoxCA_T" runat="server" Text='<%# Bind("CA_T") %>'></asp:TextBox>                            
                        </div>
                        <div class="edit-exp-water-td-div">
                            <label>CD_D:</label>
                            <asp:TextBox ID="TextBoxCD_D" runat="server" Text='<%# Bind("CD_D") %>'></asp:TextBox>                           
                            <label>CD_T:</label>
                            <asp:TextBox ID="TextBoxCD_T" runat="server" Text='<%# Bind("CD_T") %>'></asp:TextBox>                           
                        </div>
                        <div class="edit-exp-water-td-div">
                            <label>CU_D:</label>
                            <asp:TextBox ID="TextBoxCU_D" runat="server" Text='<%# Bind("CU_D") %>'></asp:TextBox>                            
                            <label>CU_T:</label>
                            <asp:TextBox ID="TextBoxCU_T" runat="server" Text='<%# Bind("CU_T") %>'></asp:TextBox>                            
                        </div>
                        <div class="edit-exp-water-td-div">
                            <label>FE_D:</label>
                            <asp:TextBox ID="TextBoxFE_D" runat="server" Text='<%# Bind("FE_D") %>'></asp:TextBox>                           
                            <label>FE_T:</label>
                            <asp:TextBox ID="TextBoxFE_T" runat="server" Text='<%# Bind("FE_T") %>'></asp:TextBox>                            
                        </div>
                        <div class="edit-exp-water-td-div">
                            <label>MG_D:</label>
                            <asp:TextBox ID="TextBoxMG_D" runat="server" Text='<%# Bind("MG_D") %>'></asp:TextBox>                            
                            <label>MG_T:</label>
                            <asp:TextBox ID="TextBoxMG_T" runat="server" Text='<%# Bind("MG_T") %>'></asp:TextBox> 
                        </div>
                        <div class="edit-exp-water-td-div">
                            <label>MN_D:</label>
                            <asp:TextBox ID="TextBoxMN_D" runat="server" Text='<%# Bind("MN_D") %>'></asp:TextBox>                            
                            <label>MN_T:</label>
                            <asp:TextBox ID="TextBoxMN_T" runat="server" Text='<%# Bind("MN_T") %>'></asp:TextBox>                            
                        </div>
                        <div class="edit-exp-water-td-div">
                            <label>PB_D:</label>
                            <asp:TextBox ID="TextBoxPB_D" runat="server" Text='<%# Bind("PB_D") %>'></asp:TextBox>                            
                            <label>PB_T:</label>
                            <asp:TextBox ID="TextBoxPB_T" runat="server" Text='<%# Bind("PB_T") %>'></asp:TextBox>                            
                        </div>
                        <div class="edit-exp-water-td-div">
                            <label>SE_D:</label>
                            <asp:TextBox ID="TextBoxSE_D" runat="server" Text='<%# Bind("SE_D") %>'></asp:TextBox>                            
                            <label>SE_T:</label>
                            <asp:TextBox ID="TextBoxSE_T" runat="server" Text='<%# Bind("SE_T") %>'></asp:TextBox>                            
                        </div>
                        <div class="edit-exp-water-td-div">
                            <label>ZN_D:</label>
                            <asp:TextBox ID="TextBoxZN_D" runat="server" Text='<%# Bind("ZN_D") %>'></asp:TextBox>                            
                            <label>ZN_T:</label>
                            <asp:TextBox ID="TextBoxZN_T" runat="server" Text='<%# Bind("ZN_T") %>'></asp:TextBox>  
                        </div>
                        <div class="edit-exp-water-td-div">
                            <label>NA_D:</label>
                            <asp:TextBox ID="TextBoxNA_D" runat="server" Text='<%# Bind("NA_D") %>'></asp:TextBox>                            
                            <label>NA_T:</label>
                            <asp:TextBox ID="TextBoxNA_T" runat="server" Text='<%# Bind("NA_T") %>'></asp:TextBox>                            
                        </div>                       
                        <div class="edit-exp-water-td-div">
                            <label>K_D:</label>
                            <asp:TextBox ID="TextBoxK_D" runat="server" Text='<%# Bind("K_D") %>'></asp:TextBox> 
                                                       
                            <label>&nbsp;K_T:</label>
                            <asp:TextBox ID="TextBoxK_T" runat="server" Text='<%# Bind("K_T") %>'></asp:TextBox> 
                        </div>                                                                            
                    </td>
                    <td>
                        <div class="edit-exp-water-td-div">
                            <label>Ammonia:</label>
                            <asp:TextBox ID="TextBoxAmmonia" runat="server" Text='<%# Bind("Ammonia") %>'></asp:TextBox>                            
                        </div>
                        <div class="edit-exp-water-td-div">
                            <label>Chloride:</label>
                            <asp:TextBox ID="TextBoxChloride" runat="server" Text='<%# Bind("Chloride") %>'></asp:TextBox>                            
                        </div>
                        <div class="edit-exp-water-td-div">
                            <label>ChlorophyllA:</label>
                            <asp:TextBox ID="TextBoxChlorophyllA" runat="server" Text='<%# Bind("ChlorophyllA") %>'></asp:TextBox>                           
                        </div>
                        <div class="edit-exp-water-td-div">
                            <label>DOC:</label>
                            <asp:TextBox ID="TextBoxDOC" runat="server" Text='<%# Bind("DOC") %>'></asp:TextBox>                            
                        </div>
                        <div class="edit-exp-water-td-div">
                            <label>NN:</label>
                            <asp:TextBox ID="TextBoxNN" runat="server" Text='<%# Bind("NN") %>'></asp:TextBox>                            
                        </div>
                        <div class="edit-exp-water-td-div">
                            <label>OP:</label>
                            <asp:TextBox ID="TextBoxOP" runat="server" Text='<%# Bind("OP") %>'></asp:TextBox>                            
                        </div>
                        <div class="edit-exp-water-td-div">
                            <label>Sulfate:</label>
                            <asp:TextBox ID="TextBoxSulfate" runat="server" Text='<%# Bind("Sulfate") %>'></asp:TextBox>                            
                        </div>
                        <div class="edit-exp-water-td-div">
                            <label>totN:</label>
                            <asp:TextBox ID="TextBoxtotN" runat="server" Text='<%# Bind("totN") %>'></asp:TextBox>                            
                        </div>
                        <div class="edit-exp-water-td-div">
                            <label>totP:</label>
                            <asp:TextBox ID="TextBoxtotP" runat="server" Text='<%# Bind("totP") %>'></asp:TextBox>                            
                        </div>
                        <div class="edit-exp-water-td-div">
                            <label>TKN:</label>
                            <asp:TextBox ID="TextBoxTKN" runat="server" Text='<%# Bind("TKN") %>'></asp:TextBox>                            
                        </div>
                        <div class="edit-exp-water-td-div">
                            <label>orgN:</label>
                            <asp:TextBox ID="TextBoxorgN" runat="server" Text='<%# Bind("orgN") %>'></asp:TextBox>                            
                        </div>
                        <div class="edit-exp-water-td-div">
                            <label>TSS:</label>
                            <asp:TextBox ID="TextBoxTSS" runat="server" Text='<%# Bind("TSS") %>'></asp:TextBox>                            
                        </div>
                        <div class="edit-exp-water-td-div">                               
                            <label>Batch:</label>
                            <asp:TextBox ID="TextBoxBatch" runat="server" TextMode="Number" Text='<%# Bind("Batch") %>'></asp:TextBox>  
                        </div>                             
                    </td>
                </tr> 
            </table>
            <div class="edit-exp-water-div-h3">
                <h3>Comments</h3>
            </div>
            <div class="edit-exp-water-comments-div">
                <label>Sample Comments:</label>
                <asp:TextBox ID="TextBoxSampleComments" runat="server" TextMode="MultiLine" Text='<%# Bind("SampleComments") %>'></asp:TextBox>
                <label>Field Comment:</label>
                <asp:TextBox ID="TextBoxFieldComment" runat="server" TextMode="MultiLine" Text='<%# Bind("FieldComment") %>'></asp:TextBox>
                <label>Nutrient Comment:</label>
                <asp:TextBox ID="TextBoxNutrientComment" runat="server" TextMode="MultiLine" Text='<%# Bind("NutrientComment") %>'></asp:TextBox>  
            </div>   
            <div class="edit-exp-water-comments-div">
                <label>Metals Comment:</label>
                <asp:TextBox ID="TextBoxMetalsComment" runat="server" TextMode="MultiLine" Text='<%# Bind("MetalsComment") %>'></asp:TextBox>
                <label>Bugs Comments:</label>
                <asp:TextBox ID="TextBoxBugsComments" runat="server" TextMode="MultiLine" Text='<%# Bind("BugsComments") %>'></asp:TextBox>
                <label>Benthics Comments:</label>
                <asp:TextBox ID="TextBoxBenthicsComments" runat="server" TextMode="MultiLine" Text='<%# Bind("BenthicsComments") %>'></asp:TextBox> 
            </div> 
            <div class="edit-exp-water-error-div">
                <asp:CompareValidator ControlToValidate="TextBoxUSGS_Flow" Operator="DataTypeCheck" 
                                ID="CompareValidator1" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                Display="Dynamic" ErrorMessage="Please enter a valid USGS_Flow value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxPH" Operator="DataTypeCheck" 
                                ID="CompareValidator2" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                Display="Dynamic" ErrorMessage="Please enter a valid PH value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxTempC" Operator="DataTypeCheck" 
                                        ID="CompareValidator3" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                        Display="Dynamic" ErrorMessage="Please enter a valid TempC value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxPHEN_ALK" Operator="DataTypeCheck" 
                                        ID="CompareValidator4" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                        Display="Dynamic" ErrorMessage="Please enter a valid PHEN_ALK value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxTOTAL_ALK" Operator="DataTypeCheck" 
                                        ID="CompareValidator5" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                        Display="Dynamic" ErrorMessage="Please enter a valid TOTAL_ALK value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxTOTAL_HARD" Operator="DataTypeCheck" 
                                        ID="CompareValidator6" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                        Display="Dynamic" ErrorMessage="Please enter a valid TOTAL_HARD value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxDO_MGL" Operator="DataTypeCheck" 
                                        ID="CompareValidator7" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                        Display="Dynamic" ErrorMessage="Please enter a valid DO_MGL value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxAL_D" Operator="DataTypeCheck" 
                                        ID="CompareValidator8" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                        Display="Dynamic" ErrorMessage="Please enter a valid AL_D value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxAL_T" Operator="DataTypeCheck" 
                                        ID="CompareValidator9" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                        Display="Dynamic" ErrorMessage="Please enter a valid AL_T value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxAS_D" Operator="DataTypeCheck" 
                                        ID="CompareValidator10" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                        Display="Dynamic" ErrorMessage="Please enter a valid AS_D value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxAS_T" Operator="DataTypeCheck" 
                                        ID="CompareValidator11" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                        Display="Dynamic" ErrorMessage="Please enter a valid AS_T value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxCA_D" Operator="DataTypeCheck" 
                                        ID="CompareValidator12" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                        Display="Dynamic" ErrorMessage="Please enter a valid CA_D value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxCA_T" Operator="DataTypeCheck" 
                                        ID="CompareValidator13" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                        Display="Dynamic" ErrorMessage="Please enter a valid CA_T value.">
                </asp:CompareValidator>
                 <asp:CompareValidator ControlToValidate="TextBoxCD_D" Operator="DataTypeCheck" 
                                        ID="CompareValidator14" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                        Display="Dynamic" ErrorMessage="Please enter a valid CD_D value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxCU_T" Operator="DataTypeCheck" 
                                        ID="CompareValidator15" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                        Display="Dynamic" ErrorMessage="Please enter a valid CU_T value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxFE_D" Operator="DataTypeCheck" 
                                        ID="CompareValidator16" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                        Display="Dynamic" ErrorMessage="Please enter a valid FE_D value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxFE_T" Operator="DataTypeCheck" 
                                        ID="CompareValidator17" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                        Display="Dynamic" ErrorMessage="Please enter a valid FE_T value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxMG_D" Operator="DataTypeCheck" 
                                        ID="CompareValidator18" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                        Display="Dynamic" ErrorMessage="Please enter a valid MG_D value.">
                </asp:CompareValidator>                
                <asp:CompareValidator ControlToValidate="TextBoxMG_T" Operator="DataTypeCheck" 
                                        ID="CompareValidator20" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                        Display="Dynamic" ErrorMessage="Please enter a valid MG_T value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxMN_D" Operator="DataTypeCheck" 
                                        ID="CompareValidator21" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                        Display="Dynamic" ErrorMessage="Please enter a valid MN_D value.">
                </asp:CompareValidator> 
                <asp:CompareValidator ControlToValidate="TextBoxMN_T" Operator="DataTypeCheck" 
                                        ID="CompareValidator22" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                        Display="Dynamic" ErrorMessage="Please enter a valid MN_T value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxPB_D" Operator="DataTypeCheck" 
                                ID="CompareValidator23" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                Display="Dynamic" ErrorMessage="Please enter a valid PB_D value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxPB_T" Operator="DataTypeCheck" 
                                ID="CompareValidator24" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                Display="Dynamic" ErrorMessage="Please enter a valid PB_T value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxSE_D" Operator="DataTypeCheck" 
                                ID="CompareValidator25" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                Display="Dynamic" ErrorMessage="Please enter a valid SE_D value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxSE_T" Operator="DataTypeCheck" 
                                ID="CompareValidator26" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                Display="Dynamic" ErrorMessage="Please enter a valid SE_T value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxZN_D" Operator="DataTypeCheck" 
                                ID="CompareValidator27" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                Display="Dynamic" ErrorMessage="Please enter a valid ZN_D value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxZN_T" Operator="DataTypeCheck" 
                                ID="CompareValidator28" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                Display="Dynamic" ErrorMessage="Please enter a valid ZN_T value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxNA_D" Operator="DataTypeCheck" 
                                ID="CompareValidator29" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                Display="Dynamic" ErrorMessage="Please enter a valid NA_D value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxNA_T" Operator="DataTypeCheck" 
                                ID="CompareValidator30" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                Display="Dynamic" ErrorMessage="Please enter a valid NA_T value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxK_D" Operator="DataTypeCheck" 
                                ID="CompareValidator31" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                Display="Dynamic" ErrorMessage="Please enter a valid K_D value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxK_T" Operator="DataTypeCheck" 
                                ID="CompareValidator32" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                Display="Dynamic" ErrorMessage="Please enter a valid K_T value.">
                </asp:CompareValidator> 
                <asp:CompareValidator ControlToValidate="TextBoxAmmonia" Operator="DataTypeCheck" 
                                ID="CompareValidator33" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                Display="Dynamic" ErrorMessage="Please enter a valid Ammonia value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxChloride" Operator="DataTypeCheck" 
                                ID="CompareValidator34" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                Display="Dynamic" ErrorMessage="Please enter a valid Chloride value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxChlorophyllA" Operator="DataTypeCheck" 
                                ID="CompareValidator35" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                Display="Dynamic" ErrorMessage="Please enter a valid ChlorophyllA value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxDOC" Operator="DataTypeCheck" 
                                ID="CompareValidator36" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                Display="Dynamic" ErrorMessage="Please enter a valid DOC value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxNN" Operator="DataTypeCheck" 
                                ID="CompareValidator37" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                Display="Dynamic" ErrorMessage="Please enter a valid NN value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxOP" Operator="DataTypeCheck" 
                                ID="CompareValidator38" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                Display="Dynamic" ErrorMessage="Please enter a valid OP value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxSulfate" Operator="DataTypeCheck" 
                                ID="CompareValidator39" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                Display="Dynamic" ErrorMessage="Please enter a valid Sulfate value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxtotN" Operator="DataTypeCheck" 
                                ID="CompareValidator40" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                Display="Dynamic" ErrorMessage="Please enter a valid totN value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxtotP" Operator="DataTypeCheck" 
                                ID="CompareValidator41" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                Display="Dynamic" ErrorMessage="Please enter a valid totP value." ValidationGroup="">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxTKN" Operator="DataTypeCheck" 
                                ID="CompareValidator42" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                Display="Dynamic" ErrorMessage="Please enter a valid TKN value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxorgN" Operator="DataTypeCheck" 
                                ID="CompareValidator43" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                Display="Dynamic" ErrorMessage="Please enter a valid orgN value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxTSS" Operator="DataTypeCheck" 
                                ID="CompareValidator45" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                Display="Dynamic" ErrorMessage="Please enter a valid TSS value.">
                </asp:CompareValidator>  
            </div> 

            <asp:HiddenField id="HiddenField_Event" runat="server" value='<%# Bind("Event") %>' />
            <asp:HiddenField id="HiddenField_WaterShed" runat="server" value='<%# Bind("WaterShed") %>' />
            <asp:HiddenField id="HiddenField_SampleNumber" runat="server" value='<%# Bind("SampleNumber") %>' />
            <asp:HiddenField id="HiddenField_RiverName" runat="server" value='<%# Bind("RiverName") %>' />
            <asp:HiddenField id="HiddenField_KitNumber" runat="server" value='<%# Bind("KitNumber") %>' />
            <asp:HiddenField id="HiddenField_OrganizationName" runat="server" value='<%# Bind("OrganizationName") %>' />

            <asp:HiddenField id="HiddenField_OrganizationID" runat="server" value='<%# Bind("OrganizationID") %>' />
            <asp:HiddenField id="HiddenField_StationNumber" runat="server" value='<%# Bind("StationNumber") %>' />
            <asp:HiddenField id="HiddenField_StationName" runat="server" value='<%# Bind("StationName") %>' />
            <asp:HiddenField id="HiddenField_StationID" runat="server" value='<%# Bind("StationID") %>' />
            <asp:HiddenField id="HiddenField_TypeCode" runat="server" value='<%# Bind("TypeCode") %>' />
            <asp:HiddenField id="HiddenField_MetalsBarCode" runat="server" value='<%# Bind("MetalsBarCode") %>' />
            <asp:HiddenField id="HiddenField_NutrientBarCode" runat="server" value='<%# Bind("NutrientBarCode") %>' />
            <asp:HiddenField id="HiddenField_FieldBarCode" runat="server" value='<%# Bind("FieldBarCode") %>' />
            <asp:HiddenField id="HiddenField_BugsBarCode" runat="server" value='<%# Bind("BugsBarCode") %>' />
            <asp:HiddenField id="HiddenField_SampleDate" runat="server" value='<%# Bind("SampleDate") %>' />

            <asp:HiddenField id="HiddenField_ID" runat="server" value='<%# Bind("ID") %>' />
            <asp:HiddenField id="HiddenField_tblSampleID" runat="server" value='<%# Bind("tblSampleID") %>' />
            <asp:HiddenField id="HiddenField_BadBlank" runat="server" value='<%# Bind("BadBlank") %>' />
            <asp:HiddenField id="HiddenField_BadDuplicate" runat="server" value='<%# Bind("BadDuplicate") %>' />
            <asp:HiddenField id="HiddenField_BadSample" runat="server" value='<%# Bind("BadSample") %>' />
            <asp:HiddenField id="HiddenField_Valid" runat="server" value='<%# Bind("Valid") %>' />
        </EditItemTemplate>            
    </asp:FormView>            
</asp:Content>
