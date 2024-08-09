<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MonCompteUtilisateur.aspx.cs" Inherits="Gamestore.MonCompteUtilisateur" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" type="text/css" href="Content/ContentCSS/MonCompteUtilisateur.css" />

    <section class="main_account">
        <div class="div_aside">
            <aside class="left_aside">
                <div class="hello">
                    <p>
                        <strong>Bonjour</strong>
                        <br />
                        <em>
                            <asp:Label ID="LblUsers" runat="server" Text=""></asp:Label>
                        </em>
                    </p>
                    <asp:Button ID="BtnDisconnect" runat="server" CssClass="btnDisconnect" Text="Me Deconnecter" OnClick="BtnDisconnect_Click" />
                </div>
            </aside>
            <aside class="right_aside">
                <div class="menu_interact">
                    <asp:Button ID="BtnChangeInfo" runat="server" CssClass="change_info" Text="Modification des informations personnelles" OnClick="BtnChangeInfo_Click" />
                    <asp:Button ID="BtnHistCommandValider" runat="server" CssClass="" Text="Historique des commandes" OnClick="BtnHistCommandValider_Click" />
                    <asp:Button ID="BtnHistCommandNonLivre" runat="server" CssClass="" Text="Commandes non récupéré" OnClick="BtnHistCommandNonLivre_Click" />
                </div>
            </aside>
        </div>
        <br />
        <br />
        <h2 id="title_menu" class="title_menu" runat="server"></h2>
        <br />
        <asp:Panel ID="PnlHistCommand" runat="server">
        </asp:Panel>
        <asp:Panel ID="PnlModifInfos" runat="server">
            <div class="mi">
                <div class="mi_left">
                    <p>Prénom :</p>
                    <asp:TextBox ID="TxtBoxPrenom" runat="server"></asp:TextBox>
                    <br />
                    <br />
                    <p>Nom de Famille :</p>
                    <asp:TextBox ID="TxtBoxNom" runat="server"></asp:TextBox>
                </div>
                <div class="mi_right">
                    <p>Email :</p>
                    <asp:TextBox ID="TxtBoxEmail" runat="server" TextMode="Email"></asp:TextBox>
                    <br />
                    <br />
                    <p>Adresse de Facturation :</p>
                    <asp:TextBox ID="TxtBoxAdresse" runat="server"></asp:TextBox>
                </div>
            </div>
            <br />
            <div class="btn_validate">
                <asp:Button ID="BtnValidate" runat="server" CssClass="btnValidate" Text="Valider les informations" OnClick="BtnValidate_Click" />
            </div>
            <br />
            <br />
            <h2 class="title_menu">Changer de mot de passe</h2>
            <br />
            <br />
            <div class="mi_password">
                <br />
                <div class="actual_password">
                    <asp:TextBox ID="TxtBoxActualPassword" placeholder="Mot de passe actuel" runat="server" TextMode="Password"></asp:TextBox>
                </div>
                <br />
                <div class="new_password">
                    <asp:TextBox ID="TxtBoxNewPassword" placeholder="Nouveau mot de passe" runat="server" TextMode="Password"></asp:TextBox>
                </div>
                <br />
                <div class="confirm_actual_password">
                    <asp:TextBox ID="TxtBoxNewPasswordConfirmed" placeholder="Confirmez le mot de passe" runat="server" TextMode="Password"></asp:TextBox>
                </div>
                <br />
            </div>
            <br />
            <div class="btn_validate">
                <asp:Button ID="BtnChangePassword" CssClass="btnChangePassword" runat="server" Text="Changement du mot de passe" OnClick="BtnChangePassword_Click" />
            </div>
        </asp:Panel>
    </section>
</asp:Content>
