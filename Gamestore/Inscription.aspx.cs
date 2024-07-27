using Gamestore.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Gamestore
{
    public partial class Inscription : System.Web.UI.Page
    {
        DALGamestore objDal = new DALGamestore();
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void BtnInscription_Click(object sender, EventArgs e)
        {
            //Variables d'inscription
            String nom;
            String prenom;
            String passwordBrut;
            String email;
            String postalAdress;
            String roleUsers = "Utilisateur";
            String tokenNewUsers = null;
            int lengthToken = 10;
            bool inscritOK = false;

            //Création d'un objet de Classe Users pour plusieurs vérification
            Classes.Users objUsers = new Users();

            //Enregistrement des informations rentrées dans les variables
            nom = TxtBoxNom.Text;
            prenom = TxtBoxPrenom.Text;
            passwordBrut = TxtBoxMDP.Text;
            email = TxtBoxMail.Text;
            postalAdress = TxtBoxAP.Text;
            tokenNewUsers = TokenUsers.GetRandom(lengthToken);

            var passwordHash = SecurePassword.Hash(passwordBrut);
            

            if(nom != "" && prenom != "" && email != "" && passwordBrut != "" && postalAdress != "")
            {
                try
                {
                    objUsers = objDal.VerifDoublonMailInscription(email);
                    if(objUsers == null)
                    {
                        inscritOK = objDal.Inscription(nom, prenom, email, passwordHash, roleUsers, postalAdress, tokenNewUsers);
                        if(inscritOK == true)
                        {
                            String textMail = "Bonjour,\r\n \r\n Merci pour la création de votre compte sur notre portail !\r\n \r\n Vous pouvez dès à présent voir notre selection de jeux vidéos et les ajouter à votre panier !\r\n \r\n Merci encore de l'attention que vous nous portez. \r\n\r\n En espérant vous voir dans un de nos magasin au plus vite.\r\n\r\n Cordialement\r\nL'équipe Gamestore";

                            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();

                            //Champ Destinataire
                            message.To.Add(email);

                            //Champs Expéditeur
                            message.From = new System.Net.Mail.MailAddress("thomas59.lesage@gmail.com");
                            //Sujet du mail
                            message.Subject = "Bienvenue chez Gamestore !";
                            //Corps du mail
                            message.Body = textMail;


                            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                            //Informations d'identification requises pour la connexion
                            smtp.Credentials = new NetworkCredential("thomas59.lesage@gmail.com", "ungx otdh nqwi elhb");

                            //Hôte SMTP + N° Port
                            smtp.Host = "smtp.gmail.com";
                            smtp.Port = 587;

                            //Activé la connexion sécurisé
                            smtp.EnableSsl = true;

                            //Envoi du mail
                            smtp.Send(message);

                            Alert.Show("Inscription Réussi ! Vous pouvez dès à présent vous connecter.");
                            Response.Redirect("~/Connexion", false);
                        }
                        else
                        {
                            Alert.Show("Inscription impossible, veuillez contacter le support.");
                        }
                        
                    }
                    else
                    {
                        Alert.Show("Cette adresse email existe déjà.");
                    }

                }
                catch (Exception ex)
                {
                    Alert.Show("Connexion à la Base de données echoué, veuillez contacter le support");
                }
            }
            else
            {
                Alert.Show("Tout les champs doivent être remplis");
            }
        }
    }
}