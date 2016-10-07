<%@ Page Title="Nutrient Limits" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminNutrientLimits.aspx.cs" Inherits="RWInbound2.Admin.AdminNutrientLimits" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" ClientIDMode="Static" runat="server">
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
                    Search By Element:
                    <asp:TextBox ID="elementSearch" 
                        AutoPostBack="true"
                        runat="server"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" Height="31px" OnClick="btnSearch_Click" />
                    <asp:Button ID="btnSearchRefresh" runat="server" Text="Reset Search" Height="31px" OnClick="btnSearchRefresh_Click" />

                    <ajaxToolkit:AutoCompleteExtender 
                        ID="tbSearch_AutoCompleteExtender" 
                        runat="server" 
                        BehaviorID="tbSearch_AutoCompleteExtender" 
                        DelimiterCharacters=""  
                        ServiceMethod="SearchForElement"             
                        TargetControlID="elementSearch"
                        MinimumPrefixLength="2"
                        CompletionInterval="100" 
                        EnableCaching="false" 
                        CompletionSetCount="10">
                    </ajaxToolkit:AutoCompleteExtender>
            </ContentTemplate>
        </asp:UpdatePanel>         
    </p>
        <asp:GridView ID="NutrientLimitsGridView" runat="server"
            DataKeyNames="ID"
            ItemType="RWInbound2.NutrientLimit" 
            SelectMethod="GetNutrientLimits"
            UpdateMethod="UpdateNutrientLimit"
            DeleteMethod="DeleteNutrientLimit" 
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
                                    OnClick = "AddNutrientLimit" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ID" HeaderText="ID" Visible="false" ReadOnly="True" SortExpression="ID" />
                <asp:TemplateField HeaderText="Element" SortExpression="Element">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtElement" runat="server" Text='<%# Bind("Element") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblElement" runat="server" Text='<%# Bind("Element") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="NewElement" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Row ID" SortExpression="RowID">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtRowID" runat="server" Text='<%# Bind("RowID") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblRowIDt" runat="server" Text='<%# Bind("RowID") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="NewRowID" runat="server"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Reporting" SortExpression="Reporting">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtReporting" runat="server" Text='<%# Bind("Reporting") %>'></asp:TextBox>
                        <asp:CompareValidator ControlToValidate="txtReporting" Operator="DataTypeCheck" 
                                              ID="txtReportingValidator" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                              Display="Dynamic" ErrorMessage="Please enter a valid reporting value.">

                        </asp:CompareValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblReporting" runat="server" Text='<%# Bind("Reporting") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="NewReporting" runat="server"></asp:TextBox>
                        <asp:CompareValidator ControlToValidate="NewReporting" Operator="DataTypeCheck" 
                                              ID="NewReportingValidator" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                              Display="Dynamic" ErrorMessage="Please enter a valid reporting value."> 
                        </asp:CompareValidator>
                    </FooterTemplate>
                </asp:TemplateField>    
                <asp:TemplateField HeaderText="MDL" SortExpression="MDL">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtMDL" runat="server" Text='<%# Bind("MDL") %>'></asp:TextBox>
                        <asp:CompareValidator ControlToValidate="txtMDL" Operator="DataTypeCheck" 
                                              ID="txtMDLValidator" runat="server" Type="Double" ForeColor="Red" Font-Bold="true" 
                                              Display="Dynamic" ErrorMessage="Please enter a valid MDL value.">

                        </asp:CompareValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblMDL" runat="server" Text='<%# Bind("MDL") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="NewMDL" runat="server"></asp:TextBox>
                        <asp:CompareValidator ControlToValidate="NewMDL" Operator="DataTypeCheck" 
                                              ID="NewMDLValidator" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                              Display="Dynamic" ErrorMessage="Please enter a valid MDL value."> 
                        </asp:CompareValidator>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DvsTDifference" SortExpression="DvsTDifference">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtDvsTDifference" runat="server" Text='<%# Bind("DvsTDifference") %>'></asp:TextBox>
                        <asp:CompareValidator ControlToValidate="txtDvsTDifference" Operator="DataTypeCheck" 
                                              ID="txtDvsTDifferenceValidator" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                              Display="Dynamic" ErrorMessage="Please enter a valid DvsTDifference value.">

                        </asp:CompareValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblDvsTDifference" runat="server" Text='<%# Bind("DvsTDifference") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="NewDvsTDifference" runat="server"></asp:TextBox>
                        <asp:CompareValidator ControlToValidate="NewDvsTDifference" Operator="DataTypeCheck" 
                                              ID="NewDvsTDifferenceValidator" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                              Display="Dynamic" ErrorMessage="Please enter a valid DvsTDifference value."> 
                        </asp:CompareValidator>
                    </FooterTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="High Limit" SortExpression="HighLimit">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtHighLimit" runat="server" Text='<%# Bind("HighLimit") %>'></asp:TextBox>
                        <asp:CompareValidator ControlToValidate="txtHighLimit" Operator="DataTypeCheck" 
                                              ID="txtHighLimitValidator" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                              Display="Dynamic" ErrorMessage="Please enter a valid High Limit value.">

                        </asp:CompareValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblHighLimit" runat="server" Text='<%# Bind("HighLimit") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="NewHighLimit" runat="server"></asp:TextBox>
                        <asp:CompareValidator ControlToValidate="NewHighLimit" Operator="DataTypeCheck" 
                                              ID="NewHighLimitValidator" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                              Display="Dynamic" ErrorMessage="Please enter a valid High Limit value."> 
                        </asp:CompareValidator>
                    </FooterTemplate>
                </asp:TemplateField>                                 
            </Columns>
            <EditRowStyle BackColor="Yellow" />            
        </asp:GridView>       
    </section>
</asp:Content>
