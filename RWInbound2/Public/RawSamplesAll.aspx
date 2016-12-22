<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RawSamplesAll.aspx.cs" Inherits="RWInbound2.Public.RawSamplesAll" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

        <br />
    <asp:Label ID="Label1" runat="server"  CssClass="PageLabel" Text="Sample Data - ALL"></asp:Label>  
    <br />
        <div class="label-placement">
        <asp:Label ID="ErrorLabel" CssClass="label-error" runat="server" />               
    </div>
    <div class="label-placement">
            <asp:Label ID="SuccessLabel" CssClass="label-success" runat="server" />
    </div>
    Select one option below to subset export, multiple selections must align (station must be in WS, etc.):<br />
    <table>

        <tr>
            <td style="width: 8px; height: 10px"></td>
            <td style="width: 190px; height: 10px">River:</td>
            <td style="height: 10px">
                <asp:DropDownList ID="ddlRiver" runat="server" Width="352px"></asp:DropDownList></td>
        </tr>
        <tr>
            <td style="width: 8px"></td>
            <td style="width: 190px">RiverWatch Watershed:</td>
            <td>
                <asp:DropDownList ID="ddlRWWaterShed" runat="server" Width="352px"></asp:DropDownList></td>
        </tr>
                                <tr>
                <td style="width: 8px"></td>
                <td style="width: 190px">WQCC Watershed:</td>
                <td>
                    <asp:DropDownList ID="ddlWQCCWaterShed" runat="server" Width="352px" Height="22px"></asp:DropDownList></td>
            </tr>
                <tr>
                <td style="width: 8px; height: 32px"></td>
                <td style="height: 32px; width: 190px;">Water Code:</td>
                <td style="height: 32px">
                    <asp:DropDownList ID="ddlWaterCode"  runat="server" Width="120px" Height="22px"></asp:DropDownList></td>
            </tr>
        <tr>
                <td style="width: 8px; height: 18px"></td>
                <td style="width: 190px; height: 18px">Water Body ID:</td>
                <td style="height: 18px">
                    <asp:DropDownList ID="ddlWaterBodyID" runat="server" Width="352px"  Height="22px"></asp:DropDownList>&nbsp;
                </td>
            </tr>
                <tr>
                <td style="width: 8px"></td>
                <td style="width: 190px">Watershed Report:</td>
                <td>
                    <asp:DropDownList ID="ddlWSR" runat="server"  Width="152px"></asp:DropDownList></td>
            </tr>

            <tr>
                <td style="width: 8px; height: 12px"></td>
                <td style="width: 190px; height: 12px">County:</td>
                <td style="height: 12px">
                    <asp:DropDownList ID="ddlCounty" runat="server" Width="352px"></asp:DropDownList></td>
            </tr>
                    <tr>
                <td style="width: 8px; height: 12px"></td>
                <td style="width: 190px; height: 12px">Project:</td>
                <td style="height: 12px">
                    <asp:DropDownList ID="ddlProject" runat="server" Width="352px"></asp:DropDownList></td>
            </tr>
        <tr>
            <td style="width: 8px"></td>
            <td style="width: 190px">Organization Name:</td>
            <td>
                <asp:TextBox ID="tbOrgName" runat="server"></asp:TextBox>
                 <ajaxToolkit:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" WatermarkText="ALL "  runat="server" TargetControlID="tbOrgName" />

                  <ajaxToolkit:AutoCompleteExtender 
                    ID="tbSearch_AutoCompleteExtender" 
                    runat="server" 
                    BehaviorID="tbSearch_AutoCompleteExtender" 
                    DelimiterCharacters=""  
                    ServiceMethod="SearchOrgs"             
                    TargetControlID="tbOrgName"
                    MinimumPrefixLength="2"
                    CompletionInterval="100" 
                    EnableCaching="false" 
                    CompletionSetCount="10">
                </ajaxToolkit:AutoCompleteExtender> 
        </tr>
        <tr>
            <td style="width: 8px"></td>
            <td style="width: 190px">Kit Number: </td>
            <td>
                <asp:TextBox ID="tbKitNumber" runat="server"></asp:TextBox>
                <ajaxToolkit:TextBoxWatermarkExtender ID="tbKitNumber_TextBoxWatermarkExtender" WatermarkText="ALL "  runat="server" TargetControlID="tbKitNumber" />
        </tr>

        <tr>
            <td style="width: 8px"></td>
            <td style="width: 190px">Station Number: </td>
            <td>
                <asp:TextBox ID="tbStationNumber" runat="server"></asp:TextBox>
                <ajaxToolkit:TextBoxWatermarkExtender ID="tbStationNumber_TextBoxWatermarkExtender" WatermarkText="ALL " runat="server" TargetControlID="tbStationNumber" />
        </tr>
            </table>

        <table>

                    <tr>
            <td style="width: 8px; height: 12px"></td>
            <td style="width: 190px; height: 12px">
               
                <asp:Label ID="Label2" runat="server" Text="First Order By: "></asp:Label>
               
            <td style="height: 14px; width: 662px;">
            &nbsp;
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" BorderColor="White" BorderStyle="Solid" BorderWidth="3px" Width="698px">
                    <asp:ListItem>None</asp:ListItem>
                    <asp:ListItem>Sample Date</asp:ListItem>
                    <asp:ListItem>Water Shed</asp:ListItem>
                    <asp:ListItem>River Name</asp:ListItem>
                    <asp:ListItem>Org Name</asp:ListItem>
                    <asp:ListItem>Station Number</asp:ListItem>
                </asp:RadioButtonList>
                    </tr>

            <tr>
                <td style="width: 8px; height: 12px"></td>
                <td style="width: 190px; height: 12px">

                    <asp:Label ID="Label3" runat="server" Text="Second Order By: "></asp:Label>

                <td style="height: 14px; width: 662px;">&nbsp;
                <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatDirection="Horizontal" BorderColor="White" BorderStyle="Solid" BorderWidth="3px" Width="698px">
                    <asp:ListItem>None</asp:ListItem>
                    <asp:ListItem>Sample Date</asp:ListItem>
                    <asp:ListItem>Water Shed</asp:ListItem>
                    <asp:ListItem>River Name</asp:ListItem>
                    <asp:ListItem>Org Name</asp:ListItem>
                    <asp:ListItem>Station Number</asp:ListItem>
                </asp:RadioButtonList>
            </tr>
            <tr>
                <td style="width: 8px; height: 12px"></td>
                <td style="width: 190px; height: 12px">

                    <asp:Label ID="Label4" runat="server" Text="Third Order By: "></asp:Label>

                <td style="height: 14px; width: 662px;">&nbsp;
                <asp:RadioButtonList ID="RadioButtonList3" runat="server" RepeatDirection="Horizontal" BorderColor="White" BorderStyle="Solid" BorderWidth="3px" Width="698px">
                    <asp:ListItem>None</asp:ListItem>
                    <asp:ListItem>Sample Date</asp:ListItem>
                    <asp:ListItem>Water Shed</asp:ListItem>
                    <asp:ListItem>River Name</asp:ListItem>
                    <asp:ListItem>Org Name</asp:ListItem>
                    <asp:ListItem>Station Number</asp:ListItem>
                </asp:RadioButtonList>
            </tr>
        <tr>
            <td style="width: 8px; height: 12px"></td>
            <td style="width: 190px; height: 12px">
                <asp:Button ID="btnGO" runat="server" Text="SELECT" OnClick="btnGO_Click" />
            </td>
            <td style="height: 12px; width: 662px;">
            &nbsp;
        </tr>


</table>

</asp:Content>
