<%@ Page Title="Enter Benthics PhysHab" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EnterBenthicsPhysHab.aspx.cs" Inherits="RWInbound2.Admin.EnterBenthicsPhysHab" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server" ClientIDMode="Static">
    <div>
        <hgroup>
            <h3>Enter/Edit Benthics or Physical Habitat Data for a Sample</h3>
        </hgroup>
    </div>    
    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="label-placement">
                        <asp:Label ID="ErrorLabel" CssClass="label-error" runat="server" />               
                    </div>
                    <div class="label-placement">
                        <asp:Label ID="SuccessLabel" CssClass="label-success" runat="server" />
                    </div>
                    <br />            
                </div>
                <div class="enter-benthics-physhab-search-by-any">
                    Search By Any One of the Options:
                </div>
                 <asp:Button ID="btnSearchRefresh" runat="server" Text="Reset Search" OnClick="btnSearchRefresh_Click" CssClass="adminButton" /> 
                <br />
                <label>Station#:</label>                  
                <asp:TextBox ID="stationNumberSearch"  
                    onkeydown = "return (event.keyCode!=13);"                   
                    runat="server"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="Select" Height="31px" OnClick="btnSearchStationNumber_Click" CausesValidation="False" CssClass="adminButton" />               
                <ajaxToolkit:AutoCompleteExtender 
                    ID="tbSearch_AutoCompleteExtender" 
                    runat="server" 
                    BehaviorID="tbSearch_AutoCompleteExtender" 
                    DelimiterCharacters=""  
                    ServiceMethod="SearchForStationNumber"             
                    TargetControlID="stationNumberSearch"
                    MinimumPrefixLength="2"
                    CompletionInterval="100" 
                    EnableCaching="true" 
                    CompletionSetCount="10"
                    UseContextKey="True">
                </ajaxToolkit:AutoCompleteExtender>             
               <%-- <div class="org-unknown-results-or"><label>OR</label></div>--%>
                <label>Sample Number:</label>                
                <asp:TextBox ID="sampleNumberSearch"  
                    onkeydown="return (event.keyCode!=13);"                   
                    runat="server"></asp:TextBox>
                <asp:Button ID="btnSearchSampleNumber" runat="server" Text="Select" Height="31px" OnClick="btnSearchSampleNumber_Click" CssClass="adminButton" />                
                <ajaxToolkit:AutoCompleteExtender 
                    ID="AutoCompleteExtender1" 
                    runat="server" 
                    BehaviorID="tbSearch_AutoCompleteExtender1" 
                    DelimiterCharacters=""  
                    ServiceMethod="SearchForSampleNumber"             
                    TargetControlID="sampleNumberSearch"
                    MinimumPrefixLength="1"
                    CompletionInterval="100" 
                    EnableCaching="false" 
                    CompletionSetCount="10">
                </ajaxToolkit:AutoCompleteExtender>               
                <label>Sample Event:</label>                
                <asp:TextBox ID="sampleEventNumberSearch" 
                    onkeydown="return (event.keyCode!=13);"                  
                    runat="server"></asp:TextBox>
                <asp:Button ID="btnSearchSampleEventNumber" runat="server" Text="Select" Height="31px" OnClick="btnSearchSampleEventNumber_Click" CssClass="adminButton" />                
                <ajaxToolkit:AutoCompleteExtender 
                    ID="AutoCompleteExtender2" 
                    runat="server" 
                    BehaviorID="tbSearch_AutoCompleteExtender2" 
                    DelimiterCharacters=""  
                    ServiceMethod="SearchForSampleEventNumber"             
                    TargetControlID="sampleEventNumberSearch"
                    MinimumPrefixLength="1"
                    CompletionInterval="100" 
                    EnableCaching="false" 
                    CompletionSetCount="10">
                </ajaxToolkit:AutoCompleteExtender>               
            </ContentTemplate>
        </asp:UpdatePanel>
        <br /><br />
        <asp:Panel ID="StationDataPanel" runat="server">
            <div>
                <b>Station#:</b>
                <asp:Label ID="lblStationNumber" runat="server"></asp:Label>
                <div class="enter-benthics-physhab-stationname">
                    <b>Station Name:</b>
                    <asp:Label ID="lblStationName" runat="server"></asp:Label>
                </div>   
            </div>
            <br /><br />
        </asp:Panel>
    </div>
    
    <asp:GridView ID="BenthicsPhysHabGridView" runat="server"
                    DataKeyNames="ID"
                    ItemType="RWInbound2.Sample"          
                    SelectMethod="GetSamples"  
                    OnRowCommand="BenthicsPhysHabGridView_RowCommand"                 
                    CellPadding="4"
                    AutoGenerateColumns="False" CssClass="grid-columns-center grid-enter-benthics-physhab-item-rowstyle"
                    HeaderStyle-CssClass="grid-enter-benthics-physhab-header"         
                    GridLines="None" ForeColor="#333333"
                    AllowSorting="true"
                    AllowPaging="true" Pagesize="15"
                    EmptyDataRowStyle-VerticalAlign="top" >                  
                    <AlternatingRowStyle BackColor="White" />   
        <EmptyDataTemplate>
            <p>No Samples were found.</p>
        </EmptyDataTemplate>
        <Columns>  
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="BenthicsButton" runat="server" Text="Benthics" CommandName="EditBenthicSample" CommandArgument='<%# Bind("ID") %>' />
                    <asp:Button ID="PhysHabButton" runat="server" Text="Phys Hab" OnClientClick="return alert('Coming Soon!');" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Sample Number" SortExpression="SampleNumber">
                <ItemTemplate>
                    <asp:Label ID="lblSampleNumber" runat="server" Text='<%# Bind("SampleNumber") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Event Number" SortExpression="NumberSample">
                <ItemTemplate>
                    <asp:Label ID="lblNumberSample" runat="server" Text='<%# Bind("NumberSample") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Date Collected" SortExpression="DateCollected">
                <ItemTemplate>
                    <asp:Label ID="lblDateCollected" runat="server" Text='<%# Eval("DateCollected", "{0:MM/dd/yyyy}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Time Collected" SortExpression="TimeCollected">
                <ItemTemplate>
                    <asp:Label ID="lblTimeCollected" runat="server" Text='<%# Eval("TimeCollected", "{0:hh:mm}") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>  
</asp:Content>
