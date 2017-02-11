<%@ Page Title="Edit Benthic Samples" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditBenthicSamples.aspx.cs" Inherits="RWInbound2.Admin.EditBenthicSamples" %>
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
        <br />
        <asp:Panel ID="SampleDataPanel" runat="server">
            <div>
                <b>Sample Number:</b>
                <asp:Label ID="lblSampleNumber" runat="server"></asp:Label>
                <div class="edit-benthics-sample-number">
                    <b>Event:</b>
                    <asp:Label ID="lblSampleEvent" runat="server"></asp:Label>
                </div>   
            </div>
            <br />
        </asp:Panel>
        <asp:GridView ID="BenthicSamplesGridView" runat="server"
            DataKeyNames="ID"
            ItemType="RWInbound2.tblBenSamp" 
            SelectMethod="GetBenthicSamples"
            UpdateMethod="UpdateBenthicSample"
            DeleteMethod="DeleteBenthicSample" 
            InsertItemPosition="LastItem"  
            OnRowEditing="BenthicSamplesGridView_RowEditing"
            ShowFooter="true"
            CellPadding="4"
            AutoGenerateColumns="False" CssClass="grid-columns-center grid-edit-benthics-samples-item-rowstyle"
            HeaderStyle-CssClass="grid-edit-benthics-samples-header" 
            GridLines="None" ForeColor="#333333" 
            AllowPaging="true" Pagesize="15">
            <AlternatingRowStyle BackColor="White" />
            <EmptyDataTemplate>
                <div class="grid-edit-benthics-samples-empty-data-div">
                    No Samples were found.
                     <%-- <% =Show() %>--%>
                    <asp:Panel ID="NoResultsPanel" runat="server" Visible="false" OnInit="Show" OnDataBinding="Show" OnLoad="Show"
                         CssClass="grid-edit-benthics-samples-no-results-panel" >
                        <%--<asp:Button ID="btnAdd" runat="server" Text="Add New Benthic Sample"
                                    OnClick="AddNewBenthicSample" CssClass="adminButton grid-edit-benthics-samples-add-new-button" />  --%>
                        <asp:Button ID="btnAdd" runat="server" Text="Add New Benthic Sample"
                                     OnClientClick="return alert('Coming Soon!');" CssClass="adminButton grid-edit-benthics-samples-add-new-button" />                    
                    </asp:Panel>
                </div>
            </EmptyDataTemplate>    
            <Columns>  
                <asp:TemplateField>
                    <ItemTemplate>
                       <%-- <asp:Button ID="EditButton" runat="server" Text="Edit" CommandName="Edit" />--%>
                        <%--<asp:Button ID="DeleteButton" runat="server" Text="Delete" CommandName="Delete" 
                                    OnClientClick="return confirm('Are you certain you want to delete this?');"/>--%>
                        <asp:Button ID="EditButton" runat="server" Text="Edit" OnClientClick="return alert('Coming Soon!');" />
                        <asp:Button ID="DeleteButton" runat="server" Text="Delete" OnClientClick="return alert('Coming Soon!');" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Button ID="CancelButton" runat="server" Text="Cancel" CommandName="Cancel" />
                        <asp:Button ID="UpdateButton" runat="server" Text="Update" CommandName="Update" />
                    </EditItemTemplate>
                    <FooterTemplate>
                        <%--<asp:Button ID="btnAdd" runat="server" Text="Add"
                                    OnClick="AddNewBenthicSample" />--%>
                        <asp:Button ID="btnAdd" runat="server" Text="Add"
                                     OnClientClick="return alert('Coming Soon!');" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ID" HeaderText="ID" Visible="false" ReadOnly="True" SortExpression="ID" />
                <asp:TemplateField HeaderText="Collected" SortExpression="CollDate">                   
                    <ItemTemplate>
                        <asp:Label ID="lblCollDate" runat="server" Text='<%# Eval("CollDate", "{0:M-dd-yyyy}") %>'></asp:Label>
                       
                        <asp:Label ID="lblCollTime" runat="server" Text='<%# Eval("CollTime", "{0:HH:mm}") %>'></asp:Label>                                              
                    </ItemTemplate>                   
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Activity" SortExpression="tlkActivityCategory.Description">                   
                    <ItemTemplate>
                        <asp:Label ID="lbltlkActivityCategoryDescription" runat="server" Text='<%# Bind("tlkActivityCategory.Description") %>'></asp:Label>
                    </ItemTemplate>                    
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="Intent" SortExpression="tlkIntent.Description">                   
                    <ItemTemplate>
                        <asp:Label ID="lbltlkIntentDescription" runat="server" Text='<%# Bind("tlkIntent.Description") %>'></asp:Label>
                    </ItemTemplate>                    
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="Community" SortExpression="tlkCommunity.Description">                   
                    <ItemTemplate>
                        <asp:Label ID="lbltlkCommunityDescription" runat="server" Text='<%# Bind("tlkCommunity.Description") %>'></asp:Label>
                    </ItemTemplate>                    
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="Intent" SortExpression="tlkBioResultsType.Description">                   
                    <ItemTemplate>
                        <asp:Label ID="lbltlkBioResultsTypeDescription" runat="server" Text='<%# Bind("tlkBioResultsType.Description") %>'></asp:Label>
                    </ItemTemplate>                    
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Reps/Grids">                   
                    <ItemTemplate>
                        <asp:Button ID="ViewRepsGridButton" runat="server" Text="View" OnClientClick="return alert('Coming Soon!');"/>
                        <%--<asp:Button ID="GetAssignedSamplesButton" runat="server" Text="Download Assigned Samples"
                                    CommandName="GetAssignedSamples" CommandArgument='<%# Bind("ID") %>' />--%>
                    </ItemTemplate>                   
                </asp:TemplateField>                             
            </Columns>
            <EditRowStyle BackColor="#2461BF" />            
        </asp:GridView>       
    </section>
</asp:Content>
