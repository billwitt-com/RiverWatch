<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GearConfig.aspx.cs" Inherits="RWInbound2.GearConfig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   <span class="site-title">Configure Field Gear

   </span> 
    <br />
    <br />
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="SqlDataSource1" CellPadding="4" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:CommandField NewText="New" ShowDeleteButton="True" ShowEditButton="True" ButtonType="Button" />
            <asp:BoundField DataField="ID" HeaderText="ID"  InsertVisible="False" ReadOnly="True" SortExpression="ID" />
            <asp:BoundField DataField="Code" HeaderText="Code" SortExpression="Code" />
            <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
            <asp:BoundField DataField="Type" HeaderText="Type" SortExpression="Type" />
            <asp:BoundField DataField="FieldGearID" HeaderText="FieldGearID" SortExpression="FieldGearID" />
            <asp:BoundField DataField="DateCreated" HeaderText="DateCreated" SortExpression="DateCreated" />
            <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" SortExpression="CreatedBy" />
            <asp:CheckBoxField DataField="Valid" HeaderText="Valid"  ReadOnly="true" SortExpression="Valid" />            
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
    </asp:GridView>
    <asp:Button ID="btnNew" runat="server" OnClick="btnNew_Click" Text="NEW" />
    <br />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RiverwatchDEV %>" 
         OnUpdating="SqlDataSource1_Updating"

        DeleteCommand="DELETE FROM [GearConfig] WHERE [ID] = @ID" 
        InsertCommand="INSERT INTO [GearConfig] ([Code], [Description], [Type], [FieldGearID], [DateCreated], [CreatedBy], [Valid]) VALUES (@Code, @Description, @Type, @FieldGearID, @DateCreated, @CreatedBy, @Valid)" 
        SelectCommand="SELECT * FROM [GearConfig] where [Valid] = 1" 
        UpdateCommand="UPDATE [GearConfig] set [Description] = @Description, [Type] = @Type, [FieldGearID] = @FieldGearID, [DateCreated] = @DateCreated, [CreatedBy] = @CreatedBy, [Valid] = @Valid WHERE [ID] = @ID">
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
            <asp:Parameter Name="ID" Type="Int32" />
            <asp:Parameter Name="Description" Type="String" />
            <asp:Parameter Name="Type" Type="String" />
            <asp:Parameter Name="FieldGearID" Type="String" />
            <asp:Parameter Name="DateCreated" Type="DateTime" />
            <asp:Parameter Name="CreatedBy" Type="String" />
            <asp:Parameter Name="Valid" Type="Boolean" />
            <asp:Parameter Name="Code" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <br />
</asp:Content>
