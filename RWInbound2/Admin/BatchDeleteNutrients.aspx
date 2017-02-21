<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BatchDeleteNutrients.aspx.cs" Inherits="RWInbound2.Admin.BatchDeleteNutrients" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <asp:Label ID="Label1" runat="server" CssClass="PageLabel" Text="Batch Delete Nutrients"></asp:Label>
    <br />
    <br />
        <asp:Label ID="Label2" runat="server" Text="Enter Batch Number:  "></asp:Label>
    <asp:TextBox ID="tbSelectBatch" runat="server"></asp:TextBox>
                    <ajaxToolkit:AutoCompleteExtender ID="tbSelectBatch_AutoCompleteExtender" runat="server" TargetControlID="tbSelectBatch" 
                            ServiceMethod="SearchBatchNumbers" CompletionSetCount="2" MinimumPrefixLength="1">
                </ajaxToolkit:AutoCompleteExtender>

        <asp:Button ID="btnSelect" runat="server" CssClass="adminButton" Text="Select" OnClick="btnSelect_Click" />
        <br />
    <asp:Label ID="lblMsg" runat="server" ></asp:Label>

        <br />
        <br />
        <asp:Panel ID="pnlResults" runat="server">

            <br />
            <asp:Label ID="lblTotalResults" runat="server"></asp:Label>
            <br />
            <br />
            <asp:Label ID="lblResults" runat="server"></asp:Label>

            <br />
            <br />
            <asp:Label ID="lblBlankDups" runat="server"></asp:Label>
            <br />
            <br />
            <asp:Label ID="lblCanBeDeleted" runat="server"></asp:Label>

            <br />
            <br />
            <asp:Button ID="btnDelete" CssClass="adminButton" runat="server" Text="Delete" OnClick="btnDelete_Click" />
            <asp:Button ID="btnCancel" runat="server" CssClass="adminButton" OnClick="btnCancel_Click" Text="Cancel" />
            <br />
            <br />
        </asp:Panel>
    <asp:Panel ID="pnlAreYouSure" runat="server">

        <asp:Label ID="Label3" runat="server" Text="Are You Sure ?  "></asp:Label>
        
        <asp:Button ID="btnYes" runat="server" Text="YES" OnClick="btnYes_Click" /><asp:Button ID="btnNo" runat="server" Text="NO" OnClick="btnNo_Click" />

    </asp:Panel>
    <asp:Panel ID="pnlConfirm" runat="server">

        <asp:Label ID="lblConfirm" runat="server" ></asp:Label> <asp:Button ID="btnConfirm" runat="server" Text="OK" OnClick="btnConfirm_Click" />


    </asp:Panel>


</asp:Content>
