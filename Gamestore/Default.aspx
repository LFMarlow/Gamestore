<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Gamestore._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="/Content/ContentCSS/Accueil.css" type="text/css" rel="stylesheet" />

    <main class="main_content">
        <!-- PRESENTATION GAMESTORE -->
        <div class="section_content" aria-labelledby="aspnetTitle">
            <div class="class_content">
                <h2 class="title_default">GAMESTORE</h2>
                <br />
                <br />
                <p class="exp_default"><strong>Mission</strong> : Gamestore s'engage à offrir une expérience de jeu inégalée en proposant une large gamme de jeux vidéo, de consoles et d'accessoires de haute qualité, accessibles à tous les passionnés de jeux, des amateurs aux hardcore gamers.</p>
                <br />
                <p class="exp_default"><strong>Concept</strong> : Gamestore est une boutique spécialisée dans la vente de jeux vidéo, située en plein cœur de la ville. Notre objectif est de devenir la destination de choix pour tous les passionnés de jeux vidéo, en offrant non seulement les dernières sorties, mais aussi une sélection rigoureusement choisie de classiques et de jeux indépendants.</p>
                <br />
            </div>
            <h2 class="title_default">Découvrez nos meilleures ventes !</h2>
            <br />
            <!-- Caroussel affichant les meilleurs ventes (Bootstrap) -->
            <div id="carouselSold" class="carousel slide" data-bs-ride="carousel">
                <div class="carousel-indicators">
                    <button type="button" data-bs-target="#carouselSold" data-bs-slide-to="0" class="active" aria-current="true" aria-label="Slide 0"></button>
                    <button type="button" data-bs-target="#carouselSold" data-bs-slide-to="1" aria-label="Slide 1"></button>
                    <button type="button" data-bs-target="#carouselSold" data-bs-slide-to="2" aria-label="Slide 2"></button>
                </div>
                <div class="carousel-inner">
                    <div class="carousel-item active">
                        <img src="Content/ContentIMG/got.jpg" class="img_slide" />
                    </div>
                    <div class="carousel-item">
                        <img src="Content/ContentIMG/ml.jpg" class="img_slide" />
                    </div>
                    <div class="carousel-item">
                        <img src="Content/ContentIMG/vr.jpg" class="img_slide" />
                    </div>
                </div>
                <button class="carousel-control-prev" type="button" data-bs-target="#carouselSold" data-bs-slide="prev">
                    <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                    <span class="visually-hidden">Précédent</span>
                </button>
                <button class="carousel-control-next" type="button" data-bs-target="#carouselSold" data-bs-slide="next">
                    <span class="carousel-control-next-icon" aria-hidden="true"></span>
                    <span class="visually-hidden">Suivant</span>
                </button>
            </div>
        </div>
    <br />
    <br />
    </main>
</asp:Content>