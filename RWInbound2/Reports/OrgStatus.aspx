<%@ Page Title="Organization Status" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrgStatus.aspx.cs" Inherits="RWInbound2.Reports.OrgStatus" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <hgroup>
        <h3><%: Page.Title %></h3>
    </hgroup>

    <div class="label-placement">
        <asp:Label ID="ErrorLabel" CssClass="label-error" runat="server" />               
    </div>
    <div class="label-placement">
            <asp:Label ID="SuccessLabel" CssClass="label-success" runat="server" />
    </div>
 

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                Search By Organization Name:
                <asp:TextBox ID="orgNameSearch" 
                    AutoPostBack="true"
                    runat="server"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="Search" Height="31px" OnClick="btnSearch_Click" />
                <asp:Button ID="btnSearchRefresh" runat="server" Text="Reset Search" Height="31px" OnClick="btnSearchRefresh_Click" />
                <ajaxToolkit:AutoCompleteExtender 
                    ID="tbSearch_AutoCompleteExtender" 
                    runat="server" 
                    BehaviorID="tbSearch_AutoCompleteExtender" 
                    DelimiterCharacters=""  
                    ServiceMethod="SearchForOrgName"             
                    TargetControlID="orgNameSearch"
                    MinimumPrefixLength="2"
                    CompletionInterval="100" 
                    EnableCaching="false" 
                    CompletionSetCount="10">
                </ajaxToolkit:AutoCompleteExtender>
            </ContentTemplate>
        </asp:UpdatePanel>         
<br />
<rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="1072px" Height="551px">
    <LocalReport ReportPath="Reports\OrgStatus.rdlc">
        <DataSources>
            <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
        </DataSources>
    </LocalReport>
</rsweb:ReportViewer>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RiverWatchDEV %>" 
    SelectCommand="SELECT [OrganizationName], [ContractStartDate], [ContractEndDate], [ContractSigned], [ContractSignedDate], [SiteVisited], [VolunteerTimeSheet1], [VolunteerTimeShee2], [VolunteerTimeSheet3], [VolunteerTimeSheet4], [DataEnteredElectronically1], [DataEnteredElectronically2], [DataEnteredElectronically3], [DataEnteredElectronically4], [NumberOfSamplesMar], [SampleShipped1], [SampleShipped2], [SampleShipped3], [SampleShipped4], [NumberOfSamplesJan], [NumberOfSamplesFeb], [NumberOfSamplesApr], [NumberOfSamplesMay], [NumberOfSamplesJun], [NumberOfSamplesJul], [NumberOfSamplesAug], [NumberOfSamplesSep], [NumberOfSamplesOct], [NumberOfSamplesNov], [NumberOfSamplesDec], [Nutrient1Collected], [Nutrient2Collected], [BugCollected], [UnknownSpringRecordedDate], [UnknownFallRecordedDate], [NumberOfSamplesBlank], [NumberOfSamplesDuplicate], [TroubleComment], [NoteComment], [HardshipComment], [DateCreated], [UserCreated] FROM [OrgStatusView] ORDER BY [OrganizationName], contractEndDate desc "></asp:SqlDataSource>
<br />

</asp:Content>
