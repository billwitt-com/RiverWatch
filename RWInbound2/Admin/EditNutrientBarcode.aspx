<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditNutrientBarcode.aspx.cs" Inherits="RWInbound2.Admin.EditNutrientBarcode" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label1" CssClass="PageLabel" runat="server" Text="Edit Nutrient Barcodes"></asp:Label>


    <br />
    <br />
    <table style="width: 100%; height: 80px;">
        <tr>
            <td class="rowId-charid-dsorder-textbox" style="width: 32px">&nbsp;</td>
            <td style="width: 157px">
                <asp:Label ID="Label2" runat="server" Text="Select Barcode: "></asp:Label>
            </td>
            <td style="width: 168px">
                <asp:TextBox ID="tbSelectBarcode" runat="server" Height="19px" Width="137px"></asp:TextBox>
                <ajaxToolkit:AutoCompleteExtender ID="tbSelectBarcode_AutoCompleteExtender" runat="server" TargetControlID="tbSelectBarcode"
                            ServiceMethod="SearchBarcodes" CompletionSetCount="2" MinimumPrefixLength="2">
                </ajaxToolkit:AutoCompleteExtender>
            </td>
            <td>
                <asp:Button ID="btnSelectBarcode" CssClass="adminButton" runat="server" Text="Select" OnClick="btnSelectBarcode_Click" />
            </td>
        </tr>
        <tr>
            <td class="rowId-charid-dsorder-textbox" style="width: 32px">&nbsp;</td>
            <td style="width: 157px">
                <asp:Label ID="Label3" runat="server" Text="Select Sample Number: "></asp:Label>
            </td>
            <td style="width: 168px">
                <asp:TextBox ID="tbSelectSampleNumber" runat="server" Height="19px" Width="137px"></asp:TextBox>
                <ajaxToolkit:AutoCompleteExtender ID="tbSelectSampleNumber_AutoCompleteExtender" runat="server" TargetControlID="tbSelectSampleNumber"
                            ServiceMethod="SearchSampleNumber" CompletionSetCount="2" MinimumPrefixLength="2">
                </ajaxToolkit:AutoCompleteExtender>
            </td>
            <td>
                <asp:Button ID="btnSelectSampleNumber" CssClass="adminButton" runat="server" Text="Select" OnClick="btnSelectSampleNumber_Click" />
            </td>
        </tr>

    </table>


    <br />
    <asp:FormView ID="FormView1" runat="server" DataKeyNames="ID" DefaultMode="Edit" DataSourceID="SqlDataSource1">
        <EditItemTemplate>
            Sample Number:
            <asp:TextBox ID="SampleNumberTextBox" runat="server" Text='<%# Bind("SampleNumber") %>' />
            <br />
            Lab ID (Barcode):
            <asp:TextBox ID="LabIDTextBox" runat="server" Text='<%# Bind("LabID") %>' />
            <br />
            Batch:
            <asp:TextBox ID="BatchTextBox" runat="server" Text='<%# Bind("Batch") %>' />
            <br />            


            Log Date:
            <asp:TextBox ID="LogDateTextBox" runat="server" Text='<%# Bind("LogDate") %>' />
            <br />
            Analyze Date:
            <asp:TextBox ID="AnalyzeDateTextBox" runat="server" Text='<%# Bind("AnalyzeDate") %>' />
            <br />
            Comment:
            <asp:TextBox ID="CommentTextBox" runat="server" Text='<%# Bind("Comment") %>' />
            <br />
            Date Created:
            <asp:TextBox ID="DateCreatedTextBox" runat="server" Text='<%# Bind("DateCreated") %>' />
            <br />
            User Created:
            <asp:TextBox ID="UserCreatedTextBox" runat="server" Text='<%# Bind("UserCreated") %>' />
            <br />

            Total Phos:
            <asp:CheckBox ID="TotalPhosCheckBox" runat="server" Checked='<%# Bind("TotalPhos") %>' />
            <br />
            Ortho Phos:
            <asp:CheckBox ID="OrthoPhosCheckBox" runat="server" Checked='<%# Bind("OrthoPhos") %>' />
            <br />
            Total Nitro:
            <asp:CheckBox ID="TotalNitroCheckBox" runat="server" Checked='<%# Bind("TotalNitro") %>' />
            <br />
            Nitrate Nitrite:
            <asp:CheckBox ID="NitrateNitriteCheckBox" runat="server" Checked='<%# Bind("NitrateNitrite") %>' />
            <br />
            Ammonia:
            <asp:CheckBox ID="AmmoniaCheckBox" runat="server" Checked='<%# Bind("Ammonia") %>' />
            <br />
            DOC:
            <asp:CheckBox ID="DOCCheckBox" runat="server" Checked='<%# Bind("DOC") %>' />
            <br />
            Chloride:
            <asp:CheckBox ID="ChlorideCheckBox" runat="server" Checked='<%# Bind("Chloride") %>' />
            <br />
            Sulfate:
            <asp:CheckBox ID="SulfateCheckBox" runat="server" Checked='<%# Bind("Sulfate") %>' />
            <br />
            TSS:
            <asp:CheckBox ID="TSSCheckBox" runat="server" Checked='<%# Bind("TSS") %>' />
            <br />
            Chlor A:
            <asp:CheckBox ID="ChlorACheckBox" runat="server" Checked='<%# Bind("ChlorA") %>' />
            <br />
          
            <asp:Label ID="IDLabel1" runat="server"  ForeColor="White" Text='<%# Eval("ID") %>' />
            <asp:TextBox ID="SampleIDTextBox" ForeColor="White" runat="server" Text='<%# Bind("SampleID") %>' />
   
            <asp:TextBox ID="DateLastModifiedTextBox" ForeColor="White" runat="server" Text='<%# Bind("DateLastModified") %>' />

            <asp:TextBox ID="UserLastModifiedTextBox" ForeColor="White" runat="server" Text='<%# Bind("UserLastModified") %>' />
            <br />
            <asp:Button ID="UpdateButton" CssClass="adminButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
            &nbsp;<asp:Button ID="UpdateCancelButton" CssClass="adminButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
        </EditItemTemplate>
       
        <ItemTemplate>
            Sample Number:
            <asp:Label ID="SampleNumberLabel" runat="server" Text='<%# Bind("SampleNumber") %>' />
            <br />
            Lab ID (Barcode):
            <asp:Label ID="LabIDLabel" runat="server" Text='<%# Bind("LabID") %>' />
            <br />
            Batch:
            <asp:Label ID="BatchLabel" runat="server" Text='<%# Bind("Batch") %>' />
            <br />

            Log Date:
            <asp:Label ID="LogDateLabel" runat="server" Text='<%# Bind("LogDate") %>' />
            <br />
            Analyze Date:
            <asp:Label ID="AnalyzeDateLabel" runat="server" Text='<%# Bind("AnalyzeDate") %>' />
            <br />
            Comment:
            <asp:Label ID="CommentLabel" runat="server" Text='<%# Bind("Comment") %>' />
            <br />
            Total Phos:
            <asp:CheckBox ID="TotalPhosCheckBox" runat="server" Checked='<%# Bind("TotalPhos") %>' Enabled="false" />
            <br />
            Ortho Phos:
            <asp:CheckBox ID="OrthoPhosCheckBox" runat="server" Checked='<%# Bind("OrthoPhos") %>' Enabled="false" />
            <br />
            Total Nitro:
            <asp:CheckBox ID="TotalNitroCheckBox" runat="server" Checked='<%# Bind("TotalNitro") %>' Enabled="false" />
            <br />
            Nitrate Nitrite:
            <asp:CheckBox ID="NitrateNitriteCheckBox" runat="server" Checked='<%# Bind("NitrateNitrite") %>' Enabled="false" />
            <br />
            Ammonia:
            <asp:CheckBox ID="AmmoniaCheckBox" runat="server" Checked='<%# Bind("Ammonia") %>' Enabled="false" />
            <br />
            DOC:
            <asp:CheckBox ID="DOCCheckBox" runat="server" Checked='<%# Bind("DOC") %>' Enabled="false" />
            <br />
            Chloride:
            <asp:CheckBox ID="ChlorideCheckBox" runat="server" Checked='<%# Bind("Chloride") %>' Enabled="false" />
            <br />
            Sulfate:
            <asp:CheckBox ID="SulfateCheckBox" runat="server" Checked='<%# Bind("Sulfate") %>' Enabled="false" />
            <br />
            TSS:
            <asp:CheckBox ID="TSSCheckBox" runat="server" Checked='<%# Bind("TSS") %>' Enabled="false" />
            <br />
            Chlor A:
            <asp:CheckBox ID="ChlorACheckBox" runat="server" Checked='<%# Bind("ChlorA") %>' Enabled="false" />
            <br />
            <asp:Button ID="EditButton" CssClass="adminButton" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit" />
            &nbsp;<asp:Button ID="DeleteButton" CssClass="adminButton" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" />
            &nbsp;<asp:Button ID="NewButton" CssClass="adminButton" runat="server" CausesValidation="False" CommandName="New" Text="New" />
        </ItemTemplate>

    </asp:FormView>
    <asp:SqlDataSource ID="SqlDataSource1" OnDataBinding="SqlDataSource1_DataBinding" runat="server" ConnectionString="<%$ ConnectionStrings:RiverwatchDEV %>" 
        DeleteCommand="DELETE FROM [NutrientBarCode] WHERE [ID] = @ID" 
        InsertCommand="INSERT INTO [NutrientBarCode] ([SampleID], [SampleNumber], [LabID], [Batch],  [LogDate], [AnalyzeDate], [Comment], [DateCreated], [UserCreated], [DateLastModified], [UserLastModified], [TotalPhos], [OrthoPhos], [TotalNitro], [NitrateNitrite], [Ammonia], [DOC], [Chloride], [Sulfate], [TSS], [ChlorA]) VALUES (@SampleID, @SampleNumber, @LabID, @Batch, @LogDate, @AnalyzeDate, @Comment, @DateCreated, @UserCreated, @DateLastModified, @UserLastModified, @TotalPhos, @OrthoPhos, @TotalNitro, @NitrateNitrite, @Ammonia, @DOC, @Chloride, @Sulfate, @TSS, @ChlorA)" 


        UpdateCommand="UPDATE [NutrientBarCode] SET [SampleID] = @SampleID, [SampleNumber] = @SampleNumber, [LabID] = @LabID, [Batch] = @Batch, [LogDate] = @LogDate, [AnalyzeDate] = @AnalyzeDate, [Comment] = @Comment, [DateCreated] = @DateCreated, [UserCreated] = @UserCreated, [DateLastModified] = @DateLastModified, [UserLastModified] = @UserLastModified, [TotalPhos] = @TotalPhos, [OrthoPhos] = @OrthoPhos, [TotalNitro] = @TotalNitro, [NitrateNitrite] = @NitrateNitrite, [Ammonia] = @Ammonia, [DOC] = @DOC, [Chloride] = @Chloride, [Sulfate] = @Sulfate, [TSS] = @TSS, [ChlorA] = @ChlorA WHERE [ID] = @ID">
        <DeleteParameters>
            <asp:Parameter Name="ID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="SampleID" Type="Int32" />
            <asp:Parameter Name="SampleNumber" Type="String" />
            <asp:Parameter Name="LabID" Type="String" />
            <asp:Parameter Name="Batch" Type="String" />

            <asp:Parameter Name="LogDate" Type="DateTime" />
            <asp:Parameter Name="AnalyzeDate" Type="DateTime" />
            <asp:Parameter Name="Comment" Type="String" />
            <asp:Parameter Name="DateCreated" Type="DateTime" />
            <asp:Parameter Name="UserCreated" Type="String" />
            <asp:Parameter Name="DateLastModified" Type="DateTime" />
            <asp:Parameter Name="UserLastModified" Type="String" />
            <asp:Parameter Name="TotalPhos" Type="Boolean" />
            <asp:Parameter Name="OrthoPhos" Type="Boolean" />
            <asp:Parameter Name="TotalNitro" Type="Boolean" />
            <asp:Parameter Name="NitrateNitrite" Type="Boolean" />
            <asp:Parameter Name="Ammonia" Type="Boolean" />
            <asp:Parameter Name="DOC" Type="Boolean" />
            <asp:Parameter Name="Chloride" Type="Boolean" />
            <asp:Parameter Name="Sulfate" Type="Boolean" />
            <asp:Parameter Name="TSS" Type="Boolean" />
            <asp:Parameter Name="ChlorA" Type="Boolean" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="SampleID" Type="Int32" />
            <asp:Parameter Name="SampleNumber" Type="String" />
            <asp:Parameter Name="LabID" Type="String" />
            <asp:Parameter Name="Batch" Type="String" />

            <asp:Parameter Name="LogDate" Type="DateTime" />
            <asp:Parameter Name="AnalyzeDate" Type="DateTime" />
            <asp:Parameter Name="Comment" Type="String" />
            <asp:Parameter Name="DateCreated" Type="DateTime" />
            <asp:Parameter Name="UserCreated" Type="String" />
            <asp:Parameter Name="DateLastModified" Type="DateTime" />
            <asp:Parameter Name="UserLastModified" Type="String" />
            <asp:Parameter Name="TotalPhos" Type="Boolean" />
            <asp:Parameter Name="OrthoPhos" Type="Boolean" />
            <asp:Parameter Name="TotalNitro" Type="Boolean" />
            <asp:Parameter Name="NitrateNitrite" Type="Boolean" />
            <asp:Parameter Name="Ammonia" Type="Boolean" />
            <asp:Parameter Name="DOC" Type="Boolean" />
            <asp:Parameter Name="Chloride" Type="Boolean" />
            <asp:Parameter Name="Sulfate" Type="Boolean" />
            <asp:Parameter Name="TSS" Type="Boolean" />
            <asp:Parameter Name="ChlorA" Type="Boolean" />
            <asp:Parameter Name="ID" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>


</asp:Content>
