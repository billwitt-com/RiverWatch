<%@ Page Title="Roles" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminRoles.aspx.cs" Inherits="RWInbound2.Admin.AdminRoles" %>
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
                    Search By Description:
                    <asp:TextBox ID="nameSearch" 
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
                        ServiceMethod="SearchForRoleName"             
                        TargetControlID="nameSearch"
                        MinimumPrefixLength="2"
                        CompletionInterval="100" 
                        EnableCaching="false" 
                        CompletionSetCount="10">
                    </ajaxToolkit:AutoCompleteExtender>
            </ContentTemplate>
        </asp:UpdatePanel>         
    </p>
        <asp:GridView ID="RolesGridView" runat="server"
            DataKeyNames="ID"
            ItemType="RWInbound2.Role" 
            SelectMethod="GetRoles"
            UpdateMethod="UpdateRole"
            DeleteMethod="DeleteRole" 
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
                        <asp:Button ID="CancelButton" runat="server" Text="Cancel" CommandName="Cancel" />
                        <asp:Button ID="UpdateButton" runat="server" Text="Update" CommandName="Update" />
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:Button ID="btnAdd" runat="server" Text="Add"
                                    OnClick = "AddNewRole" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ID" HeaderText="ID" Visible="false" ReadOnly="True" SortExpression="ID" />
                <asp:TemplateField HeaderText="Name" SortExpression="Name">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtName" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="NewName" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Level" SortExpression="Level">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtLevel" runat="server" TextMode="Number" Text='<%# Bind("Level") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblLevel" runat="server" Text='<%# Bind("Level") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="NewLevel" TextMode="Number" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>                            
            </Columns>
            <EditRowStyle BackColor="#2461BF" />            
        </asp:GridView>       
    </section>
</asp:Content>
