<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormViewOrgStatus.aspx.cs" Inherits="RWInbound2.Samples.FormViewOrgStatus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p>
     
        <br />
        <asp:FormView ID="FormView1" runat="server" DataKeyNames="ID" DefaultMode="Edit" DataSourceID="SqlDataSourceOrgStatus">
            <EditItemTemplate>
                ID:
                <asp:Label ID="IDLabel1" runat="server" Text='<%# Eval("ID") %>' />
                <br />
                OrganizationID:
                <asp:TextBox ID="OrganizationIDTextBox" runat="server" Text='<%# Bind("OrganizationID") %>' />
                <br />
                ContractStartDate:
                <asp:TextBox ID="ContractStartDateTextBox" runat="server" Text='<%# Bind("ContractStartDate") %>' />
                <br />
                ContractEndDate:
                <asp:TextBox ID="ContractEndDateTextBox" runat="server" Text='<%# Bind("ContractEndDate") %>' />
                <br />
                ContractSigned:
                <asp:CheckBox ID="ContractSignedCheckBox" runat="server" Checked='<%# Bind("ContractSigned") %>' />
                <br />
                ContractSignedDate:
                <asp:TextBox ID="ContractSignedDateTextBox" runat="server" Text='<%# Bind("ContractSignedDate") %>' />
                <br />
                SiteVisited:
                <asp:CheckBox ID="SiteVisitedCheckBox" runat="server" Checked='<%# Bind("SiteVisited") %>' />
                <br />
                VolunteerTimeSheet1:
                <asp:CheckBox ID="VolunteerTimeSheet1CheckBox" runat="server" Checked='<%# Bind("VolunteerTimeSheet1") %>' />
                <br />
                VolunteerTimeShee2:
                <asp:CheckBox ID="VolunteerTimeShee2CheckBox" runat="server" Checked='<%# Bind("VolunteerTimeShee2") %>' />
                <br />
                VolunteerTimeSheet3:
                <asp:CheckBox ID="VolunteerTimeSheet3CheckBox" runat="server" Checked='<%# Bind("VolunteerTimeSheet3") %>' />
                <br />
                VolunteerTimeSheet4:
                <asp:CheckBox ID="VolunteerTimeSheet4CheckBox" runat="server" Checked='<%# Bind("VolunteerTimeSheet4") %>' />
                <br />
                DataEnteredElectronically1:
                <asp:CheckBox ID="DataEnteredElectronically1CheckBox" runat="server" Checked='<%# Bind("DataEnteredElectronically1") %>' />
                <br />
                DataEnteredElectronically2:
                <asp:CheckBox ID="DataEnteredElectronically2CheckBox" runat="server" Checked='<%# Bind("DataEnteredElectronically2") %>' />
                <br />
                DataEnteredElectronically3:
                <asp:CheckBox ID="DataEnteredElectronically3CheckBox" runat="server" Checked='<%# Bind("DataEnteredElectronically3") %>' />
                <br />
                DataEnteredElectronically4:
                <asp:CheckBox ID="DataEnteredElectronically4CheckBox" runat="server" Checked='<%# Bind("DataEnteredElectronically4") %>' />
                <br />
                SampleShipped1:
                <asp:CheckBox ID="SampleShipped1CheckBox" runat="server" Checked='<%# Bind("SampleShipped1") %>' />
                <br />
                SampleShipped2:
                <asp:CheckBox ID="SampleShipped2CheckBox" runat="server" Checked='<%# Bind("SampleShipped2") %>' />
                <br />
                SampleShipped3:
                <asp:CheckBox ID="SampleShipped3CheckBox" runat="server" Checked='<%# Bind("SampleShipped3") %>' />
                <br />
                SampleShipped4:
                <asp:CheckBox ID="SampleShipped4CheckBox" runat="server" Checked='<%# Bind("SampleShipped4") %>' />
                <br />
                NumberOfSamplesJan:
                <asp:TextBox ID="NumberOfSamplesJanTextBox" runat="server" Text='<%# Bind("NumberOfSamplesJan") %>' />
                <br />
                NumberOfSamplesFeb:
                <asp:TextBox ID="NumberOfSamplesFebTextBox" runat="server" Text='<%# Bind("NumberOfSamplesFeb") %>' />
                <br />
                NumberOfSamplesMar:
                <asp:TextBox ID="NumberOfSamplesMarTextBox" runat="server" Text='<%# Bind("NumberOfSamplesMar") %>' />
                <br />
                NumberOfSamplesApr:
                <asp:TextBox ID="NumberOfSamplesAprTextBox" runat="server" Text='<%# Bind("NumberOfSamplesApr") %>' />
                <br />
                NumberOfSamplesMay:
                <asp:TextBox ID="NumberOfSamplesMayTextBox" runat="server" Text='<%# Bind("NumberOfSamplesMay") %>' />
                <br />
                NumberOfSamplesJun:
                <asp:TextBox ID="NumberOfSamplesJunTextBox" runat="server" Text='<%# Bind("NumberOfSamplesJun") %>' />
                <br />
                NumberOfSamplesJul:
                <asp:TextBox ID="NumberOfSamplesJulTextBox" runat="server" Text='<%# Bind("NumberOfSamplesJul") %>' />
                <br />
                NumberOfSamplesAug:
                <asp:TextBox ID="NumberOfSamplesAugTextBox" runat="server" Text='<%# Bind("NumberOfSamplesAug") %>' />
                <br />
                NumberOfSamplesSep:
                <asp:TextBox ID="NumberOfSamplesSepTextBox" runat="server" Text='<%# Bind("NumberOfSamplesSep") %>' />
                <br />
                NumberOfSamplesOct:
                <asp:TextBox ID="NumberOfSamplesOctTextBox" runat="server" Text='<%# Bind("NumberOfSamplesOct") %>' />
                <br />
                NumberOfSamplesNov:
                <asp:TextBox ID="NumberOfSamplesNovTextBox" runat="server" Text='<%# Bind("NumberOfSamplesNov") %>' />
                <br />
                NumberOfSamplesDec:
                <asp:TextBox ID="NumberOfSamplesDecTextBox" runat="server" Text='<%# Bind("NumberOfSamplesDec") %>' />
                <br />
                Nutrient1Collected:
                <asp:CheckBox ID="Nutrient1CollectedCheckBox" runat="server" Checked='<%# Bind("Nutrient1Collected") %>' />
                <br />
                Nutrient2Collected:
                <asp:CheckBox ID="Nutrient2CollectedCheckBox" runat="server" Checked='<%# Bind("Nutrient2Collected") %>' />
                <br />
                BugCollected:
                <asp:CheckBox ID="BugCollectedCheckBox" runat="server" Checked='<%# Bind("BugCollected") %>' />
                <br />
                UnknownSpringRecordedDate:
                <asp:TextBox ID="UnknownSpringRecordedDateTextBox" runat="server" Text='<%# Bind("UnknownSpringRecordedDate") %>' />
                <br />
                UnknownFallRecordedDate:
                <asp:TextBox ID="UnknownFallRecordedDateTextBox" runat="server" Text='<%# Bind("UnknownFallRecordedDate") %>' />
                <br />
                NumberOfSamplesBlank:
                <asp:TextBox ID="NumberOfSamplesBlankTextBox" runat="server" Text='<%# Bind("NumberOfSamplesBlank") %>' />
                <br />
                NumberOfSamplesDuplicate:
                <asp:TextBox ID="NumberOfSamplesDuplicateTextBox" runat="server" Text='<%# Bind("NumberOfSamplesDuplicate") %>' />
                <br />
                TroubleComment:
                <asp:TextBox ID="TroubleCommentTextBox" runat="server" Text='<%# Bind("TroubleComment") %>' />
                <br />
                NoteComment:
                <asp:TextBox ID="NoteCommentTextBox" runat="server" Text='<%# Bind("NoteComment") %>' />
                <br />
                HardshipComment:
                <asp:TextBox ID="HardshipCommentTextBox" runat="server" Text='<%# Bind("HardshipComment") %>' />
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
                NumberOfMetalsBlank:
                <asp:TextBox ID="NumberOfMetalsBlankTextBox" runat="server" Text='<%# Bind("NumberOfMetalsBlank") %>' />
                <br />
                NumberOfMetalsDuplicate:
                <asp:TextBox ID="NumberOfMetalsDuplicateTextBox" runat="server" Text='<%# Bind("NumberOfMetalsDuplicate") %>' />
                <br />
                <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
                &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
            </EditItemTemplate>
            <InsertItemTemplate>
                OrganizationID:
                <asp:TextBox ID="OrganizationIDTextBox" runat="server" Text='<%# Bind("OrganizationID") %>' />
                <br />
                ContractStartDate:
                <asp:TextBox ID="ContractStartDateTextBox" runat="server" Text='<%# Bind("ContractStartDate") %>' />
                <br />
                ContractEndDate:
                <asp:TextBox ID="ContractEndDateTextBox" runat="server" Text='<%# Bind("ContractEndDate") %>' />
                <br />
                ContractSigned:
                <asp:CheckBox ID="ContractSignedCheckBox" runat="server" Checked='<%# Bind("ContractSigned") %>' />
                <br />
                ContractSignedDate:
                <asp:TextBox ID="ContractSignedDateTextBox" runat="server" Text='<%# Bind("ContractSignedDate") %>' />
                <br />
                SiteVisited:
                <asp:CheckBox ID="SiteVisitedCheckBox" runat="server" Checked='<%# Bind("SiteVisited") %>' />
                <br />
                VolunteerTimeSheet1:
                <asp:CheckBox ID="VolunteerTimeSheet1CheckBox" runat="server" Checked='<%# Bind("VolunteerTimeSheet1") %>' />
                <br />
                VolunteerTimeShee2:
                <asp:CheckBox ID="VolunteerTimeShee2CheckBox" runat="server" Checked='<%# Bind("VolunteerTimeShee2") %>' />
                <br />
                VolunteerTimeSheet3:
                <asp:CheckBox ID="VolunteerTimeSheet3CheckBox" runat="server" Checked='<%# Bind("VolunteerTimeSheet3") %>' />
                <br />
                VolunteerTimeSheet4:
                <asp:CheckBox ID="VolunteerTimeSheet4CheckBox" runat="server" Checked='<%# Bind("VolunteerTimeSheet4") %>' />
                <br />
                DataEnteredElectronically1:
                <asp:CheckBox ID="DataEnteredElectronically1CheckBox" runat="server" Checked='<%# Bind("DataEnteredElectronically1") %>' />
                <br />
                DataEnteredElectronically2:
                <asp:CheckBox ID="DataEnteredElectronically2CheckBox" runat="server" Checked='<%# Bind("DataEnteredElectronically2") %>' />
                <br />
                DataEnteredElectronically3:
                <asp:CheckBox ID="DataEnteredElectronically3CheckBox" runat="server" Checked='<%# Bind("DataEnteredElectronically3") %>' />
                <br />
                DataEnteredElectronically4:
                <asp:CheckBox ID="DataEnteredElectronically4CheckBox" runat="server" Checked='<%# Bind("DataEnteredElectronically4") %>' />
                <br />
                SampleShipped1:
                <asp:CheckBox ID="SampleShipped1CheckBox" runat="server" Checked='<%# Bind("SampleShipped1") %>' />
                <br />
                SampleShipped2:
                <asp:CheckBox ID="SampleShipped2CheckBox" runat="server" Checked='<%# Bind("SampleShipped2") %>' />
                <br />
                SampleShipped3:
                <asp:CheckBox ID="SampleShipped3CheckBox" runat="server" Checked='<%# Bind("SampleShipped3") %>' />
                <br />
                SampleShipped4:
                <asp:CheckBox ID="SampleShipped4CheckBox" runat="server" Checked='<%# Bind("SampleShipped4") %>' />
                <br />
                NumberOfSamplesJan:
                <asp:TextBox ID="NumberOfSamplesJanTextBox" runat="server" Text='<%# Bind("NumberOfSamplesJan") %>' />
                <br />
                NumberOfSamplesFeb:
                <asp:TextBox ID="NumberOfSamplesFebTextBox" runat="server" Text='<%# Bind("NumberOfSamplesFeb") %>' />
                <br />
                NumberOfSamplesMar:
                <asp:TextBox ID="NumberOfSamplesMarTextBox" runat="server" Text='<%# Bind("NumberOfSamplesMar") %>' />
                <br />
                NumberOfSamplesApr:
                <asp:TextBox ID="NumberOfSamplesAprTextBox" runat="server" Text='<%# Bind("NumberOfSamplesApr") %>' />
                <br />
                NumberOfSamplesMay:
                <asp:TextBox ID="NumberOfSamplesMayTextBox" runat="server" Text='<%# Bind("NumberOfSamplesMay") %>' />
                <br />
                NumberOfSamplesJun:
                <asp:TextBox ID="NumberOfSamplesJunTextBox" runat="server" Text='<%# Bind("NumberOfSamplesJun") %>' />
                <br />
                NumberOfSamplesJul:
                <asp:TextBox ID="NumberOfSamplesJulTextBox" runat="server" Text='<%# Bind("NumberOfSamplesJul") %>' />
                <br />
                NumberOfSamplesAug:
                <asp:TextBox ID="NumberOfSamplesAugTextBox" runat="server" Text='<%# Bind("NumberOfSamplesAug") %>' />
                <br />
                NumberOfSamplesSep:
                <asp:TextBox ID="NumberOfSamplesSepTextBox" runat="server" Text='<%# Bind("NumberOfSamplesSep") %>' />
                <br />
                NumberOfSamplesOct:
                <asp:TextBox ID="NumberOfSamplesOctTextBox" runat="server" Text='<%# Bind("NumberOfSamplesOct") %>' />
                <br />
                NumberOfSamplesNov:
                <asp:TextBox ID="NumberOfSamplesNovTextBox" runat="server" Text='<%# Bind("NumberOfSamplesNov") %>' />
                <br />
                NumberOfSamplesDec:
                <asp:TextBox ID="NumberOfSamplesDecTextBox" runat="server" Text='<%# Bind("NumberOfSamplesDec") %>' />
                <br />
                Nutrient1Collected:
                <asp:CheckBox ID="Nutrient1CollectedCheckBox" runat="server" Checked='<%# Bind("Nutrient1Collected") %>' />
                <br />
                Nutrient2Collected:
                <asp:CheckBox ID="Nutrient2CollectedCheckBox" runat="server" Checked='<%# Bind("Nutrient2Collected") %>' />
                <br />
                BugCollected:
                <asp:CheckBox ID="BugCollectedCheckBox" runat="server" Checked='<%# Bind("BugCollected") %>' />
                <br />
                UnknownSpringRecordedDate:
                <asp:TextBox ID="UnknownSpringRecordedDateTextBox" runat="server" Text='<%# Bind("UnknownSpringRecordedDate") %>' />
                <br />
                UnknownFallRecordedDate:
                <asp:TextBox ID="UnknownFallRecordedDateTextBox" runat="server" Text='<%# Bind("UnknownFallRecordedDate") %>' />
                <br />
                NumberOfSamplesBlank:
                <asp:TextBox ID="NumberOfSamplesBlankTextBox" runat="server" Text='<%# Bind("NumberOfSamplesBlank") %>' />
                <br />
                NumberOfSamplesDuplicate:
                <asp:TextBox ID="NumberOfSamplesDuplicateTextBox" runat="server" Text='<%# Bind("NumberOfSamplesDuplicate") %>' />
                <br />
                TroubleComment:
                <asp:TextBox ID="TroubleCommentTextBox" runat="server" Text='<%# Bind("TroubleComment") %>' />
                <br />
                NoteComment:
                <asp:TextBox ID="NoteCommentTextBox" runat="server" Text='<%# Bind("NoteComment") %>' />
                <br />
                HardshipComment:
                <asp:TextBox ID="HardshipCommentTextBox" runat="server" Text='<%# Bind("HardshipComment") %>' />
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
                NumberOfMetalsBlank:
                <asp:TextBox ID="NumberOfMetalsBlankTextBox" runat="server" Text='<%# Bind("NumberOfMetalsBlank") %>' />
                <br />
                NumberOfMetalsDuplicate:
                <asp:TextBox ID="NumberOfMetalsDuplicateTextBox" runat="server" Text='<%# Bind("NumberOfMetalsDuplicate") %>' />
                <br />
                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
                &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
            </InsertItemTemplate>
            <ItemTemplate>
                ID:
                <asp:Label ID="IDLabel" runat="server" Text='<%# Eval("ID") %>' />
                <br />
                OrganizationID:
                <asp:Label ID="OrganizationIDLabel" runat="server" Text='<%# Bind("OrganizationID") %>' />
                <br />
                ContractStartDate:
                <asp:Label ID="ContractStartDateLabel" runat="server" Text='<%# Bind("ContractStartDate") %>' />
                <br />
                ContractEndDate:
                <asp:Label ID="ContractEndDateLabel" runat="server" Text='<%# Bind("ContractEndDate") %>' />
                <br />
                ContractSigned:
                <asp:CheckBox ID="ContractSignedCheckBox" runat="server" Checked='<%# Bind("ContractSigned") %>' Enabled="false" />
                <br />
                ContractSignedDate:
                <asp:Label ID="ContractSignedDateLabel" runat="server" Text='<%# Bind("ContractSignedDate") %>' />
                <br />
                SiteVisited:
                <asp:CheckBox ID="SiteVisitedCheckBox" runat="server" Checked='<%# Bind("SiteVisited") %>' Enabled="false" />
                <br />
                VolunteerTimeSheet1:
                <asp:CheckBox ID="VolunteerTimeSheet1CheckBox" runat="server" Checked='<%# Bind("VolunteerTimeSheet1") %>' Enabled="false" />
                <br />
                VolunteerTimeShee2:
                <asp:CheckBox ID="VolunteerTimeShee2CheckBox" runat="server" Checked='<%# Bind("VolunteerTimeShee2") %>' Enabled="false" />
                <br />
                VolunteerTimeSheet3:
                <asp:CheckBox ID="VolunteerTimeSheet3CheckBox" runat="server" Checked='<%# Bind("VolunteerTimeSheet3") %>' Enabled="false" />
                <br />
                VolunteerTimeSheet4:
                <asp:CheckBox ID="VolunteerTimeSheet4CheckBox" runat="server" Checked='<%# Bind("VolunteerTimeSheet4") %>' Enabled="false" />
                <br />
                DataEnteredElectronically1:
                <asp:CheckBox ID="DataEnteredElectronically1CheckBox" runat="server" Checked='<%# Bind("DataEnteredElectronically1") %>' Enabled="false" />
                <br />
                DataEnteredElectronically2:
                <asp:CheckBox ID="DataEnteredElectronically2CheckBox" runat="server" Checked='<%# Bind("DataEnteredElectronically2") %>' Enabled="false" />
                <br />
                DataEnteredElectronically3:
                <asp:CheckBox ID="DataEnteredElectronically3CheckBox" runat="server" Checked='<%# Bind("DataEnteredElectronically3") %>' Enabled="false" />
                <br />
                DataEnteredElectronically4:
                <asp:CheckBox ID="DataEnteredElectronically4CheckBox" runat="server" Checked='<%# Bind("DataEnteredElectronically4") %>' Enabled="false" />
                <br />
                SampleShipped1:
                <asp:CheckBox ID="SampleShipped1CheckBox" runat="server" Checked='<%# Bind("SampleShipped1") %>' Enabled="false" />
                <br />
                SampleShipped2:
                <asp:CheckBox ID="SampleShipped2CheckBox" runat="server" Checked='<%# Bind("SampleShipped2") %>' Enabled="false" />
                <br />
                SampleShipped3:
                <asp:CheckBox ID="SampleShipped3CheckBox" runat="server" Checked='<%# Bind("SampleShipped3") %>' Enabled="false" />
                <br />
                SampleShipped4:
                <asp:CheckBox ID="SampleShipped4CheckBox" runat="server" Checked='<%# Bind("SampleShipped4") %>' Enabled="false" />
                <br />
                NumberOfSamplesJan:
                <asp:Label ID="NumberOfSamplesJanLabel" runat="server" Text='<%# Bind("NumberOfSamplesJan") %>' />
                <br />
                NumberOfSamplesFeb:
                <asp:Label ID="NumberOfSamplesFebLabel" runat="server" Text='<%# Bind("NumberOfSamplesFeb") %>' />
                <br />
                NumberOfSamplesMar:
                <asp:Label ID="NumberOfSamplesMarLabel" runat="server" Text='<%# Bind("NumberOfSamplesMar") %>' />
                <br />
                NumberOfSamplesApr:
                <asp:Label ID="NumberOfSamplesAprLabel" runat="server" Text='<%# Bind("NumberOfSamplesApr") %>' />
                <br />
                NumberOfSamplesMay:
                <asp:Label ID="NumberOfSamplesMayLabel" runat="server" Text='<%# Bind("NumberOfSamplesMay") %>' />
                <br />
                NumberOfSamplesJun:
                <asp:Label ID="NumberOfSamplesJunLabel" runat="server" Text='<%# Bind("NumberOfSamplesJun") %>' />
                <br />
                NumberOfSamplesJul:
                <asp:Label ID="NumberOfSamplesJulLabel" runat="server" Text='<%# Bind("NumberOfSamplesJul") %>' />
                <br />
                NumberOfSamplesAug:
                <asp:Label ID="NumberOfSamplesAugLabel" runat="server" Text='<%# Bind("NumberOfSamplesAug") %>' />
                <br />
                NumberOfSamplesSep:
                <asp:Label ID="NumberOfSamplesSepLabel" runat="server" Text='<%# Bind("NumberOfSamplesSep") %>' />
                <br />
                NumberOfSamplesOct:
                <asp:Label ID="NumberOfSamplesOctLabel" runat="server" Text='<%# Bind("NumberOfSamplesOct") %>' />
                <br />
                NumberOfSamplesNov:
                <asp:Label ID="NumberOfSamplesNovLabel" runat="server" Text='<%# Bind("NumberOfSamplesNov") %>' />
                <br />
                NumberOfSamplesDec:
                <asp:Label ID="NumberOfSamplesDecLabel" runat="server" Text='<%# Bind("NumberOfSamplesDec") %>' />
                <br />
                Nutrient1Collected:
                <asp:CheckBox ID="Nutrient1CollectedCheckBox" runat="server" Checked='<%# Bind("Nutrient1Collected") %>' Enabled="false" />
                <br />
                Nutrient2Collected:
                <asp:CheckBox ID="Nutrient2CollectedCheckBox" runat="server" Checked='<%# Bind("Nutrient2Collected") %>' Enabled="false" />
                <br />
                BugCollected:
                <asp:CheckBox ID="BugCollectedCheckBox" runat="server" Checked='<%# Bind("BugCollected") %>' Enabled="false" />
                <br />
                UnknownSpringRecordedDate:
                <asp:Label ID="UnknownSpringRecordedDateLabel" runat="server" Text='<%# Bind("UnknownSpringRecordedDate") %>' />
                <br />
                UnknownFallRecordedDate:
                <asp:Label ID="UnknownFallRecordedDateLabel" runat="server" Text='<%# Bind("UnknownFallRecordedDate") %>' />
                <br />
                NumberOfSamplesBlank:
                <asp:Label ID="NumberOfSamplesBlankLabel" runat="server" Text='<%# Bind("NumberOfSamplesBlank") %>' />
                <br />
                NumberOfSamplesDuplicate:
                <asp:Label ID="NumberOfSamplesDuplicateLabel" runat="server" Text='<%# Bind("NumberOfSamplesDuplicate") %>' />
                <br />
                TroubleComment:
                <asp:Label ID="TroubleCommentLabel" runat="server" Text='<%# Bind("TroubleComment") %>' />
                <br />
                NoteComment:
                <asp:Label ID="NoteCommentLabel" runat="server" Text='<%# Bind("NoteComment") %>' />
                <br />
                HardshipComment:
                <asp:Label ID="HardshipCommentLabel" runat="server" Text='<%# Bind("HardshipComment") %>' />
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
                NumberOfMetalsBlank:
                <asp:Label ID="NumberOfMetalsBlankLabel" runat="server" Text='<%# Bind("NumberOfMetalsBlank") %>' />
                <br />
                NumberOfMetalsDuplicate:
                <asp:Label ID="NumberOfMetalsDuplicateLabel" runat="server" Text='<%# Bind("NumberOfMetalsDuplicate") %>' />
                <br />
                <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit" />
                &nbsp;<asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" />
                &nbsp;<asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" CommandName="New" Text="New" />
            </ItemTemplate>
        </asp:FormView>
        <asp:SqlDataSource ID="SqlDataSourceOrgStatus" runat="server" ConnectionString="<%$ ConnectionStrings:RiverwatchDEV %>" 
            SelectCommand="SELECT top 1 * FROM [OrgStatus] where OrganizationID = 485 order by ContractStartDate desc " 
            UpdateCommand="UPDATE [OrgStatus] SET [OrganizationID] = @OrganizationID, [ContractStartDate] = @ContractStartDate, [ContractEndDate] = @ContractEndDate, [ContractSigned] = @ContractSigned, [ContractSignedDate] = @ContractSignedDate, [SiteVisited] = @SiteVisited, [VolunteerTimeSheet1] = @VolunteerTimeSheet1, [VolunteerTimeShee2] = @VolunteerTimeShee2, [VolunteerTimeSheet3] = @VolunteerTimeSheet3, [VolunteerTimeSheet4] = @VolunteerTimeSheet4, [DataEnteredElectronically1] = @DataEnteredElectronically1, [DataEnteredElectronically2] = @DataEnteredElectronically2, [DataEnteredElectronically3] = @DataEnteredElectronically3, [DataEnteredElectronically4] = @DataEnteredElectronically4, [SampleShipped1] = @SampleShipped1, [SampleShipped2] = @SampleShipped2, [SampleShipped3] = @SampleShipped3, [SampleShipped4] = @SampleShipped4, [NumberOfSamplesJan] = @NumberOfSamplesJan, [NumberOfSamplesFeb] = @NumberOfSamplesFeb, [NumberOfSamplesMar] = @NumberOfSamplesMar, [NumberOfSamplesApr] = @NumberOfSamplesApr, [NumberOfSamplesMay] = @NumberOfSamplesMay, [NumberOfSamplesJun] = @NumberOfSamplesJun, [NumberOfSamplesJul] = @NumberOfSamplesJul, [NumberOfSamplesAug] = @NumberOfSamplesAug, [NumberOfSamplesSep] = @NumberOfSamplesSep, [NumberOfSamplesOct] = @NumberOfSamplesOct, [NumberOfSamplesNov] = @NumberOfSamplesNov, [NumberOfSamplesDec] = @NumberOfSamplesDec, [Nutrient1Collected] = @Nutrient1Collected, [Nutrient2Collected] = @Nutrient2Collected, [BugCollected] = @BugCollected, [UnknownSpringRecordedDate] = @UnknownSpringRecordedDate, [UnknownFallRecordedDate] = @UnknownFallRecordedDate, [NumberOfSamplesBlank] = @NumberOfSamplesBlank, [NumberOfSamplesDuplicate] = @NumberOfSamplesDuplicate, [TroubleComment] = @TroubleComment, [NoteComment] = @NoteComment, [HardshipComment] = @HardshipComment, [DateCreated] = @DateCreated, [UserCreated] = @UserCreated, [DateLastModified] = @DateLastModified, [UserLastModified] = @UserLastModified, [NumberOfMetalsBlank] = @NumberOfMetalsBlank, [NumberOfMetalsDuplicate] = @NumberOfMetalsDuplicate WHERE [ID] = @ID">
           
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
                <asp:Parameter Name="DateLastModified" Type="DateTime" />
                <asp:Parameter Name="UserLastModified" Type="String" />
                <asp:Parameter Name="NumberOfMetalsBlank" Type="Int32" />
                <asp:Parameter Name="NumberOfMetalsDuplicate" Type="Int32" />
                <asp:Parameter Name="ID" Type="Int32" />
            </UpdateParameters>
        </asp:SqlDataSource>
    </p>
    <p>
    </p>
</asp:Content>
