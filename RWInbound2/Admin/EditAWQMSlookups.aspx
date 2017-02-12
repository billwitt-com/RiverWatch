<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditAWQMSlookups.aspx.cs" Inherits="RWInbound2.Admin.EditAWQMSlookups" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

        <asp:Label ID="Label1" runat="server" CssClass="PageLabel" Text="Edit AWQMS Lookup Table"></asp:Label>
    <br />
        <br />
    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="SqlDataSource1" Width="1112px">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ButtonType="Button" ControlStyle-Width="40px"   ShowEditButton="True" />
            <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="ID" />
            <asp:BoundField DataField="LocalName" HeaderText="LocalName" SortExpression="LocalName" />
            <asp:BoundField DataField="CName" HeaderText="CName" SortExpression="CName" />
            <asp:BoundField DataField="ResultUnit" HeaderText="ResultUnit" SortExpression="ResultUnit" />
            <asp:BoundField DataField="ResultFraction" HeaderText="ResultFraction" SortExpression="ResultFraction" />
            <asp:BoundField DataField="AnaMethodID" HeaderText="AnaMethodID" SortExpression="AnaMethodID" />
            <asp:BoundField DataField="AnaMethodContext" HeaderText="AnaMethodContext" SortExpression="AnaMethodContext" />
            <asp:BoundField DataField="DetectionLevel" HeaderText="DetectionLevel" SortExpression="DetectionLevel" />
            <asp:BoundField DataField="LowerReportingLimit" HeaderText="LowerReportingLimit" SortExpression="LowerReportingLimit" />
            <asp:BoundField DataField="DetectionUnit" HeaderText="DetectionUnit" SortExpression="DetectionUnit" />
            <asp:BoundField DataField="MethodSpec" HeaderText="MethodSpec" SortExpression="MethodSpec" />
            <asp:BoundField DataField="StartDate" HeaderText="StartDate" SortExpression="StartDate" />
            <asp:BoundField DataField="EndDate" HeaderText="EndDate" SortExpression="EndDate" />
            <asp:BoundField DataField="DateCreated" HeaderText="DateCreated" SortExpression="DateCreated" />
            <asp:BoundField DataField="UserCreated" HeaderText="UserCreated" SortExpression="UserCreated" />
            <asp:CheckBoxField DataField="Valid" HeaderText="Valid" SortExpression="Valid" />
        </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RiverWatchDEV %>" DeleteCommand="DELETE FROM [tlkAQWMStranslation] WHERE [ID] = @ID" InsertCommand="INSERT INTO [tlkAQWMStranslation] ([LocalName], [CName], [ResultUnit], [ResultFraction], [AnaMethodID], [AnaMethodContext], [DetectionLevel], [LowerReportingLimit], [DetectionUnit], [MethodSpec], [StartDate], [EndDate], [DateCreated], [UserCreated], [Valid]) VALUES (@LocalName, @CName, @ResultUnit, @ResultFraction, @AnaMethodID, @AnaMethodContext, @DetectionLevel, @LowerReportingLimit, @DetectionUnit, @MethodSpec, @StartDate, @EndDate, @DateCreated, @UserCreated, @Valid)" SelectCommand="SELECT * FROM [tlkAQWMStranslation]" UpdateCommand="UPDATE [tlkAQWMStranslation] SET [LocalName] = @LocalName, [CName] = @CName, [ResultUnit] = @ResultUnit, [ResultFraction] = @ResultFraction, [AnaMethodID] = @AnaMethodID, [AnaMethodContext] = @AnaMethodContext, [DetectionLevel] = @DetectionLevel, [LowerReportingLimit] = @LowerReportingLimit, [DetectionUnit] = @DetectionUnit, [MethodSpec] = @MethodSpec, [StartDate] = @StartDate, [EndDate] = @EndDate, [DateCreated] = @DateCreated, [UserCreated] = @UserCreated, [Valid] = @Valid WHERE [ID] = @ID">
            <DeleteParameters>
                <asp:Parameter Name="ID" Type="Int32" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="LocalName" Type="String" />
                <asp:Parameter Name="CName" Type="String" />
                <asp:Parameter Name="ResultUnit" Type="String" />
                <asp:Parameter Name="ResultFraction" Type="String" />
                <asp:Parameter Name="AnaMethodID" Type="String" />
                <asp:Parameter Name="AnaMethodContext" Type="String" />
                <asp:Parameter Name="DetectionLevel" Type="Decimal" />
                <asp:Parameter Name="LowerReportingLimit" Type="Decimal" />
                <asp:Parameter Name="DetectionUnit" Type="String" />
                <asp:Parameter Name="MethodSpec" Type="String" />
                <asp:Parameter Name="StartDate" Type="DateTime" />
                <asp:Parameter Name="EndDate" Type="DateTime" />
                <asp:Parameter Name="DateCreated" Type="DateTime" />
                <asp:Parameter Name="UserCreated" Type="String" />
                <asp:Parameter Name="Valid" Type="Boolean" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="LocalName" Type="String" />
                <asp:Parameter Name="CName" Type="String" />
                <asp:Parameter Name="ResultUnit" Type="String" />
                <asp:Parameter Name="ResultFraction" Type="String" />
                <asp:Parameter Name="AnaMethodID" Type="String" />
                <asp:Parameter Name="AnaMethodContext" Type="String" />
                <asp:Parameter Name="DetectionLevel" Type="Decimal" />
                <asp:Parameter Name="LowerReportingLimit" Type="Decimal" />
                <asp:Parameter Name="DetectionUnit" Type="String" />
                <asp:Parameter Name="MethodSpec" Type="String" />
                <asp:Parameter Name="StartDate" Type="DateTime" />
                <asp:Parameter Name="EndDate" Type="DateTime" />
                <asp:Parameter Name="DateCreated" Type="DateTime" />
                <asp:Parameter Name="UserCreated" Type="String" />
                <asp:Parameter Name="Valid" Type="Boolean" />
                <asp:Parameter Name="ID" Type="Int32" />
            </UpdateParameters>
        </asp:SqlDataSource>
        <br />

</asp:Content>
