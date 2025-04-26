<%@ Page Title="Login Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AuthorInsights.Login" %>
<%@ Register Src="~/CaptchaButton.ascx" TagPrefix="uc" TagName="CaptchaButton" %>

<%-- Author: Webstrar 51 Group --%>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h2 id="title"><%: Title %></h2>
        <asp:Label ID="WelcomeLbl" runat="server" Text="Looks like you're not a member yet! Sign up for free below!"></asp:Label>
        <h3>Login:</h3>
        <asp:Label ID="UsernameLbl" runat="server" Text="Username:"></asp:Label>
        <br />
        <asp:TextBox ID="UsernameTxtbx" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="PasswordLbl" runat="server" Text="Password:"></asp:Label>
        <br />
        <asp:TextBox type="password" ID="PasswordTxtbx" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="SaveCookiesLbl" runat="server" Text="Save to cookies? "></asp:Label>
        <asp:CheckBox ID="SaveCookiesChkbx" runat="server" />
        <br />
        <asp:Label ID="LoginFailedLbl" runat="server" ForeColor="Red"></asp:Label>
        <br />
        <uc:CaptchaButton ID="LoginBtn" runat="server" Text="Login" OnSubmitClicked="LoginBtn_Click" />
        <h2>Cookies Test:</h2>
        <asp:Label ID="SavedUsernameLbl" runat="server" Text="Saved Username: "></asp:Label>
        <br />
        <asp:Label ID="SavedPasswordLbl" runat="server" Text="Saved Password: "></asp:Label>
    </main>
</asp:Content>
