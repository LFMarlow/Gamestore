<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inscription.aspx.cs" Inherits="Gamestore.Inscription" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="/Content/ContentCSS/Inscription.css" rel="stylesheet" />
    <main aria-labelledby="title">
        <div id="div_content" >
            <h2><b><asp:Label ID="LblInscription" runat="server" Text="Inscription"></asp:Label></b></h2>
            &nbsp;
            &nbsp;
            <asp:Label ID="LblNom" runat="server" Text="Votre Nom :"></asp:Label>
            <asp:TextBox class="TxtBx" ID="TxtBoxNom" wrapperClass="mb-4" placeholder="Votre Nom" runat="server"></asp:TextBox>
            &nbsp;
            &nbsp;
            <asp:Label ID="LblPrenom" runat="server" Text="Votre Prénom :"></asp:Label>
            <asp:TextBox class="TxtBx" ID="TxtBoxPrenom" placeholder="Votre Prenom" runat="server"></asp:TextBox>
            &nbsp;
            &nbsp;
            <asp:Label ID="LblMail" runat="server" Text="Votre Adresse Mail :"></asp:Label>
            <asp:TextBox class="TxtBx" ID="TxtBoxMail" TextMode="Email" placeholder="Votre Adresse Mail" runat="server"></asp:TextBox>
            &nbsp;
            &nbsp;
            <asp:Label ID="LblMdp" runat="server" Text="Votre Mot de passe :"></asp:Label>
            <asp:TextBox class="TxtBx" ID="TxtBoxMDP" TextMode="Password" placeholder="Votre mot de passe" runat="server"></asp:TextBox>
            &nbsp;
            &nbsp;
            <asp:Label ID="LblAdressePostale" runat="server" Text="Votre Adresse Postal :"></asp:Label>
            <asp:TextBox class="TxtBx" ID="TxtBoxAP" placeholder="Votre Adresse Postal" runat="server"></asp:TextBox>
            &nbsp;
            &nbsp;
            <asp:Button class="BtnInscr" ID="BtnInscription" runat="server" Text="S'inscrire" OnClick="BtnInscription_Click" />
            &nbsp;
            &nbsp;
        </div>
    </main>
</asp:Content>
