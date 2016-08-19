<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewSamples2.aspx.cs" Inherits="RWInbound2.Samples.NewSamples2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <br />
    <div class="site-title">
        Sample
    </div>

    <table>
        <tr>
            <td style="width: 193px">Station Number: 
                 <asp:TextBox ID="tbSite" runat="server" Height="20px" Width="46px" BorderColor="#66CCFF" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>

            </td>
            <td style="width: 166px">Kit Number: 
                 <asp:TextBox ID="tbKitNumber" runat="server" BorderColor="#66CCFF" BorderStyle="Solid" BorderWidth="1px" Height="20px" Width="46px"></asp:TextBox>
            </td>
            <td style="width: 300px">OR Org: 
                     <asp:TextBox ID="tbOrg" runat="server" BorderColor="#66CCFF" BorderStyle="Solid" BorderWidth="1px" Height="20px" Width="146px"></asp:TextBox>

            <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                TargetControlID="tbOrg"
                CompletionSetCount="10"
                ServiceMethod="SearchOrgs"
                MinimumPrefixLength="2"
                CompletionInterval="100">
            </ajaxToolkit:AutoCompleteExtender>

            </td>
            <td style="width: 116px">

                <asp:Button ID="btnSiteNumber" runat="server" Text="Select" CssClass="samplesButton" OnClick="btnSiteNumber_Click" />
            </td>
        </tr>
    </table>

    <asp:Panel ID="Panel1" runat="server" BorderColor="#0099FF" BorderStyle="Solid" BorderWidth="1px">
        <table style="width: 100%">
            <tr>
                <td>
                    <asp:Label ID="lblStationNumber" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblStationDescription" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblRiver" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="LblStartDate" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblOrganization" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblBlankForNow" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblWatershed" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblEndDate" runat="server"></asp:Label></td>
            </tr>
        </table>

        <table runat="server" id="tableSampleList">
            <tr>
                <td style="width: 68px">

                    <asp:Label ID="lblInbSampLst" runat="server" Width="164px">Inbound Sample List</asp:Label>
                </td>
                <td style="height: 10px; width: 179px;">
                    <asp:DropDownList ID="ddlInboundSamplePick" runat="server" AutoPostBack="True" Width="152px" Visible="true" OnSelectedIndexChanged="ddlInboundSamplePick_SelectedIndexChanged">
                    </asp:DropDownList></td>
            </tr>
        </table>

        <table>
            <tr>
                <td>
                    <table id="newSamp" border="0" runat="server">
                        <tr>
                            <td>
                                <asp:Label ID="lblCollectionDate" runat="server" Text="Collection Date: "></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtDateCollected" BorderColor="#66CCFF" BorderStyle="Solid" BorderWidth="1px" runat="server" Width="80px"></asp:TextBox>

                                <ajaxToolkit:CalendarExtender ID="txtDateCollected_CalendarExtender" runat="server" BehaviorID="txtDateCollected_CalendarExtender" TargetControlID="txtDateCollected"></ajaxToolkit:CalendarExtender>
                                <td>
                                    <asp:Label ID="lblCollectionTime" runat="server" Text="Time: "></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtTimeCollected" runat="server" BorderColor="#66CCFF" BorderStyle="Solid" BorderWidth="1px" Width="60px" ToolTip="Enter time as hh:mm and A or P for AM or PM"></asp:TextBox>
                                <ajaxToolkit:MaskedEditExtender ID="txtTimeCollected_MaskedEditExtender" runat="server" BehaviorID="txtTimeCollected_MaskedEditExtender" Century="2000"
                                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" TargetControlID="txtTimeCollected" MaskType="Time" Mask="99:99" AcceptAMPM="false" />
                                <br>
                            </td>
                            <td>
                                <asp:Button ID="btnCreate" runat="server" Text="Create -&gt;" CausesValidation="False" CssClass="samplesButton" OnClick="btnCreate_Click"></asp:Button></td>
                            <td>
                                <asp:Label ID="lblSampleNumber" runat="server" Text="Samp# "></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtSmpNum" runat="server" Width="128px"></asp:TextBox>
                            </td>

                            <td>
                                <asp:Label ID="lblEvent" runat="server" Text="Event# "></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtNumSmp" runat="server" Width="80px"></asp:TextBox><br>
                            </td>

                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>

    <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" UseVerticalStripPlacement="False">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        
        <ajaxToolkit:TabPanel runat="server" HeaderText="Sample" ID="TabPanelSample">
            <ContentTemplate>
                <table style="width: 762px">
                    <tr>
                        <td style="height: 21px; width: 198px;">
                            <asp:CheckBox ID="chkCOC" runat="server" Text="Chain of Custody"></asp:CheckBox></td>
                        <td style="height: 21px; width: 189px;">&nbsp;
                        </td>
                        <td style="height: 21px; width: 163px;">
                            <asp:CheckBox ID="chkPhysHab" runat="server" Text="Physical Habitat"></asp:CheckBox></td>
                        <td class="ListboxSample" rowspan="7">
                            <asp:Label ID="lblListYears" runat="server" Text="Status Year: "></asp:Label>
                            <asp:DropDownList ID="ddlYears" runat="server" OnSelectedIndexChanged="ddlYears_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList>
                            <asp:ListBox ID="lstSamples" runat="server" AutoPostBack="True" Height="153px" Rows="7" CssClass="ListboxSample"
                                OnSelectedIndexChanged="lstSamples_SelectedIndexChanged"></asp:ListBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 198px">
                            <asp:CheckBox ID="chkDataSheet" runat="server" Text="Datasheet" EnableTheming="False"></asp:CheckBox></td>
                        <td style="width: 189px">
                            <asp:CheckBox ID="chkMDSR" runat="server" Text="MDSR Received"></asp:CheckBox></td>
                        <td style="width: 163px">
                            <asp:CheckBox ID="chkBugs" runat="server" Text="Bugs"></asp:CheckBox></td>
                    </tr>
                    <tr>
                        <td style="width: 198px">
                            <asp:CheckBox ID="chkNoNut" runat="server" Text="No Nutrients"></asp:CheckBox></td>
                        <td style="width: 189px">
                            <asp:TextBox ID="txtMDSR" runat="server" Width="80px"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="txtMDSR_CalendarExtender" runat="server" BehaviorID="txtMDSR_CalendarExtender" TargetControlID="txtMDSR" />
                            MDSR</td>
                        <td style="width: 163px">
                            <asp:CheckBox ID="chkBugQA" runat="server" Text="Bugs QA"></asp:CheckBox></td>
                    </tr>
                    <tr>
                        <td style="width: 198px">
                            <asp:CheckBox ID="chkNoMtls" runat="server" Text="No Metals"></asp:CheckBox></td>
                        <td style="width: 189px">
                            <asp:CheckBox ID="chkBlkMtls" runat="server" Text="Blank Metals"></asp:CheckBox></td>
                        <td style="width: 163px">
                            <asp:CheckBox ID="chkDupeMtls" runat="server" Text="Duplicate Metals"></asp:CheckBox></td>
                    </tr>
                    <tr>
                        <td style="width: 198px">
                            <asp:CheckBox ID="chkTSS" runat="server" Text="Total Suspended Solids"></asp:CheckBox></td>
                        <td style="width: 189px">
                            <asp:CheckBox ID="chkNP" runat="server" Text="Nitrate &amp; Phos"></asp:CheckBox></td>
                        <td style="width: 163px">
                            <asp:CheckBox ID="chkCS" runat="server" Text="Chloride/Sulfate"></asp:CheckBox></td>
                    </tr>
                    <tr>
                        <td style="width: 198px">
                            <asp:CheckBox ID="chkTSSDupe" runat="server" Text="Duplicated TSS"></asp:CheckBox></td>
                        <td style="width: 189px">
                            <asp:CheckBox ID="chkNPDupe" runat="server" Text="Duplicate N&amp;P"></asp:CheckBox></td>
                        <td style="width: 163px">
                            <asp:CheckBox ID="chkCSDupe" runat="server" Text="Duplicate C/S"></asp:CheckBox></td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                </table>
                <table>
                    <tr style="width: 800px">
                        <td style="width: 57px">Comment:</td>
                        <td>
                            <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Width="391px"></asp:TextBox>
                            <br />
                        </td>
                    </tr>

                </table>

                <asp:Button ID="btnSaveSample" runat="server" CssClass="samplesButton" Text="Save Sample" OnClick="btnSaveSample_Click" Width="100px" />

                <asp:Label ID="lblWarning" runat="server" ></asp:Label>

            </ContentTemplate>
        </ajaxToolkit:TabPanel>


        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        <ajaxToolkit:TabPanel runat="server" HeaderText="Metals Barcodes" ID="TabMetalsBarcode">
            <ContentTemplate>
                <table>
                    <tr>
                        <td style="width: 133px">Bar Code / Lab ID:</td>
                        <td>
                            <asp:TextBox ID="tbBarcode" runat="server"></asp:TextBox>

                            <asp:Button ID="btnCheckBarcode" OnClick="btnCheckBarcode_Click" runat="server" Text="Check" CssClass="smallButton" />
                            <asp:Label ID="lblBarcodeUsed" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 133px">Type:</td>
                        <td>
                            <asp:RadioButtonList ID="rbListSampleTypes" runat="server" EnableTheming="True" RepeatColumns="3">
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 133px">Contaminated?</td>
                        <td>
                            <asp:CheckBox ID="chContaminated" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 133px">Box Number:</td>
                        <td>
                            <asp:TextBox ID="tbBoxNumber" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 133px">Verified:</td>
                        <td>
                            <asp:CheckBox ID="cbVerified" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 133px">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>

                <asp:Button ID="btnMetalsSave" runat="server" Text="Save" OnClick="btnSaveMetals_Click" CssClass="samplesButton" />

                <asp:Label ID="lblCodeInUse" runat="server" Text=""></asp:Label>

                <%--                add a new grid to show existing bar codes--%>

                <asp:Panel ID="pnlBarcodeTab" CssClass="pnlBarcodeTab" runat="server">
                    <asp:GridView ID="GridViewBarCodes" runat="server" CellPadding="2"></asp:GridView>

                </asp:Panel>

            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        <ajaxToolkit:TabPanel runat="server" HeaderText="Nutrient Barcodes" ID="TabNutrientBarcode">
            <ContentTemplate>
                <table>
                    <tr>
                        <td style="width: 141px">BarCode / Lab ID:</td>
                        <td>
                            <asp:TextBox ID="tbNutrientCode" runat="server"></asp:TextBox>&nbsp;
						<asp:Button ID="btnBarcodeSearch" runat="server" OnClick="btnBarcodeSearch_Click" CssClass="smallButton" Text="Search"></asp:Button>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblNutBarcodeMsg" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>

                        <td style="width: 221px" colspan="2">
                            <asp:CheckBoxList ID="chksNutrients" runat="server" Width="432px" RepeatColumns="3">
                                <asp:ListItem Value="NitrateNitrite">Nitrate + Nitrite</asp:ListItem>
                                <asp:ListItem Value="TotalPhos">Phosphorus, Total</asp:ListItem>
                                <asp:ListItem Value="Ammonia">Ammonia</asp:ListItem>
                                <asp:ListItem Value="OrthoPhos">Phosphate, Ortho</asp:ListItem>
                                <asp:ListItem Value="Chloride">Chloride</asp:ListItem>
                                <asp:ListItem Value="Sulfate">Sulfate</asp:ListItem>
                                <asp:ListItem Value="TSS">TSS</asp:ListItem>
                                <asp:ListItem Value="TotalNitro">Nitrogen, Total</asp:ListItem>
                                <asp:ListItem Value="ChlorA">Chlorophyll A</asp:ListItem>
                                <asp:ListItem Value="DOC">Dissolved Organic Carbon</asp:ListItem>
                            </asp:CheckBoxList></td>
                    </tr>
                    <tr>

                        <td style="width: 121px">Log Date:</td>
                        <td>
                            <asp:Label ID="lblLogDate" runat="server" Width="144px"></asp:Label></td>
                    </tr>
                    <tr>

                        <td style="width: 121px">Analyze Date:</td>
                        <td>
                            <asp:TextBox ID="tbAnalyzeDate" runat="server"></asp:TextBox></td>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtenderAnalyzeDate" TargetControlID="tbAnalyzeDate" runat="server" />
                    </tr>
                    <tr>

                        <td style="width: 121px">Done:</td>
                        <td>
                            <asp:CheckBox ID="chkDone" runat="server" Enabled="False"></asp:CheckBox></td>
                    </tr>
                    <%-- <tr>

                        <td style="width: 121px">Too Warm:</td>
                        <td>
                            <asp:CheckBox ID="chkTooWarm" runat="server"></asp:CheckBox></td>
                    </tr>--%>

                    <tr>

                        <td style="width: 121px">Comments:</td>
                        <td>
                            <asp:TextBox ID="tbComments" runat="server" TextMode="MultiLine" Width="420px"></asp:TextBox>

                        </td>

                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnSaveNutrient" runat="server" OnClick="btnSaveNutrient_Click" CssClass="samplesButton" Text="Save" Width="60px" />
                        </td>
                        <td>
                            <asp:Label ID="lblNutrientBCSave" runat="server" Text=""></asp:Label>

                        </td>
                        <td style="width: 121px"></td>
                        <td></td>
                    </tr>
                </table>

            </ContentTemplate>
        </ajaxToolkit:TabPanel>

        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <ajaxToolkit:TabPanel runat="server" HeaderText="Update Org" ID="TabUpdateOrg">
            <ContentTemplate>
                <div runat="server" id="divSizer" style="border-left: 12px">
                
                        <asp:Label ID="Label3" runat="server" Text="Samples Collected:"></asp:Label>
                        <asp:FormView ID="FormView1" runat="server"  AllowPaging="false" DataKeyNames="ID" DefaultMode="Edit" DataSourceID="SqlDataSourceOrgStatus">
                            <EditItemTemplate>
                                ID:
                <asp:Label ID="IDLabel1" runat="server" Text='<%# Eval("ID") %>' />
                                <br />
                                OrganizationID:
                <asp:TextBox ID="OrganizationIDTextBox" runat="server" Text='<%# Bind("OrganizationID") %>' />
                                <br />
                                ContractStartDate:
                <asp:TextBox ID="ContractStartDateTextBox" runat="server" Text='<%# Bind("ContractStartDate","{0:d}") %>' />
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" Animated="true" TargetControlID="ContractStartDateTextBox"  runat="server" />
                                <br />
                                ContractEndDate:
                <asp:TextBox ID="ContractEndDateTextBox" runat="server" Text='<%# Bind("ContractEndDate","{0:d}") %>' />
                                 <ajaxToolkit:CalendarExtender ID="CalendarExtender2" Animated="true" TargetControlID="ContractEndDateTextBox"   runat="server" />
                                <br />
                                ContractSigned:
                <asp:CheckBox ID="ContractSignedCheckBox" runat="server" Checked='<%# Bind("ContractSigned") %>' />
                                <br />
                                ContractSignedDate:
                <asp:TextBox ID="ContractSignedDateTextBox" runat="server" Text='<%# Bind("ContractSignedDate") %>' />
                                <br />
                                SiteVisited:
                <asp:CheckBox ID="SiteVisitedCheckBox" runat="server" Checked='<%# Bind("SiteVisited") %>' />
                                <br />
                                VolunteerTimeSheet1:
                <asp:CheckBox ID="VolunteerTimeSheet1CheckBox" runat="server" Checked='<%# Bind("VolunteerTimeSheet1") %>' />
                                <br />
                                VolunteerTimeShee2:
                <asp:CheckBox ID="VolunteerTimeShee2CheckBox" runat="server" Checked='<%# Bind("VolunteerTimeShee2") %>' />
                                <br />
                                VolunteerTimeSheet3:
                <asp:CheckBox ID="VolunteerTimeSheet3CheckBox" runat="server" Checked='<%# Bind("VolunteerTimeSheet3") %>' />
                                <br />
                                VolunteerTimeSheet4:
                <asp:CheckBox ID="VolunteerTimeSheet4CheckBox" runat="server" Checked='<%# Bind("VolunteerTimeSheet4") %>' />
                                <br />
                                DataEnteredElectronically1:
                <asp:CheckBox ID="DataEnteredElectronically1CheckBox" runat="server" Checked='<%# Bind("DataEnteredElectronically1") %>' />
                                <br />
                                DataEnteredElectronically2:
                <asp:CheckBox ID="DataEnteredElectronically2CheckBox" runat="server" Checked='<%# Bind("DataEnteredElectronically2") %>' />
                                <br />
                                DataEnteredElectronically3:
                <asp:CheckBox ID="DataEnteredElectronically3CheckBox" runat="server" Checked='<%# Bind("DataEnteredElectronically3") %>' />
                                <br />
                                DataEnteredElectronically4:
                <asp:CheckBox ID="DataEnteredElectronically4CheckBox" runat="server" Checked='<%# Bind("DataEnteredElectronically4") %>' />
                                <br />
                                SampleShipped1:
                <asp:CheckBox ID="SampleShipped1CheckBox" runat="server" Checked='<%# Bind("SampleShipped1") %>' />
                                <br />
                                SampleShipped2:
                <asp:CheckBox ID="SampleShipped2CheckBox" runat="server" Checked='<%# Bind("SampleShipped2") %>' />
                                <br />
                                SampleShipped3:
                <asp:CheckBox ID="SampleShipped3CheckBox" runat="server" Checked='<%# Bind("SampleShipped3") %>' />
                                <br />
                                SampleShipped4:
                <asp:CheckBox ID="SampleShipped4CheckBox" runat="server" Checked='<%# Bind("SampleShipped4") %>' />
                                <br />
                                NumberOfSamplesJan:
                <asp:TextBox ID="NumberOfSamplesJanTextBox" runat="server" Text='<%# Bind("NumberOfSamplesJan") %>' />
                                <br />
                                NumberOfSamplesFeb:
                <asp:TextBox ID="NumberOfSamplesFebTextBox" runat="server" Text='<%# Bind("NumberOfSamplesFeb") %>' />
                                <br />
                                NumberOfSamplesMar:
                <asp:TextBox ID="NumberOfSamplesMarTextBox" runat="server" Text='<%# Bind("NumberOfSamplesMar") %>' />
                                <br />
                                NumberOfSamplesApr:
                <asp:TextBox ID="NumberOfSamplesAprTextBox" runat="server" Text='<%# Bind("NumberOfSamplesApr") %>' />
                                <br />
                                NumberOfSamplesMay:
                <asp:TextBox ID="NumberOfSamplesMayTextBox" runat="server" Text='<%# Bind("NumberOfSamplesMay") %>' />
                                <br />
                                NumberOfSamplesJun:
                <asp:TextBox ID="NumberOfSamplesJunTextBox" runat="server" Text='<%# Bind("NumberOfSamplesJun") %>' />
                                <br />
                                NumberOfSamplesJul:
                <asp:TextBox ID="NumberOfSamplesJulTextBox" runat="server" Text='<%# Bind("NumberOfSamplesJul") %>' />
                                <br />
                                NumberOfSamplesAug:
                <asp:TextBox ID="NumberOfSamplesAugTextBox" runat="server" Text='<%# Bind("NumberOfSamplesAug") %>' />
                                <br />
                                NumberOfSamplesSep:
                <asp:TextBox ID="NumberOfSamplesSepTextBox" runat="server" Text='<%# Bind("NumberOfSamplesSep") %>' />
                                <br />
                                NumberOfSamplesOct:
                <asp:TextBox ID="NumberOfSamplesOctTextBox" runat="server" Text='<%# Bind("NumberOfSamplesOct") %>' />
                                <br />
                                NumberOfSamplesNov:
                <asp:TextBox ID="NumberOfSamplesNovTextBox" runat="server" Text='<%# Bind("NumberOfSamplesNov") %>' />
                                <br />
                                NumberOfSamplesDec:
                <asp:TextBox ID="NumberOfSamplesDecTextBox" runat="server" Text='<%# Bind("NumberOfSamplesDec") %>' />
                                <br />
                                Nutrient1Collected:
                <asp:CheckBox ID="Nutrient1CollectedCheckBox" runat="server" Checked='<%# Bind("Nutrient1Collected") %>' />
                                <br />
                                Nutrient2Collected:
                <asp:CheckBox ID="Nutrient2CollectedCheckBox" runat="server" Checked='<%# Bind("Nutrient2Collected") %>' />
                                <br />
                                BugCollected:
                <asp:CheckBox ID="BugCollectedCheckBox" runat="server" Checked='<%# Bind("BugCollected") %>' />
                                <br />
                                UnknownSpringRecordedDate:
                <asp:TextBox ID="UnknownSpringRecordedDateTextBox" runat="server" Text='<%# Bind("UnknownSpringRecordedDate") %>' />
                                <br />
                                UnknownFallRecordedDate:
                <asp:TextBox ID="UnknownFallRecordedDateTextBox" runat="server" Text='<%# Bind("UnknownFallRecordedDate") %>' />
                                <br />
                                NumberOfSamplesBlank:
                <asp:TextBox ID="NumberOfSamplesBlankTextBox" runat="server" Text='<%# Bind("NumberOfSamplesBlank") %>' />
                                <br />
                                NumberOfSamplesDuplicate:
                <asp:TextBox ID="NumberOfSamplesDuplicateTextBox" runat="server" Text='<%# Bind("NumberOfSamplesDuplicate") %>' />
                                <br />
                                TroubleComment:
                <asp:TextBox ID="TroubleCommentTextBox" runat="server" Text='<%# Bind("TroubleComment") %>' />
                                <br />
                                NoteComment:
                <asp:TextBox ID="NoteCommentTextBox" runat="server" Text='<%# Bind("NoteComment") %>' />
                                <br />
                                HardshipComment:
                <asp:TextBox ID="HardshipCommentTextBox" runat="server" Text='<%# Bind("HardshipComment") %>' />
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
                                NumberOfMetalsBlank:
                <asp:TextBox ID="NumberOfMetalsBlankTextBox" runat="server" Text='<%# Bind("NumberOfMetalsBlank") %>' />
                                <br />
                                NumberOfMetalsDuplicate:
                <asp:TextBox ID="NumberOfMetalsDuplicateTextBox" runat="server" Text='<%# Bind("NumberOfMetalsDuplicate") %>' />
                                <br />
                                <asp:Button ID="UpdateButton" runat="server" OnClick="UpdateButton_Click" CausesValidation="True" CssClass="samplesButton" CommandName="Update" Text="Update" />
                                &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
                            </EditItemTemplate>
                        </asp:FormView>


                    </div>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>

        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <ajaxToolkit:TabPanel runat="server" HeaderText="Create ICP Data" ID="TabICPdata">
            <ContentTemplate>

                <div>

                    <asp:Label ID="Label5" runat="server" Text="Select samples to be put in Incoming ICP table"></asp:Label>
                    <p>
                        &nbsp;
                    </p>
                    <asp:GridView ID="GridView1" runat="server">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbSelectBarcode" runat="server" Text="Select" Checked="false" />
                                </ItemTemplate>

                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                </div>

                <asp:Button ID="btnICPSave" runat="server" Text="Save" CssClass="samplesButton" OnClick="btnICPSave_Click" />

            </ContentTemplate>
        </ajaxToolkit:TabPanel>


        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <%-- &nbsp;&nbsp;&nbsp;--%>
&nbsp;&nbsp;&nbsp;
    </ajaxToolkit:TabContainer>


    <asp:SqlDataSource ID="SqlDataSourceOrgStatus" runat="server" ConnectionString="<%$ ConnectionStrings:RiverwatchDEV %>"
        SelectCommand="SELECT * FROM [OrgStatus] "
        UpdateCommand="UPDATE [OrgStatus] SET [OrganizationID] = @OrganizationID, [ContractStartDate] = @ContractStartDate, [ContractEndDate] = @ContractEndDate, [ContractSigned] = @ContractSigned, [ContractSignedDate] = @ContractSignedDate, [SiteVisited] = @SiteVisited, [VolunteerTimeSheet1] = @VolunteerTimeSheet1, [VolunteerTimeShee2] = @VolunteerTimeShee2, [VolunteerTimeSheet3] = @VolunteerTimeSheet3, [VolunteerTimeSheet4] = @VolunteerTimeSheet4, [DataEnteredElectronically1] = @DataEnteredElectronically1, [DataEnteredElectronically2] = @DataEnteredElectronically2, [DataEnteredElectronically3] = @DataEnteredElectronically3, [DataEnteredElectronically4] = @DataEnteredElectronically4, [SampleShipped1] = @SampleShipped1, [SampleShipped2] = @SampleShipped2, [SampleShipped3] = @SampleShipped3, [SampleShipped4] = @SampleShipped4, [NumberOfSamplesJan] = @NumberOfSamplesJan, [NumberOfSamplesFeb] = @NumberOfSamplesFeb, [NumberOfSamplesMar] = @NumberOfSamplesMar, [NumberOfSamplesApr] = @NumberOfSamplesApr, [NumberOfSamplesMay] = @NumberOfSamplesMay, [NumberOfSamplesJun] = @NumberOfSamplesJun, [NumberOfSamplesJul] = @NumberOfSamplesJul, [NumberOfSamplesAug] = @NumberOfSamplesAug, [NumberOfSamplesSep] = @NumberOfSamplesSep, [NumberOfSamplesOct] = @NumberOfSamplesOct, [NumberOfSamplesNov] = @NumberOfSamplesNov, [NumberOfSamplesDec] = @NumberOfSamplesDec, [Nutrient1Collected] = @Nutrient1Collected, [Nutrient2Collected] = @Nutrient2Collected, [BugCollected] = @BugCollected, [UnknownSpringRecordedDate] = @UnknownSpringRecordedDate, [UnknownFallRecordedDate] = @UnknownFallRecordedDate, [NumberOfSamplesBlank] = @NumberOfSamplesBlank, [NumberOfSamplesDuplicate] = @NumberOfSamplesDuplicate, [TroubleComment] = @TroubleComment, [NoteComment] = @NoteComment, [HardshipComment] = @HardshipComment, [DateCreated] = @DateCreated, [UserCreated] = @UserCreated, [DateLastModified] = @DateLastModified, [UserLastModified] = @UserLastModified, [NumberOfMetalsBlank] = @NumberOfMetalsBlank, [NumberOfMetalsDuplicate] = @NumberOfMetalsDuplicate WHERE [ID] = @ID">

        <UpdateParameters>
            <asp:Parameter Name="OrganizationID" Type="Int32" />
            <asp:Parameter Name="ContractStartDate" Type="DateTime" />
            <asp:Parameter Name="ContractEndDate" Type="DateTime" />
            <asp:Parameter Name="ContractSigned" Type="Boolean" />
            <asp:Parameter Name="ContractSignedDate" Type="DateTime" />
            <asp:Parameter Name="SiteVisited" Type="Boolean" />
            <asp:Parameter Name="VolunteerTimeSheet1" Type="Boolean" />
            <asp:Parameter Name="VolunteerTimeShee2" Type="Boolean" />
            <asp:Parameter Name="VolunteerTimeSheet3" Type="Boolean" />
            <asp:Parameter Name="VolunteerTimeSheet4" Type="Boolean" />
            <asp:Parameter Name="DataEnteredElectronically1" Type="Boolean" />
            <asp:Parameter Name="DataEnteredElectronically2" Type="Boolean" />
            <asp:Parameter Name="DataEnteredElectronically3" Type="Boolean" />
            <asp:Parameter Name="DataEnteredElectronically4" Type="Boolean" />
            <asp:Parameter Name="SampleShipped1" Type="Boolean" />
            <asp:Parameter Name="SampleShipped2" Type="Boolean" />
            <asp:Parameter Name="SampleShipped3" Type="Boolean" />
            <asp:Parameter Name="SampleShipped4" Type="Boolean" />
            <asp:Parameter Name="NumberOfSamplesJan" Type="Int32" />
            <asp:Parameter Name="NumberOfSamplesFeb" Type="Int32" />
            <asp:Parameter Name="NumberOfSamplesMar" Type="Int32" />
            <asp:Parameter Name="NumberOfSamplesApr" Type="Int32" />
            <asp:Parameter Name="NumberOfSamplesMay" Type="Int32" />
            <asp:Parameter Name="NumberOfSamplesJun" Type="Int32" />
            <asp:Parameter Name="NumberOfSamplesJul" Type="Int32" />
            <asp:Parameter Name="NumberOfSamplesAug" Type="Int32" />
            <asp:Parameter Name="NumberOfSamplesSep" Type="Int32" />
            <asp:Parameter Name="NumberOfSamplesOct" Type="Int32" />
            <asp:Parameter Name="NumberOfSamplesNov" Type="Int32" />
            <asp:Parameter Name="NumberOfSamplesDec" Type="Int32" />
            <asp:Parameter Name="Nutrient1Collected" Type="Boolean" />
            <asp:Parameter Name="Nutrient2Collected" Type="Boolean" />
            <asp:Parameter Name="BugCollected" Type="Boolean" />
            <asp:Parameter Name="UnknownSpringRecordedDate" Type="DateTime" />
            <asp:Parameter Name="UnknownFallRecordedDate" Type="DateTime" />
            <asp:Parameter Name="NumberOfSamplesBlank" Type="Int32" />
            <asp:Parameter Name="NumberOfSamplesDuplicate" Type="Int32" />
            <asp:Parameter Name="TroubleComment" Type="String" />
            <asp:Parameter Name="NoteComment" Type="String" />
            <asp:Parameter Name="HardshipComment" Type="String" />
            <asp:Parameter Name="DateCreated" Type="DateTime" />
            <asp:Parameter Name="UserCreated" Type="String" />
            <asp:Parameter Name="DateLastModified" Type="DateTime" />
            <asp:Parameter Name="UserLastModified" Type="String" />
            <asp:Parameter Name="NumberOfMetalsBlank" Type="Int32" />
            <asp:Parameter Name="NumberOfMetalsDuplicate" Type="Int32" />
            <asp:Parameter Name="ID" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>
