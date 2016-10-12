<%@ Page Title="Edit Participants" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditParticipants.aspx.cs" Inherits="RWInbound2.Admin.EditParticipants" %>
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
                Search By Organization:
                <asp:TextBox ID="organizationSearch" 
                    AutoPostBack="true"
                    runat="server"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="Select" Height="31px" OnClick="btnSearch_Click" CausesValidation="False" CssClass="adminButton" />
                <asp:Button ID="btnSearchRefresh" runat="server" Text="Reset Search" Height="31px" OnClick="btnSearchRefresh_Click" CausesValidation="False" CssClass="adminButton"/>
                    <ajaxToolkit:AutoCompleteExtender 
                        ID="tbSearch_AutoCompleteExtender" 
                        runat="server" 
                        BehaviorID="tbSearch_AutoCompleteExtender" 
                        DelimiterCharacters=""  
                        ServiceMethod="SearchForOrganization"             
                        TargetControlID="organizationSearch"
                        MinimumPrefixLength="2"
                        CompletionInterval="100" 
                        EnableCaching="true" 
                        CompletionSetCount="10"
                        UseContextKey="True">
                    </ajaxToolkit:AutoCompleteExtender> 
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:FormView ID="ExpWaterFormView" runat="server" 
        DataKeyNames="ID"
        ItemType="RWInbound2.tblParticipant" 
        SelectMethod="GetParticipants"
        UpdateMethod="UpdateParticipants"
        DefaultMode="Edit"
        AllowPaging="true"> 
        <EditItemTemplate> 
            <asp:Button ID="UpdateButton" runat="server" Text="Update" CommandName="Update" CssClass="adminButton" />
            <button class="adminButton" type="reset" value="Reset">Reset</button>
            <table>
                <tr>                    
                    <label>Last Name:</label> 
                <asp:TextBox ID="TextBoxLastName" runat="server" Text='<%# Bind("LastName") %>'></asp:TextBox> 
                </tr>
            </table>
        </EditItemTemplate> 
    </asp:FormView>
</asp:Content>
