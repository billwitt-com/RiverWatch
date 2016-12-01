<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OLDEditWaterCodes.aspx.cs" Inherits="RWInbound2.Admin.EditWaterCodes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 
    <asp:Label ID="Label1" runat="server"  CssClass="PageLabel" Text="Edit Water Codes"></asp:Label>
    <br />
    
    <asp:RadioButtonList ID="RadioButtonList1" runat="server" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" AutoPostBack="True">
        <asp:ListItem Selected="True">Sort By Water Name</asp:ListItem>
        <asp:ListItem>Sort By Water Code</asp:ListItem>
                </asp:RadioButtonList>

    <br />
    <asp:GridView ID="GridView1" runat="server"  AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="SqlDataSource1">
        <Columns>
            <asp:CommandField ShowInsertButton="true" ShowDeleteButton="True" ShowEditButton="True" />
            <asp:BoundField DataField="WATERCODE" HeaderText="WATERCODE" SortExpression="WATERCODE" />
            <asp:BoundField DataField="WATERNAME" HeaderText="WATERNAME" SortExpression="WATERNAME" />
            <asp:CheckBoxField DataField="STREAM" HeaderText="STREAM" SortExpression="STREAM" />
            <asp:BoundField DataField="ACRESMILES" HeaderText="ACRESMILES" SortExpression="ACRESMILES" />
            <asp:BoundField DataField="LOCATION" HeaderText="LOCATION" SortExpression="LOCATION" />
            <asp:BoundField DataField="DRAINAGE" HeaderText="DRAINAGE" SortExpression="DRAINAGE" />
            <asp:BoundField DataField="DRAINNAME" HeaderText="DRAINNAME" SortExpression="DRAINNAME" />
            <asp:BoundField DataField="HYDROCODE" HeaderText="HYDROCODE" SortExpression="HYDROCODE" />
            <asp:BoundField DataField="COUNTY" HeaderText="COUNTY" SortExpression="COUNTY" />
            <asp:BoundField DataField="REGION" HeaderText="REGION" SortExpression="REGION" />
            <asp:BoundField DataField="AWMAREA" HeaderText="AWMAREA" SortExpression="AWMAREA" />
            <asp:BoundField DataField="STKCODE" HeaderText="STKCODE" SortExpression="STKCODE" />
            <asp:BoundField DataField="BIOLOGIST" HeaderText="BIOLOGIST" SortExpression="BIOLOGIST" />
            <asp:BoundField DataField="AREABIO" HeaderText="AREABIO" SortExpression="AREABIO" />
            <asp:BoundField DataField="ADDLDATA" HeaderText="ADDLDATA" SortExpression="ADDLDATA" />
            <asp:BoundField DataField="MAPCOORDIN" HeaderText="MAPCOORDIN" SortExpression="MAPCOORDIN" />
            <asp:BoundField DataField="ATLASPAGE" HeaderText="ATLASPAGE" SortExpression="ATLASPAGE" />
            <asp:BoundField DataField="CATEGORY" HeaderText="CATEGORY" SortExpression="CATEGORY" />
            <asp:BoundField DataField="ELEVATION" HeaderText="ELEVATION" SortExpression="ELEVATION" />
            <asp:BoundField DataField="UT_ELEVATI" HeaderText="UT_ELEVATI" SortExpression="UT_ELEVATI" />
            <asp:BoundField DataField="OWNERSHIP" HeaderText="OWNERSHIP" SortExpression="OWNERSHIP" />
            <asp:CheckBoxField DataField="WILDERNESS" HeaderText="WILDERNESS" SortExpression="WILDERNESS" />
            <asp:BoundField DataField="BLMMAP" HeaderText="BLMMAP" SortExpression="BLMMAP" />
            <asp:BoundField DataField="TOPOMAP" HeaderText="TOPOMAP" SortExpression="TOPOMAP" />
            <asp:CheckBoxField DataField="OBSOLETE" HeaderText="OBSOLETE" SortExpression="OBSOLETE" />
            <asp:CheckBoxField DataField="Valid" HeaderText="Valid" SortExpression="Valid" />
            <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="ID" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RiverwatchDEV %>" 
        DeleteCommand="DELETE FROM [tblWatercodes] WHERE [ID] = @ID" 
        InsertCommand="INSERT INTO [tblWatercodes] ([WATERCODE], [WATERNAME], [STREAM], [ACRESMILES], [LOCATION], [DRAINAGE], [DRAINNAME], [HYDROCODE], [COUNTY], [REGION], [AWMAREA], [STKCODE], [BIOLOGIST], [AREABIO], [ADDLDATA], [MAPCOORDIN], [ATLASPAGE], [CATEGORY], [ELEVATION], [UT_ELEVATI], [OWNERSHIP], [WILDERNESS], [BLMMAP], [TOPOMAP], [OBSOLETE], [Valid]) VALUES (@WATERCODE, @WATERNAME, @STREAM, @ACRESMILES, @LOCATION, @DRAINAGE, @DRAINNAME, @HYDROCODE, @COUNTY, @REGION, @AWMAREA, @STKCODE, @BIOLOGIST, @AREABIO, @ADDLDATA, @MAPCOORDIN, @ATLASPAGE, @CATEGORY, @ELEVATION, @UT_ELEVATI, @OWNERSHIP, @WILDERNESS, @BLMMAP, @TOPOMAP, @OBSOLETE, @Valid)" 
 
        UpdateCommand="UPDATE [tblWatercodes] SET [WATERCODE] = @WATERCODE, [WATERNAME] = @WATERNAME, [STREAM] = @STREAM, [ACRESMILES] = @ACRESMILES, [LOCATION] = @LOCATION, [DRAINAGE] = @DRAINAGE, [DRAINNAME] = @DRAINNAME, [HYDROCODE] = @HYDROCODE, [COUNTY] = @COUNTY, [REGION] = @REGION, [AWMAREA] = @AWMAREA, [STKCODE] = @STKCODE, [BIOLOGIST] = @BIOLOGIST, [AREABIO] = @AREABIO, [ADDLDATA] = @ADDLDATA, [MAPCOORDIN] = @MAPCOORDIN, [ATLASPAGE] = @ATLASPAGE, [CATEGORY] = @CATEGORY, [ELEVATION] = @ELEVATION, [UT_ELEVATI] = @UT_ELEVATI, [OWNERSHIP] = @OWNERSHIP, [WILDERNESS] = @WILDERNESS, [BLMMAP] = @BLMMAP, [TOPOMAP] = @TOPOMAP, [OBSOLETE] = @OBSOLETE, [Valid] = @Valid WHERE [ID] = @ID">
        <DeleteParameters>
            <asp:Parameter Name="ID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="WATERCODE" Type="String" />
            <asp:Parameter Name="WATERNAME" Type="String" />
            <asp:Parameter Name="STREAM" Type="Boolean" />
            <asp:Parameter Name="ACRESMILES" Type="Decimal" />
            <asp:Parameter Name="LOCATION" Type="String" />
            <asp:Parameter Name="DRAINAGE" Type="String" />
            <asp:Parameter Name="DRAINNAME" Type="String" />
            <asp:Parameter Name="HYDROCODE" Type="String" />
            <asp:Parameter Name="COUNTY" Type="String" />
            <asp:Parameter Name="REGION" Type="String" />
            <asp:Parameter Name="AWMAREA" Type="String" />
            <asp:Parameter Name="STKCODE" Type="String" />
            <asp:Parameter Name="BIOLOGIST" Type="String" />
            <asp:Parameter Name="AREABIO" Type="String" />
            <asp:Parameter Name="ADDLDATA" Type="String" />
            <asp:Parameter Name="MAPCOORDIN" Type="String" />
            <asp:Parameter Name="ATLASPAGE" Type="String" />
            <asp:Parameter Name="CATEGORY" Type="String" />
            <asp:Parameter Name="ELEVATION" Type="Decimal" />
            <asp:Parameter Name="UT_ELEVATI" Type="Decimal" />
            <asp:Parameter Name="OWNERSHIP" Type="String" />
            <asp:Parameter Name="WILDERNESS" Type="Boolean" />
            <asp:Parameter Name="BLMMAP" Type="String" />
            <asp:Parameter Name="TOPOMAP" Type="String" />
            <asp:Parameter Name="OBSOLETE" Type="Boolean" />
            <asp:Parameter Name="Valid" Type="Boolean" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="WATERCODE" Type="String" />
            <asp:Parameter Name="WATERNAME" Type="String" />
            <asp:Parameter Name="STREAM" Type="Boolean" />
            <asp:Parameter Name="ACRESMILES" Type="Decimal" />
            <asp:Parameter Name="LOCATION" Type="String" />
            <asp:Parameter Name="DRAINAGE" Type="String" />
            <asp:Parameter Name="DRAINNAME" Type="String" />
            <asp:Parameter Name="HYDROCODE" Type="String" />
            <asp:Parameter Name="COUNTY" Type="String" />
            <asp:Parameter Name="REGION" Type="String" />
            <asp:Parameter Name="AWMAREA" Type="String" />
            <asp:Parameter Name="STKCODE" Type="String" />
            <asp:Parameter Name="BIOLOGIST" Type="String" />
            <asp:Parameter Name="AREABIO" Type="String" />
            <asp:Parameter Name="ADDLDATA" Type="String" />
            <asp:Parameter Name="MAPCOORDIN" Type="String" />
            <asp:Parameter Name="ATLASPAGE" Type="String" />
            <asp:Parameter Name="CATEGORY" Type="String" />
            <asp:Parameter Name="ELEVATION" Type="Decimal" />
            <asp:Parameter Name="UT_ELEVATI" Type="Decimal" />
            <asp:Parameter Name="OWNERSHIP" Type="String" />
            <asp:Parameter Name="WILDERNESS" Type="Boolean" />
            <asp:Parameter Name="BLMMAP" Type="String" />
            <asp:Parameter Name="TOPOMAP" Type="String" />
            <asp:Parameter Name="OBSOLETE" Type="Boolean" />
            <asp:Parameter Name="Valid" Type="Boolean" />
            <asp:Parameter Name="ID" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>
