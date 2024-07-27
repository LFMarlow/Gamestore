using Gamestore.Classes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gamestore
{
    public partial class MonCompteUtilisateur : System.Web.UI.Page
    {
        //Initialisation des objets
        DALGamestore objDal = new DALGamestore();
        WBServiceGamestore objProxy = new WBServiceGamestore();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(Session["EstConnecte"]) && Session["RoleUtilisateur"].ToString() == "Utilisateur")
            {
                LblUsers.Text = Session["Prenom"].ToString() + " " + Session["Nom"].ToString();
                if (!IsPostBack)
                {
                    PnlHistCommand.Visible = false;
                    PnlModifInfos.Visible = false;
                    title_menu.Visible = false;

                    TxtBoxNom.Text = Convert.ToString(Session["Nom"]);
                    TxtBoxPrenom.Text = Convert.ToString(Session["Prenom"]);
                    TxtBoxEmail.Text = Convert.ToString(Session["MailUtilisateur"]);
                    TxtBoxAdresse.Text = Convert.ToString(Session["PostalAdress"]);
                }
            }
            else
            {
                Response.Redirect("~/Connexion");
            }
        }

        protected void BtnHistCommandValider_Click(object sender, EventArgs e)
        {
            PnlHistCommand.Visible = true;
            PnlModifInfos.Visible = false;
            title_menu.Visible = true;
            title_menu.InnerText = "Historique des commandes";

            //Récupération de l'id de l'utilisateur
            String emailClient = Convert.ToString(Session["MailUtilisateur"]);
            int idClient = objDal.RecupClientID(emailClient);

            //Récupération des informatins dans la table "command"
            List<String> listHistCommand = new List<String>();
            listHistCommand = objDal.RécupCommandLivré(idClient);

            StringBuilder sbGrid = new StringBuilder();
            StringBuilder sb0 = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            StringBuilder sb3 = new StringBuilder();
            StringBuilder sb4 = new StringBuilder();
            StringBuilder sb5 = new StringBuilder();
            StringBuilder sb6 = new StringBuilder();

            sbGrid.Append(@"<div class='grid'>");
            sb0.Append(@"<div class='hc'>");
            sb1.Append(@"<div class='hc_menu'>");
            sb2.Append(@"<br />");
            sb3.Append(@"<div class='contenu_game'>");
            sb4.Append(@"<hr class='separate_content' />");
            sb5.Append(@"<div class='contenu3_game'>");
            sb6.Append(@"</div>");

            int i = 0;

            if (listHistCommand.Count > 0)
            {
                PnlHistCommand.Controls.Add(new LiteralControl(sbGrid.ToString()));
                for (int j = 0; j < listHistCommand.Count; j++)
                {

                    Image ImgGame = new Image();
                    Label LblTitle = new Label();
                    Label LblPrice = new Label();
                    Label LblGenre = new Label();
                    Label LblStatutCommand = new Label();

                    ImgGame.ID = "ImgGame" + i.ToString();
                    ImgGame.ImageUrl = listHistCommand[i];
                    ImgGame.CssClass = "img_game";
                    i++;
                    j++;

                    LblTitle.ID = "LblTitle" + i.ToString();
                    LblTitle.Text = "Nom du jeu : " + listHistCommand[i].ToString();

                    float priceDiscount = objDal.RecupPriceDiscountJeuxVideo(listHistCommand[i]);
                    i++;
                    j++;

                    if (priceDiscount <= 0)
                    {
                        LblPrice.ID = "LblPrice" + i.ToString();
                        LblPrice.Text = "Prix : " + listHistCommand[i].ToString() + "€";
                    }
                    else
                    {
                        LblPrice.ID = "LblPrice" + i.ToString();
                        LblPrice.Text = "Prix : " + priceDiscount.ToString() + "€";
                    }
                    i++;
                    j++;

                    LblGenre.ID = "LblGenre" + i.ToString();
                    LblGenre.Text = "Genre : " + listHistCommand[i].ToString();
                    i++;
                    j++;

                    LblStatutCommand.ID = "LblStatutCommand" + i.ToString();
                    LblStatutCommand.Text = listHistCommand[i].ToString();
                    LblStatutCommand.CssClass = "statut_command";
                    i++;
                    j++;

                    if (LblStatutCommand.Text == "Validé")
                    {
                        String dateRetrait = listHistCommand[i].ToString();
                        String dateRetraitFinal = dateRetrait.Substring(0, 10);

                        LblStatutCommand.Text = "Commande en attente de retrait le " + dateRetraitFinal;
                    }
                    i++;


                    //Affichage dynamique
                    PnlHistCommand.Controls.Add(new LiteralControl(sb0.ToString()));
                    PnlHistCommand.Controls.Add(new LiteralControl(sb1.ToString()));
                    PnlHistCommand.Controls.Add(LblStatutCommand);
                    PnlHistCommand.Controls.Add(new LiteralControl(sb2.ToString()));
                    PnlHistCommand.Controls.Add(new LiteralControl(sb2.ToString()));
                    PnlHistCommand.Controls.Add(ImgGame);
                    PnlHistCommand.Controls.Add(new LiteralControl("&nbsp;"));
                    PnlHistCommand.Controls.Add(new LiteralControl("&nbsp;"));
                    PnlHistCommand.Controls.Add(new LiteralControl(sb3.ToString()));
                    PnlHistCommand.Controls.Add(LblTitle);
                    PnlHistCommand.Controls.Add(new LiteralControl(sb2.ToString()));
                    PnlHistCommand.Controls.Add(new LiteralControl(sb2.ToString()));
                    PnlHistCommand.Controls.Add(LblGenre);
                    PnlHistCommand.Controls.Add(new LiteralControl(sb4.ToString()));
                    PnlHistCommand.Controls.Add(new LiteralControl(sb5.ToString()));
                    PnlHistCommand.Controls.Add(LblPrice);
                    PnlHistCommand.Controls.Add(new LiteralControl(sb6.ToString()));
                    PnlHistCommand.Controls.Add(new LiteralControl(sb6.ToString()));
                    PnlHistCommand.Controls.Add(new LiteralControl(sb2.ToString()));
                    PnlHistCommand.Controls.Add(new LiteralControl(sb2.ToString()));
                    PnlHistCommand.Controls.Add(new LiteralControl(sb6.ToString()));
                    PnlHistCommand.Controls.Add(new LiteralControl(sb6.ToString()));
                    PnlHistCommand.Controls.Add(new LiteralControl(sb2.ToString()));
                }
                PnlHistCommand.Controls.Add(new LiteralControl(sb6.ToString()));
            }
            else
            {
                title_menu.InnerText = "Pas d'historique de commande";
            }

            if (listHistCommand == null)
            {
                title_menu.InnerText = "Pas d'historique de commande";
            }
        }

        protected void BtnHistCommandNonLivre_Click(object sender, EventArgs e)
        {
            PnlHistCommand.Visible = true;
            PnlModifInfos.Visible = false;
            title_menu.Visible = true;
            title_menu.InnerText = "Historique des commandes";

            //Récupération de l'id de l'utilisateur
            String emailClient = Convert.ToString(Session["MailUtilisateur"]);
            int idClient = objDal.RecupClientID(emailClient);

            //Récupération des informatins dans la table "command"
            List<String> listHistCommand = new List<String>();
            listHistCommand = objDal.RécupCommandNonLivre(idClient);

            StringBuilder sbGrid = new StringBuilder();
            StringBuilder sb0 = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            StringBuilder sb3 = new StringBuilder();
            StringBuilder sb4 = new StringBuilder();
            StringBuilder sb5 = new StringBuilder();
            StringBuilder sb6 = new StringBuilder();

            sbGrid.Append(@"<div class='grid'>");
            sb0.Append(@"<div class='hc'>");
            sb1.Append(@"<div class='hc_menu'>");
            sb2.Append(@"<br />");
            sb3.Append(@"<div class='contenu_game'>");
            sb4.Append(@"<hr class='separate_content' />");
            sb5.Append(@"<div class='contenu3_game'>");
            sb6.Append(@"</div>");

            int i = 0;

            if (listHistCommand.Count > 0)
            {
                PnlHistCommand.Controls.Add(new LiteralControl(sbGrid.ToString()));
                for (int j = 0; j < listHistCommand.Count; j++)
                {

                    Image ImgGame = new Image();
                    Label LblTitle = new Label();
                    Label LblPrice = new Label();
                    Label LblGenre = new Label();
                    Label LblStatutCommand = new Label();

                    ImgGame.ID = "ImgGame" + i.ToString();
                    ImgGame.ImageUrl = listHistCommand[i];
                    ImgGame.CssClass = "img_game";
                    i++;
                    j++;

                    LblTitle.ID = "LblTitle" + i.ToString();
                    LblTitle.Text = "Nom du jeu : " + listHistCommand[i].ToString();

                    float priceDiscount = objDal.RecupPriceDiscountJeuxVideo(listHistCommand[i]);
                    i++;
                    j++;

                    if (priceDiscount <= 0)
                    {
                        LblPrice.ID = "LblPrice" + i.ToString();
                        LblPrice.Text = "Prix : " + listHistCommand[i].ToString() + "€";
                    }
                    else
                    {
                        LblPrice.ID = "LblPrice" + i.ToString();
                        LblPrice.Text = "Prix : " + priceDiscount.ToString() + "€";
                    }
                    i++;
                    j++;

                    LblGenre.ID = "LblGenre" + i.ToString();
                    LblGenre.Text = "Genre : " + listHistCommand[i].ToString();
                    i++;
                    j++;

                    LblStatutCommand.ID = "LblStatutCommand" + i.ToString();
                    LblStatutCommand.Text = listHistCommand[i].ToString();
                    LblStatutCommand.CssClass = "statut_command";
                    i++;
                    j++;

                    if (LblStatutCommand.Text == "Validé")
                    {
                        String dateRetrait = listHistCommand[i].ToString();
                        String dateRetraitFinal = dateRetrait.Substring(0, 10);

                        LblStatutCommand.Text = "Commande en attente de retrait le " + dateRetraitFinal;
                    }
                    i++;


                    //Affichage dynamique
                    PnlHistCommand.Controls.Add(new LiteralControl(sb0.ToString()));
                    PnlHistCommand.Controls.Add(new LiteralControl(sb1.ToString()));
                    PnlHistCommand.Controls.Add(LblStatutCommand);
                    PnlHistCommand.Controls.Add(new LiteralControl(sb2.ToString()));
                    PnlHistCommand.Controls.Add(new LiteralControl(sb2.ToString()));
                    PnlHistCommand.Controls.Add(ImgGame);
                    PnlHistCommand.Controls.Add(new LiteralControl("&nbsp;"));
                    PnlHistCommand.Controls.Add(new LiteralControl("&nbsp;"));
                    PnlHistCommand.Controls.Add(new LiteralControl(sb3.ToString()));
                    PnlHistCommand.Controls.Add(LblTitle);
                    PnlHistCommand.Controls.Add(new LiteralControl(sb2.ToString()));
                    PnlHistCommand.Controls.Add(new LiteralControl(sb2.ToString()));
                    PnlHistCommand.Controls.Add(LblGenre);
                    PnlHistCommand.Controls.Add(new LiteralControl(sb4.ToString()));
                    PnlHistCommand.Controls.Add(new LiteralControl(sb5.ToString()));
                    PnlHistCommand.Controls.Add(LblPrice);
                    PnlHistCommand.Controls.Add(new LiteralControl(sb6.ToString()));
                    PnlHistCommand.Controls.Add(new LiteralControl(sb6.ToString()));
                    PnlHistCommand.Controls.Add(new LiteralControl(sb2.ToString()));
                    PnlHistCommand.Controls.Add(new LiteralControl(sb2.ToString()));
                    PnlHistCommand.Controls.Add(new LiteralControl(sb6.ToString()));
                    PnlHistCommand.Controls.Add(new LiteralControl(sb6.ToString()));
                    PnlHistCommand.Controls.Add(new LiteralControl(sb2.ToString()));
                }
                PnlHistCommand.Controls.Add(new LiteralControl(sb6.ToString()));
            }
            else
            {
                title_menu.InnerText = "Pas d'historique de commande";
            }

            if (listHistCommand == null)
            {
                title_menu.InnerText = "Pas d'historique de commande";
            }
        }

        protected void BtnDisconnect_Click(object sender, EventArgs e)
        {
            Session["EstConnecte"] = false;
            Session["MailUtilisateur"] = "";
            Session["Nom"] = "";
            Session["Prenom"] = "";
            Session["RoleUtilisateur"] = "";
            Session["PostalAdress"] = "";
            Response.Redirect(Request.RawUrl);
        }

        protected void BtnChangeInfo_Click(object sender, EventArgs e)
        {
            PnlHistCommand.Visible = false;
            PnlModifInfos.Visible = true;
            title_menu.Visible = true;
            title_menu.InnerText = "Coordonnées";
        }

        protected void BtnValidate_Click(object sender, EventArgs e)
        {
            //Récupération de l'id de l'utilisateur
            String emailClient = Convert.ToString(Session["MailUtilisateur"]);
            int idClient = objDal.RecupClientID(emailClient);

            //Récupération des nouvelles informations
            String newEmail = TxtBoxEmail.Text;
            String newNom = TxtBoxNom.Text;
            String newPrenom = TxtBoxPrenom.Text;
            String newPostalAdress = TxtBoxAdresse.Text;

            bool estChangé = objDal.InfosUserChanged(idClient, newNom, newPrenom, newEmail, newPostalAdress);

            if (estChangé == true)
            {
                //Récupération des nouvelles informations dans les variables de session
                Session["EstConnecte"] = true;
                Session["MailUtilisateur"] = newEmail;
                Session["Nom"] = newNom;
                Session["Prenom"] = newPrenom;
                Session["PostalAdress"] = newPostalAdress;

                Alert.Show("Informations mis à jour.");

                //Mise à jour des TextBox
                TxtBoxNom.Text = Convert.ToString(Session["Nom"]);
                TxtBoxPrenom.Text = Convert.ToString(Session["Prenom"]);
                TxtBoxEmail.Text = Convert.ToString(Session["MailUtilisateur"]);
                TxtBoxAdresse.Text = Convert.ToString(Session["PostalAdress"]);

                //Mise à jour des labels d'accueil
                LblUsers.Text = Session["Prenom"].ToString() + " " + Session["Nom"].ToString();
            }
        }

        protected void BtnChangePassword_Click(object sender, EventArgs e)
        {
            String emailClient = Convert.ToString(Session["MailUtilisateur"]);
            String actualPassword = TxtBoxActualPassword.Text;
            String newPassword = TxtBoxNewPassword.Text;
            String newPasswordConfirmed = TxtBoxNewPasswordConfirmed.Text;
            String actualPasswordHash = objProxy.RecupPasswordHash(emailClient);

            bool isOk = false;

            if (actualPassword != newPassword)
            {
                if (newPassword == newPasswordConfirmed)
                {
                    if (actualPasswordHash != null)
                    {
                        //Vérification entre le mot de passe entré par l'utilisateur et le hachage
                        var resultPassword = SecurePassword.Verify(actualPassword, actualPasswordHash);

                        //Si le hachage est correct
                        if (resultPassword == true)
                        {
                            var newPasswordHash = SecurePassword.Hash(newPasswordConfirmed);
                            isOk = objDal.PasswordChanged(emailClient, newPasswordHash);

                            if (isOk == true)
                            {
                                Alert.Show("Mot de passe changé avec succès !");
                                TxtBoxActualPassword.Text = "";
                                TxtBoxNewPassword.Text = "";
                                TxtBoxNewPasswordConfirmed.Text = "";

                            }
                            else
                            {
                                Alert.Show("Impossible de changer le mot de passe");
                            }
                        }
                    }
                }
                else
                {
                    Alert.Show("Les nouveaux mot de passe ne sont pas identiques.");
                }
            }
            else
            {
                Alert.Show("Le mot de passe actuel et le nouveau doivent-être différents.");
            }
        }
    }
}