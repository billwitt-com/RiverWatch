﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="RWInbound2.Admin.Admin" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p class="site-title">
        Administration</p>
    <p >
    </p>
    <asp:Panel ID="pnlQuickview"  GroupingText="hello world" runat="server" BackColor="#FFFFCC"  CssClass="panelgrouping"
          Height="124px" Width="430px" ForeColor="Black"  >
        <div  style="text-align:center">
       
        <asp:Label ID="Label3" runat="server" Font-Bold="True" Text="Quick View"></asp:Label>
             </div>
        <table style="width: 90%">
            <tr>
                <td style="width: 145px">
                    <asp:Label ID="Label1" runat="server" Text="Kit Number :"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" CssClass="smallButton" Height="22px" OnClick="btnSearch_Click" Text="Search" />
                </td>
            </tr>
            <tr>
                <td style="width: 145px">
                    <asp:Label ID="Label4" runat="server" Text="Station Name :"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button8" runat="server" CssClass="smallButton" OnClick="btnSearch_Click" Text="Search" />
                </td>
            </tr>
            <tr>
                <td style="width: 145px">
                    <asp:Label ID="Label2" runat="server" Text="Org Name :"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbOrgName" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button9" runat="server" CssClass="smallButton" OnClick="btnSearch_Click" Text="Search" />
                </td>
            </tr>
        </table>
        <br />

        <br />
        <br />

    </asp:Panel>
    <ajaxToolkit:DropShadowExtender ID="pnlQuickview_DropShadowExtender" runat="server" 
        BehaviorID="pnlQuickview_DropShadowExtender" TargetControlID="pnlQuickview">
    </ajaxToolkit:DropShadowExtender>


        <table style="width: 100%" >
               <tr>
                <td>&nbsp;</td>
                   </tr>
            <tr>
                <td style="height: 18px"></td>
                <td style="width: 262px; height: 18px;">        
                    <asp:Button ID="Button6" class="adminButton" runat="server" Text="Manage Organizations" Height="27px" /></td>
                <td style="height: 18px">
        <asp:Button ID="Button5" runat="server" Text="Manage Stations" CssClass="adminButton" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td style="width: 262px">
        <asp:Button ID="Button7" runat="server" class="adminButton"  Text="Enter Unknown " />
                </td>
                <td>
        <asp:Button ID="Button3" runat="server" class="adminButton"  Enabled="False" Text="Edit Raw Data" CssClass="adminButton" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td style="width: 262px">
        <asp:Button ID="Button4" class="adminButton"  runat="server" Text="Manage Participants" />
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td style="width: 262px">
        <asp:Button ID="Button1" class="adminButton"  runat="server" Enabled="False" Text="Manage Users" CssClass="adminButton" />
                </td>
                <td>
        <asp:Button ID="Button2" class="adminButton"  runat="server" Enabled="False" Text="Manage Permissions" CssClass="adminButton" />


                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td style="width: 262px">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td style="width: 262px">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>


   
  
</asp:Content>
