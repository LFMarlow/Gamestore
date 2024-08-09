using Gamestore.Classes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Gamestore
{
    public partial class MonCompte : System.Web.UI.Page
    {
        //Initialisation de l'objet de Class DAL pour requête SQL
        DALGamestore objDal = new DALGamestore();

        DALMongo objMongo = new DALMongo();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Convert.ToBoolean(Session["EstConnecte"]) && Session["RoleUtilisateur"].ToString() == "Administrateur")
            {
                LblUsers.Text = Session["Prenom"].ToString() + " " + Session["Nom"].ToString();
                if (!IsPostBack)
                {
                    Label4.Visible = false;
                    Label5.Visible = false;
                    LblQteStock.Visible = false;

                    UPCreateVideoGames.Visible = false;
                    UPCreateEmploye.Visible = false;
                    UPGestionStocks.Visible = false;
                    PnlAddPromotions.Visible = false;
                    PnlSupprPromotions.Visible = false;
                    PnlReadSale.Visible = false;
                    PnlSaleByGenre.Visible = false;
                    PnlSaleByItem.Visible = false;
                    BtnDetailsChart.Visible = false;

                }
            }
            else
            {
                Response.Redirect("~/Connexion");
            }
        }

        protected void BtnCVG_Click(object sender, EventArgs e)
        {

            UPCreateVideoGames.Visible = true;
            UPCreateEmploye.Visible = false;
            UPGestionStocks.Visible = false;
            PnlAddPromotions.Visible = false;
            PnlSupprPromotions.Visible = false;
            PnlReadSale.Visible = false;
            PnlSaleByGenre.Visible = false;
            PnlSaleByItem.Visible = false;
            BtnDetailsChart.Visible = false;
        }

        protected void BtnGestionStocks_Click(object sender, EventArgs e)
        {
            UPCreateVideoGames.Visible = false;
            UPCreateEmploye.Visible = false;
            UPGestionStocks.Visible = true;
            PnlAddPromotions.Visible = false;
            PnlReadSale.Visible = false;
            PnlSaleByGenre.Visible = false;
            PnlSaleByItem.Visible = false;
            PnlSupprPromotions.Visible = false;
            contenu_game_gest_stock.Visible = false;
            IMGGame.Visible = false;
            BtnDetailsChart.Visible = false;

            List<String> listTitle = new List<string>();
            listTitle = objDal.RécupTitleJeuxVideo();

            if (listTitle != null)
            {
                if (DDLVideoGames.Items.Count == 0)
                {
                    DDLVideoGames.DataSource = listTitle;
                    DDLVideoGames.DataBind();
                    DDLVideoGames.Items.Insert(0, new ListItem("-- Sélectionner un jeu --", ""));
                }
            }

            DDLVideoGames.SelectedIndex = 0;
        }

        protected void BtnDashboard_Click(object sender, EventArgs e)
        {
            UPCreateVideoGames.Visible = false;
            UPCreateEmploye.Visible = false;
            UPGestionStocks.Visible = false;
            PnlAddPromotions.Visible = false;
            PnlReadSale.Visible = true;
            BtnDetailsChart.Visible = true;
            BtnDetailsChart.Visible = false;
            PnlSupprPromotions.Visible = false;
            PnlSaleByGenre.Visible = false;
            PnlSaleByItem.Visible = false;
            BtnDetailsChart.Visible = false;

            var salesData = GetSalesDataByGenre();
            string jsonSalesData = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(salesData);
            ScriptManager.RegisterStartupScript(this, GetType(), "initializeChart", $"initializeChart({jsonSalesData});", true);
        }

        protected void BtnCreateEmploye_Click(object sender, EventArgs e)
        {
            UPCreateVideoGames.Visible = false;
            UPCreateEmploye.Visible = true;
            UPGestionStocks.Visible = false;
            PnlAddPromotions.Visible = false;
            PnlSupprPromotions.Visible = false;
            PnlReadSale.Visible = false;
            PnlSaleByGenre.Visible = false;
            PnlSaleByItem.Visible = false;
            BtnDetailsChart.Visible = false;
        }

        protected void BtnCreateCompteEmploye_Click(object sender, EventArgs e)
        {
            String nom = TxtBoxNomEmploye.Text;
            String prenom = TxtBoxPrenomEmploye.Text;
            String mail = TxtBoxMailEmploye.Text;
            String password = TxtBoxPasswordEmploye.Text;
            String passwordHash = "";
            String role = "Employé";
            String tokenEmploye = TokenUsers.GetRandom(10);
            bool estInscrit;

            passwordHash = SecurePassword.Hash(password);

            if (TxtBoxNomEmploye.Text != "" && TxtBoxPrenomEmploye.Text != "" && TxtBoxMailEmploye.Text != "" && TxtBoxPasswordEmploye.Text != "")
            {
                estInscrit = objDal.InscriptionEmploye(nom, prenom, mail, passwordHash, role, tokenEmploye);

                if (estInscrit == true)
                {
                    Alert.Show("Employé inscrit avec succès !");
                }
                else
                {
                    Alert.Show("Une erreur est survenue.");
                }
            }
            else
            {
                Alert.Show("Tous les champs doivent être remplis !");
            }
        }

        protected void BtnCreateGame_Click(object sender, EventArgs e)
        {
            String titleGame = TxtBoxTitreGame.Text;
            String URLImage = TxtBoxURLImage.Text;
            String priceGame = TxtBoxPriceGame.Text;
            String pegiGame = TxtBoxPEGIGame.Text;
            String quantityGame = TxtBoxQuantityGame.Text;
            String genreGame = TxtBoxGenreGame.Text;
            String descriptionGame = TxtBoxDescription.Text;
            String discount = TxtBoxPromotion.Text;
            String priceAfterDiscount = LblCalculPrice.Text;
            String verifFirstChar = "";
            if (priceAfterDiscount != "" || discount != "")
            {
                verifFirstChar = priceAfterDiscount.Substring(0, 1);
            }
            int num = -1;

            if (verifFirstChar != "")
            {
                if (int.TryParse(verifFirstChar, out num))
                {
                    int lastIdGame;

                    bool inscritOK = false;

                    lastIdGame = objDal.RecupIdGame();
                    lastIdGame = lastIdGame + 1;


                    if (TxtBoxTitreGame.Text != "" && TxtBoxURLImage.Text != "" && TxtBoxPriceGame.Text != "" && TxtBoxPEGIGame.Text != "" && TxtBoxQuantityGame.Text != "" && TxtBoxGenreGame.Text != "" && TxtBoxDescription.Text != "")
                    {

                        inscritOK = objDal.CreateGameWithPromotion(URLImage, titleGame.Replace(":", " "), priceGame, pegiGame, quantityGame, genreGame, descriptionGame, lastIdGame, discount, priceAfterDiscount);

                    }
                    else
                    {
                        Alert.Show("Tous les champs doivent être rempli.");
                    }

                    if (inscritOK == true)
                    {
                        Alert.Show("Ajout du jeu réussi !");
                        TxtBoxTitreGame.Text = "";
                        TxtBoxURLImage.Text = "";
                        TxtBoxPriceGame.Text = "";
                        TxtBoxPEGIGame.Text = "";
                        TxtBoxQuantityGame.Text = "";
                        TxtBoxGenreGame.Text = "";
                        TxtBoxDescription.Text = "";
                    }
                    else
                    {
                        Alert.Show("Ajout du jeu impossible. Réessayer ultérieurement.");
                    }
                }
            }
            else
            {
                int lastIdGame;

                bool inscritOK = false;

                lastIdGame = objDal.RecupIdGame();
                lastIdGame = lastIdGame + 1;


                if (TxtBoxTitreGame.Text != "" && TxtBoxURLImage.Text != "" && TxtBoxPriceGame.Text != "" && TxtBoxPEGIGame.Text != "" && TxtBoxQuantityGame.Text != "" && TxtBoxGenreGame.Text != "" && TxtBoxDescription.Text != "")
                {
                    if (TxtBoxPromotion.Text == "" || LblCalculPrice.Text == "")
                    {
                        inscritOK = objDal.CreateGameWithoutPromotion(URLImage, titleGame.Replace(":", " "), priceGame, pegiGame, quantityGame, genreGame, descriptionGame, lastIdGame);
                    }
                }

                if (inscritOK == true)
                {
                    Alert.Show("Ajout du jeu réussi !");
                    TxtBoxTitreGame.Text = "";
                    TxtBoxURLImage.Text = "";
                    TxtBoxPriceGame.Text = "";
                    TxtBoxPEGIGame.Text = "";
                    TxtBoxQuantityGame.Text = "";
                    TxtBoxGenreGame.Text = "";
                    TxtBoxDescription.Text = "";
                    LblCalculPrice.Text = "";
                    TxtBoxPromotion.Text = "";
                }
                else
                {
                    Alert.Show("Ajout du jeu impossible. Réessayer ultérieurement.");
                }
            }
        }

        protected void DDLVideoGames_TextChanged(object sender, EventArgs e)
        {
            //Initialisation de l'objet de Class DAL pour requête SQL
            Classes.JeuxVideo objJeuxVideo = new Classes.JeuxVideo();

            objJeuxVideo = objDal.RécupJeuxVideo(DDLVideoGames.Text);

            IMGGame.ImageUrl = objJeuxVideo.urlImage;
            LblTitleGame.Text = objJeuxVideo.title;
            LblQteStock.Text = objJeuxVideo.quantite.ToString();

            Label4.Visible = true;
            Label5.Visible = true;
            LblQteStock.Visible = true;
            contenu_game_gest_stock.Visible = true;
            IMGGame.Visible = true;
            TxtBoxNewQte.Text = "";
            
            if (DDLVideoGames.Text == "")
            {
                Label4.Visible = false;
                Label5.Visible = false;
                LblQteStock.Visible = false;
            }
        }

        protected void BtnChangeQte_Click(object sender, EventArgs e)
        {

            String titleGame = LblTitleGame.Text;
            int nouvelleQuantite = Convert.ToInt32(TxtBoxNewQte.Text);
            if (nouvelleQuantite < 0)
            {
                Alert.Show("La quantité ne peut être un nombre Négatif.");
            }
            else
            {
                bool quantityStock = objDal.UpdateQuantiteStock(Convert.ToInt32(nouvelleQuantite), titleGame);
                if (quantityStock == true)
                {
                    TxtBoxNewQte.Text = "";
                    LblQteStock.Text = objDal.RécupQuantiteJeuxVideo(titleGame).ToString();
                }
            }

            if(TxtBoxNewQte.Text == "")
            {
                Alert.Show("Quantité mis à jour avec succès.");
            }

            
        }

        protected void BtnDisconnect_Click(object sender, EventArgs e)
        {
            Session["EstConnecte"] = false;
            Session["LoginUtilisateur"] = "";
            Session["PassUtilisateur"] = "";
            Session["MailUtilisateur"] = "";
            Session["Nom"] = "";
            Session["Prenom"] = "";
            Session["RoleUtilisateur"] = "";
            Response.Redirect(Request.RawUrl);
        }

        protected void BtnAddPromotions_Click(object sender, EventArgs e)
        {
            Label7.Visible = false;
            Label9.Visible = false;
            PnlAddPromotions.Visible = true;
            PnlSupprPromotions.Visible = false;
            UPCreateVideoGames.Visible = false;
            UPCreateEmploye.Visible = false;
            UPGestionStocks.Visible = false;
            PnlReadSale.Visible = false;
            PnlSaleByGenre.Visible = false;
            PnlSaleByItem.Visible = false;
            BtnDetailsChart.Visible = false;
            contenue_game.Visible = false;
            ImgGamePromo.Visible = false;

            List<String> listTitle = new List<string>();
            listTitle = objDal.RécupTitleJeuxVideoWithoutPromotions();

            if (listTitle != null)
            {
                DdlGameForPromotions.ClearSelection();
                DdlGameForPromotions.DataSource = listTitle;
                DdlGameForPromotions.DataBind();
                DdlGameForPromotions.Items.Insert(0, new ListItem("-- Sélectionner un jeu --", ""));

            }

            DdlGameForPromotions.SelectedIndex = 0;
        }

        protected void BtnSupprPromotions_Click(object sender, EventArgs e)
        {
            Label7.Visible = false;
            Label9.Visible = false;
            Label13.Visible = false;
            PnlAddPromotions.Visible = false;
            PnlSupprPromotions.Visible = true;
            UPCreateVideoGames.Visible = false;
            UPCreateEmploye.Visible = false;
            UPGestionStocks.Visible = false;
            PnlReadSale.Visible = false;
            PnlSaleByGenre.Visible = false;
            PnlSaleByItem.Visible = false;
            BtnDetailsChart.Visible = false;
            contenu_game.Visible = false;
            ImgSupprPromo.Visible = false;

            List<String> listTitle = new List<string>();
            listTitle = objDal.RécupTitleJeuxVideoWithPromotions();

            if (listTitle != null)
            {
                if (DdlSupprPromotions.Items.Count == 0)
                {
                    DdlSupprPromotions.DataSource = listTitle;
                    DdlSupprPromotions.DataBind();
                    DdlSupprPromotions.Items.Insert(0, new ListItem("-- Sélectionner un jeu --", ""));
                }
            }

            DdlSupprPromotions.SelectedIndex = 0;
        }

        protected void DdlGameForPromotions_TextChanged(object sender, EventArgs e)
        {
            //Initialisation de l'objet de Class DAL pour requête SQL
            Classes.JeuxVideo objJeuxVideo = new Classes.JeuxVideo();
            float discountPrice;

            objJeuxVideo = objDal.RécupJeuxVideo(DdlGameForPromotions.Text);
            discountPrice = objDal.RecupPriceDiscountJeuxVideo(DdlGameForPromotions.Text);

            ImgGamePromo.ImageUrl = objJeuxVideo.urlImage;
            LblNameGame.Text = objJeuxVideo.title;

            if (discountPrice <= 0)
            {
                LblActualPrice.Text = objJeuxVideo.prix.ToString() + "€";
            }
            else
            {
                LblActualPrice.Text = discountPrice.ToString() + "€";
            }

            Label7.Visible = true;
            Label9.Visible = true;
            LblActualPrice.Visible = true;
            contenue_game.Visible = true;
            ImgGamePromo.Visible = true;
            TxtBoxRemise.Text = "";
            TxtBoxRemise.Visible = true;
            Label11.Visible = true;
            BtnCalculPromotions.Visible = true;

            if (DdlGameForPromotions.SelectedIndex == 0)
            {
                Label7.Visible = false;
                Label9.Visible = false;
                LblActualPrice.Visible = false;
                contenue_game.Visible = false;
            }
        }

        protected void DdlSupprPromotions_TextChanged(object sender, EventArgs e)
        {
            //Initialisation de l'objet de Class DAL pour requête SQL
            Classes.JeuxVideo objJeuxVideo = new Classes.JeuxVideo();
            float discountPrice;

            objJeuxVideo = objDal.RécupJeuxVideo(DdlSupprPromotions.Text);
            discountPrice = objDal.RecupPriceDiscountJeuxVideo(DdlSupprPromotions.Text);

            ImgSupprPromo.ImageUrl = objJeuxVideo.urlImage;
            LblNomGame.Text = objJeuxVideo.title;

            if (discountPrice <= 0)
            {
                LblPriceGame.Text = objJeuxVideo.prix.ToString() + "€";
            }
            else
            {
                LblPriceGame.Text = discountPrice.ToString() + "€";
            }

            Label12.Visible = true;
            Label13.Visible = true;
            LblPriceGame.Visible = true;
            contenu_game.Visible = true;
            ImgSupprPromo.Visible = true;
            contenu3_game.Visible = true;

            if (DdlSupprPromotions.SelectedIndex == 0)
            {
                Label13.Visible = false;
                Label15.Visible = false;
                LblPriceGame.Visible = false;
                contenu_game.Visible = false;
            }
        }

        protected void BtnCalculPromotions_Click(object sender, EventArgs e)
        {
            //On split le contenu du label pour ne pas récupéré le signe "€"
            String[] splitPrice = LblActualPrice.Text.Split('€');

            //On récupére le prix actuel du jeu sans le signe "€" contenu dans l'index 0
            String actualPrice = splitPrice[0];

            String maxPromo = "100";

            if (TxtBoxRemise.Text != "")
            {
                //On récupére la valeur du TextBox pour calculer la remise
                String discount = TxtBoxRemise.Text;

                String verifFirstChar = discount.Substring(0, 1);
                int num = -1;

                if (int.TryParse(verifFirstChar, out num))
                {
                    //On convertit en float le prix actuel et la remise
                    float startingPrice = Convert.ToSingle(actualPrice);
                    float calculDiscount;
                    float realDiscount = Convert.ToSingle(discount);

                    double priceAfterDiscount;

                    string lastPrice;

                    bool estAjouté = false;

                    calculDiscount = startingPrice * realDiscount / 100;
                    priceAfterDiscount = startingPrice - calculDiscount;

                    lastPrice = priceAfterDiscount.ToString();

                    if (lastPrice.Contains(","))
                    {
                        lastPrice = lastPrice.Substring(0, 5);
                        estAjouté = objDal.UpdateDiscountVideoGame(DdlGameForPromotions.Text, realDiscount, lastPrice);
                    }
                    else
                    {
                        estAjouté = objDal.UpdateDiscountVideoGame(DdlGameForPromotions.Text, realDiscount, lastPrice);
                    }

                    if (estAjouté == true)
                    {
                        LblActualPrice.Text = lastPrice.ToString() + "€";
                        List<String> listTitle = new List<string>();
                        listTitle = objDal.RécupTitleJeuxVideoWithoutPromotions();

                        if (listTitle != null)
                        {
                            DdlGameForPromotions.DataSource = listTitle;
                            DdlGameForPromotions.DataBind();
                            DdlGameForPromotions.Items.Insert(0, new ListItem("-- Sélectionner un jeu --", ""));
                        }

                        Label11.Visible = false;
                        TxtBoxRemise.Visible = false;
                        BtnCalculPromotions.Visible = false;
                        Alert.Show("Réduction appliqué");
                        


                    }
                }
                else
                {
                    Alert.Show("Merci de ne pas entrer de signe particulier.");
                }
            }
            else
            {
                Alert.Show("La remise ne peut être nul");
            }
        }

        protected void BtnDeletePromotions_Click(object sender, EventArgs e)
        {
            bool estSuppr = false;

            estSuppr = objDal.SupprDiscountVideoGame(DdlSupprPromotions.Text);

            if (estSuppr == true)
            {
                List<String> listTitle = new List<string>();
                listTitle = objDal.RécupTitleJeuxVideoWithPromotions();

                if (listTitle != null)
                {

                    DdlSupprPromotions.DataSource = listTitle;
                    DdlSupprPromotions.DataBind();
                    DdlSupprPromotions.Items.Insert(0, new ListItem("-- Sélectionner un jeu --", ""));

                }
                ImgSupprPromo.Visible = false;
                contenu_game.Visible = false;
                contenu3_game.Visible = false;
                Alert.Show("Promotions supprimé avec succès.");
            }
        }

        protected void BtnCalculDiscount_Click(object sender, EventArgs e)
        {
            if (TxtBoxPriceGame.Text != "")
            {
                if (TxtBoxPromotion.Text != "")
                {
                    float actualPrice = Convert.ToSingle(TxtBoxPriceGame.Text.Replace(".", ","));
                    String percentPromotion = TxtBoxPromotion.Text;
                    String verifFirstCHAR = percentPromotion.Substring(0, 1);
                    int num = -1;

                    if (int.TryParse(verifFirstCHAR, out num))
                    {
                        float actualPriceFloat = Convert.ToSingle(actualPrice);
                        float calculPromotion;
                        float realPromotion = Convert.ToSingle(percentPromotion);

                        double priceAfterPromotions;

                        String lastPromotedPrice;

                        calculPromotion = actualPriceFloat * realPromotion / 100;
                        priceAfterPromotions = actualPriceFloat - calculPromotion;

                        lastPromotedPrice = priceAfterPromotions.ToString();

                        string verifNegativPrice = lastPromotedPrice.Substring(0, 1);

                        if (int.TryParse(verifNegativPrice, out num))
                        {
                            if (lastPromotedPrice.Contains(","))
                            {
                                int test = 0;
                                test = lastPromotedPrice.Length;
                                if (test > 5 || test == 4)
                                {
                                    if (test == 4)
                                    {
                                        lastPromotedPrice = lastPromotedPrice + "0";
                                        lastPromotedPrice = lastPromotedPrice.Substring(0, 5);
                                        LblCalculPrice.Text = lastPromotedPrice.ToString();
                                        Label8.Visible = true;
                                        Label10.Visible = true;
                                        LblCalculPrice.Visible = true;
                                    }
                                    else
                                    {
                                        lastPromotedPrice = lastPromotedPrice.Substring(0, 5);
                                        LblCalculPrice.Text = lastPromotedPrice.ToString();
                                        Label8.Visible = true;
                                        Label10.Visible = true;
                                        LblCalculPrice.Visible = true;
                                    }
                                }
                                else if (test == 2)
                                {
                                    lastPromotedPrice = lastPromotedPrice.Substring(0, 2);
                                    LblCalculPrice.Text = lastPromotedPrice.ToString();
                                    Label8.Visible = true;
                                    Label10.Visible = true;
                                    LblCalculPrice.Visible = true;
                                }
                                else if (test == 1)
                                {
                                    lastPromotedPrice = lastPromotedPrice.Substring(0, 1);
                                    LblCalculPrice.Text = lastPromotedPrice.ToString();
                                    Label8.Visible = true;
                                    Label10.Visible = true;
                                    LblCalculPrice.Visible = true;
                                }
                            }
                            else
                            {
                                LblCalculPrice.Text = lastPromotedPrice.ToString();
                                Label8.Visible = true;
                                Label10.Visible = true;
                                LblCalculPrice.Visible = true;
                            }
                        }
                        else
                        {
                            LblCalculPrice.Text = "La remise ne peut être négative.";
                            LblCalculPrice.Visible = true;
                        }
                    }
                    else
                    {
                        Alert.Show("Merci de ne pas entrer de signe particulier dans le champs 'Promotions'");
                    }
                }
            }
            else
            {
                Alert.Show("Vous devez entrer un prix de base avant de calculer la promotion qui peut-être appliqué.");
            }
        }

        public Dictionary<string, Dictionary<string, int>> GetSalesDataByGenre()
        {
            Dictionary<string, Dictionary<string, int>> salesData = new Dictionary<string, Dictionary<string, int>>();

            salesData = objDal.GetSalesByGenre();
            return salesData;
        }

        protected void BtnDetailsChart_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/DetailsChart");
        }

        protected void BtnChartByItem_Click(object sender, EventArgs e)
        {
            PnlSaleByGenre.Visible = false;
            PnlSaleByItem.Visible = true;
            BtnDetailsChart.Visible = true;

            var salesData = GetSalesDataByItem();
            string jsonSalesData = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(salesData);
            ScriptManager.RegisterStartupScript(this, GetType(), "initializeChartItem", $"initializeChartItem({jsonSalesData});", true);
        }

        public Dictionary<string, Dictionary<string, int>> GetSalesDataByItem()
        {
            Dictionary<string, Dictionary<string, int>> salesData = new Dictionary<string, Dictionary<string, int>>();

            salesData = objMongo.GetSalesDataByItem();
            return salesData;
        }

        protected void BtnChartByGenre_Click(object sender, EventArgs e)
        {
            PnlSaleByGenre.Visible = true;
            PnlSaleByItem.Visible = false;
            BtnDetailsChart.Visible = true;

            var salesData = GetSalesDataByGenre();
            string jsonSalesData = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(salesData);
            ScriptManager.RegisterStartupScript(this, GetType(), "initializeChartGenre", $"initializeChartGenre({jsonSalesData});", true);
        }
    }
}