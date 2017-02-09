<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RawSamplesAll.aspx.cs" Inherits="RWInbound2.Public.RawSamplesAll" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

        <br />
        <table style="width: 100%">
            <tr>
                <td style="width: 137px">
    <asp:Label ID="Label1" runat="server" CssClass="PageLabel" Text="Sample Data"></asp:Label>

                </td>
                <td style="width: 258px"> 
                    <asp:Label ID="Label5" runat="server" Text="This FAQ tells you about RW data:"></asp:Label>
                    
                </td>
                <td>
                    <asp:ImageButton ID="btnFAQ" runat="server" BorderColor="#3399FF" BorderStyle="Double" Height="73px" ImageUrl="~/Content/faq.png" OnClick="btnFAQ_Click" Width="72px" />
                </td>
            </tr>
            <tr>
                <td style="width: 137px">&nbsp;</td>
                <td class="rowId-charid-dsorder-textbox" style="width: 258px">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>

    <asp:Label ID="MSGLabel" runat="server" />
                <br />

            <br />
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
                <td style="width: 8px"></td>
                <td style="width: 190px">Watershed Gathering:</td>
                <td>
                    <asp:DropDownList ID="ddlWSG" runat="server"  Width="152px"></asp:DropDownList></td>
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
                    <asp:ListItem Value="SampleDate desc">Sample Date</asp:ListItem>
                    <asp:ListItem Value="WaterShed desc">Water Shed</asp:ListItem>
                    <asp:ListItem Value="RiverName desc">River Name</asp:ListItem>
                    <asp:ListItem Value="OrganizationName desc">Org Name</asp:ListItem>
                    <asp:ListItem Value="StationNumber desc">Station Number</asp:ListItem>
                </asp:RadioButtonList>
                    </tr>

            <tr>
                <td style="width: 8px; height: 12px"></td>
                <td style="width: 190px; height: 12px">

                    <asp:Label ID="Label3" runat="server" Text="Next Order By: "></asp:Label>

                <td style="height: 14px; width: 662px;">&nbsp;
                <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatDirection="Horizontal" BorderColor="White" BorderStyle="Solid" BorderWidth="3px" Width="698px">
                    <asp:ListItem>None</asp:ListItem>
                    <asp:ListItem Value="SampleDate desc">Sample Date</asp:ListItem>
                    <asp:ListItem Value="WaterShed desc">Water Shed</asp:ListItem>
                    <asp:ListItem Value="RiverName desc">River Name</asp:ListItem>
                    <asp:ListItem Value="OrganizationName desc">Org Name</asp:ListItem>
                    <asp:ListItem Value="StationNumber desc">Station Number</asp:ListItem>
                </asp:RadioButtonList>
            </tr>
            <tr>
                <td style="width: 8px; height: 12px"></td>
                <td style="width: 190px; height: 12px">

                    <asp:Label ID="Label4" runat="server" Text="Next Order By: "></asp:Label>

                <td style="height: 14px; width: 662px;">&nbsp;
                <asp:RadioButtonList ID="RadioButtonList3" runat="server" RepeatDirection="Horizontal" BorderColor="White" BorderStyle="Solid" BorderWidth="3px" Width="698px">
                    <asp:ListItem>None</asp:ListItem>
                    <asp:ListItem Value="SampleDate desc">Sample Date</asp:ListItem>
                    <asp:ListItem Value="WaterShed desc">Water Shed</asp:ListItem>
                    <asp:ListItem Value="RiverName desc">River Name</asp:ListItem>
                    <asp:ListItem Value="OrganizationName desc">Org Name</asp:ListItem>
                    <asp:ListItem Value="StationNumber desc">Station Number</asp:ListItem>
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

                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="908px" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" 
                    WaitMessageFont-Size="14pt" ShowPrintButton="False">
                    <LocalReport ReportPath="Public\RawSamplesAll.rdlc">
                        <DataSources>
                            <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
                        </DataSources>
                    </LocalReport>
                </rsweb:ReportViewer>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RiverWatchDEV %>" >

        </asp:SqlDataSource>

</asp:Content>
