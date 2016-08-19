<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewSamples.aspx.cs" Inherits="RWInbound2.Samples.NewSamples" %>
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
                     <asp:TextBox ID="tbOrg" runat="server" ></asp:TextBox>

                <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                     TargetControlID="tbOrg"
                     CompletionSetCount="10"
                     ServiceMethod="SearchOrgs"
                     MinimumPrefixLength="2"

                    
                    
                    
                    >




                </ajaxToolkit:AutoCompleteExtender>


<%--    ServiceMethod="SearchOrgs"
        TargetControlID="tbOrg"
        MinimumPrefixLength="2"
        CompletionInterval="100"
        EnableCaching="false"
        CompletionSetCount="10">--%>


            </td>
            <td style="width: 116px">

                <asp:Button ID="btnSiteNumber" runat="server" Text="Select" CssClass="smallButton" OnClick="btnSiteNumber_Click" />
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
                            <asp:TextBox ID="txtDateCollected"  BorderColor="#66CCFF" BorderStyle="Solid" BorderWidth="1px" runat="server" Width="80px"></asp:TextBox>

                            <ajaxToolkit:CalendarExtender ID="txtDateCollected_CalendarExtender" runat="server" BehaviorID="txtDateCollected_CalendarExtender" TargetControlID="txtDateCollected"></ajaxToolkit:CalendarExtender>
                            <td>
                                <asp:Label ID="lblCollectionTime" runat="server" Text="Time: "></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtTimeCollected" runat="server"  BorderColor="#66CCFF" BorderStyle="Solid" BorderWidth="1px" Width="60px" ToolTip="Enter time as hh:mm and A or P for AM or PM"></asp:TextBox>
                            <ajaxToolkit:MaskedEditExtender ID="txtTimeCollected_MaskedEditExtender" runat="server" BehaviorID="txtTimeCollected_MaskedEditExtender" Century="2000"
                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                CultureThousandsPlaceholder="" CultureTimePlaceholder="" TargetControlID="txtTimeCollected" MaskType="Time" Mask="99:99" AcceptAMPM="false" />
                            <br>
                        </td>
                        <td>
                            <asp:Button ID="btnCreate" runat="server" Text="Create -&gt;" CausesValidation="False" CssClass="smallButton" OnClick="btnCreate_Click"></asp:Button></td>
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
                        <td>

                        </td>
                    </tr>
                    </table>
            </td>
        </tr>
    </table>
            </asp:Panel>

    <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" UseVerticalStripPlacement="False">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        
        <ajaxToolkit:TabPanel   runat="server" HeaderText="Sample" ID="TabPanelSample">
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

                            <asp:ListBox ID="lstSamples" runat="server" AutoPostBack="True" Height="153px" Rows="7" CssClass="ListboxSample" OnSelectedIndexChanged="lstSamples_SelectedIndexChanged"></asp:ListBox>

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

                <asp:Button ID="btnSaveSample" runat="server" CssClass="smallButton" Text="Save Sample" OnClick="btnSaveSample_Click" Width="80px" />

            </ContentTemplate>
        </ajaxToolkit:TabPanel>


&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        <ajaxToolkit:TabPanel runat="server" HeaderText="Metals Barcodes"  ID="TabPanel2">
            <ContentTemplate>
                <table>
                    <tr>
                        <td style="width: 133px">Bar Code / Lab ID:</td>
                        <td>
                            <asp:TextBox ID="tbBarcode" runat="server"></asp:TextBox>               

                            <asp:Button ID="btnCheckBarcode" OnClick="btnCheckBarcode_Click" runat="server"  Text="Check" CssClass="smallButton" />
                            <asp:Label ID="lblBarcodeUsed" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 133px">Type:</td>
                        <td>
                            <asp:RadioButtonList ID="rbListSampleTypes"  runat="server" EnableTheming="True" RepeatColumns="3">
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

                <asp:Button ID="btnMetalsSave" runat="server" Text="Save" OnClick="btnSaveMetals_Click" CssClass="smallButton" />

                <asp:Label ID="lblCodeInUse" runat="server" Text=""></asp:Label>

<%--                add a new grid to show existing bar codes--%>

                <asp:Panel ID="pnlBarcodeTab"  CssClass="pnlBarcodeTab" runat="server"  >
                    <asp:GridView ID="GridViewBarCodes"   runat="server" CellPadding="2"></asp:GridView>

                </asp:Panel>

            </ContentTemplate>
        </ajaxToolkit:TabPanel>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        <ajaxToolkit:TabPanel runat="server" HeaderText="Nutrient Barcodes" ID="TabPanel3">
            <ContentTemplate>
                <table>
                    <tr>                        
                        <td style="width: 141px">BarCode / Lab ID:</td>
                        <td>
                            <asp:TextBox ID="tbNutrientCode" runat="server"></asp:TextBox>&nbsp;
						<asp:Button ID="btnBarcodeSearch" runat="server"  OnClick="btnBarcodeSearch_Click" CssClass="smallButton" Text="Search"></asp:Button>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblNutBarcodeMsg" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                    
					<TD style="WIDTH: 221px" colSpan="2" >
						<asp:CheckBoxList id="chksNutrients"  runat="server" Width="432px" RepeatColumns="3">
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
						</asp:CheckBoxList></TD>
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
                        <ajaxToolkit:CalendarExtender ID="CalendarExtenderAnalyzeDate" TargetControlID="tbAnalyzeDate"   runat="server" />
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
                            <asp:TextBox ID="tbComments"  runat="server" TextMode="MultiLine" Width="420px"></asp:TextBox>

                        </td>
                            
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnSaveNutrient" runat="server" OnClick="btnSaveNutrient_Click" CssClass="smallButton" Text="Save" Width="60px" />
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
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <ajaxToolkit:TabPanel runat="server"  HeaderText="Update Org" ID="TabPanel4">
            <ContentTemplate>
                <div runat="server" id="divSizer" style="border-left:12px">
               <div >
                   <asp:Label ID="Label3" runat="server" Text="Samples Collected:"></asp:Label>
        <table style="height: 102px" >
            <tr>
                <td>
                    <table border="0"    title="Samples Collected:">                      
                        <tr> 
                            <td>Jan</td>
                            <td>Feb</td>
                            <td>Mar</td>
                            <td>Apr</td>
                            <td>May</td>
                            <td>Jun</td>
                        </tr>
                        <tr>
                          
                            <td>
                                <asp:TextBox ID="tbJan" runat="server" Width="55px" Wrap="False"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="tbFeb" runat="server" Width="55px" Wrap="False"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="tbMar" runat="server" Width="55px" Wrap="False"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="tbApr" runat="server" Width="55px" Wrap="False"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="tbMay" runat="server" Width="55px" Wrap="False"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="tbJune" runat="server" Width="55px" Wrap="False"></asp:TextBox></td>

                        </tr>

                        <tr>                
                            <td>July</td>
                            <td>Aug</td>
                            <td>Sept</td>
                            <td>Oct</td>
                            <td>Nov</td>
                            <td>Dec</td>
                        </tr>
                        <tr>
               
                            <td>
                                <asp:TextBox ID="tbJuly" runat="server" Width="55px"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="tbAug" runat="server" Width="55px"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="tbSept" runat="server" Width="55px"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="tbOct" runat="server" Width="55px"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="tbNov" runat="server" Width="55px"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="tbDec" runat="server" Width="55px"></asp:TextBox></td>
                        </tr>
                    </table>

                </td>

            </tr>

        </table>
    </div>

    <table>
        <tr>
            <td style="width: 140px">
                <asp:CheckBox ID="chkVisit" runat="server" Width="157px" Text=" Site Visited"></asp:CheckBox></td>
            <td class="align:left">
                <asp:CheckBox ID="chkSigned" runat="server" Text="Contract Signed"></asp:CheckBox>
            </td>
            <td style="padding-left: 12px">
                <asp:Label ID="Label2" runat="server" Text="Signed Date: "></asp:Label>
            </td>
            <td>

                <asp:TextBox ID="tbSignedDate" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 140px">
                <asp:CheckBox ID="chkNut1" runat="server" Text=" 1 Nutrient"></asp:CheckBox></td>
            <td> <asp:CheckBox ID="chkNut2" runat="server" Text=" 2 Nutrients"></asp:CheckBox>

            <td></td>
            <td></td>
        </tr>
        <tr>          
            <td colspan="3" rowspan="3">
                <table border="0">
                    <tr>
                        <td align="right">Quarter Shipped:</td>
                        <td colspan="2">
                            <asp:CheckBoxList ID="cblSample" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1">1st</asp:ListItem>
                                <asp:ListItem Value="2">2nd</asp:ListItem>
                                <asp:ListItem Value="3">3rd</asp:ListItem>
                                <asp:ListItem Value="4">4th</asp:ListItem>
                            </asp:CheckBoxList></td>
                    </tr>
                    <tr>
                        <td align="right">Volunteer TimeSheet:</td>
                        <td colspan="2">
                            <asp:CheckBoxList ID="cblVolTimsheet" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1">1st</asp:ListItem>
                                <asp:ListItem Value="2">2nd</asp:ListItem>
                                <asp:ListItem Value="3">3rd</asp:ListItem>
                                <asp:ListItem Value="4">4th</asp:ListItem>
                            </asp:CheckBoxList></td>
                    </tr>
                    <tr>
                        <td align="right">Data Electronic:</td>
                        <td colspan="2">
                            <asp:CheckBoxList ID="cblData" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1">1st</asp:ListItem>
                                <asp:ListItem Value="2">2nd</asp:ListItem>
                                <asp:ListItem Value="3">3rd</asp:ListItem>
                                <asp:ListItem Value="4">4th</asp:ListItem>
                            </asp:CheckBoxList></td>
                    </tr>
                </table>
            </td>

        </tr>
        </table>
    <table>
        <tr>
            <td style="width: 140px">Unknown 1 Date:
            </td>
            <td>
                <asp:TextBox ID="tbUnknown1" runat="server" Width="80px"></asp:TextBox>
            </td>

            <td style="width: 140px">Unknown 2 Date: 
            </td>
            <td>
                <asp:TextBox ID="txtUnk2" runat="server" Width="80px"></asp:TextBox>
            </td>
           
        </tr>

        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Total Duplicates: "></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tbDupCount" runat="server" Width="37px"></asp:TextBox>
            </td>

            <td>
                <asp:Label ID="Label4" runat="server" Text="Total Blanks: "></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="tbBlankCount" runat="server" Width="37px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:CheckBox ID="chkBug" runat="server" Text="Bug"></asp:CheckBox>
            </td>
            
        </tr>
        <tr>
         
            <td  style="width: 140px">Hardship:</td>
            <td colspan="4">
                <asp:TextBox ID="txtHard" runat="server" Width="677px" TextMode="MultiLine"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 140px; height: 16px" >Troubleshoot:</td>
            <td style="height: 16px" colspan="4">
                <asp:TextBox ID="txtTrouble" runat="server" Width="677px" TextMode="MultiLine"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 140px">Notes:</td>
            <td colspan="4">
                <asp:TextBox ID="txtNote" runat="server" Width="677px" TextMode="MultiLine"></asp:TextBox></td>
        </tr>

        <tr>

            <td style="width: 140px">
                <asp:Button CssClass="smallButton" ID="cmdSaveAll" runat="server" Text="Save" CausesValidation="False"></asp:Button></td>
        </tr>
    </table>
                    </div>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <ajaxToolkit:TabPanel  runat="server"   HeaderText="Create ICP Data" ID="TabPanelICPdata">
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

                <asp:Button ID="btnICPSave" runat="server" Text="Save" CssClass="smallButton" OnClick="btnICPSave_Click" />

            </ContentTemplate>
        </ajaxToolkit:TabPanel>


&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <%-- &nbsp;&nbsp;&nbsp;--%>
&nbsp;&nbsp;&nbsp; </ajaxToolkit:TabContainer>

</asp:Content>
