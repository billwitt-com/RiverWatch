<%@ Page Title="Edit Benthic Samples" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditBenthicSamples.aspx.cs" Inherits="RWInbound2.Admin.EditBenthicSamples" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server" ClientIDMode="Static">
    <section spellcheck="true" class="benthics-samples-section">
        <asp:UpdatePanel ID="BenthicSamplesGridView_UpdatePanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
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
                <br />
                <asp:Panel ID="SampleData_Panel" runat="server">
                    <div>
                        <b>Sample Number:</b>
                        <asp:Label ID="lblSampleNumber" runat="server"></asp:Label>
                        <div class="edit-benthics-sample-number">
                            <b>Event:</b>
                            <asp:Label ID="lblSampleEvent" runat="server"></asp:Label>
                        </div>   
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
                    AllowPaging="true" Pagesize="15">
                    <AlternatingRowStyle BackColor="White" />
                    <EmptyDataTemplate>
                        <div class="grid-edit-benthics-samples-empty-data-div">
                            No Samples were found.
                             <%-- <% =Show() %>--%>
                            <asp:Panel ID="NoResultsPanel" runat="server" Visible="false" OnInit="Show" OnDataBinding="Show" OnLoad="Show"
                                 CssClass="grid-edit-benthics-samples-no-results-panel" >                              
                                <asp:Button ID="btnAdd" runat="server" Text="Add New Benthic Sample"
                                             OnClientClick="return alert('Coming Soon!');" CssClass="adminButton grid-edit-benthics-samples-add-new-button" />                    
                            </asp:Panel>
                        </div>
                    </EmptyDataTemplate>    
                    <Columns>  
                        <asp:TemplateField>
                            <ItemTemplate>         
                                <asp:Button ID="DeleteButton" runat="server" Text="Delete" OnClientClick="return alert('Coming Soon!');" />
                            </ItemTemplate>
                            <EditItemTemplate>                             
                            </EditItemTemplate>
                            <FooterTemplate>                               
                                <asp:Button ID="btnAdd" runat="server" Text="Add"
                                             OnClientClick="return alert('Coming Soon!');" />
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
                    <EditRowStyle BackColor="#2461BF" />            
                </asp:GridView> 
            </ContentTemplate>           
        </asp:UpdatePanel>
         <%--Benthics Data--%>
        <div class="benthic-data-formview">
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
                                UpdateMethod="UpdateSelectedBenthicData"
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
                               <asp:Button ID="UpdateButton" runat="server" Text="Update Benthics Data" CommandName="Update" CssClass="adminButton" />
                                
                                <asp:HiddenField id="HiddenField_SampleID" runat="server" value='<%# Bind("SampleID") %>' />
                                <asp:HiddenField id="HiddenField_Valid" runat="server" value='<%# Bind("Valid") %>' />
                                <asp:HiddenField id="HiddenField_DateLastModified" runat="server" value='<%# Bind("DateLastModified") %>' />
                                <asp:HiddenField id="HiddenField_UserLastModified" runat="server" value='<%# Bind("UserLastModified") %>' />
                            </EditItemTemplate>
                        </asp:FormView>
                    </asp:Panel> 
                </ContentTemplate>
            </asp:UpdatePanel>     
        </div>
        <div class="benthic-reps-gridview">
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
                            AutoGenerateColumns="False" CssClass="benthic-reps-grid-columns-center"
                            HeaderStyle-CssClass="grid-edit-benthics-samples-header" 
                            ForeColor="#333333" 
                            AllowPaging="true" Pagesize="15">
                            <AlternatingRowStyle BackColor="White" />
                            <EmptyDataTemplate>
                                <div class="grid-edit-benthics-samples-empty-data-div">
                                    No Benthics Reps were found.                                 
                                </div>
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
                                        <asp:Label ID="lblNewRepNumRequired" runat="server" Visible="false" CssClass="grid-benthics-reps-required">
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
                            <EditRowStyle BackColor="#2461BF"  />            
                        </asp:GridView> 
                        </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />       
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
                            AutoGenerateColumns="False" CssClass="benthic-reps-grid-columns-center"
                            HeaderStyle-CssClass="grid-edit-benthics-samples-header" 
                            ForeColor="#333333" 
                            AllowPaging="true" Pagesize="45">
                            <AlternatingRowStyle BackColor="White" />
                            <EmptyDataTemplate>
                                <div class="grid-edit-benthics-samples-empty-data-div">
                                    No Benthics Grids were found.                                
                                </div>
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
                                        <asp:Label ID="lblNewGridNumRequired" runat="server" Visible="false" CssClass="grid-benthics-reps-required">
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
                            <EditRowStyle BackColor="#2461BF"  />            
                        </asp:GridView> 
                        </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>         
    </section>
</asp:Content>
