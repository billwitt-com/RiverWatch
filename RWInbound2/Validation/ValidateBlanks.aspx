<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ValidateBlanks.aspx.cs" Inherits="RWInbound2.Validation.ValidateBlanks" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   
    <asp:Label ID="lblCount" runat="server" Text="Count"></asp:Label>


    <table style="width: 100%">
        <tr>
            <td>
                <asp:FormView ID="FormViewBlank" runat="server" DefaultMode="Edit" DataKeyNames="inbICPID" DataSourceID="SqlDataSourceBlanks">
                    <EditItemTemplate>
                        inbICPID:
                        <asp:Label ID="inbICPIDLabel1" runat="server" Text='<%# Eval("inbICPID") %>' />
                        <br />
                        CODE:
                        <asp:TextBox ID="CODETextBox" runat="server" Text='<%# Bind("CODE") %>' />
                        <br />
                        DUPLICATE:
                        <asp:TextBox ID="DUPLICATETextBox" runat="server" Text='<%# Bind("DUPLICATE") %>' />
                        <br />
                        AL_D:
                        <asp:TextBox ID="AL_DTextBox" runat="server" Text='<%# Bind("AL_D") %>' />
                        <br />
                        AL_T:
                        <asp:TextBox ID="AL_TTextBox" runat="server" Text='<%# Bind("AL_T") %>' />
                        <br />
                        AS_D:
                        <asp:TextBox ID="AS_DTextBox" runat="server" Text='<%# Bind("AS_D") %>' />
                        <br />
                        AS_T:
                        <asp:TextBox ID="AS_TTextBox" runat="server" Text='<%# Bind("AS_T") %>' />
                        <br />
                        CA_D:
                        <asp:TextBox ID="CA_DTextBox" runat="server" Text='<%# Bind("CA_D") %>' />
                        <br />
                        CA_T:
                        <asp:TextBox ID="CA_TTextBox" runat="server" Text='<%# Bind("CA_T") %>' />
                        <br />
                        CD_D:
                        <asp:TextBox ID="CD_DTextBox" runat="server" Text='<%# Bind("CD_D") %>' />
                        <br />
                        CD_T:
                        <asp:TextBox ID="CD_TTextBox" runat="server" Text='<%# Bind("CD_T") %>' />
                        <br />
                        CU_D:
                        <asp:TextBox ID="CU_DTextBox" runat="server" Text='<%# Bind("CU_D") %>' />
                        <br />
                        CU_T:
                        <asp:TextBox ID="CU_TTextBox" runat="server" Text='<%# Bind("CU_T") %>' />
                        <br />
                        FE_D:
                        <asp:TextBox ID="FE_DTextBox" runat="server" Text='<%# Bind("FE_D") %>' />
                        <br />
                        FE_T:
                        <asp:TextBox ID="FE_TTextBox" runat="server" Text='<%# Bind("FE_T") %>' />
                        <br />
                        PB_D:
                        <asp:TextBox ID="PB_DTextBox" runat="server" Text='<%# Bind("PB_D") %>' />
                        <br />
                        PB_T:
                        <asp:TextBox ID="PB_TTextBox" runat="server" Text='<%# Bind("PB_T") %>' />
                        <br />
                        MG_D:
                        <asp:TextBox ID="MG_DTextBox" runat="server" Text='<%# Bind("MG_D") %>' />
                        <br />
                        MG_T:
                        <asp:TextBox ID="MG_TTextBox" runat="server" Text='<%# Bind("MG_T") %>' />
                        <br />
                        MN_D:
                        <asp:TextBox ID="MN_DTextBox" runat="server" Text='<%# Bind("MN_D") %>' />
                        <br />
                        MN_T:
                        <asp:TextBox ID="MN_TTextBox" runat="server" Text='<%# Bind("MN_T") %>' />
                        <br />
                        SE_D:
                        <asp:TextBox ID="SE_DTextBox" runat="server" Text='<%# Bind("SE_D") %>' />
                        <br />
                        SE_T:
                        <asp:TextBox ID="SE_TTextBox" runat="server" Text='<%# Bind("SE_T") %>' />
                        <br />
                        ZN_D:
                        <asp:TextBox ID="ZN_DTextBox" runat="server" Text='<%# Bind("ZN_D") %>' />
                        <br />
                        ZN_T:
                        <asp:TextBox ID="ZN_TTextBox" runat="server" Text='<%# Bind("ZN_T") %>' />
                        <br />
                        NA_D:
                        <asp:TextBox ID="NA_DTextBox" runat="server" Text='<%# Bind("NA_D") %>' />
                        <br />
                        NA_T:
                        <asp:TextBox ID="NA_TTextBox" runat="server" Text='<%# Bind("NA_T") %>' />
                        <br />
                        K_D:
                        <asp:TextBox ID="K_DTextBox" runat="server" Text='<%# Bind("K_D") %>' />
                        <br />
                        K_T:
                        <asp:TextBox ID="K_TTextBox" runat="server" Text='<%# Bind("K_T") %>' />
                        <br />
                        ANADATE:
                        <asp:TextBox ID="ANADATETextBox" runat="server" Text='<%# Bind("ANADATE") %>' />
                        <br />
                        COMPLETE:
                        <asp:CheckBox ID="COMPLETECheckBox" runat="server" Checked='<%# Bind("COMPLETE") %>' />
                        <br />
                        DATE_SENT:
                        <asp:TextBox ID="DATE_SENTTextBox" runat="server" Text='<%# Bind("DATE_SENT") %>' />
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
                        <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
                        &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        CODE:
                        <asp:TextBox ID="CODETextBox" runat="server" Text='<%# Bind("CODE") %>' />
                        <br />
                        DUPLICATE:
                        <asp:TextBox ID="DUPLICATETextBox" runat="server" Text='<%# Bind("DUPLICATE") %>' />
                        <br />
                        AL_D:
                        <asp:TextBox ID="AL_DTextBox" runat="server" Text='<%# Bind("AL_D") %>' />
                        <br />
                        AL_T:
                        <asp:TextBox ID="AL_TTextBox" runat="server" Text='<%# Bind("AL_T") %>' />
                        <br />
                        AS_D:
                        <asp:TextBox ID="AS_DTextBox" runat="server" Text='<%# Bind("AS_D") %>' />
                        <br />
                        AS_T:
                        <asp:TextBox ID="AS_TTextBox" runat="server" Text='<%# Bind("AS_T") %>' />
                        <br />
                        CA_D:
                        <asp:TextBox ID="CA_DTextBox" runat="server" Text='<%# Bind("CA_D") %>' />
                        <br />
                        CA_T:
                        <asp:TextBox ID="CA_TTextBox" runat="server" Text='<%# Bind("CA_T") %>' />
                        <br />
                        CD_D:
                        <asp:TextBox ID="CD_DTextBox" runat="server" Text='<%# Bind("CD_D") %>' />
                        <br />
                        CD_T:
                        <asp:TextBox ID="CD_TTextBox" runat="server" Text='<%# Bind("CD_T") %>' />
                        <br />
                        CU_D:
                        <asp:TextBox ID="CU_DTextBox" runat="server" Text='<%# Bind("CU_D") %>' />
                        <br />
                        CU_T:
                        <asp:TextBox ID="CU_TTextBox" runat="server" Text='<%# Bind("CU_T") %>' />
                        <br />
                        FE_D:
                        <asp:TextBox ID="FE_DTextBox" runat="server" Text='<%# Bind("FE_D") %>' />
                        <br />
                        FE_T:
                        <asp:TextBox ID="FE_TTextBox" runat="server" Text='<%# Bind("FE_T") %>' />
                        <br />
                        PB_D:
                        <asp:TextBox ID="PB_DTextBox" runat="server" Text='<%# Bind("PB_D") %>' />
                        <br />
                        PB_T:
                        <asp:TextBox ID="PB_TTextBox" runat="server" Text='<%# Bind("PB_T") %>' />
                        <br />
                        MG_D:
                        <asp:TextBox ID="MG_DTextBox" runat="server" Text='<%# Bind("MG_D") %>' />
                        <br />
                        MG_T:
                        <asp:TextBox ID="MG_TTextBox" runat="server" Text='<%# Bind("MG_T") %>' />
                        <br />
                        MN_D:
                        <asp:TextBox ID="MN_DTextBox" runat="server" Text='<%# Bind("MN_D") %>' />
                        <br />
                        MN_T:
                        <asp:TextBox ID="MN_TTextBox" runat="server" Text='<%# Bind("MN_T") %>' />
                        <br />
                        SE_D:
                        <asp:TextBox ID="SE_DTextBox" runat="server" Text='<%# Bind("SE_D") %>' />
                        <br />
                        SE_T:
                        <asp:TextBox ID="SE_TTextBox" runat="server" Text='<%# Bind("SE_T") %>' />
                        <br />
                        ZN_D:
                        <asp:TextBox ID="ZN_DTextBox" runat="server" Text='<%# Bind("ZN_D") %>' />
                        <br />
                        ZN_T:
                        <asp:TextBox ID="ZN_TTextBox" runat="server" Text='<%# Bind("ZN_T") %>' />
                        <br />
                        NA_D:
                        <asp:TextBox ID="NA_DTextBox" runat="server" Text='<%# Bind("NA_D") %>' />
                        <br />
                        NA_T:
                        <asp:TextBox ID="NA_TTextBox" runat="server" Text='<%# Bind("NA_T") %>' />
                        <br />
                        K_D:
                        <asp:TextBox ID="K_DTextBox" runat="server" Text='<%# Bind("K_D") %>' />
                        <br />
                        K_T:
                        <asp:TextBox ID="K_TTextBox" runat="server" Text='<%# Bind("K_T") %>' />
                        <br />
                        ANADATE:
                        <asp:TextBox ID="ANADATETextBox" runat="server" Text='<%# Bind("ANADATE") %>' />
                        <br />
                        COMPLETE:
                        <asp:CheckBox ID="COMPLETECheckBox" runat="server" Checked='<%# Bind("COMPLETE") %>' />
                        <br />
                        DATE_SENT:
                        <asp:TextBox ID="DATE_SENTTextBox" runat="server" Text='<%# Bind("DATE_SENT") %>' />
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
                        <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
                        &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
                    </InsertItemTemplate>
                    <ItemTemplate>
                        inbICPID:
                        <asp:Label ID="inbICPIDLabel" runat="server" Text='<%# Eval("inbICPID") %>' />
                        <br />
                        CODE:
                        <asp:Label ID="CODELabel" runat="server" Text='<%# Bind("CODE") %>' />
                        <br />
                        DUPLICATE:
                        <asp:Label ID="DUPLICATELabel" runat="server" Text='<%# Bind("DUPLICATE") %>' />
                        <br />
                        AL_D:
                        <asp:Label ID="AL_DLabel" runat="server" Text='<%# Bind("AL_D") %>' />
                        <br />
                        AL_T:
                        <asp:Label ID="AL_TLabel" runat="server" Text='<%# Bind("AL_T") %>' />
                        <br />
                        AS_D:
                        <asp:Label ID="AS_DLabel" runat="server" Text='<%# Bind("AS_D") %>' />
                        <br />
                        AS_T:
                        <asp:Label ID="AS_TLabel" runat="server" Text='<%# Bind("AS_T") %>' />
                        <br />
                        CA_D:
                        <asp:Label ID="CA_DLabel" runat="server" Text='<%# Bind("CA_D") %>' />
                        <br />
                        CA_T:
                        <asp:Label ID="CA_TLabel" runat="server" Text='<%# Bind("CA_T") %>' />
                        <br />
                        CD_D:
                        <asp:Label ID="CD_DLabel" runat="server" Text='<%# Bind("CD_D") %>' />
                        <br />
                        CD_T:
                        <asp:Label ID="CD_TLabel" runat="server" Text='<%# Bind("CD_T") %>' />
                        <br />
                        CU_D:
                        <asp:Label ID="CU_DLabel" runat="server" Text='<%# Bind("CU_D") %>' />
                        <br />
                        CU_T:
                        <asp:Label ID="CU_TLabel" runat="server" Text='<%# Bind("CU_T") %>' />
                        <br />
                        FE_D:
                        <asp:Label ID="FE_DLabel" runat="server" Text='<%# Bind("FE_D") %>' />
                        <br />
                        FE_T:
                        <asp:Label ID="FE_TLabel" runat="server" Text='<%# Bind("FE_T") %>' />
                        <br />
                        PB_D:
                        <asp:Label ID="PB_DLabel" runat="server" Text='<%# Bind("PB_D") %>' />
                        <br />
                        PB_T:
                        <asp:Label ID="PB_TLabel" runat="server" Text='<%# Bind("PB_T") %>' />
                        <br />
                        MG_D:
                        <asp:Label ID="MG_DLabel" runat="server" Text='<%# Bind("MG_D") %>' />
                        <br />
                        MG_T:
                        <asp:Label ID="MG_TLabel" runat="server" Text='<%# Bind("MG_T") %>' />
                        <br />
                        MN_D:
                        <asp:Label ID="MN_DLabel" runat="server" Text='<%# Bind("MN_D") %>' />
                        <br />
                        MN_T:
                        <asp:Label ID="MN_TLabel" runat="server" Text='<%# Bind("MN_T") %>' />
                        <br />
                        SE_D:
                        <asp:Label ID="SE_DLabel" runat="server" Text='<%# Bind("SE_D") %>' />
                        <br />
                        SE_T:
                        <asp:Label ID="SE_TLabel" runat="server" Text='<%# Bind("SE_T") %>' />
                        <br />
                        ZN_D:
                        <asp:Label ID="ZN_DLabel" runat="server" Text='<%# Bind("ZN_D") %>' />
                        <br />
                        ZN_T:
                        <asp:Label ID="ZN_TLabel" runat="server" Text='<%# Bind("ZN_T") %>' />
                        <br />
                        NA_D:
                        <asp:Label ID="NA_DLabel" runat="server" Text='<%# Bind("NA_D") %>' />
                        <br />
                        NA_T:
                        <asp:Label ID="NA_TLabel" runat="server" Text='<%# Bind("NA_T") %>' />
                        <br />
                        K_D:
                        <asp:Label ID="K_DLabel" runat="server" Text='<%# Bind("K_D") %>' />
                        <br />
                        K_T:
                        <asp:Label ID="K_TLabel" runat="server" Text='<%# Bind("K_T") %>' />
                        <br />
                        ANADATE:
                        <asp:Label ID="ANADATELabel" runat="server" Text='<%# Bind("ANADATE") %>' />
                        <br />
                        COMPLETE:
                        <asp:CheckBox ID="COMPLETECheckBox" runat="server" Checked='<%# Bind("COMPLETE") %>' Enabled="false" />
                        <br />
                        DATE_SENT:
                        <asp:Label ID="DATE_SENTLabel" runat="server" Text='<%# Bind("DATE_SENT") %>' />
                        <br />
                        Comments:
                        <asp:Label ID="CommentsLabel" runat="server" Text='<%# Bind("Comments") %>' />
                        <br />
                        PassValStep:
                        <asp:Label ID="PassValStepLabel" runat="server" Text='<%# Bind("PassValStep") %>' />
                        <br />
                        Reviewed:
                        <asp:CheckBox ID="ReviewedCheckBox" runat="server" Checked='<%# Bind("Reviewed") %>' Enabled="false" />
                        <br />
                        FailedChems:
                        <asp:Label ID="FailedChemsLabel" runat="server" Text='<%# Bind("FailedChems") %>' />
                        <br />
                        tblSampleID:
                        <asp:Label ID="tblSampleIDLabel" runat="server" Text='<%# Bind("tblSampleID") %>' />
                        <br />
                        <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit" />
                        &nbsp;<asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" />
                        &nbsp;<asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" CommandName="New" Text="New" />
                    </ItemTemplate>
                </asp:FormView>
                <asp:SqlDataSource ID="SqlDataSourceBlanks" runat="server" ConnectionString="<%$ ConnectionStrings:RiverwatchWaterDEV %>" 
                    DeleteCommand="DELETE FROM [tblInboundICP] WHERE [inbICPID] = @inbICPID" 
                    InsertCommand="INSERT INTO [tblInboundICP] ([CODE], [DUPLICATE], [AL_D], [AL_T], [AS_D], [AS_T], [CA_D], [CA_T], [CD_D], [CD_T], [CU_D], [CU_T], [FE_D], [FE_T], [PB_D], [PB_T], [MG_D], [MG_T], [MN_D], [MN_T], [SE_D], [SE_T], [ZN_D], [ZN_T], [NA_D], [NA_T], [K_D], [K_T], [ANADATE], [COMPLETE], [DATE_SENT], [Comments], [PassValStep], [Reviewed], [FailedChems], [tblSampleID]) VALUES (@CODE, @DUPLICATE, @AL_D, @AL_T, @AS_D, @AS_T, @CA_D, @CA_T, @CD_D, @CD_T, @CU_D, @CU_T, @FE_D, @FE_T, @PB_D, @PB_T, @MG_D, @MG_T, @MN_D, @MN_T, @SE_D, @SE_T, @ZN_D, @ZN_T, @NA_D, @NA_T, @K_D, @K_T, @ANADATE, @COMPLETE, @DATE_SENT, @Comments, @PassValStep, @Reviewed, @FailedChems, @tblSampleID)" 
                 
                    UpdateCommand="UPDATE [tblInboundICP] SET [CODE] = @CODE, [DUPLICATE] = @DUPLICATE, [AL_D] = @AL_D, [AL_T] = @AL_T, [AS_D] = @AS_D, [AS_T] = @AS_T, [CA_D] = @CA_D, [CA_T] = @CA_T, [CD_D] = @CD_D, [CD_T] = @CD_T, [CU_D] = @CU_D, [CU_T] = @CU_T, [FE_D] = @FE_D, [FE_T] = @FE_T, [PB_D] = @PB_D, [PB_T] = @PB_T, [MG_D] = @MG_D, [MG_T] = @MG_T, [MN_D] = @MN_D, [MN_T] = @MN_T, [SE_D] = @SE_D, [SE_T] = @SE_T, [ZN_D] = @ZN_D, [ZN_T] = @ZN_T, [NA_D] = @NA_D, [NA_T] = @NA_T, [K_D] = @K_D, [K_T] = @K_T, [ANADATE] = @ANADATE, [COMPLETE] = @COMPLETE, [DATE_SENT] = @DATE_SENT, [Comments] = @Comments, [PassValStep] = @PassValStep, [Reviewed] = @Reviewed, [FailedChems] = @FailedChems, [tblSampleID] = @tblSampleID WHERE [inbICPID] = @inbICPID">
                    <DeleteParameters>
                        <asp:Parameter Name="inbICPID" Type="Int32" />
                    </DeleteParameters>
                    <InsertParameters>
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
                    </InsertParameters>
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
            </td>
            <td>
                <asp:FormView ID="FormViewSample" runat="server" DefaultMode="ReadOnly" DataKeyNames="inbICPID" DataSourceID="SqlDataSourceSamples">
                   
                    <ItemTemplate>
                        inbICPID:
                        <asp:Label ID="inbICPIDLabel" runat="server" Text='<%# Eval("inbICPID") %>' />
                        <br />
                        CODE:
                        <asp:Label ID="CODELabel" runat="server" Text='<%# Bind("CODE") %>' />
                        <br />
                        DUPLICATE:
                        <asp:Label ID="DUPLICATELabel" runat="server" Text='<%# Bind("DUPLICATE") %>' />
                        <br />
                        AL_D:
                        <asp:Label ID="AL_DLabel" runat="server" Text='<%# Bind("AL_D") %>' />
                        <br />
                        AL_T:
                        <asp:Label ID="AL_TLabel" runat="server" Text='<%# Bind("AL_T") %>' />
                        <br />
                        AS_D:
                        <asp:Label ID="AS_DLabel" runat="server" Text='<%# Bind("AS_D") %>' />
                        <br />
                        AS_T:
                        <asp:Label ID="AS_TLabel" runat="server" Text='<%# Bind("AS_T") %>' />
                        <br />
                        CA_D:
                        <asp:Label ID="CA_DLabel" runat="server" Text='<%# Bind("CA_D") %>' />
                        <br />
                        CA_T:
                        <asp:Label ID="CA_TLabel" runat="server" Text='<%# Bind("CA_T") %>' />
                        <br />
                        CD_D:
                        <asp:Label ID="CD_DLabel" runat="server" Text='<%# Bind("CD_D") %>' />
                        <br />
                        CD_T:
                        <asp:Label ID="CD_TLabel" runat="server" Text='<%# Bind("CD_T") %>' />
                        <br />
                        CU_D:
                        <asp:Label ID="CU_DLabel" runat="server" Text='<%# Bind("CU_D") %>' />
                        <br />
                        CU_T:
                        <asp:Label ID="CU_TLabel" runat="server" Text='<%# Bind("CU_T") %>' />
                        <br />
                        FE_D:
                        <asp:Label ID="FE_DLabel" runat="server" Text='<%# Bind("FE_D") %>' />
                        <br />
                        FE_T:
                        <asp:Label ID="FE_TLabel" runat="server" Text='<%# Bind("FE_T") %>' />
                        <br />
                        PB_D:
                        <asp:Label ID="PB_DLabel" runat="server" Text='<%# Bind("PB_D") %>' />
                        <br />
                        PB_T:
                        <asp:Label ID="PB_TLabel" runat="server" Text='<%# Bind("PB_T") %>' />
                        <br />
                        MG_D:
                        <asp:Label ID="MG_DLabel" runat="server" Text='<%# Bind("MG_D") %>' />
                        <br />
                        MG_T:
                        <asp:Label ID="MG_TLabel" runat="server" Text='<%# Bind("MG_T") %>' />
                        <br />
                        MN_D:
                        <asp:Label ID="MN_DLabel" runat="server" Text='<%# Bind("MN_D") %>' />
                        <br />
                        MN_T:
                        <asp:Label ID="MN_TLabel" runat="server" Text='<%# Bind("MN_T") %>' />
                        <br />
                        SE_D:
                        <asp:Label ID="SE_DLabel" runat="server" Text='<%# Bind("SE_D") %>' />
                        <br />
                        SE_T:
                        <asp:Label ID="SE_TLabel" runat="server" Text='<%# Bind("SE_T") %>' />
                        <br />
                        ZN_D:
                        <asp:Label ID="ZN_DLabel" runat="server" Text='<%# Bind("ZN_D") %>' />
                        <br />
                        ZN_T:
                        <asp:Label ID="ZN_TLabel" runat="server" Text='<%# Bind("ZN_T") %>' />
                        <br />
                        NA_D:
                        <asp:Label ID="NA_DLabel" runat="server" Text='<%# Bind("NA_D") %>' />
                        <br />
                        NA_T:
                        <asp:Label ID="NA_TLabel" runat="server" Text='<%# Bind("NA_T") %>' />
                        <br />
                        K_D:
                        <asp:Label ID="K_DLabel" runat="server" Text='<%# Bind("K_D") %>' />
                        <br />
                        K_T:
                        <asp:Label ID="K_TLabel" runat="server" Text='<%# Bind("K_T") %>' />
                        <br />
                        ANADATE:
                        <asp:Label ID="ANADATELabel" runat="server" Text='<%# Bind("ANADATE") %>' />
                        <br />
                        COMPLETE:
                        <asp:CheckBox ID="COMPLETECheckBox" runat="server" Checked='<%# Bind("COMPLETE") %>' Enabled="false" />
                        <br />
                        DATE_SENT:
                        <asp:Label ID="DATE_SENTLabel" runat="server" Text='<%# Bind("DATE_SENT") %>' />
                        <br />
                        Comments:
                        <asp:Label ID="CommentsLabel" runat="server" Text='<%# Bind("Comments") %>' />
                        <br />
                        PassValStep:
                        <asp:Label ID="PassValStepLabel" runat="server" Text='<%# Bind("PassValStep") %>' />
                        <br />
                        Reviewed:
                        <asp:CheckBox ID="ReviewedCheckBox" runat="server" Checked='<%# Bind("Reviewed") %>' Enabled="false" />
                        <br />
                        FailedChems:
                        <asp:Label ID="FailedChemsLabel" runat="server" Text='<%# Bind("FailedChems") %>' />
                        <br />
                        tblSampleID:
                        <asp:Label ID="tblSampleIDLabel" runat="server" Text='<%# Bind("tblSampleID") %>' />
                        <br />

                    </ItemTemplate>
                </asp:FormView>

                <asp:SqlDataSource ID="SqlDataSourceSamples" runat="server" ConnectionString="<%$ ConnectionStrings:RiverwatchWaterDEV %>" 
                >

                </asp:SqlDataSource>
                 </td>
                <td>
                    
                    <asp:FormView ID="FormViewDup" runat="server"   DefaultMode="ReadOnly" defDataKeyNames="inbICPID" DataSourceID="SqlDataSourceDups">
                    
                    <ItemTemplate>
                        inbICPID:
                        <asp:Label ID="inbICPIDLabel" runat="server" Text='<%# Eval("inbICPID") %>' />
                        <br />
                        CODE:
                        <asp:Label ID="CODELabel" runat="server" Text='<%# Bind("CODE") %>' />
                        <br />
                        DUPLICATE:
                        <asp:Label ID="DUPLICATELabel" runat="server" Text='<%# Bind("DUPLICATE") %>' />
                        <br />
                        AL_D:
                        <asp:Label ID="AL_DLabel" runat="server" Text='<%# Bind("AL_D") %>' />
                        <br />
                        AL_T:
                        <asp:Label ID="AL_TLabel" runat="server" Text='<%# Bind("AL_T") %>' />
                        <br />
                        AS_D:
                        <asp:Label ID="AS_DLabel" runat="server" Text='<%# Bind("AS_D") %>' />
                        <br />
                        AS_T:
                        <asp:Label ID="AS_TLabel" runat="server" Text='<%# Bind("AS_T") %>' />
                        <br />
                        CA_D:
                        <asp:Label ID="CA_DLabel" runat="server" Text='<%# Bind("CA_D") %>' />
                        <br />
                        CA_T:
                        <asp:Label ID="CA_TLabel" runat="server" Text='<%# Bind("CA_T") %>' />
                        <br />
                        CD_D:
                        <asp:Label ID="CD_DLabel" runat="server" Text='<%# Bind("CD_D") %>' />
                        <br />
                        CD_T:
                        <asp:Label ID="CD_TLabel" runat="server" Text='<%# Bind("CD_T") %>' />
                        <br />
                        CU_D:
                        <asp:Label ID="CU_DLabel" runat="server" Text='<%# Bind("CU_D") %>' />
                        <br />
                        CU_T:
                        <asp:Label ID="CU_TLabel" runat="server" Text='<%# Bind("CU_T") %>' />
                        <br />
                        FE_D:
                        <asp:Label ID="FE_DLabel" runat="server" Text='<%# Bind("FE_D") %>' />
                        <br />
                        FE_T:
                        <asp:Label ID="FE_TLabel" runat="server" Text='<%# Bind("FE_T") %>' />
                        <br />
                        PB_D:
                        <asp:Label ID="PB_DLabel" runat="server" Text='<%# Bind("PB_D") %>' />
                        <br />
                        PB_T:
                        <asp:Label ID="PB_TLabel" runat="server" Text='<%# Bind("PB_T") %>' />
                        <br />
                        MG_D:
                        <asp:Label ID="MG_DLabel" runat="server" Text='<%# Bind("MG_D") %>' />
                        <br />
                        MG_T:
                        <asp:Label ID="MG_TLabel" runat="server" Text='<%# Bind("MG_T") %>' />
                        <br />
                        MN_D:
                        <asp:Label ID="MN_DLabel" runat="server" Text='<%# Bind("MN_D") %>' />
                        <br />
                        MN_T:
                        <asp:Label ID="MN_TLabel" runat="server" Text='<%# Bind("MN_T") %>' />
                        <br />
                        SE_D:
                        <asp:Label ID="SE_DLabel" runat="server" Text='<%# Bind("SE_D") %>' />
                        <br />
                        SE_T:
                        <asp:Label ID="SE_TLabel" runat="server" Text='<%# Bind("SE_T") %>' />
                        <br />
                        ZN_D:
                        <asp:Label ID="ZN_DLabel" runat="server" Text='<%# Bind("ZN_D") %>' />
                        <br />
                        ZN_T:
                        <asp:Label ID="ZN_TLabel" runat="server" Text='<%# Bind("ZN_T") %>' />
                        <br />
                        NA_D:
                        <asp:Label ID="NA_DLabel" runat="server" Text='<%# Bind("NA_D") %>' />
                        <br />
                        NA_T:
                        <asp:Label ID="NA_TLabel" runat="server" Text='<%# Bind("NA_T") %>' />
                        <br />
                        K_D:
                        <asp:Label ID="K_DLabel" runat="server" Text='<%# Bind("K_D") %>' />
                        <br />
                        K_T:
                        <asp:Label ID="K_TLabel" runat="server" Text='<%# Bind("K_T") %>' />
                        <br />
                        ANADATE:
                        <asp:Label ID="ANADATELabel" runat="server" Text='<%# Bind("ANADATE") %>' />
                        <br />
                        COMPLETE:
                        <asp:CheckBox ID="COMPLETECheckBox" runat="server" Checked='<%# Bind("COMPLETE") %>' Enabled="false" />
                        <br />
                        DATE_SENT:
                        <asp:Label ID="DATE_SENTLabel" runat="server" Text='<%# Bind("DATE_SENT") %>' />
                        <br />
                        Comments:
                        <asp:Label ID="CommentsLabel" runat="server" Text='<%# Bind("Comments") %>' />
                        <br />
                        PassValStep:
                        <asp:Label ID="PassValStepLabel" runat="server" Text='<%# Bind("PassValStep") %>' />
                        <br />
                        Reviewed:
                        <asp:CheckBox ID="ReviewedCheckBox" runat="server" Checked='<%# Bind("Reviewed") %>' Enabled="false" />
                        <br />
                        FailedChems:
                        <asp:Label ID="FailedChemsLabel" runat="server" Text='<%# Bind("FailedChems") %>' />
                        <br />
                        tblSampleID:
                        <asp:Label ID="tblSampleIDLabel" runat="server" Text='<%# Bind("tblSampleID") %>' />
                        <br />

                    </ItemTemplate>
                </asp:FormView>
                <asp:SqlDataSource ID="SqlDataSourceDups" runat="server" ConnectionString="<%$ ConnectionStrings:RiverwatchWaterDEV %>" 
                   
                    ></asp:SqlDataSource>
                
            </td>
        </tr>
    </table>
   



</asp:Content>
