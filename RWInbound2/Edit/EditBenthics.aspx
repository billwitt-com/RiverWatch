<%@ Page Title="Edit Benthics" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditBenthics.aspx.cs" Inherits="RWInbound2.Edit.EditBenthics" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server" ClientIDMode="Static">
    <section spellcheck="true">
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
                    Search By Benthics Sample ID:
                    <asp:TextBox ID="benSampIDSearch" 
                        onkeydown="return (event.keyCode!=13);"
                        runat="server"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="Select" Height="31px" OnClick="btnSearch_Click" CausesValidation="False" CssClass="adminButton" />
                    <asp:Button ID="btnSearchRefresh" runat="server" Text="Reset Search" Height="31px" OnClick="btnSearchRefresh_Click" CausesValidation="False" CssClass="adminButton"/>
                        <ajaxToolkit:AutoCompleteExtender 
                            ID="tbSearch_AutoCompleteExtender" 
                            runat="server" 
                            BehaviorID="tbSearch_AutoCompleteExtender" 
                            DelimiterCharacters=""  
                            ServiceMethod="SearchForBenSampID"             
                            TargetControlID="benSampIDSearch"
                            MinimumPrefixLength="2"
                            CompletionInterval="100" 
                            EnableCaching="false" 
                            CompletionSetCount="10">
                        </ajaxToolkit:AutoCompleteExtender> 
                </ContentTemplate>
            </asp:UpdatePanel>
        </div> 
        <br />     
        <asp:GridView ID="tblBenthicsGridView" runat="server"
                    DataKeyNames="ID"
                    ItemType="RWInbound2.tblBenthic" 
                    SelectMethod="GettblBenthics"
                    UpdateMethod="UpdatetblBenthic"
                    DeleteMethod="DeletetblBenthic"                     
                    InsertItemPosition="LastItem"  
                    ShowFooter="true"           
                    AutoGenerateColumns="False" CssClass="grid-columns-center grid-view"
                    GridLines="Vertical" ForeColor="#333333" Height="238px"
                    AllowPaging="true" Pagesize="15" 
                    OnRowUpdating="tblBenthicsGridView_RowUpdating"
                    OnRowDataBound="tblBenthicsGridView_RowDataBound"
             OnRowCommand="tblBenthicsGridView_RowCommand">
                    <AlternatingRowStyle BackColor="White" />    
                    <Columns>  
                        <asp:TemplateField ItemStyle-CssClass="grid-edit-benthics-buttons" FooterStyle-CssClass="grid-edit-benthics-buttons" ControlStyle-CssClass="grid-edit-benthics-buttons" >
                            <ItemTemplate>
                                <asp:Button ID="EditButton" runat="server" Text="Edit" CommandName="Edit" />
                                <asp:Button ID="DeleteButton" runat="server" Text="Delete" CommandName="Delete" 
                                            OnClientClick="return confirm('Are you certain you want to delete this?');"/>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Button ID="CancelButton" runat="server" Text="Cancel" CommandName="Cancel"/>
                                <asp:Button ID="UpdateButton" runat="server" Text="Update" CommandName="Update" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:Button ID="btnAdd" runat="server" Text="Add" CausesValidation="false"
                                            OnClick="AddNewtblBenthic" CssClass="grid-edit-benthics-button"/>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ID" HeaderText="ID" Visible="false" ReadOnly="True" SortExpression="ID" />
                        <asp:TemplateField HeaderText="Stage" SortExpression="Stage">
                            <EditItemTemplate >
                                <asp:TextBox ID="txtStage" runat="server" Text='<%# Bind("Stage") %>' MaxLength="20"></asp:TextBox>
                                <asp:RequiredFieldValidator 
                                        id="RequiredFieldValidator" runat="server" 
                                        CssClass="edit-inboundicp-required"
                                        Display="Dynamic"
                                        ErrorMessage="Required!" 
                                        ControlToValidate="txtStage">
                                </asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblStage" runat="server" Text='<%# Bind("Stage") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="NewStage" runat="server" MaxLength="20"></asp:TextBox>                                                               
                                <asp:Label ID="lblStageRequired" runat="server" Visible="false" CssClass="edit-inboundicp-required">
                                    Required!
                                </asp:Label>
                            </FooterTemplate>                    
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Ben Sample ID" SortExpression="BenSampID" >
                            <EditItemTemplate>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate> 
                                        <asp:DropDownList ID="updateBenSampIDDropDown" runat="server" 
                                                            DataTextField="ID" DataValueField="ID">                                           
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblBenSampID" runat="server" Text='<%# Bind("BenSampID") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>                                         
                                        <asp:DropDownList ID="NewBenSampIDDropDown" runat="server"
                                                          SelectMethod="BindBenSampIDs"  
                                                          DataTextField="ID" DataValueField="ID">                                    
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Rep Num" SortExpression="RepNum" >
                            <EditItemTemplate>
                                <asp:TextBox ID="txtRepNum" runat="server" TextMode="Number" Text='<%# Bind("RepNum") %>' CssClass="grid-edit-benthics-width"></asp:TextBox>
                                <asp:RequiredFieldValidator 
                                        id="RequiredFieldValidator2" runat="server" 
                                        CssClass="edit-inboundicp-required"
                                        Display="Dynamic"
                                        ErrorMessage="Required!" 
                                        ControlToValidate="txtRepNum">
                                </asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblRepNum" runat="server" Text='<%# Bind("RepNum") %>' CssClass="grid-edit-benthics-width"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="NewRepNum" runat="server" TextMode="Number" CssClass="grid-edit-benthics-width"></asp:TextBox>
                                <asp:Label ID="lblRepNumRequired" runat="server" Visible="false" CssClass="edit-inboundicp-required">
                                    Required!
                                </asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>  
                        <asp:TemplateField HeaderText="Individuals" SortExpression="Individuals">
                            <EditItemTemplate>                        
                                <asp:TextBox ID="txtIndividuals" runat="server" Text='<%# Bind("Individuals") %>' TextMode="Number" CssClass="grid-edit-benthics-width"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblIndividuals" runat="server" Text='<%# Bind("Individuals") %>' CssClass="grid-edit-benthics-width"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="NewIndividuals" runat="server" TextMode="Number" CssClass="grid-edit-benthics-width"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Excluded Tax A" SortExpression="ExcludedTaxa">
                            <EditItemTemplate>
                                <asp:CheckBox ID="checkBoxExcludedTaxa" runat="server" Checked='<%# Bind("ExcludedTaxa") %>'/>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="checkExcludedTaxa" runat="server" Checked='<%# Bind("ExcludedTaxa") %>' Enabled="false" />
                            </ItemTemplate>
                            <FooterTemplate>
                               <asp:CheckBox ID="NewExcludedTaxa" runat="server" />
                            </FooterTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="Habitat Type" SortExpression="HabitatType">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtHabitatType" runat="server" Text='<%# Bind("HabitatType") %>' MaxLength="50"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblHabitatType" runat="server" Text='<%# Bind("HabitatType") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="NewHabitatType" runat="server" MaxLength="50"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Comments" SortExpression="Comments">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtComments" runat="server" Text='<%# Bind("Comments") %>' TextMode="MultiLine" MaxLength="255" CssClass="grid-edit-benthics-comments"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblComments" runat="server" Text='<%# Bind("Comments") %>' CssClass="grid-edit-benthics-comments"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="NewComments" runat="server" TextMode="MultiLine" MaxLength="255" CssClass="grid-edit-benthics-comments"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Enter Date" SortExpression="EnterDate">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEnterDate" runat="server" Text='<%# Bind("EnterDate") %>' TextMode="DateTime"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="txtEnterDate_CalendarExtender" runat="server" 
                                                BehaviorID="txtEnterDate_CalendarExtender" 
                                                TargetControlID="txtEnterDate"></ajaxToolkit:CalendarExtender>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblEnterDate" runat="server" Text='<%# Bind("EnterDate") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="NewEnterDate" runat="server" TextMode="DateTime"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="NewEnterDate_CalendarExtender" runat="server" 
                                                BehaviorID="NewEnterDate_CalendarExtender" 
                                                TargetControlID="NewEnterDate"></ajaxToolkit:CalendarExtender>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Storet Uploaded" SortExpression="StoretUploaded">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtStoretUploaded" runat="server" Text='<%# Bind("StoretUploaded") %>' MaxLength="25"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblStoretUploaded" runat="server" Text='<%# Bind("StoretUploaded") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="NewStoretUploaded" runat="server" MaxLength="25"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />                        
                </asp:GridView> 
    </section>
</asp:Content>
