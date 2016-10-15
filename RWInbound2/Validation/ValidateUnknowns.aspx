<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ValidateUnknowns.aspx.cs" Inherits="RWInbound2.Validation.ValidateUnknowns" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <asp:Label ID="Label1" CssClass="PageLabel" runat="server" Text="Validate Unknowns"></asp:Label>
        <br />
    <br />
    <asp:Label ID="Label2" runat="server" Text="Select by Organization: "></asp:Label>
    <asp:TextBox ID="tbOrgName" runat="server"></asp:TextBox>
        <ajaxToolkit:AutoCompleteExtender ID="tbOrgName_AutoCompleteExtender" runat="server" 
             ServiceMethod="SearchOrgs" 
            CompletionSetCount="2" MinimumPrefixLength="2"
            TargetControlID="tbOrgName">
        </ajaxToolkit:AutoCompleteExtender>
    <asp:Button ID="btnSelectOrg" runat="server" Text="Select" OnClick="btnSelectOrg_Click"/>
&nbsp;
    <br />
    <asp:Label ID="lblNumberLeft" runat="server" ></asp:Label>
   
        <br />
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
   
        <br />
   
    <br />

            <asp:FormView ID="FormView1" runat="server" AllowPaging="True" DefaultMode="Edit" PagerSettings-Position="TopAndBottom" DataKeyNames="UnknownSampleID" DataSourceID="SqlDataSource1" Width="442px">
                <EditItemTemplate>
<%--                    Organization ID:
                    <asp:TextBox ID="OrganizationIDTextBox" Enabled="false" runat="server" Text='<%# Bind("OrganizationID") %>' />
                    <br />
                    Unknown Sample ID:
                    <asp:Label ID="UnknownSampleIDLabel1" runat="server" Text='<%# Eval("UnknownSampleID") %>' />
                    <br />--%>
                    &nbsp;<asp:TextBox ID="OrganizationIDTextBox" runat="server" Text='<%# Bind("OrganizationID") %>' />
                    <br />
                    UnknownSample ID:
                    <asp:Label ID="UnknownSampleIDLabel1" runat="server" Text='<%# Eval("UnknownSampleID") %>' />
                    <br />
                    Sample Type:
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
<%--                    <asp:TextBox ID="SampleTypeTextBox" runat="server" Text='<%# Bind("SampleType") %>' />--%>
                    <br />
                    Sample Number:
                    <asp:TextBox ID="SampleNumberTextBox" runat="server" Text='<%# Bind("SampleNumber") %>' />
                    <br />
                    Batch Sample Number:
                    <asp:TextBox ID="BatchSampleNumberTextBox" runat="server" Text='<%# Bind("BatchSampleNumber") %>' />
                    <br />
                    Date Sent:
                    <asp:TextBox ID="DateSentTextBox" runat="server" Text='<%# Bind("DateSent") %>' />
                    <br />
                    Value1:
                    <asp:TextBox ID="Value1TextBox" runat="server" Text='<%# Bind("Value1") %>' />
                    <br />
                    Value2:
                    <asp:TextBox ID="Value2TextBox" runat="server" Text='<%# Bind("Value2") %>' />
                    <br />
                    Mean Value:
                    <asp:TextBox ID="MeanValueTextBox" runat="server" Text='<%# Bind("MeanValue") %>' />
                    <br />
                    True Value:
                    <asp:TextBox ID="TrueValueTextBox" OnTextChanged="TrueValueTextBox_TextChanged" runat="server" Text='<%# Bind("TrueValue") %>' />
                                <asp:Button ID="btnCalc" runat="server" OnClick="btnCalc_Click" Text="Calculate" />
                            <br />
                    Pct Recovery:
                    <asp:TextBox ID="PctRecoveryTextBox" runat="server" Text='<%# Bind("PctRecovery","{0.00}") %>' />
                    <br />
                    Round:
                    <asp:TextBox ID="RoundTextBox" runat="server" Text='<%# Bind("Round") %>' />
                    <br />
                    Comment:
                    <asp:TextBox ID="CommentTextBox" runat="server" Text='<%# Bind("Comment") %>' />
                    <br />
<%--                    OldValidated:
                    <asp:TextBox ID="OldValidatedTextBox" runat="server" Text='<%# Bind("OldValidated") %>' />
                    <br />--%>
<%--                    &nbsp;<asp:TextBox ID="OldValidatedTextBox" runat="server" Text='<%# Bind("OldValidated") %>' />
                    <br />--%>
                    Path:
                    
             <asp:DropDownList ID="ddlPath" runat="server"
                    SelectedValue='<%# Bind("Path") %>'
                    OnDataBinding="PreventErrorsOn_DataBinding">
                    <asp:ListItem Value="M">Mail</asp:ListItem>
                    <asp:ListItem Value="SV">Site Visit</asp:ListItem>
                        </asp:DropDownList>
<%--                    <asp:TextBox ID="PathTextBox" runat="server" Text='<%# Bind("Path") %>' />--%>
                    <br />
                    DateCreated:
                    <asp:TextBox ID="DateCreatedTextBox" runat="server" Text='<%# Bind("DateCreated") %>' />
                    <br />
                    <%--UserCreated:
                    <asp:TextBox ID="UserCreatedTextBox" runat="server" Text='<%# Bind("UserCreated") %>' />
                    <br />
                    DateLastModified:
                    <asp:TextBox ID="DateLastModifiedTextBox" runat="server" Text='<%# Bind("DateLastModified") %>' />
                    <br />
                    UserLastModified:
                    <asp:TextBox ID="UserLastModifiedTextBox" runat="server" Text='<%# Bind("UserLastModified") %>' />
                    <br />--%>
<%--                    &nbsp;<asp:TextBox ID="UserCreatedTextBox" runat="server" Text='<%# Bind("UserCreated") %>' />
                    <br />--%>
<%--                    Date Last Modified:  
                    <asp:TextBox ID="DateLastModifiedTextBox" runat="server" Text='<%# Bind("DateLastModified") %>' />
                    <br />--%>
<%--                    Valid:
                    <asp:CheckBox ID="ValidCheckBox" runat="server" Checked='<%# Bind("Valid") %>' />
                    <br />
                    Validated:
                    <asp:CheckBox ID="ValidatedCheckBox" runat="server" Checked='<%# Bind("Validated") %>' />
                    <br />--%>
<%--                    &nbsp;<asp:TextBox ID="UserLastModifiedTextBox" runat="server" Text='<%# Bind("UserLastModified") %>' />
                    <br />--%>

<%--                    Valid:
                    <asp:CheckBox ID="ValidCheckBox" runat="server" Checked='<%# Bind("Valid") %>' />
                    <br />
                    Validated:
                    <asp:CheckBox ID="ValidatedCheckBox" runat="server" Checked='<%# Bind("Validated") %>' />
                    <br />--%>

                    <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
                    &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
                </EditItemTemplate>
              
               
                <ItemTemplate>
                    OrganizationID:
                    <asp:Label ID="OrganizationIDLabel" runat="server" Text='<%# Bind("OrganizationID") %>' />
                    <br />
                    UnknownSampleID:
                    <asp:Label ID="UnknownSampleIDLabel" runat="server" Text='<%# Eval("UnknownSampleID") %>' />
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
                    Value1:
                    <asp:Label ID="Value1Label" runat="server" Text='<%# Bind("Value1") %>' />
                    <br />
                    Value2:
                    <asp:Label ID="Value2Label" runat="server" Text='<%# Bind("Value2") %>' />
                    <br />
                    MeanValue:
                    <asp:Label ID="MeanValueLabel" runat="server" Text='<%# Bind("MeanValue") %>' />
                    <br />
                    TrueValue:
                    <asp:Label ID="TrueValueLabel" runat="server" Text='<%# Bind("TrueValue") %>' />
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
                    DateCreated:
                    <asp:Label ID="DateCreatedLabel" runat="server" Text='<%# Bind("DateCreated") %>' />
                    <br />
                    UserCreated:
                    <asp:Label ID="UserCreatedLabel" runat="server" Text='<%# Bind("UserCreated") %>' />
                    <br />
                    DateLastModified:
                    <asp:Label ID="DateLastModifiedLabel" runat="server" Text='<%# Bind("DateLastModified") %>' />
                    <br />
                    UserLastModified:
                    <asp:Label ID="UserLastModifiedLabel" runat="server" Text='<%# Bind("UserLastModified") %>' />
                    <br />
                    BatchSampleNumber:
                    <asp:Label ID="BatchSampleNumberLabel" runat="server" Text='<%# Bind("BatchSampleNumber") %>' />
                    <br />
                    Valid:
                    <asp:CheckBox ID="ValidCheckBox" runat="server" Checked='<%# Bind("Valid") %>' Enabled="false" />
                    <br />
                    Validated:
                    <asp:CheckBox ID="ValidatedCheckBox" runat="server" Checked='<%# Bind("Validated") %>' Enabled="false" />
                    <br />
                    PctRecovery:
                    <asp:Label ID="PctRecoveryLabel" runat="server" Text='<%# Bind("PctRecovery") %>' />
                    <br />
                    <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit" />
                    &nbsp;<asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" />
                    &nbsp;<asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" CommandName="New" Text="New" />
                </ItemTemplate>

<PagerSettings Position="TopAndBottom"></PagerSettings>
              
            </asp:FormView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" OnUpdating="SqlDataSource1_Updating" ConnectionString="<%$ ConnectionStrings:RiverWatchDEV %>" 
               
                UpdateCommand="UPDATE [UnknownSample] SET [OrganizationID] = @OrganizationID, [SampleType] = @SampleType, [SampleNumber] = @SampleNumber, [DateSent] = @DateSent, [Value1] = @Value1, [Value2] = @Value2, [MeanValue] = @MeanValue, [TrueValue] = @TrueValue, [Round] = @Round, [Comment] = @Comment, [OldValidated] = @OldValidated, [Path] = @Path, [DateCreated] = @DateCreated, [UserCreated] = @UserCreated, [DateLastModified] = @DateLastModified, [UserLastModified] = @UserLastModified, [BatchSampleNumber] = @BatchSampleNumber, [Valid] = @Valid, [Validated] = @Validated, [PctRecovery] = @PctRecovery WHERE [UnknownSampleID] = @UnknownSampleID" DeleteCommand="DELETE FROM [UnknownSample] WHERE [UnknownSampleID] = @UnknownSampleID" InsertCommand="INSERT INTO [UnknownSample] ([OrganizationID], [SampleType], [SampleNumber], [DateSent], [Value1], [Value2], [MeanValue], [TrueValue], [Round], [Comment], [OldValidated], [Path], [DateCreated], [UserCreated], [DateLastModified], [UserLastModified], [BatchSampleNumber], [Valid], [Validated], [PctRecovery]) VALUES (@OrganizationID, @SampleType, @SampleNumber, @DateSent, @Value1, @Value2, @MeanValue, @TrueValue, @Round, @Comment, @OldValidated, @Path, @DateCreated, @UserCreated, @DateLastModified, @UserLastModified, @BatchSampleNumber, @Valid, @Validated, @PctRecovery)" SelectCommand="SELECT * FROM [UnknownSample]">
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

   
    <br />


</asp:Content>
