<%@ Page Title="Configure Field Gear" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GearConfig.aspx.cs" Inherits="RWInbound2.Edit.GearConfig" %>
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
        <asp:GridView ID="GearConfigGridView" runat="server"
            DataKeyNames="ID"
            ItemType="RWInbound2.tlkGearConfig" 
            SelectMethod="GetGearConfigs"
            UpdateMethod="UpdateGearConfig"
            DeleteMethod="DeleteGearConfig" 
            InsertItemPosition="LastItem"  
            OnRowEditing="GearConfigGridView_RowEditing"
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
                                    OnClick = "AddNewGearConfig" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ID" HeaderText="ID" Visible="false" ReadOnly="True" SortExpression="ID" />
                <asp:TemplateField HeaderText="Code" SortExpression="Code">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtCode" runat="server" Text='<%# Bind("Code") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblCode" runat="server" Text='<%# Bind("Code") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="NewCode" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description" SortExpression="Description">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtDescription" runat="server" Width="250px" 
                                     contenteditable="true" Text='<%# Bind("Description") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="NewDescription" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Field Gear" SortExpression="FieldGearID">
                    <EditItemTemplate>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" class="update-panel-div">
                            <ContentTemplate>              
                                <asp:DropDownList ID="dropDownFieldGear" runat="server" 
                                                SelectMethod="BindFieldGears" DataMember="it"
                                                DataValueField="ID" DataTextField="Code"                                                 
                                                SelectedValue='<%# Bind("FieldGearID") %>' >
                               </asp:DropDownList>           
                            </ContentTemplate>
                        </asp:UpdatePanel>                         
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="FieldGearID" runat="server" Text='<%# Bind("tlkFieldGear.Code") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>                        
                        <asp:DropDownList ID="NewFieldGearID" runat="server" 
                                        SelectMethod="BindFieldGears"  DataMember="it"
                                        DataValueField="ID" DataTextField="Code">
                       </asp:DropDownList>
                    </FooterTemplate>
                </asp:TemplateField>                         
            </Columns>
            <EditRowStyle BackColor="#2461BF" />            
        </asp:GridView>       
    </section>
</asp:Content>
