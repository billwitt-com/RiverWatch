<%@ Page Title="Organization Unknown Results" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrgUnknownResults.aspx.cs" Inherits="RWInbound2.Reports.OrgUnknownResults" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server" ClientIDMode="Static">
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

        <asp:UpdatePanel ID="updatePanelOrgUnknownResults" runat="server">
            <ContentTemplate>
                <label>Search By Organization Name:</label>
                <asp:TextBox ID="orgNameSearch" 
                    AutoPostBack="true"
                    runat="server"></asp:TextBox>
                <asp:Button ID="btnSearchOrgName" runat="server" Text="Select" Height="31px" OnClick="btnSearchOrgName_Click" CssClass="adminButton" />
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
                <div class="org-unknown-results-or"><label>OR</label></div>
                <label>Search By Kit Number:</label>                
                <asp:TextBox ID="kitNumberSearch" 
                    AutoPostBack="true"
                    runat="server"></asp:TextBox>
                <asp:Button ID="btnSearchKitNumber" runat="server" Text="Select" Height="31px" OnClick="btnSearchKitNumber_Click" CssClass="adminButton" />                
                <ajaxToolkit:AutoCompleteExtender 
                    ID="AutoCompleteExtender1" 
                    runat="server" 
                    BehaviorID="tbSearch_AutoCompleteExtender1" 
                    DelimiterCharacters=""  
                    ServiceMethod="SearchForkitNumber"             
                    TargetControlID="kitNumberSearch"
                    MinimumPrefixLength="1"
                    CompletionInterval="100" 
                    EnableCaching="false" 
                    CompletionSetCount="10">
                </ajaxToolkit:AutoCompleteExtender>
                <asp:Button ID="btnSearchRefresh" runat="server" Text="Reset Search" Height="31px" OnClick="btnSearchRefresh_Click" CssClass="adminButton org-unknown-results-reset-search" />                
            </ContentTemplate>
        </asp:UpdatePanel> 
    <br />  
    <br />    
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" ShowPrintButton="False" Font-Size="8pt" Width="1072px" Height="551px" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
        <LocalReport ReportPath="Reports\OrgUnknownResults.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
   
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                       ConnectionString="<%$ ConnectionStrings:RiverWatchDEV %>" 
                       SelectCommand="SELECT [KitNumber], [OrganizationName], [SampleType], [SampleNumber], [DateSent], [Value1], [Value2], [MeanValue], [PctRecovery], [Round], [TrueValue] FROM [UnknownResultsForOrgView] ORDER BY [OrganizationName]"></asp:SqlDataSource>
   
</asp:Content>
