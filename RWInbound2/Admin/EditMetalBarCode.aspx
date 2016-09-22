<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditMetalBarCode.aspx.cs" Inherits="RWInbound2.Admin.EditMetalBarCode" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    <br />
    <br />
    <asp:Label ID="Label2" runat="server" Text="Select Barcode: "></asp:Label>
    <asp:TextBox ID="tbSelectBarcode" runat="server"></asp:TextBox>
    <ajaxToolkit:AutoCompleteExtender ID="tbSelectBarcode_AutoCompleteExtender" runat="server" TargetControlID="tbSelectBarcode"
        ServiceMethod="SearchBarcodes" CompletionSetCount="2" MinimumPrefixLength="2">
    </ajaxToolkit:AutoCompleteExtender>
    <asp:Button ID="btnSelectBarCode" runat="server" Text="Select" OnClick="btnSelectBarCode_Click" />
    <br />
    <asp:FormView ID="FormView1" runat="server" DataKeyNames="ID" DefaultMode="Edit" DataSourceID="SqlDataSource1">
        <EditItemTemplate>
            ID:
            <asp:Label ID="IDLabel1" runat="server" Text='<%# Eval("ID") %>'  />
            <br />
            SampleNumber:
            <asp:TextBox ID="SampleNumberTextBox" runat="server" Text='<%# Bind("SampleNumber") %>' ReadOnly="True"  />
            <br />
            NumberSample:
            <asp:TextBox ID="NumberSampleTextBox"  runat="server" Text='<%# Bind("NumberSample") %>' ReadOnly="True" />
            <br />
            LabID:
            <asp:TextBox ID="LabIDTextBox" runat="server" Text='<%# Bind("LabID") %>' ReadOnly="True" />
            <br />
            Code:
            <asp:TextBox ID="CodeTextBox" runat="server" Text='<%# Bind("Code") %>' />
            <br />
            Type:
            <asp:TextBox ID="TypeTextBox" runat="server" Text='<%# Bind("Type") %>' />
            <br />
            Filtered:
            <asp:CheckBox ID="FilteredCheckBox" runat="server" Checked='<%# Bind("Filtered") %>' />
            <br />
            ContainMetal:
            <asp:CheckBox ID="ContainMetalCheckBox" runat="server" Checked='<%# Bind("ContainMetal") %>' />
            <br />
            BoxNumber:
            <asp:TextBox ID="BoxNumberTextBox" runat="server" Text='<%# Bind("BoxNumber") %>' />
            <br />
            Verified:
            <asp:CheckBox ID="VerifiedCheckBox" runat="server" Checked='<%# Bind("Verified") %>' />
            <br />
            LogDate:
            <asp:TextBox ID="LogDateTextBox" runat="server" Text='<%# Bind("LogDate") %>' />
            <br />
            AnalyzeDate:
            <asp:TextBox ID="AnalyzeDateTextBox" runat="server" Text='<%# Bind("AnalyzeDate") %>' />
            <br />
            DateCreated:
            <asp:TextBox ID="DateCreatedTextBox" runat="server" Text='<%# Bind("DateCreated") %>' ReadOnly="True"/>
            <br />
            UserCreated:
            <asp:TextBox ID="UserCreatedTextBox" runat="server" Text='<%# Bind("UserCreated") %>' ReadOnly="True"/>
            <br />
           
            SampleID:
            <asp:TextBox ID="SampleIDTextBox" runat="server" Text='<%# Bind("SampleID") %>' ReadOnly="True" />
            <br />
            <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
            &nbsp;<asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
             &nbsp; <asp:Button ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" />
        </EditItemTemplate>
        <InsertItemTemplate>
            SampleNumber:
            <asp:TextBox ID="SampleNumberTextBox" runat="server" Text='<%# Bind("SampleNumber") %>' />
            <br />
            NumberSample:
            <asp:TextBox ID="NumberSampleTextBox" runat="server" Text='<%# Bind("NumberSample") %>' />
            <br />
            LabID:
            <asp:TextBox ID="LabIDTextBox" runat="server" Text='<%# Bind("LabID") %>' />
            <br />
            Code:
            <asp:TextBox ID="CodeTextBox" runat="server" Text='<%# Bind("Code") %>' />
            <br />
            Type:
            <asp:TextBox ID="TypeTextBox" runat="server" Text='<%# Bind("Type") %>' />
            <br />
            Filtered:
            <asp:CheckBox ID="FilteredCheckBox" runat="server" Checked='<%# Bind("Filtered") %>' />
            <br />
            ContainMetal:
            <asp:CheckBox ID="ContainMetalCheckBox" runat="server" Checked='<%# Bind("ContainMetal") %>' />
            <br />
            BoxNumber:
            <asp:TextBox ID="BoxNumberTextBox" runat="server" Text='<%# Bind("BoxNumber") %>' />
            <br />
            Verified:
            <asp:CheckBox ID="VerifiedCheckBox" runat="server" Checked='<%# Bind("Verified") %>' />
            <br />
            LogDate:
            <asp:TextBox ID="LogDateTextBox" runat="server" Text='<%# Bind("LogDate") %>' />
            <br />
            AnalyzeDate:
            <asp:TextBox ID="AnalyzeDateTextBox" runat="server" Text='<%# Bind("AnalyzeDate") %>' />
            <br />
            DateCreated:
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
            <br />
            SampleID:
            <asp:TextBox ID="SampleIDTextBox" runat="server" Text='<%# Bind("SampleID") %>' />
            <br />
            <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
            &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
        </InsertItemTemplate>
        <ItemTemplate>
            ID:
            <asp:Label ID="IDLabel" runat="server" Text='<%# Eval("ID") %>' />
            <br />
            SampleNumber:
            <asp:Label ID="SampleNumberLabel" runat="server" Text='<%# Bind("SampleNumber") %>' />
            <br />
            NumberSample:
            <asp:Label ID="NumberSampleLabel" runat="server" Text='<%# Bind("NumberSample") %>' />
            <br />
            LabID:
            <asp:Label ID="LabIDLabel" runat="server" Text='<%# Bind("LabID") %>' />
            <br />
            Code:
            <asp:Label ID="CodeLabel" runat="server" Text='<%# Bind("Code") %>' />
            <br />
            Type:
            <asp:Label ID="TypeLabel" runat="server" Text='<%# Bind("Type") %>' />
            <br />
            Filtered:
            <asp:CheckBox ID="FilteredCheckBox" runat="server" Checked='<%# Bind("Filtered") %>' Enabled="false" />
            <br />
            ContainMetal:
            <asp:CheckBox ID="ContainMetalCheckBox" runat="server" Checked='<%# Bind("ContainMetal") %>' Enabled="false" />
            <br />
            BoxNumber:
            <asp:Label ID="BoxNumberLabel" runat="server" Text='<%# Bind("BoxNumber") %>' />
            <br />
            Verified:
            <asp:CheckBox ID="VerifiedCheckBox" runat="server" Checked='<%# Bind("Verified") %>' Enabled="false" />
            <br />
            LogDate:
            <asp:Label ID="LogDateLabel" runat="server" Text='<%# Bind("LogDate") %>' />
            <br />
            AnalyzeDate:
            <asp:Label ID="AnalyzeDateLabel" runat="server" Text='<%# Bind("AnalyzeDate") %>' />
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
            SampleID:
            <asp:Label ID="SampleIDLabel" runat="server" Text='<%# Bind("SampleID") %>' />
            <br />
            <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit" />
            &nbsp;<asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" />
            &nbsp;<asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" CommandName="New" Text="New" />
        </ItemTemplate>
    </asp:FormView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server"  OnUpdating="SqlDataSource1_Updating" ConnectionString="<%$ ConnectionStrings:RiverwatchDEV %>" 
        DeleteCommand="DELETE FROM [MetalBarCode] WHERE [ID] = @ID" 
        InsertCommand="INSERT INTO [MetalBarCode] ([SampleNumber], [NumberSample], [LabID], [Code], [Type], [Filtered], [ContainMetal], [BoxNumber], [Verified], [LogDate], [AnalyzeDate], [DateCreated], [UserCreated], [DateLastModified], [UserLastModified], [SampleID]) VALUES (@SampleNumber, @NumberSample, @LabID, @Code, @Type, @Filtered, @ContainMetal, @BoxNumber, @Verified, @LogDate, @AnalyzeDate, @DateCreated, @UserCreated, @DateLastModified, @UserLastModified, @SampleID)" 
        SelectCommand="SELECT * FROM [MetalBarCode]" 
        UpdateCommand="UPDATE [MetalBarCode] SET [SampleNumber] = @SampleNumber, [NumberSample] = @NumberSample, [LabID] = @LabID, [Code] = @Code, [Type] = @Type, [Filtered] = @Filtered, [ContainMetal] = @ContainMetal, [BoxNumber] = @BoxNumber, [Verified] = @Verified, [LogDate] = @LogDate, [AnalyzeDate] = @AnalyzeDate, [DateCreated] = @DateCreated, [UserCreated] = @UserCreated, [DateLastModified] = @DateLastModified, [UserLastModified] = @UserLastModified, [SampleID] = @SampleID WHERE [ID] = @ID">
        <DeleteParameters>
            <asp:Parameter Name="ID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="SampleNumber" Type="String" />
            <asp:Parameter Name="NumberSample" Type="String" />
            <asp:Parameter Name="LabID" Type="String" />
            <asp:Parameter Name="Code" Type="String" />
            <asp:Parameter Name="Type" Type="String" />
            <asp:Parameter Name="Filtered" Type="Boolean" />
            <asp:Parameter Name="ContainMetal" Type="Boolean" />
            <asp:Parameter Name="BoxNumber" Type="String" />
            <asp:Parameter Name="Verified" Type="Boolean" />
            <asp:Parameter Name="LogDate" Type="DateTime" />
            <asp:Parameter Name="AnalyzeDate" Type="DateTime" />
            <asp:Parameter Name="DateCreated" Type="DateTime" />
            <asp:Parameter Name="UserCreated" Type="String" />
            <asp:Parameter Name="DateLastModified" Type="DateTime" />
            <asp:Parameter Name="UserLastModified" Type="String" />
            <asp:Parameter Name="SampleID" Type="Int32" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="SampleNumber" Type="String" />
            <asp:Parameter Name="NumberSample" Type="String" />
            <asp:Parameter Name="LabID" Type="String" />
            <asp:Parameter Name="Code" Type="String" />
            <asp:Parameter Name="Type" Type="String" />
            <asp:Parameter Name="Filtered" Type="Boolean" />
            <asp:Parameter Name="ContainMetal" Type="Boolean" />
            <asp:Parameter Name="BoxNumber" Type="String" />
            <asp:Parameter Name="Verified" Type="Boolean" />
            <asp:Parameter Name="LogDate" Type="DateTime" />
            <asp:Parameter Name="AnalyzeDate" Type="DateTime" />
            <asp:Parameter Name="DateCreated" Type="DateTime" />
            <asp:Parameter Name="UserCreated" Type="String" />
            <asp:Parameter Name="DateLastModified" Type="DateTime" />
            <asp:Parameter Name="UserLastModified" Type="String" />
            <asp:Parameter Name="SampleID" Type="Int32" />
            <asp:Parameter Name="ID" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <br />


</asp:Content>
