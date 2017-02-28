<%@ Page Title="Edit Ben Taxa" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditBenTaxa.aspx.cs" Inherits="RWInbound2.Edit.EditBenTaxa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
        <div class="bentaxa-group-all-views">        
            <!-- ************************* Ben Taxa Main Gridview - Start *******************************-->  
            <asp:UpdatePanel ID="BenTaxaGridView_UpdatePanel" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div>
                        <hgroup class="edit-benthic-samples-headergroup">
                            <h3><%: Page.Title %></h3>
                        </hgroup>
                        <div class="bentaxa-messages-and-search">       
                            <div class="label-placement">
                                <asp:Label ID="ErrorLabel" CssClass="label-error" runat="server" />               
                            </div>
                            <div class="label-placement">
                                    <asp:Label ID="SuccessLabel" CssClass="label-success" runat="server" />
                            </div>  
                            <br />
                            <label>Search By Taxa:</label>                
                            <asp:TextBox ID="taxaSearch"  
                                onkeydown="return (event.keyCode!=13);"                   
                                runat="server"></asp:TextBox>
                            <asp:Button ID="btnSearchTaxa" runat="server" Text="Select" OnClick="btnSearchTaxa_Click" CssClass="adminButton adminButton-larger" />                
                            <ajaxToolkit:AutoCompleteExtender 
                                ID="AutoCompleteExtender" 
                                runat="server" 
                                BehaviorID="tbSearch_AutoCompleteExtender" 
                                DelimiterCharacters=""  
                                ServiceMethod="SearchForTaxa"             
                                TargetControlID="taxaSearch"
                                MinimumPrefixLength="1"
                                CompletionInterval="100" 
                                EnableCaching="false" 
                                CompletionSetCount="10">
                            </ajaxToolkit:AutoCompleteExtender>
                            <asp:Button ID="btnSearchRefresh" runat="server" Text="Reset Search" OnClick="btnSearchRefresh_Click" CssClass="adminButton adminButton-larger" />                                 
                        </div>                    
                    </div>                    
                    <br /> <br />
                <div class="ben-taxa-main-gridview-div">                  
                    <asp:GridView ID="BenTaxaGridView" runat="server"
                        DataKeyNames="ID"
                        ItemType="RWInbound2.tblBenTaxa" 
                        SelectMethod="GetBenTaxas"                       
                        DeleteMethod="DeleteBenTaxa" 
                        InsertItemPosition="LastItem"  
                        OnRowEditing="BenTaxaGridView_RowEditing"
                        OnRowCommand="BenTaxaGridView_RowCommand"                                    
                        ShowFooter="true"
                        CellPadding="4"
                        AutoGenerateColumns="False" CssClass="grid-columns-center grid-edit-ben-taxa"
                        HeaderStyle-CssClass="grid-edit-benthics-samples-header"                   
                        ForeColor="#333333" AllowSorting="true" 
                        AllowPaging="true" Pagesize="20">
                        <AlternatingRowStyle BackColor="White" />
                        <EmptyDataTemplate>                        
                            <div class="grid-edit-benthics-samples-empty-data-div">
                                No Taxa were found.                            
                            </div>
                        </EmptyDataTemplate>    
                        <Columns>  
                            <asp:TemplateField>
                                <ItemTemplate>         
                                    <asp:Button ID="DeleteButton" runat="server" Text="Delete" CommandName="Delete" 
                                                OnClientClick="return confirm('Are you certain you want to delete this?');"/>
                                </ItemTemplate>
                                <EditItemTemplate>                             
                                </EditItemTemplate>
                                <FooterTemplate>                               
                                    <asp:Button ID="btnAdd" runat="server" Text="Add"
                                                    OnClick="AddNewBenTaxa" />
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ID" HeaderText="ID" Visible="false" ReadOnly="True" SortExpression="ID" />
                            <asp:TemplateField HeaderText="Taxa" SortExpression="FinalID">                   
                                <ItemTemplate>
                                    <asp:Label ID="lblFinalID" runat="server" Text='<%# Bind("FinalID") %>'></asp:Label>                                                                          
                                </ItemTemplate>                   
                            </asp:TemplateField>                        
                            <asp:TemplateField HeaderText="Details">        
                                <ItemTemplate>
                                    <%--<asp:Button ID="Button1" runat="server" Text="View"  
                                                OnClientClick="return alert('Coming Soon!');"/>    --%>                           
                                    <%--<asp:HiddenField id="HiddenField1" runat="server" value='<%# Eval("ID") %>' />--%>
                                    <asp:Button ID="ViewTaxaFieldViewButton" runat="server" Text="View"  
                                                CommandName="GetTaxaData" CommandArgument='<%# ((GridViewRow) Container).RowIndex+","+ Eval("ID") %>'/>                               
                                    <asp:HiddenField id="HiddenField_SelectedGridRowTaxaID" runat="server" value='<%# Eval("ID") %>' />
                                </ItemTemplate>               
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Download Assigned <br> Benthic Samples">                   
                                <ItemTemplate>
                                    <%-- <asp:Button ID="GetAssignedBenthicSamplesAndRepsButton" runat="server" Text="Download"
                                                OnClientClick="return alert('Coming Soon!');" />--%>
                                    <asp:Button ID="GetAssignedBenthicSamplesAndRepsButton" runat="server" Text="Download"
                                                CommandName="GetAssignedBenthicSamples" CommandArgument='<%# Bind("ID") %>' />
                                </ItemTemplate>                   
                            </asp:TemplateField>                               
                        </Columns>
                        <EditRowStyle BackColor="antiquewhite" />            
                    </asp:GridView> 
                </div>
                </ContentTemplate>           
            </asp:UpdatePanel> 
            <!-- ************************* Ben Taxa Main Gridview - End *******************************-->  
            <div class="ben-taxa-data-formview-div">
                <!-- ************************* Ben Taxa Data Panel - Start *******************************-->
                <asp:UpdatePanel ID="BenTaxaDataFormView_UpdatePanel" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="BenTaxaDataFormView_Panel" runat="server" Visible="false">
                            <h4>Taxa Data</h4>
                            <div class="benthics-data-label-placement">
                                <asp:Label ID="BenTaxaDataErrorLabel" CssClass="benthics-data-label-error" runat="server" />               
                            </div>
                            <div class="benthics-data-label-placement">
                                    <asp:Label ID="BenTaxaDataSuccessLabel" CssClass="benthics-data-label-success" runat="server" />
                            </div>
                            <asp:FormView ID="BenTaxaDataFormView" runat="server" 
                                    DataKeyNames="ID"
                                    ItemType="RWInbound2.tblBenTaxa" 
                                    SelectMethod="GetSelectedBenTaxaData"
                                    UpdateMethod="UpdateOrAddBenTaxaData"                             
                                    OnItemCommand="BenTaxaDataFormView_ItemCommand"
                                    DefaultMode="Edit"
                                    AllowPaging="false"
                                    EmptyDataText="No results found.">                
                                <EditItemTemplate> 
                                    <div class="benthics-samples-form-view-labels">
                                        <label>Taxon:</label>
                                    </div>  
                                    <asp:TextBox ID="txtFinalID" runat="server" Text='<%# Bind("FinalID") %>' MaxLength="100" TextMode="MultiLine"
                                                    CssClass="ben-taxa-data-formview-textbox"></asp:TextBox>
                                    <asp:RequiredFieldValidator id="RequiredFieldValidator_txtFinalID" runat="server" 
                                        CssClass="grid-benthics-required" Display="Dynamic" ErrorMessage="Required!" ControlToValidate="txtFinalID">
                                    </asp:RequiredFieldValidator>                               
                                    <br />
                                    <div class="benthics-samples-form-view-labels">
                                        <label>TSN:</label>
                                    </div>
                                    <asp:TextBox ID="txtTSN" runat="server" Text='<%# Bind("TSN") %>' 
                                                    CssClass="ben-taxa-data-formview-textbox"></asp:TextBox>
                                    <br />
                                    <div class="benthics-samples-form-view-labels">
                                        <label>Par TSN:</label>
                                    </div>
                                    <asp:TextBox ID="txtParTSN" runat="server" Text='<%# Bind("ParTSN") %>' 
                                                    CssClass="ben-taxa-data-formview-textbox"></asp:TextBox> 
                                    <br />
                                    <div class="benthics-samples-form-view-labels">
                                        <label>Phylum:</label>
                                    </div>
                                    <asp:TextBox ID="txtPhylum" runat="server" Text='<%# Bind("Phylum") %>' 
                                                    CssClass="ben-taxa-data-formview-textbox"></asp:TextBox>
                                    <br />
                                    <div class="benthics-samples-form-view-labels">
                                        <label>Subphylum:</label>
                                    </div>
                                    <asp:TextBox ID="txtSubphylum" runat="server" Text='<%# Bind("Subphylum") %>' 
                                                    CssClass="ben-taxa-data-formview-textbox"></asp:TextBox>
                                    <br />
                                    <div class="benthics-samples-form-view-labels">
                                        <label>Class:</label>
                                    </div>
                                    <asp:TextBox ID="txtClass" runat="server" Text='<%# Bind("Class") %>' 
                                                    CssClass="ben-taxa-data-formview-textbox"></asp:TextBox>
                                    <br />
                                    <div class="benthics-samples-form-view-labels">
                                        <label>Border:</label>
                                    </div>
                                    <asp:TextBox ID="txtBOrder" runat="server" Text='<%# Bind("BOrder") %>' 
                                                    CssClass="ben-taxa-data-formview-textbox"></asp:TextBox>
                                    <br />
                                    <div class="benthics-samples-form-view-labels">
                                        <label>Family:</label>
                                    </div>
                                    <asp:TextBox ID="txtFamily" runat="server" Text='<%# Bind("Family") %>' 
                                                    CssClass="ben-taxa-data-formview-textbox"></asp:TextBox>
                                    <br />
                                    <div class="benthics-samples-form-view-labels">
                                        <label>Tribe:</label>
                                    </div>
                                    <asp:TextBox ID="txtTribe" runat="server" Text='<%# Bind("Tribe") %>' 
                                                    CssClass="ben-taxa-data-formview-textbox"></asp:TextBox>
                                    <br />
                                    <div class="benthics-samples-form-view-labels">
                                        <label>Genus:</label>
                                    </div>
                                    <asp:TextBox ID="txtGenus" runat="server" Text='<%# Bind("Genus") %>' 
                                                    CssClass="ben-taxa-data-formview-textbox"></asp:TextBox>
                                    <br />
                                    <div class="benthics-samples-form-view-labels">
                                        <label>Species:</label>
                                    </div>
                                    <asp:TextBox ID="txt" runat="server" Text='<%# Bind("Species") %>' 
                                                    CssClass="ben-taxa-data-formview-textbox"></asp:TextBox>
                                    <br />
                                    <div class="benthics-samples-form-view-labels">
                                        <label>Variety:</label>
                                    </div>
                                    <asp:TextBox ID="txtVariety" runat="server" Text='<%# Bind("Variety") %>' 
                                                    CssClass="ben-taxa-data-formview-textbox"></asp:TextBox>
                                    <br />
                                    <div class="benthics-samples-form-view-labels">
                                        <label>TaxaGroup:</label>
                                    </div>
                                    <asp:TextBox ID="txtTaxaGroup" runat="server" Text='<%# Bind("TaxaGroup") %>' 
                                                    CssClass="ben-taxa-data-formview-textbox"></asp:TextBox>
                                    <br />
                                    <div class="benthics-samples-form-view-labels">
                                        <label>Date Name Revised:</label>
                                    </div>
                                    <asp:TextBox ID="txtDateNameRevised" runat="server" Text='<%# Bind("DateNameRevised") %>'
                                                CssClass="ben-taxa-data-formview-textbox"></asp:TextBox> 
                                    <ajaxToolkit:CalendarExtender ID="txtDateNameRevised_CalendarExtender" runat="server" 
                                                                    BehaviorID="txtDateNameRevised_CalendarExtender" 
                                                                    TargetControlID="txtDateNameRevised"></ajaxToolkit:CalendarExtender> 
                                    <br />
                                    <div class="benthics-samples-form-view-labels">
                                        <label>Comments:</label>
                                    </div>
                                    <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Text='<%# Bind("Comments") %>' MaxLength="100" 
                                                    CssClass="ben-taxa-data-formview-textbox"></asp:TextBox>          
                                    <br /><br />
                                    <asp:Button ID="CancelButton" runat="server" Text="Cancel" CommandName="Cancel" CssClass="adminButton" CommandArgument='<%# Eval("ID") %>' />                                                 
                                    <asp:Button ID="UpdateButton" runat="server" Text="Save Benthics Data" CommandName="Update" CssClass="adminButton" />                                                                                               
                               
                                    <asp:HiddenField id="HiddenField_DateLastModified" runat="server" value='<%# Bind("DateLastModified") %>' />
                                    <asp:HiddenField id="HiddenField_UserLastModified" runat="server" value='<%# Bind("UserLastModified") %>' />
                                </EditItemTemplate>
                            </asp:FormView>
                        </asp:Panel> 
                    </ContentTemplate>
                </asp:UpdatePanel>
                <!-- ************************* Ben Taxa Data Panel - End *******************************-->          
            </div>
        </div>
    </section>
</asp:Content>
