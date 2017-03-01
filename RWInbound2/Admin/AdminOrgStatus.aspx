<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminOrgStatus.aspx.cs" Inherits="RWInbound2.Admin.AdminOrgStatus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <asp:Label ID="Label1" runat="server" CssClass="PageLabel" Text="Admin Organization Status"></asp:Label>
    <br />
    <br />
    <asp:Label ID="Label2" runat="server" Text="Choose Org Name:  "></asp:Label>
    <asp:TextBox ID="tbOrgName" runat="server" Height="19px" Width="415px"></asp:TextBox>
    <ajaxToolkit:AutoCompleteExtender ID="tbOrgName_AutoCompleteExtender" runat="server" TargetControlID="tbOrgName"
         ServiceMethod="SearchOrgs" CompletionSetCount="2" MinimumPrefixLength="2">
    </ajaxToolkit:AutoCompleteExtender>

    <asp:Button ID="btnSelect" runat="server" OnClick="btnSelect_Click" Text="Select" CssClass="adminButton" />
            <br />
        <asp:Label ID="lblMsg" runat="server" ></asp:Label>

        <br />
    <asp:Label ID="lblOrgName" runat="server" ></asp:Label>
        <br />
        <asp:FormView ID="FormView1" OnItemInserting="FormView1_ItemInserting"  CssClass="formviewText" runat="server"  AllowPaging="True" PagerSettings-Position="TopAndBottom" DataKeyNames="ID" DataSourceID="SqlDataSource1">
            <EditItemTemplate >                

                Contract Start Date:
                <asp:TextBox ID="ContractStartDateTextBox" runat="server" Text='<%# Bind("ContractStartDate") %>' />
                <br />
                Contract End Date:
                <asp:TextBox ID="ContractEndDateTextBox" runat="server" Text='<%# Bind("ContractEndDate") %>' />
                <br />
                Contract Signed:
                <asp:CheckBox ID="ContractSignedCheckBox" runat="server" Checked='<%# Bind("ContractSigned") %>' />
                <br />
                Contract Signed Date:
                <asp:TextBox ID="ContractSignedDateTextBox" runat="server" Text='<%# Bind("ContractSignedDate") %>' />
                <br />
                Site Visited:
                <asp:CheckBox ID="SiteVisitedCheckBox" runat="server" Checked='<%# Bind("SiteVisited") %>' />
                <br />
                Volunteer Time Sheet 1:
                <asp:CheckBox ID="VolunteerTimeSheet1CheckBox" runat="server" Checked='<%# Bind("VolunteerTimeSheet1") %>' />
                <br />
                Volunteer Time Sheet 2:
                <asp:CheckBox ID="VolunteerTimeShee2CheckBox" runat="server" Checked='<%# Bind("VolunteerTimeShee2") %>' />
                <br />
                Volunteer Time Sheet 3:
                <asp:CheckBox ID="VolunteerTimeSheet3CheckBox" runat="server" Checked='<%# Bind("VolunteerTimeSheet3") %>' />
                <br />
                Volunteer Time Sheet 4:
                <asp:CheckBox ID="VolunteerTimeSheet4CheckBox" runat="server" Checked='<%# Bind("VolunteerTimeSheet4") %>' />
                <br />
                Data Entered Electronically 1:
                <asp:CheckBox ID="DataEnteredElectronically1CheckBox" runat="server" Checked='<%# Bind("DataEnteredElectronically1") %>' />
                <br />
                Data Entered Electronically 2:
                <asp:CheckBox ID="DataEnteredElectronically2CheckBox" runat="server" Checked='<%# Bind("DataEnteredElectronically2") %>' />
                <br />
                Data Entered Electronically 3:
                <asp:CheckBox ID="DataEnteredElectronically3CheckBox" runat="server" Checked='<%# Bind("DataEnteredElectronically3") %>' />
                <br />
                Data Entered Electronically 4:
                <asp:CheckBox ID="DataEnteredElectronically4CheckBox" runat="server" Checked='<%# Bind("DataEnteredElectronically4") %>' />
                <br />
                Sample Shipped 1:
                <asp:CheckBox ID="SampleShipped1CheckBox" runat="server" Checked='<%# Bind("SampleShipped1") %>' />
                <br />
                Sample Shipped 2:
                <asp:CheckBox ID="SampleShipped2CheckBox" runat="server" Checked='<%# Bind("SampleShipped2") %>' />
                <br />
                Sample Shipped 3:
                <asp:CheckBox ID="SampleShipped3CheckBox" runat="server" Checked='<%# Bind("SampleShipped3") %>' />
                <br />
                Sample Shipped 4:
                <asp:CheckBox ID="SampleShipped4CheckBox" runat="server" Checked='<%# Bind("SampleShipped4") %>' />
                <br />
                Number Of Samples Jan:
                <asp:TextBox ID="NumberOfSamplesJanTextBox" runat="server" Text='<%# Bind("NumberOfSamplesJan") %>' />
                <br />
                Number Of Samples Feb:
                <asp:TextBox ID="NumberOfSamplesFebTextBox" runat="server" Text='<%# Bind("NumberOfSamplesFeb") %>' />
                <br />
                Number Of Samples Mar:
                <asp:TextBox ID="NumberOfSamplesMarTextBox" runat="server" Text='<%# Bind("NumberOfSamplesMar") %>' />
                <br />
                Number Of Samples Apr:
                <asp:TextBox ID="NumberOfSamplesAprTextBox" runat="server" Text='<%# Bind("NumberOfSamplesApr") %>' />
                <br />
                Number Of Samples May:
                <asp:TextBox ID="NumberOfSamplesMayTextBox" runat="server" Text='<%# Bind("NumberOfSamplesMay") %>' />
                <br />
                Number Of Samples Jun:
                <asp:TextBox ID="NumberOfSamplesJunTextBox" runat="server" Text='<%# Bind("NumberOfSamplesJun") %>' />
                <br />
                Number Of Samples Jul:
                <asp:TextBox ID="NumberOfSamplesJulTextBox" runat="server" Text='<%# Bind("NumberOfSamplesJul") %>' />
                <br />
                Number Of Samples Aug:
                <asp:TextBox ID="NumberOfSamplesAugTextBox" runat="server" Text='<%# Bind("NumberOfSamplesAug") %>' />
                <br />
                Number Of Samples Sep:
                <asp:TextBox ID="NumberOfSamplesSepTextBox" runat="server" Text='<%# Bind("NumberOfSamplesSep") %>' />
                <br />
                Number Of Samples Oct:
                <asp:TextBox ID="NumberOfSamplesOctTextBox" runat="server" Text='<%# Bind("NumberOfSamplesOct") %>' />
                <br />
                Number Of Samples Nov:
                <asp:TextBox ID="NumberOfSamplesNovTextBox" runat="server" Text='<%# Bind("NumberOfSamplesNov") %>' />
                <br />
                Number Of Samples Dec:
                <asp:TextBox ID="NumberOfSamplesDecTextBox" runat="server" Text='<%# Bind("NumberOfSamplesDec") %>' />
                <br />
                Nutrient 1 Collected:
                <asp:CheckBox ID="Nutrient1CollectedCheckBox" runat="server" Checked='<%# Bind("Nutrient1Collected") %>' />
                <br />
                Nutrient 2 Collected:
                <asp:CheckBox ID="Nutrient2CollectedCheckBox" runat="server" Checked='<%# Bind("Nutrient2Collected") %>' />
                <br />
                Bug Collected:
                <asp:CheckBox ID="BugCollectedCheckBox" runat="server" Checked='<%# Bind("BugCollected") %>' />
                <br />
                Unknown Spring Recorded Date:
                <asp:TextBox ID="UnknownSpringRecordedDateTextBox" runat="server" Text='<%# Bind("UnknownSpringRecordedDate") %>' />
                <br />
                Unknown Fall Recorded Date:
                <asp:TextBox ID="UnknownFallRecordedDateTextBox" runat="server" Text='<%# Bind("UnknownFallRecordedDate") %>' />
                <br />
                Number Of Samples Blank:
                <asp:TextBox ID="NumberOfSamplesBlankTextBox" runat="server" Text='<%# Bind("NumberOfSamplesBlank") %>' />
                <br />
                Number Of Samples Duplicate:
                <asp:TextBox ID="NumberOfSamplesDuplicateTextBox" runat="server" Text='<%# Bind("NumberOfSamplesDuplicate") %>' />
                <br />
                Trouble Comment:
                <asp:TextBox ID="TroubleCommentTextBox" runat="server" Text='<%# Bind("TroubleComment") %>' />
                <br />
                Note Comment:
                <asp:TextBox ID="NoteCommentTextBox" runat="server" Text='<%# Bind("NoteComment") %>' />
                <br />
                Hardship Comment:
                <asp:TextBox ID="HardshipCommentTextBox" runat="server" Text='<%# Bind("HardshipComment") %>' />
                <br />
                Date Created:
                <asp:TextBox ID="DateCreatedTextBox" ReadOnly="true" runat="server" Text='<%# Bind("DateCreated") %>' />
                <br />
                User Created:
                <asp:TextBox ID="UserCreatedTextBox" ReadOnly="true" runat="server" Text='<%# Bind("UserCreated") %>' />
                <br />
                <asp:TextBox ID="OrganizationIDTextBox" BackColor="White" runat="server" Text='<%# Bind("OrganizationID") %>' />
                <br />
                
                <asp:Button ID="UpdateButton" CssClass="adminButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
                &nbsp;<asp:Button ID="UpdateCancelButton" CssClass="adminButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
            </EditItemTemplate>
            <InsertItemTemplate>

                Contract Start Date:
                <asp:TextBox ID="ContractStartDateTextBox" runat="server" Text='<%# Bind("ContractStartDate") %>' />
                <ajaxToolkit:CalendarExtender TargetControlID="ContractStartDateTextBox" ID="CalendarExtender1" runat="server" />
                <br />
                Contract End Date:
                <asp:TextBox ID="ContractEndDateTextBox" runat="server" Text='<%# Bind("ContractEndDate") %>' />
                <ajaxToolkit:CalendarExtender TargetControlID="ContractEndDateTextBox" ID="CalendarExtender2" runat="server" />
                <br />
                Contract Signed:
                <asp:CheckBox ID="ContractSignedCheckBox" runat="server" Checked='<%# Bind("ContractSigned") %>' />
                <br />
                Contract Signed Date:
                <asp:TextBox ID="ContractSignedDateTextBox" runat="server" Text='<%# Bind("ContractSignedDate") %>' />
                <ajaxToolkit:CalendarExtender TargetControlID="ContractSignedDateTextBox" ID="CalendarExtender3" runat="server" />
                <br />
                Site Visited:
                <asp:CheckBox ID="SiteVisitedCheckBox" runat="server" Checked='<%# Bind("SiteVisited") %>' />
                <br />
                Volunteer Time Sheet 1:
                <asp:CheckBox ID="VolunteerTimeSheet1CheckBox" runat="server" Checked='<%# Bind("VolunteerTimeSheet1") %>' />
                <br />
                Volunteer Time Shee 2:
                <asp:CheckBox ID="VolunteerTimeShee2CheckBox" runat="server" Checked='<%# Bind("VolunteerTimeShee2") %>' />
                <br />
                Volunteer Time Sheet 3:
                <asp:CheckBox ID="VolunteerTimeSheet3CheckBox" runat="server" Checked='<%# Bind("VolunteerTimeSheet3") %>' />
                <br />
                Volunteer Time Sheet 4:
                <asp:CheckBox ID="VolunteerTimeSheet4CheckBox" runat="server" Checked='<%# Bind("VolunteerTimeSheet4") %>' />
                <br />
                Data Entered Electronically 1:
                <asp:CheckBox ID="DataEnteredElectronically1CheckBox" runat="server" Checked='<%# Bind("DataEnteredElectronically1") %>' />
                <br />
                Data Entered Electronically 2:
                <asp:CheckBox ID="DataEnteredElectronically2CheckBox" runat="server" Checked='<%# Bind("DataEnteredElectronically2") %>' />
                <br />
                Data Entered Electronically 3:
                <asp:CheckBox ID="DataEnteredElectronically3CheckBox" runat="server" Checked='<%# Bind("DataEnteredElectronically3") %>' />
                <br />
                Data Entered Electronically 4:
                <asp:CheckBox ID="DataEnteredElectronically4CheckBox" runat="server" Checked='<%# Bind("DataEnteredElectronically4") %>' />
                <br />
                Sample Shipped 1:
                <asp:CheckBox ID="SampleShipped1CheckBox" runat="server" Checked='<%# Bind("SampleShipped1") %>' />
                <br />
                Sample Shipped 2:
                <asp:CheckBox ID="SampleShipped2CheckBox" runat="server" Checked='<%# Bind("SampleShipped2") %>' />
                <br />
                Sample Shipped 3:
                <asp:CheckBox ID="SampleShipped3CheckBox" runat="server" Checked='<%# Bind("SampleShipped3") %>' />
                <br />
                Sample Shipped 4:
                <asp:CheckBox ID="SampleShipped4CheckBox" runat="server" Checked='<%# Bind("SampleShipped4") %>' />
                <br />
                Number Of Samples Jan:
                <asp:TextBox ID="NumberOfSamplesJanTextBox" runat="server" Text='<%# Bind("NumberOfSamplesJan") %>' />
                <br />
                Number Of Samples Feb:
                <asp:TextBox ID="NumberOfSamplesFebTextBox" runat="server" Text='<%# Bind("NumberOfSamplesFeb") %>' />
                <br />
                Number Of Samples Mar:
                <asp:TextBox ID="NumberOfSamplesMarTextBox" runat="server" Text='<%# Bind("NumberOfSamplesMar") %>' />
                <br />
                Number Of Samples Apr:
                <asp:TextBox ID="NumberOfSamplesAprTextBox" runat="server" Text='<%# Bind("NumberOfSamplesApr") %>' />
                <br />
                Number Of Samples May:
                <asp:TextBox ID="NumberOfSamplesMayTextBox" runat="server" Text='<%# Bind("NumberOfSamplesMay") %>' />
                <br />
                Number Of Samples Jun:
                <asp:TextBox ID="NumberOfSamplesJunTextBox" runat="server" Text='<%# Bind("NumberOfSamplesJun") %>' />
                <br />
                Number Of Samples Jul:
                <asp:TextBox ID="NumberOfSamplesJulTextBox" runat="server" Text='<%# Bind("NumberOfSamplesJul") %>' />
                <br />
                Number Of Samples Aug:
                <asp:TextBox ID="NumberOfSamplesAugTextBox" runat="server" Text='<%# Bind("NumberOfSamplesAug") %>' />
                <br />
                Number Of Samples Sep:
                <asp:TextBox ID="NumberOfSamplesSepTextBox" runat="server" Text='<%# Bind("NumberOfSamplesSep") %>' />
                <br />
                Number Of Samples Oct:
                <asp:TextBox ID="NumberOfSamplesOctTextBox" runat="server" Text='<%# Bind("NumberOfSamplesOct") %>' />
                <br />
                Number Of Samples Nov:
                <asp:TextBox ID="NumberOfSamplesNovTextBox" runat="server" Text='<%# Bind("NumberOfSamplesNov") %>' />
                <br />
                Number Of Samples Dec:
                <asp:TextBox ID="NumberOfSamplesDecTextBox" runat="server" Text='<%# Bind("NumberOfSamplesDec") %>' />
                <br />
                Nutrient 1 Collected:
                <asp:CheckBox ID="Nutrient1CollectedCheckBox" runat="server" Checked='<%# Bind("Nutrient1Collected") %>' />
                <br />
                Nutrient 2 Collected:
                <asp:CheckBox ID="Nutrient2CollectedCheckBox" runat="server" Checked='<%# Bind("Nutrient2Collected") %>' />
                <br />
                Bug Collected:
                <asp:CheckBox ID="BugCollectedCheckBox" runat="server" Checked='<%# Bind("BugCollected") %>' />
                <br />
                Unknown Spring Recorded Date:
                <asp:TextBox ID="UnknownSpringRecordedDateTextBox" runat="server" Text='<%# Bind("UnknownSpringRecordedDate") %>' />
                <br />
                Unknown Fall Recorded Date:
                <asp:TextBox ID="UnknownFallRecordedDateTextBox" runat="server" Text='<%# Bind("UnknownFallRecordedDate") %>' />
                <br />
                Number Of Samples Blank:
                <asp:TextBox ID="NumberOfSamplesBlankTextBox" runat="server" Text='<%# Bind("NumberOfSamplesBlank") %>' />
                <br />
                Number Of Samples Duplicate:
                <asp:TextBox ID="NumberOfSamplesDuplicateTextBox" runat="server" Text='<%# Bind("NumberOfSamplesDuplicate") %>' />
                <br />
                Trouble Comment:
                <asp:TextBox ID="TroubleCommentTextBox" runat="server" Text='<%# Bind("TroubleComment") %>' />
                <br />
                Note Comment:
                <asp:TextBox ID="NoteCommentTextBox" runat="server" Text='<%# Bind("NoteComment") %>' />
                <br />
                Hardship Comment:
                <asp:TextBox ID="HardshipCommentTextBox" runat="server" Text='<%# Bind("HardshipComment") %>' />
                <br />
                Date Created:
                <asp:TextBox ID="DateCreatedTextBox" ReadOnly="true" runat="server" Text='<%# Bind("DateCreated") %>' />
                <br />
                User Created:
                <asp:TextBox ID="UserCreatedTextBox" ReadOnly="true" runat="server" Text='<%# Bind("UserCreated") %>' />

                <asp:TextBox ID="OrganizationIDTextBox"  BackColor="White" runat="server" Text='<%# Bind("OrganizationID") %>' />
                <br />

                <asp:Button ID="InsertButton" CssClass="adminButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
                &nbsp;<asp:Button ID="InsertCancelButton" CssClass="adminButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
            </InsertItemTemplate>
            <ItemTemplate>
                Organization ID:
                <asp:Label ID="OrganizationIDLabel" runat="server" Text='<%# Bind("OrganizationID") %>' />
                <br />
                Contract Start Date:
                <asp:Label ID="ContractStartDateLabel" runat="server" Text='<%# Bind("ContractStartDate") %>' />
                <br />
                Contract End Date:
                <asp:Label ID="ContractEndDateLabel" runat="server" Text='<%# Bind("ContractEndDate") %>' />
                <br />
                Contract Signed:
                <asp:CheckBox ID="ContractSignedCheckBox" runat="server" Checked='<%# Bind("ContractSigned") %>' Enabled="false" />
                <br />
                Contract Signed Date:
                <asp:Label ID="ContractSignedDateLabel" runat="server" Text='<%# Bind("ContractSignedDate") %>' />
                <br />
                Site Visited:
                <asp:CheckBox ID="SiteVisitedCheckBox" runat="server" Checked='<%# Bind("SiteVisited") %>' Enabled="false" />
                <br />
                Volunteer Time Sheet 1:
                <asp:CheckBox ID="VolunteerTimeSheet1CheckBox" runat="server" Checked='<%# Bind("VolunteerTimeSheet1") %>' Enabled="false" />
                <br />
                Volunteer Time Shee 2:
                <asp:CheckBox ID="VolunteerTimeShee2CheckBox" runat="server" Checked='<%# Bind("VolunteerTimeShee2") %>' Enabled="false" />
                <br />
                Volunteer Time Sheet 3:
                <asp:CheckBox ID="VolunteerTimeSheet3CheckBox" runat="server" Checked='<%# Bind("VolunteerTimeSheet3") %>' Enabled="false" />
                <br />
                Volunteer Time Sheet 4:
                <asp:CheckBox ID="VolunteerTimeSheet4CheckBox" runat="server" Checked='<%# Bind("VolunteerTimeSheet4") %>' Enabled="false" />
                <br />
                Data Entered Electronically 1:
                <asp:CheckBox ID="DataEnteredElectronically1CheckBox" runat="server" Checked='<%# Bind("DataEnteredElectronically1") %>' Enabled="false" />
                <br />
                Data Entered Electronically 2:
                <asp:CheckBox ID="DataEnteredElectronically2CheckBox" runat="server" Checked='<%# Bind("DataEnteredElectronically2") %>' Enabled="false" />
                <br />
                Data Entered Electronically 3:
                <asp:CheckBox ID="DataEnteredElectronically3CheckBox" runat="server" Checked='<%# Bind("DataEnteredElectronically3") %>' Enabled="false" />
                <br />
                Data Entered Electronically 4:
                <asp:CheckBox ID="DataEnteredElectronically4CheckBox" runat="server" Checked='<%# Bind("DataEnteredElectronically4") %>' Enabled="false" />
                <br />
                Sample Shipped 1:
                <asp:CheckBox ID="SampleShipped1CheckBox" runat="server" Checked='<%# Bind("SampleShipped1") %>' Enabled="false" />
                <br />
                Sample Shipped 2:
                <asp:CheckBox ID="SampleShipped2CheckBox" runat="server" Checked='<%# Bind("SampleShipped2") %>' Enabled="false" />
                <br />
                Sample Shipped 3:
                <asp:CheckBox ID="SampleShipped3CheckBox" runat="server" Checked='<%# Bind("SampleShipped3") %>' Enabled="false" />
                <br />
                Sample Shipped 4:
                <asp:CheckBox ID="SampleShipped4CheckBox" runat="server" Checked='<%# Bind("SampleShipped4") %>' Enabled="false" />
                <br />
                Number Of Samples Jan:
                <asp:Label ID="NumberOfSamplesJanLabel" runat="server" Text='<%# Bind("NumberOfSamplesJan") %>' />
                <br />
                Number Of Samples Feb:
                <asp:Label ID="NumberOfSamplesFebLabel" runat="server" Text='<%# Bind("NumberOfSamplesFeb") %>' />
                <br />
                Number Of Samples Mar:
                <asp:Label ID="NumberOfSamplesMarLabel" runat="server" Text='<%# Bind("NumberOfSamplesMar") %>' />
                <br />
                Number Of Samples Apr:
                <asp:Label ID="NumberOfSamplesAprLabel" runat="server" Text='<%# Bind("NumberOfSamplesApr") %>' />
                <br />
                Number Of Samples May:
                <asp:Label ID="NumberOfSamplesMayLabel" runat="server" Text='<%# Bind("NumberOfSamplesMay") %>' />
                <br />
                Number Of Samples Jun:
                <asp:Label ID="NumberOfSamplesJunLabel" runat="server" Text='<%# Bind("NumberOfSamplesJun") %>' />
                <br />
                Number Of Samples Jul:
                <asp:Label ID="NumberOfSamplesJulLabel" runat="server" Text='<%# Bind("NumberOfSamplesJul") %>' />
                <br />
                Number Of Samples Aug:
                <asp:Label ID="NumberOfSamplesAugLabel" runat="server" Text='<%# Bind("NumberOfSamplesAug") %>' />
                <br />
                Number Of Samples Sep:
                <asp:Label ID="NumberOfSamplesSepLabel" runat="server" Text='<%# Bind("NumberOfSamplesSep") %>' />
                <br />
                Number Of Samples Oct:
                <asp:Label ID="NumberOfSamplesOctLabel" runat="server" Text='<%# Bind("NumberOfSamplesOct") %>' />
                <br />
                Number Of Samples Nov:
                <asp:Label ID="NumberOfSamplesNovLabel" runat="server" Text='<%# Bind("NumberOfSamplesNov") %>' />
                <br />
                Number Of Samples Dec:
                <asp:Label ID="NumberOfSamplesDecLabel" runat="server" Text='<%# Bind("NumberOfSamplesDec") %>' />
                <br />
                Nutrient 1 Collected:
                <asp:CheckBox ID="Nutrient1CollectedCheckBox" runat="server" Checked='<%# Bind("Nutrient1Collected") %>' Enabled="false" />
                <br />
                Nutrient 2 Collected:
                <asp:CheckBox ID="Nutrient2CollectedCheckBox" runat="server" Checked='<%# Bind("Nutrient2Collected") %>' Enabled="false" />
                <br />
                Bug Collected:
                <asp:CheckBox ID="BugCollectedCheckBox" runat="server" Checked='<%# Bind("BugCollected") %>' Enabled="false" />
                <br />
                Unknown Spring Recorded Date:
                <asp:Label ID="UnknownSpringRecordedDateLabel" runat="server" Text='<%# Bind("UnknownSpringRecordedDate") %>' />
                <br />
                Unknown Fall Recorded Date:
                <asp:Label ID="UnknownFallRecordedDateLabel" runat="server" Text='<%# Bind("UnknownFallRecordedDate") %>' />
                <br />
                Number Of Samples Blank:
                <asp:Label ID="NumberOfSamplesBlankLabel" runat="server" Text='<%# Bind("NumberOfSamplesBlank") %>' />
                <br />
                Number Of Samples Duplicate:
                <asp:Label ID="NumberOfSamplesDuplicateLabel" runat="server" Text='<%# Bind("NumberOfSamplesDuplicate") %>' />
                <br />
                Trouble Comment:
                <asp:Label ID="TroubleCommentLabel" runat="server" Text='<%# Bind("TroubleComment") %>' />
                <br />
                Note Comment:
                <asp:Label ID="NoteCommentLabel" runat="server" Text='<%# Bind("NoteComment") %>' />
                <br />
                Hardship Comment:
                <asp:Label ID="HardshipCommentLabel" runat="server" Text='<%# Bind("HardshipComment") %>' />
                <br />
                Date Created:
                <asp:Label ID="DateCreatedLabel" runat="server" Text='<%# Bind("DateCreated") %>' />
                <br />
                User Created:
                <asp:Label ID="UserCreatedLabel" runat="server" Text='<%# Bind("UserCreated") %>' />
                <br />
                
                <asp:Button ID="EditButton" CssClass="adminButton" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit" />
                &nbsp;<asp:Button CssClass="adminButton" ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" />
                &nbsp;<asp:Button CssClass="adminButton" ID="NewButton" runat="server" CausesValidation="False" CommandName="New" Text="New" />
            </ItemTemplate>

<PagerSettings Position="TopAndBottom"></PagerSettings>
        </asp:FormView>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" OnInserting="SqlDataSource1_Inserting1" ConnectionString="<%$ ConnectionStrings:RiverWatchDEV %>" 
            DeleteCommand="DELETE FROM [OrgStatus] WHERE [ID] = @ID" 
            InsertCommand="INSERT INTO [OrgStatus] ([OrganizationID], [ContractStartDate], [ContractEndDate], [ContractSigned], [ContractSignedDate], [SiteVisited], [VolunteerTimeSheet1], [VolunteerTimeShee2], [VolunteerTimeSheet3], [VolunteerTimeSheet4], [DataEnteredElectronically1], [DataEnteredElectronically2], [DataEnteredElectronically3], [DataEnteredElectronically4], [SampleShipped1], [SampleShipped2], [SampleShipped3], [SampleShipped4], [NumberOfSamplesJan], [NumberOfSamplesFeb], [NumberOfSamplesMar], [NumberOfSamplesApr], [NumberOfSamplesMay], [NumberOfSamplesJun], [NumberOfSamplesJul], [NumberOfSamplesAug], [NumberOfSamplesSep], [NumberOfSamplesOct], [NumberOfSamplesNov], [NumberOfSamplesDec], [Nutrient1Collected], [Nutrient2Collected], [BugCollected], [UnknownSpringRecordedDate], [UnknownFallRecordedDate], [NumberOfSamplesBlank], [NumberOfSamplesDuplicate], [TroubleComment], [NoteComment], [HardshipComment], [DateCreated], [UserCreated]) VALUES (@OrganizationID, @ContractStartDate, @ContractEndDate, @ContractSigned, @ContractSignedDate, @SiteVisited, @VolunteerTimeSheet1, @VolunteerTimeShee2, @VolunteerTimeSheet3, @VolunteerTimeSheet4, @DataEnteredElectronically1, @DataEnteredElectronically2, @DataEnteredElectronically3, @DataEnteredElectronically4, @SampleShipped1, @SampleShipped2, @SampleShipped3, @SampleShipped4, @NumberOfSamplesJan, @NumberOfSamplesFeb, @NumberOfSamplesMar, @NumberOfSamplesApr, @NumberOfSamplesMay, @NumberOfSamplesJun, @NumberOfSamplesJul, @NumberOfSamplesAug, @NumberOfSamplesSep, @NumberOfSamplesOct, @NumberOfSamplesNov, @NumberOfSamplesDec, @Nutrient1Collected, @Nutrient2Collected, @BugCollected, @UnknownSpringRecordedDate, @UnknownFallRecordedDate, @NumberOfSamplesBlank, @NumberOfSamplesDuplicate, @TroubleComment, @NoteComment, @HardshipComment, @DateCreated, @UserCreated)" 
            SelectCommand="SELECT * FROM [OrgStatus]" 
            UpdateCommand="UPDATE [OrgStatus] SET [OrganizationID] = @OrganizationID, [ContractStartDate] = @ContractStartDate, [ContractEndDate] = @ContractEndDate, [ContractSigned] = @ContractSigned, [ContractSignedDate] = @ContractSignedDate, [SiteVisited] = @SiteVisited, [VolunteerTimeSheet1] = @VolunteerTimeSheet1, [VolunteerTimeShee2] = @VolunteerTimeShee2, [VolunteerTimeSheet3] = @VolunteerTimeSheet3, [VolunteerTimeSheet4] = @VolunteerTimeSheet4, [DataEnteredElectronically1] = @DataEnteredElectronically1, [DataEnteredElectronically2] = @DataEnteredElectronically2, [DataEnteredElectronically3] = @DataEnteredElectronically3, [DataEnteredElectronically4] = @DataEnteredElectronically4, [SampleShipped1] = @SampleShipped1, [SampleShipped2] = @SampleShipped2, [SampleShipped3] = @SampleShipped3, [SampleShipped4] = @SampleShipped4, [NumberOfSamplesJan] = @NumberOfSamplesJan, [NumberOfSamplesFeb] = @NumberOfSamplesFeb, [NumberOfSamplesMar] = @NumberOfSamplesMar, [NumberOfSamplesApr] = @NumberOfSamplesApr, [NumberOfSamplesMay] = @NumberOfSamplesMay, [NumberOfSamplesJun] = @NumberOfSamplesJun, [NumberOfSamplesJul] = @NumberOfSamplesJul, [NumberOfSamplesAug] = @NumberOfSamplesAug, [NumberOfSamplesSep] = @NumberOfSamplesSep, [NumberOfSamplesOct] = @NumberOfSamplesOct, [NumberOfSamplesNov] = @NumberOfSamplesNov, [NumberOfSamplesDec] = @NumberOfSamplesDec, [Nutrient1Collected] = @Nutrient1Collected, [Nutrient2Collected] = @Nutrient2Collected, [BugCollected] = @BugCollected, [UnknownSpringRecordedDate] = @UnknownSpringRecordedDate, [UnknownFallRecordedDate] = @UnknownFallRecordedDate, [NumberOfSamplesBlank] = @NumberOfSamplesBlank, [NumberOfSamplesDuplicate] = @NumberOfSamplesDuplicate, [TroubleComment] = @TroubleComment, [NoteComment] = @NoteComment, [HardshipComment] = @HardshipComment, [DateCreated] = @DateCreated, [UserCreated] = @UserCreated WHERE [ID] = @ID">
            <DeleteParameters>
                <asp:Parameter Name="ID" Type="Int32" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="OrganizationID" Type="Int32" />
                <asp:Parameter Name="ContractStartDate" Type="DateTime" />
                <asp:Parameter Name="ContractEndDate" Type="DateTime" />
                <asp:Parameter Name="ContractSigned" Type="Boolean" />
                <asp:Parameter Name="ContractSignedDate" Type="DateTime" />
                <asp:Parameter Name="SiteVisited" Type="Boolean" />
                <asp:Parameter Name="VolunteerTimeSheet1" Type="Boolean" />
                <asp:Parameter Name="VolunteerTimeShee2" Type="Boolean" />
                <asp:Parameter Name="VolunteerTimeSheet3" Type="Boolean" />
                <asp:Parameter Name="VolunteerTimeSheet4" Type="Boolean" />
                <asp:Parameter Name="DataEnteredElectronically1" Type="Boolean" />
                <asp:Parameter Name="DataEnteredElectronically2" Type="Boolean" />
                <asp:Parameter Name="DataEnteredElectronically3" Type="Boolean" />
                <asp:Parameter Name="DataEnteredElectronically4" Type="Boolean" />
                <asp:Parameter Name="SampleShipped1" Type="Boolean" />
                <asp:Parameter Name="SampleShipped2" Type="Boolean" />
                <asp:Parameter Name="SampleShipped3" Type="Boolean" />
                <asp:Parameter Name="SampleShipped4" Type="Boolean" />
                <asp:Parameter Name="NumberOfSamplesJan" Type="Int32" />
                <asp:Parameter Name="NumberOfSamplesFeb" Type="Int32" />
                <asp:Parameter Name="NumberOfSamplesMar" Type="Int32" />
                <asp:Parameter Name="NumberOfSamplesApr" Type="Int32" />
                <asp:Parameter Name="NumberOfSamplesMay" Type="Int32" />
                <asp:Parameter Name="NumberOfSamplesJun" Type="Int32" />
                <asp:Parameter Name="NumberOfSamplesJul" Type="Int32" />
                <asp:Parameter Name="NumberOfSamplesAug" Type="Int32" />
                <asp:Parameter Name="NumberOfSamplesSep" Type="Int32" />
                <asp:Parameter Name="NumberOfSamplesOct" Type="Int32" />
                <asp:Parameter Name="NumberOfSamplesNov" Type="Int32" />
                <asp:Parameter Name="NumberOfSamplesDec" Type="Int32" />
                <asp:Parameter Name="Nutrient1Collected" Type="Boolean" />
                <asp:Parameter Name="Nutrient2Collected" Type="Boolean" />
                <asp:Parameter Name="BugCollected" Type="Boolean" />
                <asp:Parameter Name="UnknownSpringRecordedDate" Type="DateTime" />
                <asp:Parameter Name="UnknownFallRecordedDate" Type="DateTime" />
                <asp:Parameter Name="NumberOfSamplesBlank" Type="Int32" />
                <asp:Parameter Name="NumberOfSamplesDuplicate" Type="Int32" />
                <asp:Parameter Name="TroubleComment" Type="String" />
                <asp:Parameter Name="NoteComment" Type="String" />
                <asp:Parameter Name="HardshipComment" Type="String" />
                <asp:Parameter Name="DateCreated" Type="DateTime" />
                <asp:Parameter Name="UserCreated" Type="String" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="OrganizationID" Type="Int32" />
                <asp:Parameter Name="ContractStartDate" Type="DateTime" />
                <asp:Parameter Name="ContractEndDate" Type="DateTime" />
                <asp:Parameter Name="ContractSigned" Type="Boolean" />
                <asp:Parameter Name="ContractSignedDate" Type="DateTime" />
                <asp:Parameter Name="SiteVisited" Type="Boolean" />
                <asp:Parameter Name="VolunteerTimeSheet1" Type="Boolean" />
                <asp:Parameter Name="VolunteerTimeShee2" Type="Boolean" />
                <asp:Parameter Name="VolunteerTimeSheet3" Type="Boolean" />
                <asp:Parameter Name="VolunteerTimeSheet4" Type="Boolean" />
                <asp:Parameter Name="DataEnteredElectronically1" Type="Boolean" />
                <asp:Parameter Name="DataEnteredElectronically2" Type="Boolean" />
                <asp:Parameter Name="DataEnteredElectronically3" Type="Boolean" />
                <asp:Parameter Name="DataEnteredElectronically4" Type="Boolean" />
                <asp:Parameter Name="SampleShipped1" Type="Boolean" />
                <asp:Parameter Name="SampleShipped2" Type="Boolean" />
                <asp:Parameter Name="SampleShipped3" Type="Boolean" />
                <asp:Parameter Name="SampleShipped4" Type="Boolean" />
                <asp:Parameter Name="NumberOfSamplesJan" Type="Int32" />
                <asp:Parameter Name="NumberOfSamplesFeb" Type="Int32" />
                <asp:Parameter Name="NumberOfSamplesMar" Type="Int32" />
                <asp:Parameter Name="NumberOfSamplesApr" Type="Int32" />
                <asp:Parameter Name="NumberOfSamplesMay" Type="Int32" />
                <asp:Parameter Name="NumberOfSamplesJun" Type="Int32" />
                <asp:Parameter Name="NumberOfSamplesJul" Type="Int32" />
                <asp:Parameter Name="NumberOfSamplesAug" Type="Int32" />
                <asp:Parameter Name="NumberOfSamplesSep" Type="Int32" />
                <asp:Parameter Name="NumberOfSamplesOct" Type="Int32" />
                <asp:Parameter Name="NumberOfSamplesNov" Type="Int32" />
                <asp:Parameter Name="NumberOfSamplesDec" Type="Int32" />
                <asp:Parameter Name="Nutrient1Collected" Type="Boolean" />
                <asp:Parameter Name="Nutrient2Collected" Type="Boolean" />
                <asp:Parameter Name="BugCollected" Type="Boolean" />
                <asp:Parameter Name="UnknownSpringRecordedDate" Type="DateTime" />
                <asp:Parameter Name="UnknownFallRecordedDate" Type="DateTime" />
                <asp:Parameter Name="NumberOfSamplesBlank" Type="Int32" />
                <asp:Parameter Name="NumberOfSamplesDuplicate" Type="Int32" />
                <asp:Parameter Name="TroubleComment" Type="String" />
                <asp:Parameter Name="NoteComment" Type="String" />
                <asp:Parameter Name="HardshipComment" Type="String" />
                <asp:Parameter Name="DateCreated" Type="DateTime" />
                <asp:Parameter Name="UserCreated" Type="String" />
                <asp:Parameter Name="ID" Type="Int32" />
            </UpdateParameters>
        </asp:SqlDataSource>
        <br />



</asp:Content>
