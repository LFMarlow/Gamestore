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

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var games = await objDal.RecupAllJeuxVideoAsync();
                DisplayGames(games);
            }

            if (Convert.ToBoolean(Session["EstConnecte"]))
            {
                if (Session["RoleUtilisateur"].ToString() == "Utilisateur")
                {
                    Image Img_Cart = Master.FindControl("ImgCart") as Image;

                    //Récupération de l'email de l'utilisateur pour avoir l'ID Client
                    String emailUsers = Convert.ToString(Session["MailUtilisateur"]);
                    int idClient = objDal.RecupClientID(emailUsers);

                    //Récupération du contenu du panier avec l'idClient
                    List<String> listCart = new List<String>();
                    listCart = objDal.RécupCart(idClient);

                    //Si le panier est vide, le symbole ne s'affiche pas, sinon oui
                    if (listCart.Count <= 0)
                    {
                        Img_Cart.Visible = false;
                    }
                    else
                    {
                        Img_Cart.Visible = true;
                    }
                }
            }
        }

        //Affichage de tout les jeux + Filtre
        private void DisplayGames(List<JeuxVideo> games)
        {
            List<String> listGenreJV = new List<string>();
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

            StringBuilder sb = new StringBuilder();

            sb.Append(@"<div class='grid'>");

            foreach (var game in games)
            {
                if (game.price_discount.HasValue && game.price_discount.Value > 0)
                {
                    //Ajouter la classe de genre + le prix pour filtre dynamiquement
                    sb.AppendFormat(@"<div id='info {0}' class='genreGame' data-price='" + game.price_discount + "'>", game.genre);
                }
                else
                {
                    //Ajouter la classe de genre + le prix pour filtre dynamiquement
                    sb.AppendFormat(@"<div id='info {0}' class='genreGame' data-price='" + game.prix + "'>", game.genre);
                }

                // Lien de redirection
                string detailUrl = $"/DetailJeuxVideo?{game.title.Replace(" ", "_")}";

                // Image du jeu avec lien
                sb.AppendFormat(@"<a href='{0}'>", detailUrl);
                sb.AppendFormat(@"<img src='{0}' class='img_game' />", game.urlImage);
                sb.AppendFormat(@"</a>");

                if (game.discount > 0)
                {
                    sb.AppendFormat(@"<div class='discount'>-{0}%</div>", game.discount);
                }

                // PEGI
                sb.Append(@"<div class='PEGI'>");
                string pegiImg = objDal.RecupPegi("pegi_" + game.pegi);
                sb.AppendFormat("<img class='img_pegi' src='{0}' />", pegiImg);
                sb.Append(@"</div>");

                //InfoGame
                sb.Append(@"<div class = 'info_game'>");

                // Titre du jeu
                sb.AppendFormat(@"<div class='title_game'>{0}</div>", game.title);

                // Prix ou Prix réduit
                if (game.price_discount.HasValue && game.price_discount.Value > 0)
                {
                    sb.AppendFormat(@"<div class='price_game'>{0}€</div>", game.price_discount.Value);
                }
                else
                {
                    sb.AppendFormat(@"<div class='price_game'>{0}€</div>", game.prix);
                }

                sb.Append(@"</div>");
                // Stock
                if (game.quantite <= 0)
                {
                    sb.Append(@"<div class='no_stock_game'>En rupture de stock</div>");
                }
                else
                {
                    if (Convert.ToBoolean(Session["EstConnecte"]) == true && Session["RoleUtilisateur"].ToString() == "Utilisateur")
                    {
                        // Ajout d'un bouton avec gestionnaire d'événement
                        string buttonId = "btnAddToCart_" + game.title.Replace(" ", "_");
                        sb.AppendFormat(@"<button id='{0}' class='BtnAddToCart' onclick='addToCart(""{1}""); return false;'>Ajouter au panier</button>", buttonId, game.title.Replace("'", "\\'"));
                    }
                }

                //Fin de .info_game
                sb.Append(@"</div>");
            }

            //Fin de .grid
            sb.Append(@"</div>");

            PnlGame.Controls.Add(new LiteralControl(sb.ToString()));
        }

        [System.Web.Services.WebMethod]
        public static String AddToCart(string title)

        {
            // Vérifier si l'utilisateur est connecté
            if (HttpContext.Current.Session["EstConnecte"] != null && Convert.ToBoolean(HttpContext.Current.Session["EstConnecte"]))
            {
                // Récupérer l'ID de l'utilisateur depuis la session
                string email = HttpContext.Current.Session["MailUtilisateur"].ToString();
                DALGamestore objDal = new DALGamestore();
                int userId = objDal.RecupClientID(email);

                //Récupération de l'ID du jeux auquels le bouton est attaché
                int recupGameId = objDal.RecupGameID(title);

                // Ajouter le jeu au panier
                bool result = objDal.AddToCart(title, userId, recupGameId);

                // Retourner un message ou un statut à la requête AJAX
                return result ? "Jeu ajouté au panier avec succès!" : "Erreur lors de l'ajout au panier.";
            }
            else
            {
                // Gérer le cas où l'utilisateur n'est pas connecté
                return "Utilisateur non connecté.";
            }
        }
    }
}