<%@ Page Title="Edit Equipment" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditEquipment.aspx.cs" Inherits="RWInbound2.Edit.EditEquipment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server" ClientIDMode="Static">
    <hgroup>
        <h3><%: Page.Title %></h3>
    </hgroup>
        <asp:UpdatePanel ID="updatePanelOrgUnknownResults" runat="server">
            <ContentTemplate>
                <div class="label-placement">
                    <asp:Label ID="ErrorLabel" CssClass="label-error" runat="server" />               
                </div>
                <div class="label-placement">
                        <asp:Label ID="SuccessLabel" CssClass="label-success" runat="server" />
                </div>
                <br />
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
                        <b>Organization Name:</b>
                        <asp:Label ID="lblOrganizationName" runat="server"></asp:Label>   
                        <asp:HiddenField id="HiddenOrgID" runat="server" />
                    </div>
                </asp:Panel>
                  
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="edit-equipment-validation" DisplayMode="List" />
                  
                <asp:GridView ID="EquipmentGridView" runat="server"
                    DataKeyNames="ID"
                    ItemType="RWInbound2.View_Models.EquipmentViewModel" 
                    SelectMethod="GetEquipment"
                    UpdateMethod="UpdateEquipment"
                    DeleteMethod="DeleteEquipment" 
                    InsertItemPosition="LastItem" 
                    OnRowEditing="EquipmentGridView_RowEditing"  
                    OnRowCancelingEdit="EquipmentGridView_RowCancelingEdit"
                    ShowFooter="true"
                    CellPadding="4"
                    AutoGenerateColumns="False" CssClass="grid-columns-center grid-larger-editor-columns-edit-equipment"
                    HeaderStyle-CssClass="grid-edit-equipment-header"
                    GridLines="None" ForeColor="#333333" Height="238px"
                    AllowPaging="true" Pagesize="10"
                    EmptyDataRowStyle-VerticalAlign="top" >
                    <AlternatingRowStyle BackColor="White" />   
                    <%--Empty Data - Add a new one Start--%>                    
                    <EmptyDataTemplate>
                       <%-- <% =Show() %>--%>
                        <asp:Panel ID="NoResultsPanel" runat="server" Visible="false" OnInit="Show" OnDataBinding="Show" OnLoad="Show" >
                            <table style="color:#333333;border-collapse:collapse;">
                                <tr class="grid-edit-equipment-add-new-header">
                                    <th scope="col">&nbsp;</th>                                    
                                    <th scope="col">Item Name</th>
                                    <th scope="col">Description</th>
                                    <th scope="col">Category</th>
                                    <th scope="col">Quantity</th>
                                    <th scope="col">Serial Number</th>
                                    <th scope="col">Date Received</th>
                                    <th scope="col">Date ReJuv1</th>
                                    <th scope="col">Date ReJuv2</th>
                                    <th scope="col">Auto Replace Date</th>
                                    <th scope="col">Comment</th>
                                </tr>
                                <tr>
                                    <td colspan="12" class="grid-edit-equipment-noresults-td">
                                         <b>
                                             <br /> 
                                             No Equipment was found.                                         
                                             <br /><br />
                                         </b>  
                                    </td>                                                               
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnAdd" runat="server" Text="Add New"
                                                 OnClick="AddNewEquipment" />
                                    </td>                                    
                                    <td>
                                        <asp:DropDownList ID="dropDownNewItemNames" runat="server" AutoPostBack="false" DataMember="it"
                                                SelectMethod="BindEquipItems" CssClass="grid-edit-equipment-medium-textbox no-results"                                         
                                                AppendDataBoundItems="true" DataTextField="Description" DataValueField="ID">
                                        </asp:DropDownList> 
                                    </td>
                                    <td>
                                        <asp:TextBox ID="NewItemDescription" runat="server" TextMode="MultiLine" MaxLength="100" CssClass="grid-edit-equipment-medium-textbox"></asp:TextBox>   
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="dropDownNewEquipCategories" runat="server" AutoPostBack="false" DataMember="it"
                                                SelectMethod="BindCategories" CssClass="grid-edit-equipment-medium-textbox no-results"                                        
                                                AppendDataBoundItems="true" DataTextField="Description" DataValueField="ID">
                                        </asp:DropDownList> 
                                    </td>
                                    <td>
                                        <asp:TextBox ID="NewQuantity" runat="server" TextMode="Number" CssClass="grid-edit-equipment-small-textbox"></asp:TextBox>   
                                    </td>
                                    <td>
                                        <asp:TextBox ID="NewSerialNumber" runat="server" TextMode="MultiLine" MaxLength="50" CssClass="grid-edit-equipment-medium-textbox"></asp:TextBox>   
                                    </td>
                                    <td>
                                        <asp:TextBox ID="NewDateReceived" runat="server" CssClass="grid-edit-equipment-medium-textbox"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="NewDateReceived_CalendarExtender" runat="server" Format="MM/dd/yyyy"
                                                                        BehaviorID="NewDateReceived_CalendarExtender" 
                                                                        TargetControlID="NewDateReceived">
                                        </ajaxToolkit:CalendarExtender>
                                        <asp:CompareValidator ControlToValidate="NewDateReceived" Operator="DataTypeCheck" 
                                                                ID="CompareValidator1" runat="server" Type="Date" ForeColor="Red" Font-Bold="true"
                                                                Display="Dynamic" ErrorMessage="format (mm/dd/yyyy)." CssClass="">
                                        </asp:CompareValidator>                                    
                                    </td>
                                    <td>
                                        <asp:TextBox ID="NewDateReJuv1" runat="server" CssClass="grid-edit-equipment-medium-textbox"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="NewDateReJuv1_CalendarExtender" runat="server" Format="MM/dd/yyyy"
                                                                        BehaviorID="NewDateReJuv1_CalendarExtender" 
                                                                        TargetControlID="NewDateReJuv1">
                                        </ajaxToolkit:CalendarExtender>
                                        <asp:CompareValidator ControlToValidate="NewDateReJuv1" Operator="DataTypeCheck" 
                                                                ID="CompareValidator3" runat="server" Type="Date" ForeColor="Red" Font-Bold="true"
                                                                Display="Dynamic" ErrorMessage="format (mm/dd/yyyy)." CssClass="">
                                        </asp:CompareValidator>  
                                    </td>
                                    <td>
                                        <asp:TextBox ID="NewDateReJuv2" runat="server" CssClass="grid-edit-equipment-medium-textbox"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="NewDateReJuv2_CalendarExtender1" runat="server" Format="MM/dd/yyyy"
                                                                        BehaviorID="NewDateReJuv2_CalendarExtender" 
                                                                        TargetControlID="NewDateReJuv2">
                                        </ajaxToolkit:CalendarExtender>
                                        <asp:CompareValidator ControlToValidate="NewDateReJuv2" Operator="DataTypeCheck" 
                                                                ID="CompareValidator4" runat="server" Type="Date" ForeColor="Red" Font-Bold="true"
                                                                Display="Dynamic" ErrorMessage="format (mm/dd/yyyy)." CssClass="">
                                        </asp:CompareValidator>  
                                    </td>
                                    <td>
                                        <asp:TextBox ID="NewAutoReplaceDt" runat="server" CssClass="grid-edit-equipment-medium-textbox"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="NewAutoReplaceDt_CalendarExtender" runat="server" Format="MM/dd/yyyy"
                                                                        BehaviorID="NewAutoReplaceDt_CalendarExtender" 
                                                                        TargetControlID="NewAutoReplaceDt">
                                        </ajaxToolkit:CalendarExtender> 
                                        <asp:CompareValidator ControlToValidate="NewAutoReplaceDt" Operator="DataTypeCheck" 
                                                                ID="CompareValidator5" runat="server" Type="Date" ForeColor="Red" Font-Bold="true"
                                                                Display="Dynamic" ErrorMessage="format (mm/dd/yyyy)." CssClass="">
                                        </asp:CompareValidator>                            
                                    </td>
                                    <td>
                                        <asp:TextBox ID="NewComment" runat="server" TextMode="MultiLine" MaxLength="100" CssClass="grid-edit-equipment-medium-textbox"></asp:TextBox>   
                                    </td>
                                </tr>
                            </table>       
                        </asp:Panel>       
                    </EmptyDataTemplate>
                    <%--Empty Data - Add a new one END--%>
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
                        <asp:TemplateField HeaderText="Item Name" SortExpression="ItemName" ItemStyle-VerticalAlign="Middle" ItemStyle-CssClass="grid-edit-equipment-medium-textbox" 
                            ControlStyle-CssClass="grid-edit-equipment-dropdown">                            
                            <EditItemTemplate>
                                <asp:HiddenField id="OrganizationID" runat="server" value='<%# Bind("OrganizationID") %>' />
                                <asp:DropDownList ID="dropDownItemNames" runat="server" AutoPostBack="false" DataMember="it"
                                            SelectMethod="BindEquipItems"
                                            SelectedValue='<%# Bind("ItemName") %>'                                          
                                            AppendDataBoundItems="true" DataTextField="Description" DataValueField="Code">
                                </asp:DropDownList>                                 
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblItemName" runat="server" Text='<%# Bind("ItemName") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:HiddenField id="NewOrganizationID" runat="server" value='<%# Bind("OrganizationID") %>' />
                                <asp:DropDownList ID="dropDownNewItemNames" runat="server" AutoPostBack="false" DataMember="it"
                                            SelectMethod="BindEquipItems" CssClass="grid-edit-equipment-medium-textbox"                                         
                                            AppendDataBoundItems="true" DataTextField="Description" DataValueField="ID">
                                </asp:DropDownList>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description" SortExpression="ItemDescription" ItemStyle-VerticalAlign="Middle" ItemStyle-CssClass="grid-edit-equipment-medium-textbox" 
                            ControlStyle-CssClass="grid-edit-equipment-medium-textbox" >
                            <EditItemTemplate >
                                <asp:TextBox ID="txtItemDescription" runat="server" TextMode="MultiLine" MaxLength="100" Text='<%# Bind("ItemDescription") %>'></asp:TextBox>                                                          
                            </EditItemTemplate>
                            <ItemTemplate>
                               <asp:Label ID="lblItemDescription" runat="server" Text='<%# Bind("ItemDescription") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="NewItemDescription" runat="server" TextMode="MultiLine" MaxLength="100" CssClass="grid-edit-equipment-medium-textbox"></asp:TextBox>   
                            </FooterTemplate>
                        </asp:TemplateField>                        
                        <asp:TemplateField HeaderText="Category" SortExpression="CategoryCode" ItemStyle-VerticalAlign="Middle" ItemStyle-CssClass="grid-edit-equipment-medium-textbox" 
                            ControlStyle-CssClass="grid-edit-equipment-dropdown">
                            <EditItemTemplate>
                                <asp:DropDownList ID="dropDownEquipCategories" runat="server" AutoPostBack="false" DataMember="it"
                                            SelectMethod="BindCategories"
                                            SelectedValue='<%# Bind("CategoryID") %>'                                          
                                            AppendDataBoundItems="true" DataTextField="Description" DataValueField="ID">
                                </asp:DropDownList> 
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCategoryCode" runat="server" Text='<%# Bind("CategoryCode") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="dropDownNewEquipCategories" runat="server" AutoPostBack="false" DataMember="it"
                                            SelectMethod="BindCategories" CssClass="grid-edit-equipment-medium-textbox"                                        
                                            AppendDataBoundItems="true" DataTextField="Description" DataValueField="ID">
                                </asp:DropDownList> 
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Quantity" SortExpression="Quantity" ItemStyle-VerticalAlign="Middle" ItemStyle-CssClass="grid-edit-equipment-small-textbox" 
                            ControlStyle-CssClass="grid-edit-equipment-small-textbox" >
                            <EditItemTemplate >
                                <asp:TextBox ID="txtQuantity" runat="server" TextMode="Number" Text='<%# Bind("Quantity") %>'></asp:TextBox>                                                          
                            </EditItemTemplate>
                            <ItemTemplate>
                               <asp:Label ID="lblQuantity" runat="server" Text='<%# Bind("Quantity") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="NewQuantity" runat="server" TextMode="Number" CssClass="grid-edit-equipment-small-textbox"></asp:TextBox>   
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Serial Number" SortExpression="SerialNumber" ItemStyle-VerticalAlign="Middle" ItemStyle-CssClass="grid-edit-equipment-medium-textbox" 
                            ControlStyle-CssClass="grid-edit-equipment-medium-textbox" >
                            <EditItemTemplate >
                                <asp:TextBox ID="txtSerialNumber" runat="server" TextMode="MultiLine" MaxLength="50" Text='<%# Bind("SerialNumber") %>'></asp:TextBox>                                                          
                            </EditItemTemplate>
                            <ItemTemplate>
                               <asp:Label ID="lblSerialNumber" runat="server" Text='<%# Bind("SerialNumber") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="NewSerialNumber" runat="server" TextMode="MultiLine" MaxLength="50" CssClass="grid-edit-equipment-medium-textbox"></asp:TextBox>   
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date Received" SortExpression="DateReceived" ItemStyle-VerticalAlign="Middle" ItemStyle-CssClass="grid-edit-equipment-medium-textbox" 
                            ControlStyle-CssClass="grid-edit-equipment-medium-textbox" >
                            <EditItemTemplate >
                                <asp:TextBox ID="txtDateReceived" runat="server" Text='<%# Bind("DateReceived", "{0:MM/dd/yyyy}") %>'></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="txtDateReceived_CalendarExtender" runat="server" Format="MM/dd/yyyy"
                                                                BehaviorID="txtDateReceived_CalendarExtender" 
                                                                TargetControlID="txtDateReceived">
                                </ajaxToolkit:CalendarExtender>                                  
                                <asp:CompareValidator ControlToValidate="txtDateReceived" Operator="DataTypeCheck" 
                                                        ID="CompareValidator2" runat="server" Type="Date" ForeColor="Red" Font-Bold="true"
                                                        Display="Dynamic" ErrorMessage="format (mm/dd/yyyy)" CssClass="">
                                </asp:CompareValidator>             
                            </EditItemTemplate>
                            <ItemTemplate>
                               <asp:Label ID="lblDateReceived" runat="server" Text='<%#Eval("DateReceived", "{0:MM/dd/yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="NewDateReceived" runat="server" CssClass="grid-edit-equipment-medium-textbox"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="NewDateReceived_CalendarExtender" runat="server" Format="MM/dd/yyyy"
                                                                BehaviorID="NewDateReceived_CalendarExtender" 
                                                                TargetControlID="NewDateReceived">
                                </ajaxToolkit:CalendarExtender>
                                <asp:CompareValidator ControlToValidate="NewDateReceived" Operator="DataTypeCheck" 
                                                            ID="CompareValidator6" runat="server" Type="Date" ForeColor="Red" Font-Bold="true"
                                                            Display="Dynamic" ErrorMessage="format (mm/dd/yyyy)" CssClass="">
                                </asp:CompareValidator>  
                            </FooterTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="Date ReJuv1" SortExpression="DateReJuv1" ItemStyle-VerticalAlign="Middle" ItemStyle-CssClass="grid-edit-equipment-medium-textbox" 
                            ControlStyle-CssClass="grid-edit-equipment-medium-textbox" >
                            <EditItemTemplate >
                                <asp:TextBox ID="txtDateReJuv1" runat="server" Text='<%# Bind("DateReJuv1", "{0:MM/dd/yyyy}") %>'></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="txtDateReJuv1_CalendarExtender" runat="server" Format="MM/dd/yyyy"
                                                                BehaviorID="txtDateReJuv1_CalendarExtender" 
                                                                TargetControlID="txtDateReJuv1">
                                </ajaxToolkit:CalendarExtender>  
                                <asp:CompareValidator ControlToValidate="txtDateReJuv1" Operator="DataTypeCheck" 
                                                        ID="CompareValidator7" runat="server" Type="Date" ForeColor="Red" Font-Bold="true"
                                                        Display="Dynamic" ErrorMessage="format (mm/dd/yyyy)" CssClass="">
                                </asp:CompareValidator>                                                         
                            </EditItemTemplate>
                            <ItemTemplate>
                               <asp:Label ID="lblDateReJuv1" runat="server" Text='<%# Bind("DateReJuv1", "{0:MM/dd/yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="NewDateReJuv1" runat="server" CssClass="grid-edit-equipment-medium-textbox"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="NewDateReJuv1_CalendarExtender" runat="server" Format="MM/dd/yyyy"
                                                                BehaviorID="NewDateReJuv1_CalendarExtender" 
                                                                TargetControlID="NewDateReJuv1">
                                </ajaxToolkit:CalendarExtender>
                                <asp:CompareValidator ControlToValidate="NewDateReJuv1" Operator="DataTypeCheck" 
                                                        ID="CompareValidator8" runat="server" Type="Date" ForeColor="Red" Font-Bold="true"
                                                        Display="Dynamic" ErrorMessage="format (mm/dd/yyyy)" CssClass="">
                                </asp:CompareValidator>
                            </FooterTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="Date ReJuv2" SortExpression="DateReJuv2" ItemStyle-VerticalAlign="Middle" ItemStyle-CssClass="grid-edit-equipment-medium-textbox" 
                            ControlStyle-CssClass="grid-edit-equipment-medium-textbox" >
                            <EditItemTemplate >
                                <asp:TextBox ID="txtDateReJuv2" runat="server" Text='<%# Bind("DateReJuv2", "{0:MM/dd/yyyy}") %>'></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="txtDateReJuv2_CalendarExtender" runat="server" Format="MM/dd/yyyy"
                                                                BehaviorID="txtDateReJuv2_CalendarExtender" 
                                                                TargetControlID="txtDateReJuv2">
                                </ajaxToolkit:CalendarExtender>
                                <asp:CompareValidator ControlToValidate="txtDateReJuv2" Operator="DataTypeCheck" 
                                                        ID="CompareValidator9" runat="server" Type="Date" ForeColor="Red" Font-Bold="true"
                                                        Display="Dynamic" ErrorMessage="format (mm/dd/yyyy)" CssClass="">
                                </asp:CompareValidator>                                                        
                            </EditItemTemplate>
                            <ItemTemplate>
                               <asp:Label ID="lblDateReJuv2" runat="server" Text='<%# Bind("DateReJuv2", "{0:MM/dd/yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="NewDateReJuv2" runat="server" CssClass="grid-edit-equipment-medium-textbox"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="NewDateReJuv2_CalendarExtender" runat="server" Format="MM/dd/yyyy"
                                                                BehaviorID="NewDateReJuv2_CalendarExtender" 
                                                                TargetControlID="NewDateReJuv2">
                                </ajaxToolkit:CalendarExtender>
                                <asp:CompareValidator ControlToValidate="NewDateReJuv2" Operator="DataTypeCheck" 
                                                        ID="CompareValidator10" runat="server" Type="Date" ForeColor="Red" Font-Bold="true"
                                                        Display="Dynamic" ErrorMessage="format (mm/dd/yyyy)" CssClass="">
                                </asp:CompareValidator> 
                            </FooterTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="Auto Replace Date" SortExpression="AutoReplaceDt" ItemStyle-VerticalAlign="Middle" ItemStyle-CssClass="grid-edit-equipment-medium-textbox" 
                            ControlStyle-CssClass="grid-edit-equipment-medium-textbox" >
                            <EditItemTemplate >
                                <asp:TextBox ID="txtAutoReplaceDt" runat="server" Text='<%# Bind("AutoReplaceDt", "{0:MM/dd/yyyy}") %>'></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="txtAutoReplaceDt_CalendarExtender" runat="server" Format="MM/dd/yyyy"
                                                                BehaviorID="txtAutoReplaceDt_CalendarExtender" 
                                                                TargetControlID="txtAutoReplaceDt">
                                </ajaxToolkit:CalendarExtender>
                                <asp:CompareValidator ControlToValidate="txtAutoReplaceDt" Operator="DataTypeCheck" 
                                                        ID="CompareValidator11" runat="server" Type="Date" ForeColor="Red" Font-Bold="true"
                                                        Display="Dynamic" ErrorMessage="format (mm/dd/yyyy)" CssClass="">
                                </asp:CompareValidator>                                                         
                            </EditItemTemplate>
                            <ItemTemplate>
                               <asp:Label ID="lblAutoReplaceDt" runat="server" Text='<%# Bind("AutoReplaceDt", "{0:MM/dd/yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="NewAutoReplaceDt" runat="server" CssClass="grid-edit-equipment-medium-textbox"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="NewAutoReplaceDt_CalendarExtender" runat="server" Format="MM/dd/yyyy"
                                                                BehaviorID="NewAutoReplaceDt_CalendarExtender" 
                                                                TargetControlID="NewAutoReplaceDt">
                                </ajaxToolkit:CalendarExtender>
                                <asp:CompareValidator ControlToValidate="NewAutoReplaceDt" Operator="DataTypeCheck" 
                                                        ID="CompareValidator12" runat="server" Type="Date" ForeColor="Red" Font-Bold="true"
                                                        Display="Dynamic" ErrorMessage="format (mm/dd/yyyy)" CssClass="">
                                </asp:CompareValidator>  
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Comment" SortExpression="Comment" ItemStyle-VerticalAlign="Middle" ItemStyle-CssClass="grid-edit-equipment-medium-textbox" 
                            ControlStyle-CssClass="grid-edit-equipment-medium-textbox" >
                            <EditItemTemplate >
                                <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" MaxLength="100" Text='<%# Bind("Comment") %>'></asp:TextBox>                                                          
                            </EditItemTemplate>
                            <ItemTemplate>
                               <asp:Label ID="lblComment" runat="server" Text='<%# Bind("Comment") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="NewComment" runat="server" TextMode="MultiLine" MaxLength="100" CssClass="grid-edit-equipment-medium-textbox"></asp:TextBox>   
                            </FooterTemplate>                            
                        </asp:TemplateField>  
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />            
                </asp:GridView>       
                
            </ContentTemplate>
        </asp:UpdatePanel> 
</asp:Content>
