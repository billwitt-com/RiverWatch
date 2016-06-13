<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ValidateBlanks.aspx.cs" Inherits="RWInbound2.Validation.ValidateBlanks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Label ID="lblCount" runat="server" Text="Count"></asp:Label>
    <asp:Label ID="lblNote" runat="server" Text="Note:"></asp:Label>



    <table style=" align-items: center; vertical-align:top; ">
        <tr >
            <td >
                <asp:FormView ID="FormViewBlank" runat="server" AllowPaging="true" DefaultMode="Edit" DataKeyNames="inbICPID" DataSourceID="SqlDataSourceBlanks" OnPageIndexChanged="FormViewBlank_PageIndexChanged">
                    <EditItemTemplate>
                        <table id="VeryTop" runat="server">
                            <tr>
                                <%--<td>inbICPID:
                <asp:TextBox ID="inbICPIDTextBox" runat="server" ReadOnly="true" Text='<%# Bind("inbICPID") %>' />

                        </td>--%>
                                <td>Barcode:
                <asp:TextBox ID="CODETextBox" runat="server" ReadOnly="true" Text='<%# Bind("CODE") %>' />
                                </td>
                                <td>SampleType:
                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("DUPLICATE") %>' />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table id="topTable" runat="server">

                            <tr style="line-height: 33px">
                                <td>AL_D:
                            <asp:TextBox ID="AL_DTextBox" runat="server" Text='<%# Bind("AL_D","{0:0.0000}") %>' />

                                </td>
                                <td>
                                    <asp:Button ID="btnAL" CssClass="buttonSWAP" OnCommand="Button1_Click" runat="server" Text="SWAP" />
                                </td>
                                <td>AL_T:
                            <asp:TextBox ID="AL_TTextBox" runat="server" Text='<%# Bind("AL_T","{0:0.0000}") %>' />

                                </td>
                            </tr>
                            <tr>
                                <td>AS_D:
                                <asp:TextBox ID="AS_DTextBox" runat="server" Text='<%# Bind("AS_D","{0:0.0000}") %>' />
                                </td>
                                <td>
                                    <asp:Button ID="btnAS" OnCommand="Button1_Click" CommandArgument="<%=ID %>" runat="server" Text="SWAP" />
                                </td>
                                <td>AS_T:
                            <asp:TextBox ID="AS_TTextBox" runat="server" Text='<%# Bind("AS_T","{0:0.0000}") %>' AutoPostBack="False" /></td>
                            </tr>
                            <tr>
                                <td>CA_D:
                            <asp:TextBox ID="CA_DTextBox" runat="server" Text='<%# Bind("CA_D","{0:0.0000}") %>' />
                                </td>
                                <td>
                                    <asp:Button ID="btnCA" OnCommand="Button1_Click" CommandArgument="<%=ID %>" runat="server" Text="SWAP" />
                                </td>
                                <td>CA_T:
                            <asp:TextBox ID="CA_TTextBox" runat="server" Text='<%# Bind("CA_T","{0:0.0000}") %>' />
                                </td>
                            </tr>

                            <tr>
                                <td>CD_D:
                                <asp:TextBox ID="CD_DTextBox" runat="server" Text='<%# Bind("CD_D","{0:0.0000}") %>' />
                                </td>
                                <td>
                                    <asp:Button ID="btnCD" OnCommand="Button1_Click" CommandArgument="<%=ID %>" runat="server" Text="SWAP" />
                                </td>
                                <td>CD_T:
                               <asp:TextBox ID="CD_TTextBox" runat="server" Text='<%# Bind("CD_T","{0:0.0000}") %>' />
                                </td>
                            </tr>

                            <tr>
                                <td>CU_D:
                                <asp:TextBox ID="CU_DTextBox" runat="server" Text='<%# Bind("CU_D","{0:0.0000}") %>' />
                                </td>
                                <td>
                                    <asp:Button ID="btnCU" OnCommand="Button1_Click" CommandArgument="<%=ID %>" runat="server" Text="SWAP" />
                                </td>
                                <td>CU_T:
                            <asp:TextBox ID="CU_TTextBox" runat="server" Text='<%# Bind("CU_T","{0:0.0000}") %>' />
                                </td>
                            </tr>

                            <tr>
                                <td>FE_D:
                            <asp:TextBox ID="FE_DTextBox" runat="server" Text='<%# Bind("FE_D","{0:0.0000}") %>' />
                                </td>
                                <td>
                                    <asp:Button ID="btnFE" OnCommand="Button1_Click" CommandArgument="<%=ID %>" runat="server" Text="SWAP" />
                                </td>
                                <td>FE_T:
                             <asp:TextBox ID="FE_TTextBox" runat="server" Text='<%# Bind("FE_T","{0:0.0000}") %>' /></td>
                            </tr>

                           
                            <tr>
                                <td>PB_D:
                            <asp:TextBox ID="PB_DTextBox" runat="server" Text='<%# Bind("PB_D","{0:0.0000}") %>' />
                                    <td>
                                        <asp:Button ID="btnPB" OnCommand="Button1_Click" CommandArgument="<%=ID %>" runat="server" Text="SWAP" />
                                    </td>
                                    <td>PB_T:
                            <asp:TextBox ID="PB_TTextBox" runat="server" Text='<%# Bind("PB_T","{0:0.0000}") %>' /></td>
                                </td>
                            </tr>
                            <tr>
                                <td>MG_D:
                            <asp:TextBox ID="MG_DTextBox" runat="server" Text='<%# Bind("MG_D","{0:0.0000}") %>' />
                                </td>
                                <td>
                                    <asp:Button ID="btnMG" OnCommand="Button1_Click" CommandArgument="<%=ID %>" runat="server" Text="SWAP" />
                                </td>
                                <td>MG_T:
                            <asp:TextBox ID="MG_TTextBox" runat="server" Text='<%# Bind("MG_T","{0:0.0000}") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td>MN_D:
                            <asp:TextBox ID="MN_DTextBox" runat="server" Text='<%# Bind("MN_D","{0:0.0000}") %>' /></td>
                                <td>
                                    <asp:Button ID="btnMN" OnCommand="Button1_Click" CommandArgument="<%=ID %>" runat="server" Text="SWAP" />
                                </td>
                                <td>MN_T:
                            <asp:TextBox ID="MN_TTextBox" runat="server" Text='<%# Bind("MN_T","{0:0.0000}") %>' /></td>
                            </tr>
                            <tr>
                                <td>SE_D:
                            <asp:TextBox ID="SE_DTextBox" runat="server" Text='<%# Bind("SE_D","{0:0.0000}") %>' /></td>
                                <td>
                                    <asp:Button ID="btnSE" OnCommand="Button1_Click" CommandArgument="<%=ID %>" runat="server" Text="SWAP" />
                                </td>
                                <td>SE_T:
                            <asp:TextBox ID="SE_TTextBox" runat="server" Text='<%# Bind("SE_T","{0:0.0000}") %>' /></td>
                            </tr>

                            <tr>
                                <td>ZN_D:
                            <asp:TextBox ID="ZN_DTextBox" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" runat="server" Text='<%# Bind("ZN_D","{0:0.0000}") %>' /></td>
                                <td>
                                    <asp:Button ID="btnZN" OnCommand="Button1_Click" CommandArgument="<%=ID %>" runat="server" Text="SWAP" />
                                </td>
                                <td>ZN_T:
                            <asp:TextBox ID="ZN_TTextBox"  AutoPostBack="true" OnTextChanged="TextBox_TextChanged" runat="server" Text='<%# Bind("ZN_T","{0:0.0000}") %>' /></td>
                            </tr>
                            <tr>
                                <td>NA_D:
                            <asp:TextBox ID="NA_DTextBox" runat="server" Text='<%# Bind("NA_D","{0:0.0000}") %>' /></td>
                                <td>
                                    <asp:Button ID="btnNA" OnCommand="Button1_Click" CommandArgument="<%=ID %>" runat="server" Text="SWAP" />
                                </td>
                                <td>NA_T:
                            <asp:TextBox ID="NA_TTextBox" runat="server" Text='<%# Bind("NA_T","{0:0.0000}") %>' /></td>
                            </tr>
                            <tr>
                                <td>K_D :
                            <asp:TextBox ID="K_DTextBox" runat="server" Text='<%# Bind("K_D","{0:0.0000}") %>' /></td>
                                <td>
                                    <asp:Button ID="btnK" OnCommand="Button1_Click" CommandArgument="<%=ID %>" runat="server" Text="SWAP" />
                                </td>
                                <td>K_T :
                            <asp:TextBox ID="K_TTextBox" runat="server" Text='<%# Bind("K_T","{0:0.0000}") %>' /></td>
                            </tr>
                        </table>

                        <br />
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
                        <asp:TextBox ID="CommentsTextBox" runat="server" Text='<%# Bind("Comments") %>' />
                        <br />
                        PassValStep:
                        <asp:TextBox ID="PassValStepTextBox" runat="server" Text='<%# Bind("PassValStep") %>' />
                        <br />
                        Reviewed:
                        <asp:CheckBox ID="ReviewedCheckBox" runat="server" Checked='<%# Bind("Reviewed") %>' />
                        <br />
                        FailedChems:
                        <asp:TextBox ID="FailedChemsTextBox" runat="server" Text='<%# Bind("FailedChems") %>' />
                        <br />
                        tblSampleID:
                        <asp:TextBox ID="tblSampleIDTextBox" runat="server" Text='<%# Bind("tblSampleID") %>' />
                        <br />
                        <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" OnClick="UpdateButton_Click"  Text="Process" />
                        &nbsp;<asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False"  Text="Cancel" />
                    </EditItemTemplate>

                </asp:FormView>

            </td>
            <td style="display: inline-block">
                <asp:FormView ID="FormViewSample" runat="server" DefaultMode="ReadOnly" DataKeyNames="inbICPID" DataSourceID="SqlDataSourceSamples"  Height="100%" GridLines="Horizontal" BorderStyle="None">

                    <ItemTemplate>
                        <table id="VeryTop" runat="server">
                            <tr>
                                <td>Barcode:                                   
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("CODE") %>'></asp:Label>
                                </td>
                                <td>SampleType:

                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("DUPLICATE")%>'></asp:Label>
                                </td>
                            </tr>
                        </table>

                        <table id="topTable" runat="server" class="SampleTable">

                            <tr>
                                <td>AL_D:
                            <asp:Label ID="AL_DLabel" runat="server" Text='<%# Bind("AL_D", "{0:0.0000}")%>' />
                                </td>

                                <td>AL_T:
                           <asp:Label ID="AL_TLabel" runat="server" Text='<%# Bind("AL_T", "{0:0.0000}")%>' />
                                </td>
                            </tr>

                            <tr>
                                <td>AS_D:
                        <asp:Label ID="AS_DLabel" runat="server" Text='<%# Bind("AS_D", "{0:0.0000}")%>' />
                                </td>
                                <td>AS_T:
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("AS_T", "{0:0.0000}")%>' />
                                </td>
                            </tr>

                            <tr>
                                <td>CA_D:
                        <asp:Label ID="CA_DLabel" runat="server" Text='<%# Bind("CA_D", "{0:0.0000}")%>' />
                                </td>
                                <td>CA_T:
                        <asp:Label ID="CA_TLabel" runat="server" Text='<%# Bind("CA_T", "{0:0.0000}")%>' />
                                </td>
                            </tr>

                            <tr>
                                <td>CD_D:
                        <asp:Label ID="CD_DLabel" runat="server" Text='<%# Bind("CD_D", "{0:0.0000}")%>' />
                                </td>
                                <td>CD_T:
                        <asp:Label ID="CD_TLabel" runat="server" Text='<%# Bind("CD_T", "{0:0.0000}")%>' />
                                </td>
                            </tr>

                            <tr>
                                <td>CU_D:
                        <asp:Label ID="CU_DLabel" runat="server" Text='<%# Bind("CU_D", "{0:0.0000}")%>' />

                                </td>
                                <td>CU_T:
                        <asp:Label ID="CU_TLabel" runat="server" Text='<%# Bind("CU_T", "{0:0.0000}")%>' />
                                </td>
                            </tr>

                            <tr>
                                <td>FE_D:
                        <asp:Label ID="FE_DLabel" runat="server" Text='<%# Bind("FE_D", "{0:0.0000}")%>' />

                                </td>
                                <td>FE_T:
                        <asp:Label ID="FE_TLabel" runat="server" Text='<%# Bind("FE_T", "{0:0.0000}")%>' />
                                </td>
                            </tr>

                            <tr>
                                <td>PB_D:
                        <asp:Label ID="PB_DLabel" runat="server" Text='<%# Bind("PB_D", "{0:0.0000}")%>' />

                                </td>
                                <td>PB_T:
                        <asp:Label ID="PB_TLabel" runat="server" Text='<%# Bind("PB_T", "{0:0.0000}")%>' />
                                </td>
                            </tr>

                            <tr>
                                <td>MG_D:
                        <asp:Label ID="MG_DLabel" runat="server" Text='<%# Bind("MG_D", "{0:0.0000}")%>' />
                                </td>
                                <td>MG_T:
                        <asp:Label ID="MG_TLabel" runat="server" Text='<%# Bind("MG_T", "{0:0.0000}")%>' />
                                </td>
                            </tr>

                            <tr>
                                <td>MN_D:
                        <asp:Label ID="MN_DLabel" runat="server" Text='<%# Bind("MN_D", "{0:0.0000}")%>' />
                                </td>
                                <td>MN_T:
                        <asp:Label ID="MN_TLabel" runat="server" Text='<%# Bind("MN_T", "{0:0.0000}")%>' />
                                </td>
                            </tr>

                            <tr>
                                <td>SE_D:
                        <asp:Label ID="SE_DLabel" runat="server" Text='<%# Bind("SE_D", "{0:0.0000}")%>' />
                                </td>
                                <td>SE_T:
                        <asp:Label ID="SE_TLabel" runat="server" Text='<%# Bind("SE_T", "{0:0.0000}")%>' />
                                </td>
                            </tr>

                            <tr>
                                <td>ZN_D:
                        <asp:Label ID="ZN_DLabel" runat="server" Text='<%# Bind("ZN_D", "{0:0.0000}")%>' />
                                </td>
                                <td>ZN_T:
                        <asp:Label ID="ZN_TLabel" runat="server" Text='<%# Bind("ZN_T", "{0:0.0000}")%>' />
                                </td>
                            </tr>

                            <tr>
                                <td>NA_D:
                        <asp:Label ID="NA_DLabel" runat="server" Text='<%# Bind("NA_D", "{0:0.0000}")%>' />
                                </td>
                                <td>NA_T:
                        <asp:Label ID="NA_TLabel" runat="server" Text='<%# Bind("NA_T", "{0:0.0000}")%>' />
                                </td>
                            </tr>

                            <tr>
                                <td>K_D:
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("K_D", "{0:0.0000}")%>' />
                                </td>
                                <td>K_T:
                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("K_T", "{0:0.0000}")%>' />
                                </td>
                            </tr>
                        </table>

                        ANADATE:
                        <asp:Label ID="ANADATELabel" runat="server" Text='<%# Bind("ANADATE", "{0:d}")%>' />
                        <br />
                        COMPLETE:
                        <asp:CheckBox ID="COMPLETECheckBox" runat="server" Checked='<%# Bind("COMPLETE")%>' Enabled="false" />
                        <%--                            DATE_SENT:
                        <asp:Label ID="DATE_SENTLabel" runat="server" Text='<%# Bind("DATE_SENT") %>' />--%>
                        <br />
                        Comments:
                        <asp:Label ID="CommentsLabel" runat="server" Text='<%# Bind("Comments")%>' />
                        <br />

                        <%--                            Reviewed:
                        <asp:CheckBox ID="ReviewedCheckBox" runat="server" Checked='<%# Bind("Reviewed") %>' Enabled="false" />
                            <br />--%>
                            FailedChems:
                        <asp:Label ID="FailedChemsLabel" runat="server" Text='<%# Bind("FailedChems")%>' />
                        <br />
                    </ItemTemplate>
                </asp:FormView>
            </td>

        </tr>
    </table>

    <asp:SqlDataSource ID="SqlDataSourceSamples" runat="server" ConnectionString="<%$ ConnectionStrings:RiverwatchWaterDEV %>"></asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlDataSourceBlanks" runat="server" ConnectionString="<%$ ConnectionStrings:RiverwatchWaterDEV %>"
        DeleteCommand="DELETE FROM [tblInboundICP] WHERE [inbICPID] = @inbICPID"
        InsertCommand="INSERT INTO [tblInboundICP] ([CODE], [DUPLICATE], [AL_D], [AL_T], [AS_D], [AS_T], [CA_D], [CA_T], [CD_D], [CD_T], [CU_D], [CU_T], [FE_D], [FE_T], [PB_D], [PB_T], [MG_D], [MG_T], [MN_D], [MN_T], [SE_D], [SE_T], [ZN_D], [ZN_T], [NA_D], [NA_T], [K_D], [K_T], [ANADATE], [COMPLETE], [DATE_SENT], [Comments], [PassValStep], [Reviewed], [FailedChems], [tblSampleID]) VALUES (@CODE, @DUPLICATE, @AL_D, @AL_T, @AS_D, @AS_T, @CA_D, @CA_T, @CD_D, @CD_T, @CU_D, @CU_T, @FE_D, @FE_T, @PB_D, @PB_T, @MG_D, @MG_T, @MN_D, @MN_T, @SE_D, @SE_T, @ZN_D, @ZN_T, @NA_D, @NA_T, @K_D, @K_T, @ANADATE, @COMPLETE, @DATE_SENT, @Comments, @PassValStep, @Reviewed, @FailedChems, @tblSampleID)"
        UpdateCommand="UPDATE [tblInboundICP] SET [CODE] = @CODE, [DUPLICATE] = @DUPLICATE, [AL_D] = @AL_D, [AL_T] = @AL_T, [AS_D] = @AS_D, [AS_T] = @AS_T, [CA_D] = @CA_D, [CA_T] = @CA_T, [CD_D] = @CD_D, [CD_T] = @CD_T, [CU_D] = @CU_D, [CU_T] = @CU_T, [FE_D] = @FE_D, [FE_T] = @FE_T, [PB_D] = @PB_D, [PB_T] = @PB_T, [MG_D] = @MG_D, [MG_T] = @MG_T, [MN_D] = @MN_D, [MN_T] = @MN_T, [SE_D] = @SE_D, [SE_T] = @SE_T, [ZN_D] = @ZN_D, [ZN_T] = @ZN_T, [NA_D] = @NA_D, [NA_T] = @NA_T, [K_D] = @K_D, [K_T] = @K_T, [ANADATE] = @ANADATE, [COMPLETE] = @COMPLETE, [DATE_SENT] = @DATE_SENT, [Comments] = @Comments, [PassValStep] = @PassValStep, [Reviewed] = @Reviewed, [FailedChems] = @FailedChems, [tblSampleID] = @tblSampleID WHERE [inbICPID] = @inbICPID">
        <DeleteParameters>
            <asp:Parameter Name="inbICPID" Type="Int32" />
        </DeleteParameters>

        <UpdateParameters>
            <asp:Parameter Name="CODE" Type="String" />
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
            <asp:Parameter Name="PassValStep" Type="Decimal" />
            <asp:Parameter Name="Reviewed" Type="Boolean" />
            <asp:Parameter Name="FailedChems" Type="String" />
            <asp:Parameter Name="tblSampleID" Type="Int32" />
            <asp:Parameter Name="inbICPID" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>





</asp:Content>
