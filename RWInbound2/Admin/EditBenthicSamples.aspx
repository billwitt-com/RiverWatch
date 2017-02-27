<%@ Page Title="Edit Benthic Samples" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditBenthicSamples.aspx.cs" Inherits="RWInbound2.Admin.EditBenthicSamples" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server" ClientIDMode="Static">
    <section spellcheck="true" class="benthics-samples-section">
        <asp:UpdateProgress ID="UpdateProgress" runat="server">
            <ProgressTemplate>
                <div class="modal">
                    <div class="center">
                        <img alt="Loading Please Wait" src="../Images/loader.gif"/>
                        <span class="label-loading">Loading Please Wait...</span>
                    </div>
                </div>     
            </ProgressTemplate>
        </asp:UpdateProgress> 
        <!-- ************************* Main Sample Panel - Start *******************************-->
        <asp:UpdatePanel ID="BenthicSamplesGridView_UpdatePanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div>
                    <hgroup class="edit-benthic-samples-headergroup">
                        <h3><%: Page.Title %></h3>
                    </hgroup>
                    <div class="edit-benthic-samples-link-div">
                  <%--<a id="linkToManageBugsPhysHab" class="edit-benthic-samples-link-to-manage-bugs-physHab">Back to Manage Bugs and Phys Hab</a>--%>
                        <asp:Button ID="btnLinkToManageBugsPhysHab" runat="server" OnClick="btnLinkToManageBugsPhysHab_Click" 
                                    Text="Back to Enter/Edit Benthics or Physical Habitat Data" CssClass="adminButton edit-benthic-samples-link-to-manage-bugs-physHab"/>
                    </div>
                    <div class="label-placement">
                        <asp:Label ID="ErrorLabel" CssClass="label-error" runat="server" />               
                    </div>
                    <div class="label-placement">
                         <asp:Label ID="SuccessLabel" CssClass="label-success" runat="server" />
                    </div>
                    <br />            
                </div>
                <br />
                <asp:Panel ID="SampleData_Panel" runat="server">
                    <div>
                        <b>Sample Number:</b>
                        <asp:Label ID="lblSampleNumber" runat="server"></asp:Label>
                        <div class="edit-benthics-sample-number">
                            <b>Event:</b>
                            <asp:Label ID="lblSampleEvent" runat="server"></asp:Label>
                        </div>   
                        <asp:HiddenField id="HiddenField_SampleID" runat="server" />
                        <asp:HiddenField id="HiddenField_DateCollected" runat="server" />
                        <asp:HiddenField id="HiddenField_TimeCollected" runat="server" />
                    </div>
                    <br />
                </asp:Panel>
                <asp:GridView ID="BenthicSamplesGridView" runat="server"
                    DataKeyNames="ID"
                    ItemType="RWInbound2.tblBenSamp" 
                    SelectMethod="GetBenthicSamples"
                    UpdateMethod="UpdateBenthicSample"
                    DeleteMethod="DeleteBenthicSample" 
                    InsertItemPosition="LastItem"  
                    OnRowEditing="BenthicSamplesGridView_RowEditing"
                    OnRowCommand="BenthicSamplesGridView_RowCommand"                                    
                    ShowFooter="true"
                    CellPadding="4"
                    AutoGenerateColumns="False" CssClass="grid-columns-center grid-edit-benthics-samples-item-rowstyle"
                    HeaderStyle-CssClass="grid-edit-benthics-samples-header"                   
                    ForeColor="#333333" 
                    AllowPaging="true" Pagesize="10">
                    <AlternatingRowStyle BackColor="White" />
                    <EmptyDataTemplate>                        
                        <div class="grid-edit-benthics-samples-empty-data-div">
                            No Samples were found.
                             <%-- <% =Show() %>--%>
                            <asp:Panel ID="NoResultsPanel" runat="server" Visible="false" OnInit="Show" OnDataBinding="Show" OnLoad="Show"
                                 CssClass="grid-edit-benthics-samples-no-results-panel" >                              
                                <asp:Button ID="btnAddNewSample" runat="server" Text="Add New Benthic Sample" OnClick="AddNewBenthicSample"
                                            CssClass="adminButton grid-edit-benthics-samples-add-new-button" />                    
                            </asp:Panel>
                        </div>
                    </EmptyDataTemplate>    
                    <Columns>  
                        <asp:TemplateField>
                            <ItemTemplate>         
                                <asp:Button ID="DeleteButton" runat="server" Text="Delete" CommandName="Delete" 
                                      ToolTip="Make sure all related Reps, Grids and Taxa are deleted first!!!"
                                                OnClientClick="return confirm('Are you certain you want to delete this? \r\n Have you deleted all related Reps, Grids and Taxa?');"/> 
                            </ItemTemplate>
                            <EditItemTemplate>                             
                            </EditItemTemplate>
                            <FooterTemplate>                               
                                <asp:Button ID="btnAdd" runat="server" Text="Add"
                                             OnClick="AddNewBenthicSample" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ID" HeaderText="ID" Visible="false" ReadOnly="True" SortExpression="ID" />
                        <asp:TemplateField HeaderText="Collected" SortExpression="CollDate">                   
                            <ItemTemplate>
                                <asp:Label ID="lblCollDate" runat="server" Text='<%# Eval("CollDate", "{0:M-dd-yyyy}") %>'></asp:Label>
                       
                                <asp:Label ID="lblCollTime" runat="server" Text='<%# Eval("CollTime", "{0:HH:mm}") %>'></asp:Label>                                                                           
                            </ItemTemplate>                   
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Activity" SortExpression="tlkActivityCategory.Description">                   
                            <ItemTemplate>
                                <asp:Label ID="lbltlkActivityCategoryDescription" runat="server" Text='<%# Bind("tlkActivityCategory.Description") %>'></asp:Label>
                            </ItemTemplate>                    
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="Intent" SortExpression="tlkIntent.Description">                   
                            <ItemTemplate>
                                <asp:Label ID="lbltlkIntentDescription" runat="server" Text='<%# Bind("tlkIntent.Description") %>'></asp:Label>
                            </ItemTemplate>                    
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="Community" SortExpression="tlkCommunity.Description">                   
                            <ItemTemplate>
                                <asp:Label ID="lbltlkCommunityDescription" runat="server" Text='<%# Bind("tlkCommunity.Description") %>'></asp:Label>
                            </ItemTemplate>                    
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="Bio Result Type" SortExpression="tlkBioResultsType.Description">                   
                            <ItemTemplate>
                                <asp:Label ID="lbltlkBioResultsTypeDescription" runat="server" Text='<%# Bind("tlkBioResultsType.Description") %>'></asp:Label>
                            </ItemTemplate>                    
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reps/Grids">                   
                            <ItemTemplate>
                                <asp:Button ID="ViewRepsGridButton" runat="server" Text="View"  
                                            CommandName="GetAllBenthicsData" CommandArgument='<%# ((GridViewRow) Container).RowIndex+","+ Eval("ID") %>'/>                               
                                <asp:HiddenField id="HiddenField_SelectedGridRowBenSampID" runat="server" value='<%# Eval("ID") %>' />
                            </ItemTemplate>                   
                        </asp:TemplateField>                             
                    </Columns>
                    <EditRowStyle BackColor="antiquewhite" />            
                </asp:GridView> 
            </ContentTemplate>           
        </asp:UpdatePanel>
        <!-- ************************* Main Sample Panel - End   *******************************-->
        <div class="benthic-group-all-views">
            <div class="benthic-data-formview">
            <!-- ************************* Benthics Data Panel - Start *******************************-->
                <asp:UpdatePanel ID="BenthicDataFormView_UpdatePanel" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="BenthicDataFormView_Panel" runat="server" Visible="false">
                            <h4>Benthics Data</h4>
                            <div class="benthics-data-label-placement">
                                <asp:Label ID="BenthicsDataErrorLabel" CssClass="benthics-data-label-error" runat="server" />               
                            </div>
                            <div class="benthics-data-label-placement">
                                 <asp:Label ID="BenthicsDataSuccessLabel" CssClass="benthics-data-label-success" runat="server" />
                            </div>
                            <asp:FormView ID="BenthicDataFormView" runat="server" 
                                    DataKeyNames="ID"
                                    ItemType="RWInbound2.tblBenSamp" 
                                    SelectMethod="GetSelectedBenthicData"
                                    UpdateMethod="UpdateOrAddBenthicData"                             
                                    OnItemCommand="BenthicDataFormView_ItemCommand"
                                    DefaultMode="Edit"
                                    AllowPaging="false"
                                    EmptyDataText="No results found.">                
                                <EditItemTemplate> 
                                    <div class="benthics-samples-form-view-labels">
                                        <label>Activity:</label>
                                    </div>
                                    <asp:DropDownList ID="dropDownActivity" runat="server" DataMember="it"
                                                        SelectMethod="BindActivityCategories" CssClass="benthics-samples-formview-dropdowns"   
                                                        SelectedValue='<%# Bind("ActivityID") %>'                                          
                                                        AppendDataBoundItems="true" DataTextField="Description" DataValueField="ID">
                                    </asp:DropDownList>   
                                    <br />
                                    <div class="benthics-samples-form-view-labels">
                                        <label>Collection Date:</label>
                                    </div>
                                    <div class="benthics-samples-formview-edit-template-div">
                                        <%# Eval("CollDate", "{0:M-dd-yyyy}") %>
                                    </div>
                                    <br /> 
                                    <div class="benthics-samples-form-view-labels">
                                        <label>Collection Time:</label>
                                    </div>
                                    <div class="benthics-samples-formview-edit-template-div">
                                        <%# Eval("CollTime", "{0:HH:mm}") %>
                                    </div>
                                    <br />
                                    <div class="benthics-samples-form-view-labels">
                                        <label>Enter Date:</label>
                                    </div>
                                    <div class="benthics-samples-formview-edit-template-div">
                                        <%# Eval("EnterDate", "{0:M-dd-yyyy}") %>
                                    </div>
                                    <br />
                                    <div class="benthics-samples-form-view-labels">
                                        <label>Number Kicks:</label>
                                    </div>
                                    <asp:TextBox ID="txtNumKicksSamples" runat="server" Text='<%# Bind("NumKicksSamples") %>' 
                                                 CssClass="benthics-samples-form-view-width-number-textbox" TextMode="Number"></asp:TextBox> 
                                    <br />
                                    <div class="benthics-samples-form-view-labels">
                                        <label>Lab Count:</label>
                                    </div>
                                    <asp:TextBox ID="txtLabCount" runat="server" Text='<%# Bind("LabCount") %>' 
                                                 CssClass="benthics-samples-form-view-width-number-textbox" TextMode="Number"></asp:TextBox> 
                                    <br />
                                    <div class="benthics-samples-form-view-labels">
                                        <label>Collection Method:</label>
                                    </div>
                                    <asp:DropDownList ID="dropDownFieldProcedures" runat="server" DataMember="it"
                                                        SelectMethod="BindFieldProcedures" CssClass="benthics-samples-formview-dropdowns"   
                                                        SelectedValue='<%# Bind("CollMeth") %>'                                          
                                                        AppendDataBoundItems="true" DataTextField="Description" DataValueField="ID">
                                    </asp:DropDownList>
                                    <br />
                                    <div class="benthics-samples-form-view-labels">
                                        <label>Field Gear:</label>
                                    </div>
                                    <asp:DropDownList ID="dropDownFieldGears" runat="server" DataMember="it"
                                                        SelectMethod="BindFieldGears" CssClass="benthics-samples-formview-dropdowns"   
                                                        SelectedValue='<%# Bind("FieldGearID") %>'                                          
                                                        AppendDataBoundItems="true" DataTextField="Description" DataValueField="ID">
                                    </asp:DropDownList>
                                    <br />
                                    <div class="benthics-samples-form-view-labels">
                                        <label>Gear Configuration:</label>
                                    </div>
                                    <asp:DropDownList ID="dropDownGearConfigs" runat="server" DataMember="it"
                                                        SelectMethod="BindGearConfigs" CssClass="benthics-samples-formview-dropdowns"   
                                                        SelectedValue='<%# Bind("GearConfigID") %>'                                          
                                                        AppendDataBoundItems="true" DataTextField="Description" DataValueField="ID">
                                    </asp:DropDownList>
                                    <br />
                                    <div class="benthics-samples-form-view-labels">
                                        <label>Activity Type:</label>
                                    </div>
                                    <asp:DropDownList ID="dropDownActivityTypes" runat="server" DataMember="it"
                                                        SelectMethod="BindActivityTypes" CssClass="benthics-samples-formview-dropdowns"   
                                                        SelectedValue='<%# Bind("ActivityTypeID") %>'                                          
                                                        AppendDataBoundItems="true" DataTextField="Description" DataValueField="ID">
                                    </asp:DropDownList> 
                                    <br />
                                    <div class="benthics-samples-form-view-labels">
                                        <label>Medium:</label>
                                    </div>
                                    <asp:DropDownList ID="dropDownMediums" runat="server" DataMember="it"
                                                        SelectMethod="BindMediums" CssClass="benthics-samples-formview-dropdowns"   
                                                        SelectedValue='<%# Bind("Medium") %>'                                          
                                                        AppendDataBoundItems="true" DataTextField="Description" DataValueField="ID">
                                    </asp:DropDownList> 
                                    <br />
                                    <div class="benthics-samples-form-view-labels">
                                        <label>Intent:</label>
                                    </div>
                                    <asp:DropDownList ID="dropDownIntents" runat="server" DataMember="it"
                                                        SelectMethod="BindIntents" CssClass="benthics-samples-formview-dropdowns"   
                                                        SelectedValue='<%# Bind("Intent") %>'                                          
                                                        AppendDataBoundItems="true" DataTextField="Description" DataValueField="ID">
                                    </asp:DropDownList>
                                    <br />
                                    <div class="benthics-samples-form-view-labels">
                                        <label>Communities:</label>
                                    </div>
                                    <asp:DropDownList ID="dropDownCommunities" runat="server" DataMember="it"
                                                        SelectMethod="BindCommunities" CssClass="benthics-samples-formview-dropdowns"   
                                                        SelectedValue='<%# Bind("Community") %>'                                          
                                                        AppendDataBoundItems="true" DataTextField="Description" DataValueField="ID">
                                    </asp:DropDownList>
                                    <br />
                                    <div class="benthics-samples-form-view-labels">
                                        <label>Bio Results Type:</label>
                                    </div>
                                    <asp:DropDownList ID="dropDownBioResultsTypes" runat="server" DataMember="it"
                                                        SelectMethod="BindBioResultsTypes" CssClass="benthics-samples-formview-dropdowns"   
                                                        SelectedValue='<%# Bind("BioResultGroupID") %>'                                          
                                                        AppendDataBoundItems="true" DataTextField="Description" DataValueField="ID">
                                    </asp:DropDownList> 
                                    <br />
                                    <div class="benthics-samples-form-view-labels">
                                        <label>Comments:</label>
                                    </div>
                                    <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Text='<%# Bind("Comments") %>' MaxLength="100" 
                                                 CssClass="benthics-samples-form-view-textbox"></asp:TextBox>          
                                    <br /><br />
                                    <asp:Button ID="CancelButton" runat="server" Text="Cancel" CommandName="Cancel" CssClass="adminButton" CommandArgument='<%# Eval("ID") %>' />                                                 
                                    <asp:Button ID="UpdateButton" runat="server" Text="Save Benthics Data" CommandName="Update" CssClass="adminButton" />                                                               
                                
                                    <asp:HiddenField id="HiddenField_CollDate" runat="server"  value='<%# Bind("CollDate") %>'/>
                                    <asp:HiddenField id="HiddenField_CollTime" runat="server"  value='<%# Bind("CollTime") %>'/> 
                                    <asp:HiddenField id="HiddenField_SampleID" runat="server" value='<%# Bind("SampleID") %>' />
                                    <asp:HiddenField id="HiddenField_DateLastModified" runat="server" value='<%# Bind("DateLastModified") %>' />
                                    <asp:HiddenField id="HiddenField_UserLastModified" runat="server" value='<%# Bind("UserLastModified") %>' />
                                </EditItemTemplate>
                            </asp:FormView>
                        </asp:Panel> 
                    </ContentTemplate>
                </asp:UpdatePanel>
                <br />
            <!-- ************************* Benthics Data Panel - End *******************************-->        
            </div>
            <div class="benthic-reps-gridview">
            <!-- ************************* Benthics Reps Panel - Start *******************************-->
                <asp:UpdatePanel ID="BenthicsReps_UpdatePanel" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="BenthicsRepsGridView_Panel" runat="server" Visible="false">
                            <h4>Benthics Reps</h4>
                            <div class="benthics-data-label-placement">
                                <asp:Label ID="BenthicsRepErrorLabel" CssClass="benthics-data-label-error" runat="server" />               
                            </div>
                            <div class="benthics-data-label-placement">
                                 <asp:Label ID="BenthicsRepSuccessLabel" CssClass="benthics-data-label-success" runat="server" />
                            </div>
                            <asp:GridView ID="BenthicsRepsGridView" runat="server"
                                DataKeyNames="ID"
                                ItemType="RWInbound2.tblBenRep" 
                                SelectMethod="GetBenthicsReps"
                                UpdateMethod="UpdateBenthicRep"
                                DeleteMethod="DeleteBenthicRep" 
                                InsertItemPosition="LastItem"  
                                OnRowEditing="BenthicsRepsGridView_RowEditing"                
                                ShowFooter="true"
                                CellPadding="4"
                                AutoGenerateColumns="False" CssClass="benthic-samples-grid-columns-center"
                                HeaderStyle-CssClass="grid-edit-benthics-samples-header" 
                                ForeColor="#333333" 
                                AllowPaging="true" Pagesize="15">
                                <AlternatingRowStyle BackColor="White" />
                                <EmptyDataTemplate>
                                    <%--<div class="grid-edit-benthics-samples-empty-data-div">
                                        No Benthics Reps were found.                                 
                                    </div>--%>
                                    <table class="benthic-samples-grid-columns-center" cellspacing="0" cellpadding="4"  
                                           border="1" id="BenthicsRepsGridView" style="color:#333333;border-collapse:collapse;">
                                        <tbody>
                                            <tr class="grid-edit-benthics-samples-header">
                                                <th scope="col">&nbsp;</th>                                    
                                                <th scope="col">Rep</th>
                                                <th scope="col">Activity</th>
                                                <th scope="col">Count</th>
                                                <th scope="col">Type</th>
                                                <th scope="col">Comments</th>
                                            </tr>                              
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnAdd" runat="server" Text="Add New"
                                                             OnClick="AddNewBenthicRep" />
                                                </td>                                    
                                                <td>
                                                    <asp:TextBox ID="txtNewRepNum" runat="server"
                                                         CssClass="benthics-samples-form-view-width-number-textbox" TextMode="Number"></asp:TextBox>
                                                    <asp:Label ID="lblNewRepNumRequired" runat="server" Visible="false" CssClass="grid-benthics-required">
                                                        Required!
                                                    </asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="dropDownNewBenthicsRepActivity" runat="server" DataMember="it"
                                                                SelectMethod="BindActivityCategories" CssClass="benthics-reps-gridview-dropdowns"                                                                                               
                                                                AppendDataBoundItems="true" DataTextField="Description" DataValueField="ID">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNewGrids" runat="server"
                                                         CssClass="benthics-samples-form-view-width-number-textbox" TextMode="Number"></asp:TextBox>  
                                                </td>
                                                <td>                                                
                                                    <asp:TextBox ID="txtNewType" runat="server" MaxLength="25" CssClass="grid-benthics-reps-medium-textbox"></asp:TextBox>  
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNewComments" runat="server" TextMode="MultiLine" MaxLength="100" CssClass="grid-benthics-reps-medium-textbox" ></asp:TextBox>   
                                                </td>                                            
                                            </tr>
                                        </tbody>
                                    </table>
                                </EmptyDataTemplate>    
                                <Columns>  
                                    <asp:TemplateField>
                                        <ItemTemplate>                                        
                                            <asp:Button ID="EditButton" runat="server" Text="Edit" CommandName="Edit" />                    
                                            <asp:Button ID="DeleteButton" runat="server" Text="Delete" CommandName="Delete" 
                                                        OnClientClick="return confirm('Are you certain you want to delete this?');"/>                                     
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Button ID="CancelButton" runat="server" Text="Cancel" CommandName="Cancel" />
                                            <asp:Button ID="UpdateButton" runat="server" Text="Update" CommandName="Update" />
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:Button ID="btnAdd" runat="server" Text="Add"
                                                        OnClick="AddNewBenthicRep" />                                      
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ID" HeaderText="ID" Visible="false" ReadOnly="True" SortExpression="ID"  />
                                    <asp:TemplateField HeaderText="Rep" SortExpression="RepNum">                   
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtRepNum" runat="server" Text='<%# Bind("RepNum") %>' 
                                                         CssClass="benthics-samples-form-view-width-number-textbox" TextMode="Number"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblRepNum" runat="server" Text='<%# Bind("RepNum") %>'></asp:Label>                                                                   
                                        </ItemTemplate> 
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewRepNum" runat="server"
                                                         CssClass="benthics-samples-form-view-width-number-textbox" TextMode="Number"></asp:TextBox>
                                            <asp:Label ID="lblNewRepNumRequired" runat="server" Visible="false" CssClass="grid-benthics-required">
                                                Required!
                                            </asp:Label>
                                        </FooterTemplate>                                              
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Activity" SortExpression="tlkActivityCategory.Description">                   
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="dropDownBenthicsRepActivity" runat="server" DataMember="it"
                                                                SelectMethod="BindActivityCategories" CssClass="benthics-reps-gridview-dropdowns"   
                                                                SelectedValue='<%# Bind("ActivityCategory") %>'                                          
                                                                AppendDataBoundItems="true" DataTextField="Description" DataValueField="ID">
                                            </asp:DropDownList> 
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblBenthicsReptlkActivityCategoryDescription" runat="server" Text='<%# Bind("tlkActivityCategory.Description") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="dropDownNewBenthicsRepActivity" runat="server" DataMember="it"
                                                                SelectMethod="BindActivityCategories" CssClass="benthics-reps-gridview-dropdowns"                                                                                               
                                                                AppendDataBoundItems="true" DataTextField="Description" DataValueField="ID">
                                            </asp:DropDownList> 
                                        </FooterTemplate>                    
                                   </asp:TemplateField> 
                                   <asp:TemplateField HeaderText="Count" SortExpression="Grids">                   
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtGrids" runat="server" Text='<%# Bind("Grids") %>' 
                                                         CssClass="benthics-samples-form-view-width-number-textbox" TextMode="Number"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblGrids" runat="server" Text='<%# Bind("Grids") %>'></asp:Label>                                                                   
                                        </ItemTemplate> 
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewGrids" runat="server"
                                                         CssClass="benthics-samples-form-view-width-number-textbox" TextMode="Number"></asp:TextBox>
                                        </FooterTemplate>                                              
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type" SortExpression="Type" ItemStyle-VerticalAlign="Middle" ItemStyle-CssClass="grid-benthics-reps-medium-textbox" 
                                        ControlStyle-CssClass="grid-benthics-reps-medium-textbox" >
                                        <EditItemTemplate >
                                            <asp:TextBox ID="txtType" runat="server" TextMode="MultiLine" MaxLength="100" Text='<%# Bind("Type") %>'></asp:TextBox>                                                          
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                           <asp:Label ID="lblType" runat="server" Text='<%# Bind("Type") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewType" runat="server" MaxLength="25" CssClass="grid-benthics-reps-medium-textbox"></asp:TextBox>   
                                        </FooterTemplate>                            
                                    </asp:TemplateField>  
                                    <asp:TemplateField HeaderText="Comments" SortExpression="Comments" ItemStyle-VerticalAlign="Middle" ItemStyle-CssClass="grid-benthics-reps-medium-textbox" 
                                        ControlStyle-CssClass="grid-benthics-reps-medium-textbox" >
                                        <EditItemTemplate >
                                            <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" MaxLength="100" Text='<%# Bind("Comments") %>'></asp:TextBox>                                                          
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                           <asp:Label ID="lblComments" runat="server" Text='<%# Bind("Comments") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewComments" runat="server" TextMode="MultiLine" MaxLength="100" CssClass="grid-benthics-reps-medium-textbox"></asp:TextBox>   
                                        </FooterTemplate>                            
                                    </asp:TemplateField>  
                                    <asp:TemplateField HeaderText="Entered" SortExpression="EnterDate">                   
                                        <EditItemTemplate>
                                             <asp:Label ID="txtEnterDate" runat="server" Text='<%# Bind("EnterDate") %>'></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblEnterDate" runat="server" Text='<%# Bind("EnterDate") %>'></asp:Label>
                                        </ItemTemplate>                    
                                    </asp:TemplateField>                                                  
                                </Columns>
                                <EditRowStyle BackColor="antiquewhite"  />            
                            </asp:GridView> 
                            </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            <!-- ************************* Benthics Reps Panel - End *******************************-->
                <div class="benthic-grids-gridview">
                <!-- ************************* Benthics Grids Panel - Start *******************************-->       
                    <asp:UpdatePanel ID="BenthicsGrids_UpdatePanel" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Panel ID="BenthicsGridsGridView_Panel" runat="server" Visible="false">
                                <h4>Benthics Grids</h4>
                                <div class="benthics-data-label-placement">
                                    <asp:Label ID="BenthicsGridErrorLabel" CssClass="benthics-data-label-error" runat="server" />               
                                </div>
                                <div class="benthics-data-label-placement">
                                        <asp:Label ID="BenthicsGridSuccessLabel" CssClass="benthics-data-label-success" runat="server" />
                                </div>
                                <asp:GridView ID="BenthicsGridsGridView" runat="server"
                                    DataKeyNames="ID"
                                    ItemType="RWInbound2.tblBenGrid" 
                                    SelectMethod="GetBenthicsGrids"
                                    UpdateMethod="UpdateBenthicGrid"
                                    DeleteMethod="DeleteBenthicGrid" 
                                    InsertItemPosition="LastItem"  
                                    OnRowEditing="BenthicsGridsGridView_RowEditing"                
                                    ShowFooter="true"
                                    CellPadding="4"
                                    AutoGenerateColumns="False" CssClass="benthic-samples-grid-columns-center"
                                    HeaderStyle-CssClass="grid-edit-benthics-samples-header" 
                                    ForeColor="#333333" 
                                    AllowPaging="true" Pagesize="45">
                                    <AlternatingRowStyle BackColor="White" />
                                    <EmptyDataTemplate>
                                        <%--<div class="grid-edit-benthics-samples-empty-data-div">
                                            No Benthics Grids were found.                                
                                        </div>--%>
                                        <table class="benthic-samples-grid-columns-center" cellspacing="0" cellpadding="4"  
                                                border="1" id="BenthicsGridsGridView" style="color:#333333;border-collapse:collapse;">
                                            <tbody>
                                                <tr class="grid-edit-benthics-samples-header">
                                                    <th scope="col">&nbsp;</th>                                    
                                                    <th scope="col">Rep</th>
                                                    <th scope="col">Grid Num</th>
                                                    <th scope="col">Count</th>
                                                </tr>                              
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="btnAdd" runat="server" Text="Add New"
                                                                    OnClick="AddNewBenthicGrid" />
                                                    </td>                                    
                                                    <td>
                                                        <asp:DropDownList ID="dropDownNewBenthicsGridReps" runat="server" DataMember="it"
                                                                    SelectMethod="BindGridReps" CssClass="benthics-reps-gridview-dropdowns"                                                                                               
                                                                    AppendDataBoundItems="true"  DataTextField="RepNum" DataValueField="RepNum">
                                                        </asp:DropDownList>                                                 
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtNewGridNum" runat="server"
                                                                CssClass="benthics-samples-form-view-width-number-textbox" TextMode="Number"></asp:TextBox>
                                                        <asp:Label ID="lblNewGridNumRequired" runat="server" Visible="false" CssClass="grid-benthics-required">
                                                            Required!
                                                        </asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtNewBenCount" runat="server"
                                                                CssClass="benthics-samples-form-view-width-number-textbox" TextMode="Number"></asp:TextBox>
                                                    </td>                                                                                   
                                                </tr>
                                            </tbody>
                                        </table>
                                    </EmptyDataTemplate>    
                                    <Columns>  
                                        <asp:TemplateField>
                                            <ItemTemplate>                                         
                                                <asp:Button ID="EditButton" runat="server" Text="Edit" CommandName="Edit" />    
                                                <asp:Button ID="DeleteButton" runat="server" Text="Delete" CommandName="Delete" 
                                                            OnClientClick="return confirm('Are you certain you want to delete this?');"/>      
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Button ID="CancelButton" runat="server" Text="Cancel" CommandName="Cancel" />
                                                <asp:Button ID="UpdateButton" runat="server" Text="Update" CommandName="Update" />
                                            </EditItemTemplate>
                                            <FooterTemplate>                                   
                                                <asp:Button ID="btnAdd" runat="server" Text="Add"
                                                            OnClick="AddNewBenthicGrid" />   
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ID" HeaderText="ID" Visible="false" ReadOnly="True" SortExpression="ID"  />
                                        <asp:TemplateField HeaderText="Rep" SortExpression="RepNum">                   
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="dropDownBenthicsGridReps" runat="server" DataMember="it"
                                                                    SelectMethod="BindGridReps" CssClass="benthics-reps-gridview-dropdowns"   
                                                                    SelectedValue='<%# Bind("RepNum") %>'                                          
                                                                    AppendDataBoundItems="true" DataTextField="RepNum" DataValueField="RepNum">
                                                </asp:DropDownList> 
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblRepNum" runat="server" Text='<%# Bind("RepNum") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="dropDownNewBenthicsGridReps" runat="server" DataMember="it"
                                                                    SelectMethod="BindGridReps" CssClass="benthics-reps-gridview-dropdowns"                                                                                               
                                                                    AppendDataBoundItems="true"  DataTextField="RepNum" DataValueField="RepNum">
                                                </asp:DropDownList> 
                                            </FooterTemplate>                    
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Grid Num" SortExpression="GridNum">                   
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtGridNum" runat="server" Text='<%# Bind("GridNum") %>' 
                                                                CssClass="benthics-samples-form-view-width-number-textbox" TextMode="Number"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblGridNum" runat="server" Text='<%# Bind("GridNum") %>'></asp:Label>                                                                   
                                            </ItemTemplate> 
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtNewGridNum" runat="server"
                                                                CssClass="benthics-samples-form-view-width-number-textbox" TextMode="Number"></asp:TextBox>
                                                <asp:Label ID="lblNewGridNumRequired" runat="server" Visible="false" CssClass="grid-benthics-required">
                                                    Required!
                                                </asp:Label>
                                            </FooterTemplate>                                              
                                        </asp:TemplateField>                                
                                        <asp:TemplateField HeaderText="Count" SortExpression="BenCount">                   
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtBenCount" runat="server" Text='<%# Bind("BenCount") %>' 
                                                                CssClass="benthics-samples-form-view-width-number-textbox" TextMode="Number"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblBenCount" runat="server" Text='<%# Bind("BenCount") %>'></asp:Label>                                                                   
                                            </ItemTemplate> 
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtNewBenCount" runat="server"
                                                                CssClass="benthics-samples-form-view-width-number-textbox" TextMode="Number"></asp:TextBox>
                                            </FooterTemplate>                                              
                                        </asp:TemplateField>                                                                             
                                    </Columns>
                                    <EditRowStyle BackColor="antiquewhite"  />            
                                </asp:GridView> 
                                </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                <!-- ************************* Benthics Grids Panel - End *******************************--> 
                </div>
            </div>        
            <br />
        <div class="benthic-benthics-taxa-gridview">
        <!-- ************************* Ben Taxa (Benthics) Panel - Start *******************************-->
            <asp:UpdatePanel ID="Benthics_UpdatePanel" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="BenthicsGridView_Panel" runat="server" Visible="false">
                        <h4>Ben Taxa</h4>
                        <div class="benthics-data-label-placement">
                            <asp:Label ID="BenthicsErrorLabel" CssClass="benthics-data-label-error" runat="server" />               
                        </div>
                        <div class="benthics-data-label-placement">
                             <asp:Label ID="BenthicsSuccessLabel" CssClass="benthics-data-label-success" runat="server" />
                        </div> 
                        <div class="grid-benthics-options-div">
                            <asp:Label runat="server" Text="Show Rep:" />
                            <asp:DropDownList ID="dropDownBenthicsSelectedRepNum" runat="server" DataMember="it"
                                                SelectMethod="BindGridReps" CssClass="benthics-reps-gridview-dropdowns"   
                                                AutoPostBack="true" AppendDataBoundItems="false" 
                                                DataTextField="RepNum" DataValueField="RepNum" >
                            </asp:DropDownList>
                            <asp:Button ID="ShowAllBenTaxaButton" runat="server" OnClick="ShowAllBenTaxaButton_Click" 
                                        Text="Show All For Rep" CssClass="adminButton"/>
                            <div class="edit-benthics-sample-number">
                                <b>Event:</b>
                                <asp:Label ID="lblBenthicsSampleEvent" runat="server"></asp:Label>
                            </div> 
                        </div>                       
                        <asp:GridView ID="BenthicsGridView" runat="server"
                            DataKeyNames="ID"
                            ItemType="RWInbound2.tblBenthic" 
                            SelectMethod="GetBenthics"
                            UpdateMethod="UpdateBenthic"
                            DeleteMethod="DeleteBenthic" 
                            InsertItemPosition="LastItem"  
                            OnRowEditing="BenthicsGridView_RowEditing"  
                            OnRowDataBound="BenthicsGridView_RowDataBound"                      
                            ShowFooter="true"
                            CellPadding="4"
                            AutoGenerateColumns="False" CssClass="benthic-samples-grid-columns-center"
                            HeaderStyle-CssClass="grid-edit-benthics-samples-header" 
                            ForeColor="#333333" AllowSorting="true"   
                            AllowPaging="true" Pagesize="10">
                            <AlternatingRowStyle BackColor="White" />
                            <EmptyDataTemplate>   
                                <%--<div class="grid-edit-benthics-samples-empty-data-div">
                                    No Ben Taxa Records were found.                                 
                                </div>--%>
                                <table class="benthic-samples-grid-columns-center" cellspacing="0" cellpadding="4"  
                                       border="1" id="BenthicsGridView" style="color:#333333;border-collapse:collapse;">
                                    <tbody>
                                        <tr class="grid-edit-equipment-add-new-header">
                                            <th scope="col">&nbsp;</th>                                    
                                            <th scope="col">Taxa</th>
                                            <th scope="col">Count</th>
                                            <th scope="col">Num In 100%</th>
                                            <th scope="col">Comments</th>
                                            <th scope="col">Rep</th>
                                        </tr>                              
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnAdd" runat="server" Text="Add New"
                                                         OnClick="AddNewBenthic" />
                                            </td>                                    
                                            <td>
                                                <asp:DropDownList ID="dropDownNewBenthicsTaxa" runat="server" DataMember="it"
                                                            SelectMethod="BindBenTaxa" CssClass="benthics-reps-gridview-dropdowns"                                                                                               
                                                            DataTextField="FinalID" DataValueField="ID">
                                                </asp:DropDownList> 
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNewIndividuals" runat="server"
                                                     CssClass="benthics-samples-form-view-width-number-textbox" TextMode="Number"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNewNumInHundredPct" runat="server"
                                                     CssClass="benthics-samples-form-view-width-number-textbox" TextMode="Number"></asp:TextBox>
                                                <asp:RangeValidator ID="RangeValidator_txtNewNumInHundredPct" runat="server" ControlToValidate="txtNewNumInHundredPct"
                                                        MinimumValue="0" MaximumValue="100" Type="Integer" ErrorMessage="0 to 100" CssClass="grid-benthics-range-error"
                                                        Display="Dynamic"/>  
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNewComments" runat="server" TextMode="MultiLine" MaxLength="100" CssClass="grid-benthics-reps-medium-textbox" ></asp:TextBox>   
                                            </td>                                            
                                        </tr>
                                    </tbody>
                                </table>
                            </EmptyDataTemplate>    
                            <Columns>  
                                <asp:TemplateField>
                                    <ItemTemplate>                                        
                                        <asp:Button ID="EditButton" runat="server" Text="Edit" CommandName="Edit" />                    
                                        <asp:Button ID="DeleteButton" runat="server" Text="Delete" CommandName="Delete" 
                                                    OnClientClick="return confirm('Are you certain you want to delete this?');"/>                                     
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Button ID="CancelButton" runat="server" Text="Cancel" CommandName="Cancel" />
                                        <asp:Button ID="UpdateButton" runat="server" Text="Update" CommandName="Update" />
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:Button ID="btnAdd" runat="server" Text="Add"
                                                    OnClick="AddNewBenthic" />                                      
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ID" HeaderText="ID" Visible="false" ReadOnly="True" SortExpression="ID"  />                                
                                <asp:TemplateField HeaderText="Taxa" SortExpression="SortByBenTaxaFinalID" >                   
                                    <EditItemTemplate>
                                        <asp:Label ID="lblEdittblBenTaxaFinalID" runat="server" Text='<%# Bind("tblBenTaxa.FinalID") %>'></asp:Label>
                                        <asp:HiddenField id="HiddenField_Edit_BenTaxaID" runat="server" value='<%# Bind("BenTaxaID") %>' />
                                        <%--<asp:DropDownList ID="dropDownBenthicsTaxa" runat="server" DataMember="it"
                                                            SelectMethod="BindBenTaxa" CssClass="benthics-reps-gridview-dropdowns"   
                                                            SelectedValue='<%# Bind("BenTaxaID") %>'                                          
                                                            DataTextField="FinalID" DataValueField="ID">
                                        </asp:DropDownList> --%>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbltblBenTaxaFinalID" runat="server" Text='<%# Bind("tblBenTaxa.FinalID") %>'></asp:Label>
                                        <asp:HiddenField id="HiddenField_BenTaxaID" runat="server" value='<%# Bind("BenTaxaID") %>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="dropDownNewBenthicsTaxa" runat="server" DataMember="it"
                                                            SelectMethod="BindBenTaxa" CssClass="benthics-reps-gridview-dropdowns"                                                                                               
                                                            DataTextField="FinalID" DataValueField="ID">
                                        </asp:DropDownList> 
                                    </FooterTemplate>                    
                               </asp:TemplateField>
                                <asp:TemplateField HeaderText="Count">                   
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtIndividuals" runat="server" Text='<%# Bind("Individuals") %>' 
                                                     CssClass="benthics-samples-form-view-width-number-textbox" TextMode="Number"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblIndividuals" runat="server" Text='<%# Bind("Individuals") %>'></asp:Label>                                                                   
                                    </ItemTemplate> 
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtNewIndividuals" runat="server"
                                                     CssClass="benthics-samples-form-view-width-number-textbox" TextMode="Number"></asp:TextBox>                                       
                                    </FooterTemplate>                                              
                                </asp:TemplateField> 
                               <asp:TemplateField HeaderText="Num In 100%">                   
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtNumInHundredPct" runat="server" Text='<%# Bind("NumInHundredPct") %>' 
                                                     CssClass="benthics-samples-form-view-width-number-textbox" TextMode="Number"></asp:TextBox>
                                        <asp:RangeValidator ID="RangeValidator_txtNumInHundredPct" runat="server" ControlToValidate="txtNumInHundredPct"
                                             MinimumValue="0" MaximumValue="100" Type="Integer" ErrorMessage="0 to 100" CssClass="grid-benthics-range-error"
                                             Display="Dynamic" />                                        
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblNumInHundredPct" runat="server" Text='<%# Bind("NumInHundredPct") %>'></asp:Label>                                                                   
                                    </ItemTemplate> 
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtNewNumInHundredPct" runat="server"
                                                     CssClass="benthics-samples-form-view-width-number-textbox" TextMode="Number"></asp:TextBox>
                                        <asp:RangeValidator ID="RangeValidator_txtNewNumInHundredPct" runat="server" ControlToValidate="txtNewNumInHundredPct"
                                             MinimumValue="0" MaximumValue="100" Type="Integer" ErrorMessage="0 to 100" CssClass="grid-benthics-range-error"
                                             Display="Dynamic"/>   
                                    </FooterTemplate>                                              
                                </asp:TemplateField>                                
                                <asp:TemplateField HeaderText="Comments" ItemStyle-VerticalAlign="Middle" ItemStyle-CssClass="grid-benthics-reps-medium-textbox" 
                                    ControlStyle-CssClass="grid-benthics-reps-medium-textbox" >
                                    <EditItemTemplate >
                                        <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" MaxLength="100" Text='<%# Bind("Comments") %>'></asp:TextBox>                                                          
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                       <asp:Label ID="lblComments" runat="server" Text='<%# Bind("Comments") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtNewComments" runat="server" TextMode="MultiLine" MaxLength="100" CssClass="grid-benthics-reps-medium-textbox" ></asp:TextBox>   
                                    </FooterTemplate>                            
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Life Cycle" >
                                    <EditItemTemplate >
                                        <asp:Label ID="lblEditLifeCycle" runat="server" Text='<%# Bind("tblBenTaxa.LifeCycle") %>'></asp:Label>                                                        
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                       <asp:Label ID="lblLifeCycle" runat="server" Text='<%# Bind("tblBenTaxa.LifeCycle") %>'></asp:Label>
                                    </ItemTemplate>                                                           
                                </asp:TemplateField>    
                                <asp:TemplateField HeaderText="Entered" SortExpression="EnterDate">                   
                                    <EditItemTemplate>
                                         <asp:Label ID="txtEnterDate" runat="server" Text='<%# Bind("EnterDate") %>'></asp:Label>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblEnterDate" runat="server" Text='<%# Bind("EnterDate") %>'></asp:Label>
                                    </ItemTemplate>                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rep">                   
                                    <EditItemTemplate>
                                        <asp:Label ID="lblEditRepNum" runat="server" Text='<%# Bind("RepNum") %>'></asp:Label>                                                                           
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblRepNum" runat="server" Text='<%# Bind("RepNum") %>'></asp:Label>                                                                   
                                    </ItemTemplate>                                                                          
                                </asp:TemplateField>                                                  
                            </Columns>
                            <EditRowStyle BackColor="antiquewhite"  />            
                        </asp:GridView> 
                        </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>  
        <!-- ************************* Ben Taxa (Benthics) Panel - End *******************************-->   
        </div>         
        </div>
    </section>
</asp:Content>
