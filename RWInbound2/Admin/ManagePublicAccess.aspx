<%@ Page Title="Manage Public User Access" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManagePublicAccess.aspx.cs" Inherits="RWInbound2.Admin.ManagePublicAccess" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <%--<script src="../Scripts/WebForms/ValidateEmail.js"></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server" ClientIDMode="Static">   
    <section spellcheck="true">
        <div>
            <hgroup>
                <h3><%: Page.Title %></h3>
            </hgroup>

        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="label-placement">
                    <asp:Label ID="ErrorLabel" CssClass="label-error" runat="server" />               
                </div>
                <div class="label-placement">
                     <asp:Label ID="SuccessLabel" CssClass="label-success" runat="server" />
                </div>
                <br />            
                <p>
                    <asp:Button ID="btnUnapprovedUsers" runat="server" Text="Unapproved Users" Height="31px" OnClick="btnUnapprovedUsers_Click" CssClass="adminButton" />
                    <asp:Button ID="btnSeeAllUsers" runat="server" Text="Reset to All Users" Height="31px" OnClick="btnSeeAllUsers_Click" CssClass="adminButton"/>
                </p>       
                <asp:GridView ID="PublicUsersGridView" runat="server"
                    DataKeyNames="ID"
                    ItemType="RWInbound2.PublicUsers" 
                    SelectMethod="GetPublicUsers"
                    UpdateMethod="UpdatePublicUser"
                    DeleteMethod="DeletePublicUser" 
                    InsertItemPosition="LastItem"  
                    ShowFooter="true"           
                    AutoGenerateColumns="False" CssClass="grid-columns-center grid-larger-editor-columns-manage-public-users"
                    GridLines="Vertical" ForeColor="#333333" Height="238px"
                    AllowPaging="true" Pagesize="15"
                    OnRowUpdating="PublicUsersGridView_RowUpdating"
                    OnRowDataBound="PublicUsersGridView_RowDataBound"
                    OnRowEditing="PublicUsersGridView_RowEditing"
                    HeaderStyle-CssClass="grid-manage-public-users-header" >
                    <AlternatingRowStyle BackColor="White" />    
                    <Columns>  
                        <asp:TemplateField ControlStyle-CssClass="controls-manage-public-users" ItemStyle-VerticalAlign="Middle">
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
                                <asp:Button ID="btnAdd" runat="server" Text="Add" CausesValidation="false"
                                            OnClick="AddNewPublicUser" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ID" HeaderText="ID" Visible="false" ReadOnly="True" SortExpression="ID" />
                        <asp:TemplateField HeaderText="Email" SortExpression="Email"  ItemStyle-VerticalAlign="Middle" ItemStyle-CssClass="grid-manage-public-users-width">
                            <EditItemTemplate >
                                <asp:TextBox ID="txtEmail" runat="server" TextMode="MultiLine" Text='<%# Bind("Email") %>' CssClass="grid-manage-public-users-width"></asp:TextBox>
                                <asp:RequiredFieldValidator 
                                        id="RequiredFieldValidator" runat="server" 
                                        CssClass="edit-inboundicp-required"
                                        Display="Dynamic"
                                        ErrorMessage="Required!" 
                                        ControlToValidate="txtEmail">
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                    runat="server" ErrorMessage="Please Enter Valid Email"
                                    ControlToValidate="txtEmail"
                                    CssClass="edit-inboundicp-required"
                                    Display="Dynamic"
                                    ForeColor="Red"
                                    ValidationExpression="^[a-zA-Z0-9.!#$%&'*+=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$">
                                </asp:RegularExpressionValidator>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <div class="grid-manage-public-users-width-view-email">
                                    <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("Email") %>' CssClass="grid-manage-public-users-width"></asp:Label>
                                </div>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="NewEmail" runat="server" TextMode="MultiLine" CssClass="grid-manage-public-users-width-new-email"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                                    runat="server" ErrorMessage="Please Enter Valid Email"
                                    ControlToValidate="NewEmail"
                                    CssClass="edit-inboundicp-required"
                                    Display="Dynamic"
                                    ForeColor="Red"
                                    ValidationExpression="^[a-zA-Z0-9.!#$%&'*+=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$">
                                </asp:RegularExpressionValidator>                                
                                <asp:Label ID="lblEmailRequired" runat="server" Visible="false" CssClass="edit-inboundicp-required">
                                    Required!
                                </asp:Label>
                            </FooterTemplate>                    
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="First Name" SortExpression="FirstName" >
                            <EditItemTemplate>
                                <asp:TextBox ID="txtFirstName" runat="server" Text='<%# Bind("FirstName") %>' CssClass="grid-manage-public-users-width"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblFirstName" runat="server" Text='<%# Bind("FirstName") %>' CssClass="grid-manage-public-users-width"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="NewFirstName" runat="server" CssClass="grid-manage-public-users-width-new"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>  
                        <asp:TemplateField HeaderText="Last Name" SortExpression="LastName">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtLastName" runat="server" Text='<%# Bind("LastName") %>' CssClass="grid-manage-public-users-width"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblLastName" runat="server" Text='<%# Bind("LastName") %>' CssClass="grid-manage-public-users-width"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="NewLastName" runat="server" CssClass="grid-manage-public-users-width-new"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>  
                        <asp:TemplateField HeaderText="Address1" SortExpression="Address1">
                            <EditItemTemplate>                        
                                <asp:TextBox ID="txtAddress1" runat="server" Text='<%# Bind("Address1") %>' TextMode="MultiLine" CssClass="grid-manage-public-users-width"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblAddress1" runat="server" Text='<%# Bind("Address1") %>' CssClass="grid-manage-public-users-width"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="NewAddress1" runat="server" TextMode="MultiLine" CssClass="grid-manage-public-users-width-new"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Address2" SortExpression="Address2">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtAddress2" runat="server" Text='<%# Bind("Address2") %>' TextMode="MultiLine" CssClass="grid-manage-public-users-width"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblAddress2" runat="server" Text='<%# Bind("Address2") %>' CssClass="grid-manage-public-users-width"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="NewAddress2" runat="server" TextMode="MultiLine" CssClass="grid-manage-public-users-width-new"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>  
                        <asp:TemplateField HeaderText="City" SortExpression="City">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtCity" runat="server" Text='<%# Bind("City") %>' CssClass="grid-manage-public-users-width"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCity" runat="server" Text='<%# Bind("City") %>' CssClass="grid-manage-public-users-width"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="NewCity" runat="server" CssClass="grid-manage-public-users-width-new"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="State" SortExpression="State">
                            <EditItemTemplate>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate> 
                                        <asp:DropDownList ID="updateStateDropDown" runat="server" CssClass="grid-manage-public-users-state-dropdown" 
                                                            DataTextField="Value" DataValueField="Text">                                           
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblState" runat="server" Text='<%# Bind("State") %>' CssClass="grid-manage-public-users-width"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>                                         
                                        <asp:DropDownList ID="NewStateDropDown" runat="server" 
                                                          SelectMethod="BindStates" 
                                                          CssClass="grid-manage-public-users-state-dropdown" 
                                                          DataTextField="Value" DataValueField="Text">                                    
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Zip" SortExpression="Zip">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtZip" runat="server" Text='<%# Bind("Zip") %>' CssClass="grid-manage-public-users-width"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblZip" runat="server" Text='<%# Bind("Zip") %>' CssClass="grid-manage-public-users-width"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="NewZip" runat="server" CssClass="grid-manage-public-users-width-new-zip"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Approved" SortExpression="Approved" ItemStyle-CssClass="grid-manage-public-users-width">
                            <EditItemTemplate>
                                <asp:CheckBox ID="checkBoxApproved" runat="server" Checked='<%# Bind("Approved") %>' CssClass="grid-manage-public-users-width"/>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="checkApproved" runat="server" Checked='<%# Bind("Approved") %>' Enabled="false" CssClass="grid-manage-public-users-width" />
                            </ItemTemplate>
                            <FooterTemplate>
                               <asp:CheckBox ID="NewApproved" runat="server" CssClass="grid-manage-public-users-width-new" />
                            </FooterTemplate>
                        </asp:TemplateField>    
                        <asp:BoundField DataField="ApprovedBy" HeaderText="Approved By" ReadOnly="true" SortExpression="ApprovedBy" ItemStyle-CssClass="grid-manage-public-users-width" />
                        <asp:BoundField DataField="ApprovedDate" HeaderText="Approved Date" ReadOnly="true" SortExpression="ApprovedDate" ItemStyle-CssClass="grid-manage-public-users-width"/>
                        <asp:BoundField DataField="DateCreated" HeaderText="Date Created" ReadOnly="true" SortExpression="DateCreated" ItemStyle-CssClass="grid-manage-public-users-width"/>
                        <asp:BoundField DataField="DateLastActivity" HeaderText="Date Last Activity" ReadOnly="true" SortExpression="DateLastActivity" ItemStyle-CssClass="grid-manage-public-users-width"/>
                        <asp:BoundField DataField="UseCount" HeaderText="Use Count" ReadOnly="true" SortExpression="UseCount" />
                        <asp:TemplateField HeaderText="Receive Email Updates" SortExpression="ReceiveEmailUpdates">
                            <EditItemTemplate>
                                <asp:CheckBox ID="checkBoxReceiveEmailUpdates" runat="server" Checked='<%# Bind("ReceiveEmailUpdates") %>' />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="checkReceiveEmailUpdates" runat="server" Checked='<%# Bind("ReceiveEmailUpdates") %>' Enabled="false" />
                            </ItemTemplate>
                            <FooterTemplate>
                               <asp:CheckBox ID="NewReceiveEmailUpdates" runat="server" />
                            </FooterTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />                        
                </asp:GridView> 
            </ContentTemplate>
        </asp:UpdatePanel>       
    </section>
</asp:Content>
