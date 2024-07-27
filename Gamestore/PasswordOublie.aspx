<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PasswordOublie.aspx.cs" Inherits="Gamestore.PasswordOublie" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="/Content/ContentCSS/PasswordOublie.css" rel="stylesheet" />
    <div id="div_content_passwordForgot">
        <h2><b><asp:Label ID="LblMotdePasseOublie" runat="server" Text="Vous allez recevoir un Mail pour redéfinir un nouveau Mot de Passe"></asp:Label></b></h2>
        <br />
        <br />
        <asp:Label ID="LblMail" runat="server" CssClass="LabelOubli" Text="Adresse E-Mail"></asp:Label>
        <br />
        <asp:TextBox ID="TxtBoxMail" TextMode="Email" placeholder="Votre Adresse E-Mail" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="BtnChangePassword" runat="server" CssClass="BtnChangePassword" Text="Valider" OnClick="BtnChangePassword_Click" />
        <br />
        <br />
    </div>
</asp:Content>
