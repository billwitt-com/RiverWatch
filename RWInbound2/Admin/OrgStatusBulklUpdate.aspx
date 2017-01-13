<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeBehind="OrgStatusBulklUpdate.aspx.cs" Inherits="RWInbound2.Admin.OrgStatusBulklUpdate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

        <br />
        <asp:Label ID="Label1" CssClass="PageLabel" runat="server" Text="Org Status Bulk Update (click on column name to sort)"></asp:Label>
            <br />
                <br />
    <asp:GridView ID="GridView1"  runat="server"  AllowSorting="True" AutoGenerateColumns="False" s DataKeyNames="ID" DataSourceID="SqlDataSource1">
        <Columns>
            <asp:CommandField ShowEditButton="True" ButtonType="Button" />
            <asp:BoundField DataField="KitNumber" HeaderText="KitNumber" SortExpression="KitNumber" />
            <asp:BoundField DataField="OrganizationName" HeaderText="OrganizationName" SortExpression="OrganizationName" />
            <asp:CheckBoxField DataField="Active" HeaderText="Active" SortExpression="Active" />
            <asp:CheckBoxField DataField="Valid" HeaderText="Valid" SortExpression="Valid" />
            <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="ID" />
        </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RiverWatchDEV %>" 
            DeleteCommand="DELETE FROM [organization] WHERE [ID] = @ID" 
            InsertCommand="INSERT INTO [organization] ([KitNumber], [OrganizationName], [Active], [Valid]) VALUES (@KitNumber, @OrganizationName, @Active, @Valid)" 
            SelectCommand="SELECT [KitNumber], [OrganizationName], [Active], [Valid], [ID] FROM [organization]" 
            UpdateCommand="UPDATE [organization] SET [KitNumber] = @KitNumber, [OrganizationName] = @OrganizationName, [Active] = @Active, [Valid] = @Valid WHERE [ID] = @ID">
            <DeleteParameters>
                <asp:Parameter Name="ID" Type="Int32" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="KitNumber" Type="Int32" />
                <asp:Parameter Name="OrganizationName" Type="String" />
                <asp:Parameter Name="Active" Type="Boolean" />
                <asp:Parameter Name="Valid" Type="Boolean" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="KitNumber" Type="Int32" />
                <asp:Parameter Name="OrganizationName" Type="String" />
                <asp:Parameter Name="Active" Type="Boolean" />
                <asp:Parameter Name="Valid" Type="Boolean" />
                <asp:Parameter Name="ID" Type="Int32" />
            </UpdateParameters>
        </asp:SqlDataSource>
</asp:Content>
