<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditStationType.aspx.cs" Inherits="RWInbound2.Edit.EditStationType" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <p>
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <asp:FormView ID="FormView1" runat="server" DataSourceID="SqlDataSource1">
            <EditItemTemplate>
                Code:
                <asp:TextBox ID="CodeTextBox" runat="server" Text='<%# Bind("Code") %>' />
                <br />
                Description:
                <asp:TextBox ID="DescriptionTextBox" runat="server" Text='<%# Bind("Description") %>' />
                <br />
                DateLastModified:
                <asp:TextBox ID="DateLastModifiedTextBox" runat="server" Text='<%# Bind("DateLastModified") %>' />
                <br />
                UserLastModified:
                <asp:TextBox ID="UserLastModifiedTextBox" runat="server" Text='<%# Bind("UserLastModified") %>' />
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
                DateLastModified:
                <asp:TextBox ID="DateLastModifiedTextBox" runat="server" Text='<%# Bind("DateLastModified") %>' />
                <br />
                UserLastModified:
                <asp:TextBox ID="UserLastModifiedTextBox" runat="server" Text='<%# Bind("UserLastModified") %>' />
                <br />
                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
                &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
            </InsertItemTemplate>
            <ItemTemplate>
                Code:
                <asp:Label ID="CodeLabel" runat="server" Text='<%# Bind("Code") %>' />
                <br />
                Description:
                <asp:Label ID="DescriptionLabel" runat="server" Text='<%# Bind("Description") %>' />
                <br />
                DateLastModified:
                <asp:Label ID="DateLastModifiedLabel" runat="server" Text='<%# Bind("DateLastModified") %>' />
                <br />
                UserLastModified:
                <asp:Label ID="UserLastModifiedLabel" runat="server" Text='<%# Bind("UserLastModified") %>' />
                <br />

            </ItemTemplate>
        </asp:FormView>
        <asp:SqlDataSource ID="SqlDataSource1"   runat="server" ConnectionString="<%$ ConnectionStrings:RiverWatchDev %>" 
            SelectCommand="SELECT * FROM [tlkStationType]"></asp:SqlDataSource>
        <br />
    </p>


</asp:Content>
