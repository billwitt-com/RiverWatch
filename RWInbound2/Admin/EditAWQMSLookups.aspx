<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditAWQMSLookups.aspx.cs" Inherits="RWInbound2.Admin.EditAWQMSLookups" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
        <asp:Label ID="Label1" runat="server" CssClass="PageLabel" Text="Edit AWQMS Lookup Table"></asp:Label>
    <br />
        <br />

    <asp:Label ID="Label2" runat="server" Text="Choose Local (AL_D) Name:  "></asp:Label>
    <asp:TextBox ID="tbCommonName" runat="server" Height="19px" Width="415px"></asp:TextBox>
    <ajaxToolkit:AutoCompleteExtender ID="tbCommonName_AutoCompleteExtender" runat="server" TargetControlID="tbCommonName"
         ServiceMethod="SearchNames" CompletionSetCount="2" MinimumPrefixLength="2">
    </ajaxToolkit:AutoCompleteExtender>

    <asp:Button ID="btnSelect" runat="server"  Text="Select" CssClass="adminButton" OnClick="btnSelect_Click" />
        <asp:Button ID="btnSelectAll" CssClass="adminButton"   runat="server" Text="Select All" OnClick="btnSelectAll_Click" />
    <asp:Button ID="btnAddNew" runat="server" CssClass="adminButton"  Text="Add New" OnClick="btnAddNew_Click" />
            <br />
    <asp:Label ID="lblMsg" runat="server" ></asp:Label>
                <br />
    <asp:FormView ID="FormView1" runat="server"   OnItemUpdating="FormView1_ItemUpdating"  AllowPaging="True"  DataKeyNames="ID" DataSourceID="SqlDataSource1" Width="937px">
        <EditItemTemplate>
            ID:
            <asp:Label ID="IDLabel1" runat="server" Text='<%# Eval("ID") %>' />
            <br />
            LocalName:
            <asp:TextBox ID="LocalNameTextBox" runat="server" Text='<%# Bind("LocalName") %>' />
            <br />
            Characteristic_Name:
            <asp:TextBox ID="Characteristic_NameTextBox" runat="server" Text='<%# Bind("Characteristic_Name") %>' />
            <br />
            Result_Unit:
            <asp:TextBox ID="Result_UnitTextBox" runat="server" Text='<%# Bind("Result_Unit") %>' />
            <br />
            Result_Sample_Fraction:
            <asp:TextBox ID="Result_Sample_FractionTextBox" runat="server" Text='<%# Bind("Result_Sample_Fraction") %>' />
            <br />
            Result_Analytical_Method_ID:
            <asp:TextBox ID="Result_Analytical_Method_IDTextBox" runat="server" Text='<%# Bind("Result_Analytical_Method_ID") %>' /> 
            <br />
            Result_Analytical_Method_Context:
            <asp:TextBox ID="Result_Analytical_Method_ContextTextBox" runat="server" Text='<%# Bind("Result_Analytical_Method_Context") %>' />
            <br />
            Method_Detection_Level:
            <asp:TextBox ID="Method_Detection_LevelTextBox" runat="server" Text='<%# Bind("Method_Detection_Level") %>' />
            <br />
            Lower_Reporting_Limit:
            <asp:TextBox ID="Lower_Reporting_LimitTextBox" runat="server" Text='<%# Bind("Lower_Reporting_Limit") %>' />
            <br />
            Result_Detection_Limit_Unit:
            <asp:TextBox ID="Result_Detection_Limit_UnitTextBox" runat="server" Text='<%# Bind("Result_Detection_Limit_Unit") %>' />
            <br />
            Method_Speciation:
            <asp:TextBox ID="Method_SpeciationTextBox" runat="server" Text='<%# Bind("Method_Speciation") %>' />
            <br />
            StartDate:
            <asp:TextBox ID="StartDateTextBox" runat="server" Text='<%# Bind("StartDate") %>' />
            <br />
            EndDate:
            <asp:TextBox ID="EndDateTextBox" runat="server" Text='<%# Bind("EndDate") %>' />
            <br />
            DateCreated:
            <asp:TextBox ID="DateCreatedTextBox" runat="server" Text='<%# Bind("DateCreated") %>' />
            <br />
            UserCreated:
            <asp:TextBox ID="UserCreatedTextBox" runat="server" Text='<%# Bind("UserCreated") %>' />
            <br />
            Valid:
            <asp:CheckBox ID="ValidCheckBox" runat="server" Checked='<%# Bind("Valid") %>' />
            <br />
            <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
            &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
        </EditItemTemplate>
        <InsertItemTemplate>
            LocalName:
            <asp:TextBox ID="LocalNameTextBox" runat="server" Text='<%# Bind("LocalName") %>' />
            <br />
            Characteristic_Name:
            <asp:TextBox ID="Characteristic_NameTextBox" runat="server" Text='<%# Bind("Characteristic_Name") %>' />
            <br />
            Result_Unit:
            <asp:TextBox ID="Result_UnitTextBox" runat="server" Text='<%# Bind("Result_Unit") %>' />
            <br />
            Result_Sample_Fraction:
            <asp:TextBox ID="Result_Sample_FractionTextBox" runat="server" Text='<%# Bind("Result_Sample_Fraction") %>' />
            <br />
            Result_Analytical_Method_ID:
            <asp:TextBox ID="Result_Analytical_Method_IDTextBox" runat="server" Text='<%# Bind("Result_Analytical_Method_ID") %>' />
            <br />
            Result_Analytical_Method_Context:
            <asp:TextBox ID="Result_Analytical_Method_ContextTextBox" runat="server" Text='<%# Bind("Result_Analytical_Method_Context") %>' />
            <br />
            Method_Detection_Level:
            <asp:TextBox ID="Method_Detection_LevelTextBox" runat="server" Text='<%# Bind("Method_Detection_Level") %>' />
            <br />
            Lower_Reporting_Limit:
            <asp:TextBox ID="Lower_Reporting_LimitTextBox" runat="server" Text='<%# Bind("Lower_Reporting_Limit") %>' />
            <br />
            Result_Detection_Limit_Unit:
            <asp:TextBox ID="Result_Detection_Limit_UnitTextBox" runat="server" Text='<%# Bind("Result_Detection_Limit_Unit") %>' />
            <br />
            Method_Speciation:
            <asp:TextBox ID="Method_SpeciationTextBox" runat="server" Text='<%# Bind("Method_Speciation") %>' />
            <br />
            StartDate:
            <asp:TextBox ID="StartDateTextBox" runat="server" Text='<%# Bind("StartDate") %>' />
            <br />
            EndDate:
            <asp:TextBox ID="EndDateTextBox" runat="server" Text='<%# Bind("EndDate") %>' />
            <br />
            DateCreated:
            <asp:TextBox ID="DateCreatedTextBox" runat="server" Text='<%# Bind("DateCreated") %>' />
            <br />
            UserCreated:
            <asp:TextBox ID="UserCreatedTextBox" runat="server" Text='<%# Bind("UserCreated") %>' />
            <br />
            Valid:
            <asp:CheckBox ID="ValidCheckBox"  runat="server" Checked='<%# Bind("Valid") %>' />
            <br />
            <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
            &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
        </InsertItemTemplate>
        <ItemTemplate>
            ID:
            <asp:Label ID="IDLabel" runat="server" Text='<%# Eval("ID") %>' />
            <br />
            LocalName:
            <asp:Label ID="LocalNameLabel" runat="server" Text='<%# Bind("LocalName") %>' />
            <br />
            Characteristic_Name:
            <asp:Label ID="Characteristic_NameLabel" runat="server" Text='<%# Bind("Characteristic_Name") %>' />
            <br />
            Result_Unit:
            <asp:Label ID="Result_UnitLabel" runat="server" Text='<%# Bind("Result_Unit") %>' />
            <br />
            Result_Sample_Fraction:
            <asp:Label ID="Result_Sample_FractionLabel" runat="server" Text='<%# Bind("Result_Sample_Fraction") %>' />
            <br />
            Result_Analytical_Method_ID:
            <asp:Label ID="Result_Analytical_Method_IDLabel" runat="server" Text='<%# Bind("Result_Analytical_Method_ID") %>' />
            <br />
            Result_Analytical_Method_Context:
            <asp:Label ID="Result_Analytical_Method_ContextLabel" runat="server" Text='<%# Bind("Result_Analytical_Method_Context") %>' />
            <br />
            Method_Detection_Level:
            <asp:Label ID="Method_Detection_LevelLabel" runat="server" Text='<%# Bind("Method_Detection_Level") %>' />
            <br />
            Lower_Reporting_Limit:
            <asp:Label ID="Lower_Reporting_LimitLabel" runat="server" Text='<%# Bind("Lower_Reporting_Limit") %>' />
            <br />
            Result_Detection_Limit_Unit:
            <asp:Label ID="Result_Detection_Limit_UnitLabel" runat="server" Text='<%# Bind("Result_Detection_Limit_Unit") %>' />
            <br />
            Method_Speciation:
            <asp:Label ID="Method_SpeciationLabel" runat="server" Text='<%# Bind("Method_Speciation") %>' />
            <br />
            StartDate:
            <asp:Label ID="StartDateLabel" runat="server" Text='<%# Bind("StartDate") %>' />
            <br />
            EndDate:
            <asp:Label ID="EndDateLabel" runat="server" Text='<%# Bind("EndDate") %>' />
            <br />
            DateCreated:
            <asp:Label ID="DateCreatedLabel" runat="server" Text='<%# Bind("DateCreated") %>' />
            <br />
            UserCreated:
            <asp:Label ID="UserCreatedLabel" runat="server" Text='<%# Bind("UserCreated") %>' />
            <br />
            Valid:
            <asp:CheckBox ID="ValidCheckBox" runat="server" Checked='<%# Bind("Valid") %>' Enabled="false" />
            <br />
            <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit" />
            &nbsp;<asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" />
            &nbsp;<asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" CommandName="New" Text="New" />
        </ItemTemplate>
        </asp:FormView>


        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RiverWatchDEV %>" DeleteCommand="DELETE FROM [tlkAQWMStranslation] WHERE [ID] = @ID" InsertCommand="INSERT INTO [tlkAQWMStranslation] ([LocalName], [Characteristic_Name], [Result_Unit], [Result_Sample_Fraction], [Result_Analytical_Method_ID], [Result_Analytical_Method_Context], [Method_Detection_Level], [Lower_Reporting_Limit], [Result_Detection_Limit_Unit], [Method_Speciation], [StartDate], [EndDate], [DateCreated], [UserCreated], [Valid]) VALUES (@LocalName, @Characteristic_Name, @Result_Unit, @Result_Sample_Fraction, @Result_Analytical_Method_ID, @Result_Analytical_Method_Context, @Method_Detection_Level, @Lower_Reporting_Limit, @Result_Detection_Limit_Unit, @Method_Speciation, @StartDate, @EndDate, @DateCreated, @UserCreated, @Valid)" SelectCommand="SELECT * FROM [tlkAQWMStranslation]" UpdateCommand="UPDATE [tlkAQWMStranslation] SET [LocalName] = @LocalName, [Characteristic_Name] = @Characteristic_Name, [Result_Unit] = @Result_Unit, [Result_Sample_Fraction] = @Result_Sample_Fraction, [Result_Analytical_Method_ID] = @Result_Analytical_Method_ID, [Result_Analytical_Method_Context] = @Result_Analytical_Method_Context, [Method_Detection_Level] = @Method_Detection_Level, [Lower_Reporting_Limit] = @Lower_Reporting_Limit, [Result_Detection_Limit_Unit] = @Result_Detection_Limit_Unit, [Method_Speciation] = @Method_Speciation, [StartDate] = @StartDate, [EndDate] = @EndDate, [DateCreated] = @DateCreated, [UserCreated] = @UserCreated, [Valid] = @Valid WHERE [ID] = @ID">
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


</asp:Content>
