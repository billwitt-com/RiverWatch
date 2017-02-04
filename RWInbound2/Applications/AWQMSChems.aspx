<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AWQMSChems.aspx.cs" Inherits="RWInbound2.Applications.AWQMSChems" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Label ID="Label1" runat="server" CssClass="PageLabel" Text="AWQMS Metals and Nutrients Download"></asp:Label>
    <br />

    <table style="width: 100%">
        <tr>
            <td style="width: 36px">&nbsp;</td>
            <td style="width: 222px">
                <asp:Label ID="Label2" runat="server" CssClass="label" Text="Start Date"></asp:Label>
            </td>
            <td style="width: 219px">
                <asp:Label ID="Label3" runat="server" CssClass="label"  Text="End Date"></asp:Label>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 36px">&nbsp;</td>
            <td style="width: 222px">
                <asp:TextBox ID="tbStartDate" BorderColor="#66CCFF" BorderStyle="Solid" BorderWidth="1px" Width="80px" runat="server"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="tbStartDate_CalendarExtender"  runat="server"  TargetControlID="tbStartDate" />
            </td>
            <td style="width: 219px">
                <asp:TextBox ID="tbEndDate" BorderColor="#66CCFF" BorderStyle="Solid" BorderWidth="1px" runat="server" Width="80px"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="tbEndDate_CalendarExtender" BehaviorID="tbEndDate_CalendarExtender" Animated="true"   runat="server" TargetControlID="tbEndDate" />
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 36px">&nbsp;</td>
            <td style="width: 222px">
                <asp:Label ID="Label4" runat="server" CssClass="label"  Text="Select first 4 of WBID"></asp:Label>
            </td>
            <td style="width: 219px">
                <asp:TextBox ID="tbWBID4" runat="server"></asp:TextBox>
                <ajaxToolkit:AutoCompleteExtender ID="tbWBID4_AutoCompleteExtender"
                    ServiceMethod="SearchWBID4"
                    CompletionSetCount="2" MinimumPrefixLength="2"
                    runat="server" TargetControlID="tbWBID4">
                </ajaxToolkit:AutoCompleteExtender>
            </td>
            <td>
                <asp:Button ID="btnSelect4" runat="server" CssClass="adminButton" Text="WBID '4' Select " OnClick="btnSelect4_Click" />
            </td>
        </tr>
        <tr>
            <td style="width: 36px">&nbsp;</td>
            <td style="width: 222px">
                <asp:Label ID="Label5" CssClass="label"  runat="server" Text="Select first 6 of WBID"></asp:Label>
            </td>
            <td style="width: 219px">
                <asp:TextBox ID="tbWBID6" runat="server"></asp:TextBox>
                <ajaxToolkit:AutoCompleteExtender 
                                        ServiceMethod="SearchWBID6"
                    CompletionSetCount="2" MinimumPrefixLength="2"
                    
                    ID="tbSelect6_AutoCompleteExtender" runat="server" 
                    TargetControlID="tbWBID6">
                </ajaxToolkit:AutoCompleteExtender>
            </td>
            <td>
                <asp:Button ID="btnSelect6" runat="server" CssClass="adminButton" Text="WBID '6' Select " OnClick="btnSelect6_Click" />
            </td>
        </tr>
        <tr>
            <td style="width: 36px">&nbsp;</td>
            <td style="width: 222px">
                <asp:Label ID="Label6" runat="server" CssClass="label"  Text="Select all 8+ of WBID"></asp:Label>
            </td>
            <td style="width: 219px">
                <asp:TextBox ID="tbWBID8" runat="server"></asp:TextBox>
                <ajaxToolkit:AutoCompleteExtender 
                                        ServiceMethod="SearchWBID8"
                    CompletionSetCount="2" MinimumPrefixLength="2"
                    ID="tbSelect8_AutoCompleteExtender" runat="server" TargetControlID="tbWBID8">
                </ajaxToolkit:AutoCompleteExtender>
            </td>
            <td>
                <asp:Button ID="btnSelect8" runat="server" CssClass="adminButton" Text="WBID '8' Select " OnClick="btnSelect8_Click" />
            </td>
        </tr>
        <tr>
            <td style="width: 36px">&nbsp;</td>
            <td style="width: 222px">&nbsp;</td>
            <td style="width: 219px">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>

    <asp:TextBox ID="tbResults" runat="server" ReadOnly="True" Font-Bold="True" Height="20px" Width="966px" ></asp:TextBox>



</asp:Content>
