<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"  CodeBehind="EditAWQMSlookups.aspx.cs" Inherits="RWInbound2.Admin.EditAWQMSlookups" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

        <asp:Label ID="Label1" runat="server" CssClass="PageLabel" Text="Edit AWQMS Lookup Table"></asp:Label>
    <br />
        <br />
        <asp:GridView ID="GridView1" on runat="server" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="SqlDataSource1">
            <Columns>
                <asp:CommandField ButtonType="Button" ShowDeleteButton="True" ShowEditButton="True"  ShowInsertButton="True" />
                <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="ID" />
                <asp:BoundField DataField="LocalName" HeaderText="LocalName" SortExpression="LocalName" />
                <asp:BoundField DataField="Characteristic Name" HeaderText="Characteristic Name" SortExpression="Characteristic Name" />
                <asp:BoundField DataField="Result Unit" HeaderText="Result Unit" SortExpression="Result Unit" />
                <asp:BoundField DataField="Result Sample Fraction" HeaderText="Result Sample Fraction" SortExpression="Result Sample Fraction" />
                <asp:BoundField DataField="Result Analytical Method ID" HeaderText="Result Analytical Method ID" SortExpression="Result Analytical Method ID" />
                <asp:BoundField DataField="Result Analytical Method Context" HeaderText="Result Analytical Method Context" SortExpression="Result Analytical Method Context" />
                <asp:BoundField DataField="Method Detection Level" HeaderText="Method Detection Level" SortExpression="Method Detection Level" />
                <asp:BoundField DataField="Lower Reporting Limit" HeaderText="Lower Reporting Limit" SortExpression="Lower Reporting Limit" />
                <asp:BoundField DataField="Result Detection Limit Unit" HeaderText="Result Detection Limit Unit" SortExpression="Result Detection Limit Unit" />
                <asp:BoundField DataField="Method Speciation" HeaderText="Method Speciation" SortExpression="Method Speciation" />
                <asp:BoundField DataField="StartDate" HeaderText="StartDate" SortExpression="StartDate" />
                <asp:BoundField DataField="EndDate" HeaderText="EndDate" SortExpression="EndDate" />
                <asp:BoundField DataField="DateCreated" HeaderText="DateCreated" SortExpression="DateCreated" />
                <asp:BoundField DataField="UserCreated" HeaderText="UserCreated" SortExpression="UserCreated" />
                <asp:CheckBoxField DataField="Valid" HeaderText="Valid" SortExpression="Valid" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RiverWatchDEV %>" 
            DeleteCommand="DELETE FROM [tlkAQWMStranslation] WHERE [ID] = @ID" 
            InsertCommand="INSERT INTO [tlkAQWMStranslation] ([LocalName], [Characteristic Name], [Result Unit], [Result Sample Fraction], [Result Analytical Method ID], [Result Analytical Method Context], [Method Detection Level], [Lower Reporting Limit], [Result Detection Limit Unit], [Method Speciation], [StartDate], [EndDate], [DateCreated], [UserCreated], [Valid]) VALUES (@LocalName, @Characteristic_Name, @Result_Unit, @Result_Sample_Fraction, @Result_Analytical_Method_ID, @Result_Analytical_Method_Context, @Method_Detection_Level, @Lower_Reporting_Limit, @Result_Detection_Limit_Unit, @Method_Speciation, @StartDate, @EndDate, @DateCreated, @UserCreated, @Valid)" 
            SelectCommand="SELECT * FROM [tlkAQWMStranslation]" 
            UpdateCommand="UPDATE [tlkAQWMStranslation] SET [LocalName] = @LocalName, [Characteristic Name] = @Characteristic_Name, [Result Unit] = @Result_Unit, [Result Sample Fraction] = @Result_Sample_Fraction, [Result Analytical Method ID] = @Result_Analytical_Method_ID, [Result Analytical Method Context] = @Result_Analytical_Method_Context, [Method Detection Level] = @Method_Detection_Level, [Lower Reporting Limit] = @Lower_Reporting_Limit, [Result Detection Limit Unit] = @Result_Detection_Limit_Unit, [Method Speciation] = @Method_Speciation, [StartDate] = @StartDate, [EndDate] = @EndDate, [DateCreated] = @DateCreated, [UserCreated] = @UserCreated, [Valid] = @Valid WHERE [ID] = @ID">
            <DeleteParameters>
                <asp:Parameter Name="ID" Type="Int32" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="LocalName" Type="String" />
                <asp:Parameter Name="Characteristic_Name" Type="String" />
                <asp:Parameter Name="Result_Unit" Type="String" />
                <asp:Parameter Name="Result_Sample_Fraction" Type="String" />
                <asp:Parameter Name="Result_Analytical_Method_ID" Type="String" />
                <asp:Parameter Name="Result_Analytical_Method_Context" Type="String" />
                <asp:Parameter Name="Method_Detection_Level" Type="Decimal" />
                <asp:Parameter Name="Lower_Reporting_Limit" Type="Decimal" />
                <asp:Parameter Name="Result_Detection_Limit_Unit" Type="String" />
                <asp:Parameter Name="Method_Speciation" Type="String" />
                <asp:Parameter Name="StartDate" Type="DateTime" />
                <asp:Parameter Name="EndDate" Type="DateTime" />
                <asp:Parameter Name="DateCreated" Type="DateTime" />
                <asp:Parameter Name="UserCreated" Type="String" />
                <asp:Parameter Name="Valid" Type="Boolean" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="LocalName" Type="String" />
                <asp:Parameter Name="Characteristic_Name" Type="String" />
                <asp:Parameter Name="Result_Unit" Type="String" />
                <asp:Parameter Name="Result_Sample_Fraction" Type="String" />
                <asp:Parameter Name="Result_Analytical_Method_ID" Type="String" />
                <asp:Parameter Name="Result_Analytical_Method_Context" Type="String" />
                <asp:Parameter Name="Method_Detection_Level" Type="Decimal" />
                <asp:Parameter Name="Lower_Reporting_Limit" Type="Decimal" />
                <asp:Parameter Name="Result_Detection_Limit_Unit" Type="String" />
                <asp:Parameter Name="Method_Speciation" Type="String" />
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
