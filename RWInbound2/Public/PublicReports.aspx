<%@ Page Title="Public Reports" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PublicReports.aspx.cs" Inherits="RWInbound2.Public.PublicReports" %>
<%@ Import Namespace="BotDetect.Web.UI" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server" ClientIDMode="Static">
    <div class="site-title">
        <h2>Public Reports</h2> 
        <br />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnlLogin" runat="server" Height="81px">
                    <div class="public-labels">
                        <label>Login:</label>
                    </div>            
                    <asp:TextBox ID="tbEmail" runat="server" CssClass="public-textbox"></asp:TextBox>
                    <asp:Button ID="btnLogin" runat="server" CssClass="public-button" Text="Login" OnClick="btnLogin_Click" CausesValidation="false" />
                    <div class="public-request-access-link">
                        <asp:LinkButton ID="btnRequestAccess" runat="server" OnClick="btnRequestAccess_Click" CssClass="public-request-access" 
                                        CausesValidation="false">Request Access?</asp:LinkButton>
                    </div>                        
                </asp:Panel>                
                
                <div class="label-placement">
                    <asp:Label ID="ErrorLabel" CssClass="label-error" runat="server" />               
                </div>
                <div class="label-placement">
                     <asp:Label ID="SuccessLabel" CssClass="label-success" runat="server" />
                </div>         

                <br />
                <asp:Panel ID="pnlRequest" runat="server">
                    <asp:FormView ID="PublicUserFormView" runat="server" 
                                DataKeyNames="ID"
                                ItemType="RWInbound2.PublicUsers" 
                                InsertMethod="InboundICPFormView_InsertItem"
                                DefaultMode="Insert"
                                CssClass="request-form">
                                <InsertItemTemplate>
                                    <div class="fields-required">Submit your infomration to request access.</div>
                                    <br />
                                    <div class="public-labels">
                                        <span class="required">*</span><label>Email:</label>
                                    </div>
                                    <asp:TextBox ID="newEmail" runat="server" CssClass="public-textbox" TextMode="Email" Text='<%# Bind("Email") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator 
                                            id="RequiredFieldValidator3" runat="server" 
                                            CssClass="edit-inboundicp-required"
                                            ErrorMessage="Required!" 
                                            ControlToValidate="newEmail">
                                    </asp:RequiredFieldValidator>
                                    <br />
                                    <div class="public-labels">
                                        <label>First Name:</label>
                                    </div>                            
                                    <asp:TextBox ID="newFirstName" runat="server" CssClass="public-textbox" Text='<%# Bind("FirstName") %>'></asp:TextBox>
                                    <br />
                                    <div class="public-labels">
                                        <label>Last Name:</label>
                                    </div>                            
                                    <asp:TextBox ID="newLastName" runat="server" CssClass="public-textbox" Text='<%# Bind("LastName") %>'></asp:TextBox>
                                    <br />
                                    <div class="public-labels">
                                        <label>Address1: </label>
                                    </div>                             
                                    <asp:TextBox ID="newAddress1" runat="server" CssClass="public-textbox" Text='<%# Bind("Address1") %>'></asp:TextBox>
                                    <br />
                                    <div class="public-labels">
                                        <label>Address2: </label>
                                    </div>
                                    <asp:TextBox ID="newAddress2" runat="server" CssClass="public-textbox" Text='<%# Bind("Address2") %>'></asp:TextBox>
                                    <br />
                                    <div class="public-labels">
                                        <label>City:</label>
                                    </div>                            
                                    <asp:TextBox ID="newCity" runat="server" CssClass="public-textbox" Text='<%# Bind("City") %>'></asp:TextBox>
                                    <br />   
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate> 
                                            <div class="public-labels">
                                                <label>State:</label>
                                            </div>
                                            <asp:DropDownList ID="newStateDropDown" runat="server" CssClass="public-dropdown" 
                                                                DataTextField="Value" DataValueField="Text">                                           
                                            </asp:DropDownList>
                                            </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <div class="public-labels">
                                        <label>Zip:</label>
                                    </div>
                                    <asp:TextBox ID="newZip" runat="server" CssClass="public-textbox-zip" Text='<%# Bind("Zip") %>'></asp:TextBox>                        
                                    <br /><br />
                                    <div class="public-receive-emails">
                                        <label> Receive emails about River Watch updates:</label>
                                        <div class="public-receive-emails-checkbox">
                                            <asp:CheckBox ID="newReceiveEmailUpdates" runat="server" Checked='<%# Bind("ReceiveEmailUpdates") %>'/>
                                        </div>                                
                                    </div>   
                                                                        
                                    <asp:Panel ID="pnlVerifyCaptcha" runat="server">
                                        <br /><br />
                                        <div>
                                            <BotDetect:WebFormsCaptcha ID="RegisterCaptcha" runat="server" />
                                        </div>
                                        <asp:Label runat="server" AssociatedControlID="CaptchaCode">
                                          Retype the characters from the picture:
                                        </asp:Label>

                                        <asp:TextBox ID="CaptchaCode" runat="server" CssClass="public-textbox-zip captchaVal" ></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="CaptchaRequiredValidator" 
                                                 runat="server" ControlToValidate="CaptchaCode" CssClass="edit-inboundicp-required" Display="Dynamic" 
                                                 ErrorMessage="Retyping the code from the picture is required." />       
                                        <br />
                                        <div class="public-button-submit-div">
                                            <asp:Label ID="verifyCaptchaErrorLabel" CssClass="public-captcha-label-error" 
                                                         runat="server" />                                                                                                    
                                        </div>
                                        <div id="div" class="public-button-submit-div">
                                            <asp:Button ID="btnVerifyCaptcha" runat="server" CausesValidation="True" OnClick="btnVerifyCaptcha_Click" 
                                                        Text="Verify Code" CssClass="public-button" />
                                        </div>
                                    </asp:Panel>

                                    <asp:Panel ID="pnlSubmit" runat="server">
                                        <asp:HiddenField ID="verified" runat="server" />    
                                        <div class="public-button-submit-div">                                            
                                            <asp:Button ID="btnInsert" runat="server" CausesValidation="True" CommandName="Insert" 
                                                        Text="Submit" CssClass="public-button public-button-submit" />
                                        </div>
                                    </asp:Panel>
                                                               
                            </InsertItemTemplate>     
                        </asp:FormView>
                </asp:Panel>
                <br />
                <asp:Panel ID="pnlReports" runat="server" CssClass="public-panel-reports">                                
                    <asp:Button ID="btnStations" runat="server" Text="All Stations" OnClick="btnStations_Click" CssClass="public-button-reports"/>
                    <br />
                    <asp:Button ID="btnStationsWithGauges" runat="server" Text="Stations With Gauges" OnClick="btnStationsWithGauges_Click" CssClass="public-button-reports" />
                    <br />
                    <asp:Button ID="btnOrgPerformance" runat="server" Text="Organization Performance" CssClass="public-button-reports"/>
                    <br />    
                    <asp:Button ID="btnOrgStatus" runat="server" Text="Organization Status" OnClick="btnOrgStatus_Click" CssClass="public-button-reports"/>
                    <br />
                    <asp:Button ID="btnOrgStations" runat="server" Text="Organization Stations" OnClick="btnOrgStations_Click" CssClass="public-button-reports"/>
                    <br />
                    <asp:Button ID="btnOrganizations" runat="server" Text="All Organizations" OnClick="btnOrganizations_Click" CssClass="public-button-reports"/>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>    
    </div>    
</asp:Content>
