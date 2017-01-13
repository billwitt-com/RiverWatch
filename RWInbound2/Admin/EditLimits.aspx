<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditLimits.aspx.cs" Inherits="RWInbound2.Edit.EditLimits" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Label runat="server" Text="Validation Limits Editor   "></asp:Label>
    <asp:Label ID="lblWelcomeName" runat="server" ></asp:Label>
    
    <asp:FormView ID="FormView1" runat="server" PagerSettings-Mode="NumericFirstLast"  AllowPaging="True" DefaultMode="ReadOnly" DataKeyNames="ID" DataSourceID="SqlDataSource1">
        <EditItemTemplate>
            ID:
            <asp:Label ID="IDLabel1" runat="server" Text='<%# Eval("ID") %>' />
            <br />
            Element:
            <asp:TextBox ID="ElementTextBox" runat="server" Text='<%# Bind("Element") %>' />
            <br />
            RowID:
            <asp:TextBox ID="RowIDTextBox" runat="server" Text='<%# Bind("RowID") %>' />
            <br />
            Reporting:
            <asp:TextBox ID="ReportingTextBox" runat="server" Text='<%# Bind("Reporting") %>' />
            <br />
            MDL:
            <asp:TextBox ID="MDLTextBox" runat="server" Text='<%# Bind("MDL") %>' />
            <br />
            DvsTDifference:
            <asp:TextBox ID="DvsTDifferenceTextBox" runat="server" Text='<%# Bind("DvsTDifference") %>' />
            <br />
>
            <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
            &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
        </EditItemTemplate>
        <InsertItemTemplate>
            Element:
            <asp:TextBox ID="ElementTextBox" runat="server" Text='<%# Bind("Element") %>' />
            <br />
            RowID:
            <asp:TextBox ID="RowIDTextBox" runat="server" Text='<%# Bind("RowID") %>' />
            <br />
            Reporting:
            <asp:TextBox ID="ReportingTextBox" runat="server" Text='<%# Bind("Reporting") %>' />
            <br />
            MDL:
            <asp:TextBox ID="MDLTextBox" runat="server" Text='<%# Bind("MDL") %>' />
            <br />
            DvsTDifference:
            <asp:TextBox ID="DvsTDifferenceTextBox" runat="server" Text='<%# Bind("DvsTDifference") %>' />
            <br />

            <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
            &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
        </InsertItemTemplate>
        <ItemTemplate>
            ID:
            <asp:Label ID="IDLabel" runat="server" Text='<%# Eval("ID") %>' />
            <br />
            Element:
            <asp:Label ID="ElementLabel" runat="server" Text='<%# Bind("Element") %>' />
            <br />
            RowID:
            <asp:Label ID="RowIDLabel" runat="server" Text='<%# Bind("RowID") %>' />
            <br />
            Reporting:
            <asp:Label ID="ReportingLabel" runat="server" Text='<%# Bind("Reporting") %>' />
            <br />
            MDL:
            <asp:Label ID="MDLLabel" runat="server" Text='<%# Bind("MDL") %>' />
            <br />
            DvsTDifference:
            <asp:Label ID="DvsTDifferenceLabel" runat="server" Text='<%# Bind("DvsTDifference") %>' />
            <br />

            <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit" />
            &nbsp;<asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" />
            &nbsp;<asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" CommandName="New" Text="New" />
        </ItemTemplate>
    </asp:FormView>
       
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" OnInserting="SqlDataSource1_Inserting" OnUpdating="SqlDataSource1_Updating" ConnectionString="<%$ ConnectionStrings:RiverwatchDEV %>" 
        DeleteCommand="DELETE FROM [tlkLimits] WHERE [ID] = @ID" 
        InsertCommand="INSERT INTO [tlkLimits] ([Element], [RowID], [Reporting], [MDL], [DvsTDifference], [DateCreated], [UserCreated],[Valid] ) VALUES (@Element, @RowID, @Reporting, @MDL, @DvsTDifference,  @DateCreated,  @UserCreated, @Valid )" 
        SelectCommand="SELECT * FROM [tlkLimits]" 
        UpdateCommand="UPDATE [tlkLimits] SET [Element] = @Element, [RowID] = @RowID, [Reporting] = @Reporting, [MDL] = @MDL, [DvsTDifference] = @DvsTDifference, [DateCreated] = @DateCreated, [UserCreated] = @UserCreated, [Valid] = @Valid WHERE [ID] = @ID">
        <DeleteParameters>
            <asp:Parameter Name="ID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="Element" Type="String" />
            <asp:Parameter Name="RowID" Type="String" />
            <asp:Parameter Name="Reporting" Type="Decimal" />
            <asp:Parameter Name="MDL" Type="Decimal" />
            <asp:Parameter Name="DvsTDifference" Type="Decimal" />
            <asp:Parameter DbType="Date" Name="DateCreated" />
            <asp:Parameter Name="UserCreated" Type="String" />
            <asp:Parameter Name="Valid" Type="Boolean" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="Element" Type="String" />
            <asp:Parameter Name="RowID" Type="String" />
            <asp:Parameter Name="Reporting" Type="Decimal" />
            <asp:Parameter Name="MDL" Type="Decimal" />
            <asp:Parameter Name="DvsTDifference" Type="Decimal" />
            <asp:Parameter DbType="Date" Name="DateCreated" />
            <asp:Parameter Name="UserCreated" Type="String" />
            <asp:Parameter Name="Valid" Type="Boolean" />
            <asp:Parameter Name="ID" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
       
</asp:Content>
