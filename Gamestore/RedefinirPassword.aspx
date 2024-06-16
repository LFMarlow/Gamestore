<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RedefinirPassword.aspx.cs" Inherits="Gamestore.RedefinirPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="/Content/ContentCSS/RedefinirPassword.css" rel="stylesheet" />
    <div id="div_content_redefine_password">
        <h2><b><asp:Label ID="LblRedefinirPassword" runat="server" Text="Redéfinir un Mot de Passe"></asp:Label></b></h2>
        <br />
        <br />
        <asp:Label ID="LblFirstMDP" runat="server" CssClass="LablMDP" Text="Nouveau Mot de Passe"></asp:Label>
        <br />
        <asp:TextBox ID="TxtBoxFirstMDP" TextMode="Password" placeholder="Entrez le nouveau Mot de Passe" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="LblSecondMDP" runat="server" CssClass="LablMDP" Text="Confirmez le Mot de Passe"></asp:Label>
        <br />
        <asp:TextBox ID="TxtBoxSecondMDP" placeholder="Mot de Passe" TextMode="Password" runat="server"></asp:TextBox>
        <br />
        <br />
        <br />
        <br />
        <asp:Button ID="BtnNewPassword" runat="server" Text="Redefinir le Mot de Passe" />
        <br />
        <br />
    </div>
</asp:Content>
