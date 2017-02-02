<%@ Page Title="Enter Benthics PhysHab" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EnterBenthicsPhysHab.aspx.cs" Inherits="RWInbound2.Data.EnterBenthicsPhysHab" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server" ClientIDMode="Static">
    <div>
    <hgroup>
        <h3>Select a Station to Enter Benthics or Physical Habitat Data to a Sample</h3>
    </hgroup>    
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
                Search By Station#:
                <asp:TextBox ID="stationNumberSearch" 
                    AutoPostBack="true"
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
                <div class="org-unknown-results-or"><label>OR</label></div>
                <label>Search By Sample Number:</label>                
                <asp:TextBox ID="sampleNumberSearch" 
                    AutoPostBack="true"
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
                <asp:Button ID="Button1" runat="server" Text="Reset Search" Height="31px" OnClick="btnSearchRefresh_Click" CssClass="adminButton org-unknown-results-reset-search" /> 
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
    
    <asp:GridView ID="EquipmentGridView" runat="server"
                    DataKeyNames="ID"
                    ItemType="RWInbound2.Sample"          
                    SelectMethod="GetSamples"                   
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
                    <asp:Button ID="BenthicsButton" runat="server" Text="Benthics" OnClientClick="return alert('Not Implmented Yet!');" />
                    <asp:Button ID="PhysHabButton" runat="server" Text="Phys Hab" OnClientClick="return alert('Not Implmented Yet!');" />
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
