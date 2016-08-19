<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ValidateField.aspx.cs" Inherits="RWInbound2.Validation.ValidateField" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   <asp:Label ID="Label1" runat="server" Text=" Validate Field Data"></asp:Label>

&nbsp;
    <table style="width: 100%">
            <tr>
                <td style="height: 20px;"></td>
                <td style="width: 100px; height: 20px">Organization:</td>
                <td style="height: 20px; width: 249px;">
                    <asp:TextBox ID="tbOrgName" Width="211px" runat="server"></asp:TextBox>
                </td>
                 <td style="width: 58px">River:</td>
                <td>
                    <asp:TextBox Width="211px" ID="tbRiver" runat="server"></asp:TextBox>
                </td>
            </tr>
           
            <tr>
                <td style="height: 20px;"></td>

                                <td style="width: 100px">Station Name: </td>
                <td>
                    <asp:TextBox Style="width: 211px" ID="tbStationName" runat="server"></asp:TextBox>
                </td>
                <td style="width: 58px"></td>
                <td></td>
            </tr>
 

        </table>
    <asp:FormView ID="FormView1" runat="server" DefaultMode="Edit" AllowPaging="true" DataKeyNames="inbSampleID" DataSourceID="SqlDataSource1" 
        OnPageIndexChanged="FormView1_PageIndexChanged" OnDataBound="FormView1_DataBound">
        <EditItemTemplate>
        <table>
            <tr>
                StationNum:
            <asp:TextBox ID="StationNumTextBox" runat="server" Text='<%# Bind("StationNum") %>' />
            <br />
            SampleID:
            <asp:TextBox ID="SampleIDTextBox" runat="server" Text='<%# Bind("SampleID") %>' />
            <br />
<%--             txtSampleID:
            <asp:TextBox ID="txtSampleIDTextBox" runat="server" Text='<%# Bind("txtSampleID") %>' />
            <br />--%>
            
            KitNum:
            <asp:TextBox ID="KitNumTextBox" runat="server" Text='<%# Bind("KitNum") %>' />
    </tr>
            </table>

                <table>
                    <tr>
                        <td>Sample Date:
                        </td>
                        <td>
                            <asp:TextBox ID="DateTextBox" runat="server" Text='<%# Bind("Date", "{0:d}") %>' />
                         </td>
                    </tr>

                    <tr>
                        <td>Sample Time (24 Hr HH:MM):
                        </td>
                        <td>
                            <asp:TextBox ID="TimeTextBox" runat="server" Text='<%# Bind("Time") %>' />
                             <ajaxToolkit:MaskedEditExtender ID="txtTimeCollected_MaskedEditExtender" runat="server" BehaviorID="txtTimeCollected_MaskedEditExtender" 
                                 Century="2000" 
                                 TargetControlID="TimeTextBox" MaskType="Time" Mask="99:99" AcceptAMPM="false" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TimeTextBox" ErrorMessage="Time is required"></asp:RequiredFieldValidator>
                        
                           </td>
                    </tr>

                    <tr>
                        <td>USGSFlow (CFSecond):               

                        </td>
                        <td>
                            <asp:TextBox ID="USGSFlowTextBox" runat="server" Text='<%# Bind("USGSFlow") %>' />
                    </tr>
                    <tr>
                        <td>Temp (In CENTEGRADE):
                        </td>
                        <td>
                            <asp:TextBox ID="TempCTextBox" runat="server" Text='<%# Bind("TempC") %>' />
                            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="TempCTextBox" MaximumValue="25" MinimumValue="-10"
                                ErrorMessage="CELCIUS - Must be between -10 and 25" Display="Dynamic" Type="Double"
                                ForeColor="Red" SetFocusOnError="true"></asp:RangeValidator>
                        </td>
                        </td>
                    </tr>
                    <tr>
                        <td>PH:
                        </td>
                        <td>
                            <asp:TextBox ID="PHTextBox" runat="server" Text='<%# Bind("PH") %>' />
                            <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="PHTextBox" MaximumValue="14" MinimumValue="0"
                                ErrorMessage="Must be between 0 and 14" Display="Dynamic" Type="Double"
                                ForeColor="Red" SetFocusOnError="true"></asp:RangeValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>Alkalinity mg/L:
                        </td>
                        <td>Phenol = 
            <asp:TextBox ID="PhenAlkTextBox" runat="server" Text='<%# Bind("PhenAlk") %>' />
                            Total = 
                        <asp:TextBox ID="TotalAlkTextBox" runat="server" Text='<%# Bind("TotalAlk") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td>TotalHard mg/L:
                        </td>
                        <td>
                            <asp:TextBox ID="TotalHardTextBox" runat="server" Text='<%# Bind("TotalHard") %>' />
                        </td>
                    </tr>



                    <tr>
                        <td>Dissolved Oxygen:
                        </td>
                        <td>mg/L = 
            <asp:TextBox ID="DOTextBox" runat="server" Text='<%# Bind("DO") %>' />
                            % Saturation = 
                                    <asp:TextBox ID="DOsatTextBox" runat="server" Text='<%# Bind("DOsat") %>' />
                        </td>
                    </tr>

                </table>


                <%-- Tag:
            <asp:TextBox ID="TagTextBox" runat="server" Text='<%# Bind("Tag") %>' />
            <br />
            Chk:
            <asp:TextBox ID="ChkTextBox" runat="server" Text='<%# Bind("Chk") %>' />
            <br />
            EntryType:
            <asp:TextBox ID="EntryTypeTextBox" runat="server" Text='<%# Bind("EntryType") %>' />
            <br />
            EntryStaff:
            <asp:TextBox ID="EntryStaffTextBox" runat="server" Text='<%# Bind("EntryStaff") %>' />
            <br />--%>

                <asp:Panel ID="Panel2" runat="server" Height="40px">
                    <asp:Label ID="Label1" runat="server" Text="Samples Collected" Height="34px"></asp:Label>
                </asp:Panel>
                <table style="width: 100%">
                    <tr>
                        <td></td>
                        <td>Metals</td>
                        <td>Nutrients</td>
                        <td>Invertebrates</td>
                    </tr>
                    <tr>
                        <%-- row 2 --%>
                        <td>&nbsp;</td>
                        <td>
                            <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("MetalsNormal") %>' />
                            Normal
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox2" runat="server" Checked='<%# Bind("NP") %>' />
                            NP
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox3" runat="server" Checked='<%# Bind("Bugs") %>' />
                            Bugs
                        </td>
                    </tr>
                    <tr>
                        <%-- row 3 --%>
                        <td>&nbsp;</td>
                        <td>
                            <asp:CheckBox ID="CheckBox4" runat="server" Checked='<%# Bind("MetalsBlnk") %>' />
                            Blank
                        </td>

                        <td>
                            <asp:CheckBox ID="CheckBox5" runat="server" Checked='<%# Bind("CS") %>' />
                            Standard CS
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBox6" runat="server" Checked='<%# Bind("BugsQA") %>' />
                            BugsQA
                        </td>
                    </tr>
                    <tr>
                        <%-- row 4 --%>
                        <td>&nbsp;</td>
                        <td>
                            <asp:CheckBox ID="CheckBox7" runat="server" Checked='<%# Bind("MetalsDupe") %>' />
                            Duplicate
                        </td>

                        <td>
                            <asp:CheckBox ID="CheckBox8" runat="server" Checked='<%# Bind("TSS") %>' />
                            TSS
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <%-- row 5 --%>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:CheckBox ID="CheckBox9" runat="server" Checked='<%# Bind("NPDupe") %>' />
                            Duplicate NP
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <%-- row 6 --%>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:CheckBox ID="CheckBox10" runat="server" Checked='<%# Bind("CSDupe") %>' />
                            Duplicate CS
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <%-- row 7 --%>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:CheckBox ID="CheckBox11" runat="server" Checked='<%# Bind("TSSDupe") %>' />
                            Duplicate TSS
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <%-- row 8 --%>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>


                <%-- FieldValid:
            <asp:CheckBox ID="FieldValidCheckBox" runat="server" Checked='<%# Bind("FieldValid") %>' />
            <br />
            MetalsStat:
            <asp:TextBox ID="MetalsStatTextBox" runat="server" Text='<%# Bind("MetalsStat") %>' />
            <br />
            FinalCheck:
            <asp:CheckBox ID="FinalCheckCheckBox" runat="server" Checked='<%# Bind("FinalCheck") %>' />
            <br />
            Method:
            <asp:TextBox ID="MethodTextBox" runat="server" Text='<%# Bind("Method") %>' />
            <br />--%>
            Comments:
            <asp:TextBox ID="CommentsTextBox" runat="server" Text='<%# Bind("Comments") %>' TextMode="MultiLine" Width="360" Height="44" />

                <%-- DateReceived:
            <asp:TextBox ID="DateReceivedTextBox" runat="server" Text='<%# Bind("DateReceived") %>' />
            <br />
            DataSheetIncluded:
            <asp:CheckBox ID="DataSheetIncludedCheckBox" runat="server" Checked='<%# Bind("DataSheetIncluded") %>' />
            <br />
            MissingDataSheetReqDate:
            <asp:TextBox ID="MissingDataSheetReqDateTextBox" runat="server" Text='<%# Bind("MissingDataSheetReqDate") %>' />
            <br />
            ChainOfCustody:
            <asp:CheckBox ID="ChainOfCustodyCheckBox" runat="server" Checked='<%# Bind("ChainOfCustody") %>' />
            <br />
            MissingDataSheetReceived:
            <asp:CheckBox ID="MissingDataSheetReceivedCheckBox" runat="server" Checked='<%# Bind("MissingDataSheetReceived") %>' />
            <br />
            PassValStep:
            <asp:TextBox ID="PassValStepTextBox" runat="server" Text='<%# Bind("PassValStep") %>' />
            <br />
            tblSampleID:
            <asp:TextBox ID="tblSampleIDTextBox" runat="server" Text='<%# Bind("tblSampleID") %>' />
            <br />--%>
                <table>
                    <tr>
                        <td style="width: 240px;"></td>
                        <td>
                            <asp:Button ID="UpdateButton" ClientIDMode="Static"  runat="server" CausesValidation="True" CommandName="Update" Text="Validate" />
                             <asp:Button ID="btnBAD" ClientIDMode="Static"  runat="server"  OnClick="btnBAD_Click" CausesValidation="True" Text="BAD" />

                        </td>

                    </tr>
                </table>

                &nbsp;
                <%--            <asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />--%>
  </EditItemTemplate>
      
    </asp:FormView>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RiverwatchDEV %>" 
        DeleteCommand="DELETE FROM [InboundSamples] WHERE [inbSampleID] = @inbSampleID" 
        InsertCommand="INSERT INTO [InboundSamples] ([StationNum], [SampleID], [txtSampleID], [KitNum], [Date], [Time], [USGSFlow], [PH], [TempC], [PhenAlk], [TotalAlk], [TotalHard], [DO], [DOsat], [Tag], [Chk], [EntryType], [EntryStaff], [MetalsNormal], [MetalsBlnk], [MetalsDupe], [Bugs], [BugsQA], [TSS], [CS], [NP], [TSSDupe], [CSDupe], [NPDupe], [FieldValid], [MetalsStat], [FinalCheck], [Method], [Comments], [DateReceived], [DataSheetIncluded], [MissingDataSheetReqDate], [ChainOfCustody], [MissingDataSheetReceived], [PassValStep], [tblSampleID]) VALUES (@StationNum, @SampleID, @txtSampleID, @KitNum, @Date, @Time, @USGSFlow, @PH, @TempC, @PhenAlk, @TotalAlk, @TotalHard, @DO, @DOsat, @Tag, @Chk, @EntryType, @EntryStaff, @MetalsNormal, @MetalsBlnk, @MetalsDupe, @Bugs, @BugsQA, @TSS, @CS, @NP, @TSSDupe, @CSDupe, @NPDupe, @FieldValid, @MetalsStat, @FinalCheck, @Method, @Comments, @DateReceived, @DataSheetIncluded, @MissingDataSheetReqDate, @ChainOfCustody, @MissingDataSheetReceived, @PassValStep, @tblSampleID)" 
        SelectCommand="SELECT * FROM [InboundSamples] order by date desc" 
        UpdateCommand="UPDATE [InboundSamples] SET [StationNum] = @StationNum, [SampleID] = @SampleID, [txtSampleID] = @txtSampleID, [KitNum] = @KitNum, [Date] = @Date, [Time] = @Time, [USGSFlow] = @USGSFlow, [PH] = @PH, [TempC] = @TempC, [PhenAlk] = @PhenAlk, [TotalAlk] = @TotalAlk, [TotalHard] = @TotalHard, [DO] = @DO, [DOsat] = @DOsat, [Tag] = @Tag, [Chk] = @Chk, [EntryType] = @EntryType, [EntryStaff] = @EntryStaff, [MetalsNormal] = @MetalsNormal, [MetalsBlnk] = @MetalsBlnk, [MetalsDupe] = @MetalsDupe, [Bugs] = @Bugs, [BugsQA] = @BugsQA, [TSS] = @TSS, [CS] = @CS, [NP] = @NP, [TSSDupe] = @TSSDupe, [CSDupe] = @CSDupe, [NPDupe] = @NPDupe, [FieldValid] = @FieldValid, [MetalsStat] = @MetalsStat, [FinalCheck] = @FinalCheck, [Method] = @Method, [Comments] = @Comments, [DateReceived] = @DateReceived, [DataSheetIncluded] = @DataSheetIncluded, [MissingDataSheetReqDate] = @MissingDataSheetReqDate, [ChainOfCustody] = @ChainOfCustody, [MissingDataSheetReceived] = @MissingDataSheetReceived, [PassValStep] = @PassValStep, [tblSampleID] = @tblSampleID WHERE [inbSampleID] = @inbSampleID" OnSelected="SqlDataSource1_Selected">
        <DeleteParameters>
            <asp:Parameter Name="inbSampleID" Type="Int32" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="StationNum" Type="Int32" />
            <asp:Parameter Name="SampleID" Type="Int64" />
            <asp:Parameter Name="txtSampleID" Type="String" />
            <asp:Parameter Name="KitNum" Type="Int32" />
            <asp:Parameter Name="Date" Type="DateTime" />
            <asp:Parameter Name="Time" Type="Int32" />
            <asp:Parameter Name="USGSFlow" Type="Decimal" />
            <asp:Parameter Name="PH" Type="Decimal" />
            <asp:Parameter Name="TempC" Type="Decimal" />
            <asp:Parameter Name="PhenAlk" Type="Decimal" />
            <asp:Parameter Name="TotalAlk" Type="Decimal" />
            <asp:Parameter Name="TotalHard" Type="Decimal" />
            <asp:Parameter Name="DO" Type="Decimal" />
            <asp:Parameter Name="DOsat" Type="Decimal" />
            <asp:Parameter Name="Tag" Type="String" />
            <asp:Parameter Name="Chk" Type="String" />
            <asp:Parameter Name="EntryType" Type="Int32" />
            <asp:Parameter Name="EntryStaff" Type="String" />
            <asp:Parameter Name="MetalsNormal" Type="Boolean" />
            <asp:Parameter Name="MetalsBlnk" Type="Boolean" />
            <asp:Parameter Name="MetalsDupe" Type="Boolean" />
            <asp:Parameter Name="Bugs" Type="Boolean" />
            <asp:Parameter Name="BugsQA" Type="Boolean" />
            <asp:Parameter Name="TSS" Type="Boolean" />
            <asp:Parameter Name="CS" Type="Boolean" />
            <asp:Parameter Name="NP" Type="Boolean" />
            <asp:Parameter Name="TSSDupe" Type="Boolean" />
            <asp:Parameter Name="CSDupe" Type="Boolean" />
            <asp:Parameter Name="NPDupe" Type="Boolean" />
            <asp:Parameter Name="FieldValid" Type="Boolean" />
            <asp:Parameter Name="MetalsStat" Type="Int32" />
            <asp:Parameter Name="FinalCheck" Type="Boolean" />
            <asp:Parameter Name="Method" Type="Int32" />
            <asp:Parameter Name="Comments" Type="String" />
            <asp:Parameter Name="DateReceived" Type="DateTime" />
            <asp:Parameter Name="DataSheetIncluded" Type="Boolean" />
            <asp:Parameter Name="MissingDataSheetReqDate" Type="DateTime" />
            <asp:Parameter Name="ChainOfCustody" Type="Boolean" />
            <asp:Parameter Name="MissingDataSheetReceived" Type="Boolean" />
            <asp:Parameter Name="PassValStep" Type="Decimal" />
            <asp:Parameter Name="tblSampleID" Type="Int32" />
            <asp:Parameter Name="inbSampleID" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>

</asp:Content>
