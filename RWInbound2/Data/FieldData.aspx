<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FieldData.aspx.cs" Inherits="RWInbound2.Data.FieldData" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <p style="font-size: medium; text-align: center;">
        <strong>Colorado Riverwatch Volunteer Data Entry Form</strong>
    </p>
    <asp:Panel ID="Panel3" runat="server">
        <Table style="border-collapse: collapse; border-spacing: 0px; border-width: 0px; padding: 0px; margin: 0px; border-style: none; table-layout: fixed;">
            <tr>
                <td style="width: 192px;">Station Number: 
                 <asp:TextBox ID="tbSite" runat="server" Height="20px" Width="46px" BorderColor="#66CCFF" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>

                </td>
                <td style="width: 171px">Kit Number: 
                 <asp:TextBox ID="tbKitNumber" runat="server" BorderColor="#66CCFF" BorderStyle="Solid" BorderWidth="1px" Height="20px" Width="46px"></asp:TextBox>
                </td>
                <td style="width: 300px"><span style="font-size: large">OR </span>&nbsp;Org: 
                     <asp:TextBox ID="tbOrg" Height="20px" runat="server" Width="201px"></asp:TextBox>
                </td>
                <td style="width: 116px">

                    &nbsp;</td>
            </tr>
             </Table>
            
            
            <Table style="height: 55px" >
            <tr>
                <td style="width: 209px;">Date Collected:
                    <asp:TextBox ID="tbDateCollected" runat="server" Height="20px"></asp:TextBox>
                       <ajaxToolkit:CalendarExtender ID="txtDateCollected_CalendarExtender" runat="server" BehaviorID="txtDateCollected_CalendarExtender" TargetControlID="tbDateCollected"></ajaxToolkit:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbDateCollected" ErrorMessage="Date is required"></asp:RequiredFieldValidator>
                        
                </td>
                <td style="width: 210px">Time Collected:
                    <asp:TextBox ID="tbTimeCollected" runat="server" Height="20px"></asp:TextBox>
                     <ajaxToolkit:MaskedEditExtender ID="txtTimeCollected_MaskedEditExtender" runat="server" BehaviorID="txtTimeCollected_MaskedEditExtender" Century="2000"
                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                CultureThousandsPlaceholder="" CultureTimePlaceholder="" TargetControlID="tbTimeCollected" MaskType="Time" Mask="99:99" AcceptAMPM="false" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbTimeCollected" ErrorMessage="Time is required"></asp:RequiredFieldValidator>
                        
                </td>
                <td style="width: 300px">
                    <asp:Button ID="btnSelect" runat="server" BorderColor="#66CCFF" BorderStyle="Solid" BorderWidth="1px" Font-Size="Large" Height="29px" OnClick="btnSelect_Click" Text="Select" Width="76px" />
                </td>
                <td style="width: 116px">
                    &nbsp;</td>
            </tr>
             </Table>      

    </asp:Panel>

    <ajaxToolkit:AutoCompleteExtender ID="tbSearch_AutoCompleteExtender" runat="server" BehaviorID="tbSearch_AutoCompleteExtender" DelimiterCharacters=""
        ServiceMethod="SearchOrgs"
        TargetControlID="tbOrg"
        MinimumPrefixLength="2"
        CompletionInterval="100"
        EnableCaching="false"
        CompletionSetCount="10">
    </ajaxToolkit:AutoCompleteExtender>
    <asp:Label ID="lblErrorMsg" runat="server" Visible="false" Text=""></asp:Label>
    <asp:Panel ID="pnlExisting" BackColor="PeachPuff" runat="server">
        <asp:Label ID="lblWarnExisting" runat="server" ></asp:Label>

        <br />

        <asp:Button ID="btnUseExisting" OnClick="btnUseExisting_Click" runat="server" Text="Update Existing Record" />
        <asp:Button ID="btnCreateNew" OnClick="btnCreateNew_Click" runat="server" Text="Create New Record" />
    </asp:Panel>
    <asp:Panel ID="Panel1" runat="server" >
        <table style="width: 100%">
            <tr>
                <td style="width: 24px; height: 20px;"></td>
                <td style="width: 142px; height: 20px">Organization:</td>
                <td style="height: 20px">
                    <asp:TextBox ID="tbOrgName" Width="211px" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 24px; height: 20px;">&nbsp;</td>
                <td style="width: 142px">Kit Number</td>
                <td>
                    <asp:TextBox Width="211px" ID="tbKitNum" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 24px; height: 20px;"></td>
                <td style="width: 142px">Station Number:</td>
                <td>
                    <asp:TextBox Width="211px" ID="tbStationNum" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 24px; height: 20px;">&nbsp;</td>
                <td style="width: 142px">River:</td>
                <td>
                    <asp:TextBox Width="211px" ID="tbRiver" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 24px; height: 20px;"></td>
                <td>Station Name: </td>
                <td>
                    <asp:TextBox Style="width: 211px" ID="tbStationName" runat="server"></asp:TextBox>
                </td>
            </tr>

        </table>


        <asp:FormView ID="FormView1" runat="server" DataSourceID="SqlDataSourceInBoundSample" DefaultMode="Insert" DataKeyNames="inbSampleID"  Width="674px">

            <InsertItemTemplate>
<%--                 StationNum:
            <asp:TextBox ID="StationNumTextBox" runat="server" Text='<%# Bind("StationNum") %>' />
            <br />
            SampleID:
            <asp:TextBox ID="SampleIDTextBox" runat="server" Text='<%# Bind("SampleID") %>' />
            <br />
            txtSampleID:
            <asp:TextBox ID="txtSampleIDTextBox" runat="server" Text='<%# Bind("txtSampleID") %>' />
            <br />
            KitNum:
            <asp:TextBox ID="KitNumTextBox" runat="server" Text='<%# Bind("KitNum") %>' />
            <br />--%>

                <table>
<%--                    <tr>
                        <td>Sample Date:
                        </td>
                        <td>
                            <asp:TextBox ID="DateTextBox" runat="server" Text='<%# Bind("Date") %>' />
                         </td>
                    </tr>

                    <tr>
                        <td>Sample Time (24 Hr HH:MM):
                        </td>
                        <td>
                            <asp:TextBox ID="TimeTextBox" runat="server" Text='<%# Bind("Time") %>' />
                           </td>
                    </tr>--%>

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
                    <asp:Label ID="Label1" runat="server" Text="Samples Collected (please select which sample types were collected)" Height="34px"></asp:Label>
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
                            <asp:CheckBox ID="MetalsNormalCheckBox" runat="server" Checked='<%# Bind("MetalsNormal") %>' />
                            Normal
                        </td>
                        <td>
                            <asp:CheckBox ID="NPCheckBox" runat="server" Checked='<%# Bind("NP") %>' />
                            NP
                        </td>
                        <td>
                            <asp:CheckBox ID="BugsCheckBox" runat="server" Checked='<%# Bind("Bugs") %>' />
                            Bugs
                        </td>
                    </tr>
                    <tr>
                        <%-- row 3 --%>
                        <td>&nbsp;</td>
                        <td>
                            <asp:CheckBox ID="MetalsBlnkCheckBox" runat="server" Checked='<%# Bind("MetalsBlnk") %>' />
                            Blank
                        </td>

                        <td>
                            <asp:CheckBox ID="CSCheckBox" runat="server" Checked='<%# Bind("CS") %>' />
                            Standard CS
                        </td>
                        <td>
                            <asp:CheckBox ID="BugsQACheckBox" runat="server" Checked='<%# Bind("BugsQA") %>' />
                            BugsQA
                        </td>
                    </tr>
                    <tr>
                        <%-- row 4 --%>
                        <td>&nbsp;</td>
                        <td>
                            <asp:CheckBox ID="MetalsDupeCheckBox" runat="server" Checked='<%# Bind("MetalsDupe") %>' />
                            Duplicate
                        </td>

                        <td>
                            <asp:CheckBox ID="TSSCheckBox" runat="server" Checked='<%# Bind("TSS") %>' />
                            TSS
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <%-- row 5 --%>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:CheckBox ID="NPDupeCheckBox" runat="server" Checked='<%# Bind("NPDupe") %>' />
                            Duplicate NP
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <%-- row 6 --%>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("CSDupe") %>' />
                            Duplicate CS
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <%-- row 7 --%>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:CheckBox ID="TSSDupeCheckBox" runat="server" Checked='<%# Bind("TSSDupe") %>' />
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
                            <asp:Button ID="InsertButton" ClientIDMode="Static" OnClick="InsertButton_Click" runat="server" CausesValidation="True" CommandName="Insert" Text="Save Data" />
                        </td>

                    </tr>
                </table>

                &nbsp;
                <%--            <asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />--%>
            </InsertItemTemplate>

<%--            added new update item template using copy and paste from insert template--%>

             <EditItemTemplate>
<%--                 StationNum:
            <asp:TextBox ID="StationNumTextBox" runat="server" Text='<%# Bind("StationNum") %>' />
            <br />
            SampleID:
            <asp:TextBox ID="SampleIDTextBox" runat="server" Text='<%# Bind("SampleID") %>' />
            <br />
            txtSampleID:
            <asp:TextBox ID="txtSampleIDTextBox" runat="server" Text='<%# Bind("txtSampleID") %>' />
            <br />
            KitNum:
            <asp:TextBox ID="KitNumTextBox" runat="server" Text='<%# Bind("KitNum") %>' />
            <br />--%>

                <table>
<%--                    <tr>
                        <td>Sample Date:
                        </td>
                        <td>
                            <asp:TextBox ID="DateTextBox" runat="server" Text='<%# Bind("Date") %>' />
                         </td>
                    </tr>

                    <tr>
                        <td>Sample Time (24 Hr HH:MM):
                        </td>
                        <td>
                            <asp:TextBox ID="TimeTextBox" runat="server" Text='<%# Bind("Time") %>' />
                           </td>
                    </tr>--%>

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
                    <asp:Label ID="Label1" runat="server" Text="Samples Collected (please select which sample types were collected)" Height="34px"></asp:Label>
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
                            <asp:CheckBox ID="MetalsNormalCheckBox" runat="server" Checked='<%# Bind("MetalsNormal") %>' />
                            Normal
                        </td>
                        <td>
                            <asp:CheckBox ID="NPCheckBox" runat="server" Checked='<%# Bind("NP") %>' />
                            NP
                        </td>
                        <td>
                            <asp:CheckBox ID="BugsCheckBox" runat="server" Checked='<%# Bind("Bugs") %>' />
                            Bugs
                        </td>
                    </tr>
                    <tr>
                        <%-- row 3 --%>
                        <td>&nbsp;</td>
                        <td>
                            <asp:CheckBox ID="MetalsBlnkCheckBox" runat="server" Checked='<%# Bind("MetalsBlnk") %>' />
                            Blank
                        </td>

                        <td>
                            <asp:CheckBox ID="CSCheckBox" runat="server" Checked='<%# Bind("CS") %>' />
                            Standard CS
                        </td>
                        <td>
                            <asp:CheckBox ID="BugsQACheckBox" runat="server" Checked='<%# Bind("BugsQA") %>' />
                            BugsQA
                        </td>
                    </tr>
                    <tr>
                        <%-- row 4 --%>
                        <td>&nbsp;</td>
                        <td>
                            <asp:CheckBox ID="MetalsDupeCheckBox" runat="server" Checked='<%# Bind("MetalsDupe") %>' />
                            Duplicate
                        </td>

                        <td>
                            <asp:CheckBox ID="TSSCheckBox" runat="server" Checked='<%# Bind("TSS") %>' />
                            TSS
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <%-- row 5 --%>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:CheckBox ID="NPDupeCheckBox" runat="server" Checked='<%# Bind("NPDupe") %>' />
                            Duplicate NP
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <%-- row 6 --%>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("CSDupe") %>' />
                            Duplicate CS
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <%-- row 7 --%>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:CheckBox ID="TSSDupeCheckBox" runat="server" Checked='<%# Bind("TSSDupe") %>' />
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
            <asp:TextBox ID="CommentsTextBox" runat="server" Text='<%# Bind("Comments") %>' TextMode="MultiLine" Width="560" Height="44" />

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
                            <asp:Button ID="UpdateButton" ClientIDMode="Static"  runat="server" CausesValidation="True" CommandName="Update" Text="Update Data" />
                        </td>
                    </tr>
                </table>

                &nbsp;
                            <asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
            </EditItemTemplate>


        </asp:FormView>

    </asp:Panel>

<asp:SqlDataSource ID="SqlDataSourceInBoundSample" runat="server" OnInserting="SqlDataSourceInBoundSample_Inserting" ConnectionString="<%$ ConnectionStrings:RiverWatchConnectionString %>"
    DeleteCommand="DELETE FROM [InboundSamples] WHERE [inbSampleID] = @inbSampleID"
    InsertCommand="INSERT INTO [InboundSamples] ([StationNum], [SampleID], [txtSampleID], [KitNum], [Date], [Time], [USGSFlow], [PH], [TempC], [PhenAlk], [TotalAlk], [TotalHard], [DO], [DOsat], [Tag], [Chk], [EntryType], [EntryStaff], [MetalsNormal], [MetalsBlnk], [MetalsDupe], [Bugs], [BugsQA], [TSS], [CS], [NP], [TSSDupe], [CSDupe], [NPDupe], [FieldValid], [MetalsStat], [FinalCheck], [Method], [Comments], [DateReceived], [DataSheetIncluded], [MissingDataSheetReqDate], [ChainOfCustody], [MissingDataSheetReceived], [PassValStep], [tblSampleID]) VALUES (@StationNum, @SampleID, @txtSampleID, @KitNum, @Date, @Time, @USGSFlow, @PH, @TempC, @PhenAlk, @TotalAlk, @TotalHard, @DO, @DOsat, @Tag, @Chk, @EntryType, @EntryStaff, @MetalsNormal, @MetalsBlnk, @MetalsDupe, @Bugs, @BugsQA, @TSS, @CS, @NP, @TSSDupe, @CSDupe, @NPDupe, @FieldValid, @MetalsStat, @FinalCheck, @Method, @Comments, @DateReceived, @DataSheetIncluded, @MissingDataSheetReqDate, @ChainOfCustody, @MissingDataSheetReceived, @PassValStep, @tblSampleID)"
    SelectCommand="SELECT * FROM [InboundSamples]"
    UpdateCommand="UPDATE [InboundSamples] SET [StationNum] = @StationNum, [SampleID] = @SampleID, [txtSampleID] = @txtSampleID, [KitNum] = @KitNum, [Date] = @Date, [Time] = @Time, [USGSFlow] = @USGSFlow, [PH] = @PH, [TempC] = @TempC, [PhenAlk] = @PhenAlk, [TotalAlk] = @TotalAlk, [TotalHard] = @TotalHard, [DO] = @DO, [DOsat] = @DOsat, [Tag] = @Tag, [Chk] = @Chk, [EntryType] = @EntryType, [EntryStaff] = @EntryStaff, [MetalsNormal] = @MetalsNormal, [MetalsBlnk] = @MetalsBlnk, [MetalsDupe] = @MetalsDupe, [Bugs] = @Bugs, [BugsQA] = @BugsQA, [TSS] = @TSS, [CS] = @CS, [NP] = @NP, [TSSDupe] = @TSSDupe, [CSDupe] = @CSDupe, [NPDupe] = @NPDupe, [FieldValid] = @FieldValid, [MetalsStat] = @MetalsStat, [FinalCheck] = @FinalCheck, [Method] = @Method, [Comments] = @Comments, [DateReceived] = @DateReceived, [DataSheetIncluded] = @DataSheetIncluded, [MissingDataSheetReqDate] = @MissingDataSheetReqDate, [ChainOfCustody] = @ChainOfCustody, [MissingDataSheetReceived] = @MissingDataSheetReceived, [PassValStep] = @PassValStep, [tblSampleID] = @tblSampleID WHERE [inbSampleID] = @inbSampleID">
    <DeleteParameters>
        <asp:Parameter Name="inbSampleID" Type="Int32" />
    </DeleteParameters>
    <InsertParameters>
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
    </InsertParameters>

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

