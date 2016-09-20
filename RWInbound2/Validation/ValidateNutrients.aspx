<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ValidateNutrients.aspx.cs" Inherits="RWInbound2.Validation.ValidateNutrients" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label1" CssClass="PageLabel" runat="server" Text="Validate Nutrients"></asp:Label>

    <br />
    <br />
    <asp:Label ID="Label2" runat="server" Text="Select by Batch: "></asp:Label>
    <asp:TextBox ID="tbBatchNumber" runat="server"></asp:TextBox>
    <asp:Button ID="btnSelectBatch" runat="server" Text="Select" OnClick="btnSelectBatch_Click" />
&nbsp;
    <br />
    <asp:Label ID="lblNumberLeft" runat="server" Text="Label"></asp:Label>
   
    <br />
   
    <br />

    <asp:FormView ID="FormView1"  PagerSettings-Position="TopAndBottom" runat="server" OnDataBound="FormView1_DataBound" AllowPaging="True" DefaultMode="Edit" DataKeyNames="ID" DataSourceID="SqlDataSource1" Width="389px">
        <EditItemTemplate>



            <table style="width: 100%">
                <tr>
                    <td>            BARCODE:</td>
                    <td> <asp:TextBox ID="BARCODETextBox"  runat="server" ReadOnly="true" Text='<%# Bind("BARCODE") %>' /></td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>  Batch:</td>
                    <td>          
            <asp:TextBox ID="BatchTextBox" runat="server" Text='<%# Bind("Batch") %>' /></td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>TypeCode:</td>
                    <td>            
            <asp:TextBox ID="TypeCodeTextBox" OnTextChanged="TextBox_TextChanged"  AutoPostBack="true" runat="server" Text='<%# Bind("TypeCode") %>' /></td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td> SampleNumber:</td>
                    <td>           
            <asp:TextBox ID="SampleNumberTextBox" ReadOnly="true" runat="server" Text='<%# Bind("SampleNumber") %>' /></td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>

                <tr>
                    <td>TotalPhos:</td>
                    <td>
                        <asp:TextBox ID="TotalPhosTextBox" OnTextChanged="TextBox_TextChanged" AutoPostBack="true" runat="server" Text='<%# Bind("TotalPhos") %>' />


                        <br />
                    </td>
                    <td>CH:
            <asp:CheckBox ID="TotalPhos_CHCheckBox" runat="server" Checked='<%# Bind("TotalPhos_CH") %>' /></td>
                </tr>
                <tr>
                    <td>OrthoPhos:</td>
                    <td>
                        <asp:TextBox ID="OrthoPhosTextBox" OnTextChanged="TextBox_TextChanged" AutoPostBack="true" runat="server" Text='<%# Bind("OrthoPhos") %>' />
                    </td>
                    <td>CH:
                <asp:CheckBox ID="OrthoPhos_CHCheckBox" runat="server" Checked='<%# Bind("OrthoPhos_CH") %>' />
                    </td>
                </tr>
                <tr>
                    <td>TotalNitro:</td>
                    <td>
                        <asp:TextBox ID="TotalNitroTextBox" OnTextChanged="TextBox_TextChanged" AutoPostBack="true" runat="server" Text='<%# Bind("TotalNitro") %>' />
                    </td>
                    <td>CH:
            <asp:CheckBox ID="TotalNitro_CHCheckBox" runat="server" Checked='<%# Bind("TotalNitro_CH") %>' /></td>
                </tr>
                <tr>
                    <td>NitrateNitrite:</td>
                    <td>
                        <asp:TextBox ID="NitrateNitriteTextBox" OnTextChanged="TextBox_TextChanged" AutoPostBack="true" runat="server" Text='<%# Bind("NitrateNitrite") %>' />
                    </td>
                    <td>CH:
            <asp:CheckBox ID="NitrateNitrite_CHCheckBox" runat="server" Checked='<%# Bind("NitrateNitrite_CH") %>' /></td>
                </tr>
                <tr>
                    <td>Ammonia:</td>
                    <td>
                        <asp:TextBox ID="AmmoniaTextBox" runat="server" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" Text='<%# Bind("Ammonia") %>' />

                    </td>
                    <td>CH:
            <asp:CheckBox ID="Ammonia_CHCheckBox" runat="server" Checked='<%# Bind("Ammonia_CH") %>' />
                    </td>
                </tr>
        <tr>
            <td> DOC:</td>
            <td>           
            <asp:TextBox ID="DOCTextBox" OnTextChanged="TextBox_TextChanged" AutoPostBack="true" runat="server" Text='<%# Bind("DOC") %>' />
</td>
            <td>
            CH:
            <asp:CheckBox ID="DOC_CHCheckBox" runat="server" Checked='<%# Bind("DOC_CH") %>' /></td>
        </tr>
        <tr>
            <td>Chloride:</td>
            <td>            
            <asp:TextBox ID="ChlorideTextBox" OnTextChanged="TextBox_TextChanged" AutoPostBack="true" runat="server" Text='<%# Bind("Chloride") %>' />

</td>
            <td>            CH:
            <asp:CheckBox ID="Chloride_CHCheckBox" runat="server" Checked='<%# Bind("Chloride_CH") %>' /></td>
        </tr>
        <tr>
            <td> Sulfate:</td>
            <td>           
            <asp:TextBox ID="SulfateTextBox" OnTextChanged="TextBox_TextChanged" AutoPostBack="true"  runat="server" Text='<%# Bind("Sulfate") %>' />

</td>
            <td>            CH:
            <asp:CheckBox ID="Sulfate_CHCheckBox" runat="server" Checked='<%# Bind("Sulfate_CH") %>' /></td>
        </tr>
        <tr>
            <td> TSS:</td>
            <td>           
            <asp:TextBox ID="TSSTextBox" OnTextChanged="TextBox_TextChanged" AutoPostBack="true" runat="server" Text='<%# Bind("TSS") %>' />

</td>
            <td>            CH:
            <asp:CheckBox ID="TSS_CHCheckBox" runat="server" Checked='<%# Bind("TSS_CH") %>' /></td>
        </tr>
        <tr>
            <td>ChlorA:</td>
            <td>
            
            <asp:TextBox ID="ChlorATextBox" OnTextChanged="TextBox_TextChanged" AutoPostBack="true" runat="server" Text='<%# Bind("ChlorA") %>' />
           </td>
            <td> CH:
            <asp:CheckBox ID="ChlorA_CHCheckBox" runat="server" Checked='<%# Bind("ChlorA_CH") %>' /></td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>


            <asp:Button ID="UpdateButton" runat="server" OnClick="UpdateButton_Click" CausesValidation="True" CommandName="Update" Text="Update" />
            &nbsp;<asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
             &nbsp;<asp:Button ID="btnBad" runat="server" CausesValidation="False"  OnClick="btnBad_Click" Text="BAD" />
        </EditItemTemplate>
       
    </asp:FormView>
   

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" OnUpdating="SqlDataSource1_Updating" ConnectionString="<%$ ConnectionStrings:RiverwatchDEV %>" 
        DeleteCommand="DELETE FROM [NutrientData] WHERE [ID] = @ID" 
        InsertCommand="INSERT INTO [NutrientData] ([BARCODE], [Batch], [TypeCode], [SampleNumber], [TotalPhos], [TotalPhos_CH], [OrthoPhos], [OrthoPhos_CH], [TotalNitro], [TotalNitro_CH], [NitrateNitrite], [NitrateNitrite_CH], [Ammonia], [Ammonia_CH], [DOC], [DOC_CH], [Chloride], [Chloride_CH], [Sulfate], [Sulfate_CH], [TSS], [TSS_CH], [ChlorA], [ChlorA_CH], [Valid], [Validated], [DateCreated], [CreatedBy]) VALUES (@BARCODE, @Batch, @TypeCode, @SampleNumber, @TotalPhos, @TotalPhos_CH, @OrthoPhos, @OrthoPhos_CH, @TotalNitro, @TotalNitro_CH, @NitrateNitrite, @NitrateNitrite_CH, @Ammonia, @Ammonia_CH, @DOC, @DOC_CH, @Chloride, @Chloride_CH, @Sulfate, @Sulfate_CH, @TSS, @TSS_CH, @ChlorA, @ChlorA_CH, @Valid, @Validated, @DateCreated, @CreatedBy)" 
        SelectCommand="SELECT * FROM [NutrientData]  where valid = 1 and validated = 0 and SampleNumber is not null and typecode LIKE '05'" 
        UpdateCommand="UPDATE [NutrientData] SET [BARCODE] = @BARCODE, [Batch] = @Batch, [TypeCode] = @TypeCode, [SampleNumber] = @SampleNumber, [TotalPhos] = @TotalPhos, [TotalPhos_CH] = @TotalPhos_CH, [OrthoPhos] = @OrthoPhos, [OrthoPhos_CH] = @OrthoPhos_CH, [TotalNitro] = @TotalNitro, [TotalNitro_CH] = @TotalNitro_CH, [NitrateNitrite] = @NitrateNitrite, [NitrateNitrite_CH] = @NitrateNitrite_CH, [Ammonia] = @Ammonia, [Ammonia_CH] = @Ammonia_CH, [DOC] = @DOC, [DOC_CH] = @DOC_CH, [Chloride] = @Chloride, [Chloride_CH] = @Chloride_CH, [Sulfate] = @Sulfate, [Sulfate_CH] = @Sulfate_CH, [TSS] = @TSS, [TSS_CH] = @TSS_CH, [ChlorA] = @ChlorA, [ChlorA_CH] = @ChlorA_CH, [Valid] = @Valid, [Validated] = @Validated, [DateCreated] = @DateCreated, [CreatedBy] = @CreatedBy WHERE [ID] = @ID">
        <DeleteParameters>
            <asp:Parameter Name="ID" Type="Int32" />
        </DeleteParameters>
       
        <UpdateParameters>
            <asp:Parameter Name="BARCODE" Type="String" />
            <asp:Parameter Name="Batch" Type="String" />
            <asp:Parameter Name="TypeCode" Type="String" />
            <asp:Parameter Name="SampleNumber" Type="String" />
            <asp:Parameter Name="TotalPhos" Type="Decimal" />
            <asp:Parameter Name="TotalPhos_CH" Type="Boolean" />
            <asp:Parameter Name="OrthoPhos" Type="Decimal" />
            <asp:Parameter Name="OrthoPhos_CH" Type="Boolean" />
            <asp:Parameter Name="TotalNitro" Type="Decimal" />
            <asp:Parameter Name="TotalNitro_CH" Type="Boolean" />
            <asp:Parameter Name="NitrateNitrite" Type="Decimal" />
            <asp:Parameter Name="NitrateNitrite_CH" Type="Boolean" />
            <asp:Parameter Name="Ammonia" Type="Decimal" />
            <asp:Parameter Name="Ammonia_CH" Type="Boolean" />
            <asp:Parameter Name="DOC" Type="Decimal" />
            <asp:Parameter Name="DOC_CH" Type="Boolean" />
            <asp:Parameter Name="Chloride" Type="Decimal" />
            <asp:Parameter Name="Chloride_CH" Type="Boolean" />
            <asp:Parameter Name="Sulfate" Type="Decimal" />
            <asp:Parameter Name="Sulfate_CH" Type="Boolean" />
            <asp:Parameter Name="TSS" Type="Decimal" />
            <asp:Parameter Name="TSS_CH" Type="Boolean" />
            <asp:Parameter Name="ChlorA" Type="Decimal" />
            <asp:Parameter Name="ChlorA_CH" Type="Boolean" />
            <asp:Parameter Name="Valid" Type="Boolean" />
            <asp:Parameter Name="Validated" Type="Boolean" />
            <asp:Parameter Name="DateCreated" Type="DateTime" />
            <asp:Parameter Name="CreatedBy" Type="String" />
            <asp:Parameter Name="ID" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
   

</asp:Content>
