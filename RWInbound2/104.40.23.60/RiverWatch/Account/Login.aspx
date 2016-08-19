<%@ Page Title="Log in" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="RWInbound2.Account.Login" %>


<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Title %></h1>
    </hgroup>
<%--    <section id="loginForm">--%>
        <h3>Use your River Watch user name to log in.</h3>

                <p class="validation-summary-errors">
                    <asp:Label runat="server" ID="lblFailureText" />
                </p>

                    <ol>
                        <li>
                            <asp:Label runat="server" AssociatedControlID="tbUserName">User name</asp:Label>
                            <asp:TextBox runat="server" ID="tbUserName" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="tbUserName" CssClass="field-validation-error" ErrorMessage="The user name field is required." />
                        </li>
                        <li>
                            <asp:Label runat="server" AssociatedControlID="tbPassword">Password</asp:Label>
                            <asp:TextBox runat="server" ID="tbPassword" TextMode="Password" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="tbPassword" CssClass="field-validation-error" ErrorMessage="The password field is required." />
                        </li>
                        <li>
                            <asp:CheckBox runat="server" ID="cbRememberMe" />
                            <asp:Label runat="server" AssociatedControlID="cbRememberMe" CssClass="checkbox">Remember me?</asp:Label>
                        </li>
                    </ol>
                    <asp:Button runat="server"  id="btnLogin" Text="Log in" OnClick="btnLogin_Click" BackColor="#6699FF" Font-Bold="True" Font-Size="Large"  />
   

       <%-- <p>
            <asp:HyperLink runat="server" ID="RegisterHyperLink" ViewStateMode="Disabled">Register</asp:HyperLink>
            if you don't have an account.
        </p>--%>
<%--    </section>--%>

<%--    <section id="socialLoginForm">
        <h2>Use another service to log in.</h2>
        <uc:OpenAuthProviders runat="server" ID="OpenAuthLogin" />
    </section>--%>
</asp:Content>
