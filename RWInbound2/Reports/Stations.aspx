<%@ Page Title="Stations" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Stations.aspx.cs" Inherits="RWInbound2.Reports.Stations" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <hgroup>
        <h3><%: Page.Title %></h3>
    </hgroup>

    <div class="label-placement">
        <asp:Label ID="ErrorLabel" CssClass="label-error" runat="server" />               
    </div>
    <div class="label-placement">
            <asp:Label ID="SuccessLabel" CssClass="label-success" runat="server" />
    </div>
    Select By:<br />

    <table>
        <tr>
                <td style="width: 8px; height: 32px"></td>
                <td style="height: 32px; width: 190px;">Water Code:</td>
                <td style="height: 32px">
                    <asp:DropDownList ID="ddlWaterCode"  runat="server" Width="120px" Height="22px"></asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 8px; height: 10px"></td>
                <td style="width: 190px; height: 10px">River:</td>
                <td style="height: 10px">
				<asp:DropDownList ID="ddlRiver" runat="server" Width="152px"></asp:DropDownList></td>
            </tr>
         <tr>
                <td style="width: 8px; height: 16px"></td>
                <td style="width: 190px; height: 16px">Eco Region:</td>
                <td style="height: 16px">
                    <asp:DropDownList ID="ddlEcoRegion"  runat="server" Width="619px" Height="22px"></asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 8px; height: 18px"></td>
                <td style="width: 190px; height: 18px">Station Type:</td>
                <td style="height: 18px">
                    <asp:DropDownList ID="ddlStationType" runat="server"  Width="700px" Height="22px"></asp:DropDownList>&nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 8px; height: 4px"></td>
                <td style="width: 190px; height: 4px">Station Status:</td>
                <td style="height: 4px">
                    <asp:DropDownList ID="ddlStationStatus" runat="server"  Width="152px"></asp:DropDownList>&nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 8px; height: 18px"></td>
                <td style="width: 190px; height: 18px">Water Body ID:</td>
                <td style="height: 18px">
                    <asp:DropDownList ID="ddlWaterBodyID" runat="server"   Height="22px"></asp:DropDownList>&nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 8px"></td>
                <td style="width: 190px">RiverWatch Watershed:</td>
                <td>
                    <asp:DropDownList ID="ddlRWWaterShed"  runat="server" Width="152px"></asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 8px"></td>
                <td style="width: 190px">WQCC Watershed:</td>
                <td>
                    <asp:DropDownList ID="ddlWQCCWaterShed" runat="server" Width="616px" Height="22px"></asp:DropDownList></td>
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
                    <asp:DropDownList ID="ddlCounty" runat="server" Width="152px"></asp:DropDownList></td>
            </tr>
        <tr>
                <td style="width: 8px; height: 12px"></td>
                <td style="width: 190px; height: 12px">
                    <asp:Button ID="btnGO" runat="server" Text="SELECT" OnClick="btnGO_Click" /> </td>
                <td style="height: 12px">
                   
                    &nbsp;</tr>
            
           

    </table>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="1072px" Height="551px">
        <LocalReport ReportPath="Reports\Stations.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources>
        </LocalReport>
</rsweb:ReportViewer>
<%--        SelectCommand="SELECT [StationNumber], [River], [AquaticModelIndex], [WaterCode], [WaterBodyID], [StationType], [RWWaterShed], [StationQUAD], [Grid], [QuaterSection], [Section], [Range], [Township], [QUADI], [WQCCWaterShed], [HydroUnit], [EcoRegion], [StationName], [StationStatus], [Elevation], [WaterShedRegion], [Longtitude], [Latitude], [County], [State], [NearCity], [Move], [Description], [UTMX], [UTMY], [UserLastModified], [DateLastModified], [UserCreated], [DateCreated], [StateEngineering], [USGS], [Comments], [Region], [StoretUploaded] FROM [Station] ORDER BY [StationName]">--%>

<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RiverWatchDEV %>" >



</asp:SqlDataSource>
</asp:Content>
