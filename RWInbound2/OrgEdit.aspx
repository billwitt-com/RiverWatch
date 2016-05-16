<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrgEdit.aspx.cs" Inherits="RWInbound2.Content.OrgEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <p>
        Organization Edit</p>
  
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="OrganizationID" DataSourceID="SqlDataSource1" OnRowEditing="GridView1_RowEditing">
            <Columns>
                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                <asp:BoundField DataField="OrganizationID" HeaderText="OrganizationID" InsertVisible="False" ReadOnly="True" SortExpression="OrganizationID" />
                <asp:BoundField DataField="KitNumber" HeaderText="KitNumber" SortExpression="KitNumber" />
                <asp:BoundField DataField="OrganizationName" HeaderText="OrganizationName" SortExpression="OrganizationName" />
                <asp:BoundField DataField="OrganizationType" HeaderText="OrganizationType" SortExpression="OrganizationType" />
                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                <asp:BoundField DataField="MailingAddress" HeaderText="MailingAddress" SortExpression="MailingAddress" />
                <asp:BoundField DataField="ShippingAddress" HeaderText="ShippingAddress" SortExpression="ShippingAddress" />
                <asp:BoundField DataField="City" HeaderText="City" SortExpression="City" />
                <asp:BoundField DataField="State" HeaderText="State" SortExpression="State" />
                <asp:BoundField DataField="Zip" HeaderText="Zip" SortExpression="Zip" />
                <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone" />
                <asp:BoundField DataField="Fax" HeaderText="Fax" SortExpression="Fax" />
                <asp:BoundField DataField="YearStarted" HeaderText="YearStarted" SortExpression="YearStarted" />
                <asp:BoundField DataField="WaterShed" HeaderText="WaterShed" SortExpression="WaterShed" />
                <asp:BoundField DataField="WaterShedGathering" HeaderText="WaterShedGathering" SortExpression="WaterShedGathering" />
                <asp:BoundField DataField="Password" HeaderText="Password" SortExpression="Password" />
                <asp:CheckBoxField DataField="Active" HeaderText="Active" SortExpression="Active" />
                <asp:BoundField DataField="DateCreated" HeaderText="DateCreated" SortExpression="DateCreated" />
                <asp:BoundField DataField="UserCreated" HeaderText="UserCreated" SortExpression="UserCreated" />
                <asp:BoundField DataField="DateLastModified" HeaderText="DateLastModified" SortExpression="DateLastModified" />
                <asp:BoundField DataField="UserLastModified" HeaderText="UserLastModified" SortExpression="UserLastModified" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:dbRiverwatchWaterDataConnectionString %>" 
            DeleteCommand="DELETE FROM [tblOrganization] WHERE [OrganizationID] = @original_OrganizationID AND (([KitNumber] = @original_KitNumber) OR ([KitNumber] IS NULL AND @original_KitNumber IS NULL)) AND [OrganizationName] = @original_OrganizationName AND [OrganizationType] = @original_OrganizationType AND (([Email] = @original_Email) OR ([Email] IS NULL AND @original_Email IS NULL)) AND (([MailingAddress] = @original_MailingAddress) OR ([MailingAddress] IS NULL AND @original_MailingAddress IS NULL)) AND (([ShippingAddress] = @original_ShippingAddress) OR ([ShippingAddress] IS NULL AND @original_ShippingAddress IS NULL)) AND (([City] = @original_City) OR ([City] IS NULL AND @original_City IS NULL)) AND (([State] = @original_State) OR ([State] IS NULL AND @original_State IS NULL)) AND (([Zip] = @original_Zip) OR ([Zip] IS NULL AND @original_Zip IS NULL)) AND (([Phone] = @original_Phone) OR ([Phone] IS NULL AND @original_Phone IS NULL)) AND (([Fax] = @original_Fax) OR ([Fax] IS NULL AND @original_Fax IS NULL)) AND (([YearStarted] = @original_YearStarted) OR ([YearStarted] IS NULL AND @original_YearStarted IS NULL)) AND (([WaterShed] = @original_WaterShed) OR ([WaterShed] IS NULL AND @original_WaterShed IS NULL)) AND (([WaterShedGathering] = @original_WaterShedGathering) OR ([WaterShedGathering] IS NULL AND @original_WaterShedGathering IS NULL)) AND (([Password] = @original_Password) OR ([Password] IS NULL AND @original_Password IS NULL)) AND [Active] = @original_Active AND (([DateCreated] = @original_DateCreated) OR ([DateCreated] IS NULL AND @original_DateCreated IS NULL)) AND (([UserCreated] = @original_UserCreated) OR ([UserCreated] IS NULL AND @original_UserCreated IS NULL)) AND (([DateLastModified] = @original_DateLastModified) OR ([DateLastModified] IS NULL AND @original_DateLastModified IS NULL)) AND (([UserLastModified] = @original_UserLastModified) OR ([UserLastModified] IS NULL AND @original_UserLastModified IS NULL))" 
            InsertCommand="INSERT INTO [tblOrganization] ([KitNumber], [OrganizationName], [OrganizationType], [Email], [MailingAddress], [ShippingAddress], [City], [State], [Zip], [Phone], [Fax], [YearStarted], [WaterShed], [WaterShedGathering], [Password], [Active], [DateCreated], [UserCreated], [DateLastModified], [UserLastModified]) VALUES (@KitNumber, @OrganizationName, @OrganizationType, @Email, @MailingAddress, @ShippingAddress, @City, @State, @Zip, @Phone, @Fax, @YearStarted, @WaterShed, @WaterShedGathering, @Password, @Active, @DateCreated, @UserCreated, @DateLastModified, @UserLastModified)" OldValuesParameterFormatString="original_{0}" 
            SelectCommand="SELECT * FROM [tblOrganization]" UpdateCommand="UPDATE [tblOrganization] SET [KitNumber] = @KitNumber, [OrganizationName] = @OrganizationName, [OrganizationType] = @OrganizationType, [Email] = @Email, [MailingAddress] = @MailingAddress, [ShippingAddress] = @ShippingAddress, [City] = @City, [State] = @State, [Zip] = @Zip, [Phone] = @Phone, [Fax] = @Fax, [YearStarted] = @YearStarted, [WaterShed] = @WaterShed, [WaterShedGathering] = @WaterShedGathering, [Password] = @Password, [Active] = @Active, [DateCreated] = @DateCreated, [UserCreated] = @UserCreated, [DateLastModified] = @DateLastModified, [UserLastModified] = @UserLastModified WHERE [OrganizationID] = @original_OrganizationID AND (([KitNumber] = @original_KitNumber) OR ([KitNumber] IS NULL AND @original_KitNumber IS NULL)) AND [OrganizationName] = @original_OrganizationName AND [OrganizationType] = @original_OrganizationType AND (([Email] = @original_Email) OR ([Email] IS NULL AND @original_Email IS NULL)) AND (([MailingAddress] = @original_MailingAddress) OR ([MailingAddress] IS NULL AND @original_MailingAddress IS NULL)) AND (([ShippingAddress] = @original_ShippingAddress) OR ([ShippingAddress] IS NULL AND @original_ShippingAddress IS NULL)) AND (([City] = @original_City) OR ([City] IS NULL AND @original_City IS NULL)) AND (([State] = @original_State) OR ([State] IS NULL AND @original_State IS NULL)) AND (([Zip] = @original_Zip) OR ([Zip] IS NULL AND @original_Zip IS NULL)) AND (([Phone] = @original_Phone) OR ([Phone] IS NULL AND @original_Phone IS NULL)) AND (([Fax] = @original_Fax) OR ([Fax] IS NULL AND @original_Fax IS NULL)) AND (([YearStarted] = @original_YearStarted) OR ([YearStarted] IS NULL AND @original_YearStarted IS NULL)) AND (([WaterShed] = @original_WaterShed) OR ([WaterShed] IS NULL AND @original_WaterShed IS NULL)) AND (([WaterShedGathering] = @original_WaterShedGathering) OR ([WaterShedGathering] IS NULL AND @original_WaterShedGathering IS NULL)) AND (([Password] = @original_Password) OR ([Password] IS NULL AND @original_Password IS NULL)) AND [Active] = @original_Active AND (([DateCreated] = @original_DateCreated) OR ([DateCreated] IS NULL AND @original_DateCreated IS NULL)) AND (([UserCreated] = @original_UserCreated) OR ([UserCreated] IS NULL AND @original_UserCreated IS NULL)) AND (([DateLastModified] = @original_DateLastModified) OR ([DateLastModified] IS NULL AND @original_DateLastModified IS NULL)) AND (([UserLastModified] = @original_UserLastModified) OR ([UserLastModified] IS NULL AND @original_UserLastModified IS NULL))">
            <DeleteParameters>
                <asp:Parameter Name="original_OrganizationID" Type="Int32" />
                <asp:Parameter Name="original_KitNumber" Type="Int32" />
                <asp:Parameter Name="original_OrganizationName" Type="String" />
                <asp:Parameter Name="original_OrganizationType" Type="String" />
                <asp:Parameter Name="original_Email" Type="String" />
                <asp:Parameter Name="original_MailingAddress" Type="String" />
                <asp:Parameter Name="original_ShippingAddress" Type="String" />
                <asp:Parameter Name="original_City" Type="String" />
                <asp:Parameter Name="original_State" Type="String" />
                <asp:Parameter Name="original_Zip" Type="String" />
                <asp:Parameter Name="original_Phone" Type="String" />
                <asp:Parameter Name="original_Fax" Type="String" />
                <asp:Parameter Name="original_YearStarted" Type="String" />
                <asp:Parameter Name="original_WaterShed" Type="String" />
                <asp:Parameter Name="original_WaterShedGathering" Type="String" />
                <asp:Parameter Name="original_Password" Type="String" />
                <asp:Parameter Name="original_Active" Type="Boolean" />
                <asp:Parameter Name="original_DateCreated" Type="DateTime" />
                <asp:Parameter Name="original_UserCreated" Type="String" />
                <asp:Parameter Name="original_DateLastModified" Type="DateTime" />
                <asp:Parameter Name="original_UserLastModified" Type="String" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="KitNumber" Type="Int32" />
                <asp:Parameter Name="OrganizationName" Type="String" />
                <asp:Parameter Name="OrganizationType" Type="String" />
                <asp:Parameter Name="Email" Type="String" />
                <asp:Parameter Name="MailingAddress" Type="String" />
                <asp:Parameter Name="ShippingAddress" Type="String" />
                <asp:Parameter Name="City" Type="String" />
                <asp:Parameter Name="State" Type="String" />
                <asp:Parameter Name="Zip" Type="String" />
                <asp:Parameter Name="Phone" Type="String" />
                <asp:Parameter Name="Fax" Type="String" />
                <asp:Parameter Name="YearStarted" Type="String" />
                <asp:Parameter Name="WaterShed" Type="String" />
                <asp:Parameter Name="WaterShedGathering" Type="String" />
                <asp:Parameter Name="Password" Type="String" />
                <asp:Parameter Name="Active" Type="Boolean" />
                <asp:Parameter Name="DateCreated" Type="DateTime" />
                <asp:Parameter Name="UserCreated" Type="String" />
                <asp:Parameter Name="DateLastModified" Type="DateTime" />
                <asp:Parameter Name="UserLastModified" Type="String" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="KitNumber" Type="Int32" />
                <asp:Parameter Name="OrganizationName" Type="String" />
                <asp:Parameter Name="OrganizationType" Type="String" />
                <asp:Parameter Name="Email" Type="String" />
                <asp:Parameter Name="MailingAddress" Type="String" />
                <asp:Parameter Name="ShippingAddress" Type="String" />
                <asp:Parameter Name="City" Type="String" />
                <asp:Parameter Name="State" Type="String" />
                <asp:Parameter Name="Zip" Type="String" />
                <asp:Parameter Name="Phone" Type="String" />
                <asp:Parameter Name="Fax" Type="String" />
                <asp:Parameter Name="YearStarted" Type="String" />
                <asp:Parameter Name="WaterShed" Type="String" />
                <asp:Parameter Name="WaterShedGathering" Type="String" />
                <asp:Parameter Name="Password" Type="String" />
                <asp:Parameter Name="Active" Type="Boolean" />
                <asp:Parameter Name="DateCreated" Type="DateTime" />
                <asp:Parameter Name="UserCreated" Type="String" />
                <asp:Parameter Name="DateLastModified" Type="DateTime" />
                <asp:Parameter Name="UserLastModified" Type="String" />
                <asp:Parameter Name="original_OrganizationID" Type="Int32" />
                <asp:Parameter Name="original_KitNumber" Type="Int32" />
                <asp:Parameter Name="original_OrganizationName" Type="String" />
                <asp:Parameter Name="original_OrganizationType" Type="String" />
                <asp:Parameter Name="original_Email" Type="String" />
                <asp:Parameter Name="original_MailingAddress" Type="String" />
                <asp:Parameter Name="original_ShippingAddress" Type="String" />
                <asp:Parameter Name="original_City" Type="String" />
                <asp:Parameter Name="original_State" Type="String" />
                <asp:Parameter Name="original_Zip" Type="String" />
                <asp:Parameter Name="original_Phone" Type="String" />
                <asp:Parameter Name="original_Fax" Type="String" />
                <asp:Parameter Name="original_YearStarted" Type="String" />
                <asp:Parameter Name="original_WaterShed" Type="String" />
                <asp:Parameter Name="original_WaterShedGathering" Type="String" />
                <asp:Parameter Name="original_Password" Type="String" />
                <asp:Parameter Name="original_Active" Type="Boolean" />
                <asp:Parameter Name="original_DateCreated" Type="DateTime" />
                <asp:Parameter Name="original_UserCreated" Type="String" />
                <asp:Parameter Name="original_DateLastModified" Type="DateTime" />
                <asp:Parameter Name="original_UserLastModified" Type="String" />
            </UpdateParameters>
        </asp:SqlDataSource>

</asp:Content>

