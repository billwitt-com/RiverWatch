<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ValidateNormals.aspx.cs" Inherits="RWInbound2.Validation.ValidateNormals" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Panel ID="pnlHelp"  BackColor="WhiteSmoke" runat="server" BorderColor="Blue" BorderStyle="Solid" BorderWidth="1px">
        Validation for Normal Samples:
        <br />
        The normal sample data is editable and the edits will be written to the final data.
        <br />
        The data values are colored RED if they exceed the saved limits (see EDIT tab for changing limits)<br /> The background colors are changed to blue for any sample where the Disolved values are larger than the Total values<br /> by the limit saved in the data tables.
        <br />
        A tooltip (flyover) is available for any text value and will help you understand the data.<br /> Just place your mouse over any value in the blank chemical data.
        <br />
        The SWAP button will exchange the dissolved and total values.
        <br />
        The accept button will update the data base and remove this sample from the list of samples<br /> that need to be validated.
        <br />
        The BAD button will move this sample data to a data table of bad samples.
        <br />
        Once the accept or BAD buttons are pressed, you will not be able to edit or change this data.
        <br />
        <br />
        <asp:Button ID="btnDone"  CssClass="adminButton" runat="server" OnClick="btnDone_Click" Text="Done" />

    </asp:Panel>

    <asp:Label ID="lblCount" runat="server" Text="Count"></asp:Label>
    <asp:Label ID="lblNote" runat="server" Text=""></asp:Label>
    <asp:Button ID="btnHelp" runat="server" Text="Help" CssClass="helpButton" OnClick="btnHelp_Click" Font-Size="0.95em" />

    <table style=" align-items: center; vertical-align:top; ">
        <tr >
            <td >              
                <asp:FormView ID="FormViewNormals" runat="server"  OnDataBound="FormViewNormals_DataBound" AllowPaging="true" DefaultMode="Edit" DataKeyNames="ID" DataSourceID="SqlDataSourceNormals"   >    
                     <EditItemTemplate>

                        <table id="topTable"  runat="server">
                            <tr>
                                <td> Code:
                                    <asp:TextBox ID="tbCode" runat="server" BackColor="Silver" ReadOnly="true" Text='<%# Bind("CODE") %>' />
                                </td>
                                <td style="width:49px;">
                                    
                                </td>
                                <td>Type:
                                    <asp:TextBox ID="tbType" runat="server" Text='<%# Bind("DUPLICATE") %>' />
                                </td>
                            </tr>
                            <tr style="line-height: 33px">
                                <td> AL_D:
                                    <asp:TextBox ID="AL_DTextBox" AutoPostBack="true"  OnTextChanged="TextBox_TextChanged" runat="server" Text='<%# Bind("AL_D","{0:0.00}") %>' />
                                </td>
                                <td style="width:40px;">
                                    <asp:Button ID="btnAL" OnCommand="btnSwap_Click" CommandArgument="<%=ID %>" runat="server" Text="SWAP" />
                                </td>
                                <td> AL_T:
                                    <asp:TextBox ID="AL_TTextBox" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" runat="server" Text='<%# Bind("AL_T","{0:0.00}") %>' />

                                </td>
                            </tr>
                            <tr>
                                <td>AS_D:
                                <asp:TextBox ID="AS_DTextBox" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" runat="server" Text='<%# Bind("AS_D","{0:0.00}") %>' />
                                </td>
                                <td>
                                    <asp:Button ID="btnAS" OnCommand="btnSwap_Click" CommandArgument="<%=ID %>" runat="server" Text="SWAP" />
                                </td>
                                <td>AS_T:
                            <asp:TextBox ID="AS_TTextBox" AutoPostBack="true"  runat="server" OnTextChanged="TextBox_TextChanged" Text='<%# Bind("AS_T","{0:0.00}") %>' />

                                </td>
                            </tr>
                            <tr>
                                <td>CA_D:
                            <asp:TextBox ID="CA_DTextBox" AutoPostBack="true"  runat="server" OnTextChanged="TextBox_TextChanged" Text='<%# Bind("CA_D","{0:0.00}") %>' />
                                </td>
                                <td>
                                    <asp:Button ID="btnCA" OnCommand="btnSwap_Click" CommandArgument="<%=ID %>" runat="server" Text="SWAP" />
                                </td>
                                <td>CA_T:
                            <asp:TextBox ID="CA_TTextBox" runat="server" OnTextChanged="TextBox_TextChanged" Text='<%# Bind("CA_T","{0:0.00}") %>' />
                                </td>
                            </tr>

                            <tr>
                                <td>CD_D:
                                <asp:TextBox ID="CD_DTextBox" AutoPostBack="true" OnTextChanged="TextBox_TextChanged"  runat="server" Text='<%# Bind("CD_D","{0:0.00}") %>' />
                                </td>
                                <td>
                                    <asp:Button ID="btnCD" OnCommand="btnSwap_Click" CommandArgument="<%=ID %>" runat="server" Text="SWAP" />
                                </td>
                                <td>CD_T:
                               <asp:TextBox ID="CD_TTextBox" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" runat="server" Text='<%# Bind("CD_T","{0:0.00}") %>' />
                                </td>
                            </tr>

                            <tr>
                                <td>CU_D:
                                <asp:TextBox ID="CU_DTextBox" AutoPostBack="true" OnTextChanged="TextBox_TextChanged"  runat="server" Text='<%# Bind("CU_D","{0:0.00}") %>' />
                                </td>
                                <td>
                                    <asp:Button ID="btnCU" OnCommand="btnSwap_Click" CommandArgument="<%=ID %>" runat="server" Text="SWAP" />
                                </td>
                                <td>CU_T:
                            <asp:TextBox ID="CU_TTextBox" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" runat="server" Text='<%# Bind("CU_T","{0:0.00}") %>' />
                                </td>
                            </tr>

                            <tr>
                                <td>FE_D:
                            <asp:TextBox ID="FE_DTextBox" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" runat="server" Text='<%# Bind("FE_D","{0:0.00}") %>' />
                                </td>
                                 <td>
                                    <asp:Button ID="btnFE" OnCommand="btnSwap_Click" CommandArgument="<%=ID %>" runat="server" Text="SWAP" />
                                </td>
                                <td>FE_T:
                             <asp:TextBox ID="FE_TTextBox" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" runat="server" Text='<%# Bind("FE_T","{0:0.00}") %>' /></td>
                            </tr>

                            <tr>
                                <td>PB_D:
                                    <asp:TextBox ID="PB_DTextBox" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" runat="server" Text='<%# Bind("PB_D","{0:0.00}") %>' />
                                </td>
                                 <td>
                                    <asp:Button ID="btnPB" OnCommand="btnSwap_Click" CommandArgument="<%=ID %>" runat="server" Text="SWAP" />
                                </td>
                                <td>PB_T:
                                    <asp:TextBox ID="PB_TTextBox" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" runat="server" Text='<%# Bind("PB_T","{0:0.00}") %>' />
                                </td>
                            </tr>

                            <tr>
                                <td>MG_D:
                            <asp:TextBox ID="MG_DTextBox" AutoPostBack="true" OnTextChanged="TextBox_TextChanged"  runat="server" Text='<%# Bind("MG_D","{0:0.00}") %>' />
                              </td> 
                                 <td>
                                    <asp:Button ID="btnMG" OnCommand="btnSwap_Click" CommandArgument="<%=ID %>" runat="server" Text="SWAP" />
                                </td>
                                <td>MG_T:
                            <asp:TextBox ID="MG_TTextBox" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" runat="server" Text='<%# Bind("MG_T","{0:0.00}") %>' />
                                </td>
                            </tr>
      
                            <tr>
                                <td>MN_D:
                                    <asp:TextBox ID="MN_DTextBox" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" runat="server" Text='<%# Bind("MN_D","{0:0.00}") %>' />
                                </td>
                                <td>
                                    <asp:Button ID="btnMN" OnCommand="btnSwap_Click" CommandArgument="<%=ID %>" runat="server" Text="SWAP" />
                                </td>
                                <td>MN_T:
                                    <asp:TextBox ID="MN_TTextBox" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" runat="server" Text='<%# Bind("MN_T","{0:0.00}") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td>SE_D:
                                    <asp:TextBox ID="SE_DTextBox" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" runat="server" Text='<%# Bind("SE_D","{0:0.00}") %>' />

                                </td>
                                <td>
                                    <asp:Button ID="btnSE" OnCommand="btnSwap_Click" CommandArgument="<%=ID %>" runat="server" Text="SWAP" />
                                </td>
                                <td>SE_T:
                                    <asp:TextBox ID="SE_TTextBox" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" runat="server" Text='<%# Bind("SE_T","{0:0.00}") %>' /></td>
                            </tr>

                            <tr>
                                <td>ZN_D:
                                    <asp:TextBox ID="ZN_DTextBox" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" runat="server" Text='<%# Bind("ZN_D","{0:0.00}") %>' />

                                </td>
                                <td>
                                    <asp:Button ID="btnZN" OnCommand="btnSwap_Click" CommandArgument="<%=ID %>" runat="server" Text="SWAP" />
                                </td>
                                <td>ZN_T:
                                    <asp:TextBox ID="ZN_TTextBox" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" runat="server" Text='<%# Bind("ZN_T","{0:0.00}") %>' />
                                </td>
                            </tr>

                            <tr>
                                <td>NA_D:
                                    <asp:TextBox ID="NA_DTextBox" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" runat="server" Text='<%# Bind("NA_D","{0:0.00}") %>' />
                                </td>
                                <td>
                                    <asp:Button ID="btnNA" OnCommand="btnSwap_Click" CommandArgument="<%=ID %>" runat="server" Text="SWAP" />
                                </td>
                                <td>NA_T:
                                    <asp:TextBox ID="NA_TTextBox" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" runat="server" Text='<%# Bind("NA_T","{0:0.00}") %>' />
                                 </td>
                            </tr>
                            <tr>
                                <td>K_D:
                            <asp:TextBox ID="K_DTextBox" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" runat="server" Text='<%# Bind("K_D","{0:0.00}") %>' />
                                </td>
                                 <td>
                                    <asp:Button ID="btnK" OnCommand="btnSwap_Click" CommandArgument="<%=ID %>" runat="server" Text="SWAP" />
                                </td>
                                <td>K_T:
                                    <asp:TextBox ID="K_TTextBox" AutoPostBack="true" OnTextChanged="TextBox_TextChanged"  runat="server" Text='<%# Bind("K_T","{0:0.00}") %>' />
                                </td>
                            </tr>
                        </table>
                         ANADATE:
                        <asp:TextBox ID="ANADATETextBox" runat="server" Text='<%# Bind("ANADATE", "{0:d}") %>' />
                        <br />
                        COMPLETE:
                        <asp:CheckBox ID="COMPLETECheckBox" runat="server" Checked='<%# Bind("COMPLETE") %>' />
                        <br />
                        DATE_SENT:
                        <asp:TextBox ID="DATE_SENTTextBox" runat="server" Text='<%# Bind("DATE_SENT", "{0:d}") %>' />
                        <br />
                        Comments:
                        <asp:TextBox ID="CommentsTextBox" TextMode="MultiLine"  Width="200px" runat="server" Text='<%# Bind("Comments") %>' />
                        <br />                      

                        SampleID:
                        <asp:TextBox ID="tblSampleIDTextBox"  ReadOnly="true" runat="server" Text='<%# Bind("tblSampleID") %>' />
                        <br />
                     <%--   <asp:Button ID="Button1" CssClass="adminButton" runat="server" CausesValidation="True" OnClick="UpdateButton_Click"  Text="Accept" />--%>
                        <asp:Button ID="NormalUpdateButton" Width="60px" OnClick="UpdateButton_Click" runat="server" CssClass="adminButton" CausesValidation="True" CommandName="Update" Text="Accept" />
                       <%--<asp:Button ID="btnSaveEdit" Width="80px" runat="server" CssClass="adminButton"   OnClick="btnSaveEdit_Click"   CausesValidation="True" Text="SaveEdit" />--%>

                         <asp:Button ID="btnBadNormal" Width="80px" runat="server" CssClass="adminButton"  OnClick="btnBadNormal_Click" CausesValidation="True" Text="BAD" />

<%--                         &nbsp;<asp:Button ID="UpdateCancelButton" Width="80px" CssClass="adminButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />--%>
                     </EditItemTemplate>
                </asp:FormView>
            </td>           
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlDataSourceNormals" runat="server" ConnectionString="<%$ ConnectionStrings:RiverWatchConnectionString %>"   OnUpdating="SqlDataSourceNormals_Updating"
         
        UpdateCommand="UPDATE [InboundICPFinal] SET [CODE] = @CODE, [tblSampleID] = @tblSampleID, [DUPLICATE] = @DUPLICATE, [AL_D] = @AL_D, [AL_T] = @AL_T, 
        [AS_D] = @AS_D, [AS_T] = @AS_T, [CA_D] = @CA_D, [CA_T] = @CA_T, [CD_D] = @CD_D, [CD_T] = @CD_T, [CU_D] = @CU_D, [CU_T] = @CU_T, [FE_D] = @FE_D, [FE_T] = @FE_T, 
        [PB_D] = @PB_D, [PB_T] = @PB_T, [MG_D] = @MG_D, [MG_T] = @MG_T, [MN_D] = @MN_D, [MN_T] = @MN_T, [SE_D] = @SE_D, [SE_T] = @SE_T, [ZN_D] = @ZN_D, [ZN_T] = @ZN_T, 
        [NA_D] = @NA_D, [NA_T] = @NA_T, [K_D] = @K_D, [K_T] = @K_T, [ANADATE] = @ANADATE, [COMPLETE] = @COMPLETE, [DATE_SENT] = @DATE_SENT, [Comments] = @Comments       
        WHERE [ID] = @ID">
<%-- we fill these in in code  [CreatedBy] = @CreatedBy, [CreatedDate] = @CreatedDate, [Valid] = @Valid, [Edited] = @Edited, [Saved] = @Saved --%>
       
        <UpdateParameters>
            <asp:Parameter Name="CODE" Type="String" />
            <asp:Parameter Name="tblSampleID" Type="Int32" />
            <asp:Parameter Name="DUPLICATE" Type="String" />
            <asp:Parameter Name="AL_D" Type="Decimal" />
            <asp:Parameter Name="AL_T" Type="Decimal" />
            <asp:Parameter Name="AS_D" Type="Decimal" />
            <asp:Parameter Name="AS_T" Type="Decimal" />
            <asp:Parameter Name="CA_D" Type="Decimal" />
            <asp:Parameter Name="CA_T" Type="Decimal" />
            <asp:Parameter Name="CD_D" Type="Decimal" />
            <asp:Parameter Name="CD_T" Type="Decimal" />
            <asp:Parameter Name="CU_D" Type="Decimal" />
            <asp:Parameter Name="CU_T" Type="Decimal" />
            <asp:Parameter Name="FE_D" Type="Decimal" />
            <asp:Parameter Name="FE_T" Type="Decimal" />
            <asp:Parameter Name="PB_D" Type="Decimal" />
            <asp:Parameter Name="PB_T" Type="Decimal" />
            <asp:Parameter Name="MG_D" Type="Decimal" />
            <asp:Parameter Name="MG_T" Type="Decimal" />
            <asp:Parameter Name="MN_D" Type="Decimal" />
            <asp:Parameter Name="MN_T" Type="Decimal" />
            <asp:Parameter Name="SE_D" Type="Decimal" />
            <asp:Parameter Name="SE_T" Type="Decimal" />
            <asp:Parameter Name="ZN_D" Type="Decimal" />
            <asp:Parameter Name="ZN_T" Type="Decimal" />
            <asp:Parameter Name="NA_D" Type="Decimal" />
            <asp:Parameter Name="NA_T" Type="Decimal" />
            <asp:Parameter Name="K_D" Type="Decimal" />
            <asp:Parameter Name="K_T" Type="Decimal" />
            <asp:Parameter Name="ANADATE" Type="DateTime" />
            <asp:Parameter Name="COMPLETE" Type="Boolean" />
            <asp:Parameter Name="DATE_SENT" Type="DateTime" />
            <asp:Parameter Name="Comments" Type="String" />
            <asp:Parameter Name="CreatedBy" Type="String" />
            <asp:Parameter Name="CreatedDate" Type="DateTime" />
            <asp:Parameter Name="Valid" Type="Boolean" />
            <asp:Parameter Name="Edited" Type="Boolean" />
            <asp:Parameter Name="Saved" Type="Boolean" />
            <asp:Parameter Name="ID" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>

   
</asp:Content>