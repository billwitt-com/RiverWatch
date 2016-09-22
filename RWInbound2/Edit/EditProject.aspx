<%@ Page Title="Project" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditProject.aspx.cs" Inherits="RWInbound2.Edit.EditProject" %>
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
                    Search By Project Name:
                    <asp:TextBox ID="projectNameSearch" 
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
                                ServiceMethod="SearchForProjectsName"             
                                TargetControlID="projectNameSearch"
                                MinimumPrefixLength="2"
                                CompletionInterval="100" 
                                EnableCaching="true" 
                                CompletionSetCount="10"
                                UseContextKey="True">
                            </ajaxToolkit:AutoCompleteExtender> 
                </ContentTemplate>
            </asp:UpdatePanel>             
        </p>
        <asp:GridView ID="ProjectsGridView" runat="server"
            DataKeyNames="ProjectID"
            ItemType="RWInbound2.Project" 
            SelectMethod="GetProjects"
            UpdateMethod="UpdateProject"
            DeleteMethod="DeleteProject" 
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
                                    OnClick = "AddNewProject" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" Visible="false" ReadOnly="True" SortExpression="ProjectID" />
                <asp:TemplateField HeaderText="Project Name" SortExpression="Project Name">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtProjectName" runat="server" Text='<%# Bind("ProjectName") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblProjectName" runat="server" Text='<%# Bind("ProjectName") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="NewProjectName" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Project Description" SortExpression="Project Description">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtProjectDescription" runat="server" CssClass="edit-table-project-description-cell"
                                     contenteditable="true" TextMode="MultiLine" Wrap="true"  
                                     Text='<%# Bind("ProjectDescription") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <span style="text-wrap"></span>
                        <asp:Label ID="lblProjectDescription" runat="server" CssClass="edit-table-project-description-label" 
                                   Text='<%# Bind("ProjectDescription") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="NewProjectDescription" runat="server" CssClass="edit-table-project-description-cell"
                                     TextMode="MultiLine" Wrap="true" ></asp:TextBox>
                    </FooterTemplate>
                    <ItemStyle CssClass="edit-table-project-description-label" />
                </asp:TemplateField>
                <asp:CheckBoxField DataField="valid" HeaderText="Valid"  ReadOnly="true" SortExpression="valid" />                
            </Columns>
            <EditRowStyle BackColor="#2461BF" />            
        </asp:GridView>       
    </section>
</asp:Content>
