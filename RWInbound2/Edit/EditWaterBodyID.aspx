<%@ Page Title="Edit Water Body ID" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditWaterBodyID.aspx.cs" Inherits="RWInbound2.Edit.EditWaterBodyID" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" ClientIDMode="Static" runat="server">
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
                Search By Water Body ID:
                <asp:TextBox ID="waterBodyIDSearch" 
                    onkeydown="return (event.keyCode!=13);"
                    runat="server"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="Select" Height="31px" OnClick="btnSearch_Click" CausesValidation="False" CssClass="adminButton" />
                <asp:Button ID="btnSearchRefresh" runat="server" Text="Reset Search" Height="31px" OnClick="btnSearchRefresh_Click" CausesValidation="False" CssClass="adminButton"/>
                    <ajaxToolkit:AutoCompleteExtender 
                        ID="tbSearch_AutoCompleteExtender" 
                        runat="server" 
                        BehaviorID="tbSearch_AutoCompleteExtender" 
                        DelimiterCharacters=""  
                        ServiceMethod="SearchForWaterBodyID"             
                        TargetControlID="waterBodyIDSearch"
                        MinimumPrefixLength="2"
                        CompletionInterval="100" 
                        EnableCaching="false" 
                        CompletionSetCount="10">
                    </ajaxToolkit:AutoCompleteExtender> 
            </ContentTemplate>
        </asp:UpdatePanel>
    </div> 
    <br />         
    <asp:FormView ID="tblWBKeyViewModelFormView" runat="server" 
        DataKeyNames="ID"
        ItemType="RWInbound2.View_Models.tblWBKeyViewModel" 
        SelectMethod="GettblWBKeys"
        UpdateMethod="UpdatetblWBKey"
        DeleteMethod="DeletetblWBKey" 
        InsertMethod="InserttblWBKey"                  
        AllowPaging="true"
        OnModeChanging="tblWBKeyViewModelFormView_ModeChanging">                    
            <ItemTemplate>
                <div class="form-view-labels">
                    <label>Water Body ID:</label>                 
                </div>
                <%# Eval("WBID") %>
                <br />
                <div class="form-view-labels">
                    <label>What Shed:</label>                 
                </div>
                <%# Eval("WATERSHED") %>
                <br />  
                <div class="form-view-labels">
                    <label>Basin:</label>                 
                </div>
                <%# Eval("BASIN") %>
                <br /> 
                <div class="form-view-labels">
                    <label>Sub Basin:</label>                 
                </div>
                <%# Eval("SUBBASIN") %>
                <br />
                <div class="form-view-labels">
                    <label>Region:</label>                 
                </div>
                <%# Eval("REGION") %>
                <br /> 
                <div class="form-view-labels">
                    <label>Segment:</label>                 
                </div>
                <%# Eval("SEGMENT") %>
                <br />
                <div class="form-view-labels">
                    <label>Desig:</label>                 
                </div>
                <%# Eval("DESIG") %>
                <br />
                <div class="form-view-labels">
                    <label>Segment Description:</label>                 
                </div>
                <div class="formview-wide-item-template-div">
                    <%# Eval("SegDesc") %>
                </div>                
                <br />
                <div class="form-view-labels">
                    <label>Verify Date:</label>                 
                </div>
                <%# Eval("VerifyDate") %>
                <br />
                <div class="form-view-labels">
                    <label>Comment:</label>                 
                </div>
                <div class="formview-wide-item-template-div">
                    <%# Eval("Comment") %>
                </div>                 
                <br /><br />          
                <asp:Button ID="Button1" runat="server" Text="Edit" CommandName="Edit" CssClass="adminButton"/>
                <asp:Button ID="Button2" runat="server" Text="Delete" CommandName="Delete" CssClass="adminButton"
                            OnClientClick="return confirm('Are you certain you want to delete this?');"/>
                <asp:Button ID="Button3" runat="server" Text="New" CommandName="New" CssClass="adminButton"/> 
            </ItemTemplate>

            <EditItemTemplate>
                <div class="form-view-labels">
                    <label>Water Body ID:</label>
                </div>                 
                <asp:TextBox ID="txtWBID" runat="server" Text='<%# Bind("WBID") %>' MaxLength="25" CssClass="form-view-textbox"></asp:TextBox>
                <asp:RequiredFieldValidator id="RequiredFieldValidator" runat="server" CssClass="edit-inboundicp-required"
                                        Display="Dynamic" ErrorMessage="Required!" ControlToValidate="txtWBID">
                </asp:RequiredFieldValidator>
                <br />
                <div class="form-view-labels">
                    <label>Water Shed:</label>
                </div> 
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" class="update-panel-div">
                    <ContentTemplate>              
                        <asp:DropDownList ID="dropDownWATERSHED" runat="server" AutoPostBack="True" DataMember="it"
                                            SelectMethod="BindWATERSHED" CssClass="formview-dropdowns"   
                                            SelectedValue='<%# Bind("WATERSHED") %>'                                          
                                            AppendDataBoundItems="true" DataTextField="Description" DataValueField="Code">
                        </asp:DropDownList>             
                    </ContentTemplate>
                </asp:UpdatePanel>  
                <br />
                <div class="form-view-labels">
                    <label>Basin:</label>
                </div>    
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" class="update-panel-div">
                    <ContentTemplate>              
                        <asp:DropDownList ID="dropdownBASIN" runat="server" AutoPostBack="True" DataMember="it"
                                                    SelectMethod="BindBASIN" CssClass="formview-dropdowns"
                                                    AppendDataBoundItems="true" SelectedValue='<%# Bind("BASIN") %>'  
                                                    DataTextField="Description" DataValueField="Description">
                        </asp:DropDownList>               
                    </ContentTemplate>
                </asp:UpdatePanel>              
                <br />
                <div class="form-view-labels">
                    <label>Sub Basin:</label>
                </div>     
                <asp:UpdatePanel ID="UpdatePanel4" runat="server" class="update-panel-div">
                    <ContentTemplate>              
                        <asp:DropDownList ID="dropdownSUBBASIN" runat="server" AutoPostBack="True" DataMember="it"
                                                    SelectMethod="BindBASIN" CssClass="formview-dropdowns"
                                                    AppendDataBoundItems="true" SelectedValue='<%# Bind("SUBBASIN") %>'
                                                    DataTextField="Description" DataValueField="Description">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>              
                <br />
                <div class="form-view-labels">
                    <label>Region:</label>
                </div>                
                <asp:TextBox ID="txtREGION" runat="server" Text='<%# Bind("REGION") %>' MaxLength="6" CssClass="form-view-textbox"></asp:TextBox>
                <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" CssClass="edit-inboundicp-required"
                                        Display="Dynamic" ErrorMessage="Required!" ControlToValidate="txtREGION">
                </asp:RequiredFieldValidator>
                <br />
                <div class="form-view-labels">
                    <label>Segment:</label>
                </div>                  
                <asp:TextBox ID="txtSEGMENT" runat="server" Text='<%# Bind("SEGMENT") %>' MaxLength="4" CssClass="form-view-textbox"></asp:TextBox>
                <asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" CssClass="edit-inboundicp-required"
                                        Display="Dynamic" ErrorMessage="Required!" ControlToValidate="txtSEGMENT">
                </asp:RequiredFieldValidator>
                <br />
                <div class="form-view-labels">
                    <label>Desig:</label>
                </div>                
                <asp:TextBox ID="txtDESIG" runat="server" Text='<%# Bind("DESIG") %>' MaxLength="2" CssClass="form-view-textbox"></asp:TextBox>
                <br />
                <div class="form-view-labels">
                    <label>Seg Desc:</label>
                </div>                 
                <asp:TextBox ID="txtSegDesc" runat="server" TextMode="MultiLine" Text='<%# Bind("SegDesc") %>' MaxLength="1024" CssClass="form-view-textbox"></asp:TextBox>
                <br />
                <div class="form-view-labels">
                    <label>Verify Date: </label>
                </div>                
                <asp:TextBox ID="txtVerifyDate" runat="server" Text='<%# Bind("VerifyDate") %>' MaxLength="2" CssClass="form-view-textbox"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="txtVerifyDate_CalendarExtender" runat="server" 
                                                BehaviorID="txtVerifyDate_CalendarExtender" 
                                                TargetControlID="txtVerifyDate">
                </ajaxToolkit:CalendarExtender>
                <br />
                <div class="form-view-labels">
                    <label>Comment:</label>
                </div>                 
                <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Text='<%# Bind("Comment") %>' MaxLength="50" CssClass="form-view-textbox"></asp:TextBox>

                <br /><br />
                <asp:HiddenField id="WATERSHEDSELECTED" runat="server" value='<%# Bind("WATERSHED") %>' />
                <asp:HiddenField id="ID" runat="server" value='<%# Bind("ID") %>' />
                <asp:Button ID="CancelButton" runat="server" Text="Cancel" CommandName="Cancel" CssClass="adminButton" />
                <asp:Button ID="UpdateButton" runat="server" Text="Update" CommandName="Update" CssClass="adminButton" />
            </EditItemTemplate>

            <InsertItemTemplate>
                <div class="form-view-labels">
                    <label>Water Body ID:</label>
                </div>                 
                <asp:TextBox ID="newWBID" runat="server" Text='<%# Bind("WBID") %>' MaxLength="25" CssClass="form-view-textbox"></asp:TextBox>
                <asp:RequiredFieldValidator id="RequiredFieldValidator3" runat="server" CssClass="edit-inboundicp-required"
                                        Display="Dynamic" ErrorMessage="Required!" ControlToValidate="newWBID">
                </asp:RequiredFieldValidator>
                <br />
                <div class="form-view-labels">
                    <label>Water Shed:</label>
                </div>                      
                <asp:UpdatePanel ID="UpdatePanel5" runat="server" class="update-panel-div">
                    <ContentTemplate>              
                        <asp:DropDownList ID="newDropDownWATERSHED" runat="server" AutoPostBack="True" DataMember="it"
                                            CssClass="formview-dropdowns"
                                            AppendDataBoundItems="true" SelectMethod="BindWATERSHED"
                                            DataTextField="Description" DataValueField="Code">
                        </asp:DropDownList>    
                    </ContentTemplate>
                </asp:UpdatePanel>             
                <br />
                <div class="form-view-labels">
                    <label>Basin:</label>
                </div>   
                <asp:UpdatePanel ID="UpdatePanel6" runat="server" class="update-panel-div">
                    <ContentTemplate>              
                        <asp:DropDownList ID="newDropdownBASIN" runat="server" AutoPostBack="True" DataMember="it"
                                            CssClass="formview-dropdowns"
                                            AppendDataBoundItems="true" SelectMethod="BindBASIN"
                                            DataTextField="Description" DataValueField="Description">
                        </asp:DropDownList>   
                    </ContentTemplate>
                </asp:UpdatePanel>               
                <br />
                <div class="form-view-labels">
                    <label>Sub Basin:</label>
                </div>     
                <asp:UpdatePanel ID="UpdatePanel7" runat="server" class="update-panel-div">
                    <ContentTemplate>              
                        <asp:DropDownList ID="newDropdownSUBBASIN" runat="server" AutoPostBack="True" DataMember="it"
                                            CssClass="formview-dropdowns"
                                            AppendDataBoundItems="true" SelectMethod="BindBASIN"
                                            DataTextField="Description" DataValueField="Description">
                        </asp:DropDownList> 
                    </ContentTemplate>
                </asp:UpdatePanel>              
                <br />
                <div class="form-view-labels">
                    <label>Region:</label>
                </div>                
                <asp:TextBox ID="newREGION" runat="server" Text='<%# Bind("REGION") %>' MaxLength="6" CssClass="form-view-textbox"></asp:TextBox>
                <asp:RequiredFieldValidator id="RequiredFieldValidator4" runat="server" CssClass="edit-inboundicp-required"
                                        Display="Dynamic" ErrorMessage="Required!" ControlToValidate="newREGION">
                </asp:RequiredFieldValidator>
                <br />
                <div class="form-view-labels">
                    <label>Segment:</label>
                </div>                  
                <asp:TextBox ID="newSEGMENT" runat="server" Text='<%# Bind("SEGMENT") %>' MaxLength="4" CssClass="form-view-textbox"></asp:TextBox>
                <asp:RequiredFieldValidator id="RequiredFieldValidator5" runat="server" CssClass="edit-inboundicp-required"
                                        Display="Dynamic" ErrorMessage="Required!" ControlToValidate="newSEGMENT">
                </asp:RequiredFieldValidator>
                <br />
                <div class="form-view-labels">
                    <label>Desig:</label>
                </div>                
                <asp:TextBox ID="newDESIG" runat="server" Text='<%# Bind("DESIG") %>' MaxLength="2" CssClass="form-view-textbox"></asp:TextBox>
                <br />
                <div class="form-view-labels">
                    <label>Seg Desc:</label>
                </div>                 
                <asp:TextBox ID="newSegDesc" runat="server" TextMode="MultiLine" Text='<%# Bind("SegDesc") %>' MaxLength="1024" CssClass="form-view-textbox"></asp:TextBox>
                <br />
                <div class="form-view-labels">
                    <label>Verify Date: </label>
                </div>                
                <asp:TextBox ID="newVerifyDate" runat="server" Text='<%# Bind("VerifyDate") %>' MaxLength="2" CssClass="form-view-textbox"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="newVerifyDate_CalendarExtender" runat="server" 
                                                BehaviorID="newVerifyDate_CalendarExtender" 
                                                TargetControlID="newVerifyDate">
                </ajaxToolkit:CalendarExtender>
                <br />
                <div class="form-view-labels">
                    <label>Comment:</label>
                </div>                 
                <asp:TextBox ID="newComment" runat="server" TextMode="MultiLine" Text='<%# Bind("Comment") %>' MaxLength="50" CssClass="form-view-textbox"></asp:TextBox>

                <br /><br />
                <asp:HiddenField id="ID" runat="server" value='<%# Bind("ID") %>' />
                <asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
                <asp:Button ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
            </InsertItemTemplate> 
        </asp:FormView>       
</asp:Content>
