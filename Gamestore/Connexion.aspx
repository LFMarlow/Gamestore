<%@ Page Title="Connexion" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Connexion.aspx.cs" Inherits="Gamestore.Connexion" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="/Content/ContentCSS/Connexion.css" rel="stylesheet" />
    <div id="div_content_connexion">
        <h2><b><asp:Label ID="LblConnexion" runat="server" Text="Connexion"></asp:Label></b></h2>
        &nbsp;
        &nbsp;
        <asp:Label ID="LblMailConnexion" runat="server" Text="Adresse E-Mail"></asp:Label>
        &nbsp;
        <asp:TextBox ID="TxtBoxMailConnexion" TextMode="Email" placeholder="Adresse E-Mail" runat="server"></asp:TextBox>
        &nbsp;
        &nbsp;
        <asp:Label ID="LblMDPConnexion" runat="server" Text="Mot de Passe"></asp:Label>
        &nbsp;
        <asp:TextBox ID="TxtBoxMDPConnexion" placeholder="Mot de Passe" TextMode="Password" runat="server"></asp:TextBox>
        <asp:HyperLink ID="HPLoubli" NavigateUrl="~/PasswordOublie" CssClass="LabelConnect" runat="server">Mot de passe oublié</asp:HyperLink>
        &nbsp;
        &nbsp;
        <asp:Button ID="BtnConnexion" runat="server" Text="Connexion" OnClick="BtnConnexion_Click" />
        &nbsp;
        &nbsp;
        <asp:Label ID="LblNoRegister" runat="server" CssClass="LabelConnect" Text="Pas de compte ?"></asp:Label>
        <asp:HyperLink ID="HPLInscription" NavigateUrl="~/Inscription" runat="server">Inscrivez-vous</asp:HyperLink>
    </div>
</asp:Content>
