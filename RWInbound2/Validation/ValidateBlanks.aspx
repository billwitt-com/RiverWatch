<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ValidateBlanks.aspx.cs" Inherits="RWInbound2.Validation.ValidateBlanks" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Panel ID="pnlHelp"  BackColor="WhiteSmoke" runat="server" BorderColor="Blue" BorderStyle="Solid" BorderWidth="1px" style="font-weight: 700">
        Validation for Blanks:
        <br />
        Blank sample detail is in the left table - Associated dup (if there is one) and normal sample is/are in center and right tables<br /> Sample data can be edited as needed.<br /> The normal sample data is editable but not tested for any rules and is only for reference. You can edit it in later steps after the blanks
        <br />
        The data values are colored RED if they exceed the saved limits (see EDIT Menu for changing limits)<br />
        A tooltip (flyover) is available for any text value in left table and will help understand the data and the associated rules.<br /> 
        Just place your mouse over any value in the blank chemical data to see more detail.
        <br />
        The ACCEPT button will move the blank sample data to permanent storage and remove the sample from the validation steps.
        <br />
        The BAD button is for bad samples or samples without assocated normals. They will be saved in the data base and marked bad.
        <br />
        Updating the duplicate and or normal data will not cause any validation but will update the database so when you get to these samples<br /> in the vaidation steps, the edited values will be presented.
        <br />
        <br />
        <asp:Button ID="btnDone"  CssClass="adminButton" runat="server" OnClick="btnDone_Click" Text="Done" />

    </asp:Panel>

    <asp:Label ID="lblCount" runat="server" Text="Count"></asp:Label>
    <asp:Label ID="lblNote" runat="server" Text=""></asp:Label>
    <asp:Button ID="btnHelp" runat="server" Text="Help" CssClass="helpButton" OnClick="btnHelp_Click" Font-Size="0.95em" />

    <table runat="server" id="mainTable" >
 
        <tr >   <%--single row to hold all content   --%>         
            <td >       <%-- start of left column in major table for Blanks --%>  

                <asp:FormView ID="FormViewBlank" runat ="server"  OnDataBound ="FormViewBlank_DataBound"  AllowPaging="true" 
                    DefaultMode="Edit" DataKeyNames ="ID" DataSourceID ="SqlDataSourceBlanks"  
                    OnPageIndexChanging = "FormViewBlank_PageIndexChanging" >
                   
                     <EditItemTemplate>    

                         <table id="topTable" class="headerTable" runat="server">
                             <tr>
                                 <td>Code:
                                        <asp:TextBox ID="tbBlankCode" runat="server" ReadOnly="true" Text='<%# Bind("CODE") %>' BackColor="Silver" />
                                 </td>
                                 <td>Type:
                                        <asp:TextBox ID="tbBlankType" runat="server" Text='<%# Bind("DUPLICATE") %>' />
                                 </td>
                             </tr>
                             <tr style="line-height: 33px">
                                 <td>AL_D:
                                    <asp:TextBox ID="AL_DTextBox" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" runat="server" Text='<%# Bind("AL_D","{0:0.00}") %>' />

                                 </td>
                                 <%--<td>
                                    <asp:Button ID="btnAL" CssClass="buttonSWAP" OnCommand="Button1_Click" runat="server" Text="SWAP" />
                                </td>--%>
                                 <td>AL_T:
                                    <asp:TextBox ID="AL_TTextBox" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" runat="server" Text='<%# Bind("AL_T","{0:0.00}") %>' />

                                 </td>
                             </tr>
                            <tr>
                                <td>AS_D:
                                <asp:TextBox ID="AS_DTextBox" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" runat="server" Text='<%# Bind("AS_D","{0:0.00}") %>' />
                                </td>
                                <%--<td>
                                    <asp:Button ID="btnAS" OnCommand="Button1_Click" CommandArgument="<%=ID %>" runat="server" Text="SWAP" />
                                </td>--%>
                                <td>AS_T:
                            <asp:TextBox ID="AS_TTextBox" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" runat="server" Text='<%# Bind("AS_T","{0:0.00}") %>' />

                                </td>
                            </tr>
                            <tr>
                                <td>CA_D:
                            <asp:TextBox ID="CA_DTextBox" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" runat="server" Text='<%# Bind("CA_D","{0:0.00}") %>' />
                                </td>
                               <%-- <td>
                                    <asp:Button ID="btnCA"  AutoPostBack="true" OnTextChanged="TextBox_TextChanged" OnCommand="Button1_Click" CommandArgument="<%=ID %>" runat="server" Text="SWAP" />
                                </td>--%>
                                <td>CA_T:
                            <asp:TextBox ID="CA_TTextBox" runat="server" Text='<%# Bind("CA_T","{0:0.00}") %>' />
                                </td>
                            </tr>

                            <tr>
                                <td>CD_D:
                                <asp:TextBox ID="CD_DTextBox" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" runat="server" Text='<%# Bind("CD_D","{0:0.00}") %>' />
                                </td>
                                <%--<td>
                                    <asp:Button ID="btnCD" OnCommand="Button1_Click" CommandArgument="<%=ID %>" runat="server" Text="SWAP" />
                                </td>--%>
                                <td>CD_T:
                               <asp:TextBox ID="CD_TTextBox" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" runat="server" Text='<%# Bind("CD_T","{0:0.00}") %>' />
                                </td>
                            </tr>

                            <tr>
                                <td>CU_D:
                                <asp:TextBox ID="CU_DTextBox" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" runat="server" Text='<%# Bind("CU_D","{0:0.00}") %>' />
                                </td>
                               <%-- <td>
                                    <asp:Button ID="btnCU" OnCommand="Button1_Click" CommandArgument="<%=ID %>" runat="server" Text="SWAP" />
                                </td>--%>
                                <td>CU_T:
                            <asp:TextBox ID="CU_TTextBox" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" runat="server" Text='<%# Bind("CU_T","{0:0.00}") %>' />
                                </td>
                            </tr>

                            <tr>
                                <td>FE_D:
                            <asp:TextBox ID="FE_DTextBox" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" runat="server" Text='<%# Bind("FE_D","{0:0.00}") %>' />
                                </td>
                  <%--              <td>
                                    <asp:Button ID="btnFE" OnCommand="Button1_Click" CommandArgument="<%=ID %>" runat="server" Text="SWAP" />
                                </td>--%>
                                <td>FE_T:
                             <asp:TextBox ID="FE_TTextBox" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" runat="server" Text='<%# Bind("FE_T","{0:0.00}") %>' /></td>
                            </tr>
                              
                          <tr>
                                <td>PB_D:
                            <asp:TextBox ID="PB_DTextBox"  AutoPostBack="true" OnTextChanged="TextBox_TextChanged" runat="server" Text='<%# Bind("PB_D","{0:0.00}") %>' />
                               <%--     <td>
                                        <asp:Button ID="btnPB"  OnCommand="Button1_Click" CommandArgument="<%=ID %>" runat="server" Text="SWAP" />
                                    </td>--%>
                                    <td>PB_T:
                            <asp:TextBox ID="PB_TTextBox" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" runat="server" Text='<%# Bind("PB_T","{0:0.00}") %>' /></td>
                                </td>
                            </tr>

                             <%-- XXXX--%>
                           
                             <tr>
                                <td>MG_D:
                            <asp:TextBox ID="MG_DTextBox" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" runat="server" Text='<%# Bind("MG_D","{0:0.00}") %>' />
                              </td>
                               <%--   <td>
                                    <asp:Button ID="btnMG" OnCommand="Button1_Click" CommandArgument="<%=ID %>" runat="server" Text="SWAP" />
                                </td>--%>                                  
                                      

                                <td>MG_T:
                            <asp:TextBox ID="MG_TTextBox" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" runat="server" Text='<%# Bind("MG_T","{0:0.00}") %>' />
                                </td>
                            </tr>

                            <tr>
                                <td>MN_D:
                            <asp:TextBox ID="MN_DTextBox" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" runat="server" Text='<%# Bind("MN_D","{0:0.00}") %>' /></td>
                                <%--<td>
                                    <asp:Button ID="btnMN" OnCommand="Button1_Click" CommandArgument="<%=ID %>" runat="server" Text="SWAP" />
                                </td>--%>
                                <td>MN_T:
                            <asp:TextBox ID="MN_TTextBox" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" runat="server" Text='<%# Bind("MN_T","{0:0.00}") %>' /></td>
                            </tr>
                            <tr>
                                <td>SE_D:
                            <asp:TextBox ID="SE_DTextBox" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" runat="server" Text='<%# Bind("SE_D","{0:0.00}") %>' /></td>
                                <%--<td>
                                    <asp:Button ID="btnSE" OnCommand="Button1_Click" CommandArgument="<%=ID %>" runat="server" Text="SWAP" />
                                </td>--%>
                                <td>SE_T:
                            <asp:TextBox ID="SE_TTextBox" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" runat="server" Text='<%# Bind("SE_T","{0:0.00}") %>' /></td>
                            </tr>

                            <tr>
                                <td>ZN_D:
                            <asp:TextBox ID="ZN_DTextBox"  AutoPostBack="true" OnTextChanged="TextBox_TextChanged" runat="server" Text='<%# Bind("ZN_D","{0:0.00}") %>' /></td>
                                <%--<td>
                                    <asp:Button ID="btnZN" OnCommand="Button1_Click" CommandArgument="<%=ID %>" runat="server" Text="SWAP" />
                                </td>--%>
                                <td>ZN_T:
                            <asp:TextBox ID="ZN_TTextBox"  AutoPostBack="true" OnTextChanged="TextBox_TextChanged" runat="server" Text='<%# Bind("ZN_T","{0:0.00}") %>' /></td>
                            </tr>
                            <tr>
                                <td>NA_D:
                            <asp:TextBox ID="NA_DTextBox" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" runat="server" Text='<%# Bind("NA_D","{0:0.00}") %>' /></td>
                                <%--<td>
                                    <asp:Button ID="btnNA" OnCommand="Button1_Click" CommandArgument="<%=ID %>" runat="server" Text="SWAP" />
                                </td>--%>
                                <td>NA_T:
                            <asp:TextBox ID="NA_TTextBox" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" runat="server" Text='<%# Bind("NA_T","{0:0.00}") %>' /></td>
                            </tr>
                            <tr>
                                <td>K_D :
                            <asp:TextBox ID="K_DTextBox" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" runat="server" Text='<%# Bind("K_D","{0:0.00}") %>' />
                                </td>
                                <%--<td>
                                    <asp:Button ID="btnK" OnCommand="Button1_Click" CommandArgument="<%=ID %>" runat="server" Text="SWAP" />
                                </td>--%>
                                <td>K_T :
                                    <asp:TextBox ID="K_TTextBox" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" runat="server" Text='<%# Bind("K_T","{0:0.00}") %>' />
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
                        <asp:TextBox ID="CommentsTextBox" Width="170px"  runat="server" Text='<%# Bind("Comments") %>' TextMode="MultiLine" />
                        <br />                      

                        tblSampleID:
                        <asp:TextBox ID="tblSampleIDTextBox" ReadOnly="true"  runat="server" Text='<%# Bind("tblSampleID") %>' />
                        <br />
                        <asp:Button ID="UpdateButton" CssClass="adminButton" runat="server"  UseSubmitBehavior="true" OnClick="UpdateButton_Click"  Text="Accept" />
                  
                      <%--  <asp:Button ID="UpdateCancelButton" CssClass="adminButton" runat="server" CausesValidation="False"  Text="Cancel" />--%>
                        <asp:Button ID="btnBadBlank" CssClass="adminButton"  OnClick="btnBadBlank_Click" runat="server" Text="Bad Blank" />
                        
                    </EditItemTemplate>
                </asp:FormView>
        
            </td>  <%-- end of left column in major table--%>

            <td>    <%-- start of middle column in major table for dups if they exist--%>

                <asp:FormView ID="FormViewDuplicate" DefaultMode ="Edit" runat="server" DataKeyNames="ID" DataSourceID="SqlDataSourceDups" BorderStyle="None" 
                    BorderWidth="0px" >
                    <EditItemTemplate>
                                               
                        <table id="topTable" class="headerTable"  runat="server">
                              <tr>
                                <td>
                                        <asp:TextBox ID="tbCode" runat="server"  BackColor="Silver" ReadOnly="true" Text='<%# Bind("CODE") %>' />
                                </td>
                                <td>
                                        <asp:TextBox ID="tbType" runat="server" Text='<%# Bind("DUPLICATE") %>' />
                                </td>
                            </tr>
                          
                            <tr style="line-height: 33px">
                                <td>
                                    <asp:TextBox ID="AL_DTextBox" AutoPostBack="true"  runat="server" Text='<%# Bind("AL_D","{0:0.00}") %>' />

                                </td>

                                <td>
                                    <asp:TextBox ID="AL_TTextBox" AutoPostBack="true"  runat="server" Text='<%# Bind("AL_T","{0:0.00}") %>' />

                                </td>
                            </tr>
                            <tr>
                                <td>
                                <asp:TextBox ID="AS_DTextBox" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" runat="server" Text='<%# Bind("AS_D","{0:0.00}") %>' />
                                </td>

                                <td>
                            <asp:TextBox ID="AS_TTextBox" AutoPostBack="true"  runat="server" Text='<%# Bind("AS_T","{0:0.00}") %>' />

                                </td>
                            </tr>
                            <tr>
                                <td>
                            <asp:TextBox ID="CA_DTextBox" AutoPostBack="true"  runat="server" Text='<%# Bind("CA_D","{0:0.00}") %>' />
                                </td>

                                <td>
                            <asp:TextBox ID="CA_TTextBox" runat="server" Text='<%# Bind("CA_T","{0:0.00}") %>' />
                                </td>
                            </tr>

                            <tr>
                                <td>
                                <asp:TextBox ID="CD_DTextBox" AutoPostBack="true"  runat="server" Text='<%# Bind("CD_D","{0:0.00}") %>' />
                                </td>

                                <td>
                               <asp:TextBox ID="CD_TTextBox" AutoPostBack="true"  runat="server" Text='<%# Bind("CD_T","{0:0.00}") %>' />
                                </td>
                            </tr>

                            <tr>
                                <td>
                                <asp:TextBox ID="CU_DTextBox" AutoPostBack="true"  runat="server" Text='<%# Bind("CU_D","{0:0.00}") %>' />
                                </td>

                                <td>
                            <asp:TextBox ID="CU_TTextBox" AutoPostBack="true"  runat="server" Text='<%# Bind("CU_T","{0:0.00}") %>' />
                                </td>
                            </tr>

                            <tr>
                                <td>
                            <asp:TextBox ID="FE_DTextBox" AutoPostBack="true"  runat="server" Text='<%# Bind("FE_D","{0:0.00}") %>' />
                                </td>

                                <td>
                             <asp:TextBox ID="FE_TTextBox" AutoPostBack="true"  runat="server" Text='<%# Bind("FE_T","{0:0.00}") %>' /></td>
                            </tr>
                              
                          <tr>
                                <td>
                            <asp:TextBox ID="PB_DTextBox"  AutoPostBack="true"  runat="server" Text='<%# Bind("PB_D","{0:0.00}") %>' />
 
                                    <td>
                            <asp:TextBox ID="PB_TTextBox" AutoPostBack="true"  runat="server" Text='<%# Bind("PB_T","{0:0.00}") %>' /></td>
                                </td>
                            </tr>                    
                           
                             <tr>
                                <td>
                            <asp:TextBox ID="MG_DTextBox" AutoPostBack="true"  runat="server" Text='<%# Bind("MG_D","{0:0.00}") %>' />
                              </td> 

                                <td>
                            <asp:TextBox ID="MG_TTextBox" AutoPostBack="true"  runat="server" Text='<%# Bind("MG_T","{0:0.00}") %>' />
                                </td>
                            </tr>
      
                            <tr>
                                <td>
                            <asp:TextBox ID="MN_DTextBox" AutoPostBack="true"  runat="server" Text='<%# Bind("MN_D","{0:0.00}") %>' /></td>
     
                                <td>
                            <asp:TextBox ID="MN_TTextBox" AutoPostBack="true"  runat="server" Text='<%# Bind("MN_T","{0:0.00}") %>' /></td>
                            </tr>
                            <tr>
                                <td>
                            <asp:TextBox ID="SE_DTextBox" AutoPostBack="true"  runat="server" Text='<%# Bind("SE_D","{0:0.00}") %>' /></td>
     
                                <td>
                            <asp:TextBox ID="SE_TTextBox" AutoPostBack="true"  runat="server" Text='<%# Bind("SE_T","{0:0.00}") %>' /></td>
                            </tr>

                            <tr>
                                <td>
                            <asp:TextBox ID="ZN_DTextBox"  AutoPostBack="true"  runat="server" Text='<%# Bind("ZN_D","{0:0.00}") %>' /></td>

                                <td>
                            <asp:TextBox ID="ZN_TTextBox"  AutoPostBack="true" runat="server" Text='<%# Bind("ZN_T","{0:0.00}") %>' /></td>
                            </tr>
                            <tr>
                                <td>
                            <asp:TextBox ID="NA_DTextBox" AutoPostBack="true"  runat="server" Text='<%# Bind("NA_D","{0:0.00}") %>' /></td>
     
                                <td>
                            <asp:TextBox ID="NA_TTextBox" AutoPostBack="true"  runat="server" Text='<%# Bind("NA_T","{0:0.00}") %>' /></td>
                            </tr>
                            <tr>
                                <td>
                            <asp:TextBox ID="K_DTextBox" AutoPostBack="true"  runat="server" Text='<%# Bind("K_D","{0:0.00}") %>' />
                                </td>

                                <td>
                                    <asp:TextBox ID="K_TTextBox" AutoPostBack="true"  runat="server" Text='<%# Bind("K_T","{0:0.00}") %>' />
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
                        <asp:TextBox ID="CommentsTextBox"  TextMode="MultiLine"  runat="server" Text='<%# Bind("Comments") %>' />
                        <br />                      

                        SampleID:
                        <asp:TextBox ID="tblSampleIDTextBox" ReadOnly="true"  runat="server" Text='<%# Bind("tblSampleID") %>' />
                        <br />
                     <%--   <asp:Button ID="Button1" CssClass="adminButton" runat="server" CausesValidation="True" OnClick="UpdateButton_Click"  Text="Accept" />--%>
                        <asp:Button ID="DupUpdateButton"  runat="server" CssClass="adminButton" CausesValidation="True" CommandName="Update" Text="Update" />
                  <%--      &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />--%>
                    </EditItemTemplate>
                   
                </asp:FormView>


              


            </td>
            <%-- start of right column in major table for samples --%>
            <td >
                <asp:FormView ID="FormViewNormals" runat="server" DefaultMode="Edit" DataKeyNames="ID" DataSourceID="SqlDataSourceNormals"  
                    GridLines="Horizontal" BorderStyle="None" BorderWidth="0px">
                     <EditItemTemplate>                           
                        <table id="topTable" class="headerTable"  runat="server">
                           <tr>
                                <td>
                                        <asp:TextBox ID="tbCode" runat="server"  BackColor="Silver" ReadOnly="true" Text='<%# Bind("CODE") %>' />
                                </td>
                                <td>
                                        <asp:TextBox ID="tbType" runat="server" Text='<%# Bind("DUPLICATE") %>' />
                                </td>
                            </tr>
                            <tr style="line-height: 33px">
                                <td>
                                    <asp:TextBox ID="AL_DTextBox" AutoPostBack="true"  runat="server" Text='<%# Bind("AL_D","{0:0.00}") %>' />

                                </td>

                                <td>
                                    <asp:TextBox ID="AL_TTextBox" AutoPostBack="true"  runat="server" Text='<%# Bind("AL_T","{0:0.00}") %>' />

                                </td>
                            </tr>
                            <tr>
                                <td>
                                <asp:TextBox ID="AS_DTextBox" AutoPostBack="true" OnTextChanged="TextBox_TextChanged" runat="server" Text='<%# Bind("AS_D","{0:0.00}") %>' />
                                </td>

                                <td>
                            <asp:TextBox ID="AS_TTextBox" AutoPostBack="true"  runat="server" Text='<%# Bind("AS_T","{0:0.00}") %>' />

                                </td>
                            </tr>
                            <tr>
                                <td>
                            <asp:TextBox ID="CA_DTextBox" AutoPostBack="true"  runat="server" Text='<%# Bind("CA_D","{0:0.00}") %>' />
                                </td>

                                <td>
                            <asp:TextBox ID="CA_TTextBox" runat="server" Text='<%# Bind("CA_T","{0:0.00}") %>' />
                                </td>
                            </tr>

                            <tr>
                                <td>
                                <asp:TextBox ID="CD_DTextBox" AutoPostBack="true"  runat="server" Text='<%# Bind("CD_D","{0:0.00}") %>' />
                                </td>

                                <td>
                               <asp:TextBox ID="CD_TTextBox" AutoPostBack="true"  runat="server" Text='<%# Bind("CD_T","{0:0.00}") %>' />
                                </td>
                            </tr>

                            <tr>
                                <td>
                                <asp:TextBox ID="CU_DTextBox" AutoPostBack="true"  runat="server" Text='<%# Bind("CU_D","{0:0.00}") %>' />
                                </td>

                                <td>
                            <asp:TextBox ID="CU_TTextBox" AutoPostBack="true"  runat="server" Text='<%# Bind("CU_T","{0:0.00}") %>' />
                                </td>
                            </tr>

                            <tr>
                                <td>
                            <asp:TextBox ID="FE_DTextBox" AutoPostBack="true"  runat="server" Text='<%# Bind("FE_D","{0:0.00}") %>' />
                                </td>

                                <td>
                             <asp:TextBox ID="FE_TTextBox" AutoPostBack="true"  runat="server" Text='<%# Bind("FE_T","{0:0.00}") %>' /></td>
                            </tr>
                              
                          <tr>
                                <td>
                            <asp:TextBox ID="PB_DTextBox"  AutoPostBack="true"  runat="server" Text='<%# Bind("PB_D","{0:0.00}") %>' />
 
                                    <td>
                            <asp:TextBox ID="PB_TTextBox" AutoPostBack="true"  runat="server" Text='<%# Bind("PB_T","{0:0.00}") %>' /></td>
                                </td>
                            </tr>                    
                           
                             <tr>
                                <td>
                            <asp:TextBox ID="MG_DTextBox" AutoPostBack="true"  runat="server" Text='<%# Bind("MG_D","{0:0.00}") %>' />
                              </td> 

                                <td>
                            <asp:TextBox ID="MG_TTextBox" AutoPostBack="true"  runat="server" Text='<%# Bind("MG_T","{0:0.00}") %>' />
                                </td>
                            </tr>
      
                            <tr>
                                <td>
                            <asp:TextBox ID="MN_DTextBox" AutoPostBack="true"  runat="server" Text='<%# Bind("MN_D","{0:0.00}") %>' /></td>
     
                                <td>
                            <asp:TextBox ID="MN_TTextBox" AutoPostBack="true"  runat="server" Text='<%# Bind("MN_T","{0:0.00}") %>' /></td>
                            </tr>
                            <tr>
                                <td>
                            <asp:TextBox ID="SE_DTextBox" AutoPostBack="true"  runat="server" Text='<%# Bind("SE_D","{0:0.00}") %>' /></td>
     
                                <td>
                            <asp:TextBox ID="SE_TTextBox" AutoPostBack="true"  runat="server" Text='<%# Bind("SE_T","{0:0.00}") %>' /></td>
                            </tr>

                            <tr>
                                <td>
                            <asp:TextBox ID="ZN_DTextBox"  AutoPostBack="true"  runat="server" Text='<%# Bind("ZN_D","{0:0.00}") %>' /></td>

                                <td>
                            <asp:TextBox ID="ZN_TTextBox"  AutoPostBack="true" runat="server" Text='<%# Bind("ZN_T","{0:0.00}") %>' /></td>
                            </tr>
                            <tr>
                                <td>
                            <asp:TextBox ID="NA_DTextBox" AutoPostBack="true"  runat="server" Text='<%# Bind("NA_D","{0:0.00}") %>' /></td>
     
                                <td>
                            <asp:TextBox ID="NA_TTextBox" AutoPostBack="true"  runat="server" Text='<%# Bind("NA_T","{0:0.00}") %>' /></td>
                            </tr>
                            <tr>
                                <td>
                            <asp:TextBox ID="K_DTextBox" AutoPostBack="true"  runat="server" Text='<%# Bind("K_D","{0:0.00}") %>' />
                                </td>

                                <td>
                                    <asp:TextBox ID="K_TTextBox" AutoPostBack="true"  runat="server" Text='<%# Bind("K_T","{0:0.00}") %>' />
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
                        <asp:TextBox ID="CommentsTextBox" TextMode="MultiLine"  runat="server" Text='<%# Bind("Comments") %>' />
                        <br />                      

                        SampleID:
                        <asp:TextBox ID="tblSampleIDTextBox" ReadOnly="true" runat="server" Text='<%# Bind("tblSampleID") %>' />
                        <br />
                     <%--   <asp:Button ID="Button1" CssClass="adminButton" runat="server" CausesValidation="True" OnClick="UpdateButton_Click"  Text="Accept" />--%>
                        <asp:Button ID="NormalUpdateButton"  runat="server" CssClass="adminButton" CausesValidation="True" CommandName="Update" Text="Update" />
<%--                        &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />--%>
                    </EditItemTemplate>
                   
                </asp:FormView>               
            </td>
        </tr>
    </table>

    <asp:SqlDataSource ID="SqlDataSourceNormals" OnUpdating="SqlDataSourceNormals_Updating"  OnUpdated="SqlDataSourceNormals_Updated" runat="server" ConnectionString="<%$ ConnectionStrings:RiverWatchDev %>" 
         
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


<%--    we really don't care about this since we manage in code... --%>
    <asp:SqlDataSource ID="SqlDataSourceBlanks" runat="server" ConnectionString="<%$ ConnectionStrings:RiverWatchDev %>"

        UpdateCommand="UPDATE [InboundICPFinal] SET [CODE] = @CODE, [DUPLICATE] = @DUPLICATE, [AL_D] = @AL_D, [AL_T] = @AL_T, [AS_D] = @AS_D, [AS_T] = @AS_T, [CA_D] = @CA_D, [CA_T] = @CA_T, [CD_D] = @CD_D, [CD_T] = @CD_T, [CU_D] = @CU_D, [CU_T] = @CU_T, [FE_D] = @FE_D, 
        [FE_T] = @FE_T, [PB_D] = @PB_D, [PB_T] = @PB_T, [MG_D] = @MG_D, [MG_T] = @MG_T, [MN_D] = @MN_D, [MN_T] = @MN_T, [SE_D] = @SE_D, [SE_T] = @SE_T, 
        [ZN_D] = @ZN_D, [ZN_T] = @ZN_T, [NA_D] = @NA_D, [NA_T] = @NA_T, [K_D] = @K_D, [K_T] = @K_T, [ANADATE] = @ANADATE, [COMPLETE] = @COMPLETE, [DATE_SENT] = @DATE_SENT, 
        [Comments] = @Comments, [PassValStep] = @PassValStep, [Reviewed] = @Reviewed, [tblSampleID] = @tblSampleID WHERE [ID] = @inbICPID">
        
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
            <asp:Parameter Name="tblSampleID" Type="Int32" />
            <asp:Parameter Name="ID" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>

      <asp:SqlDataSource ID="SqlDataSourceDups" runat="server" OnUpdating="SqlDataSourceDups_Updating"  OnUpdated="SqlDataSourceDups_Updated" ConnectionString="<%$ ConnectionStrings:RiverWatchDev %>" 
          UpdateCommand="UPDATE [InboundICPFinal] SET [CODE] = @CODE, [tblSampleID] = @tblSampleID, [DUPLICATE] = @DUPLICATE, [AL_D] = @AL_D, [AL_T] = @AL_T, 
        [AS_D] = @AS_D, [AS_T] = @AS_T, [CA_D] = @CA_D, [CA_T] = @CA_T, [CD_D] = @CD_D, [CD_T] = @CD_T, [CU_D] = @CU_D, [CU_T] = @CU_T, [FE_D] = @FE_D, [FE_T] = @FE_T, 
        [PB_D] = @PB_D, [PB_T] = @PB_T, [MG_D] = @MG_D, [MG_T] = @MG_T, [MN_D] = @MN_D, [MN_T] = @MN_T, [SE_D] = @SE_D, [SE_T] = @SE_T, [ZN_D] = @ZN_D, [ZN_T] = @ZN_T, 
        [NA_D] = @NA_D, [NA_T] = @NA_T, [K_D] = @K_D, [K_T] = @K_T, [ANADATE] = @ANADATE, [COMPLETE] = @COMPLETE, [DATE_SENT] = @DATE_SENT, [Comments] = @Comments       
        WHERE [ID] = @ID">

<%-- we fill these in in code        [CreatedBy] = @CreatedBy, [CreatedDate] = @CreatedDate, [Valid] = @Valid, [Edited] = @Edited, [Saved] = @Saved --%>
       
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
