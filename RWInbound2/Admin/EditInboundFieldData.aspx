<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditInboundFieldData.aspx.cs" Inherits="RWInbound2.Admin.EditInboundFieldData" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label1" runat="server" CssClass="PageLabel" Text="View - Delete Inbound Field Data"></asp:Label>

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
                <asp:Button ID="btnSelectOrg" runat="server" Text="Select" OnClick="btnSelectOrg_Click" />
            </td>
        </tr>
    </table>

    <br />
    <asp:Label ID="lblNumberLeft" runat="server"></asp:Label>
<%--    AutoGenerateColumns="False"   InsertVisible="False"  --%>
    <br />
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
    <br />
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="SqlDataSource1" Width="1000px">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ButtonType="Button" />
            <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="ID" />
            <asp:BoundField DataField="StationNum" HeaderText="StationNum" SortExpression="StationNum" />
            <asp:BoundField DataField="Sample#" HeaderText="SampleID" SortExpression="SampleID" />
            <asp:BoundField DataField="txtSampleID" HeaderText="txtSampleID" SortExpression="txtSampleID" />
            <asp:BoundField DataField="KitNum" HeaderText="KitNum" SortExpression="KitNum" />
            <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
            <asp:BoundField DataField="Time" HeaderText="Time" SortExpression="Time" />
            <asp:BoundField DataField="USGSFlow" HeaderText="USGSFlow" SortExpression="USGSFlow" />
            <asp:BoundField DataField="PH" HeaderText="PH" SortExpression="PH" />
            <asp:BoundField DataField="TempC" HeaderText="TempC" SortExpression="TempC" />
            <asp:BoundField DataField="PhenAlk" HeaderText="PhenAlk" SortExpression="PhenAlk" />
            <asp:BoundField DataField="TotalAlk" HeaderText="TotalAlk" SortExpression="TotalAlk" />
            <asp:BoundField DataField="TotalHard" HeaderText="TotalHard" SortExpression="TotalHard" />
            <asp:BoundField DataField="DO" HeaderText="DO" SortExpression="DO" />
            <asp:BoundField DataField="DOsat" HeaderText="DOsat" SortExpression="DOsat" />
            <asp:BoundField DataField="Tag" HeaderText="Tag" SortExpression="Tag" />
            <asp:BoundField DataField="Chk" HeaderText="Chk" SortExpression="Chk" />
            <asp:BoundField DataField="EntryType" HeaderText="EntryType" SortExpression="EntryType" />
            <asp:BoundField DataField="EntryStaff" HeaderText="EntryStaff" SortExpression="EntryStaff" />
            <asp:CheckBoxField DataField="MetalsNormal" HeaderText="MetalsNormal" SortExpression="MetalsNormal" />
            <asp:CheckBoxField DataField="MetalsBlnk" HeaderText="MetalsBlnk" SortExpression="MetalsBlnk" />
            <asp:CheckBoxField DataField="MetalsDupe" HeaderText="MetalsDupe" SortExpression="MetalsDupe" />
            <asp:CheckBoxField DataField="Bugs" HeaderText="Bugs" SortExpression="Bugs" />
            <asp:CheckBoxField DataField="BugsQA" HeaderText="BugsQA" SortExpression="BugsQA" />
            <asp:CheckBoxField DataField="TSS" HeaderText="TSS" SortExpression="TSS" />
            <asp:CheckBoxField DataField="CS" HeaderText="CS" SortExpression="CS" />
            <asp:CheckBoxField DataField="NP" HeaderText="NP" SortExpression="NP" />
            <asp:CheckBoxField DataField="TSSDupe" HeaderText="TSSDupe" SortExpression="TSSDupe" />
            <asp:CheckBoxField DataField="CSDupe" HeaderText="CSDupe" SortExpression="CSDupe" />
            <asp:CheckBoxField DataField="NPDupe" HeaderText="NPDupe" SortExpression="NPDupe" />
            <asp:CheckBoxField DataField="FieldValid" HeaderText="FieldValid" SortExpression="FieldValid" />
            <asp:BoundField DataField="MetalsStat" HeaderText="MetalsStat" SortExpression="MetalsStat" />
            <asp:CheckBoxField DataField="FinalCheck" HeaderText="FinalCheck" SortExpression="FinalCheck" />
            <asp:BoundField DataField="Method" HeaderText="Method" SortExpression="Method" />
            <asp:BoundField DataField="Comments" HeaderText="Comments" SortExpression="Comments" />
            <asp:BoundField DataField="DateReceived" HeaderText="DateReceived" SortExpression="DateReceived" />
            <asp:CheckBoxField DataField="DataSheetIncluded" HeaderText="DataSheetIncluded" SortExpression="DataSheetIncluded" />
            <asp:BoundField DataField="MissingDataSheetReqDate" HeaderText="MissingDataSheetReqDate" SortExpression="MissingDataSheetReqDate" />
            <asp:CheckBoxField DataField="ChainOfCustody" HeaderText="ChainOfCustody" SortExpression="ChainOfCustody" />
            <asp:CheckBoxField DataField="MissingDataSheetReceived" HeaderText="MissingDataSheetReceived" SortExpression="MissingDataSheetReceived" />
            <asp:BoundField DataField="PassValStep" HeaderText="PassValStep" SortExpression="PassValStep" />
            <asp:BoundField DataField="tblSampleID" HeaderText="tblSampleID" SortExpression="tblSampleID" />
            <asp:CheckBoxField DataField="Valid" HeaderText="Valid" SortExpression="Valid" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RiverWatchDEV %>" 
        DeleteCommand="DELETE FROM [InboundSamples] WHERE [ID] = @ID" 
        InsertCommand="INSERT INTO [InboundSamples] ([StationNum], [SampleID], [txtSampleID], [KitNum], [Date], [Time], [USGSFlow], [PH], [TempC], [PhenAlk], [TotalAlk], [TotalHard], [DO], [DOsat], [Tag], [Chk], [EntryType], [EntryStaff], [MetalsNormal], [MetalsBlnk], [MetalsDupe], [Bugs], [BugsQA], [TSS], [CS], [NP], [TSSDupe], [CSDupe], [NPDupe], [FieldValid], [MetalsStat], [FinalCheck], [Method], [Comments], [DateReceived], [DataSheetIncluded], [MissingDataSheetReqDate], [ChainOfCustody], [MissingDataSheetReceived], [PassValStep], [tblSampleID], [Valid]) VALUES (@StationNum, @SampleID, @txtSampleID, @KitNum, @Date, @Time, @USGSFlow, @PH, @TempC, @PhenAlk, @TotalAlk, @TotalHard, @DO, @DOsat, @Tag, @Chk, @EntryType, @EntryStaff, @MetalsNormal, @MetalsBlnk, @MetalsDupe, @Bugs, @BugsQA, @TSS, @CS, @NP, @TSSDupe, @CSDupe, @NPDupe, @FieldValid, @MetalsStat, @FinalCheck, @Method, @Comments, @DateReceived, @DataSheetIncluded, @MissingDataSheetReqDate, @ChainOfCustody, @MissingDataSheetReceived, @PassValStep, @tblSampleID, @Valid)" 

        UpdateCommand="UPDATE [InboundSamples] SET [StationNum] = @StationNum, [SampleID] = @SampleID, [txtSampleID] = @txtSampleID, [KitNum] = @KitNum, [Date] = @Date, [Time] = @Time, [USGSFlow] = @USGSFlow, [PH] = @PH, [TempC] = @TempC, [PhenAlk] = @PhenAlk, [TotalAlk] = @TotalAlk, [TotalHard] = @TotalHard, [DO] = @DO, [DOsat] = @DOsat, [Tag] = @Tag, [Chk] = @Chk, [EntryType] = @EntryType, [EntryStaff] = @EntryStaff, [MetalsNormal] = @MetalsNormal, [MetalsBlnk] = @MetalsBlnk, [MetalsDupe] = @MetalsDupe, [Bugs] = @Bugs, [BugsQA] = @BugsQA, [TSS] = @TSS, [CS] = @CS, [NP] = @NP, [TSSDupe] = @TSSDupe, [CSDupe] = @CSDupe, [NPDupe] = @NPDupe, [FieldValid] = @FieldValid, [MetalsStat] = @MetalsStat, [FinalCheck] = @FinalCheck, [Method] = @Method, [Comments] = @Comments, [DateReceived] = @DateReceived, [DataSheetIncluded] = @DataSheetIncluded, [MissingDataSheetReqDate] = @MissingDataSheetReqDate, [ChainOfCustody] = @ChainOfCustody, [MissingDataSheetReceived] = @MissingDataSheetReceived, [PassValStep] = @PassValStep, [tblSampleID] = @tblSampleID, [Valid] = @Valid WHERE [ID] = @ID">
        <DeleteParameters>
            <asp:Parameter Name="ID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="StationNum" Type="Int32" />
            <asp:Parameter Name="SampleID" Type="String" />
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
            <asp:Parameter Name="Valid" Type="Boolean" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="StationNum" Type="Int32" />
            <asp:Parameter Name="SampleID" Type="String" />
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
            <asp:Parameter Name="Valid" Type="Boolean" />
            <asp:Parameter Name="ID" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <br />
</asp:Content>
