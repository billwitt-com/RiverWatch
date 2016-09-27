<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UnknownData.aspx.cs" Inherits="RWInbound2.Public.UnknownData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label1" CssClass="PageLabel" runat="server" Text="Colorado Riverwatch Volunteer Unknown Data Entry Form"></asp:Label>

    <asp:Panel ID="pnlLogin" runat="server">

        <br />

        <table style="width: 100%">
            <tr>
                <td style="width: 36px; height: 17px;"></td>
                <td style="width: 193px; height: 17px;"></td>
                <td style="width: 260px; height: 17px;"></td>
                <td style="height: 17px"></td>
            </tr>
            <tr>
                <td style="width: 36px">&nbsp;</td>
                <td style="width: 193px">
                    <asp:Label ID="Label2" runat="server" Text="Kit Number: "></asp:Label>
                    <asp:TextBox ID="tbKitNumber" runat="server" BorderColor="#66CCFF" BorderStyle="Solid" BorderWidth="1px" Height="20px" Width="46px"></asp:TextBox>
                </td>

                <td style="width: 260px">
                    <asp:Label ID="Label3" runat="server" Text="Password: "></asp:Label>
                    <asp:TextBox ID="tbOrgPwd" runat="server" Height="20px" Width="144px"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" Text="Log In" />
                </td>
            </tr>

        </table>


    </asp:Panel>
    <br />
    <asp:Panel ID="pnlData" runat="server" >
        <table style="width: 100%">
            <tr>
                <td style="width: 34px">&nbsp;</td>
                <td>
                    <asp:Label ID="lblOrgName" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblKitNumber" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
                <br />
        <table style="width: 100%">
            <tr>
                <td style="width: 33px">&nbsp;</td>
                <td style="width: 166px">
                    <asp:Label ID="Label17" runat="server" Text="Test Date: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbTestDate" runat="server"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="tbTestDate_CalendarExtender"  runat="server" TargetControlID="tbTestDate" />
                </td>
            </tr>
            <tr>
                <td style="width: 33px">&nbsp;</td>
                <td style="width: 166px">
                    <asp:Label ID="Label5" runat="server" Text="Ph"></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 33px">&nbsp;</td>
                <td style="width: 166px">
                    <asp:Label ID="Label4" runat="server" Text="pH Batch Number: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbpHBatchNumber" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 33px">&nbsp;</td>
                <td style="width: 166px">
                    <asp:Label ID="Label6" runat="server" Text="Ph 1: "></asp:Label>
                </td>
                <td>                   
                    <asp:TextBox ID="tbpH1" runat="server"></asp:TextBox>
                    <asp:RangeValidator ID="RangeValidator1" runat="server"  ControlToValidate="tbpH1" MinimumValue="0"  Type="Double" 
                        ErrorMessage="Please enter a decimal value"></asp:RangeValidator>
                </td>
            </tr>
            <tr>
                <td style="width: 33px">&nbsp;</td>
                <td style="width: 166px">
                    <asp:Label ID="Label7" runat="server" Text="pH 2: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbpH2" runat="server"></asp:TextBox>
                    <asp:RangeValidator ID="RangeValidator2" runat="server"  ControlToValidate="tbpH2" MinimumValue="0"  Type="Double" 
                        ErrorMessage="Please enter a decimal value"></asp:RangeValidator>
                </td>
            </tr>
            <tr>
                <td style="width: 33px">&nbsp;</td>
                <td style="width: 166px">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>

        <%-- now add tabole for alk--%>

        <table style="width: 100%">
            <tr>
                <td style="width: 33px">&nbsp;</td>
                <td style="width: 166px">
                    <asp:Label ID="Label8" runat="server" Text="Alkalinity"></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 33px">&nbsp;</td>
                <td style="width: 166px">
                    <asp:Label ID="Label9" runat="server" Text="Alk Batch Number: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbAlkBatchNumber" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 33px">&nbsp;</td>
                <td style="width: 166px">
                    <asp:Label ID="Label10" runat="server" Text="Alk 1: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbAlk1" runat="server"></asp:TextBox>
                    <asp:RangeValidator ID="RangeValidator3" runat="server"  ControlToValidate="tbAlk1" MinimumValue="0"  Type="Double" 
                        ErrorMessage="Please enter a decimal value"></asp:RangeValidator>
                </td>
            </tr>
            <tr>
                <td style="width: 33px">&nbsp;</td>
                <td style="width: 166px">
                    <asp:Label ID="Label11" runat="server" Text="Alk 2: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbAlk2" runat="server"></asp:TextBox>
                    <asp:RangeValidator ID="RangeValidator4" runat="server"  ControlToValidate="tbAlk2" MinimumValue="0"  Type="Double" 
                        ErrorMessage="Please enter a decimal value"></asp:RangeValidator>
                </td>
            </tr>
            <tr>
                <td style="width: 33px">&nbsp;</td>
                <td style="width: 166px">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>

        <%--             And table for hardness--%>

        <table style="width: 100%">
            <tr>
                <td style="width: 33px">&nbsp;</td>
                <td style="width: 166px">
                    <asp:Label ID="Label12" runat="server" Text="Hardness"></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 33px">&nbsp;</td>
                <td style="width: 166px">
                    <asp:Label ID="Label13" runat="server" Text="Hardness Batch Number: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbHardnessBatchNumber" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 33px">&nbsp;</td>
                <td style="width: 166px">
                    <asp:Label ID="Label14" runat="server" Text="Hardness 1: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbHard1" runat="server"></asp:TextBox>
                     <asp:RangeValidator ID="RangeValidator5" runat="server"  ControlToValidate="tbHard1" MinimumValue="0"  Type="Double" 
                        ErrorMessage="Please enter a decimal value"></asp:RangeValidator>
                </td>
            </tr>
            <tr>
                <td style="width: 33px">&nbsp;</td>
                <td style="width: 166px">
                    <asp:Label ID="Label15" runat="server" Text="Hardness 2: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbHard2" runat="server"></asp:TextBox>
                     <asp:RangeValidator ID="RangeValidator6" runat="server"  ControlToValidate="tbHard2" MinimumValue="0"  Type="Double" 
                        ErrorMessage="Please enter a decimal value"></asp:RangeValidator>

                </td>
            </tr>
            <tr>
                <td style="width: 33px">&nbsp;</td>
                <td style="width: 166px">
                    <asp:Label ID="Label16" runat="server" Text="Comments: "></asp:Label>

                </td>
                <td>
                    <asp:TextBox ID="tbComments" TextMode="MultiLine" runat="server" Height="61px" Width="529px"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td style="width: 33px">&nbsp;</td>
                <td style="width: 166px">
                 
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                </td>
                <td>

                    <asp:Label ID="lblMessage0" runat="server" Text=""></asp:Label>

                </td>
            </tr>
        </table>

        <br />

    </asp:Panel>
            <br />
            <br />
</asp:Content>
