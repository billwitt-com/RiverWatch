<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RWInbound2._Default" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

    <asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        
        <div>
            <asp:Label ID="lblCount" runat="server"></asp:Label>
            <br />
            <asp:Label ID="lblNoBarCode" runat="server"></asp:Label>
            <br />
            <asp:Label ID="lblNoSampleID" runat="server"></asp:Label>
            <br />
            <asp:Label ID="lblNote" runat="server"></asp:Label>
        </div>

        <asp:FormView ID="FormView1" runat="server" style="margin-right: 0px; top: -6px; left: 1px;" AllowPaging="True" DataSourceID="SqlDataSource1" 
            DefaultMode="Edit"  OnPageIndexChanged="FormView1_PageIndexChanged" OnDataBinding="FormView1_DataBinding" OnItemCreated="FormView1_ItemCreated"  OnPreRender="FormView1_PreRender" OnDataBound="FormView1_DataBound" OnItemUpdated="FormView1_ItemUpdated" PagerSettings-Position="TopAndBottom" OnPageIndexChanging="FormView1_PageIndexChanging">

            <EditItemTemplate>
                <table id="VeryTop"  runat="server">
                    <tr>
                        <%--<td>inbICPID:
                <asp:TextBox ID="inbICPIDTextBox" runat="server" ReadOnly="true" Text='<%# Bind("inbICPID") %>' />

                        </td>--%>
                        <td>Barcode:
                <asp:TextBox ID="CODETextBox" runat="server" ReadOnly="true"  Text='<%# Bind("CODE") %>' />
                        </td>
                        <td>SampleType:
                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("DUPLICATE") %>' />
                        </td>
                    </tr>
                </table>
                 <br />
                <table id="topTable" runat="server">

                    <tr>
                        <td>AL_D:
                            <asp:TextBox ID="AL_DTextBox" runat="server" Text='<%# Bind("AL_D") %>' />
                            <asp:TextBox ID="AL_DTextBoxN" runat="server" Text='<%# Bind("AL_D") %>' />
                        </td>
                        <td>
                            <asp:Button ID="btnAL" CssClass="buttonSWAP" OnCommand="Button1_Click" runat="server" Text="SWAP" />
                        </td>
                        <td>AL_T:
                            <asp:TextBox ID="AL_TTextBox" runat="server" Text='<%# Bind("AL_T") %>' />
                            <asp:TextBox ID="AL_TTextBoxN" runat="server" Text='<%# Bind("AL_T") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td>AS_D:
                                <asp:TextBox ID="AS_DTextBox" runat="server" Text='<%# Bind("AS_D") %>' />
                        </td>
                        <td>
                            <asp:Button ID="btnAS" OnCommand="Button1_Click" CommandArgument="<%=ID %>" runat="server" Text="SWAP" />
                        </td>
                        <td>AS_T:
                            <asp:TextBox ID="AS_TTextBox" runat="server" Text='<%# Bind("AS_T") %>' AutoPostBack="False" /></td>
                    </tr>
                    <tr>
                        <td> CA_D:
                            <asp:TextBox ID="CA_DTextBox" runat="server" Text='<%# Bind("CA_D") %>' />            
                        </td>
                        <td>
                            <asp:Button ID="btnCA" OnCommand="Button1_Click" CommandArgument="<%=ID %>" runat="server" Text="SWAP" />
                        </td>
                        <td> CA_T:
                            <asp:TextBox ID="CA_TTextBox" runat="server" Text='<%# Bind("CA_T") %>' />
                        </td>
                    </tr>

                    <tr>
                        <td>
                              CD_D:
                                <asp:TextBox ID="CD_DTextBox" runat="server" Text='<%# Bind("CD_D") %>' />              
                        </td>
                        <td>
                              <asp:Button ID="btnCD" OnCommand="Button1_Click" CommandArgument="<%=ID %>" runat="server" Text="SWAP" />
                        </td>
                         <td> CD_T:
                               <asp:TextBox ID="CD_TTextBox" runat="server" Text='<%# Bind("CD_T") %>' />
                         </td>
                    </tr>

                    <tr>
                        <td>CU_D:
                                <asp:TextBox ID="CU_DTextBox" runat="server" Text='<%# Bind("CU_D") %>' />
                        </td>
                           <td>
                              <asp:Button ID="btnCU" OnCommand="Button1_Click" CommandArgument="<%=ID %>" runat="server" Text="SWAP" />
                        </td>
                        <td>CU_T:
                            <asp:TextBox ID="CU_TTextBox" runat="server" Text='<%# Bind("CU_T") %>' />
                        </td>
                    </tr>

                    <tr>
                        <td>FE_D:
                            <asp:TextBox ID="FE_DTextBox" runat="server" Text='<%# Bind("FE_D") %>' />
                        </td>
                        <td>
                            <asp:Button ID="btnFE" OnCommand="Button1_Click" CommandArgument="<%=ID %>" runat="server" Text="SWAP" />
                        </td>
                        <td>FE_T:
                             <asp:TextBox ID="FE_TTextBox" runat="server" Text='<%# Bind("FE_T") %>' /></td>
                    </tr>
                    
                    <tr>
                        <td></td>
                          
                        <td></td>
                    </tr>
                    <tr>
                        <td>PB_D:
                            <asp:TextBox ID="PB_DTextBox" runat="server" Text='<%# Bind("PB_D") %>' />
                        <td>
                            <asp:Button ID="btnPB" OnCommand="Button1_Click" CommandArgument="<%=ID %>" runat="server" Text="SWAP" />
                        </td>
                        <td>PB_T:
                            <asp:TextBox ID="PB_TTextBox" runat="server" Text='<%# Bind("PB_T") %>' /></td>
                        </td>
                    </tr>
                    <tr>
                        <td> MG_D:
                            <asp:TextBox ID="MG_DTextBox" runat="server" Text='<%# Bind("MG_D") %>' />   
                        </td>
                         <td>
                            <asp:Button ID="btnMG" OnCommand="Button1_Click" CommandArgument="<%=ID %>" runat="server" Text="SWAP" />
                        </td>
                        <td> MG_T:
                            <asp:TextBox ID="MG_TTextBox" runat="server" Text='<%# Bind("MG_T") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td>MN_D:
                            <asp:TextBox ID="MN_DTextBox" runat="server" Text='<%# Bind("MN_D") %>' /></td>
                        <td>
                            <asp:Button ID="btnMN" OnCommand="Button1_Click" CommandArgument="<%=ID %>" runat="server" Text="SWAP" />
                        </td>
                        <td>MN_T:
                            <asp:TextBox ID="MN_TTextBox" runat="server" Text='<%# Bind("MN_T") %>' /></td>
                    </tr>
                    <tr>
                        <td>SE_D:
                            <asp:TextBox ID="SE_DTextBox" runat="server" Text='<%# Bind("SE_D") %>' /></td>
                        <td>
                            <asp:Button ID="btnSE" OnCommand="Button1_Click" CommandArgument="<%=ID %>" runat="server" Text="SWAP" />
                        </td>
                        <td>  SE_T:
                            <asp:TextBox ID="SE_TTextBox" runat="server" Text='<%# Bind("SE_T") %>' /></td>
                    </tr>
                    
                    <tr>
                        <td> ZN_D:
                            <asp:TextBox ID="ZN_DTextBox" runat="server" Text='<%# Bind("ZN_D") %>' /></td>
                        <td>
                            <asp:Button ID="btnZN" OnCommand="Button1_Click" CommandArgument="<%=ID %>" runat="server" Text="SWAP" />
                        </td>
                        <td> ZN_T:
                            <asp:TextBox ID="ZN_TTextBox" runat="server" Text='<%# Bind("ZN_T") %>' /></td>
                    </tr>
                    <tr>
                        <td>NA_D:
                            <asp:TextBox ID="NA_DTextBox" runat="server" Text='<%# Bind("NA_D") %>' /></td>
                        <td>
                            <asp:Button ID="btnNA" OnCommand="Button1_Click" CommandArgument="<%=ID %>" runat="server" Text="SWAP" />
                        </td>
                        <td>NA_T:
                            <asp:TextBox ID="NA_TTextBox" runat="server" Text='<%# Bind("NA_T") %>' /></td>
                    </tr>
                    <tr>
                        <td>K_D :
                            <asp:TextBox ID="K_DTextBox" runat="server" Text='<%# Bind("K_D") %>' /></td>
                        <td>
                            <asp:Button ID="btnK" OnCommand="Button1_Click" CommandArgument="<%=ID %>" runat="server" Text="SWAP" />
                        </td>
                        <td> K_T :
                            <asp:TextBox ID="K_TTextBox" runat="server" Text='<%# Bind("K_T") %>' /></td>
                    </tr>
                </table> 
     <br />
                ANADATE:
                <asp:TextBox ID="ANADATETextBox" runat="server" Text='<%# Bind("ANADATE") %>' />
                 DATE_SENT:
                <asp:TextBox ID="DATE_SENTTextBox" runat="server" Text='<%# Bind("DATE_SENT") %>' />
                <br />
                COMPLETE:
                <asp:CheckBox ID="COMPLETECheckBox" runat="server" Checked='<%# Bind("COMPLETE") %>' />
                <br />
               
                <br />
                Comments:
                <asp:TextBox ID="CommentsTextBox" runat="server" Text='<%# Bind("Comments") %>' />
                <br />
                <%--PassValStep:
                <asp:TextBox ID="PassValStepTextBox" runat="server" Text='<%# Bind("PassValStep") %>' />
                <br />--%>
                Reviewed:
                <asp:CheckBox ID="ReviewedCheckBox" runat="server" Checked='<%# Bind("Reviewed") %>' />
                <br />
                FailedChems:
                <asp:TextBox ID="FailedChemsTextBox" runat="server" Text='<%# Bind("FailedChems") %>' />
                <br />
                tblSampleID:
                <asp:TextBox ID="tblSampleIDTextBox" runat="server" Text='<%# Bind("tblSampleID") %>' />
                <br />
                <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
                &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
            </EditItemTemplate>

            <PagerSettings Mode="NumericFirstLast" PageButtonCount="16" />


        </asp:FormView>
     
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:dbRiverwatchWaterDataConnectionString %>" 
            SelectCommand="SELECT * FROM [tblInboundICP] " 
            InsertCommand="INSERT INTO tblInboundICPBlkDupe(CODE, DUPLICATE, AL_D, AL_T, AS_D, AS_T, CA_D, CA_T, CD_D, CD_T, CU_D, CU_T, FE_D, FE_T, PB_D, PB_T, MG_D, MG_T, MN_D, MN_T, SE_D, SE_T, ZN_D, ZN_T, NA_D, NA_T, K_D, K_T, ANADATE, COMPLETE, DATE_SENT, Comments, PassValStep, Reviewed, FailedChems, tblSampleID) 
            VALUES (@CODE, @DUPLICATE, @AL_D, @AL_T, @AS_D, @AS_T, @CA_D, @CA_T, @CD_D, @CD_T, @CU_D, @CU_T, @FE_D, @FE_T, @PB_D, @PB_T, @MG_D, @MG_T, @MN_D, @MN_T, @SE_D, @SE_T, @ZN_D, @ZN_T, @NA_D, @NA_T, @K_D, @K_T, @ANADATE, @COMPLETE, @DATE_SENT, @Comments, @PassValStep, @Reviewed, @FailedChems, @tblSampleID) where inbICPID = @inbICPID" 
            
            UpdateCommand="UPDATE tblInboundICPBlkDupe SET  CODE = @CODE, DUPLICATE = @DUPLICATE, AL_D = @AL_D, 
            AL_T = @AL_T, AS_D = @AS_D, AS_T = @AS_T, CA_D = @CA_D, CA_T = @CA_T, CD_D = @CD_D, CD_T = @CD_T, CU_D = @CU_D, CU_T = CU_T, 
            FE_D = @FE_D, FE_T = @FE_T, PB_D = @PB_D, PB_T = @PB_T, MG_D = @MG_D, MG_T = @MG_T, MN_D = @MN_D, MN_T = @MN_T, SE_D = @SE_D, 
            SE_T = @SE_T, ZN_D = @ZN_D, ZN_T = @ZN_T, NA_D = @NA_D, NA_T = @NA_T, K_D = @K_D, K_T = @K_T, ANADATE = @ANADATE, COMPLETE = @COMPLETE, 
            DATE_SENT = @COMPLETE, Comments = @Comments, PassValStep = 0, Reviewed = @Reviewed, FailedChems = @FailedChems, tblSampleID = tblSampleID where inbICPID = @inbICPID" 
            OnSelecting="SqlDataSource1_Selecting"
            ></asp:SqlDataSource>
   
</asp:Content>
