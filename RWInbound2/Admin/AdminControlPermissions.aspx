<%@ Page Title="Control Permissions" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminControlPermissions.aspx.cs" Inherits="RWInbound2.Admin.AdminControlPermissions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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
        <p>        
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    Search By Page Name:
                    <asp:TextBox ID="controlPermissionsPageNameSearch" 
                        AutoPostBack="true"
                        runat="server"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" Height="31px" OnClick="btnSearch_Click" />
                    <asp:Button ID="btnSearchRefresh" runat="server" Text="Reset Search" Height="31px" OnClick="btnSearchRefresh_Click" />
                     <%--This is from drag and drop from toolbox. Note, servicemethod was added by hand by me--%>
                        <ajaxToolkit:AutoCompleteExtender 
                                ID="tbSearch_AutoCompleteExtender" 
                                runat="server" 
                                BehaviorID="tbSearch_AutoCompleteExtender" 
                                DelimiterCharacters=""  
                                ServiceMethod="SearchForControlPermissionsPageName"             
                                TargetControlID="controlPermissionsPageNameSearch"
                                MinimumPrefixLength="2"
                                CompletionInterval="100" 
                                EnableCaching="true" 
                                CompletionSetCount="10"
                                UseContextKey="True">
                            </ajaxToolkit:AutoCompleteExtender> 
                </ContentTemplate>
            </asp:UpdatePanel>             
        </p>
        <asp:GridView ID="ControlPermissionsGridView" runat="server"
            DataKeyNames="ID"
            ItemType="RWInbound2.ControlPermission" 
            SelectMethod="GetControlPermissions"
            UpdateMethod="UpdateControlPermission"
            DeleteMethod="DeleteControlPermission" 
            InsertItemPosition="LastItem"  
            ShowFooter="True"
            CellPadding="4"
            AutoGenerateColumns="False" CssClass="grid-larger-editor-columns-center"
            GridLines="None" ForeColor="#333333" Height="238px"
            AllowPaging="True" Pagesize="15">
            <AlternatingRowStyle BackColor="White" />    
            <Columns>  
                <asp:TemplateField >
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
                                    OnClick = "AddNewControlPermission" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ID" HeaderText="ID" Visible="false" ReadOnly="True" SortExpression="ID" />
                <asp:TemplateField HeaderText="Description" SortExpression="Description">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtDescription" runat="server" CssClass="edit-table-control-permission-cell"
                                     contenteditable="true" TextMode="MultiLine" Wrap="true"  
                                     Text='<%# Bind("Description") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <span style="text-wrap"></span>
                        <asp:Label ID="lblDescription" runat="server" CssClass="edit-table-control-permission-label" 
                                   Text='<%# Bind("Description") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="NewDescription" runat="server" CssClass="edit-table-control-permission-cell"
                                     TextMode="MultiLine" Wrap="true" ></asp:TextBox>
                    </FooterTemplate>
                    <ItemStyle CssClass="edit-table-control-permission-description-label" />
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Page Name" SortExpression="PageName">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtPageName" runat="server" Text='<%# Bind("PageName") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblPageName" runat="server" Text='<%# Bind("PageName") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="NewPageName" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="Control ID" SortExpression="ControlID">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtControlID" runat="server" Text='<%# Bind("ControlID") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblControlID" runat="server" Text='<%# Bind("ControlID") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="NewControlID" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>    
                <asp:TemplateField HeaderText="Role Value (Number)" SortExpression="RoleValue">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtRoleValue" runat="server" FilterType="Numbers" Text='<%# Bind("RoleValue") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblRoleValue" runat="server" Text='<%# Bind("RoleValue") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="NewRoleValue" FilterType="Numbers" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="Comments" SortExpression="Comments">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtComments" runat="server" CssClass="edit-table-control-permission-cell"
                                     contenteditable="true" TextMode="MultiLine" Wrap="true"  
                                     Text='<%# Bind("Comments") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <span style="text-wrap"></span>
                        <asp:Label ID="lblComments" runat="server" CssClass="edit-table-control-permission-label" 
                                   Text='<%# Bind("Comments") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="NewComments" runat="server" CssClass="edit-table-control-permission-cell"
                                     TextMode="MultiLine" Wrap="true" ></asp:TextBox>
                    </FooterTemplate>
                    <ItemStyle CssClass="edit-table-control-permission-description-label" />
                </asp:TemplateField>                           
            </Columns>
            <EditRowStyle BackColor="#2461BF" />            
        </asp:GridView>       
    </section>
</asp:Content>
