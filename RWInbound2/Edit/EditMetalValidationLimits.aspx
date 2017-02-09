<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditMetalValidationLimits.aspx.cs" Inherits="RWInbound2.Edit.EditMetalValidationLimits" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
                <br />
    <asp:Label ID="Label1" CssClass="PageLabel" runat="server" Text="Edit Metals Validation Tables:"></asp:Label>
    <br />
            <asp:FormView ID="FormView1" class="formviewText" runat="server" AllowPaging="True" DataKeyNames="ID" DataSourceID="SqlDataSource1" CssClass="formviewText" Width="635px">
                <EditItemTemplate>
                    ID:
                    <asp:Label ID="IDLabel1" runat="server" Text='<%# Eval("ID") %>' />
                    <br />
                    Element:
                    <asp:TextBox ID="ElementTextBox" runat="server" Text='<%# Bind("Element") %>' />
                    <br />
                    Reporting:
                    <asp:TextBox ID="ReportingTextBox" runat="server" Text='<%# Bind("Reporting") %>' />
                    <br />
                    MDL:
                    <asp:TextBox ID="MDLTextBox" runat="server" Text='<%# Bind("MDL") %>' />
                    <br />
                    DvsTDifference:
                    <asp:TextBox ID="DvsTDifferenceTextBox" runat="server" Text='<%# Bind("DvsTDifference") %>' />
                    <br />
                    DateCreated:
                    <asp:TextBox ID="DateCreatedTextBox" runat="server" Text='<%# Bind("DateCreated") %>' />
                    <br />
                    UserCreated:
                    <asp:TextBox ID="UserCreatedTextBox" runat="server" Text='<%# Bind("UserCreated") %>' />
                    <br />
                    Valid:
                    <asp:CheckBox ID="ValidCheckBox" runat="server" Checked='<%# Bind("Valid") %>' />
                    <br />
                    <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
                    &nbsp;<asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
                </EditItemTemplate>
                <InsertItemTemplate>
                    Element:
                    <asp:TextBox ID="ElementTextBox" runat="server" Text='<%# Bind("Element") %>' />
                    <br />
                    Reporting:
                    <asp:TextBox ID="ReportingTextBox" runat="server" Text='<%# Bind("Reporting") %>' />
                    <br />
                    MDL:
                    <asp:TextBox ID="MDLTextBox" runat="server" Text='<%# Bind("MDL") %>' />
                    <br />
                    DvsTDifference:
                    <asp:TextBox ID="DvsTDifferenceTextBox" runat="server" Text='<%# Bind("DvsTDifference") %>' />
                    <br />
                    DateCreated:
                    <asp:TextBox ID="DateCreatedTextBox" runat="server" Text='<%# Bind("DateCreated") %>' />
                    <br />
                    UserCreated:
                    <asp:TextBox ID="UserCreatedTextBox" runat="server" Text='<%# Bind("UserCreated") %>' />
                    <br />
                    Valid:
                    <asp:CheckBox ID="ValidCheckBox" runat="server" Checked='<%# Bind("Valid") %>' />
                    <br />
                    <asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
                    &nbsp;<asp:Button ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
                </InsertItemTemplate>
                <ItemTemplate>
                    ID:
                    <asp:Label ID="IDLabel" runat="server" Text='<%# Eval("ID") %>' />
                    <br />
                    Element:
                    <asp:Label ID="ElementLabel" runat="server" Text='<%# Bind("Element") %>' />
                    <br />
                    Reporting:
                    <asp:Label ID="ReportingLabel" runat="server" Text='<%# Bind("Reporting") %>' />
                    <br />
                    MDL:
                    <asp:Label ID="MDLLabel" runat="server" Text='<%# Bind("MDL") %>' />
                    <br />
                    DvsTDifference:
                    <asp:Label ID="DvsTDifferenceLabel" runat="server" Text='<%# Bind("DvsTDifference") %>' />
                    <br />
                    DateCreated:
                    <asp:Label ID="DateCreatedLabel" runat="server" Text='<%# Bind("DateCreated") %>' />
                    <br />
                    UserCreated:
                    <asp:Label ID="UserCreatedLabel" runat="server" Text='<%# Bind("UserCreated") %>' />
                    <br />
                    Valid:
                    <asp:CheckBox ID="ValidCheckBox" runat="server" Checked='<%# Bind("Valid") %>' Enabled="false" />
                    <br />
                    <asp:Button ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit" />
                    &nbsp;<asp:Button ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" />
                    &nbsp;<asp:Button ID="NewButton" runat="server" CausesValidation="False" CommandName="New" Text="New" />
                </ItemTemplate>
            </asp:FormView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:RiverWatchDEV %>" 
                 OnDeleting="SqlDataSource1_Deleting" OnUpdating="SqlDataSource1_Updating" OnInserting="SqlDataSource1_Inserting"
                DeleteCommand="UPDATE [tlkLimits] SET [Valid] = 0 WHERE [ID] = @ID" 
                InsertCommand="INSERT INTO [tlkLimits] ([Element], [Reporting], [MDL], [DvsTDifference], [DateCreated], [UserCreated], [Valid]) VALUES (@Element, @Reporting, @MDL, @DvsTDifference, @DateCreated, @UserCreated, @Valid)" 
                SelectCommand="SELECT [ID], [Element], [Reporting], [MDL], [DvsTDifference], [DateCreated], [UserCreated], [Valid] FROM [tlkLimits]" 
                UpdateCommand="UPDATE [tlkLimits] SET [Element] = @Element, [Reporting] = @Reporting, [MDL] = @MDL, [DvsTDifference] = @DvsTDifference, [DateCreated] = @DateCreated, [UserCreated] = @UserCreated, [Valid] = @Valid WHERE [ID] = @ID"
                >
                <DeleteParameters>
                    <asp:Parameter Name="Element" Type="String" />
                    <asp:Parameter Name="Reporting" Type="Decimal" />
                    <asp:Parameter Name="MDL" Type="Decimal" />
                    <asp:Parameter Name="DvsTDifference" Type="Decimal" />
                    <asp:Parameter DbType="Date" Name="DateCreated" />
                    <asp:Parameter Name="UserCreated" Type="String" />
                    <asp:Parameter Name="Valid" Type="Boolean" />
                    <asp:Parameter Name="ID" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="Element" Type="String" />
                    <asp:Parameter Name="Reporting" Type="Decimal" />
                    <asp:Parameter Name="MDL" Type="Decimal" />
                    <asp:Parameter Name="DvsTDifference" Type="Decimal" />
                    <asp:Parameter DbType="Date" Name="DateCreated" />
                    <asp:Parameter Name="UserCreated" Type="String" />
                    <asp:Parameter Name="Valid" Type="Boolean" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Element" Type="String" />
                    <asp:Parameter Name="Reporting" Type="Decimal" />
                    <asp:Parameter Name="MDL" Type="Decimal" />
                    <asp:Parameter Name="DvsTDifference" Type="Decimal" />
                    <asp:Parameter DbType="Date" Name="DateCreated" />
                    <asp:Parameter Name="UserCreated" Type="String" />
                    <asp:Parameter Name="Valid" Type="Boolean" />
                    <asp:Parameter Name="ID" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>




            
            </asp:Content>
