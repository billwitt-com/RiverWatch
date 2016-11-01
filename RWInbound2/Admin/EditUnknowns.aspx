<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditUnknowns.aspx.cs" Inherits="RWInbound2.Admin.EditUnknowns" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Label ID="Label1" runat="server" CssClass="PageLabel" Text="Edit Unknowns"></asp:Label>
    <br />
    <br />    <asp:Label ID="Label2" runat="server" Text="Select Organization: "></asp:Label>
    <asp:TextBox ID="tbOrgName" runat="server" Height="19px" Width="256px"></asp:TextBox>
        <ajaxToolkit:AutoCompleteExtender ID="tbOrgName_AutoCompleteExtender" runat="server" 
             ServiceMethod="SearchOrgs" 
            CompletionSetCount="2" MinimumPrefixLength="2"
            TargetControlID="tbOrgName">
        </ajaxToolkit:AutoCompleteExtender>
    <asp:Button ID="btnSelectOrg" runat="server" Text="Select" OnClick="btnSelectOrg_Click"/>
&nbsp;
    <asp:Button ID="btnNew" runat="server" OnClick="btnNew_Click" Text="Add New" />
    <br />    
    <asp:Label ID="lblNumberLeft" runat="server" ></asp:Label>
        <br />

        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
   
    <br />

<%--    <asp:DropDownList ID="ddlSampleType" runat="server">
        <asp:ListItem>DA</asp:ListItem>
        <asp:ListItem>DO</asp:ListItem>
        <asp:ListItem>DH</asp:ListItem>
        <asp:ListItem>A</asp:ListItem>
        <asp:ListItem>P</asp:ListItem>
        <asp:ListItem>H</asp:ListItem>
    </asp:DropDownList>--%>

<%--                <asp:DropDownList ID="ddlSampleType" runat="server"
                
                >
                    <asp:ListItem Value="M">Mail</asp:ListItem>
                    <asp:ListItem Value="SV">Site Visit</asp:ListItem>
    </asp:DropDownList>--%>

    <asp:FormView ID ="FormView1" runat="server" DefaultMode="ReadOnly" Width="665px" DataKeyNames="UnknownSampleID" 
         AllowPaging="true" PagerSettings-Mode="NumericFirstLast"
        OnItemUpdated ="FormView1_ItemUpdated"
        OnItemInserted="FormView1_ItemInserted"
        OnDataBound = "FormView1_DataBound" 
        DataSourceID = "SqlDataSource1">
        <EditItemTemplate> 
            OrganizationID:

            BatchSampleNumber:
            <asp:TextBox ID="BatchSampleNumberTextBox" runat="server" Text='<%# Bind("BatchSampleNumber") %>' />
                        <asp:TextBox ID="OrganizationIDTextBox" Width="0px"  runat="server" Text='<%# Bind("OrganizationID") %>' ForeColor="White" />
            <br />

            SampleType:
                <asp:DropDownList ID="ddlSampleType"
                    SelectedValue='<%# Bind("SampleType") %>'
                    OnDataBinding="PreventErrorsOn_DataBinding"
                    runat="server">
                    <asp:ListItem>DA</asp:ListItem>
                    <asp:ListItem>DO</asp:ListItem>
                    <asp:ListItem>DH</asp:ListItem>
                    <asp:ListItem>A</asp:ListItem>
                    <asp:ListItem>P</asp:ListItem>
                    <asp:ListItem>H</asp:ListItem>
                </asp:DropDownList>
<%--            <asp:TextBox ID="SampleTypeTextBox" runat="server" Text='<%# Bind("SampleType") %>' />--%>
            <br />
            SampleNumber:
            <asp:TextBox ID="SampleNumberTextBox" runat="server" Text='<%# Bind("SampleNumber") %>' />
            <br />
            DateSent:
            <asp:TextBox ID="DateSentTextBox" runat="server" Text='<%# Bind("DateSent") %>' />
            <br />
            Vol Value 1 / DO Value 2:
            <asp:TextBox ID="Value1TextBox" runat="server" Text='<%# Bind("Value1") %>' />
            <br />
            Vol Value 2:
            <asp:TextBox ID="Value2TextBox" runat="server" Text='<%# Bind("Value2") %>' />
            <br />
            TrueValue:
            <asp:TextBox ID="TrueValueTextBox" OnTextChanged="TrueValueTextBox_TextChanged"  AutoPostBack="true" runat="server" Text='<%# Bind("TrueValue") %>' />
            <asp:Button ID="btnCalc" runat="server" OnClick="btnCalc_Click" Text="Calculate" />
            <br />
            MeanValue:
            <asp:TextBox ID="MeanValueTextBox" runat="server" Text='<%# Bind("MeanValue") %>' />
            <br />
            PctRecovery:
            <asp:TextBox ID="PctRecoveryTextBox" runat="server" Text='<%# Bind("PctRecovery") %>' />
            <br />
            Round:
            <asp:TextBox ID="RoundTextBox" runat="server" Text='<%# Bind("Round") %>' />
            <br />
            Comment:
            <asp:TextBox ID="CommentTextBox" runat="server" Text='<%# Bind("Comment") %>' TextMode="MultiLine" Width="220px" />
            <br />
            Path:            
                <asp:DropDownList ID="ddlPath" runat="server"
                    SelectedValue='<%# Bind("Path") %>'

                    OnDataBinding="PreventErrorsOn_DataBinding">

                    <asp:ListItem Value="M">Mail</asp:ListItem>
                    <asp:ListItem Value="SV">Site Visit</asp:ListItem>
                </asp:DropDownList>
<%--            <asp:TextBox ID="PathTextBox" runat="server" Text='<%# Bind("Path") %>' />--%>
            <br />

            Valid:
            <asp:CheckBox ID="ValidCheckBox" Enabled="false" runat="server" Checked='<%# Bind("Valid") %>' />
            <br />
            Validated:
            <asp:CheckBox ID="ValidatedCheckBox" runat="server" Checked='<%# Bind("Validated") %>' />
            <br />

            <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CssClass="adminButton" CommandName="Update" Text="Update" />
            &nbsp;<asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False" CssClass="adminButton" CommandName="Cancel" Text="Cancel" />
        </EditItemTemplate>
        <InsertItemTemplate>
  
            BatchSampleNumber:
            <asp:TextBox ID="BatchSampleNumberTextBox" runat="server" Text='<%# Bind("BatchSampleNumber") %>' />
              OrganizationID:
            <asp:TextBox ID="OrganizationIDTextBox" runat="server"  ForeColor="White"  Width="0px" Text='<%# Bind("OrganizationID") %>' />
            <br />
            SampleType:
                <asp:DropDownList ID="ddlSampleType"
                    SelectedValue='<%# Bind("SampleType") %>'
                    OnDataBinding="PreventErrorsOn_DataBinding"
                    runat="server">
                    <asp:ListItem>DA</asp:ListItem>
                    <asp:ListItem>DO</asp:ListItem>
                    <asp:ListItem>DH</asp:ListItem>
                    <asp:ListItem>A</asp:ListItem>
                    <asp:ListItem>P</asp:ListItem>
                    <asp:ListItem>H</asp:ListItem>
                </asp:DropDownList>
<%--            <asp:TextBox ID="SampleTypeTextBox" runat="server" Text='<%# Bind("SampleType") %>' />--%>
            <br />
            SampleNumber:
            <asp:TextBox ID="SampleNumberTextBox" runat="server" Text='<%# Bind("SampleNumber") %>' />
            <br />
            DateSent:
            <asp:TextBox ID="DateSentTextBox" runat="server" Text='<%# Bind("DateSent") %>' />
            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"   TargetControlID="DateSentTextBox" />
            <br />
            Vol Value 1 / DO Value 2:
            <asp:TextBox ID="Value1TextBox" runat="server" Text='<%# Bind("Value1") %>' />
            <br />
            Vol Value 2:
            <asp:TextBox ID="Value2TextBox" runat="server" Text='<%# Bind("Value2") %>' />
            <br />

            True Value / RW Equip value / DO value 1:
            <asp:TextBox ID="TrueValueTextBox" OnTextChanged="TrueValueTextBox_TextChanged" AutoPostBack="true" runat="server" Text='<%# Bind("TrueValue") %>' />
            <asp:Button ID="btnCalc" runat="server" OnClick="btnCalc_Click" Text="Calculate" />
            <br />
            MeanValue:
            <asp:TextBox ID="MeanValueTextBox" runat="server" Text='<%# Bind("MeanValue") %>' />
            <br />
            PctRecovery:
            <asp:TextBox ID="PctRecoveryTextBox" runat="server" Text='<%# Bind("PctRecovery") %>' />
            <br />
            Round:
            <asp:TextBox ID="RoundTextBox" runat="server" Text='<%# Bind("Round") %>' />
            <br />
            Comment:
            <asp:TextBox ID="CommentTextBox" runat="server" Text='<%# Bind("Comment") %>' TextMode="MultiLine" Width="220px" />
            <br />
            OldValidated:
            <asp:TextBox ID="OldValidatedTextBox" runat="server" Text='<%# Bind("OldValidated") %>' />
            <br />
            Path:

             <asp:DropDownList ID="ddlPath" runat="server"
                    SelectedValue='<%# Bind("Path") %>'

                    OnDataBinding="PreventErrorsOn_DataBinding">

                    <asp:ListItem Value="M">Mail</asp:ListItem>
                    <asp:ListItem Value="SV">Site Visit</asp:ListItem>
                </asp:DropDownList>
<%--            <asp:TextBox ID="PathTextBox" runat="server" Text='<%# Bind("Path") %>' />--%>
 
            <br />
<%--            DateCreated:
            <asp:TextBox ID="DateCreatedTextBox" runat="server" Text='<%# Bind("DateCreated") %>' />
            <br />
            UserCreated:
            <asp:TextBox ID="UserCreatedTextBox" runat="server" Text='<%# Bind("UserCreated") %>' />
            <br />
            DateLastModified:
            <asp:TextBox ID="DateLastModifiedTextBox" runat="server" Text='<%# Bind("DateLastModified") %>' />
            <br />
            UserLastModified:
            <asp:TextBox ID="UserLastModifiedTextBox" runat="server" Text='<%# Bind("UserLastModified") %>' />
            <br />--%>

            Valid:
            <asp:CheckBox ID="ValidCheckBox" runat="server" Checked='<%# Bind("Valid") %>' />
            <br />
            Validated:
            <asp:CheckBox ID="ValidatedCheckBox" runat="server" Checked='<%# Bind("Validated") %>' />
            <br />
            <asp:Button ID="InsertButton" runat="server" CausesValidation="True" CssClass="adminButton" CommandName="Insert" Text="Insert" />
            &nbsp;<asp:Button ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
        </InsertItemTemplate>
        <ItemTemplate>
            OrganizationID:
            <asp:Label ID="OrganizationIDLabel" runat="server" Text='<%# Bind("OrganizationID") %>' />
            <br />
            UnknownSampleID:
            <asp:Label ID="UnknownSampleIDLabel" runat="server" Text='<%# Eval("UnknownSampleID") %>' />
            <br />
            BatchSampleNumber:
            <asp:Label ID="BatchSampleNumberLabel" runat="server" Text='<%# Bind("BatchSampleNumber") %>' />
            <br />
            SampleType:

            <asp:Label ID="SampleTypeLabel" runat="server" Text='<%# Bind("SampleType") %>' />
            <br />
            SampleNumber:
            <asp:Label ID="SampleNumberLabel" runat="server" Text='<%# Bind("SampleNumber") %>' />
            <br />
            DateSent:
            <asp:Label ID="DateSentLabel" runat="server" Text='<%# Bind("DateSent") %>' />
            <br />
            Vol Value 1 / DO Value 2:
            <asp:Label ID="Value1Label" runat="server" Text='<%# Bind("Value1") %>' />
            <br />
            Vol Value 2:
            <asp:Label ID="Value2Label" runat="server" Text='<%# Bind("Value2") %>' />
            <br />
            MeanValue:
            <asp:Label ID="MeanValueLabel" runat="server" Text='<%# Bind("MeanValue") %>' />
            <br />
            True Value / RW Equip value / DO value 1:
            <asp:Label ID="TrueValueLabel" runat="server" Text='<%# Bind("TrueValue") %>' />
            <br />
            PctRecovery:
            <asp:Label ID="PctRecoveryLabel" runat="server" Text = '<%# Bind("PctRecovery") %>' />
            <br />
            Round:
            <asp:Label ID="RoundLabel" runat="server" Text='<%# Bind("Round") %>' />
            <br />
            Comment:
            <asp:Label ID="CommentLabel" runat="server" Text='<%# Bind("Comment") %>' />
            <br />
            OldValidated:
            <asp:Label ID="OldValidatedLabel" runat="server" Text='<%# Bind("OldValidated") %>' />
            <br />
            Path:
            <asp:Label ID="PathLabel" runat="server" Text='<%# Bind("Path") %>' />
            <br />
           

            Valid:
            <asp:CheckBox ID="ValidCheckBox" runat="server" Checked='<%# Bind("Valid") %>' Enabled="false" />
            <br />
            Validated:
            <asp:CheckBox ID="ValidatedCheckBox" runat="server" Checked='<%# Bind("Validated") %>' Enabled="false" />
            <br />

            <asp:Button ID="EditButton" runat="server" CssClass="adminButton" CausesValidation="False" CommandName="Edit" Text="Edit" />
            &nbsp;<asp:Button ID="DeleteButton" runat="server" CssClass="adminButton" CausesValidation="False" CommandName="Delete" Text="Delete" />
<%--            &nbsp;<asp:Button ID="NewButton" runat="server" CausesValidation="False" OnClick="NewButton_Click" CommandName="New" Text="New" />--%>
        </ItemTemplate>
    </asp:FormView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RiverWatchDEV %>"  OnInserting="SqlDataSource1_Inserting" OnUpdating="SqlDataSource1_Updating"
        DeleteCommand="DELETE FROM [UnknownSample] WHERE [UnknownSampleID] = @UnknownSampleID" 
        InsertCommand="INSERT INTO [UnknownSample] ([OrganizationID], [SampleType], [SampleNumber], [DateSent], [Value1], [Value2], [MeanValue], [TrueValue], [Round], [Comment], [OldValidated], [Path], [DateCreated], [UserCreated], [DateLastModified], [UserLastModified], [BatchSampleNumber], [Valid], [Validated], [PctRecovery]) VALUES (@OrganizationID, @SampleType, @SampleNumber, @DateSent, @Value1, @Value2, @MeanValue, @TrueValue, @Round, @Comment, @OldValidated, @Path, @DateCreated, @UserCreated, @DateLastModified, @UserLastModified, @BatchSampleNumber, @Valid, @Validated, @PctRecovery)" 
        SelectCommand="SELECT * FROM [UnknownSample]" 
        UpdateCommand="UPDATE [UnknownSample] SET [OrganizationID] = @OrganizationID, [SampleType] = @SampleType, [SampleNumber] = @SampleNumber, [DateSent] = @DateSent, [Value1] = @Value1, [Value2] = @Value2, [MeanValue] = @MeanValue, [TrueValue] = @TrueValue, [Round] = @Round, [Comment] = @Comment, [OldValidated] = @OldValidated, [Path] = @Path, [DateCreated] = @DateCreated, [UserCreated] = @UserCreated, [DateLastModified] = @DateLastModified, [UserLastModified] = @UserLastModified, [BatchSampleNumber] = @BatchSampleNumber, [Valid] = @Valid, [Validated] = @Validated, [PctRecovery] = @PctRecovery WHERE [UnknownSampleID] = @UnknownSampleID">
        <DeleteParameters>
            <asp:Parameter Name="UnknownSampleID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="OrganizationID" Type="Int32" />
            <asp:Parameter Name="SampleType" Type="String" />
            <asp:Parameter Name="SampleNumber" Type="String" />
            <asp:Parameter Name="DateSent" Type="DateTime" />
            <asp:Parameter Name="Value1" Type="Decimal" />
            <asp:Parameter Name="Value2" Type="Decimal" />
            <asp:Parameter Name="MeanValue" Type="Decimal" />
            <asp:Parameter Name="TrueValue" Type="Decimal" />
            <asp:Parameter Name="Round" Type="Int32" />
            <asp:Parameter Name="Comment" Type="String" />
            <asp:Parameter Name="OldValidated" Type="String" />
            <asp:Parameter Name="Path" Type="String" />
            <asp:Parameter Name="DateCreated" Type="DateTime" />
            <asp:Parameter Name="UserCreated" Type="String" />
            <asp:Parameter Name="DateLastModified" Type="DateTime" />
            <asp:Parameter Name="UserLastModified" Type="String" />
            <asp:Parameter Name="BatchSampleNumber" Type="String" />
            <asp:Parameter Name="Valid" Type="Boolean" />
            <asp:Parameter Name="Validated" Type="Boolean" />
            <asp:Parameter Name="PctRecovery" Type="Decimal" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="OrganizationID" Type="Int32" />
            <asp:Parameter Name="SampleType" Type="String" />
            <asp:Parameter Name="SampleNumber" Type="String" />
            <asp:Parameter Name="DateSent" Type="DateTime" />
            <asp:Parameter Name="Value1" Type="Decimal" />
            <asp:Parameter Name="Value2" Type="Decimal" />
            <asp:Parameter Name="MeanValue" Type="Decimal" />
            <asp:Parameter Name="TrueValue" Type="Decimal" />
            <asp:Parameter Name="Round" Type="Int32" />
            <asp:Parameter Name="Comment" Type="String" />
            <asp:Parameter Name="OldValidated" Type="String" />
            <asp:Parameter Name="Path" Type="String" />
            <asp:Parameter Name="DateCreated" Type="DateTime" />
            <asp:Parameter Name="UserCreated" Type="String" />
            <asp:Parameter Name="DateLastModified" Type="DateTime" />
            <asp:Parameter Name="UserLastModified" Type="String" />
            <asp:Parameter Name="BatchSampleNumber" Type="String" />
            <asp:Parameter Name="Valid" Type="Boolean" />
            <asp:Parameter Name="Validated" Type="Boolean" />
            <asp:Parameter Name="PctRecovery" Type="Decimal" />
            <asp:Parameter Name="UnknownSampleID" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>
