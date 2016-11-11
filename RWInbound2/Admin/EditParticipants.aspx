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
    <div>
       <label>
           <asp:Label ID="OrganizationLabel" runat="server" Text="Organization Name:" CssClass="organization-label"/>
       </label>
        <asp:Label ID="OrganizationName" runat="server" />
       <asp:Label ID="OrgID" runat="server" Visible="false" />
    </div> 
    <asp:FormView ID="ExpWaterFormView" runat="server" 
            DataKeyNames="ID"
            ItemType="RWInbound2.View_Models.ParticipantsViewModel" 
            SelectMethod="GetParticipants"
            UpdateMethod="UpdateParticipant"
            DeleteMethod="DeleteParticipant" 
            InsertMethod="InsertParticipant"
            ShowFooter="true"
            CellPadding="4"
            GridLines="None" ForeColor="#333333" 
            AllowPaging="true" Pagesize="15" PagerSettings-Mode="NumericFirstLast" PagerSettings-Position="Bottom"
            OnItemDeleted="ExpWaterFormView_ItemDeleted"> 
            
            <ItemTemplate>
                <asp:Panel ID="ItemTemplatePanel" runat="server">
                    <br />
                    <label>First Name:</label>
                    <asp:Label ID="lblFirstName" runat="server" Text='<%# Bind("FirstName") %>'></asp:Label>
                    <br />
                    <label>Last Name:</label>
                    <asp:Label ID="lblLastName" runat="server" Text='<%# Bind("LastName") %>'></asp:Label>
                    <br />
                    <label>Title:</label>
                    <asp:Label ID="lblTitle" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
                    <br />
                    <label>Year Signed On:</label>
                    <asp:Label ID="lblYearSignedOn" runat="server" Text='<%# Bind("YearSignedOn") %>'></asp:Label>
                    <br />
                    <label>Phone:</label>
                    <asp:Label ID="lblPhone" runat="server" Text='<%# Bind("Phone") %>'></asp:Label>
                    <br />
                    <label>Email: </label>
                    <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
                    <br />
                    <label>Address1: </label>
                    <asp:Label ID="lblAddress1" runat="server" Text='<%# Bind("Address1") %>'></asp:Label>
                    <br />
                    <label>Address2:</label>
                    <asp:Label ID="lblAddress2" runat="server" Text='<%# Bind("Address2") %>'></asp:Label>
                    <br />
                     <label>City: </label>
                    <asp:Label  ID= "lblCity" runat="server" Text='<%# Bind("City") %>'></asp:Label>
                    <br />
                    <label>State: </label>
                    <asp:Label ID="lblState" runat="server" Text='<%# Bind("State") %>'></asp:Label>
                    <br />
                    <label>Zip:</label>
                    <asp:Label ID="lblZip" runat="server" Text='<%# Bind("Zip") %>'></asp:Label>
                    <br />
                    <label>Home Phone</label>
                    <asp:Label ID="lblHomePhone" runat="server" Text='<%# Bind("HomePhone") %>'></asp:Label>
                    <br />
                    <label>Home Email:</label>
                    <asp:Label ID="lblHomeEmail" runat="server" Text='<%# Bind("HomeEmail") %>'></asp:Label>
                    <br />
                    <label>Mail Preference:</label>
                    <asp:Label ID="lblMailPreference" runat="server" Text='<%# Bind("MailPreference") %>'></asp:Label>
                    <br />
                    <label>Active:</label>
                    <asp:CheckBox ID="checkBoxActive" runat="server" Checked='<%# Bind("Active") %>' Enabled="false" />
                    <br />
                    <label>Primary Contact:</label>
                    <asp:CheckBox ID="checkBoxPrimaryContact" runat="server" Checked='<%# Bind("PrimaryContact") %>' Enabled="false" />
                    <br />
                    <label>Training:</label>
                    <asp:CheckBox ID="checkBoxTraining" runat="server" Checked='<%# Bind("Training") %>' Enabled="false" />

                    <br /><br />
                    <asp:Button ID="EditButton" runat="server" Text="Edit" CommandName="Edit" CssClass="adminButton" /> 
                    <asp:Button ID="DeleteButton" runat="server" Text="Delete" CommandName="Delete" CssClass="adminButton"
                                OnClientClick="return confirm('Are you certain you want to delete this?');"/>
                </asp:Panel>                 
                <asp:Button ID="NewButton" runat="server" Text="  New  " CommandName="New" CssClass="adminButton"/>
            </ItemTemplate>

            <EditItemTemplate>
                <br />
                <label>First Name:</label>
                <asp:TextBox ID="txtFirstName" runat="server" Text='<%# Bind("FirstName") %>'></asp:TextBox>
                <br />
                <label>Last Name:</label>
                <asp:TextBox ID="txtLastName" runat="server" Text='<%# Bind("LastName") %>'></asp:TextBox>
                <br />
                <label>Title:</label>
                <asp:TextBox ID="txtTitle" runat="server" Text='<%# Bind("Title") %>'></asp:TextBox>
                 <br />
                <label>Year Signed On:</label>
                <asp:TextBox ID="txtYearSignedOn" runat="server" TextMode="Number" Text='<%# Bind("YearSignedOn") %>'></asp:TextBox>
                <br />
                <label>Phone:</label>
                <asp:TextBox ID="txtPhone" runat="server" Text='<%# Bind("Phone") %>'></asp:TextBox>
                <br />
                <label>Email:</label>
                <asp:TextBox ID="txtEmail" runat="server" Text='<%# Bind("Email") %>'></asp:TextBox>
                <br />
                <label>Address1: </label>
                <asp:TextBox ID="txtAddress1" runat="server" Text='<%# Bind("Address1") %>'></asp:TextBox>
                <br />
                <label>Address2:</label>
                <asp:TextBox ID="txtAddress2" runat="server" Text='<%# Bind("Address2") %>'></asp:TextBox>
                <br />
                 <label>City: </label>
                <asp:TextBox ID= "txtCity" runat="server" Text='<%# Bind("City") %>'></asp:TextBox>
                 <br />
                <label>State: </label>
                <asp:TextBox ID="txtState" runat="server" Text='<%# Bind("State") %>'></asp:TextBox>
                <br />
                <label>Zip</label>
                <asp:TextBox ID="txtZip" runat="server" Text='<%# Bind("Zip") %>'></asp:TextBox>
                <br />
                <label>Home Phone</label>
                <asp:TextBox ID="txtHomePhone" runat="server" Text='<%# Bind("HomePhone") %>'></asp:TextBox>
                <br />
                <label>Home Email:</label>
                <asp:TextBox ID="txtHomeEmail" runat="server" Text='<%# Bind("HomeEmail") %>'></asp:TextBox>
                <br />
                <label>Mail Preference:</label>
                <asp:TextBox ID="txtMailPreference" runat="server" Text='<%# Bind("MailPreference") %>'></asp:TextBox>
                <br />
                <label>Active:</label>
                <asp:CheckBox ID="CheckBoxActiveEdit" runat="server" Checked='<%# Bind("Active") %>' />
                <br />
                <label>Primary Contact:</label>
                <asp:CheckBox ID="CheckBoxPrimaryContactEdit" runat="server" Checked='<%# Bind("PrimaryContact") %>'/>
                <br />
                <label>Training:</label>
                <asp:CheckBox ID="CheckBoxTrainingEdit" runat="server" Checked='<%# Bind("Training") %>'/>

                <br /><br />
                <asp:HiddenField id="ID" runat="server" value='<%# Bind("ID") %>' />
                <asp:HiddenField id="OrganizationID" runat="server" value='<%# Bind("OrganizationID") %>' />
                <asp:Button ID="CancelButton" runat="server" Text="Cancel" CommandName="Cancel" CssClass="adminButton" />
                <asp:Button ID="UpdateButton" runat="server" Text="Update" CommandName="Update" CssClass="adminButton" />
            </EditItemTemplate>

            <InsertItemTemplate>
                <label>First Name:</label>
                <asp:TextBox ID="newFirstName" runat="server" Text='<%# Bind("FirstName") %>'></asp:TextBox>
                <br />
                <label>Last Name:</label>
                <asp:TextBox ID="newLastName" runat="server" Text='<%# Bind("LastName") %>'></asp:TextBox>
                <br />
                <label>Title:</label>
                <asp:TextBox ID="newTitle" runat="server" Text='<%# Bind("Title") %>'></asp:TextBox>
                <br />
                <label>Year Signed On:</label>
                <asp:TextBox ID="newYearSignedOn" runat="server" TextMode="Number" Text='<%# Bind("YearSignedOn") %>'></asp:TextBox>
                <br />
                <label>Phone:</label>
                <asp:TextBox ID="newPhone" runat="server" Text='<%# Bind("Phone") %>'></asp:TextBox>
                <br />
                <label>Email:</label>
                <asp:TextBox ID="newEmail" runat="server" Text='<%# Bind("Email") %>'></asp:TextBox>
                <br />
                <label>Address1: </label>
                <asp:TextBox ID="newAddress1" runat="server" Text='<%# Bind("Address1") %>'></asp:TextBox>
                <br />
                <label>Address2:</label>
                <asp:TextBox ID="newAddress2" runat="server" Text='<%# Bind("Address2") %>'></asp:TextBox>
                <br />
                <label>City: </label>
                <asp:TextBox ID= "newCity" runat="server" Text='<%# Bind("City") %>'></asp:TextBox>
                 <br />
                <label>State: </label>
                <asp:TextBox ID="newState" runat="server" Text='<%# Bind("State") %>'></asp:TextBox>
                <br />
                <label>Zip</label>
                <asp:TextBox ID="newZip" runat="server" Text='<%# Bind("Zip") %>'></asp:TextBox>
                <br />
                <label>Home Phone</label>
                <asp:TextBox ID="newHomePhone" runat="server" Text='<%# Bind("HomePhone") %>'></asp:TextBox>
                <br />
                <label>Home Email:</label>
                <asp:TextBox ID="newHomeEmail" runat="server" Text='<%# Bind("HomeEmail") %>'></asp:TextBox>
                <br />
                <label>Mail Preference</label>
                <asp:TextBox ID="newMailPreference" runat="server" Text='<%# Bind("MailPreference") %>'></asp:TextBox>
                <br />
                <label>Active:</label>
                <asp:CheckBox ID="CheckBoxActivenew" runat="server" Checked='<%# Bind("Active") %>'/>
                <br />
                <label>Primary Contact:</label>
                <asp:CheckBox ID="CheckBoxPrimaryContactnew" runat="server" Checked='<%# Bind("PrimaryContact") %>'/>
                <br />
                <label>Training:</label>
                <asp:CheckBox ID="CheckBoxTrainingnew" runat="server" Checked='<%# Bind("Training") %>'/>

                <br /><br />
                <asp:HiddenField id="OrganizationID" runat="server" value='<%# Bind("OrganizationID") %>' />
                <asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
                <asp:Button ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
            </InsertItemTemplate>             
                   
        </asp:FormView>             
</asp:Content>
