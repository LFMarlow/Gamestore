using Gamestore.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gamestore
{
    public partial class MonCompte : System.Web.UI.Page
    {
        //Initialisation de l'objet de Class DAL pour requête SQL
        DALGamestore objDal = new DALGamestore();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(Session["EstConnecte"]) && Session["RoleUtilisateur"].ToString() == "Administrateur")
            {
                LblUsers.Text = Session["Prenom"].ToString() + " " + Session["Nom"].ToString();
                if (!IsPostBack)
                {
                    UPCreateVideoGames.Visible = false;
                    UPCreateEmploye.Visible = false;
                    UPGestionStocks.Visible = false;
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
        }

        protected void BtnGestionStocks_Click(object sender, EventArgs e)
        {
            UPCreateVideoGames.Visible = false;
            UPCreateEmploye.Visible = false;
            UPGestionStocks.Visible = true;

            List<String> listTitle = new List<string>();
            listTitle = objDal.RécupTousJeuxVideo();

            if (listTitle != null)
            {
                if (DDLVideoGames.Items.Count == 0)
                {
                    DDLVideoGames.Items.Add("");
                    foreach (var titre in listTitle)
                    {
                        DDLVideoGames.Items.Add(titre);
                    }
                }
            }
          /*  IMGGame.ImageUrl  =  objJeuxVideo.urlImage;
            LblTitleGame.Text = objJeuxVideo.title;
            LblQteStock.Text = objJeuxVideo.quantite.ToString();*/
        }

        protected void BtnDashboard_Click(object sender, EventArgs e)
        {
            UPCreateVideoGames.Visible = false;
            UPCreateEmploye.Visible = false;
        }

        protected void BtnCreateEmploye_Click(object sender, EventArgs e)
        {
            UPCreateVideoGames.Visible = false;
            UPCreateEmploye.Visible = true;
        }

        protected void BtnCreateCompteEmploye_Click(object sender, EventArgs e)
        {
            String nom = TxtBoxNomEmploye.Text;
            String prenom = TxtBoxPrenomEmploye.Text;
            String mail = TxtBoxMailEmploye.Text;
            String password = TxtBoxPasswordEmploye.Text;
            String passwordHash = "";
            String role = "Employé";
            bool estInscrit = false;

            passwordHash = SecurePassword.Hash(password);

            if (TxtBoxNomEmploye.Text != "" && TxtBoxPrenomEmploye.Text != "" && TxtBoxMailEmploye.Text != "" && TxtBoxPasswordEmploye.Text != "")
            {
                estInscrit = objDal.InscriptionEmploye(nom, prenom, mail, passwordHash, role);

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

            bool inscritOK = false;

            if (TxtBoxTitreGame.Text != "" && TxtBoxURLImage.Text != "" && TxtBoxPriceGame.Text != "" && TxtBoxPEGIGame.Text != "" && TxtBoxQuantityGame.Text != "" && TxtBoxGenreGame.Text != "" && TxtBoxDescription.Text != "")
            {
                inscritOK = objDal.CreateGame(URLImage, titleGame, priceGame, pegiGame, quantityGame, genreGame, descriptionGame);
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
}