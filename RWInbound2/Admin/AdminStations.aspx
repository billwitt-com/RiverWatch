
<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminStations.aspx.cs" Inherits="RWInbound2.Admin.AdminStations" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <br />
    <ajaxToolkit:AutoCompleteExtender ID="tbStationName_AutoCompleteExtender" runat="server" TargetControlID="tbStationName"
         ServiceMethod="SearchStations" CompletionSetCount="2" MinimumPrefixLength="2">
    </ajaxToolkit:AutoCompleteExtender>
    <br />
      <asp:Button ID="btnNewStation" runat="server" Text="New Station" OnClick="btnNewStation_Click" />
    <asp:Panel ID="pnlInput" runat="server">
    <table style="width: 100%">
        <tr>
                      <td style="width: 52px">
                        
                          OR</td>
            <td style="width: 159px">
    <asp:Label ID="Label1" runat="server" Text="Select Station Name: "></asp:Label>
            </td>
            <td style="width: 188px">
    <asp:TextBox ID="tbStationName" runat="server"></asp:TextBox>
            </td>
            <td style="width: 60px">
    <asp:Button ID="btnSelectStnName" runat="server" Text="Select" OnClick="btnSelectStnName_Click" />
            </td>
            <td style="width: 106px">
                <asp:Label ID="Label2" runat="server" Text="  OR Station #: "></asp:Label>
            </td>
            <td style="width: 135px">
                <asp:TextBox ID="tbStnNumber" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btnSelectStnNumber" runat="server" Text="Select" OnClick="btnSelectStnNumber_Click" />
            </td>  
        </tr>       
       
    </table>
    </asp:Panel>
    <br />
    <table id="Table1" style="width: 584px"
        border="0">
        <tr>
            <td style="width: 14px">Station</td>
            <td colspan="2"></td>
        </tr>
        <tr>
            <td style="width: 14px"></td>
            <td colspan="2">
                <asp:Label ID="lblStatus" runat="server" BackColor="White" ForeColor="Red" Font-Bold="true"
                    Visible="False">

                </asp:Label></td>
        </tr>
        <tr>
            <td style="width: 14px"></td>
            <td>Station Name: *</td>
            <td>
                <asp:TextBox ID="txtStationName" runat="server">
                </asp:TextBox>

                <%--  <asp:requiredfieldvalidator id="valName" runat="server" 
                                ErrorMessage="Name is required." CssClass="errMsg" ControlToValidate="txtStationName"></asp:requiredfieldvalidator></td>--%>
        </tr>
        <tr>
            <td style="width: 14px"></td>
            <td>Station Number: *</td>
            <td>
                <asp:TextBox ID="txtStationNumber" runat="server" Width="72px">
                </asp:TextBox>
<%--                            <asp:requiredfieldvalidator id="vaStnNum" runat="server" ErrorMessage="Station Number is required." CssClass="errMsg"
								ControlToValidate="txtStationNumber"></asp:requiredfieldvalidator></td>--%>

					</tr>
					<tr>
						<td style="WIDTH: 14px; HEIGHT: 32px"></td>
						<td style="HEIGHT: 32px">Water Code: *</td>
						<td style="HEIGHT: 32px"><asp:dropdownlist id="ddlWaterCode" runat="server" Width="432px"></asp:dropdownlist></td>
					</tr>
					<tr>
						<td style="WIDTH: 14px; HEIGHT: 10px"></td>
						<td style="WIDTH: 111px; HEIGHT: 10px">River:</td>
						<td style="HEIGHT: 10px"><asp:textbox id="txtriver" runat="server"></asp:textbox>&nbsp; 
							Select River:
							<asp:dropdownlist id="ddlRiver" runat="server" Width="152px" AutoPostBack="true" onselectedindexchanged="ddlRiver_SelectedIndexChanged"></asp:dropdownlist></td>
					</tr>
					<tr>
						<td style="WIDTH: 14px; HEIGHT: 22px"></td>
						<td style="WIDTH: 111px; HEIGHT: 22px">Description:</td>
						<td style="HEIGHT: 22px"><asp:textbox id="tbdescription" runat="server"></asp:textbox></td>
					</tr>
					<tr>
						<td style="WIDTH: 14px; HEIGHT: 18px"></td>
						<td style="WIDTH: 111px; HEIGHT: 18px">Station Type: *</td>
						<td style="HEIGHT: 18px"><asp:dropdownlist id="ddlStationType" runat="server" Width="152px"></asp:dropdownlist>&nbsp;
						</td>
					</tr>
					<tr>
						<td style="WIDTH: 14px; HEIGHT: 4px"></td>
						<td style="WIDTH: 111px; HEIGHT: 4px">Station Status: *</td>
						<td style="HEIGHT: 4px"><asp:dropdownlist id="ddlStationStatus" runat="server" Width="152px"></asp:dropdownlist>&nbsp;
						</td>
					</tr>
					<tr>
						<td style="WIDTH: 14px"></td>
						<td style="WIDTH: 111px">Aquatic Model Index:</td>
						<td><asp:textbox id="tbAquaticModelIndex" runat="server"></asp:textbox></td>
					</tr>
					<tr>
						<td style="WIDTH: 14px; HEIGHT: 18px"></td>
						<td style="WIDTH: 111px; HEIGHT: 18px">Water Body ID: *</td>
						<td style="HEIGHT: 18px"><asp:dropdownlist id="ddlWaterBodyID" runat="server" Width="152px"></asp:dropdownlist>&nbsp;
						</td>
					</tr>
					<tr>
						<td style="WIDTH: 14px"></td>
						<td style="WIDTH: 111px">QUADI:</td>
						<td><asp:dropdownlist id="ddlQUADI" runat="server" Width="152px"></asp:dropdownlist></td>
					</tr>
					<tr>
						<td style="WIDTH: 14px"></td>
						<td style="WIDTH: 111px">Township:</td>
						<td><asp:dropdownlist id="ddlTownship" OnDataBinding="ddl_DataBinding" runat="server" Width="152px"></asp:dropdownlist></td>
					</tr>
					<tr>
						<td style="WIDTH: 14px; HEIGHT: 19px"></td>
						<td style="WIDTH: 111px; HEIGHT: 19px">Range:</td>
						<td style="HEIGHT: 19px"><asp:dropdownlist id="ddlRange" runat="server" Width="152px"></asp:dropdownlist></td>
					</tr>
					<tr>
						<td style="WIDTH: 14px"></td>
						<td style="WIDTH: 111px">Section:</td>
						<td><asp:dropdownlist id="ddlSection" runat="server" Width="152px"></asp:dropdownlist></td>
					</tr>
					<tr>
						<td style="WIDTH: 14px"></td>
						<td style="WIDTH: 111px">Quarter Section:</td>
						<td><asp:dropdownlist id="ddlQuarterSection" runat="server" Width="152px"></asp:dropdownlist></td>
					</tr>
					<tr>
						<td style="WIDTH: 14px; HEIGHT: 15px"></td>
						<td style="WIDTH: 111px; HEIGHT: 15px">Grid:</td>
						<td style="HEIGHT: 15px"><asp:dropdownlist id="ddlGrid" runat="server" Width="152px"></asp:dropdownlist></td>
					</tr>
					<tr>
						<td style="WIDTH: 14px"></td>
						<td style="WIDTH: 111px">Station QUAD:</td>
						<td><asp:dropdownlist id="ddlStationQUAD" runat="server" Width="152px"></asp:dropdownlist></td>
					</tr>
					<tr>
						<td style="WIDTH: 14px"></td>
						<td style="WIDTH: 111px">RiverWatch Watershed:</td>
						<td><asp:dropdownlist id="ddlRWWaterShed" runat="server" Width="152px"></asp:dropdownlist></td>
					</tr>
					<tr>
						<td style="WIDTH: 14px"></td>
						<td style="WIDTH: 111px">WQCC Watershed:</td>
						<td><asp:dropdownlist id="ddlWQCCWaterShed" runat="server" Width="152px"></asp:dropdownlist></td>
					</tr>
					<tr>
						<td style="WIDTH: 14px"></td>
						<td style="WIDTH: 111px">Hydro Unit:</td>
						<td><asp:dropdownlist id="ddlHydroUnit" runat="server" Width="152px"></asp:dropdownlist></td>
					</tr>
					<tr>
						<td style="WIDTH: 14px; HEIGHT: 16px"></td>
						<td style="WIDTH: 111px; HEIGHT: 16px">Eco Region:</td>
						<td style="HEIGHT: 16px"><asp:dropdownlist id="ddlEcoRegion" runat="server" Width="312px"></asp:dropdownlist></td>
					</tr>
					<tr>
						<td style="WIDTH: 14px"></td>
						<td style="WIDTH: 111px">Elevation:</td>
						<td><asp:textbox id="tbElevation" runat="server" Width="152px" Height="22px"></asp:textbox></td>
					</tr>
					<tr>
						<td style="WIDTH: 14px"></td>
						<td style="WIDTH: 111px">Watershed Report:</td>
						<td><asp:dropdownlist id="ddlWSR" runat="server" Width="152px"></asp:dropdownlist></td>
					</tr>
					<tr>
						<td style="WIDTH: 14px"></td>
						<td style="WIDTH: 111px">Longitude: *</td>
						<td><asp:textbox id="tbLongtitude" runat="server">
                            </asp:textbox>
<%--						    <asp:requiredfieldvalidator id="rfvLongitude" runat="server" ErrorMessage="Longitude is required." CssClass="errMsg"
								ControlToValidate="tbLongtitude" BorderStyle="None"></asp:requiredfieldvalidator></td>--%>
					</tr>
					<tr>
						<td style="WIDTH: 14px; HEIGHT: 6px"></td>
						<td style="WIDTH: 111px; HEIGHT: 6px">Latitude: *</td>
						<td style="HEIGHT: 6px">
                            <asp:textbox id="tbLatitude" runat="server"> </asp:textbox>

<%--                         <asp:requiredfieldvalidator id="rfvLatitude" runat="server" ErrorMessage="Latitude is required." CssClass="errMsg"
								ControlToValidate="tbLatitude"></asp:requiredfieldvalidator></td>--%>
					</tr>
					<tr>
						<td style="WIDTH: 14px; HEIGHT: 12px"></td>
						<td style="WIDTH: 111px; HEIGHT: 12px">UTMX:</td>
						<td style="HEIGHT: 12px"><asp:textbox id="tbUTMX" runat="server"></asp:textbox></td>
					</tr>
					<tr>
						<td style="WIDTH: 14px; HEIGHT: 12px"></td>
						<td style="WIDTH: 111px; HEIGHT: 12px">UTMY:</td>
						<td style="HEIGHT: 12px"><asp:textbox id="tbUTMY" runat="server"></asp:textbox></td>
					</tr>
					<tr>
						<td style="WIDTH: 14px; HEIGHT: 12px"></td>
						<td style="WIDTH: 111px; HEIGHT: 12px">County:</td>
						<td style="HEIGHT: 12px"><asp:dropdownlist id="ddlCounty" runat="server" Width="152px"></asp:dropdownlist></td>
					</tr>
					<tr>
						<td style="WIDTH: 14px"></td>
						<td style="WIDTH: 111px">State:</td>
						<td><asp:dropdownlist id="ddlState" runat="server" Width="152px"></asp:dropdownlist></td>
					</tr>
					<tr>
						<td style="WIDTH: 14px"></td>
						<td style="WIDTH: 111px">Near City:</td>
						<td><asp:textbox id="tbNearCity" runat="server"></asp:textbox></td>
					</tr>
					<tr>
						<td style="WIDTH: 14px; HEIGHT: 9px"></td>
						<td style="WIDTH: 111px; HEIGHT: 9px">Move:</td>
						<td style="HEIGHT: 9px">
                            <asp:textbox id="tbMove" runat="server"></asp:textbox></td>
					</tr>
					<tr>
						<td style="WIDTH: 14px"></td>
						<td style="WIDTH: 111px">Region:</td>
						<td><asp:dropdownlist id="ddlRegion" runat="server" Width="152px"></asp:dropdownlist></td>
					</tr>
					<tr>
						<td style="WIDTH: 14px"></td>
						<td style="WIDTH: 111px">USGS:</td>
						<td><asp:checkbox id="cbUSGS" runat="server"></asp:checkbox></td>
					</tr>
					<tr>
						<td style="WIDTH: 14px"></td>
						<td style="WIDTH: 111px">State Engineering:</td>
						<td><asp:checkbox id="cbStateEngineering" runat="server"></asp:checkbox></td>
					</tr>
					<tr>
						<td style="WIDTH: 14px"></td>
						<td style="WIDTH: 111px">Comment:</td>
						<td><asp:textbox id="tbComment" runat="server" Width="330px" Rows="10" TextMode="MultiLine"></asp:textbox></td>
					</tr>
					<tr>
						<td style="WIDTH: 14px"></td>
						<td style="WIDTH: 111px"></td>
						<td></td>
					</tr>
					<tr>
						<td style="WIDTH: 14px"></td>
						<td style="WIDTH: 111px"></td>
						<td></td>
					</tr>
					<tr>
						<td style="WIDTH: 14px"></td>
						<td style="WIDTH: 111px"><asp:button id="cmdUpdate" runat="server" Width="61px" Height="24px" Text="Save"  OnClick="cmdUpdate_Click"></asp:button></td>
						<td><asp:button id="btnCancel" runat="server" Width="80px" Text="Cancel" CausesValidation="False"></asp:button></td>
					</tr>
					<tr>
						<td style="WIDTH: 60px" colSpan="3"></td>
					</tr>
				</TABLE>

</asp:Content>
 