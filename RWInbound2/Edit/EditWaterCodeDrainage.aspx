<%@ Page Title="Edit Water Code Drainage" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditWaterCodeDrainage.aspx.cs" Inherits="RWInbound2.Edit.EditWaterCodeDrainage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
        <asp:GridView ID="WaterCodeDrainageGridView" runat="server"
            DataKeyNames="ID"
            ItemType="RWInbound2.WaterCodeDrainage" 
            SelectMethod="GetWaterCodeDrainages"
            UpdateMethod="UpdateWaterCodeDrainage"
            DeleteMethod="DeleteWaterCodeDrainage" 
            InsertItemPosition="LastItem"  
            ShowFooter="true"
            CellPadding="4"
            OnRowEditing="WaterCodeDrainageGridView_RowEditing"
            OnRowCancelingEdit="WaterCodeDrainageGridView_RowCancelingEdit"
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
                                    OnClick = "AddNewWaterCodeDrainage" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ID" HeaderText="ID" Visible="false" ReadOnly="True" SortExpression="ID" />
                <asp:TemplateField HeaderText="Code" SortExpression="Code">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtCode" MaxLength="3" runat="server" Text='<%# Bind("Code") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator 
                                        id="RequiredFieldValidator1" runat="server" 
                                        CssClass="required"
                                        Display="Dynamic"
                                        ErrorMessage="Required!" 
                                        ControlToValidate="txtCode">
                        </asp:RequiredFieldValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblCode" runat="server" Text='<%# Bind("Code") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="NewCode" MaxLength="3" runat="server"></asp:TextBox>
                        <asp:Label ID="lblNewCodeRequired" runat="server" Visible="false" CssClass="required">
                            Required!
                        </asp:Label>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description" SortExpression="Description">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtDescription" runat="server" Text='<%# Bind("Description") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator 
                                        id="RequiredFieldValidator2" runat="server" 
                                        CssClass="required"
                                        Display="Dynamic"
                                        ErrorMessage="Required!" 
                                        ControlToValidate="txtDescription">
                        </asp:RequiredFieldValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="NewDescription" runat="server"></asp:TextBox>
                        <asp:Label ID="lblNewDescriptionRequired" runat="server" Visible="false" CssClass="required">
                            Required!
                        </asp:Label>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Comment" SortExpression="Comment">
                    <EditItemTemplate>
                        <asp:TextBox ID="txComment" runat="server" TextMode="MultiLine" 
                                     CssClass="grid-edit-water-code-drainage-textbox-comments" Text='<%# Bind("Comment") %>'></asp:TextBox>  
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblComment" runat="server" Text='<%# Bind("Comment") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="NewComment" TextMode="MultiLine" CssClass="grid-edit-water-code-drainage-textbox-comments" runat="server"></asp:TextBox>                                         
                    </FooterTemplate>
                </asp:TemplateField>                              
            </Columns>
            <EditRowStyle BackColor="#2461BF" />            
        </asp:GridView>       
    </section>
</asp:Content>
