<%@ Page Title="Add InboundICP" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddInboundICP.aspx.cs" Inherits="RWInbound2.Admin.AddInboundICP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" ClientIDMode="Static" runat="server">
    <div>
        <hgroup>
            <h3><%: Page.Title %></h3>
        </hgroup>

        <div class="label-placement">
            <asp:Label ID="ErrorLabel" CssClass="label-error" runat="server" />               
        </div>
        <div class="label-placement">
            <asp:Label ID="SuccessLabel" CssClass="label-success" runat="server" />
        </div>
        <br />            
    </div>
    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                Search By Metals Bar Code:
                <asp:TextBox ID="inboundICPMetalsBarCodeSearch" 
                    AutoPostBack="true"
                    runat="server"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="Select" Height="31px" OnClick="btnSearch_Click" CausesValidation="False" CssClass="adminButton" />
                <asp:Button ID="btnSearchRefresh" runat="server" Text="Reset Search" Height="31px" OnClick="btnSearchRefresh_Click" CausesValidation="False" CssClass="adminButton"/>
                    <ajaxToolkit:AutoCompleteExtender 
                        ID="tbSearch_AutoCompleteExtender" 
                        runat="server" 
                        BehaviorID="tbSearch_AutoCompleteExtender" 
                        DelimiterCharacters=""  
                        ServiceMethod="SearchForMetalsBarCode"             
                        TargetControlID="inboundICPMetalsBarCodeSearch"
                        MinimumPrefixLength="2"
                        CompletionInterval="100" 
                        EnableCaching="true" 
                        CompletionSetCount="10"
                        UseContextKey="True">
                    </ajaxToolkit:AutoCompleteExtender> 
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <asp:FormView ID="InboundICPFormView" runat="server" 
        DataKeyNames="ID"
        ItemType="RWInbound2.InboundICPFinal" 
        SelectMethod="GetMetalBarCodeAndSampleID"
        UpdateMethod="InsertNewInboundICPFinal"
        DefaultMode="Edit">                
        <EditItemTemplate> 
            <asp:Button ID="InsertButton" runat="server" Text="Add" CommandName="Update" CssClass="adminButton"/>
            <button class="adminButton" type="reset" value="Reset">Reset</button>
            <br />   
            <div class="edit-inboundicp-div">
                <label>Metals Bar Code:</label>
                <asp:Label ID="TextBoxCODE" runat="server" Text='<%# Bind("CODE") %>' ReadOnly="true"></asp:Label>              
                <label>Sample ID:</label>
                <asp:Label ID="TextBoxtblSampleID" runat="server" Text='<%# Bind("tblSampleID") %>' ReadOnly="true"></asp:Label>                              
            </div>   
            <hr class="edit-inboundicp-hr" />  
            <div class="edit-inboundicp-div">
                <label>ANADATE:</label> 
                <asp:TextBox ID="TextBoxANADATE" runat="server" Text='<%# Bind("ANADATE") %>'></asp:TextBox> 
                <ajaxToolkit:CalendarExtender ID="TextBoxANADATE_CalendarExtender" runat="server" 
                                                BehaviorID="TextBoxANADATE_CalendarExtender" 
                                                TargetControlID="TextBoxANADATE"></ajaxToolkit:CalendarExtender>                
                <label>Date Sent:</label> 
                <asp:TextBox ID="TextBoxDATE_SENT" runat="server" Text='<%# Bind("DATE_SENT") %>'></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="TextBoxDATE_SENT_CalendarExtender" runat="server" 
                                                BehaviorID="TextBoxSDATE_SENT_CalendarExtender" 
                                                TargetControlID="TextBoxDATE_SENT"></ajaxToolkit:CalendarExtender>
                <label>Complete:</label> 
                <asp:CheckBox ID="CheckBoxCOMPLETE" DataField="COMPLETE" runat="server" Text='<%# Bind("COMPLETE") %>'/>
            </div> 
            <hr class="edit-inboundicp-hr" />    
            <div class="edit-inboundicp-div">
                <label>Duplicate:</label>
                <asp:TextBox ID="TextBoxDUPLICATE" MaxLength="2" runat="server" Text='<%# Bind("DUPLICATE") %>'></asp:TextBox>                     
                <asp:RequiredFieldValidator 
                        id="RequiredFieldValidator3" runat="server" 
                        CssClass="edit-inboundicp-required"
                        ErrorMessage="Required!" 
                        ControlToValidate="TextBoxDUPLICATE">
                </asp:RequiredFieldValidator>        
            </div>
            <hr class="edit-inboundicp-hr" />  
            <br />  
            <div class="edit-inboundicp-error-div">                
                <asp:CompareValidator ControlToValidate="TextBoxAL_D" Operator="DataTypeCheck" 
                                        ID="CompareValidator8" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                        Display="Dynamic" ErrorMessage="Please enter a valid AL_D value." CssClass="">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxAL_T" Operator="DataTypeCheck" 
                                        ID="CompareValidator9" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                        Display="Dynamic" ErrorMessage="Please enter a valid AL_T value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxAS_D" Operator="DataTypeCheck" 
                                        ID="CompareValidator10" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                        Display="Dynamic" ErrorMessage="Please enter a valid AS_D value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxAS_T" Operator="DataTypeCheck" 
                                        ID="CompareValidator11" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                        Display="Dynamic" ErrorMessage="Please enter a valid AS_T value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxCA_D" Operator="DataTypeCheck" 
                                        ID="CompareValidator12" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                        Display="Dynamic" ErrorMessage="Please enter a valid CA_D value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxCA_T" Operator="DataTypeCheck" 
                                        ID="CompareValidator13" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                        Display="Dynamic" ErrorMessage="Please enter a valid CA_T value.">
                </asp:CompareValidator>
                 <asp:CompareValidator ControlToValidate="TextBoxCD_D" Operator="DataTypeCheck" 
                                        ID="CompareValidator14" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                        Display="Dynamic" ErrorMessage="Please enter a valid CD_D value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxCU_T" Operator="DataTypeCheck" 
                                        ID="CompareValidator15" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                        Display="Dynamic" ErrorMessage="Please enter a valid CU_T value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxFE_D" Operator="DataTypeCheck" 
                                        ID="CompareValidator16" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                        Display="Dynamic" ErrorMessage="Please enter a valid FE_D value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxFE_T" Operator="DataTypeCheck" 
                                        ID="CompareValidator17" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                        Display="Dynamic" ErrorMessage="Please enter a valid FE_T value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxPB_D" Operator="DataTypeCheck" 
                                ID="CompareValidator23" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                Display="Dynamic" ErrorMessage="Please enter a valid PB_D value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxPB_T" Operator="DataTypeCheck" 
                                ID="CompareValidator24" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                Display="Dynamic" ErrorMessage="Please enter a valid PB_T value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxMG_D" Operator="DataTypeCheck" 
                                        ID="CompareValidator18" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                        Display="Dynamic" ErrorMessage="Please enter a valid MG_D value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxMG_T" Operator="DataTypeCheck" 
                                        ID="CompareValidator20" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                        Display="Dynamic" ErrorMessage="Please enter a valid MG_T value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxMN_D" Operator="DataTypeCheck" 
                                        ID="CompareValidator21" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                        Display="Dynamic" ErrorMessage="Please enter a valid MN_D value.">
                </asp:CompareValidator> 
                <asp:CompareValidator ControlToValidate="TextBoxMN_T" Operator="DataTypeCheck" 
                                        ID="CompareValidator22" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                        Display="Dynamic" ErrorMessage="Please enter a valid MN_T value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxSE_D" Operator="DataTypeCheck" 
                                ID="CompareValidator25" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                Display="Dynamic" ErrorMessage="Please enter a valid SE_D value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxSE_T" Operator="DataTypeCheck" 
                                ID="CompareValidator26" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                Display="Dynamic" ErrorMessage="Please enter a valid SE_T value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxZN_D" Operator="DataTypeCheck" 
                                ID="CompareValidator27" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                Display="Dynamic" ErrorMessage="Please enter a valid ZN_D value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxZN_T" Operator="DataTypeCheck" 
                                ID="CompareValidator28" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                Display="Dynamic" ErrorMessage="Please enter a valid ZN_T value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxNA_D" Operator="DataTypeCheck" 
                                ID="CompareValidator29" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                Display="Dynamic" ErrorMessage="Please enter a valid NA_D value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxNA_T" Operator="DataTypeCheck" 
                                ID="CompareValidator30" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                Display="Dynamic" ErrorMessage="Please enter a valid NA_T value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxK_D" Operator="DataTypeCheck" 
                                ID="CompareValidator31" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                Display="Dynamic" ErrorMessage="Please enter a valid K_D value.">
                </asp:CompareValidator>
                <asp:CompareValidator ControlToValidate="TextBoxK_T" Operator="DataTypeCheck" 
                                ID="CompareValidator32" runat="server" Type="Double" ForeColor="Red" Font-Bold="true"
                                Display="Dynamic" ErrorMessage="Please enter a valid K_T value.">
                </asp:CompareValidator> 
            </div>  
            <table>                
                <tr>                   
                    <td>
                        <div class="edit-inboundicp-div">
                            <label>AL_D:</label>
                            <asp:TextBox ID="TextBoxAL_D" runat="server" Text='<%# Bind("AL_D") %>'></asp:TextBox>                            
                            <label>AL_T:</label>
                            <asp:TextBox ID="TextBoxAL_T" runat="server" Text='<%# Bind("AL_T") %>'></asp:TextBox> 
                        </div>
                    </td>
                </tr>
                <tr>                   
                    <td>                          
                        <div class="edit-inboundicp-div">
                            <label>AS_D:</label>
                            <asp:TextBox ID="TextBoxAS_D" runat="server" Text='<%# Bind("AS_D") %>'></asp:TextBox>                           
                            <label>AS_T:</label>
                            <asp:TextBox ID="TextBoxAS_T" runat="server" Text='<%# Bind("AS_T") %>'></asp:TextBox>                            
                        </div>
                    </td>
                </tr>
                <tr>                   
                    <td>
                        <div class="edit-inboundicp-div">
                            <label>CA_D:</label>
                            <asp:TextBox ID="TextBoxCA_D" runat="server" Text='<%# Bind("CA_D") %>'></asp:TextBox>                            
                            <label>CA_T:</label>
                            <asp:TextBox ID="TextBoxCA_T" runat="server" Text='<%# Bind("CA_T") %>'></asp:TextBox>   
                        </div>
                    </td>
                </tr>
                <tr>                   
                    <td>
                        <div class="edit-inboundicp-div">
                            <label>CD_D:</label>
                            <asp:TextBox ID="TextBoxCD_D" runat="server" Text='<%# Bind("CD_D") %>'></asp:TextBox>                           
                            <label>CD_T:</label>
                            <asp:TextBox ID="TextBoxCD_T" runat="server" Text='<%# Bind("CD_T") %>'></asp:TextBox>                             
                        </div>
                    </td>
                </tr>
                <tr>                   
                    <td>
                        <div class="edit-inboundicp-div">
                            <label>CU_D:</label>
                            <asp:TextBox ID="TextBoxCU_D" runat="server" Text='<%# Bind("CU_D") %>'></asp:TextBox> 
                            <label>CU_T:</label>
                            <asp:TextBox ID="TextBoxCU_T" runat="server" Text='<%# Bind("CU_T") %>'></asp:TextBox> 
                        </div>
                    </td>
                </tr>
                <tr>                   
                    <td>
                        <div class="edit-inboundicp-div">                        
                            <label>FE_D:</label>
                            <asp:TextBox ID="TextBoxFE_D" runat="server" Text='<%# Bind("FE_D") %>'></asp:TextBox>                           
                            <label>FE_T:</label>
                            <asp:TextBox ID="TextBoxFE_T" runat="server" Text='<%# Bind("FE_T") %>'></asp:TextBox>                    
                        </div>
                    </td>
                </tr>
                <tr>                   
                    <td>
                        <div class="edit-inboundicp-div">
                            <label>PB_D:</label>
                            <asp:TextBox ID="TextBoxPB_D" runat="server" Text='<%# Bind("PB_D") %>'></asp:TextBox> 
                            <label>PB_T:</label>
                            <asp:TextBox ID="TextBoxPB_T" runat="server" Text='<%# Bind("PB_T") %>'></asp:TextBox>                             
                        </div>
                    </td>
                </tr>
                <tr>                   
                    <td>
                        <div class="edit-inboundicp-div">
                            <label>MG_D:</label>
                            <asp:TextBox ID="TextBoxMG_D" runat="server" Text='<%# Bind("MG_D") %>'></asp:TextBox>                            
                            <label>MG_T:</label>
                            <asp:TextBox ID="TextBoxMG_T" runat="server" Text='<%# Bind("MG_T") %>'></asp:TextBox>                            
                        </div>
                    </td>
                </tr>
                <tr>                   
                    <td>
                        <div class="edit-inboundicp-div">
                            <label>MN_D:</label>
                            <asp:TextBox ID="TextBoxMN_D" runat="server" Text='<%# Bind("MN_D") %>'></asp:TextBox>                           
                            <label>MN_T:</label>
                            <asp:TextBox ID="TextBoxMN_T" runat="server" Text='<%# Bind("MN_T") %>'></asp:TextBox>   
                        </div>
                    </td>
                </tr>
                <tr>                   
                    <td>
                        <div class="edit-inboundicp-div">
                            <label>SE_D:</label>
                            <asp:TextBox ID="TextBoxSE_D" runat="server" Text='<%# Bind("SE_D") %>'></asp:TextBox>                            
                            <label>SE_T:</label>
                            <asp:TextBox ID="TextBoxSE_T" runat="server" Text='<%# Bind("SE_T") %>'></asp:TextBox>                            
                        </div>
                    </td>
                </tr>
                <tr>                   
                    <td>
                        <div class="edit-inboundicp-div">
                            <label>ZN_D:</label>
                            <asp:TextBox ID="TextBoxZN_D" runat="server" Text='<%# Bind("ZN_D") %>'></asp:TextBox>                            
                            <label>ZN_T:</label>
                            <asp:TextBox ID="TextBoxZN_T" runat="server" Text='<%# Bind("ZN_T") %>'></asp:TextBox>  
                        </div>
                    </td>
                </tr>
                <tr>                   
                    <td>                       
                        <div class="edit-inboundicp-div">
                            <label>NA_D:</label>
                            <asp:TextBox ID="TextBoxNA_D" runat="server" Text='<%# Bind("NA_D") %>'></asp:TextBox>                            
                            <label>NA_T:</label>
                            <asp:TextBox ID="TextBoxNA_T" runat="server" Text='<%# Bind("NA_T") %>'></asp:TextBox>                            
                        </div> 
                  </td>
                </tr>
                 <tr>                   
                    <td>                                             
                        <div class="edit-inboundicp-div">
                            <label>K_D:</label>
                            <asp:TextBox ID="TextBoxK_D" runat="server" Text='<%# Bind("K_D") %>'></asp:TextBox>                            
                            <label>K_T:</label>
                            <asp:TextBox ID="TextBoxK_T" runat="server" Text='<%# Bind("K_T") %>'></asp:TextBox> 
                        </div>                                                       
                    </td>                   
                </tr> 
            </table> 
            <div class="edit-exp-water-comments-div">
                <label>Comments:</label>
                <asp:TextBox ID="TextBoxComments" runat="server" TextMode="MultiLine" Text='<%# Bind("Comments") %>'></asp:TextBox>
            </div>
        </EditItemTemplate>            
    </asp:FormView>
</asp:Content>
