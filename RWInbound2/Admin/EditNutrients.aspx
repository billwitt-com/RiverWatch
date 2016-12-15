<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditNutrients.aspx.cs" Inherits="RWInbound2.Admin.EditNutrients" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <asp:Label ID="Label1" runat="server" CssClass="PageLabel" Text="Edit Nutrients"></asp:Label>
    <br />
    <asp:Label ID="Label2" runat="server" Text="Enter Barcode:  "></asp:Label>
    <asp:TextBox ID="tbSelectBarcode" runat="server"></asp:TextBox>
                    <ajaxToolkit:AutoCompleteExtender ID="tbSelectBarcode_AutoCompleteExtender" runat="server" TargetControlID="tbSelectBarcode"
                            ServiceMethod="SearchBarcodes" CompletionSetCount="2" MinimumPrefixLength="2">
                </ajaxToolkit:AutoCompleteExtender>

    <asp:Button ID="btnBarcodeSelected" runat="server" Text="Select" OnClick="btnBarcodeSelected_Click" />
            <asp:Label ID="Label3" runat="server"  Font-Size="Large" ForeColor="Red" Text="_____ CAUTION !! _____" Width="231px"></asp:Label>
        <asp:Button ID="btnDeleteAll" runat="server" CssClass="adminButton"  Text="Delete this Barcode!!" Font-Size="Large" ForeColor="#FF3300" OnClick="btnDeleteAll_Click" />

            <br />

        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
        <br />
        <br /> 
        <asp:FormView ID="FormView1" runat="server" OnPageIndexChanging="FormView1_PageIndexChanging" AllowPaging="true" PagerSettings-Mode="NumericFirstLast" PagerSettings-Position="TopAndBottom" OnDataBound="FormView1_DataBound" DataKeyNames="inbLachatID" DataSourceID="SqlDataSource1">
            <EditItemTemplate>
                Batch:
                <asp:TextBox ID="BatchTextBox" runat="server" Text='<%# Bind("Batch") %>' />
                <br />
                SampleType:
                <asp:TextBox ID="SampleTypeTextBox" runat="server" Text='<%# Bind("SampleType") %>' />
                <br />
                SampleDescription:
                <asp:TextBox ID="SampleDescriptionTextBox" runat="server" Text='<%# Bind("SampleDescription") %>' />
                <br />
                CODE:
                <asp:TextBox ID="CODETextBox" runat="server" Text='<%# Bind("CODE") %>' />
                <br />
                Parameter:
                <asp:TextBox ID="ParameterTextBox" runat="server" Text='<%# Bind("Parameter") %>' />
                <br />
                Result:
                <asp:TextBox ID="ResultTextBox" runat="server" Text='<%# Bind("Result") %>' />
                <br />
                Unit:
                <asp:TextBox ID="UnitTextBox" runat="server" Text='<%# Bind("Unit") %>' />
                <br />
                AnalDate:
                <asp:TextBox ID="AnalDateTextBox" runat="server" Text='<%# Bind("AnalDate") %>' />
                <br />
                CONHI:
                <asp:CheckBox ID="CONHICheckBox" runat="server" Checked='<%# Bind("CONHI") %>' />
                <br />
                RPDDivPctREC:
                <asp:TextBox ID="RPDDivPctRECTextBox" runat="server" Text='<%# Bind("RPDDivPctREC") %>' />
                <br />
                CHANGED:
                <asp:CheckBox ID="CHANGEDCheckBox" runat="server" Checked='<%# Bind("CHANGED") %>' />
                <br />
                NumChanged:
                <asp:TextBox ID="NumChangedTextBox" runat="server" Text='<%# Bind("NumChanged") %>' />
                <br />
                Comment:
                <asp:TextBox ID="CommentTextBox" runat="server" Text='<%# Bind("Comment") %>' />
                <br />
                PassValStep:
                <asp:TextBox ID="PassValStepTextBox" runat="server" Text='<%# Bind("PassValStep") %>' />
                <br />
                tblSampleID:
                <asp:TextBox ID="tblSampleIDTextBox" runat="server" Text='<%# Bind("tblSampleID") %>' />
                <br />
                Valid:
                <asp:CheckBox ID="ValidCheckBox" runat="server" Checked='<%# Bind("Valid") %>' />
                <br />
                Validated:
                <asp:CheckBox ID="ValidatedCheckBox" runat="server" Checked='<%# Bind("Validated") %>' />
                <br />
                BlkDup:
                <asp:CheckBox ID="BlkDupCheckBox" runat="server" Checked='<%# Bind("BlkDup") %>' />
                <br />
                Failed:
                <asp:CheckBox ID="FailedCheckBox" runat="server" Checked='<%# Bind("Failed") %>' />
                <br />
                 inbLachatID:
                <asp:Label ID="inbLachatIDLabel1" runat="server" Text='<%# Eval("inbLachatID") %>' />
                <br />
                <asp:Button ID="UpdateButton" CssClass="adminButton"  runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
                &nbsp;<asp:Button ID="UpdateCancelButton" CssClass="adminButton"  runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
            </EditItemTemplate>
          
            <ItemTemplate>
               inbLachatID:
                <asp:Label ID="inbLachatIDLabel" runat="server" Text='<%# Eval("inbLachatID") %>' />
                <br />
                Batch:
                <asp:Label ID="BatchLabel" runat="server" Text='<%# Eval("Batch") %>' />
                <br />
                SampleType:
                <asp:Label ID="SampleTypeLabel" runat="server" Text='<%# Eval("SampleType") %>' />
                <br />
                SampleDescription:
                <asp:Label ID="SampleDescriptionLabel" runat="server" Text='<%# Eval("SampleDescription") %>' />
                <br />
                CODE:
                <asp:Label ID="CODELabel" runat="server" Text='<%# Eval("CODE") %>' />
                <br />
                Parameter:
                <asp:Label ID="ParameterLabel" runat="server" Text='<%# Eval("Parameter") %>' />
                <br />
                Result:
                <asp:Label ID="ResultLabel" runat="server" Text='<%# Eval("Result") %>' />
                <br />
                Unit:
                <asp:Label ID="UnitLabel" runat="server" Text='<%# Eval("Unit") %>' />
                <br />
                AnalDate:
                <asp:Label ID="AnalDateLabel" runat="server" Text='<%# Eval("AnalDate") %>' />
                <br />
                CONHI:
                <asp:CheckBox ID="CONHICheckBox" runat="server" Checked='<%# Eval("CONHI") %>'  />
                <br />
                RPDDivPctREC:
                <asp:Label ID="RPDDivPctRECLabel" runat="server" Text='<%# Eval("RPDDivPctREC") %>' />
                <br />
                CHANGED:
                <asp:CheckBox ID="CHANGEDCheckBox" runat="server" Checked='<%# Eval("CHANGED") %>'  />
                <br />
                NumChanged:
                <asp:Label ID="NumChangedLabel" runat="server" Text='<%# Eval("NumChanged") %>' />
                <br />
                Comment:
                <asp:Label ID="CommentLabel" runat="server" Text='<%# Eval("Comment") %>' />
                <br />
                PassValStep:
                <asp:Label ID="PassValStepLabel" runat="server" Text='<%# Eval("PassValStep") %>' />
                <br />
                tblSampleID:
                <asp:Label ID="tblSampleIDLabel" runat="server" Text='<%# Eval("tblSampleID") %>' />
                <br />
                Valid:
                <asp:CheckBox ID="ValidCheckBox" runat="server" Checked='<%# Eval("Valid") %>' />
                <br />
                Validated:
                <asp:CheckBox ID="ValidatedCheckBox" runat="server" Checked='<%# Eval("Validated") %>'  />
                <br />
                BlkDup:
                <asp:CheckBox ID="BlkDupCheckBox" runat="server" Checked='<%# Eval("BlkDup") %>' />
                <br />
                Failed:
                <asp:CheckBox ID="FailedCheckBox" runat="server" Checked='<%# Eval("Failed") %>'  />
                <br />
                <asp:Button ID="EditButton" runat="server" CssClass="adminButton"  CausesValidation="False" CommandName="Edit" Text="Edit" />
                &nbsp;<asp:Button ID="DeleteButton" runat="server" CssClass="adminButton"  CausesValidation="False" CommandName="Delete" Text="Delete" />

            </ItemTemplate>
        </asp:FormView>



        <asp:SqlDataSource ID="SqlDataSource1" runat="server"  OnUpdated="SqlDataSource1_Updated"  OnDeleted="SqlDataSource1_Deleted" ConnectionString="<%$ ConnectionStrings:RiverWatchDEV %>" 
            DeleteCommand="UPDATE [Lachat] SET [Valid] = 0  WHERE [inbLachatID] = @inbLachatID" 
            InsertCommand="INSERT INTO [Lachat] ([Batch], [SampleType], [SampleDescription], [CODE], [Parameter], [Result], [Unit], [AnalDate], [CONHI], [RPDDivPctREC], [CHANGED], [NumChanged], [Comment], [PassValStep], [tblSampleID], [Valid], [Validated], [BlkDup], [Failed]) VALUES (@Batch, @SampleType, @SampleDescription, @CODE, @Parameter, @Result, @Unit, @AnalDate, @CONHI, @RPDDivPctREC, @CHANGED, @NumChanged, @Comment, @PassValStep, @tblSampleID, @Valid, @Validated, @BlkDup, @Failed)" 

            UpdateCommand="UPDATE [Lachat] SET [Batch] = @Batch, [SampleType] = @SampleType, [SampleDescription] = @SampleDescription, [CODE] = @CODE, [Parameter] = @Parameter, [Result] = @Result, [Unit] = @Unit, [AnalDate] = @AnalDate, [CONHI] = @CONHI, [RPDDivPctREC] = @RPDDivPctREC, [CHANGED] = @CHANGED, [NumChanged] = @NumChanged, [Comment] = @Comment, [PassValStep] = @PassValStep, [tblSampleID] = @tblSampleID, [Valid] = @Valid, [Validated] = @Validated, [BlkDup] = @BlkDup, [Failed] = @Failed WHERE [inbLachatID] = @inbLachatID">
            <DeleteParameters>
                <asp:Parameter Name="inbLachatID" Type="Int32" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="Batch" Type="String" />
                <asp:Parameter Name="SampleType" Type="String" />
                <asp:Parameter Name="SampleDescription" Type="String" />
                <asp:Parameter Name="CODE" Type="String" />
                <asp:Parameter Name="Parameter" Type="String" />
                <asp:Parameter Name="Result" Type="Decimal" />
                <asp:Parameter Name="Unit" Type="String" />
                <asp:Parameter Name="AnalDate" Type="DateTime" />
                <asp:Parameter Name="CONHI" Type="Boolean" />
                <asp:Parameter Name="RPDDivPctREC" Type="Decimal" />
                <asp:Parameter Name="CHANGED" Type="Boolean" />
                <asp:Parameter Name="NumChanged" Type="Int32" />
                <asp:Parameter Name="Comment" Type="String" />
                <asp:Parameter Name="PassValStep" Type="Decimal" />
                <asp:Parameter Name="tblSampleID" Type="Int32" />
                <asp:Parameter Name="Valid" Type="Boolean" />
                <asp:Parameter Name="Validated" Type="Boolean" />
                <asp:Parameter Name="BlkDup" Type="Boolean" />
                <asp:Parameter Name="Failed" Type="Boolean" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="Batch" Type="String" />
                <asp:Parameter Name="SampleType" Type="String" />
                <asp:Parameter Name="SampleDescription" Type="String" />
                <asp:Parameter Name="CODE" Type="String" />
                <asp:Parameter Name="Parameter" Type="String" />
                <asp:Parameter Name="Result" Type="Decimal" />
                <asp:Parameter Name="Unit" Type="String" />
                <asp:Parameter Name="AnalDate" Type="DateTime" />
                <asp:Parameter Name="CONHI" Type="Boolean" />
                <asp:Parameter Name="RPDDivPctREC" Type="Decimal" />
                <asp:Parameter Name="CHANGED" Type="Boolean" />
                <asp:Parameter Name="NumChanged" Type="Int32" />
                <asp:Parameter Name="Comment" Type="String" />
                <asp:Parameter Name="PassValStep" Type="Decimal" />
                <asp:Parameter Name="tblSampleID" Type="Int32" />
                <asp:Parameter Name="Valid" Type="Boolean" />
                <asp:Parameter Name="Validated" Type="Boolean" />
                <asp:Parameter Name="BlkDup" Type="Boolean" />
                <asp:Parameter Name="Failed" Type="Boolean" />
                <asp:Parameter Name="inbLachatID" Type="Int32" />
            </UpdateParameters>
        </asp:SqlDataSource>
        <br />
</asp:Content>
