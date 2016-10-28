<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminStations.aspx.cs" Inherits="RWInbound2.Admin.AdminStations" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <br />
    <ajaxToolkit:AutoCompleteExtender ID="tbStationName_AutoCompleteExtender" runat="server" TargetControlID="tbStationName"
        ServiceMethod="SearchStations" CompletionSetCount="2" MinimumPrefixLength="2">
    </ajaxToolkit:AutoCompleteExtender>
    <br />
    <asp:Button ID="btnNewStation" runat="server" Text="New Station" OnClick="btnNewStation_Click" CssClass="adminButton" />
    <asp:Panel ID="pnlInput" runat="server">
        <table style="width: 100%">
            <tr>
                <td style="width: 43px">&nbsp;<asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Size="18px" Text="  OR"></asp:Label>
                </td>
                <td style="width: 108px">
                    <asp:Label ID="Label1" runat="server" Text="Station Name: "></asp:Label>
                </td>
                <td style="width: 132px">
                    <asp:TextBox ID="tbStationName" runat="server"></asp:TextBox>
                </td>
                <td style="width: 60px">
                    <asp:Button ID="btnSelectStnName" runat="server" Text="Select" OnClick="btnSelectStnName_Click" CssClass="adminButton" />
                </td>
                <td style="width: 106px">
                    <asp:Label ID="Label2" runat="server" Text="  OR Station #: "></asp:Label>
                </td>
                <td style="width: 135px">
                    <asp:TextBox ID="tbStnNumber" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnSelectStnNumber" runat="server" Text="Select" OnClick="btnSelectStnNumber_Click" CssClass="adminButton" />
                </td>
            </tr>

        </table>
    </asp:Panel>
    <br />
<%--    <asp:Panel ID="pnlTable" runat="server">--%>

        <table id="Table1" style="width: 917px"
            border="0">
            <%--<tr>
            <td style="width: 8px">Station</td>
            <td colspan="2"></td>
        </tr>--%>
            <tr>
                <td style="width: 8px"></td>
                <td colspan="2">
                    <asp:Label ID="lblStatus" runat="server" BackColor="White" ForeColor="Red" Font-Bold="true"
                        Visible="False">

                    </asp:Label></td>
            </tr>
            <tr>
                <td style="width: 8px"></td>
                <td style="width: 190px">Station Name: *</td>
                <td>
                    <asp:TextBox ID="txtStationName" runat="server">
                    </asp:TextBox>

                    <%--  <asp:requiredfieldvalidator id="valName" runat="server" 
                                ErrorMessage="Name is required." CssClass="errMsg" ControlToValidate="txtStationName"></asp:requiredfieldvalidator></td>--%>
            </tr>
            <tr>
                <td style="width: 8px"></td>
                <td style="width: 190px">Station Number: *</td>
                <td>
                    <asp:TextBox ID="txtStationNumber" runat="server" Width="72px">
                    </asp:TextBox>
                    <%--                            <asp:requiredfieldvalidator id="vaStnNum" runat="server" ErrorMessage="Station Number is required." CssClass="errMsg"
								ControlToValidate="txtStationNumber"></asp:requiredfieldvalidator></td>--%>
            </tr>
            <tr>
                <td style="width: 8px; height: 32px"></td>
                <td style="height: 32px; width: 190px;">Water Code: *</td>
                <td style="height: 32px">
                    <asp:DropDownList ID="ddlWaterCode" runat="server" Width="432px"></asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 8px; height: 10px"></td>
                <td style="width: 190px; height: 10px">River:</td>
                <td style="height: 10px">
                    <asp:TextBox ID="txtriver" runat="server"></asp:TextBox>&nbsp; 
							Select River:
							<asp:DropDownList ID="ddlRiver" runat="server" Width="152px" AutoPostBack="true" OnSelectedIndexChanged="ddlRiver_SelectedIndexChanged"></asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 8px; height: 22px"></td>
                <td style="width: 190px; height: 22px">Description:</td>
                <td style="height: 22px">
                    <asp:TextBox ID="tbdescription" runat="server" Height="53px" TextMode="MultiLine" Width="631px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 8px; height: 18px"></td>
                <td style="width: 190px; height: 18px">Station Type: *</td>
                <td style="height: 18px">
                    <asp:DropDownList ID="ddlStationType" runat="server" Width="152px"></asp:DropDownList>&nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 8px; height: 4px"></td>
                <td style="width: 190px; height: 4px">Station Status: *</td>
                <td style="height: 4px">
                    <asp:DropDownList ID="ddlStationStatus" runat="server" Width="152px"></asp:DropDownList>&nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 8px"></td>
                <td style="width: 190px">Aquatic Model Index:</td>
                <td>
                    <asp:TextBox ID="tbAquaticModelIndex" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 8px; height: 18px"></td>
                <td style="width: 190px; height: 18px">Water Body ID: *</td>
                <td style="height: 18px">
                    <asp:DropDownList ID="ddlWaterBodyID" runat="server" Width="152px"></asp:DropDownList>&nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 8px"></td>
                <td style="width: 190px">QUADI:</td>
                <td>
                    <asp:DropDownList ID="ddlQUADI" runat="server" Width="152px"></asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 8px"></td>
                <td style="width: 190px">Township:</td>
                <td>
                    <asp:DropDownList ID="ddlTownship" OnDataBinding="ddl_DataBinding" runat="server" Width="152px"></asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 8px; height: 19px"></td>
                <td style="width: 190px; height: 19px">Range:</td>
                <td style="height: 19px">
                    <asp:DropDownList ID="ddlRange" runat="server" Width="152px"></asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 8px"></td>
                <td style="width: 190px">Section:</td>
                <td>
                    <asp:DropDownList ID="ddlSection" runat="server" Width="152px"></asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 8px"></td>
                <td style="width: 190px">Quarter Section:</td>
                <td>
                    <asp:DropDownList ID="ddlQuarterSection" runat="server" Width="152px"></asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 8px; height: 15px"></td>
                <td style="width: 190px; height: 15px">Grid:</td>
                <td style="height: 15px">
                    <asp:DropDownList ID="ddlGrid" runat="server" Width="152px"></asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 8px"></td>
                <td style="width: 190px">Station QUAD:</td>
                <td>
                    <asp:DropDownList ID="ddlStationQUAD" runat="server" Width="152px"></asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 8px"></td>
                <td style="width: 190px">RiverWatch Watershed:</td>
                <td>
                    <asp:DropDownList ID="ddlRWWaterShed" runat="server" Width="152px"></asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 8px"></td>
                <td style="width: 190px">WQCC Watershed:</td>
                <td>
                    <asp:DropDownList ID="ddlWQCCWaterShed" runat="server" Width="152px"></asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 8px"></td>
                <td style="width: 190px">Hydro Unit:</td>
                <td>
                    <asp:DropDownList ID="ddlHydroUnit" runat="server" Width="152px"></asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 8px; height: 16px"></td>
                <td style="width: 190px; height: 16px">Eco Region:</td>
                <td style="height: 16px">
                    <asp:DropDownList ID="ddlEcoRegion" runat="server" Width="312px"></asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 8px"></td>
                <td style="width: 190px">Elevation:</td>
                <td>
                    <asp:TextBox ID="tbElevation" runat="server" Width="152px" Height="22px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 8px"></td>
                <td style="width: 190px">Watershed Report:</td>
                <td>
                    <asp:DropDownList ID="ddlWSR" runat="server" Width="152px"></asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 8px"></td>
                <td style="width: 190px">Longitude: *</td>
                <td>
                    <asp:TextBox ID="tbLongtitude" runat="server">
                    </asp:TextBox>
                    <%--						    <asp:requiredfieldvalidator id="rfvLongitude" runat="server" ErrorMessage="Longitude is required." CssClass="errMsg"
								ControlToValidate="tbLongtitude" BorderStyle="None"></asp:requiredfieldvalidator></td>--%>
            </tr>
            <tr>
                <td style="width: 8px; height: 6px"></td>
                <td style="width: 190px; height: 6px">Latitude: *</td>
                <td style="height: 6px">
                    <asp:TextBox ID="tbLatitude" runat="server"> </asp:TextBox>

                    <%--                         <asp:requiredfieldvalidator id="rfvLatitude" runat="server" ErrorMessage="Latitude is required." CssClass="errMsg"
								ControlToValidate="tbLatitude"></asp:requiredfieldvalidator></td>--%>
            </tr>
            <tr>
                <td style="width: 8px; height: 12px"></td>
                <td style="width: 190px; height: 12px">UTMX:</td>
                <td style="height: 12px">
                    <asp:TextBox ID="tbUTMX" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 8px; height: 12px"></td>
                <td style="width: 190px; height: 12px">UTMY:</td>
                <td style="height: 12px">
                    <asp:TextBox ID="tbUTMY" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 8px; height: 12px"></td>
                <td style="width: 190px; height: 12px">County:</td>
                <td style="height: 12px">
                    <asp:DropDownList ID="ddlCounty" runat="server" Width="152px"></asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 8px"></td>
                <td style="width: 190px">State:</td>
                <td>
                    <asp:DropDownList ID="ddlState" runat="server" Width="152px"></asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 8px"></td>
                <td style="width: 190px">Near City:</td>
                <td>
                    <asp:TextBox ID="tbNearCity" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 8px; height: 9px"></td>
                <td style="width: 190px; height: 9px">Move:</td>
                <td style="height: 9px">
                    <asp:TextBox ID="tbMove" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 8px"></td>
                <td style="width: 190px">Region:</td>
                <td>
                    <asp:DropDownList ID="ddlRegion" runat="server" Width="152px"></asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 8px"></td>
                <td style="width: 190px">USGS:</td>
                <td>
                    <asp:CheckBox ID="cbUSGS" runat="server"></asp:CheckBox></td>
            </tr>
            <tr>
                <td style="width: 8px"></td>
                <td style="width: 190px">State Engineering:</td>
                <td>
                    <asp:CheckBox ID="cbStateEngineering" runat="server"></asp:CheckBox></td>
            </tr>
            <tr>
                <td style="width: 8px"></td>
                <td style="width: 190px">Comment:</td>
                <td>
                    <asp:TextBox ID="tbComment" runat="server" Width="448px" Rows="10" TextMode="MultiLine" Height="76px"></asp:TextBox></td>
            </tr>
            <%--<tr>
						<td style="WIDTH: 8px"></td>
						<td style="WIDTH: 190px"></td>
						<td></td>
					</tr>
					<tr>
						<td style="WIDTH: 8px"></td>
						<td style="WIDTH: 190px"></td>
						<td></td>
					</tr>--%>
            <tr>
                <td style="width: 8px"></td>
                <td style="width: 190px">
                    <asp:Button ID="cmdUpdate" runat="server" Width="61px" Height="24px" Text="Save" OnClick="cmdUpdate_Click" CssClass="adminButton"></asp:Button>
                </td>
                <td>
                    <asp:Button ID="btnCancel" runat="server" Width="80px" Text="Cancel" CausesValidation="False" CssClass="adminButton"></asp:Button></td>
            </tr>
            <tr>
                <td style="width: 60px" colspan="3"></td>
            </tr>
        </table>
<%--    </asp:Panel>--%>

</asp:Content>
