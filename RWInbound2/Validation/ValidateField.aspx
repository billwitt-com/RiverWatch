<%@ Page Title="" MaintainScrollPositionOnPostback="true"  Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ValidateField.aspx.cs" Inherits="RWInbound2.Validation.ValidateField" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label1" runat="server"  CssClass="PageLabel" Text=" Validate Field Data"></asp:Label>

    <br />
    <table>
        <tr>
            <td style="width: 387px">
     <asp:Label ID="Label2" runat="server" Text="Select by Organization: "></asp:Label>
    <asp:TextBox ID="tbOrgName" BorderColor="#66CCFF" BorderStyle="Solid" BorderWidth="1px" Height="20px" Width="202px" runat="server"></asp:TextBox>
        <ajaxToolkit:AutoCompleteExtender ID="tbOrgName_AutoCompleteExtender" runat="server" 
             ServiceMethod="SearchOrgs" 
            CompletionSetCount="2" MinimumPrefixLength="2"
            TargetControlID="tbOrgName">
        </ajaxToolkit:AutoCompleteExtender>

            </td>
<td style="width: 184px">
    <asp:Label ID="Label4" runat="server" Text=" OR Kit Number: "></asp:Label>
                 <asp:TextBox ID="tbKitNumber" runat="server" BorderColor="#66CCFF" BorderStyle="Solid" BorderWidth="1px" Height="20px" Width="46px"></asp:TextBox>
    </td>
          <td>

    <asp:Button ID="btnSelectOrg" runat="server" Text="Select" OnClick="btnSelectOrg_Click"/>

          </td>
            </tr>
        </table>
&nbsp;
    <br />
    <asp:Label ID="lblNumberLeft" runat="server"></asp:Label>
   
        <br />
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
   
        <br />
   
    <br />
    <asp:FormView ID="FormView1" runat="server"  OnDataBound="FormView1_DataBound" DefaultMode="Edit" AllowPaging="True" DataKeyNames="ID" DataSourceID="SqlDataSource1"
        Width="808px">
        <EditItemTemplate>
            <table>
                <tr>
                    <td>StationNum:
                    </td>
                    <td>
                        <asp:TextBox ID="StationNumTextBox" runat="server" Text='<%# Bind("StationNum") %>' />
                    </td>
                </tr>

                <%--             txtSampleID:
            <asp:TextBox ID="txtSampleIDTextBox" runat="server" ReadOnly="true" Text='<%# Bind("txtSampleID") %>' />
            <br />--%>
                <tr>
            <td>
            KitNum:</td>
           <td> <asp:TextBox ID="KitNumTextBox" runat="server" Text='<%# Bind("KitNum") %>' />
               </td>
                </tr>
                <tr>
                    <td>Sample Date:
                    </td>
                    <td>
                        <asp:TextBox ID="DateTextBox" runat="server" Text='<%# Bind("Date", "{0:d}") %>' />
                    </td>
                    <td>

                    </td>
                </tr>

                <tr>
                    <td>Sample Time (24 Hr HH:MM):
                    </td>
                    <td>
                        <asp:TextBox ID="TimeTextBox" runat="server" Text='<%# Bind("Time") %>' />

                    </td>
                   <td>

                    </td>
                </tr>

                <tr>
                    <td>USGSFlow (CFSecond):               

                    </td>
                    <td>
                        <asp:TextBox ID="USGSFlowTextBox" runat="server" Text='<%# Bind("USGSFlow") %>' />


                     
                    </td>
                    <td>
                                                <asp:CheckBox ID="FinalCheckCheckBox" runat="server" Checked='<%# Bind("FinalCheck") %>' />
                           <asp:Label ID="Label3" runat="server" ForeColor="Red" Font-Bold="true" Text="CHECK IF IS GUAGE DATA: "></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Temp (In CENTEGRADE):
                    </td>
                    <td>
                        <asp:TextBox ID="TempCTextBox" runat="server"  OnTextChanged="TextBox_TextChanged" AutoPostBack="True"  Text='<%# Bind("TempC") %>' />
                    </td>
                    <td>
                        <asp:Label ID="lblTempC" runat="server" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>PH:
                    </td>
                    <td>
                        <asp:TextBox ID="PHTextBox" OnTextChanged="TextBox_TextChanged" runat="server" Text='<%# Bind("PH") %>' AutoPostBack="True" />

                    </td>
                    <td>
                        <asp:Label ID="lblpH" runat="server" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Alkalinity mg/L: Phenol = 
                    </td>
                    <td>
                            <asp:TextBox ID="PhenAlkTextBox" OnTextChanged="TextBox_TextChanged" AutoPostBack="True"  runat="server" Text='<%# Bind("PhenAlk") %>'  />
                    </td>
                    <td>
                        <asp:Label ID="lblPhenol" Text="" runat="server" ></asp:Label>
                    </td>
                </tr>
                <tr>
                <tr>
                    <td>Alkalinity mg/L: Total = 
                    </td>
                    <td>
                            <asp:TextBox ID="AlkTotalTextBox" OnTextChanged="TextBox_TextChanged" AutoPostBack="True" runat="server" Text='<%# Bind("TotalAlk") %>' />
                    </td>
                    <td>
                        <asp:Label ID="lblAlkTotal" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>TotalHard mg/L:
                    </td>
                    <td>
                        <asp:TextBox ID="TotalHardTextBox" OnTextChanged="TextBox_TextChanged" AutoPostBack="True" runat="server" Text='<%# Bind("TotalHard") %>' />
                    </td>
                    <td>
                        <asp:Label ID="lblHardness" runat="server" Text=""></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td>Dissolved Oxygen: mg/L = 
                    </td>
                    <td>
                                <asp:TextBox ID="DOTextBox" OnTextChanged="TextBox_TextChanged" AutoPostBack="True" runat="server" Text='<%# Bind("DO") %>' />

                    </td>
                    <td>
                        <asp:Label ID="lblDO" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Dissolved Oxygen: % Saturation =
                    </td>
                    <td> 
                                <asp:TextBox ID="DOSatTextBox" OnTextChanged="TextBox_TextChanged" AutoPostBack="True" runat="server" Text='<%# Bind("DOsat") %>' />
                    </td>
                    <td>
                        <asp:Label ID="lblDoSat" runat="server" ></asp:Label>
                    </td>
                </tr>

            </table>

            <table>
                <tr>
                    <td>Method:

                    </td>
                    <td>
                        <asp:TextBox ID="MethodTextBox" runat="server" Text='<%# Bind("Method") %>' />
                    </td>
                </tr>

                <tr>
                    <td>DateReceived:

                    </td>
                    <td>
                        <asp:TextBox ID="DateReceivedTextBox" runat="server" Text='<%# Bind("DateReceived") %>' />
                    </td>
                </tr>
                <tr>
                    <td>
                        DataSheetIncluded:
                    </td>
                    <td>
                        <asp:CheckBox ID="DataSheetIncludedCheckBox" runat="server" Checked='<%# Bind("DataSheetIncluded") %>' /> 
                    </td>
                </tr>
                <tr>
                    <td>MissingDataSheetReqDate:

                    </td>
                    <td>
                        <asp:TextBox ID="MissingDataSheetReqDateTextBox" runat="server" Text='<%# Bind("MissingDataSheetReqDate") %>' />
                    </td>
                    <tr>
                        <td>ChainOfCustody:

                        </td>
                        <td>
                            <asp:CheckBox ID="ChainOfCustodyCheckBox" runat="server" Checked='<%# Bind("ChainOfCustody") %>' />
                        </td>
                    </tr>
                <tr>
                    <td>MissingDataSheetReceived:

                    </td>
                    <td>
                        <asp:CheckBox ID="MissingDataSheetReceivedCheckBox" runat="server" Checked='<%# Bind("MissingDataSheetReceived") %>' />
                    </td>
                </tr>

                <tr>
                    <td>EntryType:

                    </td>
                    <td>
                        <asp:TextBox ID="EntryTypeTextBox" runat="server" Text='<%# Bind("EntryType") %>' />
                    </td>
                </tr>
                <tr>
                    <td>EntryStaff:

                    </td>
                    <td>
                        <asp:TextBox ID="EntryStaffTextBox" runat="server" Text='<%# Bind("EntryStaff") %>' />
                    </td>
                </tr>
                <tr>
                    <td>Comments:
 
                    </td>
                    <td>
                        <asp:TextBox ID="CommentsTextBox" runat="server" Text='<%# Bind("Comments") %>' TextMode="MultiLine" Width="360" Height="44" />
                    </td>
                </tr>
                <tr>
                    <td>SampleID:
                    </td>
                    <td>
                        <asp:TextBox ID="SampleIDTextBox" runat="server" ReadOnly="true" Text='<%# Bind("SampleID") %>' />
                    </td>

                </tr>



            </table>

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


            <%-- 

            FinalCheck: NOW USED AS FLAG FOR IS GUAGE FOR FLOW 
            <asp:CheckBox ID="FinalCheckCheckBox" runat="server" Checked='<%# Bind("FinalCheck") %>' />
     --%>
             

            <table>
                <tr>
                    <td style="width: 20px;"></td>
                    <td>
                        <asp:Button ID="UpdateButton" ClientIDMode="Static" runat="server" CausesValidation="True" CommandName="Update" Text="Validate" />
                        <asp:Button ID="btnBAD" ClientIDMode="Static" runat="server" OnClick="btnBAD_Click" CausesValidation="True" Text="BAD" />
                    </td>

                </tr>
            </table>
        </EditItemTemplate>

    </asp:FormView>

<%--            InsertCommand="INSERT INTO [InboundSamples] ([StationNum], [SampleID], [txtSampleID], [KitNum], [Date], [Time], [USGSFlow], [PH], [TempC], [PhenAlk], [TotalAlk], [TotalHard], [DO], [DOsat], [Tag], [Chk], [EntryType], [EntryStaff], [Metals], [MetalsBlnk], [MetalsDupe], [Bugs], [BugsQA], [TSS], [CS], [NP], [TSSDupe], [CSDupe], [NPDupe], [FieldValid], [MetalsStat], [FinalCheck], [Method], [Comments], [DateReceived], [DataSheetIncluded], [MissingDataSheetReqDate], [ChainOfCustody], [MissingDataSheetReceived], [PassValStep], [tblSampleID]) VALUES (@StationNum, @SampleID, @txtSampleID, @KitNum, @Date, @Time, @USGSFlow, @PH, @TempC, @PhenAlk, @TotalAlk, @TotalHard, @DO, @DOsat, @Tag, @Chk, @EntryType, @EntryStaff, @Metals, @MetalsBlnk, @MetalsDupe, @Bugs, @BugsQA, @TSS, @CS, @NP, @TSSDupe, @CSDupe, @NPDupe, @FieldValid, @MetalsStat, @FinalCheck, @Method, @Comments, @DateReceived, @DataSheetIncluded, @MissingDataSheetReqDate, @ChainOfCustody, @MissingDataSheetReceived, @PassValStep, @tblSampleID)"--%>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" OnUpdating="SqlDataSource1_Updating" ConnectionString="<%$ ConnectionStrings:RiverWatchDev %>"
        DeleteCommand="DELETE FROM [InboundSamples] WHERE [inbSampleID] = @inbSampleID"
        SelectCommand="SELECT * FROM [InboundSamples] order by date desc"
        UpdateCommand="UPDATE [InboundSamples] SET [StationNum] = @StationNum, [SampleID] = @SampleID, [txtSampleID] = @txtSampleID, [KitNum] = @KitNum, [Date] = @Date, [Time] = @Time, [USGSFlow] = @USGSFlow, [PH] = @PH, [TempC] = @TempC, [PhenAlk] = @PhenAlk, [TotalAlk] = @TotalAlk, [TotalHard] = @TotalHard, [DO] = @DO, [DOsat] = @DOsat, [Tag] = @Tag, [Chk] = @Chk, [EntryType] = @EntryType, [EntryStaff] = @EntryStaff, [MetalsNormal] = @MetalsNormal, [MetalsBlnk] = @MetalsBlnk, [MetalsDupe] = @MetalsDupe, [Bugs] = @Bugs, [BugsQA] = @BugsQA, [TSS] = @TSS, [CS] = @CS, [NP] = @NP, [TSSDupe] = @TSSDupe, [CSDupe] = @CSDupe, [NPDupe] = @NPDupe, [FieldValid] = @FieldValid, [MetalsStat] = @MetalsStat, [FinalCheck] = @FinalCheck, [Method] = @Method, [Comments] = @Comments, [DateReceived] = @DateReceived, [DataSheetIncluded] = @DataSheetIncluded, [MissingDataSheetReqDate] = @MissingDataSheetReqDate, [ChainOfCustody] = @ChainOfCustody, [MissingDataSheetReceived] = @MissingDataSheetReceived, [PassValStep] = @PassValStep, [tblSampleID] = @tblSampleID WHERE [inbSampleID] = @inbSampleID">
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

