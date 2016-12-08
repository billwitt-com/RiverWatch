<%@ Page Title="Reports" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="RWInbound2.Reports.Reports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="site-title">
        <asp:Label ID="Label1" runat="server" CssClass =" PageLabel" Text="Reports"></asp:Label>
    </div>
    <asp:Table ID="Table1" 
                runat="server" 
                CssClass="edit-table" >

        <asp:TableRow CssClass="edit-table-row">
            <asp:TableCell CssClass="edit-table-cell">
                <asp:Button ID="btnErrorLogReport" runat="server" Text="Error Log" OnClick="btnErrorLogReport_Click"
                            CssClass="adminButton" />
            </asp:TableCell>  
            <asp:TableCell CssClass="edit-table-cell">
                <asp:Button ID="btnLachatNoBC" runat="server" Text="Lachat BC not Entered" OnClick="btnLachatNoBC_Click"
                            CssClass="adminButton" />
            </asp:TableCell> 
            <asp:TableCell CssClass="edit-table-cell"> 
                <asp:Button ID="btnMailingList" runat="server" Text="Mailing List" OnClick="btnMailingList_Click" 
                            CssClass="adminButton"/>          
            </asp:TableCell>   
             <asp:TableCell CssClass="edit-table-cell">
                <asp:Button ID="btnMetalBarCodes" runat="server" Text="Metal Bar Codes" OnClick="btnMetalBarCodes_Click" 
                            CssClass="adminButton"/>
            </asp:TableCell>
            <asp:TableCell CssClass="edit-table-cell">
                <asp:Button ID="btnOrganizations" runat="server" Text="Organizations" OnClick="btnOrganizations_Click"
                            CssClass="adminButton" />
            </asp:TableCell>             
        </asp:TableRow>

        <asp:TableRow CssClass="edit-table-row">
            <asp:TableCell CssClass="edit-table-cell">
                <asp:Button ID="btnOrgStations" runat="server" Text="Organization Stations" OnClick="btnOrgStations_Click" 
                            CssClass="adminButton"/>                
            </asp:TableCell>     
            <asp:TableCell CssClass="edit-table-cell">                
                <asp:Button ID="btnOrgStatus" runat="server" Text="Organization Status" OnClick="btnOrgStatus_Click" 
                            CssClass="adminButton"/>
            </asp:TableCell>  
            <asp:TableCell CssClass="edit-table-cell">
                <asp:Button ID="btnParticipants" runat="server" Text="Participants" OnClick="btnParticipants_Click" 
                            CssClass="adminButton"/>
            </asp:TableCell> 
             <asp:TableCell CssClass="edit-table-cell">
                <asp:Button ID="btnParticipantsPrimaryContacts" runat="server" Text="Participants - Primary Contacts" OnClick="btnParticipantsPrimaryContacts_Click"
                            CssClass="adminButton"/>
            </asp:TableCell>
            <asp:TableCell CssClass="edit-table-cell">
                <asp:Button ID="btnOrgUnknownResults" runat="server" Text="Organization Unknown Results" OnClick="btnOrgUnknownResults_Click"
                            CssClass="adminButton"/>  
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow CssClass="edit-table-row">
            <asp:TableCell CssClass="edit-table-cell">
                <asp:Button ID="btnPublicUsers" runat="server" Text="Public Users" OnClick="btnPublicUsers_Click" 
                            CssClass="adminButton"/>
            </asp:TableCell>             

                        <asp:TableCell CssClass="edit-table-cell">
                <asp:Button ID="btnQAQC" runat="server" Text="QAQC Report" OnClick="btnQAQC_Click" 
                            CssClass="adminButton"/>
            </asp:TableCell>             

            <asp:TableCell CssClass="edit-table-cell">           
                <asp:Button ID="btnSamples" runat="server" Text="Samples" OnClick="btnSamples_Click" 
                            CssClass="adminButton"/>
            </asp:TableCell>     
            <asp:TableCell CssClass="edit-table-cell"> 
                <asp:Button ID="btnStations" runat="server" Text="Stations" OnClick="btnStations_Click" 
                            CssClass="adminButton"/>
            </asp:TableCell>  
            <asp:TableCell CssClass="edit-table-cell">
                <asp:Button ID="btnStationsWithGauges" runat="server" Text="Stations With Gauges" OnClick="btnStationsWithGauges_Click" 
                            CssClass="adminButton"/>    
            </asp:TableCell> 
             <asp:TableCell CssClass="edit-table-cell">                 
            </asp:TableCell>
        </asp:TableRow>

        <%--<asp:TableRow CssClass="edit-table-row">
            <asp:TableCell CssClass="edit-table-cell"> 
            </asp:TableCell>  
            <asp:TableCell CssClass="edit-table-cell">
            </asp:TableCell> 
             <asp:TableCell CssClass="edit-table-cell">
            </asp:TableCell>
            <asp:TableCell CssClass="edit-table-cell">
            </asp:TableCell>             
            <asp:TableCell CssClass="edit-table-cell">           
            </asp:TableCell>     
        </asp:TableRow>--%>
    </asp:Table> 
</asp:Content>
