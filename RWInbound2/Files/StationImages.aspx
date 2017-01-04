﻿<%@ Page Title="Station Images" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StationImages.aspx.cs" Inherits="RWInbound2.Files.StationImages" %>
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
    <asp:Panel ID="StationNamePanel" runat="server">
        <div>
            <b>Station Name:</b>
            <asp:Label ID="lblStationName" runat="server"></asp:Label>   
            <asp:HiddenField id="HiddenStationID" runat="server" />
        </div>
    </asp:Panel>
    <br />
    <br />

    <asp:GridView ID="StationImagesGridView" runat="server"
            DataKeyNames="ID"
            ItemType="RWInbound2.StationImage" 
            SelectMethod="GetStationImages"
            DeleteMethod="DeleteStationImage" 
            InsertItemPosition="LastItem"  
            ShowFooter="True"
            CellPadding="4"
            AutoGenerateColumns="False" CssClass="grid-larger-editor-columns-center"
            GridLines="None" ForeColor="#333333" Height="238px"
            AllowPaging="True" Pagesize="15">
            <AlternatingRowStyle BackColor="White" />            
            <Columns>  
                <asp:TemplateField>
                    <ItemTemplate>
                        <%--<asp:Button ID="EditButton" runat="server" Text="Edit" CommandName="Edit" />--%>
                        <asp:Button ID="DeleteButton" runat="server" Text="Delete" CommandName="Delete" 
                                    OnClientClick="return confirm('Are you certain you want to delete this?');"/>
                    </ItemTemplate>                    
                    <FooterTemplate>
                        <asp:Button ID="btnAdd" runat="server" Text="Add"
                                    OnClick="AddNewStationImage" />
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ID" HeaderText="ID" Visible="false" ReadOnly="True" SortExpression="ID" />                
                <asp:TemplateField HeaderText="Image Name" SortExpression="FileName">                    
                    <ItemTemplate>
                        <asp:Label ID="lblFileName" runat="server" Text='<%# Bind("FileName") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>                         
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Image" SortExpression="Image">                    
                    <ItemTemplate>
                        <asp:Image runat="server" ImageUrl='<%# Eval("FileUrl") %>' CssClass="grid-station-images-image-size" />  
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:FileUpload id="FileUploadStationImages" accept="image/*" multiple="false"                 
                            runat="server" >
                        </asp:FileUpload>
                        <p class="grid-station-images-upload-comment">
                            Max Image Size: 1 MB (1000KB) <br />
                            Only Image file types allowed <br />
                        </p>
                        <asp:Label ID="lblFileUploadStationImagesMaxSize" runat="server" Visible="false" CssClass="required">
                            Image exceeds maximum Image size!
                        </asp:Label>
                        <asp:Label ID="lblFileUploadStationImagesFileType" runat="server" Visible="false" CssClass="required">
                            Invalid Image file type!
                        </asp:Label>
                    </FooterTemplate>
                </asp:TemplateField>               
                <asp:TemplateField HeaderText="Primary Image" SortExpression="Primary">
                   <ItemTemplate>
                        <asp:CheckBox ID="checkBoxPrimary" runat="server" Checked='<%# Bind("Primary") %>' Enabled="false" />
                    </ItemTemplate>
                    <FooterTemplate>
                       <asp:CheckBox ID="NewPrimary" runat="server" />
                    </FooterTemplate>
                </asp:TemplateField> 
            </Columns>
            <EditRowStyle BackColor="#2461BF" />            
        </asp:GridView>      
    
</asp:Content>
