﻿<%@ Page Title="Edit Townships" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditTownship.aspx.cs" Inherits="RWInbound2.Edit.EditTownship" %>
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
                <asp:TextBox ID="descriptionSearch" 
                    onkeydown="return (event.keyCode!=13);"
                    runat="server"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="Search" Height="31px" OnClick="btnSearch_Click" />
                <asp:Button ID="btnSearchRefresh" runat="server" Text="Reset Search" Height="31px" OnClick="btnSearchRefresh_Click" />
               <ajaxToolkit:AutoCompleteExtender 
                    ID="tbSearch_AutoCompleteExtender" 
                    runat="server" 
                    BehaviorID="tbSearch_AutoCompleteExtender" 
                    DelimiterCharacters=""  
                    ServiceMethod="SearchForTownshipsDescription"             
                    TargetControlID="descriptionSearch"
                    MinimumPrefixLength="2"
                    CompletionInterval="100" 
                    EnableCaching="false" 
                    CompletionSetCount="10">
                </ajaxToolkit:AutoCompleteExtender> 
            </ContentTemplate>
        </asp:UpdatePanel>
    </p>
        <asp:GridView ID="TownshipsGridView" runat="server"
            DataKeyNames="ID"
            ItemType="RWInbound2.tlkTownship" 
            SelectMethod="GetTownships"
            UpdateMethod="UpdateTownship"
            DeleteMethod="DeleteTownship" 
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
                                    OnClick = "AddNewTownship" />
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
                <asp:CheckBoxField DataField="Valid" HeaderText="Valid"  ReadOnly="true" SortExpression="Valid" />                
            </Columns>
            <EditRowStyle BackColor="#2461BF" />            
        </asp:GridView>       
    </section>
</asp:Content>
