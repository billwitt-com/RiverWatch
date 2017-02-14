<%@ Page Title="Edit Phys Hab Para" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditPhysHabPara.aspx.cs" Inherits="RWInbound2.Edit.EditPhysHabPara" %>
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
                    Search By Parameter Name:
                    <asp:TextBox ID="parameterNameSearch" 
                        onkeydown="return (event.keyCode!=13);"
                        runat="server"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" Height="31px" OnClick="btnSearch_Click" CssClass="adminButton"/>
                    <asp:Button ID="btnSearchRefresh" runat="server" Text="Reset Search" Height="31px" 
                                OnClick="btnSearchRefresh_Click" CssClass="adminButton" />
                    <br /><br />
                    <ajaxToolkit:AutoCompleteExtender 
                        ID="tbSearch_AutoCompleteExtender" 
                        runat="server" 
                        BehaviorID="tbSearch_AutoCompleteExtender" 
                        DelimiterCharacters=""  
                        ServiceMethod="SearchForParameterName"             
                        TargetControlID="parameterNameSearch"
                        MinimumPrefixLength="2"
                        CompletionInterval="100" 
                        EnableCaching="false" 
                        CompletionSetCount="10">
                    </ajaxToolkit:AutoCompleteExtender>                     
            </ContentTemplate>
        </asp:UpdatePanel>  
    </div>       
    <asp:FormView ID="PhysHabParaFormView" runat="server" 
        DataKeyNames="PhysHabParaID"
        ItemType="RWInbound2.tlkPhysHabPara" 
        SelectMethod="GetPhysHabParas"
        UpdateMethod="UpdatePhysHabPara"
        DeleteMethod="DeletePhysHabPara"
        InsertMethod="InsertPhysHabPara"
        AllowPaging="true" >   
                     
        <ItemTemplate>
            <div class="form-view-labels">
                <label>Long Parameter Name:</label>                 
            </div>
            <%# Eval("LongParameterName") %>
            <br />
            <div class="form-view-labels">
                <label>Parameter Name:</label>                 
            </div>
            <%# Eval("ParameterName") %>
            <br />  
            <div class="form-view-labels">
                <label>Parameter Type I:</label>                 
            </div>
            <%# Eval("ParameterTypeI") %>
            <br /> 
            <div class="form-view-labels">
                <label>Parameter Type II:</label>                 
            </div>
            <%# Eval("ParameterTypeII") %>
            <br />
            <div class="form-view-labels">
                <label>Row ID:</label>                 
            </div>
            <%# Eval("RowID") %>
            <br /> 
            <div class="form-view-labels">
                <label>Char Group ID:</label>                 
            </div>
            <%# Eval("CharGroupID") %>
            <br />
            <div class="form-view-labels">
                <label>Comments:</label>                 
            </div>
            <%# Eval("Comments") %>
            <br />  
            <div class="form-view-labels">
                <label>Description:</label>                 
            </div>
            <%# Eval("Description") %>
            <br />
            <div class="form-view-labels">
                <label>dsOrder:</label>                 
            </div>
            <%# Eval("dsOrder") %>
            <br /><br />          
            <asp:Button ID="EditButton" runat="server" Text="Edit" CommandName="Edit" CssClass="adminButton"/>
            <asp:Button ID="DeleteButton" runat="server" Text="Delete" CommandName="Delete" CssClass="adminButton"
                        OnClientClick="return confirm('Are you certain you want to delete this?');"/>
            <asp:Button ID="NewButton" runat="server" Text="New" CommandName="New" CssClass="adminButton"/>
        </ItemTemplate>
       <EditItemTemplate>
            <div class="form-view-labels">
                <label>Long Parameter Name:</label>
            </div> 
            <asp:TextBox ID="txtLongParameterName" runat="server" CssClass="form-view-textbox" Text='<%# Bind("LongParameterName") %>'></asp:TextBox>
            <asp:RequiredFieldValidator 
                    id="RequiredFieldValidator1" runat="server" 
                    CssClass="edit-inboundicp-required"
                    ErrorMessage="Required!" 
                    ControlToValidate="txtLongParameterName">
            </asp:RequiredFieldValidator>
            <br />
            <div class="form-view-labels">
                <label>Parameter Name:</label>
            </div> 
            <asp:TextBox ID="txtParameterName" runat="server" TextMode="MultiLine" CssClass="form-view-textbox" Text='<%# Bind("ParameterName") %>'></asp:TextBox>
            <asp:RequiredFieldValidator 
                    id="RequiredFieldValidator2" runat="server" 
                    CssClass="edit-inboundicp-required"
                    ErrorMessage="Required!" 
                    ControlToValidate="txtParameterName">
            </asp:RequiredFieldValidator>
            <br />
           <div class="form-view-labels">
                <label>Parameter Type I:</label>
            </div> 
            <asp:TextBox ID="txtParameterTypeI" runat="server" CssClass="form-view-textbox" Text='<%# Bind("ParameterTypeI") %>'></asp:TextBox>
            <asp:RequiredFieldValidator 
                    id="RequiredFieldValidator3" runat="server" 
                    CssClass="edit-inboundicp-required"
                    ErrorMessage="Required!" 
                    ControlToValidate="txtParameterTypeI">
            </asp:RequiredFieldValidator>
            <br />
           <div class="form-view-labels">
                <label>Parameter Type II:</label>
            </div> 
            <asp:TextBox ID="txtParameterTypeII" runat="server" CssClass="form-view-textbox" Text='<%# Bind("ParameterTypeII") %>'></asp:TextBox>
            <asp:RequiredFieldValidator 
                    id="RequiredFieldValidator4" runat="server" 
                    CssClass="edit-inboundicp-required"
                    ErrorMessage="Required!" 
                    ControlToValidate="txtParameterTypeII">
            </asp:RequiredFieldValidator>
            <br />
           <div class="form-view-labels">
                <label>Row ID:</label>
            </div> 
            <asp:TextBox ID="txtRowID" runat="server" TextMode="Number" CssClass="form-view-textbox rowId-charid-dsorder-textbox" Text='<%# Bind("RowID") %>'></asp:TextBox>
            <asp:RequiredFieldValidator 
                    id="RequiredFieldValidator5" runat="server" 
                    CssClass="edit-inboundicp-required"
                    ErrorMessage="Required!" 
                    ControlToValidate="txtRowID">
            </asp:RequiredFieldValidator>
            <br />
            <div class="form-view-labels">
                <label>Char Group ID:</label>
            </div> 
            <asp:TextBox ID="txtCharGroupID" runat="server" MaxLength="8" CssClass="form-view-textbox rowId-charid-dsorder-textbox" Text='<%# Bind("CharGroupID") %>'></asp:TextBox>
            <asp:RequiredFieldValidator 
                    id="RequiredFieldValidator6" runat="server" 
                    CssClass="edit-inboundicp-required"
                    ErrorMessage="Required!" 
                    ControlToValidate="txtCharGroupID">
            </asp:RequiredFieldValidator>
            <br />
            <div class="form-view-labels">
                <label>Comments:</label>
            </div> 
            <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" CssClass="form-view-textbox" Text='<%# Bind("Comments") %>'></asp:TextBox>            
            <br />
            <div class="form-view-labels">
                <label>Description:</label>
            </div> 
            <asp:TextBox ID="newTxtDescription" runat="server" CssClass="form-view-textbox" Text='<%# Bind("Description") %>'></asp:TextBox>            
            <br />
            <div class="form-view-labels">
                <label>dsOrder:</label>
            </div> 
            <asp:TextBox ID="txtdsOrder" runat="server" TextMode="Number" CssClass="form-view-textbox rowId-charid-dsorder-textbox" Text='<%# Bind("dsOrder") %>'></asp:TextBox>
            <asp:RequiredFieldValidator 
                    id="RequiredFieldValidator9" runat="server" 
                    CssClass="edit-inboundicp-required"
                    ErrorMessage="Required!" 
                    ControlToValidate="txtdsOrder">
            </asp:RequiredFieldValidator>
            <br /><br />
            <asp:Button ID="CancelButton" runat="server" Text="Cancel" CommandName="Cancel" CausesValidation="false" CssClass="adminButton"/>
            <asp:Button ID="UpdateButton" runat="server" Text="Update" CommandName="Update" CssClass="adminButton"/>
        </EditItemTemplate>
        <InsertItemTemplate>
            <div class="form-view-labels">
                <label>Long Parameter Name:</label>
            </div> 
            <asp:TextBox ID="newTxtLongParameterName" runat="server" TextMode="MultiLine" CssClass="form-view-textbox" Text='<%# Bind("LongParameterName") %>'></asp:TextBox>
            <asp:RequiredFieldValidator 
                    id="RequiredFieldValidator10" runat="server" 
                    CssClass="edit-inboundicp-required"
                    ErrorMessage="Required!" 
                    ControlToValidate="newTxtLongParameterName">
            </asp:RequiredFieldValidator>
            <br />
            <div class="form-view-labels">
                <label>Parameter Name:</label>
            </div> 
            <asp:TextBox ID="newTxtParameterName" runat="server" CssClass="form-view-textbox" Text='<%# Bind("ParameterName") %>'></asp:TextBox>
            <asp:RequiredFieldValidator 
                    id="RequiredFieldValidator11" runat="server" 
                    CssClass="edit-inboundicp-required"
                    ErrorMessage="Required!" 
                    ControlToValidate="newTxtParameterName">
            </asp:RequiredFieldValidator>
            <br />
            <div class="form-view-labels">
                <label>Parameter Type I:</label>
            </div> 
            <asp:TextBox ID="newTxtParameterTypeI" runat="server" CssClass="form-view-textbox" Text='<%# Bind("ParameterTypeI") %>'></asp:TextBox>
            <asp:RequiredFieldValidator 
                    id="RequiredFieldValidator12" runat="server" 
                    CssClass="edit-inboundicp-required"
                    ErrorMessage="Required!" 
                    ControlToValidate="newTxtParameterTypeI">
            </asp:RequiredFieldValidator>
            <br />
            <div class="form-view-labels">
                <label>Parameter Type II:</label>
            </div> 
            <asp:TextBox ID="newTxtParameterTypeII" runat="server" CssClass="form-view-textbox" Text='<%# Bind("ParameterTypeII") %>'></asp:TextBox>
            <asp:RequiredFieldValidator 
                    id="RequiredFieldValidator13" runat="server" 
                    CssClass="edit-inboundicp-required"
                    ErrorMessage="Required!" 
                    ControlToValidate="newTxtParameterTypeII">
            </asp:RequiredFieldValidator>
            <br />
            <div class="form-view-labels">
                <label>Row ID:</label>
            </div> 
            <asp:TextBox ID="newTxtRowID" runat="server" TextMode="Number" CssClass="form-view-textbox rowId-charid-dsorder-textbox" Text='<%# Bind("RowID") %>'></asp:TextBox>
            <asp:RequiredFieldValidator 
                    id="RequiredFieldValidator14" runat="server" 
                    CssClass="edit-inboundicp-required"
                    ErrorMessage="Required!" 
                    ControlToValidate="newTxtRowID">
            </asp:RequiredFieldValidator>
            <br />
            <div class="form-view-labels">
                <label>Char Group ID:</label>
            </div> 
            <asp:TextBox ID="newTxtCharGroupID" runat="server" MaxLength="8" CssClass="form-view-textbox rowId-charid-dsorder-textbox" Text='<%# Bind("CharGroupID") %>'></asp:TextBox>
            <asp:RequiredFieldValidator 
                    id="RequiredFieldValidator15" runat="server" 
                    CssClass="edit-inboundicp-required"
                    ErrorMessage="Required!" 
                    ControlToValidate="newTxtCharGroupID">
            </asp:RequiredFieldValidator>
            <br />
            <div class="form-view-labels">
                <label>Comments:</label>
            </div> 
            <asp:TextBox ID="newTxtComments" runat="server" TextMode="MultiLine" CssClass="form-view-textbox" Text='<%# Bind("Comments") %>'></asp:TextBox>            
            <br />
            <div class="form-view-labels">
                <label>Description:</label>
            </div> 
            <asp:TextBox ID="txtDescription" runat="server" CssClass="form-view-textbox" Text='<%# Bind("Description") %>'></asp:TextBox>            
            <br />
            <div class="form-view-labels">
                <label>dsOrder:</label>
            </div> 
            <asp:TextBox ID="newTxtdsOrder" runat="server" TextMode="Number" CssClass="form-view-textbox rowId-charid-dsorder-textbox" Text='<%# Bind("dsOrder") %>'></asp:TextBox>
            <asp:RequiredFieldValidator 
                    id="RequiredFieldValidator18" runat="server" 
                    CssClass="edit-inboundicp-required"
                    ErrorMessage="Required!" 
                    ControlToValidate="newTxtdsOrder">
            </asp:RequiredFieldValidator>
            <br /><br />
            <asp:Button ID="CancelButton" runat="server" Text="Cancel" CommandName="Cancel" CausesValidation="false" CssClass="adminButton" />
            <asp:Button ID="InsertButton" runat="server" Text="Insert" CommandName="Insert" CssClass="adminButton"/>
        </InsertItemTemplate>
    </asp:FormView>            
</asp:Content>
