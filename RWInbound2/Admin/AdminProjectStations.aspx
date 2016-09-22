<%@ Page Title="Project Stations" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminProjectStations.aspx.cs" 
         Inherits="RWInbound2.Admin.AdminProjectStations" ClientIDMode="Static" %>
<asp:Content ID="HeaderContent1" runat="server" ContentPlaceHolderID="HeaderContent"> 
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">      
    <section class="assign-stations-section" spellcheck="true">
        <hgroup>
            <h3><%: Page.Title %></h3>
        </hgroup>              
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
                <div class="modal">
                    <div class="center">
                        <img alt="Loading Please Wait" src="../Images/loader.gif" />
                    </div>
                </div>     
            </ProgressTemplate>
        </asp:UpdateProgress>        
        <h5>Assign Station(s) to This Project</h5>   
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:FormView runat="server" ID="UpdateProjectStationsForm"
                    ItemType="RWInbound2.View_Models.ProjectStationViewModel" 
                    SelectMethod="GetProjectStations"
                    RenderOuterTable="false">                    
                    <ItemTemplate>   
                        <div class="project-stations-label-placement">
                            <asp:Label ID="ErrorLabel" CssClass="project-stations-label-error" 
                                       Text="" runat="server" />               
                        </div>
                        <div class="project-stations-label-placement">
                            <asp:Label ID="SuccessLabel" CssClass="project-stations-label-success" 
                                       Text="" runat="server" />
                        </div>                       
                        <div>
                            <br />
                            Select a Project:
                            <asp:DropDownList ID="ProjectsDropDown" runat="server" AutoPostBack="True" DataMember="it"
                                AppendDataBoundItems="true" DataSource='<%# Bind("Projects") %>'
                                DataTextField="ProjectName" DataValueField="ProjectID" 
                                OnSelectedIndexChanged="ProjectsDropDown_SelectedIndexChanged">
                                <asp:ListItem Value="0">Select One:</asp:ListItem>
                            </asp:DropDownList>
                            <div>
                                <asp:Button id="Update"
                                    OnClick="Update_Click"
                                    Text="Update"
                                    CssClass="multiselect-update"
                                    runat="server"/>
                                <asp:Button id="Cancel"
                                    OnClick="Cancel_Click"
                                    Text="Cancel" 
                                    CssClass="multiselect-cancel"
                                    runat="server" />                                       
                            </div>
                            <br />
                            <label class="admin-project-station-narrow-search">
                                Narrow Station Search
                            </label>
                                <small>(Select One)</small>
                            <br />
                            <div>
                                <ul class="admin-project-station-clear-list-style search">
                                    <li>
                                        <div class="admin-project-station-number-label">By River:</div>
                                        <asp:DropDownList ID="RiversDropDown" runat="server" AutoPostBack="True" DataMember="it"
                                            AppendDataBoundItems="true" DataSource='<%# Bind("Rivers") %>'
                                            DataTextField="Text" DataValueField="Value" 
                                            OnSelectedIndexChanged="RiversDropDown_SelectedIndexChanged" 
                                            Enabled="false">
                                            <asp:ListItem Value="0">Select One:</asp:ListItem>
                                        </asp:DropDownList>
                                    </li>
                                    <li>
                                        <span class="admin-project-station-or-label" >OR</span>
                                        <div class="admin-project-station-number-label">Station #:</div>
                                        <asp:TextBox ID="StationNumberSearchTextBox" 
                                            AutoPostBack="true"
                                            Enabled="false"                                            
                                            runat="server"></asp:TextBox>                                                                         
                                        <%--This is from drag and drop from toolbox. Note, servicemethod was added by hand by me--%>
                                        <ajaxToolkit:AutoCompleteExtender 
                                            ID="tbSearch_AutoCompleteExtender" 
                                            runat="server" 
                                            BehaviorID="tbSearch_AutoCompleteExtender" 
                                            DelimiterCharacters=""  
                                            ServiceMethod="SearchForStationNumbers"             
                                            TargetControlID="StationNumberSearchTextBox"
                                            MinimumPrefixLength="1"
                                            CompletionInterval="100" 
                                            EnableCaching="false" 
                                            CompletionSetCount="10">
                                        </ajaxToolkit:AutoCompleteExtender> 
                                    </li>
                                    <li>
                                        <asp:Button ID="StationNumberSearchTextBoxButton" 
                                            Text="Search" 
                                            CssClass="admin-project-station-number-search-button"
                                            OnClick="StationNumberSearchTextBoxButton_Click"
                                            runat="server" 
                                            Enabled="false"/>  
                                    </li>
                                </ul>
                            </div>   
                            <br />                   
                        </div> 
                        <div class="project-stations-label-placement-station-number-assigned">
                            <asp:Label ID="StationNumberAssignedLabel" CssClass="project-stations-label-error" 
                                       Text="" runat="server" />               
                        </div>
                        <div>
                            <label class="admin-project-station-avaliable-label">
                                Avaliable Stations
                            </label>
                            <label class="admin-project-station-assigned-label">
                                Assigned Stations
                            </label>
                            <ul class="admin-project-station-clear-list-style">
                                <li>                                    
                                    <div class="multiselect-stations-label">                                        
                                        <asp:ListBox id="AvaliableStationsListBox" 
                                            CssClass="multiselect-locations-list"
                                            SelectionMode="Multiple" 
                                            AppendDataBoundItems="true"   
                                            DataTextField="StationName" DataValueField="StationNumber"
                                            runat="server">
                                        </asp:ListBox>                 
                                    </div>        
                                    <div class="multiselect-move-arrows-div">
                                        <asp:Button id="AddSingleToAssigned"
                                            OnClick="AddSingleToAssigned_Click" 
                                            Text=">"
                                            runat="server"/>
                                        <asp:Button id="AddAllToAssigned"
                                            OnClick="AddAllToAssigned_Click"  
                                            Text=">>" 
                                            runat="server" />                                       
                                    </div>
                                    <div class="multiselect-stations-label">                                       
                                        <asp:ListBox id="AssignedStationsListBox" 
                                            CssClass="multiselect-locations-list"
                                            SelectionMode="Multiple" 
                                            AppendDataBoundItems="true" 
                                            DataTextField="StationName" DataValueField="StationNumber"
                                            runat="server">                                            
                                        </asp:ListBox>
                                    </div>
                                    <div class="multiselect-reset-delete-buttons">
                                        <asp:Button id="RemoveAssigned"
                                            OnClick="RemoveAssigned_Click" 
                                            Text="Remove Selected Assigned Station(s)"
                                            CssClass="multiselect-remove-assigned"
                                            runat="server"/>                                                                              
                                    </div>
                                </li>
                            </ul>
                        </div> 
                    </ItemTemplate>       
                </asp:FormView>                
            </ContentTemplate>
        </asp:UpdatePanel> 
    </section>   
</asp:Content>

