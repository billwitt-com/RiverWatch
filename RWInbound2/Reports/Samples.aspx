<%@ Page Title="Samples" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Samples.aspx.cs" Inherits="RWInbound2.Reports.Samples" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
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
    <br />
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="602px" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="1047px">
        <LocalReport ReportPath="Reports\Samples.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RiverWatchDEV %>" SelectCommand="SELECT [SampleID], [StationID], [StationName], [OrganizationID], [OrganizationName], [SampleNumber], [MissingDataSheetReceived], [ChainOfCustody], [MissingDataSheetReqDate], [NumberSample], [DateCollected], [TimeCollected], [DateReceived], [DataSheetIncluded], [NoMetals], [PhysicalHabitat], [Bug], [NoNutrient], [TotalSuspendedSolids], [NitratePhosphorus], [DuplicatedTSS], [DuplicatedNP], [DuplicatedMetals], [BlankMetals], [ChlorideSulfate], [UserLastModified], [DateLastModified], [DateCreated], [UserCreated], [Comment], [Valid], [DuplicatedCS], [BugsQA] FROM [SamplesView] ORDER BY [SampleNumber]"></asp:SqlDataSource>
</asp:Content>
