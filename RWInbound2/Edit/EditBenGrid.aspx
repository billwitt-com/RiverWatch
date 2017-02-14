<%@ Page Title="Edit Ben Grid" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditBenGrid.aspx.cs" Inherits="RWInbound2.Edit.EditBenGrid" %>
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
        <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                Search By Ben Sample ID:
                <asp:TextBox ID="benSampleIDSearch" 
                    onkeydown="return (event.keyCode!=13);"
                    runat="server"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="Select" Height="31px" OnClick="btnSearch_Click" CausesValidation="False" CssClass="adminButton" />
                <asp:Button ID="btnSearchRefresh" runat="server" Text="Reset Search" Height="31px" OnClick="btnSearchRefresh_Click" CausesValidation="False" CssClass="adminButton"/>
                    <ajaxToolkit:AutoCompleteExtender 
                        ID="tbSearch_AutoCompleteExtender" 
                        runat="server" 
                        BehaviorID="tbSearch_AutoCompleteExtender" 
                        DelimiterCharacters=""  
                        ServiceMethod="SearchForBenSampleID"             
                        TargetControlID="benSampleIDSearch"
                        MinimumPrefixLength="2"
                        CompletionInterval="100" 
                        EnableCaching="true" 
                        CompletionSetCount="10"
                        UseContextKey="True">
                    </ajaxToolkit:AutoCompleteExtender> 
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>        
        <asp:GridView ID="BenGridGridView" runat="server"
            DataKeyNames="ID"
            ItemType="RWInbound2.tblBenGrid" 
            SelectMethod="GetBenGrids"
            UpdateMethod="UpdateBenGrid"
            DeleteMethod="DeleteBenGrid" 
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
                                    OnClick = "AddNewBenGrid" />
                    </FooterTemplate>
                </asp:TemplateField>                
                <asp:BoundField DataField="ID" HeaderText="ID" Visible="false" ReadOnly="True" SortExpression="ID" />
                <asp:TemplateField HeaderText="Ben SampID" SortExpression="BenSampID">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtBenSampID" TextMode="Number" runat="server" Text='<%# Bind("BenSampID") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator 
                                        id="RequiredFieldValidator" runat="server" 
                                        CssClass="edit-inboundicp-required"
                                        Display="Dynamic"
                                        ErrorMessage="Required!" 
                                        ControlToValidate="txtBenSampID">
                        </asp:RequiredFieldValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblBenSampID" runat="server" Text='<%# Bind("BenSampID") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="NewBenSampID" TextMode="Number" runat="server"></asp:TextBox>
                        <asp:Label ID="lblNewBenSampIDRequired" runat="server" Visible="false" CssClass="edit-inboundicp-required">
                            Required!
                        </asp:Label>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="RepNum" SortExpression="RepNum">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtRepNum" TextMode="Number" runat="server" Text='<%# Bind("RepNum") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator 
                                        id="RequiredFieldValidator1" runat="server" 
                                        CssClass="edit-inboundicp-required"
                                        Display="Dynamic"
                                        ErrorMessage="Required!" 
                                        ControlToValidate="txtRepNum">
                        </asp:RequiredFieldValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblRepNum" runat="server" Text='<%# Bind("RepNum") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="NewRepNum" TextMode="Number" runat="server"></asp:TextBox>
                        <asp:Label ID="lblNewRepNumRequired" runat="server" Visible="false" CssClass="edit-inboundicp-required">
                            Required!
                        </asp:Label>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Grid Num" SortExpression="GridNum">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtGridNum" TextMode="Number" runat="server" Text='<%# Bind("GridNum") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator 
                                        id="RequiredFieldValidator2" runat="server" 
                                        CssClass="edit-inboundicp-required"
                                        Display="Dynamic"
                                        ErrorMessage="Required!" 
                                        ControlToValidate="txtGridNum">
                        </asp:RequiredFieldValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblGridNum" runat="server" Text='<%# Bind("GridNum") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="NewGridNum" TextMode="Number" runat="server"></asp:TextBox>
                        <asp:Label ID="lblNewGridNumRequired" runat="server" Visible="false" CssClass="edit-inboundicp-required">
                            Required!
                        </asp:Label>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ben Count" SortExpression="BenCount">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtBenCount" TextMode="Number" runat="server" Text='<%# Bind("BenCount") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator 
                                        id="RequiredFieldValidator3" runat="server" 
                                        CssClass="edit-inboundicp-required"
                                        Display="Dynamic"
                                        ErrorMessage="Required!" 
                                        ControlToValidate="txtBenCount">
                        </asp:RequiredFieldValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblBenCount" runat="server" Text='<%# Bind("BenCount") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="NewBenCount" TextMode="Number" runat="server"></asp:TextBox>
                        <asp:Label ID="lblNewBenCountRequired" runat="server" Visible="false" CssClass="edit-inboundicp-required">
                            Required!
                        </asp:Label>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Storet Uploaded" SortExpression="StoretUploaded">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtStoretUploaded" MaxLength="25" runat="server" Text='<%# Bind("StoretUploaded") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator 
                                        id="RequiredFieldValidator4" runat="server" 
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
            </Columns>
            <EditRowStyle BackColor="#2461BF" />            
        </asp:GridView>       
    </section>
</asp:Content>

