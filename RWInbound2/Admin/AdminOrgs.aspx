<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminOrgs.aspx.cs" Inherits="RWInbound2.Admin.AdminOrgs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label1" runat="server" CssClass="PageLabel" Text="Admin Organizations"></asp:Label>
    <br />
    <br />
    <asp:Label ID="Label2" runat="server" Text="Choose Org Name:  "></asp:Label>
    <asp:TextBox ID="tbOrgName" runat="server" Height="19px" Width="415px"></asp:TextBox>
    <ajaxToolkit:AutoCompleteExtender ID="tbOrgName_AutoCompleteExtender" runat="server" TargetControlID="tbOrgName"
         ServiceMethod="SearchOrgs" CompletionSetCount="2" MinimumPrefixLength="2">
    </ajaxToolkit:AutoCompleteExtender>
    <asp:Button ID="btnSelect" runat="server" OnClick="btnSelect_Click" Text="Select" CssClass="adminButton" />
    <asp:Button ID="btnAddNew" runat="server" CssClass="adminButton" OnClick="btnAddNew_Click" Text="Add New" />
    <br />
    <asp:CheckBox ID="chbStatusAdd" runat="server" Text="Add New Status " />
    <br />
    <asp:Label ID="lblMsg" runat="server" ></asp:Label>
    <br />
    <asp:Label ID="lblLastUsedText" runat="server" Text="Last Used Kit Number:  "></asp:Label>
    <asp:Label ID="lblKitNumber" runat="server" Font-Bold="True" Font-Size="11pt" ForeColor="#0066FF"></asp:Label>
    <asp:FormView ID="FormView1" runat="server" DefaultMode ="ReadOnly"  AllowPaging="true" 
        PagerSettings-Mode="NumericFirstLast"  
        PagerSettings-Position="Bottom" DataKeyNames="ID" DataSourceID="SqlDataSource1" Width="467px">
        <EditItemTemplate>
            ID:
            <asp:Label ID="IDLabel1" runat="server" Text='<%# Eval("ID") %>' />
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
<%--                            SelectedValue = '<%# Bind("WaterShed") %>'--%>
            WaterShed:
<%--            <asp:TextBox ID="WaterShedTextBox" runat="server" Text='<%# Bind("WaterShed") %>' />--%>
            <asp:DropDownList ID="ddlWaterShed" runat="server" 
                OnDataBinding="PreventErrorsOn_DataBinding"
                DataTextField = "Description"
                DataValueField = "Code"
                SelectedValue = '<%# Bind("WaterShed") %>'
                DataSourceID = "SqlDataSourceWaterShed">
            </asp:DropDownList>

           
 
            <br />
<%--                        SelectedValue = '<%# Bind("WaterShedGathering") %>'    --%>
            WaterShedGathering:
<%--                        <asp:TextBox ID="WaterShedGatheringTextBox" runat="server" Text='<%# Bind("WaterShedGathering") %>' />--%>
            <asp:DropDownList ID="ddlWaterShedGathering" runat="server" 
                OnDataBinding="PreventErrorsOn_DataBinding"    
                DataSourceID="SqlDataSourceWSGathering" 
                DataTextField="Description" 
                DataValueField="Code" 
                SelectedValue = '<%# Bind("WaterShedGathering") %>'>
            </asp:DropDownList >


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
            Valid:
            <asp:CheckBox ID="ValidCheckBox" runat="server" Checked='<%# Bind("Valid") %>' />
            <br />
            <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" OnClick="UpdateButton_Click" CommandName="Update" Text="Update" />
            &nbsp;<asp:Button ID="UpdateCancelButton" OnClick="UpdateCancelButton_Click" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
<%--             &nbsp;<asp:Button ID="NewButton" runat="server" CausesValidation="False" CommandName="New" Text="New" />--%>
        </EditItemTemplate>
        <InsertItemTemplate>
            ID:
            <asp:Label ID="IDLabel1" runat="server" Text='<%# Eval("ID") %>' />
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
<%--            <asp:TextBox ID="WaterShedTextBox" runat="server" Text='<%# Bind("WaterShed") %>' />--%>
            <asp:DropDownList ID="ddlWaterShed" runat="server" 
                OnDataBinding="PreventErrorsOn_DataBinding"
                DataTextField = "Description"
                DataValueField = "Code"
                SelectedValue = '<%# Bind("WaterShed") %>'
                DataSourceID = "SqlDataSourceWaterShed">
            </asp:DropDownList>           
 
            <br />
<%--                        SelectedValue = '<%# Bind("WaterShedGathering") %>'    --%>
            WaterShedGathering:
<%--                        <asp:TextBox ID="WaterShedGatheringTextBox" runat="server" Text='<%# Bind("WaterShedGathering") %>' />--%>
            <asp:DropDownList ID="ddlWaterShedGathering" runat="server" 
                OnDataBinding="PreventErrorsOn_DataBinding"    
                DataSourceID="SqlDataSourceWSGathering" 
                DataTextField="Description" 
                DataValueField="Code" 
                SelectedValue = '<%# Bind("WaterShedGathering") %>'>
            </asp:DropDownList >

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
<%--            <br />
            DateLastModified:
            <asp:TextBox ID="DateLastModifiedTextBox" runat="server" Text='<%# Bind("DateLastModified") %>' />
            <br />
            UserLastModified:
            <asp:TextBox ID="UserLastModifiedTextBox" runat="server" Text='<%# Bind("UserLastModified") %>' />--%>
            <br />
            Valid:
            <asp:CheckBox ID="ValidCheckBox" runat="server" Checked='<%# Bind("Valid") %>' />
            <br />
            <asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
            &nbsp;<asp:Button ID="InsertCancelButton" runat="server" CausesValidation="False" OnClick="InsertCancelButton_Click" CommandName="Cancel" Text="Cancel" />
        </InsertItemTemplate>
        <ItemTemplate>
<%--            ID:
            <asp:Label ID="IDLabel" runat="server" Text='<%# Eval("ID") %>' />
            <br />--%>
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
<%--            DateLastModified:
            <asp:Label ID="DateLastModifiedLabel" runat="server" Text='<%# Bind("DateLastModified") %>' />
            <br />
            UserLastModified:
            <asp:Label ID="UserLastModifiedLabel" runat="server" Text='<%# Bind("UserLastModified") %>' />
            <br />--%>
            Valid:
            <asp:CheckBox ID="ValidCheckBox" runat="server" Checked='<%# Bind("Valid") %>' Enabled="false" />
            <br />
            <asp:Button ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit" />
            &nbsp;
            <asp:Button ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" />
<%--            &nbsp;<asp:Button ID="NewButton" runat="server" CausesValidation="False" CommandName="New" Text="New" />--%>
        </ItemTemplate>

<%--                SelectCommand="SELECT * FROM [organization]"--%>
<PagerSettings Mode="NumericFirstLast"></PagerSettings>
    </asp:FormView>


    <asp:SqlDataSource ID="SqlDataSourceWSGathering" runat="server" 
        ConnectionString="<%$ ConnectionStrings:RiverwatchDEV %>"
        SelectCommand="SELECT [Description], [Code] FROM [tlkWSG] where valid = 1"></asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSourceWaterShed" runat="server"
        ConnectionString="<%$ ConnectionStrings:RiverwatchDEV %>"
        SelectCommand="SELECT [Description], [Code] FROM [tlkWQCCWaterShed] where valid = 1"></asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server"  OnInserted="SqlDataSource1_Inserted" 
        ConnectionString="<%$ ConnectionStrings:RiverwatchDEV %>" 
        DeleteCommand="UPDATE [organization] SET [Valid] = 1 WHERE [ID] = @ID" 
        InsertCommand="INSERT INTO [organization] ([KitNumber], [OrganizationName], [OrganizationType], [Email], [MailingAddress], [ShippingAddress], [City], [State], [Zip], [Phone], [Fax], [YearStarted], [WaterShed], [WaterShedGathering], [Password], [Active], [DateCreated], [UserCreated], [DateLastModified], [UserLastModified], [Valid]) VALUES (@KitNumber, @OrganizationName, @OrganizationType, @Email, @MailingAddress, @ShippingAddress, @City, @State, @Zip, @Phone, @Fax, @YearStarted, @WaterShed, @WaterShedGathering, @Password, @Active, @DateCreated, @UserCreated, @DateLastModified, @UserLastModified, @Valid)" 
 
        UpdateCommand="UPDATE [organization] SET [KitNumber] = @KitNumber, [OrganizationName] = @OrganizationName, [OrganizationType] = @OrganizationType, [Email] = @Email, [MailingAddress] = @MailingAddress, [ShippingAddress] = @ShippingAddress, [City] = @City, [State] = @State, [Zip] = @Zip, [Phone] = @Phone, [Fax] = @Fax, [YearStarted] = @YearStarted, [WaterShed] = @WaterShed, [WaterShedGathering] = @WaterShedGathering, [Password] = @Password, [Active] = @Active, [DateCreated] = @DateCreated, [UserCreated] = @UserCreated, [DateLastModified] = @DateLastModified, [UserLastModified] = @UserLastModified, [Valid] = @Valid WHERE [ID] = @ID">
        <DeleteParameters>
            <asp:Parameter Name="ID" Type="Int32" />
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
            <asp:Parameter Name="Valid" Type="Boolean" />
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
            <asp:Parameter Name="Valid" Type="Boolean" />
            <asp:Parameter Name="ID" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <br />
</asp:Content>
