<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditLimits.aspx.cs" Inherits="RWInbound2.Edit.EditLimits" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Label runat="server" Text="Validation Limits Editor   "></asp:Label>
    <asp:Label ID="lblWelcomeName" runat="server" ></asp:Label>
    <asp:FormView ID="FormView1" runat="server" DataKeyNames="ID" DataSourceID="SqlDataSource1"  AllowPaging=" true">       

        <EditItemTemplate>
     <%--       ID:
            <asp:Label ID="IDLabel1" runat="server" Text='<%# Eval("ID") %>' />
            <br />--%>
            Name:
            <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' />
            <br />
            Value:
            <asp:TextBox ID="ValueTextBox" runat="server" Text='<%# Bind("Value") %>' />
            <br />
            Note:
            <asp:TextBox ID="NoteTextBox" runat="server" Text='<%# Bind("Note") %>' />
            <br />
           <%-- CreatedBy:
            <asp:TextBox ID="CreatedByTextBox" runat="server" Text='<%# Bind("CreatedBy") %>' />
            <br />
            CreatedDate:
            <asp:TextBox ID="CreatedDateTextBox" runat="server" Text='<%# Bind("CreatedDate") %>' />
            <br />
            Valid:
            <asp:TextBox ID="ValidTextBox" runat="server" Text='<%# Bind("Valid") %>' />--%>
            <br />
            <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
            &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
        </EditItemTemplate>
        <InsertItemTemplate>
            Name:
            <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' />
            <br />
            Value:
            <asp:TextBox ID="ValueTextBox" runat="server" Text='<%# Bind("Value") %>' />
            <br />
            Note:
            <asp:TextBox ID="NoteTextBox" runat="server" Text='<%# Bind("Note") %>' />
            <br />
           <%-- CreatedBy:
            <asp:TextBox ID="CreatedByTextBox" runat="server" Text='<%# Bind("CreatedBy") %>' />
            <br />
            CreatedDate:
            <asp:TextBox ID="CreatedDateTextBox" runat="server" Text='<%# Bind("CreatedDate") %>' />
            <br />
            Valid:
            <asp:TextBox ID="ValidTextBox" runat="server" Text='<%# Bind("Valid") %>' />--%>
            <br />
            <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
            &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
        </InsertItemTemplate>
        <ItemTemplate>
<%--            ID:
            <asp:Label ID="IDLabel" runat="server" Text='<%# Eval("ID") %>' />
            <br />--%>
            Name:
            <asp:Label ID="NameLabel" runat="server" Text='<%# Bind("Name") %>' />
            <br />
            Value:
            <asp:Label ID="ValueLabel" runat="server" Text='<%# Bind("Value") %>' />
            <br />
            Note:
            <asp:Label ID="NoteLabel" runat="server" Text='<%# Bind("Note") %>' />
            <br />
            CreatedBy:
            <asp:Label ID="CreatedByLabel" runat="server" Text='<%# Bind("CreatedBy") %>' />
            <br />
            CreatedDate:
            <asp:Label ID="CreatedDateLabel" runat="server" Text='<%# Bind("CreatedDate") %>' />
            <br />
            Valid:
            <asp:Label ID="ValidLabel" runat="server" Text='<%# Bind("Valid") %>' />
            <br />
            <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit" />
            &nbsp;<asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" />
            &nbsp;<asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" CommandName="New" Text="New" />
        </ItemTemplate>
    </asp:FormView>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RiverWatchDev%>"
        OnUpdating="SqlDataSource1_Updating"
        OnInserting="SqlDataSource1_Inserting"
        OnDeleting="SqlDataSource1_Deleting"
      
        InsertCommand="INSERT INTO [tlkLimits] ([Name], [Value], [Note], [CreatedBy], [CreatedDate], [Valid]) VALUES (@Name, @Value, @Note, @CreatedBy, @CreatedDate, @Valid)"
        UpdateCommand="UPDATE [tlkLimits] SET [Valid] = 0 WHERE [ID] = @ID  INSERT INTO [tlkLimits] ([Name], [Value], [Note], [CreatedBy], [CreatedDate], [Valid]) VALUES (@Name, @Value, @Note, @CreatedBy, @CreatedDate, @Valid) "
        SelectCommand="SELECT * FROM [tlkLimits] where [valid] = 1 order by name"
        DeleteCommand="Delete from [tlkLimits] where [ID] = @ID"
        >   
        
        <DeleteParameters>
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="Value" Type="Double" />
            <asp:Parameter Name="Note" Type="String" />
            <asp:Parameter Name="CreatedBy" Type="String" />
            <asp:Parameter Name="CreatedDate" Type="DateTime" />
            <asp:Parameter Name="Valid" Type="Int32" />
            <asp:Parameter Name="ID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="Value" Type="Double" />
            <asp:Parameter Name="Note" Type="String" />
            <asp:Parameter Name="CreatedBy" Type="String" />
            <asp:Parameter Name="CreatedDate" Type="DateTime" />
            <asp:Parameter Name="Valid" Type="Int32" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="Value" Type="Double" />
            <asp:Parameter Name="Note" Type="String" />
            <asp:Parameter Name="CreatedBy" Type="String" />
            <asp:Parameter Name="CreatedDate" Type="DateTime" />
            <asp:Parameter Name="Valid" Type="Int32" />
            <asp:Parameter Name="ID" Type="Int32" />
        </UpdateParameters>
      </asp:SqlDataSource>
</asp:Content>
