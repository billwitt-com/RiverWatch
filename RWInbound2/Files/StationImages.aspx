<%@ Page Title="Station Images" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StationImages.aspx.cs" Inherits="RWInbound2.Files.StationImages" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
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
    </div>
    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <label>Search By Station Name:</label>
                <asp:TextBox ID="stationNameSearch" 
                    AutoPostBack="true"
                    runat="server"></asp:TextBox>
                <asp:Button ID="btnSearchStationName" runat="server" Text="Select" Height="31px" OnClick="btnSearchStationName_Click" CssClass="adminButton" />
                <ajaxToolkit:AutoCompleteExtender 
                    ID="tbSearch_AutoCompleteExtender" 
                    runat="server" 
                    BehaviorID="tbSearch_AutoCompleteExtender" 
                    DelimiterCharacters=""  
                    ServiceMethod="SearchForStationName"             
                    TargetControlID="stationNameSearch"
                    MinimumPrefixLength="2"
                    CompletionInterval="100" 
                    EnableCaching="false" 
                    CompletionSetCount="10">
                </ajaxToolkit:AutoCompleteExtender> 
                <div class="org-unknown-results-or"><label>OR</label></div>
                <label>By Station#:</label>                
                <asp:TextBox ID="stationNumberSearch" 
                    AutoPostBack="true"
                    runat="server"></asp:TextBox>
                <asp:Button ID="btnSearchStationNumber" runat="server" Text="Select" Height="31px" OnClick="btnSearchStationNumber_Click" CssClass="adminButton" />                
                <ajaxToolkit:AutoCompleteExtender 
                    ID="AutoCompleteExtender1" 
                    runat="server" 
                    BehaviorID="tbSearch_AutoCompleteExtender1" 
                    DelimiterCharacters=""  
                    ServiceMethod="SearchForStationNumber"             
                    TargetControlID="stationNumberSearch"
                    MinimumPrefixLength="1"
                    CompletionInterval="100" 
                    EnableCaching="false" 
                    CompletionSetCount="10">
                </ajaxToolkit:AutoCompleteExtender>
                <asp:Button ID="btnSearchRefresh" runat="server" Text="Reset Search" Height="31px" OnClick="btnSearchRefresh_Click" CssClass="adminButton org-unknown-results-reset-search" /> 
                <br />
                <br />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div>
        <h4>Select a file to upload:</h4>

        <asp:FileUpload id="FileUploadStationImages"                 
            runat="server">
        </asp:FileUpload>

        <br/><br/>

        <asp:Button id="UploadBtn" 
            Text="Upload file"
            OnClick="UploadBtn_Click"
            runat="server">
        </asp:Button>    

        <hr />

        <asp:Label id="UploadStatusLabel"
            runat="server">
        </asp:Label>     
    </div>
</asp:Content>
