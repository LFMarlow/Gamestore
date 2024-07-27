<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Gamestore._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="/Content/ContentCSS/Accueil.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="Scripts/ContentJS/Carousel.js"></script>

    <main class="main_content">

        <div class="section_content" aria-labelledby="aspnetTitle">
            <div class="class_content">
                <h2 class="title_default">GAMESTORE</h2>
                <br />
                <br />
                <p class="exp_default"><strong>Mission</strong> : Gamestore s'engage à offrir une expérience de jeu inégalée en proposant une large gamme de jeux vidéo, de consoles et d'accessoires de haute qualité, accessibles à tous les passionnés de jeux, des amateurs aux hardcore gamers.</p>
                <br />
                <p class="exp_default"><strong>Concept</strong> : Gamestore est une boutique spécialisée dans la vente de jeux vidéo, située en plein cœur de la ville. Notre objectif est de devenir la destination de choix pour tous les passionnés de jeux vidéo, en offrant non seulement les dernières sorties, mais aussi une sélection rigoureusement choisie de classiques et de jeux indépendants.</p>
            </div>
            <div id="carouselSold" class="carousel slide" data-bs-ride="carousel">
                <h2 class="title_default">Découvrez nos tout derniers jeux !</h2>
                <br />
                <div class="carousel-indicators">
                    <button type="button" data-bs-target="#carouselSold" data-bs-slide-to="0" class="active" aria-current="true" aria-label="Slide 0"></button>
                    <button type="button" data-bs-target="#carouselSold" data-bs-slide-to="1" aria-label="Slide 1"></button>
                    <button type="button" data-bs-target="#carouselSold" data-bs-slide-to="2" aria-label="Slide 2"></button>
                    <button type="button" data-bs-target="#carouselSold" data-bs-slide-to="3" aria-label="Slide 3"></button>
                    <button type="button" data-bs-target="#carouselSold" data-bs-slide-to="4" aria-label="Slide 4"></button>
                    <button type="button" data-bs-target="#carouselSold" data-bs-slide-to="5" aria-label="Slide 5"></button>
                    <button type="button" data-bs-target="#carouselSold" data-bs-slide-to="6" aria-label="Slide 6"></button>
                    <button type="button" data-bs-target="#carouselSold" data-bs-slide-to="7" aria-label="Slide 7"></button>
                    <button type="button" data-bs-target="#carouselSold" data-bs-slide-to="8" aria-label="Slide 8"></button>
                    <button type="button" data-bs-target="#carouselSold" data-bs-slide-to="9" aria-label="Slide 9"></button>
                </div>
                <div class="carousel-inner">
                    <div class="carousel-item active">
                        <a id="img0" runat="server">
                            <asp:Image ID="Image0" CssClass="img_slide" runat="server" /></a>
                    </div>
                    <div class="carousel-item">
                        <a id="img1" runat="server">
                            <asp:Image ID="Image1" CssClass="img_slide" runat="server" /></a>
                    </div>
                    <div class="carousel-item">
                        <a id="img2" runat="server">
                            <asp:Image ID="Image2" CssClass="img_slide" runat="server" /></a>
                    </div>
                    <div class="carousel-item ">
                        <a id="img3" runat="server">
                            <asp:Image ID="Image3" CssClass="img_slide" runat="server" /></a>
                    </div>
                    <div class="carousel-item">
                        <a id="img4" runat="server">
                            <asp:Image ID="Image4" CssClass="img_slide" runat="server" /></a>
                    </div>
                    <div class="carousel-item">
                        <a id="img5" runat="server">
                            <asp:Image ID="Image5" CssClass="img_slide" runat="server" /></a>
                    </div>
                    <div class="carousel-item">
                        <a id="img6" runat="server">
                            <asp:Image ID="Image6" CssClass="img_slide" runat="server" /></a>
                    </div>
                    <div class="carousel-item">
                        <a id="img7" runat="server">
                            <asp:Image ID="Image7" CssClass="img_slide" runat="server" /></a>
                    </div>
                    <div class="carousel-item">
                        <a id="img8" runat="server">
                            <asp:Image ID="Image8" CssClass="img_slide" runat="server" /></a>
                    </div>
                    <div class="carousel-item">
                        <a id="img9" runat="server">
                            <asp:Image ID="Image9" CssClass="img_slide" runat="server" /></a>
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
            <div id="carouselReduction" class="carousel slide" data-bs-ride="carousel">
                <h2 id="title_promo" class="title_default" runat="server">Découvrez nos meilleures promotions !</h2>
                <br />
                <div class="carousel-indicators">
                    <!-- Indicateurs initialement définis pour correspondre aux slides -->
                    <button type="button" data-bs-target="#carouselReduction" data-bs-slide-to="0" class="active" aria-current="true" aria-label="Slide 0"></button>
                    <button type="button" data-bs-target="#carouselReduction" data-bs-slide-to="1" aria-label="Slide 1"></button>
                    <button type="button" data-bs-target="#carouselReduction" data-bs-slide-to="2" aria-label="Slide 2"></button>
                    <button type="button" data-bs-target="#carouselReduction" data-bs-slide-to="3" aria-label="Slide 3"></button>
                    <button type="button" data-bs-target="#carouselReduction" data-bs-slide-to="4" aria-label="Slide 4"></button>
                    <button type="button" data-bs-target="#carouselReduction" data-bs-slide-to="5" aria-label="Slide 5"></button>
                    <button type="button" data-bs-target="#carouselReduction" data-bs-slide-to="6" aria-label="Slide 6"></button>
                    <button type="button" data-bs-target="#carouselReduction" data-bs-slide-to="7" aria-label="Slide 7"></button>
                    <button type="button" data-bs-target="#carouselReduction" data-bs-slide-to="8" aria-label="Slide 8"></button>
                    <button type="button" data-bs-target="#carouselReduction" data-bs-slide-to="9" aria-label="Slide 9"></button>
                </div>
                <div class="carousel-inner">
                    <!-- Slides du carrousel -->
                    <div class="carousel-item active">
                        <a id="A0" runat="server">
                            <asp:Image ID="ImgReduc0" CssClass="img_slide" runat="server" />
                        </a>
                        <div class="discount">
                            <asp:Label ID="LblDiscount0" CssClass="LblDiscount" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <div class="carousel-item">
                        <a id="A1" runat="server">
                            <asp:Image ID="ImgReduc1" CssClass="img_slide" runat="server" />
                        </a>
                        <div class="discount">
                            <asp:Label ID="LblDiscount1" CssClass="LblDiscount" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <div class="carousel-item">
                        <a id="A2" runat="server">
                            <asp:Image ID="ImgReduc2" CssClass="img_slide" runat="server" />
                        </a>
                        <div class="discount">
                            <asp:Label ID="LblDiscount2" CssClass="LblDiscount" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <div class="carousel-item">
                        <a id="A3" runat="server">
                            <asp:Image ID="ImgReduc3" CssClass="img_slide" runat="server" />
                        </a>
                        <div class="discount">
                            <asp:Label ID="LblDiscount3" CssClass="LblDiscount" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <div class="carousel-item">
                        <a id="A4" runat="server">
                            <asp:Image ID="ImgReduc4" CssClass="img_slide" runat="server" />
                        </a>
                        <div class="discount">
                            <asp:Label ID="LblDiscount4" CssClass="LblDiscount" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <div class="carousel-item">
                        <a id="A5" runat="server">
                            <asp:Image ID="ImgReduc5" CssClass="img_slide" runat="server" />
                        </a>
                        <div class="discount">
                            <asp:Label ID="LblDiscount5" CssClass="LblDiscount" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <div class="carousel-item">
                        <a id="A6" runat="server">
                            <asp:Image ID="ImgReduc6" CssClass="img_slide" runat="server" />
                        </a>
                        <div class="discount">
                            <asp:Label ID="LblDiscount6" CssClass="LblDiscount" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <div class="carousel-item">
                        <a id="A7" runat="server">
                            <asp:Image ID="ImgReduc7" CssClass="img_slide" runat="server" />
                        </a>
                        <div class="discount">
                            <asp:Label ID="LblDiscount7" CssClass="LblDiscount" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <div class="carousel-item">
                        <a id="A8" runat="server">
                            <asp:Image ID="ImgReduc8" CssClass="img_slide" runat="server" />
                        </a>
                        <div class="discount">
                            <asp:Label ID="LblDiscount8" CssClass="LblDiscount" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <div class="carousel-item">
                        <a id="A9" runat="server">
                            <asp:Image ID="ImgReduc9" CssClass="img_slide" runat="server" />
                        </a>
                        <div class="discount">
                            <asp:Label ID="LblDiscount9" CssClass="LblDiscount" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                </div>
                <button class="carousel-control-prev" type="button" data-bs-target="#carouselReduction" data-bs-slide="prev">
                    <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                    <span class="visually-hidden">Précédent</span>
                </button>
                <button class="carousel-control-next" type="button" data-bs-target="#carouselReduction" data-bs-slide="next">
                    <span class="carousel-control-next-icon" aria-hidden="true"></span>
                    <span class="visually-hidden">Suivant</span>
                </button>
            </div>
        </div>
        <br />
        <br />
    </main>
</asp:Content>
