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

            <asp:FormView ID="FormView1" runat="server" AllowPaging="True" DefaultMode="Edit" PagerSettings-Position="TopAndBottom" DataKeyNames="UnknownSampleID" DataSourceID="SqlDataSource1">
                <EditItemTemplate>
<%--                    Organization ID:
                    <asp:TextBox ID="OrganizationIDTextBox" Enabled="false" runat="server" Text='<%# Bind("OrganizationID") %>' />
                    <br />
                    Unknown Sample ID:
                    <asp:Label ID="UnknownSampleIDLabel1" runat="server" Text='<%# Eval("UnknownSampleID") %>' />
                    <br />--%>
                    Sample Type:
                    <asp:TextBox ID="SampleTypeTextBox" runat="server" Text='<%# Bind("SampleType") %>' />
                    <br />
                    Sample Number:
                    <asp:TextBox ID="SampleNumberTextBox" runat="server" Text='<%# Bind("SampleNumber") %>' />
                    <br />
                    Date Sent:
                    <asp:TextBox ID="DateSentTextBox" runat="server" Text='<%# Bind("DateSent") %>' />
                    <br />
                    Value 1:
                    <asp:TextBox ID="Value1TextBox" OnTextChanged="TextBox_TextChanged" runat="server" Text='<%# Bind("Value1") %>' />
                    <br />
                    Value 2:
                    <asp:TextBox ID="Value2TextBox" OnTextChanged="TextBox_TextChanged" runat="server" Text='<%# Bind("Value2") %>' />
                    <br />
                    Mean Value:
                    <asp:TextBox ID="MeanValueTextBox" runat="server" Text='<%# Bind("MeanValue") %>' />
                    <br />
                    True Value:
                    <asp:TextBox ID="TrueValueTextBox" runat="server" Text='<%# Bind("TrueValue") %>' />
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
                    Path:
                    <asp:TextBox ID="PathTextBox" runat="server" Text='<%# Bind("Path") %>' />
                    <br />
                    Date Created:
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
                    Batch Sample Number:  
                    <asp:TextBox ID="BatchSampleNumberTextBox" runat="server" Text='<%# Bind("BatchSampleNumber") %>' />
                    <br />
<%--                    Valid:
                    <asp:CheckBox ID="ValidCheckBox" runat="server" Checked='<%# Bind("Valid") %>' />
                    <br />
                    Validated:
                    <asp:CheckBox ID="ValidatedCheckBox" runat="server" Checked='<%# Bind("Validated") %>' />
                    <br />--%>
                    <asp:Button ID="UpdateButton" runat="server" CausesValidation="True"  OnClick="UpdateButton_Click" CommandName="Update" Text="Validate" />
                    &nbsp;<asp:Button ID="btnBAD" runat="server" CausesValidation="true" OnClick="btnBAD_Click" Text="BAD" />
                </EditItemTemplate>
              
            </asp:FormView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" OnUpdating="SqlDataSource1_Updating" ConnectionString="<%$ ConnectionStrings:RiverwatchDEV %>" 
               
                UpdateCommand="UPDATE [UnknownSample] SET [OrganizationID] = @OrganizationID, [SampleType] = @SampleType, [SampleNumber] = @SampleNumber, [DateSent] = @DateSent, [Value1] = @Value1, [Value2] = @Value2, [MeanValue] = @MeanValue, [TrueValue] = @TrueValue, [Round] = @Round, [Comment] = @Comment, [OldValidated] = @OldValidated, [Path] = @Path, [DateCreated] = @DateCreated, [UserCreated] = @UserCreated, [DateLastModified] = @DateLastModified, [UserLastModified] = @UserLastModified, [BatchSampleNumber] = @BatchSampleNumber, [Valid] = @Valid, [Validated] = @Validated WHERE [UnknownSampleID] = @UnknownSampleID">
                <DeleteParameters>
                    <asp:Parameter Name="UnknownSampleID" Type="Int32" />
                </DeleteParameters>
               
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
                    <asp:Parameter Name="UnknownSampleID" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>

   
    <br />


</asp:Content>
