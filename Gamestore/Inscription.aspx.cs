using Gamestore.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
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
            // Récupération des données du formulaire
            string nom = TxtBoxNom.Text.Trim();
            string prenom = TxtBoxPrenom.Text.Trim();
            string email = TxtBoxMail.Text.Trim();
            string passwordBrut = TxtBoxMDP.Text;
            string postalAdress = TxtBoxAP.Text.Trim();
            string role = "Utilisateur";
            int lengthToken = 10;

            // Validation des entrées
            if (ValidateInputs(nom, prenom, email, passwordBrut, postalAdress))
            {
                // Hashage du mot de passe
                string passwordHash = SecurePassword.Hash(passwordBrut);
                string tokenNewUsers = TokenUsers.GetRandom(lengthToken);

                try
                {
                    // Vérification de doublon et inscription
                    Users objUsers = objDal.VerifDoublonMailInscription(email);
                    if (objUsers == null)
                    {
                        bool inscritOK = objDal.Inscription(nom, prenom, email, passwordHash, role, postalAdress, tokenNewUsers);
                        if (inscritOK)
                        {
                            SendWelcomeEmail(email);
                            string script = "alert('Inscription réussie ! Vous pouvez dès à présent vous connecter.'); window.setTimeout(function() { window.location.href = 'Connexion.aspx'; }, 3000);";
                            ClientScript.RegisterStartupScript(this.GetType(), "redirect", script, true);
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
                    Alert.Show("Connexion à la base de données échouée, veuillez contacter le support.");
                }
            }
            else
            {
                Alert.Show("Les entrées contiennent des caractères invalides ou sont incomplètes.");
            }
        }

        private bool ValidateInputs(string nom, string prenom, string email, string password, string address)
        {
            //Regex pour identifier les tentatives d'injection de script
            string pattern = @"<script>|javascript:|<[^>]+>";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

            //Valider que les entrées ne contiennent pas de scripts ou de HTML malveillant
            if (regex.IsMatch(nom) || regex.IsMatch(prenom) || regex.IsMatch(email) ||
                regex.IsMatch(password) || regex.IsMatch(address))
            {
                return false;
            }

            string emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            return !string.IsNullOrWhiteSpace(nom) &&
                   !string.IsNullOrWhiteSpace(prenom) &&
                   Regex.IsMatch(email, emailRegex) &&
                   !string.IsNullOrWhiteSpace(password) &&
                   !string.IsNullOrWhiteSpace(address) &&
                   email.Contains("@");
        }

        private void SendWelcomeEmail(string email)
        {
            var message = new System.Net.Mail.MailMessage("thomas59.lesage@gmail.com", email)
            {
                Subject = "Bienvenue chez Gamestore !",
                Body = "Bonjour,\\r\\n \\r\\n Merci pour la création de votre compte sur notre portail !\\r\\n \\r\\n Vous pouvez dès à présent voir notre selection de jeux vidéos et les ajouter à votre panier !\\r\\n \\r\\n Merci encore de l'attention que vous nous portez. \\r\\n\\r\\n En espérant vous voir dans un de nos magasin au plus vite.\\r\\n\\r\\n Cordialement\\r\\nL'équipe Gamestore\""
            };

            using (var smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587))
            {
                smtp.Credentials = new NetworkCredential("thomas59.lesage@gmail.com", "ofgu iskc oyvj ynqc");
                smtp.EnableSsl = true;
                smtp.Send(message);
            }
        }
    }
}