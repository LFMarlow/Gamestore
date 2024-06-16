<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MonCompte.aspx.cs" Inherits="Gamestore.MonCompte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" type="text/css" href="Content/ContentCSS/MonCompte.css" />

    <section class="main_account">
        <div class="div_aside">
            <aside class="left_aside">
                <div class="hello">
                    <p>
                        <strong>Bonjour</strong>
                        <br />
                        <em>
                            <asp:Label ID="LblUsers" runat="server" Text=""></asp:Label></em>
                    </p>
                    <asp:Button ID="BtnDisconnect" runat="server" CssClass="btnDisconnect" Text="Me Deconnecter" />
                </div>
            </aside>
            <aside class="right_aside">
                <div class="menu_interact">
                    <asp:Button ID="BtnCVG" runat="server" CssClass="" Text="Création d'un jeu" OnClick="BtnCVG_Click" />
                    <asp:Button ID="BtnGestionStocks" runat="server" CssClass="" Text="Gestion des Stocks" OnClick="BtnGestionStocks_Click" />
                    <asp:Button ID="BtnDashboard" runat="server" CssClass="" Text="DashBoard" OnClick="BtnDashboard_Click" />
                    <asp:Button ID="BtnCreateEmploye" runat="server" CssClass="" Text="Création d'un employé" OnClick="BtnCreateEmploye_Click" />
                </div>
            </aside>
        </div>
        <br />
        <br />
        <div class="cvg">
            <div class="cvg_menu">
                <asp:UpdatePanel ID="UPCreateVideoGames" runat="server">
                    <ContentTemplate>
                        <h2 class="title_menu">Menu de Création d'un Jeux Vidéo</h2>
                        <br />
                        <br />
                        <article class="info_container">
                            <section class="cvg_first_section">
                                <asp:Label runat="server" Text="Image :"></asp:Label>
                                &nbsp;
                                &nbsp;
                                <asp:TextBox ID="TxtBoxURLImage" runat="server" placeholder="URL de l'image"></asp:TextBox>
                                &nbsp;
                                &nbsp;
                                <asp:Label ID="Label1" runat="server" CssClass="title_game" Text="Titre du jeu :"></asp:Label>
                                &nbsp;
                                &nbsp;
                                <asp:TextBox ID="TxtBoxTitreGame" runat="server" placeholder="Titre"></asp:TextBox>
                                &nbsp;
                                &nbsp;
                                <asp:Label runat="server" CssClass="price_game" Text="Prix du jeu :"></asp:Label>
                                &nbsp;
                                &nbsp;
                                <asp:TextBox ID="TxtBoxPriceGame" runat="server" placeholder="Prix"></asp:TextBox>
                            </section>
                        </article>
                        <br />
                        <br />
                        <article class="info_container_second">
                            <section class="cvg_second_section">
                                <asp:Label runat="server" Text="PEGI :"></asp:Label>
                                &nbsp;
                                &nbsp;
                                <asp:TextBox ID="TxtBoxPEGIGame" runat="server" placeholder="PEGI"></asp:TextBox>
                                &nbsp;
                                &nbsp;
                                <asp:Label ID="Label2" runat="server" CssClass="qte_game" Text="Quantité en stock :"></asp:Label>
                                &nbsp;
                                &nbsp;
                                <asp:TextBox ID="TxtBoxQuantityGame" runat="server" placeholder="Stock Total"></asp:TextBox>
                                &nbsp;
                                &nbsp;
                                <asp:Label runat="server" CssClass="genre_game" Text="Genre du jeu :"></asp:Label>
                                &nbsp;
                                &nbsp;
                                <asp:TextBox ID="TxtBoxGenreGame" runat="server" placeholder="(RPG, Action, Adventure..)"></asp:TextBox>
                            </section>
                        </article>
                        <br />
                        <br />
                        <div class="description_cvg">
                            <h3>Descriptif du Jeu :</h3>
                            &nbsp;
                            &nbsp;
                            <asp:TextBox ID="TxtBoxDescription" runat="server" placeholder="Description du jeu" CssClass="TxtBoxDescription" TextMode="MultiLine"></asp:TextBox>
                        </div>
                        <br />
                        <br />
                        <div class="div_btn">
                            <asp:Button ID="BtnCreateGame" runat="server" CssClass="btnCVG" Text="Créer le jeu" OnClick="BtnCreateGame_Click" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="UPCreateEmploye" runat="server">
                    <ContentTemplate>
                        <br />
                        <h2 class="title_menu">Inscription d'un Employé</h2>
                        <br />
                        <br />
                        <article class="info_container_ce">
                            <section class="ce_first_section">
                                <asp:Label runat="server" Text="Nom :"></asp:Label>
                                &nbsp;
                                &nbsp;
                                <asp:TextBox ID="TxtBoxNomEmploye" runat="server" placeholder="Nom de l'employé"></asp:TextBox>
                                &nbsp;
                                &nbsp;
                                <asp:Label ID="Label3" runat="server" Text="Prénom :"></asp:Label>
                                &nbsp;
                                &nbsp;
                                <asp:TextBox ID="TxtBoxPrenomEmploye" runat="server" placeholder="Prénom de l'employé"></asp:TextBox>
                                &nbsp;
                                &nbsp;
                                <asp:Label runat="server" Text="Adresse E-Mail :"></asp:Label>
                                &nbsp;
                                &nbsp;
                                <asp:TextBox ID="TxtBoxMailEmploye" runat="server" placeholder="Adresse E-Mail"></asp:TextBox>
                                &nbsp;
                                &nbsp;
                                <asp:Label runat="server" Text="Mot de Passe :"></asp:Label>
                                &nbsp;
                                &nbsp;
                                <asp:TextBox ID="TxtBoxPasswordEmploye" runat="server" placeholder="Mot de Passe du compte" TextMode="Password"></asp:TextBox>
                            </section>
                        </article>
                        <br />
                        <br />
                        <div class="div_btn">
                            <asp:Button ID="BtnCreateCompteEmploye" runat="server" CssClass="btnCVG" Text="Créer le compte" OnClick="BtnCreateCompteEmploye_Click" />
                        </div>
                        <br />
                        <br />
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="UPGestionStocks" runat="server">
                    <ContentTemplate>
                        <br />
                        <h2 class="title_menu">Gestion de la quantité des stocks</h2>
                        <br />
                        <br />
                        <article class="info_container_gs">
                            <section class="gs_first_section">
                                <asp:DropDownList ID="DDLVideoGames" runat="server"></asp:DropDownList>
                                &nbsp;
                                &nbsp;
                                &nbsp;
                                <div class="test">
                                    <asp:Image ID="IMGGame" CssClass="img_game" runat="server" />
                                    &nbsp;
                                    &nbsp;
                                    <asp:Label ID="LblTitleGame" runat="server" Text=""></asp:Label>
                                    &nbsp;
                                    <asp:Label ID="LblQteStock" runat="server" Text=""></asp:Label>
                                    &nbsp;
                                    &nbsp;
                                    <asp:TextBox ID="TxtBoxNewQte" runat="server"></asp:TextBox>
                                    <asp:Button ID="BtnChangeQte" runat="server" Text="Button" />
                                </div>
                            </section>
                        </article>
                        <br />
                        <br />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </section>
</asp:Content>
