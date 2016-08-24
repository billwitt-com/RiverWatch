<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GearConfigNewRow.aspx.cs" Inherits="RWInbound2.GearConfigNewRow" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p class="site-title">
        GearConfig Add New Item</p>
    <p>
        <asp:FormView ID="FormView1" runat="server" DataKeyNames="ID" DataSourceID="SqlDataSource1">
            <EditItemTemplate>
                ID:
                <asp:Label ID="IDLabel1" runat="server" Text='<%# Eval("ID") %>' />
                <br />
                Code:
                <asp:TextBox ID="CodeTextBox" runat="server" Text='<%# Bind("Code") %>' />
                <br />
                Description:
                <asp:TextBox ID="DescriptionTextBox" runat="server" Text='<%# Bind("Description") %>' />
                <br />
                Type:
                <asp:TextBox ID="TypeTextBox" runat="server" Text='<%# Bind("Type") %>' />
                <br />
                FieldGearID:
                <asp:TextBox ID="FieldGearIDTextBox" runat="server" Text='<%# Bind("FieldGearID") %>' />
                <br />
                DateCreated:
                <asp:TextBox ID="DateCreatedTextBox" runat="server" Text='<%# Bind("DateCreated") %>' />
                <br />
                CreatedBy:
                <asp:TextBox ID="CreatedByTextBox" runat="server" Text='<%# Bind("CreatedBy") %>' />
                <br />
                Valid:
                <asp:CheckBox ID="ValidCheckBox" runat="server" Checked='<%# Bind("Valid") %>' />
                <br />
                <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
                &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
            </EditItemTemplate>
            <InsertItemTemplate>
                Code:
                <asp:TextBox ID="CodeTextBox" runat="server" Text='<%# Bind("Code") %>' />
                <br />
                Description:
                <asp:TextBox ID="DescriptionTextBox" runat="server" Text='<%# Bind("Description") %>' />
                <br />
                Type:
                <asp:TextBox ID="TypeTextBox" runat="server" Text='<%# Bind("Type") %>' />
                <br />
                FieldGearID:
                <asp:TextBox ID="FieldGearIDTextBox" runat="server" Text='<%# Bind("FieldGearID") %>' />
                <br />
<%--                DateCreated:
                <asp:TextBox ID="DateCreatedTextBox" runat="server" Text='<%# Bind("DateCreated") %>' />
                <br />
                CreatedBy:
                <asp:TextBox ID="CreatedByTextBox" runat="server" Text='<%# Bind("CreatedBy") %>' />
                <br />
                Valid:
                <asp:CheckBox ID="ValidCheckBox" runat="server" Checked='<%# Bind("Valid") %>' />
                <br />--%>
                <asp:LinkButton ID="InsertButton" OnClick="InsertButton_Click" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
                &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
            </InsertItemTemplate>
            <ItemTemplate>
                ID:
                <asp:Label ID="IDLabel" runat="server" Text='<%# Eval("ID") %>' />
                <br />
                Code:
                <asp:Label ID="CodeLabel" runat="server" Text='<%# Bind("Code") %>' />
                <br />
                Description:
                <asp:Label ID="DescriptionLabel" runat="server" Text='<%# Bind("Description") %>' />
                <br />
                Type:
                <asp:Label ID="TypeLabel" runat="server" Text='<%# Bind("Type") %>' />
                <br />
                FieldGearID:
                <asp:Label ID="FieldGearIDLabel" runat="server" Text='<%# Bind("FieldGearID") %>' />
                <br />
                DateCreated:
                <asp:Label ID="DateCreatedLabel" runat="server" Text='<%# Bind("DateCreated") %>' />
                <br />
                CreatedBy:
                <asp:Label ID="CreatedByLabel" runat="server" Text='<%# Bind("CreatedBy") %>' />
                <br />
                Valid:
                <asp:CheckBox ID="ValidCheckBox" runat="server" Checked='<%# Bind("Valid") %>' Enabled="false" />
                <br />
                <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit" />
                &nbsp;<asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" />
                &nbsp;<asp:LinkButton ID="NewButton"  OnClick="NewButton_Click" runat="server" CausesValidation="False" CommandName="New" Text="New" />
            </ItemTemplate>
        </asp:FormView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RiverWatchConnectionString %>" 
            OnUpdating="SqlDataSource1_Updating" 
             OnInserting="SqlDataSource1_Inserting"
             OnInserted="SqlDataSource1_Inserted"
            DeleteCommand="DELETE FROM [GearConfig] WHERE [ID] = @ID" 
            InsertCommand="INSERT INTO [GearConfig] ([Code], [Description], [Type], [FieldGearID], [DateCreated], [CreatedBy], [Valid]) VALUES (@Code, @Description, @Type, @FieldGearID, @DateCreated, @CreatedBy, @Valid)" 
            SelectCommand="SELECT * FROM [GearConfig]" 
            UpdateCommand="UPDATE [GearConfig] SET [Code] = @Code, [Description] = @Description, [Type] = @Type, [FieldGearID] = @FieldGearID, [DateCreated] = @DateCreated, [CreatedBy] = @CreatedBy, [Valid] = @Valid WHERE [ID] = @ID">
            <DeleteParameters>
                <asp:Parameter Name="ID" Type="Int32" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="Code" Type="Int32" />
                <asp:Parameter Name="Description" Type="String" />
                <asp:Parameter Name="Type" Type="String" />
                <asp:Parameter Name="FieldGearID" Type="String" />
                <asp:Parameter Name="DateCreated" Type="DateTime" />
                <asp:Parameter Name="CreatedBy" Type="String" />
                <asp:Parameter Name="Valid" Type="Boolean" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="Code" Type="Int32" />
                <asp:Parameter Name="Description" Type="String" />
                <asp:Parameter Name="Type" Type="String" />
                <asp:Parameter Name="FieldGearID" Type="String" />
                <asp:Parameter Name="DateCreated" Type="DateTime" />
                <asp:Parameter Name="CreatedBy" Type="String" />
                <asp:Parameter Name="Valid" Type="Boolean" />
                <asp:Parameter Name="ID" Type="Int32" />
            </UpdateParameters>
        </asp:SqlDataSource>
    </p>
</asp:Content>
