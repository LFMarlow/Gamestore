using Gamestore.Classes;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Util;
using static System.Net.Mime.MediaTypeNames;

namespace Gamestore
{
    public partial class DetailJeuxVideo : System.Web.UI.Page
    {
        DALGamestore objDal = new DALGamestore();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    String urlGameBrut = null;
                    String realURLGame = null;
                    String verifDoublon = null;
                    String emailUsers = null;

                    int discount = 0;
                    float priceDiscount = 0;

                    if (Convert.ToBoolean(Session["EstConnecte"]))
                    {
                        //Variable pour Récupération de l'email de l'utilisateur pour avoir son ID
                        emailUsers = Session["MailUtilisateur"].ToString();
                    }
                    //Récupération du titre du jeux via l'URL
                    urlGameBrut = QueryURL();
                    realURLGame = urlGameBrut.Replace('_', ' ');

                    int idClient = objDal.RecupClientID(emailUsers);
                    verifDoublon = objDal.VerifDoublonInCart(realURLGame, idClient);
                    discount = objDal.RecupDiscountJeuxVideo(realURLGame);
                    priceDiscount = objDal.RecupPriceDiscountJeuxVideo(realURLGame);
                    
                    //On affiche toutes les informations retenues dans la BDD
                    Classes.JeuxVideo objJeuxVideo = new Classes.JeuxVideo();
                    objJeuxVideo = objDal.RécupJeuxVideo(realURLGame);

                    IMGGame.ImageUrl = objJeuxVideo.urlImage;
                    LblTitleGame.Text = objJeuxVideo.title;
                    LblStock.Text = objJeuxVideo.quantite + " " + "en Stock";
                    LblPEGI.Text = "PEGI" + " " + objJeuxVideo.pegi;
                    LblGenre.Text = objJeuxVideo.genre;
                    LblPrice.Text = objJeuxVideo.prix + "€";

                    //Si il y a un prix remisé on l'affiche en même temps que la réduction
                    if (priceDiscount > 0)
                    {
                        //Si il y a un prix remisé, il y a forcément une remise, on affiche donc la remise ainsi que le prix remisé
                        LblDiscount.Visible = true;
                        LblDiscount.Text = "-" + discount.ToString() + "%";

                        //On affiche le prix remisé
                        LblPriceDiscount.Visible = true;
                        LblPriceDiscount.Text = priceDiscount.ToString() + "€";

                        //On ajoute un attribut CSS pour barrer le prix de base si réduction il y a
                        LblPrice.Attributes.CssStyle.Add("text-decoration", "line-through");
                        
                    }
                    if (objJeuxVideo.quantite == 0)
                    {
                        bi_check.Visible = false;
                        LblStock.Visible = false;
                        bi_cross.Visible = true;
                        LblStockVide.Text = objJeuxVideo.quantite + " " + "en Stock";
                    }
                    else
                    {
                        bi_check.Visible = true;
                        LblStock.Visible = true;
                        bi_cross.Visible = false;
                        LblStockVide.Visible = false;
                    }

                    if (objJeuxVideo.description != null)
                    {
                        //On Split la description des jeux par rapport au fin de ligne.
                        List<String> DescriptionJeuxVideoSplit = new List<string>(objJeuxVideo.description.Split('.'));
                        int i = 0;

                        foreach (var desc in DescriptionJeuxVideoSplit)
                        {
                            if (desc != "")
                            {
                                if (desc != "\" ")
                                {
                                    BulletListDescription.Items.Add(desc + ".");
                                }
                            }
                            i++;
                        }

                        //On supprime la dernière ligne qui ne contient aucun caractère (Split au .)
                        //BulletListDescription.Items.RemoveAt(i - 1);

                        if (bi_cross.Visible == true)
                        {
                            BtnAddCart.Visible = false;
                        }

                        if (Convert.ToBoolean(Session["EstConnecte"]) == false)
                        {
                            BtnAddCart.Visible = false;
                            div_check.Visible = false;
                            LblAlreadyInCart.Visible = false;
                        }
                        else
                        {
                            if (verifDoublon == objJeuxVideo.title)
                            {
                                BtnAddCart.Visible = false;
                                div_check.Visible = true;
                                LblAlreadyInCart.Visible = true;
                            }
                            else
                            {
                                BtnAddCart.Visible = true;
                                div_check.Visible = false;
                                LblAlreadyInCart.Visible = false;
                            }
                        }
                        if (Convert.ToBoolean(Session["EstConnecte"]) == true && Session["RoleUtilisateur"].ToString() != "Utilisateur")
                        {
                            BtnAddCart.Visible = false;
                        }
                    }
                    else
                    {
                        Response.Redirect("~/Jeuxvideo");
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }

        protected void BtnAddCart_Click(object sender, EventArgs e)
        {
            string titleGame = LblTitleGame.Text;
            string recupMailIUsers = Session["MailUtilisateur"].ToString();
            bool isOk = false;
            int recupClientID = -1;
            int recupGameId = -1;

            recupClientID = objDal.RecupClientID(recupMailIUsers);
            recupGameId = objDal.RecupGameID(titleGame);

            if (recupClientID > -1)
            {
                isOk = objDal.AddToCart(titleGame, recupClientID, recupGameId);

                if(isOk)
                {
                    Alert.Show("Ajout au panier réussi !");
                    BtnAddCart.Visible = false;
                    div_check.Visible = true;
                    LblAlreadyInCart.Visible = true;

                    //Affichage de l'image du panier
                    System.Web.UI.WebControls.Image Img_Cart = Master.FindControl("ImgCart") as System.Web.UI.WebControls.Image;
                    if (Img_Cart != null)
                    {
                        Img_Cart.Visible = true;
                    }
                }
            }
            else
            {
                Alert.Show("Impossible d'ajouter ce jeu au panier. Assurez-vous d'être connecté.");
            }
            
        }

        public String QueryURL()
        {
            String str = Request.Url.ToString();
            Uri uri = new Uri(str);
            
            var parameters = HttpUtility.ParseQueryString(uri.Query);
            var encoded = Uri.EscapeDataString(parameters.ToString());
            string rootFolder = encoded.ToString();

            return rootFolder;
        }
    }
}