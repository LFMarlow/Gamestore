using Gamestore.Classes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Gamestore
{
    public partial class Panier : System.Web.UI.Page
    {
        DALGamestore objDal = new DALGamestore();
        
        GeolocationHandler geolocationHandler = new GeolocationHandler();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(Session["EstConnecte"]) == true)
            {
                if (!IsPostBack)
                {
                    //On récupére les informations des magasins pour les ajouter au DropDownList
                    DdlNameStore.DataSource = geolocationHandler.GetStores();
                    DdlNameStore.DataTextField = "StoreName";
                    DdlNameStore.DataValueField = "StoreID";
                    DdlNameStore.DataBind();

                    DdlNameStore.Items.Insert(0, new ListItem("Sélectionnez un magasin", ""));

                    List<Store> magasins = geolocationHandler.GetStores();
                    double userLat = Convert.ToDouble(Session["LatitudeUser"]);
                    double userLon = Convert.ToDouble(Session["LongitudeUser"]);

                    if (userLat != 0 && userLon != 0)
                    {
                        Store magasinProche = geolocationHandler.NearestStore(userLat, userLon, magasins);
                        if (magasinProche != null)
                        {
                            LblStoreNearUserReal.Text = magasinProche.StoreName;
                        }
                        LblStoreNearUser.Visible = true;
                    }
                    else
                    {
                        LblStoreNearUser.Visible = false;
                    }
                }

                //On récupére les infos pour afficher les éléments du panier
                String emailUsers = Session["MailUtilisateur"].ToString();

                //Variable pour concatener les prix du panier
                float result = 0;

                List<String> stringsInCart = new List<String>();
                List<float> priceInCart = new List<float>();
                
                //Récupération du client ID et de la liste des "ID" de jeux dans le panier
                int idClient = objDal.RecupClientID(emailUsers);
                
                //Récupération de tout ce que contient le panier
                stringsInCart = objDal.RécupCart(idClient);

                if (stringsInCart.Count != 0)
                {
                    //On cache le Label du panier vide
                    LblCartEmpty.Visible = false;

                    //Initialisation des StringBuilder pour crééer les Div dynamiquement
                    StringBuilder sb0 = new StringBuilder();
                    StringBuilder sb1 = new StringBuilder();
                    StringBuilder sb2 = new StringBuilder();
                    StringBuilder sb3 = new StringBuilder();
                    StringBuilder sb4 = new StringBuilder();
                    StringBuilder sb5 = new StringBuilder();

                    int i = 0;

                    sb0.Append(@"<div class='content_cart'>");
                    sb1.Append(@"<div class='contenu_game'>");
                    sb2.Append(@"<div class='contenu2_game'>");
                    sb3.Append(@"</div>");
                    sb4.Append(@"</div>");
                    sb5.Append(@"</div>");

                    //Affichage dynamique de tout les jeux vidéos présent dans le panier
                    for (int j = 0; j < stringsInCart.Count; j++)
                    {

                        System.Web.UI.WebControls.Image ImgJV = new System.Web.UI.WebControls.Image();
                        System.Web.UI.WebControls.Label LblTitleGame = new System.Web.UI.WebControls.Label();
                        System.Web.UI.WebControls.Label LblPrice = new System.Web.UI.WebControls.Label();
                        System.Web.UI.WebControls.Button BtnSupprGame = new System.Web.UI.WebControls.Button();

                        ImgJV.ID = "ImgGame" + i.ToString();
                        ImgJV.ImageUrl = stringsInCart[i];
                        ImgJV.CssClass = "img_game";
                        i++;
                        j++;

                        //Label pour afficher le titre du jeu
                        LblTitleGame.ID = "LblTitleGame" + i.ToString();
                        LblTitleGame.Text = stringsInCart[i];
                        LblTitleGame.Visible = true;

                        float priceDiscount = objDal.RecupPriceDiscountJeuxVideo(LblTitleGame.Text);

                        i++;
                        j++;

                        if (priceDiscount <= 0)
                        {
                            //Label pour afficher le prix du jeu
                            LblPrice.ID = "LblPrice" + i.ToString();
                            LblPrice.Text = stringsInCart[i] + "€";
                            LblPrice.Visible = true;
                            LblPrice.CssClass = "LblPrice";

                            String[] splitPrice = LblPrice.Text.Split('€');

                            priceInCart.Add(Convert.ToSingle(splitPrice[0]));
                        }
                        else
                        {
                            //Label pour afficher le prix du jeu
                            LblPrice.ID = "LblPrice" + i.ToString();
                            LblPrice.Text = priceDiscount + "€";
                            LblPrice.Visible = true;
                            LblPrice.CssClass = "LblPrice";
                            priceInCart.Add(Convert.ToSingle(priceDiscount));
                        }                    
                        i++;

                        //Bouton de suppression du jeu dans le panier auquel le bouton est lié
                        BtnSupprGame.ID = "BtnSupprGame" + i.ToString();
                        BtnSupprGame.Text = "Supprimer du panier";
                        BtnSupprGame.Visible = true;
                        BtnSupprGame.CssClass = "BtnSuppr btn btn-primary btn-lg";
                        BtnSupprGame.Click += (source, args) =>
                        {
                            //Récupération du titre du jeu
                            String titleGame = LblTitleGame.Text;

                            bool isDelete = false;

                            isDelete = objDal.DeleteInCart(titleGame, idClient);

                            if (isDelete == true)
                            {
                                //Appel de la fonction de mise à jour du panier après suppression d'un jeu
                                MaJCart();
                            }
                            else
                            {
                                Alert.Show("Impossible d'effacer ce jeu.");
                            }
                        };

                        //Affichage dans l'ordre "Div content_cart", Image du JV, Espace x2, "Div contenue_game", Titre du jeux, Espace, "Div contnue2_game", Prix du jeu, Bouton et fermeture des 3 Divs 
                        PnlCart.Controls.Add(new LiteralControl(sb0.ToString()));
                        PnlCart.Controls.Add(ImgJV);
                        PnlCart.Controls.Add(new LiteralControl("&nbsp;"));
                        PnlCart.Controls.Add(new LiteralControl("&nbsp;"));
                        PnlCart.Controls.Add(new LiteralControl(sb1.ToString()));
                        PnlCart.Controls.Add(LblTitleGame);
                        PnlCart.Controls.Add(new LiteralControl("&nbsp;"));
                        PnlCart.Controls.Add(new LiteralControl(sb2.ToString()));
                        PnlCart.Controls.Add(LblPrice);
                        PnlCart.Controls.Add(new LiteralControl("&nbsp;"));
                        PnlCart.Controls.Add(new LiteralControl("&nbsp;"));
                        PnlCart.Controls.Add(new LiteralControl("<br/>"));
                        PnlCart.Controls.Add(BtnSupprGame);
                        PnlCart.Controls.Add(new LiteralControl(sb3.ToString()));
                        PnlCart.Controls.Add(new LiteralControl(sb4.ToString()));
                        PnlCart.Controls.Add(new LiteralControl(sb5.ToString()));
                        PnlCart.Controls.Add(new LiteralControl("<br/>"));
                        PnlCart.Controls.Add(new LiteralControl("<br/>"));
                    }

                    //Récupération de tout les prix de tout les jeux dans le panier pour afficher le total
                    for (int k = 0; k < priceInCart.Count; k++)
                    {
                        result = result + Convert.ToSingle(priceInCart[k]);
                    }

                    //Affichage du prix total
                    LblTotalPrice.Text = result.ToString() + " " + "€";
                }
                else
                {
                    LblCartEmpty.Visible = true;
                    LblTotal.Visible = false;
                    LblTotalPrice.Visible = false;
                    BtnValiderCart.Visible = false;
                    CalendarCart.Visible = false;

                    System.Web.UI.WebControls.Image Img_Cart = Master.FindControl("ImgCart") as System.Web.UI.WebControls.Image;
                    if (Img_Cart != null)
                    {
                        Img_Cart.Visible = false;
                    }

                    Response.Redirect("~/Jeuxvideo");
                }
            }
            else
            {
                Response.Redirect("~/Connexion");
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false)]
        public static void ProcessData(double latitude, double longitude)
        {
            // Stocker les données dans la session
            HttpContext.Current.Session["LatitudeUser"] = latitude;
            HttpContext.Current.Session["LongitudeUser"] = longitude;
        }

        //Mise à jour des informations du Panier
        protected void MaJCart ()
        {
            PnlCart.Controls.Clear();

            //On récupére les infos pour afficher les éléments du panier
            String emailUsers = Session["MailUtilisateur"].ToString();

            //Variable pour concatener les prix du panier
            float result = 0;

            List<String> stringsInCart = new List<String>();
            List<float> priceInCart = new List<float>();

            //Récupération du client ID et de la liste des "ID" de jeux dans le panier
            int idClient = objDal.RecupClientID(emailUsers);

            //Récupération de tout ce que contient le panier
            stringsInCart = objDal.RécupCart(idClient);

            if (stringsInCart.Count != 0)
            {
                //On cache le Label du panier vide
                LblCartEmpty.Visible = false;

                //Récupération du prix de chaquee jeux dans le panier pour afficher le total
                priceInCart = objDal.RécupPriceInCart(idClient);

                //Initialisation des StringBuilder pour crééer les Div dynamiquement
                StringBuilder sb0 = new StringBuilder();
                StringBuilder sb1 = new StringBuilder();
                StringBuilder sb2 = new StringBuilder();
                StringBuilder sb3 = new StringBuilder();
                StringBuilder sb4 = new StringBuilder();
                StringBuilder sb5 = new StringBuilder();

                int i = 0;

                sb0.Append(@"<div class='content_cart'>");
                sb1.Append(@"<div class='contenu_game'>");
                sb2.Append(@"<div class='contenu2_game'>");
                sb3.Append(@"</div>");
                sb4.Append(@"</div>");
                sb5.Append(@"</div>");

                //Affichage dynamique de tout les jeux vidéos présent dans le panier
                for (int j = 0; j < stringsInCart.Count; j++)
                {

                    System.Web.UI.WebControls.Image ImgJV = new System.Web.UI.WebControls.Image();
                    System.Web.UI.WebControls.Label LblTitleGame = new System.Web.UI.WebControls.Label();
                    System.Web.UI.WebControls.Label LblPrice = new System.Web.UI.WebControls.Label();
                    System.Web.UI.WebControls.Button BtnSupprGame = new System.Web.UI.WebControls.Button();

                    ImgJV.ID = "ImgGame" + i.ToString();
                    ImgJV.ImageUrl = stringsInCart[i];
                    ImgJV.CssClass = "img_game";
                    i++;
                    j++;

                    //Label pour afficher le titre du jeu
                    LblTitleGame.ID = "LblTitleGame" + i.ToString();
                    LblTitleGame.Text = stringsInCart[i];
                    LblTitleGame.Visible = true;

                    float priceDiscount = objDal.RecupPriceDiscountJeuxVideo(LblTitleGame.Text);
                    i++;
                    j++;

                    if (priceDiscount <= 0)
                    {
                        //Label pour afficher le prix du jeu
                        LblPrice.ID = "LblPrice" + i.ToString();
                        LblPrice.Text = stringsInCart[i] + "€";
                        LblPrice.Visible = true;
                        LblPrice.CssClass = "LblPrice";

                        String[] splitPrice = LblPrice.Text.Split('€');

                        priceInCart.Add(Convert.ToSingle(splitPrice[0]));
                    }
                    else
                    {
                        //Label pour afficher le prix du jeu
                        LblPrice.ID = "LblPrice" + i.ToString();
                        LblPrice.Text = priceDiscount + "€";
                        LblPrice.Visible = true;
                        LblPrice.CssClass = "LblPrice";
                        priceInCart.Add(Convert.ToSingle(priceDiscount));
                    }
                    i++;

                    //Bouton de suppression du jeu dans le panier auquel le bouton est lié
                    BtnSupprGame.ID = "BtnSupprGame" + i.ToString();
                    BtnSupprGame.Text = "Supprimer du panier";
                    BtnSupprGame.Visible = true;
                    BtnSupprGame.CssClass = "BtnSuppr btn btn-primary btn-lg";
                    BtnSupprGame.Click += (source, args) =>
                    {
                        String titleGame = LblTitleGame.Text;
                        bool isDelete = false;

                        if (isDelete == true)
                        {
                            //Appel de la fonction de mise à jour du panier après suppression d'un jeu
                            MaJCart();
                        }
                        else
                        {
                            Alert.Show("Impossible d'effacer ce jeu.");
                        }
                    };

                    //Affichage dans l'ordre "Div content_cart", Image du JV, Espace x2, "Div contenue_game", Titre du jeux, Espace, "Div contnue2_game", Prix du jeu, Bouton et fermeture des 3 Divs 
                    PnlCart.Controls.Add(new LiteralControl(sb0.ToString()));
                    PnlCart.Controls.Add(ImgJV);
                    PnlCart.Controls.Add(new LiteralControl("&nbsp;"));
                    PnlCart.Controls.Add(new LiteralControl("&nbsp;"));
                    PnlCart.Controls.Add(new LiteralControl(sb1.ToString()));
                    PnlCart.Controls.Add(LblTitleGame);
                    PnlCart.Controls.Add(new LiteralControl("&nbsp;"));
                    PnlCart.Controls.Add(new LiteralControl(sb2.ToString()));
                    PnlCart.Controls.Add(LblPrice);
                    PnlCart.Controls.Add(new LiteralControl("&nbsp;"));
                    PnlCart.Controls.Add(BtnSupprGame);
                    PnlCart.Controls.Add(new LiteralControl(sb3.ToString()));
                    PnlCart.Controls.Add(new LiteralControl(sb4.ToString()));
                    PnlCart.Controls.Add(new LiteralControl(sb5.ToString()));
                    PnlCart.Controls.Add(new LiteralControl("<br/>"));
                    PnlCart.Controls.Add(new LiteralControl("<br/>"));
                }

                //Récupération de tout les prix de tout les jeux dans le panier pour afficher le total
                for (int k = 0; k < priceInCart.Count; k++)
                {
                    result = result + Convert.ToSingle(priceInCart[k]);
                }

                //Affichage du prix total
                LblTotalPrice.Text = result.ToString() + " " + "€";

                
            }
            else
            {
                LblCartEmpty.Visible = true;
                LblTotal.Visible = false;
                LblTotalPrice.Visible = false;
                BtnValiderCart.Visible = false;
                CalendarCart.Visible = false;

                System.Web.UI.WebControls.Image Img_Cart = Master.FindControl("ImgCart") as System.Web.UI.WebControls.Image;
                if (Img_Cart != null)
                {
                    Img_Cart.Visible = false;
                }
            }
        }

        //Validation du Panier
        protected void BtnValiderCart_Click(object sender, EventArgs e)
        {
            //On récupére les infos pour afficher les éléments du panier
            String emailUsers = Session["MailUtilisateur"].ToString();
            String etatCommand = "Validé";
            String genreGame = "";
            List<String> stringsInCart = new List<String>();
            

            //Récupération de la date choisi par l'utilisateur
            string dateTimeSelection = CalendarCart.SelectedDate.ToShortDateString();
            DateTime dateSelection = CalendarCart.SelectedDate;

            //Format pour Parse
            string inputFormat = "dd/MM/yyyy";
            string outputFormat = "yyyy/MM/dd";

            DateTime dateCommand = DateTime.ParseExact(dateTimeSelection, inputFormat, CultureInfo.InvariantCulture);
            string dateRetrait = dateCommand.ToString(outputFormat);

            //Initialisation d'une variable pour avoir la date du jour
            DateTime dateToday = DateTime.Today;

            //Initialisation d'une variable pour calculer les 7 jours de plus que la date du jour
            DateTime dateTodayPlusSeven = DateTime.Today;
            dateTodayPlusSeven = dateTodayPlusSeven.AddDays(7);

            DateTime datePlus = DateTime.ParseExact(dateTodayPlusSeven.ToShortDateString(), inputFormat, CultureInfo.InvariantCulture);
            string datePlusSeven = datePlus.ToString(outputFormat);

            int i = 0;
            int idClient = 0;
            bool commandInscrit = false;
            
            idClient = objDal.RecupClientID(emailUsers);
            stringsInCart = objDal.RécupCartForCommand(idClient);
            
            for (int j = 0; j < stringsInCart.Count; j++)
            {
                String titleGame = stringsInCart[i];
                i++;
                j++;

                genreGame = objDal.RecupGenreForCart(titleGame);

                String idClientCommand = stringsInCart[i];
                i++;
                j++;

                String idGame = stringsInCart[i];
                i++;

                if (stringsInCart.Count != 0 && dateSelection > dateToday)
                {
                    if (dateSelection < datePlus)
                    {
                        //On récupére le stock du jeu ciblé et on le décrémente d'un
                        int quantityStockGame = objDal.RécupQuantiteJeuxVideo(titleGame);
                        

                        if(quantityStockGame > 0)
                        {
                            if (DdlNameStore.SelectedIndex == 0 && LblStoreNearUserReal.Text != "")
                            {
                                String selectedStore = LblStoreNearUserReal.Text;

                                //On créer la command du jeu ciblé
                                commandInscrit = objDal.CreateCommand(etatCommand, titleGame, genreGame, dateRetrait, selectedStore, idGame, idClientCommand);
                                quantityStockGame = quantityStockGame - 1;
                            }
                            else if(DdlNameStore.SelectedIndex > 0)
                            {
                                String selectedStore = DdlNameStore.SelectedItem.Text;

                                //On créer la command du jeu ciblé
                                commandInscrit = objDal.CreateCommand(etatCommand, titleGame, genreGame, dateRetrait, selectedStore, idGame, idClientCommand);
                                quantityStockGame = quantityStockGame - 1;

                            }
                            else if(DdlNameStore.SelectedIndex == 0 && LblStoreNearUserReal.Visible == false)
                            {
                                Alert.Show("Vous devez choisir un magasin de retrait");
                            }
                        }
                        else
                        {
                            Alert.Show("Impossible de passé la commande. Nous n'avons plus ce jeu en stock");
                        }
                        

                        if(commandInscrit == true)
                        {
                            //On change la quantité en stock du jeu une fois que la commande est passé
                            bool isOk = objDal.UpdateQuantiteStock(quantityStockGame, titleGame);
                        }
                    }
                    else
                    {
                        Alert.Show("La date de retrait doit être à moins de 7 jours après la commande.");
                    }
                }
                else
                {
                    j = stringsInCart.Count + 1;
                    commandInscrit = false;
                    Alert.Show("Votre Panier ne doit pas être vide et une date de retrait doit être choisi avant de valider le panier.");
                }
            }

            if (commandInscrit == true)
            {
                //On supprime les objets dans le panier
                bool isAllDlelete = false;
                isAllDlelete = objDal.DeleteAllInCart(idClient);

                //On envoi l'email et on mets à jour le panier
                SendEmail();
                MaJCart();

                Alert.Show("Commande validé avecc succès ! Vous allez recevoir un mail de confirmation de commande.");

                Response.Redirect("~/Jeuxvideo");
            }
            else
            {
                Alert.Show("Impossible de passé la commande");
            }
        }

        //Méthode pour envoi du mail après validation des commandes
        private void SendEmail()
        {
            //Corps du Message du Mail
            String textMail = "Bonjour,\r\n \r\n Merci d'avoir passé commande chez Gamestore !\r\n \r\n Vous pouvez dès à présent voir le suivie de votre commande sur la page 'Mon Compte' de notre portail. \r\n \r\n N'oubliez pas de venir retirer votre commande dans le magasin choisi et à la date indiqué. \r\n\r\n En espérant vous voir dans un de nos magasin au plus vite.\r\n\r\n Cordialement\r\nL'équipe Gamestore";
            
            //Récupération de l'email de l'utilisateur via les Variables de Session
            String email = Session["MailUtilisateur"].ToString();

            //Initialisation des variables utiles pour l'envoi de mail
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();

            //Champ Destinataire
            message.To.Add(email);

            //Champs Expéditeur
            message.From = new System.Net.Mail.MailAddress("thomas59.lesage@gmail.com");
            //Sujet du mail
            message.Subject = "Validation de votre commande !";
            //Corps du mail
            message.Body = textMail;

            //Informations d'identification requises pour la connexion
            smtp.Credentials = new NetworkCredential("thomas59.lesage@gmail.com", "ungx otdh nqwi elhb");

            //Hôte SMTP + N° Port
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;

            //Activé la connexion sécurisé
            smtp.EnableSsl = true;

            //Envoi du mail
            smtp.Send(message);
        }

        //Cacher certaine date du Calendrier
        protected void CalendarCart_DayRender(object sender, DayRenderEventArgs e)
        {
            //Initialisation d'une variable pour avoir la date du jour
            DateTime dateToday = DateTime.Today;

            //On empêche l'utilisateur de pouvoir selectionner les dates antérieur à la date du jour
            if(e.Day.Date < dateToday)
            {
                e.Day.IsSelectable = false;
                e.Cell.ForeColor = Color.Gray;
            }
            else
            {
                e.Day.IsSelectable = true;
            }

            //On désactive les jours "Lundi" et" Dimanche"
            if (e.Day.Date.DayOfWeek == DayOfWeek.Monday)
            {
                e.Day.IsSelectable = false;
                e.Cell.ForeColor = Color.Gray;
            }
            else if (e.Day.Date.DayOfWeek == DayOfWeek.Sunday)
            {
                e.Day.IsSelectable = false;
                e.Cell.ForeColor = Color.Gray;
            }
        }

        
    }
}