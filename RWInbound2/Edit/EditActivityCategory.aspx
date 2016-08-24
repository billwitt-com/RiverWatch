<%@ Page Title="Activity Categories" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
         CodeBehind="EditActivityCategory.aspx.cs" Inherits="RWInbound2.EditActivityCategory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <section spellcheck="true">
        <div>
            <hgroup>
                <h3><%: Page.Title %></h3>
            </hgroup>
        </div>
        <asp:GridView ID="ActivityCategoryGridView" runat="server"
            DataKeyNames="ID"
            ItemType="RWInbound2.tlkActivityCategory" 
            SelectMethod="GetActivityCategories"
            UpdateMethod="UpdateActivityCategory"
            InsertMethod="AddNewActivityCategory"
            CellPadding="4"
            AutoGenerateColumns="False" CssClass="grid-columns-center"
            GridLines="None" ForeColor="#333333" Height="238px">
            <AlternatingRowStyle BackColor="White" />            
            <Columns>                
                <asp:CommandField NewText="New" ShowDeleteButton="True" ShowEditButton="True" ButtonType="Button" />
                <asp:BoundField DataField="ID" HeaderText="ID" Visible="false" ReadOnly="True" SortExpression="ID" />
                <asp:TemplateField HeaderText="Code" SortExpression="Code">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtCode" runat="server" Text='<%# Bind("Code") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblCode" runat="server" Text='<%# Bind("Code") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtCode" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description" SortExpression="Description">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtDescription" runat="server" Width="250px" contenteditable="true" Text='<%# Bind("Description") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtDescription" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:CheckBoxField DataField="Valid" HeaderText="Valid"  ReadOnly="true" SortExpression="Valid" />
                <asp:TemplateField>                
                    <FooterTemplate>
                        <asp:Button runat="server" Text="Add New" CommandName="Insert" />
                    </FooterTemplate>
                </asp:TemplateField>
            </Columns>
            <EditRowStyle BackColor="#2461BF" />            
        </asp:GridView>        
    </section>
</asp:Content>
