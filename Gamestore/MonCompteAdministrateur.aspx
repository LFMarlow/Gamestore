<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MonCompteAdministrateur.aspx.cs" Inherits="Gamestore.MonCompte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" type="text/css" href="Content/ContentCSS/MonCompteAdministrateur.css" />
    <script type="text/javascript" src="Scripts/ContentJS/GraphParArticle.js"></script>
    <script type="text/javascript" src="Scripts/ContentJS/GraphParGenre.js"></script>

    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/chartjs-adapter-date-fns"></script>

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
                    <asp:Button ID="BtnCVG" runat="server" CssClass="" Text="Création d'un jeu" OnClick="BtnCVG_Click" />
                    <asp:Button ID="BtnGestionStocks" runat="server" CssClass="" Text="Gestion des Stocks" OnClick="BtnGestionStocks_Click" />
                    <asp:Button ID="BtnDashboard" runat="server" CssClass="" Text="DashBoard" OnClick="BtnDashboard_Click" />
                    <asp:Button ID="BtnCreateEmploye" runat="server" CssClass="" Text="Création d'un employé" OnClick="BtnCreateEmploye_Click" />
                    <asp:Button ID="BtnAddPromotions" runat="server" CssClass="" Text="Appliquer une promotion" OnClick="BtnAddPromotions_Click" />
                    <asp:Button ID="BtnSupprPromotions" runat="server" CssClass="" Text="Supprimer une promotion" OnClick="BtnSupprPromotions_Click" />
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
                        <article class="info_container_third">
                            <section class="cvg_third_section">
                                <asp:Label runat="server" Text="Appliquer une promotion :"></asp:Label>
                                &nbsp;
                                &nbsp;
                                <asp:TextBox ID="TxtBoxPromotion" runat="server" placeholder="Pourcentage de la promotion" TextMode="Number"></asp:TextBox>
                                &nbsp;
                                &nbsp;
                                <asp:Button ID="BtnCalculDiscount" runat="server" Text="Calcul du prix après remise" OnClick="BtnCalculDiscount_Click" />
                                &nbsp;
                                &nbsp;
                                <asp:Label ID="Label8" runat="server" Visible="false" Text="Le nouveau prix sera de : "></asp:Label>
                                <asp:Label ID="LblCalculPrice" runat="server" Visible="false" Text=""></asp:Label>
                                <asp:Label ID="Label10" runat="server" Visible="false" Text="€"></asp:Label>
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
                                <asp:Label ID="LblSelectGame" runat="server" Text="Veuillez selectionner un jeu : "></asp:Label>
                                <asp:DropDownList ID="DDLVideoGames" runat="server" AutoPostBack="true" OnTextChanged="DDLVideoGames_TextChanged"></asp:DropDownList>
                                &nbsp;
                                &nbsp;
                                <br />
                                <br />
                                <asp:Image ID="IMGGame" CssClass="img_game" runat="server" />
                                &nbsp;
                                &nbsp;
                                <div id="contenu_game_gest_stock" class="contenu_game" runat="server">
                                    <asp:Label ID="Label4" runat="server" Text="Nom du Jeu :"></asp:Label>
                                    <asp:Label ID="LblTitleGame" runat="server" Text=""></asp:Label>
                                    &nbsp;
                                    <div class="contenu2_game">
                                        <asp:Label ID="Label5" runat="server" Text="Stock Total du jeu :"></asp:Label>
                                        <asp:Label ID="LblQteStock" runat="server" Text=""></asp:Label>
                                    </div>
                                    &nbsp;
                                    &nbsp;
                                    <hr class="separate" />
                                    <br />
                                    <br />
                                    <div class="contenu3_game">
                                        <asp:Label ID="LblNewQte" runat="server" Text="Veuillez entrer la nouvelle quantité :"></asp:Label>
                                        <asp:TextBox ID="TxtBoxNewQte" runat="server" placeholder="Entrez la nouvelle quantité" TextMode="Number"></asp:TextBox>
                                        &nbsp;
                                        <asp:Button ID="BtnChangeQte" runat="server" Text="Valider" OnClick="BtnChangeQte_Click" />
                                    </div>
                                </div>
                            </section>
                        </article>
                        <br />
                        <br />
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:Panel ID="PnlAddPromotions" runat="server">
                        <br />
                        <h2 class="title_menu">Application d'une promotion</h2>
                        <br />
                        <br />
                        <article class="info_container_gs">
                            <section class="gs_first_section">
                                <asp:Label ID="Label6" runat="server" Text="Veuillez selectionner un jeu : "></asp:Label>
                                <asp:DropDownList ID="DdlGameForPromotions" runat="server" AutoPostBack="true" OnTextChanged="DdlGameForPromotions_TextChanged"></asp:DropDownList>
                                &nbsp;
                                &nbsp;
                                <br />
                                <br />
                                <asp:Image ID="ImgGamePromo" CssClass="img_game" runat="server" />
                                &nbsp;
                                &nbsp;
                                <div id="contenue_game" class="contenu_game" runat="server">
                                    <asp:Label ID="Label7" runat="server" Text="Nom du Jeu :"></asp:Label>
                                    <asp:Label ID="LblNameGame" runat="server" Text=""></asp:Label>
                                    &nbsp;
                                    <div class="contenu2_game">
                                        <asp:Label ID="Label9" runat="server" Text="Prix du jeu :"></asp:Label>
                                        <asp:Label ID="LblActualPrice" runat="server" Text=""></asp:Label>
                                    </div>
                                    &nbsp;
                                    &nbsp;
                                    <hr class="separate" />
                                    <br />
                                    <br />
                                    <div class="contenu3_game">
                                        <asp:Label ID="Label11" runat="server" Text="Veuillez entrer la remise à appliqué :"></asp:Label>
                                        <asp:TextBox ID="TxtBoxRemise" runat="server" placeholder="Entrez la remise à appliquer" TextMode="Number"></asp:TextBox>
                                        &nbsp;
                                        <asp:Button ID="BtnCalculPromotions" runat="server" Text="Valider" OnClick="BtnCalculPromotions_Click" />
                                    </div>
                                </div>
                            </section>
                        </article>
                        <br />
                        <br />
                </asp:Panel>
                <asp:Panel ID="PnlSupprPromotions" runat="server">
                    <br />
                    <h2 class="title_menu">Supprimer une promotion</h2>
                    <br />
                    <br />
                    <article class="info_container_gs">
                        <section class="gs_first_section">
                            <asp:Label ID="Label12" runat="server" Text="Veuillez selectionner un jeu : "></asp:Label>
                            <asp:DropDownList ID="DdlSupprPromotions" runat="server" AutoPostBack="true" OnTextChanged="DdlSupprPromotions_TextChanged"></asp:DropDownList>
                            &nbsp;
                            &nbsp;
                            <br />
                            <br />
                            <asp:Image ID="ImgSupprPromo" CssClass="img_game" runat="server" />
                            &nbsp;
                            &nbsp;
                            <div  id="contenu_game" class="contenu_game" runat="server">
                                <asp:Label ID="Label13" runat="server" Text="Nom du Jeu :"></asp:Label>
                                <asp:Label ID="LblNomGame" runat="server" Text=""></asp:Label>
                                &nbsp;
                                <div class="contenu2_game">
                                    <asp:Label ID="Label15" runat="server" Text="Prix du jeu :"></asp:Label>
                                    <asp:Label ID="LblPriceGame" runat="server" Text=""></asp:Label>
                                </div>
                                &nbsp;
                                &nbsp;
                                <hr class="separate" />
                                <br />
                                <br />
                                <div id="contenu3_game" class="contenu3_game" runat="server">
                                    <asp:Button ID="BtnDeletePromotions" runat="server" Text="Supprimer la promotions en cours" OnClick="BtnDeletePromotions_Click" />
                                </div>
                            </div>
                        </section>
                    </article>
                    <br />
                    <br />
                </asp:Panel>
            </div>
        </div>
        <asp:Panel ID="PnlReadSale" runat="server">
            <asp:Button ID="BtnChartByGenre" runat="server" Text="Vente par Genre" OnClick="BtnChartByGenre_Click" OnClientClick="return graphByGenreClick();" />
            <asp:Button ID="BtnChartByItem" runat="server" Text="Vente par Article" OnClick="BtnChartByItem_Click" OnClientClick="return graphByItemClick();" />
        </asp:Panel>
        <br />
        <br />
        <asp:Panel ID="PnlSaleByGenre" runat="server">
            <div id="GraphVenteGenre" class="graph_vente" style="background-color: white;">
                <canvas id="salesChart" width="400" height="400"></canvas>
            </div>
            <br />
        </asp:Panel>
        <asp:Panel ID="PnlSaleByItem" runat="server">
            <div id="GraphVenteArticle" class="graph_vente" style="background-color: white;">
                <canvas id="salesChartArticle" width="400" height="400"></canvas>
            </div>
            <br />
        </asp:Panel>
        <asp:Button ID="BtnDetailsChart" runat="server" Text="Détails" OnClick="BtnDetailsChart_Click" />
    </section>
</asp:Content>
