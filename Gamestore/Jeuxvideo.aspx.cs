using Gamestore.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace Gamestore
{
    public partial class About : Page
    {
        DALGamestore objDal = new DALGamestore();
        protected void Page_Load(object sender, EventArgs e)
        {
            List<String> listImageJV = new List<String>();
            List<String> listTitleJV = new List<String>();
            List<String> listGenreJV = new List<String>();
            List<float> listPriceJV = new List<float>();
            List<int> listPegiJV = new List<int>();

            int i = 0;
            int stocksGame = 0;
            int listDiscountJV = 0;
            float priceDiscountJV = 0;


            //Récupération de toutes les informations de chaque jeux vidéo présent dans la BDD
            listImageJV = objDal.RecupImageJeuxVideo();
            listTitleJV = objDal.RecupTitreJeuxVideo();
            listPriceJV = objDal.RecupPriceJeuxVideo();
            listPegiJV = objDal.RecupPegiJeuxVideo();
            listGenreJV = objDal.RecupGenreJeuxVideo();

            for (int j = 0; j < listGenreJV.Count; j++)
            {
                //Récupération des entrées de la list string
                String genre = listGenreJV[j].ToString();

                //Split des entrées de la list string avec ',' comme séparateur
                String[] Split = genre.Split(',');

                //Ajout des entrées au DropDownList
                foreach (var s in Split)
                {
                    var item = new ListItem(s);
                    bool existeDeja = CheckBoxGenre.Items.Contains(item);
                    if (existeDeja == false)
                    {
                        CheckBoxGenre.Items.Add(s);
                        
                    }
                }
            }
            
            //Initialisation + affichage dnynamique des jeux videos
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            StringBuilder sb3 = new StringBuilder();
            StringBuilder sb4 = new StringBuilder();
            StringBuilder sb5 = new StringBuilder();
            StringBuilder sb6 = new StringBuilder();
            StringBuilder sb7 = new StringBuilder();
            StringBuilder sbgDiscount = new StringBuilder();
            StringBuilder sbgPEGI = new StringBuilder();
            StringBuilder sbgImgPegi = new StringBuilder();

            sb1.Append(@"<div class='grid'>");
            sb2.Append(@"<div class = 'info_game'>");
            sbgDiscount.Append(@"<div class= 'discount'>");
            sbgPEGI.Append(@"<div class='PEGI'>");
            
            sb3.Append(@"<br/>");
            sb4.Append(@"</div>");
            sb5.Append(@"</div>");
            sb6.Append(@"</div>");
            


            //Affichage dynamique de tout les jeux vidéos présent dans BDD
            PnlGame.Controls.Add(new LiteralControl(sb1.ToString()));

            foreach (var url in listImageJV)
            {
                if (url != "")
                {
                    ImageButton ImgBtn = new ImageButton();
                    Label LblTitle = new Label();
                    Label LblPrice = new Label();
                    Label LblDiscount = new Label();
                    Label LblPriceDiscount = new Label();
                    Label LblPegi = new Label();
                    Label LblNoStocks = new Label();
                    Button BtnAddToCart = new Button();

                    //Boolean pour savoir si il y a une réduction ou non sur le jeu en cours
                    bool discountExist = false;
                    bool priceDiscountExist = false;

                    //Vérification du stock du jeu
                    stocksGame = objDal.VerifStocksGame(listTitleJV[i]);

                    //Récupération d'une potentiel remise existante
                    listDiscountJV = objDal.RecupDiscountJeuxVideo(listTitleJV[i]);

                    //Récupération d'un prix réduit si existant
                    priceDiscountJV = objDal.RecupPriceDiscountJeuxVideo(listTitleJV[i]);

                    LblTitle.ID = "LblTitleGame" + i.ToString();
                    LblTitle.Text = listTitleJV[i];
                    LblTitle.Visible = true;
                    LblTitle.CssClass = "title_game";

                    ImgBtn.ID = "ImgBtn" + i.ToString();
                    ImgBtn.ImageUrl = url;
                    ImgBtn.CssClass = "img_game";
                    ImgBtn.Click += (source, args) =>
                    {
                        Response.Redirect("~/DetailJeuxVideo?" + LblTitle.Text.Replace(" ", "_"));
                    };

                    if (priceDiscountJV <= 0)
                    {
                        LblPrice.ID = "LblPriceGame" + i.ToString();
                        LblPrice.Text = listPriceJV[i] + "€";
                        LblPrice.Visible = true;
                        LblPrice.CssClass = "price_game";
                    }

                    if (listDiscountJV > 0)
                    {
                        LblDiscount.ID = "LblDiscount" + i.ToString();
                        LblDiscount.Text = "-" + listDiscountJV + "%";
                        LblDiscount.Visible = true;
                        LblDiscount.CssClass = "discount_game";

                        discountExist = true;
                    }

                    if (priceDiscountJV > 0)
                    {
                        LblPriceDiscount.ID = "LblPriceDiscount" + i.ToString();
                        LblPriceDiscount.Text = priceDiscountJV + "€";
                        LblPriceDiscount.Visible = true;
                        LblPriceDiscount.CssClass = "price_game";

                        priceDiscountExist = true;
                    }
                    

                    LblPegi.ID = "LblPegi" + i.ToString();
                    LblPegi.Text = "pegi_" + " " + listPegiJV[i];
                    LblPegi.Visible = false;
                    LblPegi.CssClass = "pegi_game";

                    if (stocksGame <= 0)
                    {
                        LblNoStocks.ID = "LblNoStocks" + i.ToString();
                        LblNoStocks.Text = "En rupture de stock";
                        LblNoStocks.Visible = false;
                        LblNoStocks.CssClass = "no_stock_game";
                    }

                    

                    String emailClient = null;
                    if (Convert.ToBoolean(Session["EstConnecte"]) == true)
                    {
                        //Variable pour pouvoir vérifier si le jeu est déjà dans le panier ou non
                        emailClient = Session["MailUtilisateur"].ToString();
                    }

                    String titreGame = LblTitle.Text;
                    int idUsers = objDal.RecupClientID(emailClient);

                    //Vérification si déjà dans le panier
                    String verifDoublon = objDal.VerifDoublonInCart(titreGame, idUsers);

                    if (Convert.ToBoolean(Session["EstConnecte"]) == true && Session["RoleUtilisateur"].ToString() == "Utilisateur")
                    {
                        if (verifDoublon != titreGame)
                        {
                            BtnAddToCart.ID = "BtnAddToCart" + i.ToString();
                            BtnAddToCart.Text = "Ajouter au panier";
                            BtnAddToCart.Visible = true;
                            BtnAddToCart.CssClass = "BtnAddToCart";
                            BtnAddToCart.Click += (source, args) =>
                            {
                                //Variable pour Récupération de l'email de l'utilisateur pour avoir son ID
                                String emailUsers = Session["MailUtilisateur"].ToString();

                                //Variable pour Récupération du titre du jeux auquels le bouton est attaché
                                String titleGame = LblTitle.Text;

                                //Variable pour Récupération de l'id du jeu auquels le bouton est attaché
                                int recupGameId = -1;

                                //Récupération de l'ID du client grâce à son adresse mail
                                int idClient = objDal.RecupClientID(emailUsers);

                                //Récupération de l'ID du jeux auquels le bouton est attaché
                                recupGameId = objDal.RecupGameID(titleGame);

                                //Ajout du jeu au panier
                                bool estInscrit = objDal.AddToCart(titleGame, idClient, recupGameId);

                                if (estInscrit == true)
                                {
                                    Alert.Show("Jeu bien ajouté au panier.");
                                    BtnAddToCart.Visible = false;

                                    Image Img_Cart = Master.FindControl("ImgCart") as Image;
                                    if (Img_Cart != null)
                                    {
                                        Img_Cart.Visible = true;
                                    }

                                }
                                else
                                {
                                    Alert.Show("Impossible d'ajouter ce jeu au panier.");
                                }
                            };
                        }
                    }

                    sb7.Clear();
                    if (priceDiscountJV > 0)
                    {
                        sb7.Append(@"<div id = 'info" + " " + listGenreJV[i] + "' class = 'genreGame' data-price='" + priceDiscountJV + "'>");
                    }
                    else
                    {
                        sb7.Append(@"<div id = 'info" + " " + listGenreJV[i] + "' class = 'genreGame' data-price='" + listPriceJV[i] + "'>");
                    }

                    

                    String pegiImg = objDal.RecupPegi(LblPegi.Text);

                    sbgImgPegi.Clear();
                    sbgImgPegi.Append("<img class='img_pegi' src='" + pegiImg + "'/>");


                    PnlGame.Controls.Add(new LiteralControl(sb7.ToString()));
                    PnlGame.Controls.Add(ImgBtn);
                    PnlGame.Controls.Add(new LiteralControl(sbgPEGI.ToString()));
                    PnlGame.Controls.Add(new LiteralControl(sbgImgPegi.ToString()));
                    PnlGame.Controls.Add(new LiteralControl(sb4.ToString()));
                    if (discountExist == true)
                    {
                        PnlGame.Controls.Add(new LiteralControl(sbgDiscount.ToString()));
                        PnlGame.Controls.Add(LblDiscount);
                        PnlGame.Controls.Add(new LiteralControl(sb4.ToString()));
                    }
                    PnlGame.Controls.Add(new LiteralControl(sb2.ToString()));
                    PnlGame.Controls.Add(LblTitle);

                    if (priceDiscountExist == false)
                    {
                        PnlGame.Controls.Add(LblPrice);
                    }
                    else
                    {
                        PnlGame.Controls.Add(LblPriceDiscount);
                    }
                    PnlGame.Controls.Add(new LiteralControl(sb3.ToString()));
                    PnlGame.Controls.Add(LblPegi);
                    if (Convert.ToBoolean(Session["EstConnecte"]) == true && verifDoublon != titreGame && Session["RoleUtilisateur"].ToString() == "Utilisateur")
                    {
                        PnlGame.Controls.Add(BtnAddToCart);
                    }

                    if (stocksGame <= 0)
                    {
                        PnlGame.Controls.Add(LblNoStocks);
                        LblNoStocks.Visible = true;
                        BtnAddToCart.Visible = false;
                    }
                    PnlGame.Controls.Add(new LiteralControl(sb4.ToString()));
                    PnlGame.Controls.Add(new LiteralControl(sb5.ToString()));
                    i++;
                }

            }

            PnlGame.Controls.Add(new LiteralControl(sb6.ToString()));

        }
    }
}