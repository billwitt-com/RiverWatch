
<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminStations.aspx.cs" Inherits="RWInbound2.Admin.AdminStations" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <br />
    <asp:Label ID="Label1" runat="server" Text="Seelect Station Name: "></asp:Label>
    <asp:TextBox ID="tbStationName" runat="server"></asp:TextBox>
    <asp:Button ID="btnSelect" runat="server" Text="Select" OnClick="btnSelect_Click" />
    <ajaxToolkit:AutoCompleteExtender ID="tbStationName_AutoCompleteExtender" runat="server" TargetControlID="tbStationName"
         ServiceMethod="SearchStations" CompletionSetCount="2" MinimumPrefixLength="2">
    </ajaxToolkit:AutoCompleteExtender>
<asp:FormView  ID="FormViewStations" DefaultMode="ReadOnly"  AllowPaging="true" PagerSettings-Mode="NumericFirstLast" PagerSettings-Position="TopAndBottom" runat="server" DataKeyNames="ID" DataSourceID="SqlDataSource1">
    <EditItemTemplate>
        ID:
        <asp:Label ID="IDLabel1" runat="server" Text='<%# Eval("ID") %>' />
        <br />
        StationName:
        <asp:TextBox ID="StationNameTextBox" runat="server" Text='<%# Bind("StationName") %>' />
        <br />
        StationNumber:
        <asp:TextBox ID="StationNumberTextBox" runat="server" Text='<%# Bind("StationNumber") %>' />
        <br />
        River:
        <asp:TextBox ID="RiverTextBox" runat="server" Text='<%# Bind("River") %>' />
        <br />
        AquaticModelIndex:
        <asp:TextBox ID="AquaticModelIndexTextBox" runat="server" Text='<%# Bind("AquaticModelIndex") %>' />
        <br />
        WaterCode:
        <asp:TextBox ID="WaterCodeTextBox" runat="server" Text='<%# Bind("WaterCode") %>' />
        <br />
        WaterBodyID:
        <asp:TextBox ID="WaterBodyIDTextBox" runat="server" Text='<%# Bind("WaterBodyID") %>' />
        <br />
        StationType:
        <asp:TextBox ID="StationTypeTextBox" runat="server" Text='<%# Bind("StationType") %>' />
        <br />
        QUADI:
        <asp:TextBox ID="QUADITextBox" runat="server" Text='<%# Bind("QUADI") %>' />
        <br />
        Township:
        <asp:TextBox ID="TownshipTextBox" runat="server" Text='<%# Bind("Township") %>' />
        <br />
        Range:
        <asp:TextBox ID="RangeTextBox" runat="server" Text='<%# Bind("Range") %>' />
        <br />
        Section:
        <asp:TextBox ID="SectionTextBox" runat="server" Text='<%# Bind("Section") %>' />
        <br />
        QuaterSection:
        <asp:TextBox ID="QuaterSectionTextBox" runat="server" Text='<%# Bind("QuaterSection") %>' />
        <br />
        Grid:
        <asp:TextBox ID="GridTextBox" runat="server" Text='<%# Bind("Grid") %>' />
        <br />
        StationQUAD:
        <asp:TextBox ID="StationQUADTextBox" runat="server" Text='<%# Bind("StationQUAD") %>' />
        <br />
        RWWaterShed:
        <asp:TextBox ID="RWWaterShedTextBox" runat="server" Text='<%# Bind("RWWaterShed") %>' />
        <br />
        WQCCWaterShed:
        <asp:TextBox ID="WQCCWaterShedTextBox" runat="server" Text='<%# Bind("WQCCWaterShed") %>' />
        <br />
        HydroUnit:
        <asp:TextBox ID="HydroUnitTextBox" runat="server" Text='<%# Bind("HydroUnit") %>' />
        <br />
        EcoRegion:
        <asp:TextBox ID="EcoRegionTextBox" runat="server" Text='<%# Bind("EcoRegion") %>' />
        <br />

        StationStatus:
        <asp:TextBox ID="StationStatusTextBox" runat="server" Text='<%# Bind("StationStatus") %>' />
        <br />
        Elevation:
        <asp:TextBox ID="ElevationTextBox" runat="server" Text='<%# Bind("Elevation") %>' />
        <br />
        WaterShedRegion:
        <asp:TextBox ID="WaterShedRegionTextBox" runat="server" Text='<%# Bind("WaterShedRegion") %>' />
        <br />
        Longtitude:
        <asp:TextBox ID="LongtitudeTextBox" runat="server" Text='<%# Bind("Longtitude") %>' />
        <br />
        Latitude:
        <asp:TextBox ID="LatitudeTextBox" runat="server" Text='<%# Bind("Latitude") %>' />
        <br />
        County:
        <asp:TextBox ID="CountyTextBox" runat="server" Text='<%# Bind("County") %>' />
        <br />
        State:
        <asp:TextBox ID="StateTextBox" runat="server" Text='<%# Bind("State") %>' />
        <br />
        NearCity:
        <asp:TextBox ID="NearCityTextBox" runat="server" Text='<%# Bind("NearCity") %>' />
        <br />
        Move:
        <asp:TextBox ID="MoveTextBox" runat="server" Text='<%# Bind("Move") %>' />
        <br />
        Description:
        <asp:TextBox ID="DescriptionTextBox" runat="server" Text='<%# Bind("Description") %>' />
        <br />
        UTMX:
        <asp:TextBox ID="UTMXTextBox" runat="server" Text='<%# Bind("UTMX") %>' />
        <br />
        UTMY:
        <asp:TextBox ID="UTMYTextBox" runat="server" Text='<%# Bind("UTMY") %>' />
        <br />
        Region:
        <asp:TextBox ID="RegionTextBox" runat="server" Text='<%# Bind("Region") %>' />
        <br />
        Comments:
        <asp:TextBox ID="CommentsTextBox" runat="server" Text='<%# Bind("Comments") %>' />
        <br />
        USGS:
        <asp:CheckBox ID="USGSCheckBox" runat="server" Checked='<%# Bind("USGS") %>' />
        <br />
        StateEngineering:
        <asp:CheckBox ID="StateEngineeringCheckBox" runat="server" Checked='<%# Bind("StateEngineering") %>' />
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
        StoretUploaded:
        <asp:TextBox ID="StoretUploadedTextBox" runat="server" Text='<%# Bind("StoretUploaded") %>' />
        <br />
        <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
        &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
    </EditItemTemplate>
    <InsertItemTemplate>
        StationNumber:
        <asp:TextBox ID="StationNumberTextBox" runat="server" Text='<%# Bind("StationNumber") %>' />
        <br />
        StationName:
        <asp:TextBox ID="StationNameTextBox" runat="server" Text='<%# Bind("StationName") %>' />
        <br />
        River:
        <asp:TextBox ID="RiverTextBox" runat="server" Text='<%# Bind("River") %>' />
        <br />
        AquaticModelIndex:
        <asp:TextBox ID="AquaticModelIndexTextBox" runat="server" Text='<%# Bind("AquaticModelIndex") %>' />
        <br />
        WaterCode:
        <asp:TextBox ID="WaterCodeTextBox" runat="server" Text='<%# Bind("WaterCode") %>' />
        <br />
        WaterBodyID:
        <asp:TextBox ID="WaterBodyIDTextBox" runat="server" Text='<%# Bind("WaterBodyID") %>' />
        <br />
        StationType:
        <asp:TextBox ID="StationTypeTextBox" runat="server" Text='<%# Bind("StationType") %>' />
        <br />
        QUADI:
        <asp:TextBox ID="QUADITextBox" runat="server" Text='<%# Bind("QUADI") %>' />
        <br />
        Township:
        <asp:TextBox ID="TownshipTextBox" runat="server" Text='<%# Bind("Township") %>' />
        <br />
        Range:
        <asp:TextBox ID="RangeTextBox" runat="server" Text='<%# Bind("Range") %>' />
        <br />
        Section:
        <asp:TextBox ID="SectionTextBox" runat="server" Text='<%# Bind("Section") %>' />
        <br />
        QuaterSection:
        <asp:TextBox ID="QuaterSectionTextBox" runat="server" Text='<%# Bind("QuaterSection") %>' />
        <br />
        Grid:
        <asp:TextBox ID="GridTextBox" runat="server" Text='<%# Bind("Grid") %>' />
        <br />
        StationQUAD:
        <asp:TextBox ID="StationQUADTextBox" runat="server" Text='<%# Bind("StationQUAD") %>' />
        <br />
        RWWaterShed:
        <asp:TextBox ID="RWWaterShedTextBox" runat="server" Text='<%# Bind("RWWaterShed") %>' />
        <br />
        WQCCWaterShed:
        <asp:TextBox ID="WQCCWaterShedTextBox" runat="server" Text='<%# Bind("WQCCWaterShed") %>' />
        <br />
        HydroUnit:
        <asp:TextBox ID="HydroUnitTextBox" runat="server" Text='<%# Bind("HydroUnit") %>' />
        <br />
        EcoRegion:
        <asp:TextBox ID="EcoRegionTextBox" runat="server" Text='<%# Bind("EcoRegion") %>' />
        <br />

        StationStatus:
        <asp:TextBox ID="StationStatusTextBox" runat="server" Text='<%# Bind("StationStatus") %>' />
        <br />
        Elevation:
        <asp:TextBox ID="ElevationTextBox" runat="server" Text='<%# Bind("Elevation") %>' />
        <br />
        WaterShedRegion:
        <asp:TextBox ID="WaterShedRegionTextBox" runat="server" Text='<%# Bind("WaterShedRegion") %>' />
        <br />
        Longtitude:
        <asp:TextBox ID="LongtitudeTextBox" runat="server" Text='<%# Bind("Longtitude") %>' />
        <br />
        Latitude:
        <asp:TextBox ID="LatitudeTextBox" runat="server" Text='<%# Bind("Latitude") %>' />
        <br />
        County:
        <asp:TextBox ID="CountyTextBox" runat="server" Text='<%# Bind("County") %>' />
        <br />
        State:
        <asp:TextBox ID="StateTextBox" runat="server" Text='<%# Bind("State") %>' />
        <br />
        NearCity:
        <asp:TextBox ID="NearCityTextBox" runat="server" Text='<%# Bind("NearCity") %>' />
        <br />
        Move:
        <asp:TextBox ID="MoveTextBox" runat="server" Text='<%# Bind("Move") %>' />
        <br />
        Description:
        <asp:TextBox ID="DescriptionTextBox" runat="server" Text='<%# Bind("Description") %>' />
        <br />
        UTMX:
        <asp:TextBox ID="UTMXTextBox" runat="server" Text='<%# Bind("UTMX") %>' />
        <br />
        UTMY:
        <asp:TextBox ID="UTMYTextBox" runat="server" Text='<%# Bind("UTMY") %>' />
        <br />
        Region:
        <asp:TextBox ID="RegionTextBox" runat="server" Text='<%# Bind("Region") %>' />
        <br />
        Comments:
        <asp:TextBox ID="CommentsTextBox" runat="server" Text='<%# Bind("Comments") %>' />
        <br />
        USGS:
        <asp:CheckBox ID="USGSCheckBox" runat="server" Checked='<%# Bind("USGS") %>' />
        <br />
        StateEngineering:
        <asp:CheckBox ID="StateEngineeringCheckBox" runat="server" Checked='<%# Bind("StateEngineering") %>' />
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
        StoretUploaded:
        <asp:TextBox ID="StoretUploadedTextBox" runat="server" Text='<%# Bind("StoretUploaded") %>' />
        <br />
        <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
        &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
    </InsertItemTemplate>
    <ItemTemplate>
        ID:
        <asp:Label ID="IDLabel" runat="server" Text='<%# Eval("ID") %>' />
        <br />
                StationName:
        <asp:Label ID="StationNameLabel" runat="server" Text='<%# Bind("StationName") %>' />
        <br />
        StationNumber:
        <asp:Label ID="StationNumberLabel" runat="server" Text='<%# Bind("StationNumber") %>' />
        <br />
        River:
        <asp:Label ID="RiverLabel" runat="server" Text='<%# Bind("River") %>' />
        <br />
        AquaticModelIndex:
        <asp:Label ID="AquaticModelIndexLabel" runat="server" Text='<%# Bind("AquaticModelIndex") %>' />
        <br />
        WaterCode:
        <asp:Label ID="WaterCodeLabel" runat="server" Text='<%# Bind("WaterCode") %>' />
        <br />
        WaterBodyID:
        <asp:Label ID="WaterBodyIDLabel" runat="server" Text='<%# Bind("WaterBodyID") %>' />
        <br />
        StationType:
        <asp:Label ID="StationTypeLabel" runat="server" Text='<%# Bind("StationType") %>' />
        <br />
        QUADI:
        <asp:Label ID="QUADILabel" runat="server" Text='<%# Bind("QUADI") %>' />
        <br />
        Township:
        <asp:Label ID="TownshipLabel" runat="server" Text='<%# Bind("Township") %>' />
        <br />
        Range:
        <asp:Label ID="RangeLabel" runat="server" Text='<%# Bind("Range") %>' />
        <br />
        Section:
        <asp:Label ID="SectionLabel" runat="server" Text='<%# Bind("Section") %>' />
        <br />
        QuaterSection:
        <asp:Label ID="QuaterSectionLabel" runat="server" Text='<%# Bind("QuaterSection") %>' />
        <br />
        Grid:
        <asp:Label ID="GridLabel" runat="server" Text='<%# Bind("Grid") %>' />
        <br />
        StationQUAD:
        <asp:Label ID="StationQUADLabel" runat="server" Text='<%# Bind("StationQUAD") %>' />
        <br />
        RWWaterShed:
        <asp:Label ID="RWWaterShedLabel" runat="server" Text='<%# Bind("RWWaterShed") %>' />
        <br />
        WQCCWaterShed:
        <asp:Label ID="WQCCWaterShedLabel" runat="server" Text='<%# Bind("WQCCWaterShed") %>' />
        <br />
        HydroUnit:
        <asp:Label ID="HydroUnitLabel" runat="server" Text='<%# Bind("HydroUnit") %>' />
        <br />
        EcoRegion:
        <asp:Label ID="EcoRegionLabel" runat="server" Text='<%# Bind("EcoRegion") %>' />
        <br />

        StationStatus:
        <asp:Label ID="StationStatusLabel" runat="server" Text='<%# Bind("StationStatus") %>' />
        <br />
        Elevation:
        <asp:Label ID="ElevationLabel" runat="server" Text='<%# Bind("Elevation") %>' />
        <br />
        WaterShedRegion:
        <asp:Label ID="WaterShedRegionLabel" runat="server" Text='<%# Bind("WaterShedRegion") %>' />
        <br />
        Longtitude:
        <asp:Label ID="LongtitudeLabel" runat="server" Text='<%# Bind("Longtitude") %>' />
        <br />
        Latitude:
        <asp:Label ID="LatitudeLabel" runat="server" Text='<%# Bind("Latitude") %>' />
        <br />
        County:
        <asp:Label ID="CountyLabel" runat="server" Text='<%# Bind("County") %>' />
        <br />
        State:
        <asp:Label ID="StateLabel" runat="server" Text='<%# Bind("State") %>' />
        <br />
        NearCity:
        <asp:Label ID="NearCityLabel" runat="server" Text='<%# Bind("NearCity") %>' />
        <br />
        Move:
        <asp:Label ID="MoveLabel" runat="server" Text='<%# Bind("Move") %>' />
        <br />
        Description:
        <asp:Label ID="DescriptionLabel" runat="server" Text='<%# Bind("Description") %>' />
        <br />
        UTMX:
        <asp:Label ID="UTMXLabel" runat="server" Text='<%# Bind("UTMX") %>' />
        <br />
        UTMY:
        <asp:Label ID="UTMYLabel" runat="server" Text='<%# Bind("UTMY") %>' />
        <br />
        Region:
        <asp:Label ID="RegionLabel" runat="server" Text='<%# Bind("Region") %>' />
        <br />
        Comments:
        <asp:Label ID="CommentsLabel" runat="server" Text='<%# Bind("Comments") %>' />
        <br />
        USGS:
        <asp:CheckBox ID="USGSCheckBox" runat="server" Checked='<%# Bind("USGS") %>' Enabled="false" />
        <br />
        StateEngineering:
        <asp:CheckBox ID="StateEngineeringCheckBox" runat="server" Checked='<%# Bind("StateEngineering") %>' Enabled="false" />
        <br />
        DateCreated:
        <asp:Label ID="DateCreatedLabel" runat="server" Text='<%# Bind("DateCreated") %>' />
        <br />
        UserCreated:
        <asp:Label ID="UserCreatedLabel" runat="server" Text='<%# Bind("UserCreated") %>' />
        <br />
        DateLastModified:
        <asp:Label ID="DateLastModifiedLabel" runat="server" Text='<%# Bind("DateLastModified") %>' />
        <br />
        UserLastModified:
        <asp:Label ID="UserLastModifiedLabel" runat="server" Text='<%# Bind("UserLastModified") %>' />
        <br />
        StoretUploaded:
        <asp:Label ID="StoretUploadedLabel" runat="server" Text='<%# Bind("StoretUploaded") %>' />
        <br />
        <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit" />
        &nbsp;<asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" />
        &nbsp;<asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" CommandName="New" Text="New" />
    </ItemTemplate>
    </asp:FormView>


    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RiverwatchDEV %>" 
        DeleteCommand="DELETE FROM [Station] WHERE [ID] = @ID" 
        InsertCommand="INSERT INTO [Station] ([StationNumber], [River], [AquaticModelIndex], [WaterCode], [WaterBodyID], [StationType], [QUADI], [Township], [Range], [Section], [QuaterSection], [Grid], [StationQUAD], [RWWaterShed], [WQCCWaterShed], [HydroUnit], [EcoRegion], [StationName], [StationStatus], [Elevation], [WaterShedRegion], [Longtitude], [Latitude], [County], [State], [NearCity], [Move], [Description], [UTMX], [UTMY], [Region], [Comments], [USGS], [StateEngineering], [DateCreated], [UserCreated], [DateLastModified], [UserLastModified], [StoretUploaded]) VALUES (@StationNumber, @River, @AquaticModelIndex, @WaterCode, @WaterBodyID, @StationType, @QUADI, @Township, @Range, @Section, @QuaterSection, @Grid, @StationQUAD, @RWWaterShed, @WQCCWaterShed, @HydroUnit, @EcoRegion, @StationName, @StationStatus, @Elevation, @WaterShedRegion, @Longtitude, @Latitude, @County, @State, @NearCity, @Move, @Description, @UTMX, @UTMY, @Region, @Comments, @USGS, @StateEngineering, @DateCreated, @UserCreated, @DateLastModified, @UserLastModified, @StoretUploaded)" 
        SelectCommand="SELECT * FROM [Station]" 
        UpdateCommand="UPDATE [Station] SET [StationNumber] = @StationNumber, [River] = @River, [AquaticModelIndex] = @AquaticModelIndex, [WaterCode] = @WaterCode, [WaterBodyID] = @WaterBodyID, [StationType] = @StationType, [QUADI] = @QUADI, [Township] = @Township, [Range] = @Range, [Section] = @Section, [QuaterSection] = @QuaterSection, [Grid] = @Grid, [StationQUAD] = @StationQUAD, [RWWaterShed] = @RWWaterShed, [WQCCWaterShed] = @WQCCWaterShed, [HydroUnit] = @HydroUnit, [EcoRegion] = @EcoRegion, [StationName] = @StationName, [StationStatus] = @StationStatus, [Elevation] = @Elevation, [WaterShedRegion] = @WaterShedRegion, [Longtitude] = @Longtitude, [Latitude] = @Latitude, [County] = @County, [State] = @State, [NearCity] = @NearCity, [Move] = @Move, [Description] = @Description, [UTMX] = @UTMX, [UTMY] = @UTMY, [Region] = @Region, [Comments] = @Comments, [USGS] = @USGS, [StateEngineering] = @StateEngineering, [DateCreated] = @DateCreated, [UserCreated] = @UserCreated, [DateLastModified] = @DateLastModified, [UserLastModified] = @UserLastModified, [StoretUploaded] = @StoretUploaded WHERE [ID] = @ID">
        <DeleteParameters>
            <asp:Parameter Name="ID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="StationNumber" Type="Int32" />
            <asp:Parameter Name="River" Type="String" />
            <asp:Parameter Name="AquaticModelIndex" Type="String" />
            <asp:Parameter Name="WaterCode" Type="String" />
            <asp:Parameter Name="WaterBodyID" Type="String" />
            <asp:Parameter Name="StationType" Type="String" />
            <asp:Parameter Name="QUADI" Type="String" />
            <asp:Parameter Name="Township" Type="String" />
            <asp:Parameter Name="Range" Type="String" />
            <asp:Parameter Name="Section" Type="Int32" />
            <asp:Parameter Name="QuaterSection" Type="String" />
            <asp:Parameter Name="Grid" Type="String" />
            <asp:Parameter Name="StationQUAD" Type="String" />
            <asp:Parameter Name="RWWaterShed" Type="String" />
            <asp:Parameter Name="WQCCWaterShed" Type="String" />
            <asp:Parameter Name="HydroUnit" Type="String" />
            <asp:Parameter Name="EcoRegion" Type="String" />
            <asp:Parameter Name="StationName" Type="String" />
            <asp:Parameter Name="StationStatus" Type="String" />
            <asp:Parameter Name="Elevation" Type="Double" />
            <asp:Parameter Name="WaterShedRegion" Type="String" />
            <asp:Parameter Name="Longtitude" Type="Double" />
            <asp:Parameter Name="Latitude" Type="Double" />
            <asp:Parameter Name="County" Type="String" />
            <asp:Parameter Name="State" Type="String" />
            <asp:Parameter Name="NearCity" Type="String" />
            <asp:Parameter Name="Move" Type="String" />
            <asp:Parameter Name="Description" Type="String" />
            <asp:Parameter Name="UTMX" Type="Double" />
            <asp:Parameter Name="UTMY" Type="Double" />
            <asp:Parameter Name="Region" Type="String" />
            <asp:Parameter Name="Comments" Type="String" />
            <asp:Parameter Name="USGS" Type="Boolean" />
            <asp:Parameter Name="StateEngineering" Type="Boolean" />
            <asp:Parameter Name="DateCreated" Type="DateTime" />
            <asp:Parameter Name="UserCreated" Type="String" />
            <asp:Parameter Name="DateLastModified" Type="DateTime" />
            <asp:Parameter Name="UserLastModified" Type="String" />
            <asp:Parameter Name="StoretUploaded" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="StationNumber" Type="Int32" />
            <asp:Parameter Name="River" Type="String" />
            <asp:Parameter Name="AquaticModelIndex" Type="String" />
            <asp:Parameter Name="WaterCode" Type="String" />
            <asp:Parameter Name="WaterBodyID" Type="String" />
            <asp:Parameter Name="StationType" Type="String" />
            <asp:Parameter Name="QUADI" Type="String" />
            <asp:Parameter Name="Township" Type="String" />
            <asp:Parameter Name="Range" Type="String" />
            <asp:Parameter Name="Section" Type="Int32" />
            <asp:Parameter Name="QuaterSection" Type="String" />
            <asp:Parameter Name="Grid" Type="String" />
            <asp:Parameter Name="StationQUAD" Type="String" />
            <asp:Parameter Name="RWWaterShed" Type="String" />
            <asp:Parameter Name="WQCCWaterShed" Type="String" />
            <asp:Parameter Name="HydroUnit" Type="String" />
            <asp:Parameter Name="EcoRegion" Type="String" />
            <asp:Parameter Name="StationName" Type="String" />
            <asp:Parameter Name="StationStatus" Type="String" />
            <asp:Parameter Name="Elevation" Type="Double" />
            <asp:Parameter Name="WaterShedRegion" Type="String" />
            <asp:Parameter Name="Longtitude" Type="Double" />
            <asp:Parameter Name="Latitude" Type="Double" />
            <asp:Parameter Name="County" Type="String" />
            <asp:Parameter Name="State" Type="String" />
            <asp:Parameter Name="NearCity" Type="String" />
            <asp:Parameter Name="Move" Type="String" />
            <asp:Parameter Name="Description" Type="String" />
            <asp:Parameter Name="UTMX" Type="Double" />
            <asp:Parameter Name="UTMY" Type="Double" />
            <asp:Parameter Name="Region" Type="String" />
            <asp:Parameter Name="Comments" Type="String" />
            <asp:Parameter Name="USGS" Type="Boolean" />
            <asp:Parameter Name="StateEngineering" Type="Boolean" />
            <asp:Parameter Name="DateCreated" Type="DateTime" />
            <asp:Parameter Name="UserCreated" Type="String" />
            <asp:Parameter Name="DateLastModified" Type="DateTime" />
            <asp:Parameter Name="UserLastModified" Type="String" />
            <asp:Parameter Name="StoretUploaded" Type="String" />
            <asp:Parameter Name="ID" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>


</asp:Content>
 