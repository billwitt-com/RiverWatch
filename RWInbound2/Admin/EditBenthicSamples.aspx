<%@ Page Title="Edit Benthic Samples" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditBenthicSamples.aspx.cs" Inherits="RWInbound2.Admin.EditBenthicSamples" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server" ClientIDMode="Static">
    <section spellcheck="true">
        <asp:UpdatePanel ID="BenthicSamplesGridView_UpdatePanel" runat="server">
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
                                <%--<asp:Button ID="btnAdd" runat="server" Text="Add New Benthic Sample"
                                            OnClick="AddNewBenthicSample" CssClass="adminButton grid-edit-benthics-samples-add-new-button" />  --%>
                                <asp:Button ID="btnAdd" runat="server" Text="Add New Benthic Sample"
                                             OnClientClick="return alert('Coming Soon!');" CssClass="adminButton grid-edit-benthics-samples-add-new-button" />                    
                            </asp:Panel>
                        </div>
                    </EmptyDataTemplate>    
                    <Columns>  
                        <asp:TemplateField>
                            <ItemTemplate>                       
                                <%--<asp:Button ID="DeleteButton" runat="server" Text="Delete" CommandName="Delete" 
                                            OnClientClick="return confirm('Are you certain you want to delete this?');"/>--%>
                                <asp:Button ID="DeleteButton" runat="server" Text="Delete" OnClientClick="return alert('Coming Soon!');" />
                            </ItemTemplate>
                            <EditItemTemplate>
                               <%-- <asp:Button ID="CancelButton" runat="server" Text="Cancel" CommandName="Cancel" />
                                <asp:Button ID="UpdateButton" runat="server" Text="Update" CommandName="Update" />--%>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <%--<asp:Button ID="btnAdd" runat="server" Text="Add"
                                            OnClick="AddNewBenthicSample" />--%>
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
                        <asp:TemplateField HeaderText="Intent" SortExpression="tlkBioResultsType.Description">                   
                            <ItemTemplate>
                                <asp:Label ID="lbltlkBioResultsTypeDescription" runat="server" Text='<%# Bind("tlkBioResultsType.Description") %>'></asp:Label>
                            </ItemTemplate>                    
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reps/Grids">                   
                            <ItemTemplate>
                                <asp:Button ID="ViewRepsGridButton" runat="server" Text="View"  
                                            CommandName="GetAllBenthicsData" CommandArgument='<%# ((GridViewRow) Container).RowIndex+","+ Eval("ID") %>'/>
                                <%--<asp:Button ID="GetAssignedSamplesButton" runat="server" Text="Download Assigned Samples"
                                            CommandName="GetAssignedSamples" CommandArgument='<%# Bind("ID") %>' />
                                    OnClientClick="return alert('Coming Soon!');"--%>
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
                                                    SelectedValue='<%# Bind("tlkActivityCategory.ID") %>'                                          
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
                                                    SelectedValue='<%# Bind("tlkFieldProcedure.ID") %>'                                          
                                                    AppendDataBoundItems="true" DataTextField="Description" DataValueField="ID">
                                </asp:DropDownList>
                                <br />
                                <div class="benthics-samples-form-view-labels">
                                    <label>Field Gear:</label>
                                </div>
                                <asp:DropDownList ID="dropDownFieldGears" runat="server" DataMember="it"
                                                    SelectMethod="BindFieldGears" CssClass="benthics-samples-formview-dropdowns"   
                                                    SelectedValue='<%# Bind("tlkFieldGear.ID") %>'                                          
                                                    AppendDataBoundItems="true" DataTextField="Description" DataValueField="ID">
                                </asp:DropDownList>
                                <br />
                                <div class="benthics-samples-form-view-labels">
                                    <label>Gear Configuration:</label>
                                </div>
                                <asp:DropDownList ID="dropDownGearConfigs" runat="server" DataMember="it"
                                                    SelectMethod="BindGearConfigs" CssClass="benthics-samples-formview-dropdowns"   
                                                    SelectedValue='<%# Bind("tlkGearConfig.ID") %>'                                          
                                                    AppendDataBoundItems="true" DataTextField="Description" DataValueField="ID">
                                </asp:DropDownList>
                                <br />
                                <div class="benthics-samples-form-view-labels">
                                    <label>Activity Type:</label>
                                </div>
                                <asp:DropDownList ID="dropDownActivityTypes" runat="server" DataMember="it"
                                                    SelectMethod="BindActivityTypes" CssClass="benthics-samples-formview-dropdowns"   
                                                    SelectedValue='<%# Bind("tlkActivityType.ID") %>'                                          
                                                    AppendDataBoundItems="true" DataTextField="Description" DataValueField="ID">
                                </asp:DropDownList> 
                                <br />
                                <div class="benthics-samples-form-view-labels">
                                    <label>Medium:</label>
                                </div>
                                <asp:DropDownList ID="dropDownMediums" runat="server" DataMember="it"
                                                    SelectMethod="BindMediums" CssClass="benthics-samples-formview-dropdowns"   
                                                    SelectedValue='<%# Bind("tlkMedium.ID") %>'                                          
                                                    AppendDataBoundItems="true" DataTextField="Description" DataValueField="ID">
                                </asp:DropDownList> 
                                <br />
                                <div class="benthics-samples-form-view-labels">
                                    <label>Intent:</label>
                                </div>
                                <asp:DropDownList ID="dropDownIntents" runat="server" DataMember="it"
                                                    SelectMethod="BindIntents" CssClass="benthics-samples-formview-dropdowns"   
                                                    SelectedValue='<%# Bind("tlkIntent.ID") %>'                                          
                                                    AppendDataBoundItems="true" DataTextField="Description" DataValueField="ID">
                                </asp:DropDownList>
                                <br />
                                <div class="benthics-samples-form-view-labels">
                                    <label>Communities:</label>
                                </div>
                                <asp:DropDownList ID="dropDownCommunities" runat="server" DataMember="it"
                                                    SelectMethod="BindCommunities" CssClass="benthics-samples-formview-dropdowns"   
                                                    SelectedValue='<%# Bind("tlkCommunity.ID") %>'                                          
                                                    AppendDataBoundItems="true" DataTextField="Description" DataValueField="ID">
                                </asp:DropDownList>
                                <br />
                                <div class="benthics-samples-form-view-labels">
                                    <label>Bio Results Type:</label>
                                </div>
                                <asp:DropDownList ID="dropDownBioResultsTypes" runat="server" DataMember="it"
                                                    SelectMethod="BindBioResultsTypes" CssClass="benthics-samples-formview-dropdowns"   
                                                    SelectedValue='<%# Bind("tlkBioResultsType.ID") %>'                                          
                                                    AppendDataBoundItems="true" DataTextField="Description" DataValueField="ID">
                                </asp:DropDownList> 
                                <br />
                                <div class="benthics-samples-form-view-labels">
                                    <label>Comments:</label>
                                </div>
                                <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Text='<%# Bind("Comments") %>' MaxLength="100" CssClass="benthics-samples-form-view-textbox"></asp:TextBox>          
                                <br /><br />
                                <asp:Button ID="CancelButton" runat="server" Text="Cancel" CommandName="Cancel" CssClass="adminButton" CommandArgument='<%# Eval("ID") %>' />                                                 
                                <%--<asp:Button ID="UpdateButton" runat="server" Text="Update Benthics Data" OnClientClick="return alert('Coming Soon!');" CssClass="adminButton" />--%>                        
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
    </section>
</asp:Content>
