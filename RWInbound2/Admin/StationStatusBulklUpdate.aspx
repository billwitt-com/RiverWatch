<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"  CodeBehind="StationStatusBulklUpdate.aspx.cs" Inherits="RWInbound2.Admin.StationStatusBulklUpdate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    
        <br />
        <asp:Label ID="Label1" CssClass="PageLabel" runat="server" Text="Station Status Bulk Update (click on column name to sort)"></asp:Label>
            <br />
                <br />
        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="SqlDataSource1">
            <Columns>
                <asp:CommandField ButtonType="Button" ShowEditButton="True" />

                <asp:TemplateField HeaderText="StationStatus" SortExpression="StationStatus">
                    <EditItemTemplate>
                    <asp:DropDownList ID="ddlStationStatus" runat="server" 
                    OnDataBinding="PreventErrorsOn_DataBinding"
                    DataTextField = "Code"
                    DataValueField = "Description"
                    SelectedValue = '<%# Bind("StationStatus") %>'
                    DataSourceID ="SqlDataSource2">
               </asp:DropDownList>
<%--                        <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource2" OnDataBinding="DropDownList1_DataBinding"  SelectedValue='<%# Bind("StationStatus") %>'>
                        </asp:DropDownList>--%>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("StationStatus") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:BoundField DataField="StationName" HeaderText="StationName" SortExpression="StationName" />
                <asp:BoundField DataField="StationNumber" HeaderText="StationNumber" SortExpression="StationNumber" />
                <asp:BoundField DataField="RWWaterShed" HeaderText="RWWaterShed" SortExpression="RWWaterShed" />
                <asp:BoundField DataField="WQCCWaterShed" HeaderText="WQCCWaterShed" SortExpression="WQCCWaterShed" />
                <asp:BoundField DataField="River" HeaderText="River" SortExpression="River" />
                <asp:BoundField DataField="StationType" HeaderText="StationType" SortExpression="StationType" />
                <asp:BoundField DataField="County" HeaderText="County" SortExpression="County" />
                <asp:BoundField DataField="State" HeaderText="State" SortExpression="State" />
                <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="ID" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RiverWatchDEV %>" 
            DeleteCommand="DELETE FROM [Station] WHERE [ID] = @ID" 
            InsertCommand="INSERT INTO [Station] ([StationName], [StationNumber], [StationStatus], [RWWaterShed], [WQCCWaterShed], [River], [StationType], [County], [State]) VALUES (@StationName, @StationNumber, @StationStatus, @RWWaterShed, @WQCCWaterShed, @River, @StationType, @County, @State)" 
            SelectCommand="SELECT [StationName], [StationNumber], [StationStatus], [RWWaterShed], [WQCCWaterShed], [River], [StationType], [County], [State], [ID] FROM [Station]" UpdateCommand="UPDATE [Station] SET [StationName] = @StationName, [StationNumber] = @StationNumber, [StationStatus] = @StationStatus, [RWWaterShed] = @RWWaterShed, [WQCCWaterShed] = @WQCCWaterShed, [River] = @River, [StationType] = @StationType, [County] = @County, [State] = @State WHERE [ID] = @ID">
            <DeleteParameters>
                <asp:Parameter Name="ID" Type="Int32" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="StationName" Type="String" />
                <asp:Parameter Name="StationNumber" Type="Int32" />
                <asp:Parameter Name="StationStatus" Type="String" />
                <asp:Parameter Name="RWWaterShed" Type="String" />
                <asp:Parameter Name="WQCCWaterShed" Type="String" />
                <asp:Parameter Name="River" Type="String" />
                <asp:Parameter Name="StationType" Type="String" />
                <asp:Parameter Name="County" Type="String" />
                <asp:Parameter Name="State" Type="String" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="StationName" Type="String" />
                <asp:Parameter Name="StationNumber" Type="Int32" />
                <asp:Parameter Name="StationStatus" Type="String" />
                <asp:Parameter Name="RWWaterShed" Type="String" />
                <asp:Parameter Name="WQCCWaterShed" Type="String" />
                <asp:Parameter Name="River" Type="String" />
                <asp:Parameter Name="StationType" Type="String" />
                <asp:Parameter Name="County" Type="String" />
                <asp:Parameter Name="State" Type="String" />
                <asp:Parameter Name="ID" Type="Int32" />
            </UpdateParameters>
        </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:RiverWatchDEV %>" 
                    SelectCommand="SELECT [Description], [Code] FROM [tlkStationStatus] WHERE [Valid] = 1">
        </asp:SqlDataSource>
                <br />

<%--    , [Description]--%>

















</asp:Content>
