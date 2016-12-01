<%@ Page Title="Edit Water Codes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeBehind="EditWaterCodes.aspx.cs" Inherits="RWInbound2.Admin.EditWaterCodes1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
  </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server" ClientIDMode="Static">   
    <section spellcheck="true">
        <div>
            <hgroup>
                <h3>
                    <%: Page.Title %> 
                    <span class="edit-water-codes-zoom-message">Zoom in for easier viewing ( Zoom <u>I</u>n: Ctrl++  | Zoom <u>O</u>ut: Ctrl+- | <u>R</u>eset: Ctrl+0 )</span>
                </h3>                 
            </hgroup>
        </div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
                <div class="modal">
                    <div class="center">
                        <img alt="Loading Please Wait" src="../Images/loader.gif"/>
                        <span class="label-loading">Loading Please Wait...</span>
                    </div>
                </div>     
            </ProgressTemplate>
        </asp:UpdateProgress>    
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="label-placement">
                    <asp:Label ID="ErrorLabel" CssClass="label-error" runat="server" />               
                </div>
                <div class="label-placement">
                     <asp:Label ID="SuccessLabel" CssClass="label-success" runat="server" />
                </div>
                <br />            
                Select a Drainage:
                <asp:DropDownList ID="DrainageDropDown" runat="server" AutoPostBack="True"
                    AppendDataBoundItems="true" SelectMethod="BindDrainages" 
                    DataTextField="Key" DataValueField="Key" 
                    OnSelectedIndexChanged="DrainageDropDown_SelectedIndexChanged">
                    <asp:ListItem Value="0">Select One:</asp:ListItem>
                </asp:DropDownList>
                <div class="org-unknown-results-or"><label>OR</label></div>
                <label>Search By Water Code:</label>                
                <asp:TextBox ID="WaterCodeSearchTextBox" 
                    AutoPostBack="true"
                    runat="server"></asp:TextBox>
                <asp:Button ID="btnSearchWaterCode" runat="server" Text="Select" Height="31px" OnClick="btnSearchWaterCode_Click" CssClass="adminButton" />                
                <ajaxToolkit:AutoCompleteExtender 
                    ID="AutoCompleteExtender1" 
                    runat="server" 
                    BehaviorID="tbSearch_AutoCompleteExtender1" 
                    DelimiterCharacters=""  
                    ServiceMethod="SearchForWaterCode"             
                    TargetControlID="WaterCodeSearchTextBox"
                    MinimumPrefixLength="1"
                    CompletionInterval="100" 
                    EnableCaching="false" 
                    CompletionSetCount="10">
                </ajaxToolkit:AutoCompleteExtender>
                <br /> <br />       
               <asp:GridView ID="WaterCodeGridView" runat="server"
                    DataKeyNames="ID"
                    ItemType="RWInbound2.tblWatercode" 
                    SelectMethod="GetWaterCodes"
                    UpdateMethod="UpdateWaterCode"
                    DeleteMethod="DeleteWaterCode" 
                    InsertItemPosition="LastItem"  
                    ShowFooter="True"           
                    AutoGenerateColumns="False" CssClass="grid-columns-center grid-larger-editor-columns-edit-water-codes"
                    GridLines="Vertical" ForeColor="#333333"
                    AllowPaging="True" Pagesize="10"
                    OnRowEditing="WaterCodeGridView_RowEditing"
                    OnRowCancelingEdit="WaterCodeGridView_RowCancelingEdit"
                    HeaderStyle-CssClass="grid-edit-water-codes-header" OnSelectedIndexChanged="WaterCodeGridView_SelectedIndexChanged" >
                    <AlternatingRowStyle BackColor="White" />    
                    <Columns>  
                        <asp:TemplateField ControlStyle-CssClass="controls-manage-public-users" ItemStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Button ID="EditButton" runat="server" Text="Edit" CommandName="Edit" />
                                <asp:Button ID="DeleteButton" runat="server" Text="Delete" CommandName="Delete" 
                                            OnClientClick="return confirm('Are you certain you want to delete this?');"/>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Button ID="CancelButton" runat="server" Text="Cancel" CommandName="Cancel" />
                                <asp:Button ID="UpdateButton" runat="server" Text="Update" CommandName="Update" />
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:Button ID="btnAdd" runat="server" Text="Add" CausesValidation="false"
                                            OnClick="AddNewWaterCode" />
                            </FooterTemplate>
                           <%-- <ControlStyle CssClass="controls-manage-public-users" />
                            <ItemStyle VerticalAlign="Middle" />--%>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ID" HeaderText="ID" Visible="false" ReadOnly="True" SortExpression="ID" />
                        <asp:TemplateField HeaderText="Water Code" SortExpression="WATERCODE" ItemStyle-VerticalAlign="Middle" ControlStyle-CssClass="grid-edit-water-codes-small-textbox">
                            <EditItemTemplate >
                                <asp:TextBox ID="txtWATERCODE" runat="server" MaxLength="5" Text='<%# Bind("WATERCODE") %>'></asp:TextBox>
                                <asp:RequiredFieldValidator 
                                        id="RequiredFieldValidator" runat="server" 
                                        CssClass="edit-inboundicp-required"
                                        Display="Dynamic"
                                        ErrorMessage="Required!" 
                                        ControlToValidate="txtWATERCODE">
                                </asp:RequiredFieldValidator>                                
                            </EditItemTemplate>
                            <ItemTemplate>
                               <asp:Label ID="lblWATERCODE" runat="server" Text='<%# Bind("WATERCODE") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="NewWATERCODE" runat="server" MaxLength="5" CssClass="grid-edit-water-codes-small-textbox"></asp:TextBox>                                                             
                                <asp:Label ID="lblWATERCODERequired" runat="server" Visible="false" CssClass="edit-inboundicp-required">
                                    Required!
                                </asp:Label>
                            </FooterTemplate>
                            <ItemStyle CssClass="grid-edit-water-codes-edit-textbox" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Water Name" SortExpression="WATERNAME" ItemStyle-VerticalAlign="Middle" ItemStyle-CssClass="grid-edit-water-codes-medium-textbox" 
                            ControlStyle-CssClass="grid-edit-water-codes-medium-textbox" >
                            <EditItemTemplate >
                                <asp:TextBox ID="txtWATERNAME" runat="server" TextMode="MultiLine" MaxLength="30" Text='<%# Bind("WATERNAME") %>'></asp:TextBox>                                                          
                            </EditItemTemplate>
                            <ItemTemplate>
                               <asp:Label ID="lblWATERNAME" runat="server" Text='<%# Bind("WATERNAME") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="NewWATERNAME" runat="server" TextMode="MultiLine" MaxLength="30" CssClass="grid-edit-water-codes-medium-textbox"></asp:TextBox>   
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Stream" SortExpression="STREAM">
                            <EditItemTemplate>
                                <asp:CheckBox ID="checkBoxSTREAM" runat="server" Checked='<%# Bind("STREAM") %>'/>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="checkSTREAM" runat="server" Checked='<%# Bind("STREAM") %>' Enabled="false"/>
                            </ItemTemplate>
                            <FooterTemplate>
                               <asp:CheckBox ID="NewSTREAM" runat="server" />
                            </FooterTemplate>
                            <ItemStyle CssClass="grid-manage-public-users-width" />
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="Acres Miles" SortExpression="ACRESMILES" ItemStyle-VerticalAlign="Middle" ControlStyle-CssClass="grid-edit-water-codes-small-textbox">
                            <EditItemTemplate >
                                <asp:TextBox ID="txtACRESMILES" runat="server" Text='<%# Bind("ACRESMILES") %>'></asp:TextBox>
                                <asp:CompareValidator ControlToValidate="txtACRESMILES" Operator="DataTypeCheck" 
                                        ID="CompareValidator1" runat="server" Type="Double" CssClass="edit-inboundicp-required"
                                        Display="Dynamic" ErrorMessage="Please enter a valid decimal value.">
                                </asp:CompareValidator>                                                          
                            </EditItemTemplate>
                            <ItemTemplate>
                               <asp:Label ID="lblACRESMILES" runat="server" Text='<%# Bind("ACRESMILES") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="NewACRESMILES" runat="server" CssClass="grid-edit-water-codes-small-textbox"></asp:TextBox> 
                                <asp:CompareValidator ControlToValidate="NewACRESMILES" Operator="DataTypeCheck" 
                                        ID="CompareValidator2" runat="server" Type="Double" CssClass="edit-inboundicp-required"
                                        Display="Dynamic" ErrorMessage="Please enter a valid decimal value.">
                                </asp:CompareValidator>   
                            </FooterTemplate>                            
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Location" SortExpression="LOCATION" ItemStyle-VerticalAlign="Middle" ItemStyle-CssClass="grid-edit-water-codes-medium-textbox" 
                            ControlStyle-CssClass="grid-edit-water-codes-medium-textbox">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtLOCATION" runat="server" Text='<%# Bind("LOCATION") %>' TextMode="MultiLine"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblLOCATION" runat="server" Text='<%# Bind("LOCATION") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="NewLOCATION" runat="server" TextMode="MultiLine" CssClass="grid-edit-water-codes-medium-textbox"></asp:TextBox>
                            </FooterTemplate>                        
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="Drainage" SortExpression="DRAINAGE" ItemStyle-VerticalAlign="Middle" ItemStyle-CssClass="grid-edit-water-codes-small-textbox" 
                            ControlStyle-CssClass="grid-edit-water-codes-small-textbox">
                            <EditItemTemplate >
                                <asp:TextBox ID="txtDRAINAGE" runat="server" MaxLength="2" Text='<%# Bind("DRAINAGE") %>'></asp:TextBox>  
                                <asp:RequiredFieldValidator 
                                        id="RequiredFieldValidator3" runat="server" 
                                        CssClass="edit-inboundicp-required"
                                        Display="Dynamic"
                                        ErrorMessage="Required!" 
                                        ControlToValidate="txtDRAINAGE">
                                </asp:RequiredFieldValidator>                                                          
                            </EditItemTemplate>
                            <ItemTemplate>
                               <asp:Label ID="lblDRAINAGE" runat="server" Text='<%# Bind("DRAINAGE") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="NewDRAINAGE" runat="server" MaxLength="2" CssClass="grid-edit-water-codes-small-textbox"></asp:TextBox> 
                                <asp:Label ID="lblDRAINAGERequired" runat="server" Visible="false" CssClass="edit-inboundicp-required">
                                    Required!
                                </asp:Label>  
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Drain Name" SortExpression="DRAINNAME" ItemStyle-VerticalAlign="Middle" ItemStyle-CssClass="grid-edit-water-codes-medium-textbox" 
                            ControlStyle-CssClass="grid-edit-water-codes-medium-textbox">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDRAINNAME" runat="server" Text='<%# Bind("DRAINNAME") %>' TextMode="MultiLine" ></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblDRAINNAME" runat="server" Text='<%# Bind("DRAINNAME") %>' ></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="NewDRAINNAME" runat="server" TextMode="MultiLine" CssClass="grid-edit-water-codes-medium-textbox"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="Hydro Code" SortExpression="HYDROCODE" ItemStyle-VerticalAlign="Middle" ItemStyle-CssClass="grid-edit-water-codes-medium-textbox" 
                            ControlStyle-CssClass="grid-edit-water-codes-medium-textbox">
                            <EditItemTemplate >
                                <asp:TextBox ID="txtHYDROCODE" runat="server" MaxLength="10" Text='<%# Bind("HYDROCODE") %>'></asp:TextBox>                                                          
                            </EditItemTemplate>
                            <ItemTemplate>
                               <asp:Label ID="lblHYDROCODE" runat="server" Text='<%# Bind("HYDROCODE") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="NewHYDROCODE" runat="server" MaxLength="10" CssClass="grid-edit-water-codes-medium-textbox"></asp:TextBox>   
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="County" SortExpression="COUNTY" ItemStyle-VerticalAlign="Middle" ItemStyle-CssClass="grid-edit-water-codes-small-textbox" 
                            ControlStyle-CssClass="grid-edit-water-codes-small-textbox">
                            <EditItemTemplate >
                                <asp:TextBox ID="txtCOUNTY" runat="server" MaxLength="3" Text='<%# Bind("COUNTY") %>'></asp:TextBox>                                                          
                            </EditItemTemplate>
                            <ItemTemplate>
                               <asp:Label ID="lblCOUNTY" runat="server" Text='<%# Bind("COUNTY") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="NewCOUNTY" runat="server" MaxLength="3" CssClass="grid-edit-water-codes-small-textbox"></asp:TextBox>   
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Region" SortExpression="REGION" ItemStyle-VerticalAlign="Middle" ItemStyle-CssClass="grid-edit-water-codes-small-textbox" 
                            ControlStyle-CssClass="grid-edit-water-codes-small-textbox">
                            <EditItemTemplate >
                                <asp:TextBox ID="txtREGION" runat="server" MaxLength="2" Text='<%# Bind("REGION") %>'></asp:TextBox>                                                          
                            </EditItemTemplate>
                            <ItemTemplate>
                               <asp:Label ID="lblREGION" runat="server" Text='<%# Bind("REGION") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="NewREGION" runat="server" MaxLength="2" CssClass="grid-edit-water-codes-small-textbox"></asp:TextBox>   
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="AWM Area" SortExpression="AWMAREA" ItemStyle-VerticalAlign="Middle" ItemStyle-CssClass="grid-edit-water-codes-small-textbox" 
                            ControlStyle-CssClass="grid-edit-water-codes-medium-textbox">
                            <EditItemTemplate >
                                <asp:TextBox ID="txtAWMAREA" runat="server" MaxLength="2" Text='<%# Bind("AWMAREA") %>'></asp:TextBox>                                                          
                            </EditItemTemplate>
                            <ItemTemplate>
                               <asp:Label ID="lblAWMAREA" runat="server" Text='<%# Bind("AWMAREA") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="NewAWMAREA" runat="server" MaxLength="2" CssClass="grid-edit-water-codes-small-textbox"></asp:TextBox>   
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Stk Code" SortExpression="STKCODE" ItemStyle-VerticalAlign="Middle" ItemStyle-CssClass="grid-edit-water-codes-small-textbox" 
                            ControlStyle-CssClass="grid-edit-water-codes-small-textbox">
                            <EditItemTemplate >
                                <asp:TextBox ID="txtSTKCODE" runat="server" MaxLength="4" Text='<%# Bind("STKCODE") %>'></asp:TextBox>                                                          
                            </EditItemTemplate>
                            <ItemTemplate>
                               <asp:Label ID="lblSTKCODE" runat="server" Text='<%# Bind("STKCODE") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="NewSTKCODE" runat="server" MaxLength="4" CssClass="grid-edit-water-codes-small-textbox"></asp:TextBox>   
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Biologist" SortExpression="BIOLOGIST" ItemStyle-VerticalAlign="Middle" ItemStyle-CssClass="grid-edit-water-codes-small-textbox" 
                            ControlStyle-CssClass="grid-edit-water-codes-small-textbox">
                            <EditItemTemplate >
                                <asp:TextBox ID="txtBIOLOGIST" runat="server" MaxLength="8" Text='<%# Bind("BIOLOGIST") %>'></asp:TextBox>                                                          
                            </EditItemTemplate>
                            <ItemTemplate>
                               <asp:Label ID="lblBIOLOGIST" runat="server" Text='<%# Bind("BIOLOGIST") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="NewBIOLOGIST" runat="server" MaxLength="8" CssClass="grid-edit-water-codes-small-textbox"></asp:TextBox>   
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Area Bio" SortExpression="AREABIO" ItemStyle-VerticalAlign="Middle" ItemStyle-CssClass="grid-edit-water-codes-small-textbox" 
                            ControlStyle-CssClass="grid-edit-water-codes-small-textbox">
                            <EditItemTemplate >
                                <asp:TextBox ID="txtAREABIO" runat="server" MaxLength="8" Text='<%# Bind("AREABIO") %>'></asp:TextBox>                                                          
                            </EditItemTemplate>
                            <ItemTemplate>
                               <asp:Label ID="lblAREABIO" runat="server" Text='<%# Bind("AREABIO") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="NewAREABIO" runat="server" MaxLength="8" CssClass="grid-edit-water-codes-small-textbox"></asp:TextBox>   
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Addl Data" SortExpression="ADDLDATA" ItemStyle-VerticalAlign="Middle" ItemStyle-CssClass="grid-edit-water-codes-medium-textbox" 
                            ControlStyle-CssClass="grid-edit-water-codes-medium-textbox">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtADDLDATA" runat="server" Text='<%# Bind("ADDLDATA") %>' TextMode="MultiLine"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblADDLDATA" runat="server" Text='<%# Bind("ADDLDATA") %>' ></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="NewADDLDATA" runat="server" TextMode="MultiLine" CssClass="grid-edit-water-codes-medium-textbox"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="Map Coordin" SortExpression="MAPCOORDIN" ItemStyle-VerticalAlign="Middle" ItemStyle-CssClass="grid-edit-water-codes-small-textbox" 
                            ControlStyle-CssClass="grid-edit-water-codes-small-textbox">
                            <EditItemTemplate >
                                <asp:TextBox ID="txtMAPCOORDIN" runat="server" MaxLength="2" Text='<%# Bind("MAPCOORDIN") %>'></asp:TextBox>                                                          
                            </EditItemTemplate>
                            <ItemTemplate>
                               <asp:Label ID="lblMAPCOORDIN" runat="server" Text='<%# Bind("MAPCOORDIN") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="NewMAPCOORDIN" runat="server" MaxLength="2" CssClass="grid-edit-water-codes-small-textbox"></asp:TextBox>   
                            </FooterTemplate>                         
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Atlas Page" SortExpression="ATLASPAGE" ItemStyle-VerticalAlign="Middle" ItemStyle-CssClass="grid-edit-water-codes-small-textbox" 
                            ControlStyle-CssClass="grid-edit-water-codes-small-textbox">
                            <EditItemTemplate >
                                <asp:TextBox ID="txtATLASPAGE" runat="server" MaxLength="3" Text='<%# Bind("ATLASPAGE") %>'></asp:TextBox>                                                          
                            </EditItemTemplate>
                            <ItemTemplate>
                               <asp:Label ID="lblATLASPAGE" runat="server" Text='<%# Bind("ATLASPAGE") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="NewATLASPAGE" runat="server" MaxLength="3" CssClass="grid-edit-water-codes-small-textbox"></asp:TextBox>   
                            </FooterTemplate>                           
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Category" SortExpression="CATEGORY" ItemStyle-VerticalAlign="Middle" ItemStyle-CssClass="grid-edit-water-codes-small-textbox" 
                            ControlStyle-CssClass="grid-edit-water-codes-small-textbox">
                            <EditItemTemplate >
                                <asp:TextBox ID="txtCATEGORY" runat="server" MaxLength="3" Text='<%# Bind("CATEGORY") %>'></asp:TextBox>                                                          
                            </EditItemTemplate>
                            <ItemTemplate>
                               <asp:Label ID="lblCATEGORY" runat="server" Text='<%# Bind("CATEGORY") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="NewCATEGORY" runat="server" MaxLength="3" CssClass="grid-edit-water-codes-small-textbox"></asp:TextBox>   
                            </FooterTemplate>                           
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Elevation" SortExpression="ELEVATION" ItemStyle-VerticalAlign="Middle" ItemStyle-CssClass="grid-edit-water-codes-small-textbox" 
                            ControlStyle-CssClass="grid-edit-water-codes-small-textbox">
                            <EditItemTemplate >
                                <asp:TextBox ID="txtELEVATION" runat="server" Text='<%# Bind("ACRESMILES") %>'></asp:TextBox>
                                <asp:CompareValidator ControlToValidate="txtELEVATION" Operator="DataTypeCheck" 
                                        ID="CompareValidator3" runat="server" Type="Double" CssClass="edit-inboundicp-required"
                                        Display="Dynamic" ErrorMessage="Please enter a valid decimal value.">
                                </asp:CompareValidator>                                                          
                            </EditItemTemplate>
                            <ItemTemplate>
                               <asp:Label ID="lblELEVATION" runat="server" Text='<%# Bind("ELEVATION") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="NewELEVATION" runat="server" CssClass="grid-edit-water-codes-small-textbox"></asp:TextBox> 
                                <asp:CompareValidator ControlToValidate="NewELEVATION" Operator="DataTypeCheck" 
                                        ID="CompareValidator4" runat="server" Type="Double" CssClass="edit-inboundicp-required"
                                        Display="Dynamic" ErrorMessage="Please enter a valid decimal value.">
                                </asp:CompareValidator>   
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UT Elevation" SortExpression="UT_ELEVATI" ItemStyle-VerticalAlign="Middle" ItemStyle-CssClass="grid-edit-water-codes-small-textbox" 
                            ControlStyle-CssClass="grid-edit-water-codes-small-textbox">
                            <EditItemTemplate >
                                <asp:TextBox ID="txtUT_ELEVATI" runat="server" Text='<%# Bind("UT_ELEVATI") %>'></asp:TextBox>
                                <asp:CompareValidator ControlToValidate="txtUT_ELEVATI" Operator="DataTypeCheck" 
                                        ID="CompareValidator5" runat="server" Type="Double" CssClass="edit-inboundicp-required"
                                        Display="Dynamic" ErrorMessage="Please enter a valid decimal value.">
                                </asp:CompareValidator>                                                          
                            </EditItemTemplate>
                            <ItemTemplate>
                               <asp:Label ID="lblUT_ELEVATI" runat="server" Text='<%# Bind("UT_ELEVATI") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="NewUT_ELEVATI" runat="server" CssClass="grid-edit-water-codes-small-textbox"></asp:TextBox> 
                                <asp:CompareValidator ControlToValidate="NewUT_ELEVATI" Operator="DataTypeCheck" 
                                        ID="CompareValidator6" runat="server" Type="Double" CssClass="edit-inboundicp-required"
                                        Display="Dynamic" ErrorMessage="Please enter a valid decimal value.">
                                </asp:CompareValidator>   
                            </FooterTemplate>                          
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ownership" SortExpression="OWNERSHIP" ItemStyle-VerticalAlign="Middle" ItemStyle-CssClass="grid-edit-water-codes-small-textbox" 
                            ControlStyle-CssClass="grid-edit-water-codes-small-textbox">
                            <EditItemTemplate >
                                <asp:TextBox ID="txtOWNERSHIP" runat="server" MaxLength="6" Text='<%# Bind("OWNERSHIP") %>'></asp:TextBox>                                                          
                            </EditItemTemplate>
                            <ItemTemplate>
                               <asp:Label ID="lblOWNERSHIP" runat="server" Text='<%# Bind("OWNERSHIP") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="NewOWNERSHIP" runat="server" MaxLength="6" CssClass="grid-edit-water-codes-small-textbox"></asp:TextBox>   
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Wilderness" SortExpression="WILDERNESS">
                            <EditItemTemplate>
                                <asp:CheckBox ID="checkBoxWILDERNESS" runat="server" Checked='<%# Bind("WILDERNESS") %>' CssClass="grid-manage-public-users-width"/>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="checkWILDERNESS" runat="server" Checked='<%# Bind("WILDERNESS") %>' Enabled="false" CssClass="grid-manage-public-users-width" />
                            </ItemTemplate>
                            <FooterTemplate>
                               <asp:CheckBox ID="NewWILDERNESS" runat="server" CssClass="grid-manage-public-users-width-new" />
                            </FooterTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="Blm Map" SortExpression="BLMMAP" ItemStyle-VerticalAlign="Middle" ItemStyle-CssClass="grid-edit-water-codes-small-textbox" 
                            ControlStyle-CssClass="grid-edit-water-codes-small-textbox">
                            <EditItemTemplate >
                                <asp:TextBox ID="txtBLMMAP" runat="server" MaxLength="2" Text='<%# Bind("BLMMAP") %>'></asp:TextBox>                                                          
                            </EditItemTemplate>
                            <ItemTemplate>
                               <asp:Label ID="lblBLMMAP" runat="server" Text='<%# Bind("BLMMAP") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="NewBLMMAP" runat="server" MaxLength="2" CssClass="grid-edit-water-codes-small-textbox"></asp:TextBox>   
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Topo Map" SortExpression="TOPOMAP" ItemStyle-VerticalAlign="Middle" ItemStyle-CssClass="grid-edit-water-codes-small-textbox" 
                            ControlStyle-CssClass="grid-edit-water-codes-small-textbox">
                            <EditItemTemplate >
                                <asp:TextBox ID="txtTOPOMAP" runat="server" MaxLength="4" Text='<%# Bind("TOPOMAP") %>'></asp:TextBox>                                                          
                            </EditItemTemplate>
                            <ItemTemplate>
                               <asp:Label ID="lblTOPOMAP" runat="server" Text='<%# Bind("TOPOMAP") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="NewTOPOMAP" runat="server" MaxLength="4" CssClass="grid-edit-water-codes-small-textbox"></asp:TextBox>   
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Obsolete" SortExpression="OBSOLETE">
                            <EditItemTemplate>
                                <asp:CheckBox ID="checkBoxOBSOLETE" runat="server" Checked='<%# Bind("OBSOLETE") %>'/>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="checkOBSOLETE" runat="server" Checked='<%# Bind("OBSOLETE") %>' Enabled="false" />
                            </ItemTemplate>
                            <FooterTemplate>
                               <asp:CheckBox ID="NewOBSOLETE" runat="server" />
                            </FooterTemplate>
                        </asp:TemplateField> 
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />                        
                    <HeaderStyle CssClass="grid-edit-water-codes-header" />
                </asp:GridView> 
            </ContentTemplate>
        </asp:UpdatePanel>       
    </section>
</asp:Content>
