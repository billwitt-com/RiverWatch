<%@ Page Title="Edit Equipment" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditEquipment.aspx.cs" Inherits="RWInbound2.Edit.EditEquipment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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

        <asp:UpdatePanel ID="updatePanelOrgUnknownResults" runat="server">
            <ContentTemplate>
                <label>Search By Organization Name:</label>
                <asp:TextBox ID="orgNameSearch" 
                    AutoPostBack="true"
                    runat="server"></asp:TextBox>
                <asp:Button ID="btnSearchOrgName" runat="server" Text="Select" Height="31px" OnClick="btnSearchOrgName_Click" CssClass="adminButton" />
                <ajaxToolkit:AutoCompleteExtender 
                    ID="tbSearch_AutoCompleteExtender" 
                    runat="server" 
                    BehaviorID="tbSearch_AutoCompleteExtender" 
                    DelimiterCharacters=""  
                    ServiceMethod="SearchForOrgName"             
                    TargetControlID="orgNameSearch"
                    MinimumPrefixLength="2"
                    CompletionInterval="100" 
                    EnableCaching="false" 
                    CompletionSetCount="10">
                </ajaxToolkit:AutoCompleteExtender> 
                <div class="org-unknown-results-or"><label>OR</label></div>
                <label>Search By Kit Number:</label>                
                <asp:TextBox ID="kitNumberSearch" 
                    AutoPostBack="true"
                    runat="server"></asp:TextBox>
                <asp:Button ID="btnSearchKitNumber" runat="server" Text="Select" Height="31px" OnClick="btnSearchKitNumber_Click" CssClass="adminButton" />                
                <ajaxToolkit:AutoCompleteExtender 
                    ID="AutoCompleteExtender1" 
                    runat="server" 
                    BehaviorID="tbSearch_AutoCompleteExtender1" 
                    DelimiterCharacters=""  
                    ServiceMethod="SearchForkitNumber"             
                    TargetControlID="kitNumberSearch"
                    MinimumPrefixLength="1"
                    CompletionInterval="100" 
                    EnableCaching="false" 
                    CompletionSetCount="10">
                </ajaxToolkit:AutoCompleteExtender>
                <asp:Button ID="btnSearchRefresh" runat="server" Text="Reset Search" Height="31px" OnClick="btnSearchRefresh_Click" CssClass="adminButton org-unknown-results-reset-search" /> 
                <br />
                <br />
                <asp:Panel ID="OrganizationNamePanel" runat="server">
                    <div>
                        <b>Orgnization Name:</b>
                        <asp:Label ID="lblOrganizationName" runat="server"></asp:Label>   
                    </div>
                </asp:Panel>
                  
                <asp:GridView ID="EquipmentGridView" runat="server"
                    DataKeyNames="ID"
                    ItemType="RWInbound2.View_Models.EquipmentViewModel" 
                    SelectMethod="GetEquipment"
                    UpdateMethod="UpdateEquipment"
                    DeleteMethod="DeleteEquipment" 
                    InsertItemPosition="LastItem"  
                    ShowFooter="true"
                    CellPadding="4"
                    AutoGenerateColumns="False" CssClass="grid-columns-center"
                    GridLines="None" ForeColor="#333333" Height="238px"
                    AllowPaging="true" Pagesize="15">
                    <AlternatingRowStyle BackColor="White" />    
                    <Columns>  
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="EditButton" runat="server" Text="Edit" CommandName="Edit" />
                                <asp:Button ID="DeleteButton" runat="server" Text="Delete" CommandName="Delete" 
                                            OnClientClick="return confirm('Are you certain you want to delete this?');"/>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Button ID="CancelButton" runat="server" Text="Cancel" CommandName="Cancel" CausesValidation="false" />
                                <asp:Button ID="UpdateButton" runat="server" Text="Update" CommandName="Update" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:Button ID="btnAdd" runat="server" Text="Add"
                                            OnClick = "AddNewEquipment" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ID" HeaderText="ID" Visible="false" ReadOnly="True" SortExpression="ID" />
                        <asp:TemplateField HeaderText="Item Name" SortExpression="ItemName">                            
                            <EditItemTemplate>
                                <asp:DropDownList ID="dropDownItemNames" runat="server" AutoPostBack="True" DataMember="it"
                                            SelectMethod="BindEquipItems"
                                            SelectedValue='<%# Bind("ItemName") %>'                                          
                                            AppendDataBoundItems="true" DataTextField="Description" DataValueField="Code">
                                </asp:DropDownList> 
                                <asp:HiddenField id="OrganizationID" runat="server" value='<%# Bind("OrganizationID") %>' />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblItemName" runat="server" Text='<%# Bind("ItemName") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="dropDownNewItemNames" runat="server" AutoPostBack="True" DataMember="it"
                                            SelectMethod="BindEquipItems"                                         
                                            AppendDataBoundItems="true" DataTextField="Description" DataValueField="ID">
                                </asp:DropDownList> 
                                <asp:HiddenField id="OrganizationID" runat="server" value='<%# Bind("OrganizationID") %>' />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Category" SortExpression="CategoryCode">
                            <EditItemTemplate>
                                <asp:DropDownList ID="dropDownEquipCategories" runat="server" AutoPostBack="True" DataMember="it"
                                            SelectMethod="BindCategories"
                                            SelectedValue='<%# Bind("CategoryID") %>'                                          
                                            AppendDataBoundItems="true" DataTextField="Description" DataValueField="ID">
                                </asp:DropDownList> 
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCategoryCode" runat="server" Text='<%# Bind("CategoryCode") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="dropDownNewEquipCategories" runat="server" AutoPostBack="True" DataMember="it"
                                            SelectMethod="BindCategories"                                         
                                            AppendDataBoundItems="true" DataTextField="Description" DataValueField="ID">
                                </asp:DropDownList> 
                            </FooterTemplate>
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="ParaID" SortExpression="ParaID">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtParaID" TextMode="Number" runat="server" Text='<%# Bind("ParaID") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblParaID" runat="server" Text='<%# Bind("ParaID") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="NewParaID" TextMode="Number" runat="server"></asp:TextBox>
                                <asp:Label ID="lblNewParaIDRequired" runat="server" Visible="false" CssClass="edit-inboundicp-required">
                                    Required!
                                </asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Value" SortExpression="Value">
                            <EditItemTemplate>
                                <asp:TextBox ID="txValue" runat="server" Text='<%# Bind("Value") %>'></asp:TextBox>
                                <asp:RequiredFieldValidator 
                                                id="RequiredFieldValidator" runat="server" 
                                                CssClass="edit-inboundicp-required"
                                                Display="Dynamic"
                                                ErrorMessage="Required!" 
                                                ControlToValidate="txValue">
                                </asp:RequiredFieldValidator>
                                <asp:CompareValidator ControlToValidate="txValue" Operator="DataTypeCheck" 
                                                ID="CompareValidator1" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                                Display="Dynamic" ErrorMessage="Please enter a valid decimal value." CssClass="">
                                </asp:CompareValidator>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblValue" runat="server" Text='<%# Bind("Value") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="NewValue" runat="server"></asp:TextBox>
                                <asp:CompareValidator ControlToValidate="NewValue" Operator="DataTypeCheck" 
                                                ID="CompareValidator2" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                                Display="Dynamic" ErrorMessage="Please enter a valid decimal value." CssClass="">
                                </asp:CompareValidator>                        
                            </FooterTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="SampleID" SortExpression="SampleID">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtSampleID" TextMode="Number" runat="server" Text='<%# Bind("SampleID") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblSampleID" runat="server" Text='<%# Bind("SampleID") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="NewSampleID" TextMode="Number" runat="server"></asp:TextBox>
                                <asp:Label ID="lblNewSampleIDRequired" runat="server" Visible="false" CssClass="edit-inboundicp-required">
                                    Required!
                                </asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Storet Uploaded" SortExpression="StoretUploaded">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtStoretUploaded" MaxLength="25" runat="server" Text='<%# Bind("StoretUploaded") %>'></asp:TextBox>
                                <asp:RequiredFieldValidator 
                                                id="RequiredFieldValidator2" runat="server" 
                                                CssClass="edit-inboundicp-required"
                                                Display="Dynamic"
                                                ErrorMessage="Required!" 
                                                ControlToValidate="txtStoretUploaded">
                                </asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblStoretUploaded" runat="server" Text='<%# Bind("StoretUploaded") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="NewStoretUploaded" MaxLength="25" runat="server"></asp:TextBox>
                                <asp:Label ID="lblNewStoretUploadedRequired" runat="server" Visible="false" CssClass="edit-inboundicp-required">
                                    Required!
                                </asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="Enter Date" SortExpression="EnterDate">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEnterDate" runat="server" Text='<%# Bind("EnterDate") %>'></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="txtEnterDate_CalendarExtender" runat="server" 
                                                        BehaviorID="txtEnterDate_CalendarExtender" 
                                                        TargetControlID="txtEnterDate"></ajaxToolkit:CalendarExtender>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblEnterDate" runat="server" Text='<%# Bind("EnterDate") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="NewEnterDate" runat="server"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="NewEnterDate_CalendarExtender" runat="server" 
                                                        BehaviorID="NewEnterDate_CalendarExtender" 
                                                        TargetControlID="NewEnterDate"></ajaxToolkit:CalendarExtender>
                            </FooterTemplate>
                        </asp:TemplateField>    --%>             
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />            
                </asp:GridView>       
            </ContentTemplate>
        </asp:UpdatePanel> 
</asp:Content>
