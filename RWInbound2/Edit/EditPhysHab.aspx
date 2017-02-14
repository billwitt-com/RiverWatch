<%@ Page Title="Edit Phys Hab" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditPhysHab.aspx.cs" Inherits="RWInbound2.Edit.EditPhysHab" %>
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
                Search By SampleID:
                <asp:TextBox ID="sampleIDSearch" 
                    onkeydown="return (event.keyCode!=13);"
                    runat="server"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="Select" Height="31px" OnClick="btnSearch_Click" CausesValidation="False" CssClass="adminButton" />
                <asp:Button ID="btnSearchRefresh" runat="server" Text="Reset Search" Height="31px" OnClick="btnSearchRefresh_Click" CausesValidation="False" CssClass="adminButton"/>
                    <ajaxToolkit:AutoCompleteExtender 
                        ID="tbSearch_AutoCompleteExtender" 
                        runat="server" 
                        BehaviorID="tbSearch_AutoCompleteExtender" 
                        DelimiterCharacters=""  
                        ServiceMethod="SearchForSampleID"             
                        TargetControlID="sampleIDSearch"
                        MinimumPrefixLength="2"
                        CompletionInterval="100" 
                        EnableCaching="true" 
                        CompletionSetCount="10"
                        UseContextKey="True">
                    </ajaxToolkit:AutoCompleteExtender> 
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>        
        <asp:GridView ID="PhysHabGridView" runat="server"
            DataKeyNames="ID"
            ItemType="RWInbound2.tblPhysHab" 
            SelectMethod="GetPhysHabs"
            UpdateMethod="UpdatePhysHab"
            DeleteMethod="DeletePhysHab" 
            InsertItemPosition="LastItem"  
            ShowFooter="true"
            CellPadding="4"
            AutoGenerateColumns="False" CssClass="grid-columns-center grid-view"
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
                                    OnClick = "AddNewPhysHab" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ID" HeaderText="ID" Visible="false" ReadOnly="True" SortExpression="ID" />
                <asp:TemplateField HeaderText="RepNum" SortExpression="RepNum">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtRepNum" TextMode="Number" runat="server" Text='<%# Bind("RepNum") %>'></asp:TextBox>
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
                <asp:TemplateField HeaderText="ParaID" SortExpression="ParaID">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtParaID" TextMode="Number" runat="server" Text='<%# Bind("ParaID") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblParaID" runat="server" Text='<%# Bind("ParaID") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="NewParaID" TextMode="Number" runat="server"></asp:TextBox>
                        <asp:Label ID="lblNewParaIDRequired" runat="server" Visible="false" CssClass="edit-inboundicp-required">
                            Required!
                        </asp:Label>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Value" SortExpression="Value">
                    <EditItemTemplate>
                        <asp:TextBox ID="txValue" runat="server" Text='<%# Bind("Value") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator 
                                        id="RequiredFieldValidator" runat="server" 
                                        CssClass="edit-inboundicp-required"
                                        Display="Dynamic"
                                        ErrorMessage="Required!" 
                                        ControlToValidate="txValue">
                        </asp:RequiredFieldValidator>
                        <asp:CompareValidator ControlToValidate="txValue" Operator="DataTypeCheck" 
                                        ID="CompareValidator1" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                        Display="Dynamic" ErrorMessage="Please enter a valid decimal value." CssClass="">
                        </asp:CompareValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblValue" runat="server" Text='<%# Bind("Value") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="NewValue" runat="server"></asp:TextBox>
                        <asp:CompareValidator ControlToValidate="NewValue" Operator="DataTypeCheck" 
                                        ID="CompareValidator2" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                        Display="Dynamic" ErrorMessage="Please enter a valid decimal value." CssClass="">
                        </asp:CompareValidator>                        
                    </FooterTemplate>
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="Enter Date" SortExpression="EnterDate">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEnterDate" runat="server" Text='<%# Bind("EnterDate") %>'></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="txtEnterDate_CalendarExtender" runat="server" 
                                                BehaviorID="txtEnterDate_CalendarExtender" 
                                                TargetControlID="txtEnterDate"></ajaxToolkit:CalendarExtender>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblEnterDate" runat="server" Text='<%# Bind("EnterDate") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="NewEnterDate" runat="server"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="NewEnterDate_CalendarExtender" runat="server" 
                                                BehaviorID="NewEnterDate_CalendarExtender" 
                                                TargetControlID="NewEnterDate"></ajaxToolkit:CalendarExtender>
                    </FooterTemplate>
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Comment" SortExpression="Comment">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtComment" MaxLength="50" runat="server" TextMode="MultiLine" CssClass="grid-edit-physhab-comment" 
                                    Text='<%# Bind("Comment") %>'></asp:TextBox>                        
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblComment" runat="server" Text='<%# Bind("Comment") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="NewComment" MaxLength="50" TextMode="MultiLine" runat="server" 
                                     CssClass="grid-edit-physhab-comment"></asp:TextBox>                       
                    </FooterTemplate>
                </asp:TemplateField>                
                <asp:TemplateField HeaderText="SampleID" SortExpression="SampleID">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtSampleID" TextMode="Number" runat="server" Text='<%# Bind("SampleID") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblSampleID" runat="server" Text='<%# Bind("SampleID") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="NewSampleID" TextMode="Number" runat="server"></asp:TextBox>
                        <asp:Label ID="lblNewSampleIDRequired" runat="server" Visible="false" CssClass="edit-inboundicp-required">
                            Required!
                        </asp:Label>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Storet Uploaded" SortExpression="StoretUploaded">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtStoretUploaded" MaxLength="25" runat="server" Text='<%# Bind("StoretUploaded") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator 
                                        id="RequiredFieldValidator2" runat="server" 
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
