<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditOrgFormView.aspx.cs" Inherits="RWInbound2.EditOrgFormView" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p class="site-title">
        Edit Organizations</p>
    <p>
        Search on Org Name :
        <asp:TextBox ID="tbSearch" runat="server"></asp:TextBox>
        <asp:Button ID="btnSearch" runat="server" Text="Search" Height="31px" OnClick="btnSearch_Click" />
        <%--This is from drag and drop from toolbox. Note, servicemethod was added by hand by me--%>
        <ajaxToolkit:AutoCompleteExtender ID="tbSearch_AutoCompleteExtender" runat="server" BehaviorID="tbSearch_AutoCompleteExtender" DelimiterCharacters=""  
            ServiceMethod="SearchOrgs"             
            TargetControlID="tbSearch"
            MinimumPrefixLength="2"
            CompletionInterval="100" 
            EnableCaching="false" 
            CompletionSetCount="10">
        </ajaxToolkit:AutoCompleteExtender> 
    </p>
    <p>
       <%-- this was built by VS and only required adding a sql data source. --%>
        <asp:FormView ID="FormView1"   runat="server" AllowPaging="True" DataKeyNames="OrganizationID" DataSourceID="SqlDataSource1" OnItemUpdating="FormView1_ItemUpdating">
            <EditItemTemplate>
                OrganizationID:
                <asp:Label ID="OrganizationIDLabel1" runat="server" Text='<%# Eval("OrganizationID") %>' />
                <br />
                KitNumber:
                <asp:TextBox ID="KitNumberTextBox" runat="server" Text='<%# Bind("KitNumber") %>' />
                <br />
                OrganizationName:
                <asp:TextBox ID="OrganizationNameTextBox" runat="server" Text='<%# Bind("OrganizationName") %>' />
                <br />
                OrganizationType:
                <asp:TextBox ID="OrganizationTypeTextBox" runat="server" Text='<%# Bind("OrganizationType") %>' />
                <br />
                Email:
                <asp:TextBox ID="EmailTextBox" runat="server" Text='<%# Bind("Email") %>' />
                <br />
                MailingAddress:
                <asp:TextBox ID="MailingAddressTextBox" runat="server" Text='<%# Bind("MailingAddress") %>' />
                <br />
                ShippingAddress:
                <asp:TextBox ID="ShippingAddressTextBox" runat="server" Text='<%# Bind("ShippingAddress") %>' />
                <br /> 
                City:
                <asp:TextBox ID="CityTextBox" runat="server" Text='<%# Bind("City") %>' />
                <br />
                State:
                <asp:TextBox ID="StateTextBox" runat="server" Text='<%# Bind("State") %>' />
                <br />
                Zip:
                <asp:TextBox ID="ZipTextBox" runat="server" Text='<%# Bind("Zip") %>' />
                <br />
                Phone:
                <asp:TextBox ID="PhoneTextBox" runat="server" Text='<%# Bind("Phone") %>' />
                <br />
                Fax:
                <asp:TextBox ID="FaxTextBox" runat="server" Text='<%# Bind("Fax") %>' />
                <br />
                YearStarted:
                <asp:TextBox ID="YearStartedTextBox" runat="server" Text='<%# Bind("YearStarted") %>' />
                <br />
                WaterShed:
                <asp:TextBox ID="WaterShedTextBox" runat="server" Text='<%# Bind("WaterShed") %>' />
                <br />
                WaterShedGathering:
                <asp:TextBox ID="WaterShedGatheringTextBox" runat="server" Text='<%# Bind("WaterShedGathering") %>' />
                <br />
                Password:
                <asp:TextBox ID="PasswordTextBox" runat="server" Text='<%# Bind("Password") %>' />
                <br />
                Active:
                <asp:CheckBox ID="ActiveCheckBox" runat="server" Checked='<%# Bind("Active") %>' />
                <br />
                DateCreated:
                <asp:TextBox ID="DateCreatedTextBox" runat="server" Text='<%# Bind("DateCreated") %>' />
                <br />
                UserCreated:
                <asp:TextBox ID="UserCreatedTextBox" runat="server" Text='<%# Bind("UserCreated") %>' />
                <br />
                DateLastModified:
                <asp:TextBox ID="DateLastModifiedTextBox" runat="server" Text='<%# Bind("DateLastModified") %>' />
                <br />
                UserLastModified:
                <asp:TextBox ID="UserLastModifiedTextBox" runat="server" Text='<%# Bind("UserLastModified") %>' />
                <br />
                <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
                &nbsp;
               <%-- link buttons, as per VS changed to buttons by editing--%>
                <%--<asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />--%>
                <asp:Button ID="UpdateCancelButton" CausesValidation="false" CommandName="Cancel" runat="server" Text="Cancel" />
            </EditItemTemplate>
            <InsertItemTemplate>
                KitNumber:
                <asp:TextBox ID="KitNumberTextBox" runat="server" Text='<%# Bind("KitNumber") %>' />
                <br />
                OrganizationName:
                <asp:TextBox ID="OrganizationNameTextBox" runat="server" Text='<%# Bind("OrganizationName") %>' />
                <br />
                OrganizationType:
                <asp:TextBox ID="OrganizationTypeTextBox" runat="server" Text='<%# Bind("OrganizationType") %>' />
                <br />
                Email:
                <asp:TextBox ID="EmailTextBox" runat="server" Text='<%# Bind("Email") %>' />
                <br />
                MailingAddress:
                <asp:TextBox ID="MailingAddressTextBox" runat="server" Text='<%# Bind("MailingAddress") %>' />
                <br />
                ShippingAddress:
                <asp:TextBox ID="ShippingAddressTextBox" runat="server" Text='<%# Bind("ShippingAddress") %>' />
                <br />
                City:
                <asp:TextBox ID="CityTextBox" runat="server" Text='<%# Bind("City") %>' />
                <br />
                State:
                <asp:TextBox ID="StateTextBox" runat="server" Text='<%# Bind("State") %>' />
                <br />
                Zip:
                <asp:TextBox ID="ZipTextBox" runat="server" Text='<%# Bind("Zip") %>' />
                <br />
                Phone:
                <asp:TextBox ID="PhoneTextBox" runat="server" Text='<%# Bind("Phone") %>' />
                <br />
                Fax:
                <asp:TextBox ID="FaxTextBox" runat="server" Text='<%# Bind("Fax") %>' />
                <br />
                YearStarted:
                <asp:TextBox ID="YearStartedTextBox" runat="server" Text='<%# Bind("YearStarted") %>' />
                <br />
                WaterShed:
                <asp:TextBox ID="WaterShedTextBox" runat="server" Text='<%# Bind("WaterShed") %>' />
                <br />
                WaterShedGathering:
                <asp:TextBox ID="WaterShedGatheringTextBox" runat="server" Text='<%# Bind("WaterShedGathering") %>' />
                <br />
                Password:
                <asp:TextBox ID="PasswordTextBox" runat="server" Text='<%# Bind("Password") %>' />
                <br />
                Active:
                <asp:CheckBox ID="ActiveCheckBox" runat="server" Checked='<%# Bind("Active") %>' />
                <br />
                DateCreated:
                <asp:TextBox ID="DateCreatedTextBox" runat="server" Text='<%# Bind("DateCreated") %>' />
                <br />
                UserCreated:
                <asp:TextBox ID="UserCreatedTextBox" runat="server" Text='<%# Bind("UserCreated") %>' />
                <br />
                DateLastModified:
                <asp:TextBox ID="DateLastModifiedTextBox" runat="server" Text='<%# Bind("DateLastModified") %>' />
                <br />
                UserLastModified:
                <asp:TextBox ID="UserLastModifiedTextBox" runat="server" Text='<%# Bind("UserLastModified") %>' />
                <br />
                <asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
                &nbsp;<asp:Button ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
            </InsertItemTemplate>
            <ItemTemplate>
                OrganizationID:
                <asp:Label ID="OrganizationIDLabel" runat="server" Text='<%# Eval("OrganizationID") %>' />
                <br />
                KitNumber:
                <asp:Label ID="KitNumberLabel" runat="server" Text='<%# Bind("KitNumber") %>' />
                <br />
                OrganizationName:
                <asp:Label ID="OrganizationNameLabel" runat="server" Text='<%# Bind("OrganizationName") %>' />
                <br />
                OrganizationType:
                <asp:Label ID="OrganizationTypeLabel" runat="server" Text='<%# Bind("OrganizationType") %>' />
                <br />
                Email:
                <asp:Label ID="EmailLabel" runat="server" Text='<%# Bind("Email") %>' />
                <br />
                MailingAddress:
                <asp:Label ID="MailingAddressLabel" runat="server" Text='<%# Bind("MailingAddress") %>' />
                <br />
                ShippingAddress:
                <asp:Label ID="ShippingAddressLabel" runat="server" Text='<%# Bind("ShippingAddress") %>' />
                <br />
                City:
                <asp:Label ID="CityLabel" runat="server" Text='<%# Bind("City") %>' />
                <br />
                State:
                <asp:Label ID="StateLabel" runat="server" Text='<%# Bind("State") %>' />
                <br />
                Zip:
                <asp:Label ID="ZipLabel" runat="server" Text='<%# Bind("Zip") %>' />
                <br />
                Phone:
                <asp:Label ID="PhoneLabel" runat="server" Text='<%# Bind("Phone") %>' />
                <br />
                Fax:
                <asp:Label ID="FaxLabel" runat="server" Text='<%# Bind("Fax") %>' />
                <br />
                YearStarted:
                <asp:Label ID="YearStartedLabel" runat="server" Text='<%# Bind("YearStarted") %>' />
                <br />
                WaterShed:
                <asp:Label ID="WaterShedLabel" runat="server" Text='<%# Bind("WaterShed") %>' />
                <br />
                WaterShedGathering:
                <asp:Label ID="WaterShedGatheringLabel" runat="server" Text='<%# Bind("WaterShedGathering") %>' />
                <br />
                Password:
                <asp:Label ID="PasswordLabel" runat="server" Text='<%# Bind("Password") %>' />
                <br />
                Active:
                <asp:CheckBox ID="ActiveCheckBox" runat="server" Checked='<%# Bind("Active") %>' Enabled="false" />
                <br />
                DateCreated:
                <asp:Label ID="DateCreatedLabel" runat="server" Text='<%# Bind("DateCreated") %>' />
                <br />
                UserCreated:
                <asp:Label ID="UserCreatedLabel" runat="server" Text='<%# Bind("UserCreated") %>' />
                <br />
                DateLastModified:
                <asp:Label ID="DateLastModifiedLabel" runat="server" Text='<%# Bind("DateLastModified") %>' />
                <br />
                UserLastModified:
                <asp:Label ID="UserLastModifiedLabel" runat="server" Text='<%# Bind("UserLastModified") %>' />
                <br />
                <asp:Button ID="EditButton" OnClick="EditButton_Click" runat="server" CausesValidation="True" CommandName="Edit" Text="Edit" />
<%--                return button added so user could get from search/edit page back to main page. It is turned on and off in code behind--%>
                <asp:Button ID="ReturnButton" runat="server" CausesValidation="True"  Visible="false" OnClick="ReturnButton_Click" Text="Return" />
                <asp:Button ID="InsertButton" runat="server" CausesValidation="True"  OnClick="InsertButton_Click"  Text="Insert" />
            </ItemTemplate>
        </asp:FormView>
        <ajaxToolkit:RoundedCornersExtender ID="FormView1_RoundedCornersExtender" runat="server" BehaviorID="FormView1_RoundedCornersExtender" TargetControlID="FormView1">
        </ajaxToolkit:RoundedCornersExtender>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RiverwatchWaterDEV %>" 
            DeleteCommand="DELETE FROM [tblOrganization] WHERE [OrganizationID] = @OrganizationID" 
            InsertCommand="INSERT INTO [tblOrganization] ([KitNumber], [OrganizationName], [OrganizationType], [Email], [MailingAddress], [ShippingAddress], [City], [State], [Zip], [Phone], [Fax], [YearStarted], [WaterShed], [WaterShedGathering], [Password], [Active], [DateCreated], [UserCreated], [DateLastModified], [UserLastModified]) VALUES (@KitNumber, @OrganizationName, @OrganizationType, @Email, @MailingAddress, @ShippingAddress, @City, @State, @Zip, @Phone, @Fax, @YearStarted, @WaterShed, @WaterShedGathering, @Password, @Active, @DateCreated, @UserCreated, @DateLastModified, @UserLastModified)" 
            SelectCommand="SELECT * FROM [tblOrganization]" 
            UpdateCommand="UPDATE [tblOrganization] SET [KitNumber] = @KitNumber, [OrganizationName] = @OrganizationName, [OrganizationType] = @OrganizationType, [Email] = @Email, [MailingAddress] = @MailingAddress, [ShippingAddress] = @ShippingAddress, [City] = @City, [State] = @State, [Zip] = @Zip, [Phone] = @Phone, [Fax] = @Fax, [YearStarted] = @YearStarted, [WaterShed] = @WaterShed, [WaterShedGathering] = @WaterShedGathering, [Password] = @Password, [Active] = @Active, [DateCreated] = @DateCreated, [UserCreated] = @UserCreated, [DateLastModified] = @DateLastModified, [UserLastModified] = @UserLastModified WHERE [OrganizationID] = @OrganizationID">
            <DeleteParameters>
                <asp:Parameter Name="OrganizationID" Type="Int32" />
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
                <asp:Parameter Name="OrganizationID" Type="Int32" />
            </UpdateParameters>
        </asp:SqlDataSource>
    </p>
</asp:Content>
