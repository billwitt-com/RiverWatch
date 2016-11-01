<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminUsers.aspx.cs" Inherits="RWInbound2.Admin.AdminUsers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label1" runat="server"  CssClass="PageLabel" Text="Manage Users"></asp:Label>
<%--    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>--%>
    <br />
    <br />
    <asp:FormView ID="FormView1" runat="server" AllowPaging="True" CellPadding="4" DataKeyNames="ID" DataSourceID="SqlDataSource1" ForeColor="#333333">
        <EditItemTemplate>
            UserName:
            <asp:TextBox ID="UserNameTextBox" runat="server" Text='<%# Bind("UserName") %>' />
            <br />
            Password:
            <asp:TextBox ID="PasswordTextBox" runat="server" Text='<%# Bind("Password") %>' />
            <br />
            Email:
            <asp:TextBox ID="EmailTextBox" runat="server" Text='<%# Bind("Email") %>' />
            <br />
            FirstName:
            <asp:TextBox ID="FirstNameTextBox" runat="server" Text='<%# Bind("FirstName") %>' />
            <br />
            LastName:
            <asp:TextBox ID="LastNameTextBox" runat="server" Text='<%# Bind("LastName") %>' />
            <br />
            Role:
            <asp:TextBox ID="RoleTextBox" runat="server" Text='<%# Bind("Role") %>' />
            <br />
            DateLastActivity:
            <asp:TextBox ID="DateLastActivityTextBox" runat="server" Text='<%# Bind("DateLastActivity") %>' />
            <br />
            Valid:
            <asp:CheckBox ID="ValidCheckBox" runat="server" Checked='<%# Bind("Valid") %>' />
            <br />
            ID:
            <asp:Label ID="IDLabel1" runat="server" Text='<%# Eval("ID") %>' />
            <br />
            <asp:Button ID="UpdateButton" runat="server" CausesValidation="True"  CssClass="adminButton" CommandName="Update" Text="Update" />
            &nbsp;
            <asp:Button ID="UpdateCancelButton" runat="server" CssClass="adminButton" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
        </EditItemTemplate>
<%--        <EditRowStyle BackColor="#999999" />--%>
<%--        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />--%>
<%--        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />--%>
        <InsertItemTemplate>
            UserName:
            <asp:TextBox ID="UserNameTextBox" runat="server" Text='<%# Bind("UserName") %>' />
            <br />
            Password:
            <asp:TextBox ID="PasswordTextBox" runat="server" Text='<%# Bind("Password") %>' />
            <br />
            Email:
            <asp:TextBox ID="EmailTextBox" runat="server" Text='<%# Bind("Email") %>' />
            <br />
            FirstName:
            <asp:TextBox ID="FirstNameTextBox" runat="server" Text='<%# Bind("FirstName") %>' />
            <br />
            LastName:
            <asp:TextBox ID="LastNameTextBox" runat="server" Text='<%# Bind("LastName") %>' />
            <br />
            Role:
            <asp:TextBox ID="RoleTextBox" runat="server" Text='<%# Bind("Role") %>' />
            <br />
            DateLastActivity:
            <asp:TextBox ID="DateLastActivityTextBox" runat="server" Text='<%# Bind("DateLastActivity") %>' />
            <br />
            Valid:
            <asp:CheckBox ID="ValidCheckBox" runat="server" Checked='<%# Bind("Valid") %>' />
            <br />

            <asp:Button ID="InsertButton" runat="server" CssClass="adminButton" CausesValidation="True" CommandName="Insert" Text="Insert" />
            &nbsp;
            <asp:Button ID="InsertCancelButton" runat="server" CssClass="adminButton" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
        </InsertItemTemplate>
        <ItemTemplate>
            UserName:
            <asp:Label ID="UserNameLabel" runat="server" Text='<%# Bind("UserName") %>' />
            <br />
            Password:
            <asp:Label ID="PasswordLabel" runat="server" Text='<%# Bind("Password") %>' />
            <br />
            Email:
            <asp:Label ID="EmailLabel" runat="server" Text='<%# Bind("Email") %>' />
            <br />
            FirstName:
            <asp:Label ID="FirstNameLabel" runat="server" Text='<%# Bind("FirstName") %>' />
            <br />
            LastName:
            <asp:Label ID="LastNameLabel" runat="server" Text='<%# Bind("LastName") %>' />
            <br />
            Role:
            <asp:Label ID="RoleLabel" runat="server" Text='<%# Bind("Role") %>' />
            <br />
            DateLastActivity:
            <asp:Label ID="DateLastActivityLabel" runat="server" Text='<%# Bind("DateLastActivity") %>' />
            <br />
            Valid:
            <asp:CheckBox ID="ValidCheckBox" runat="server" Checked='<%# Bind("Valid") %>' Enabled="false" />
            <br />
            ID:
            <asp:Label ID="IDLabel" runat="server" Text='<%# Eval("ID") %>' />
            <br />
            <asp:Button ID="EditButton" runat="server" CausesValidation="False" CssClass="adminButton" CommandName="Edit" Text="Edit" />
            &nbsp;<asp:Button ID="DeleteButton" CssClass="adminButton" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" />
            &nbsp;<asp:Button ID="NewButton" CssClass="adminButton" runat="server" CausesValidation="False" CommandName="New" Text="New" />
        </ItemTemplate>
<%--        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />--%>
<%--        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />--%>
    </asp:FormView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RiverWatchDEV %>" DeleteCommand="DELETE FROM [tblUser] WHERE [ID] = @ID" InsertCommand="INSERT INTO [tblUser] ([UserName], [Password], [Email], [FirstName], [LastName], [Role], [DateLastActivity], [Valid]) VALUES (@UserName, @Password, @Email, @FirstName, @LastName, @Role, @DateLastActivity, @Valid)" SelectCommand="SELECT * FROM [tblUser]" UpdateCommand="UPDATE [tblUser] SET [UserName] = @UserName, [Password] = @Password, [Email] = @Email, [FirstName] = @FirstName, [LastName] = @LastName, [Role] = @Role, [DateLastActivity] = @DateLastActivity, [Valid] = @Valid WHERE [ID] = @ID">
        <DeleteParameters>
            <asp:Parameter Name="ID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="UserName" Type="String" />
            <asp:Parameter Name="Password" Type="String" />
            <asp:Parameter Name="Email" Type="String" />
            <asp:Parameter Name="FirstName" Type="String" />
            <asp:Parameter Name="LastName" Type="String" />
            <asp:Parameter Name="Role" Type="Int32" />
            <asp:Parameter Name="DateLastActivity" Type="DateTime" />
            <asp:Parameter Name="Valid" Type="Boolean" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="UserName" Type="String" />
            <asp:Parameter Name="Password" Type="String" />
            <asp:Parameter Name="Email" Type="String" />
            <asp:Parameter Name="FirstName" Type="String" />
            <asp:Parameter Name="LastName" Type="String" />
            <asp:Parameter Name="Role" Type="Int32" />
            <asp:Parameter Name="DateLastActivity" Type="DateTime" />
            <asp:Parameter Name="Valid" Type="Boolean" />
            <asp:Parameter Name="ID" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <br />
    <br />

</asp:Content>
