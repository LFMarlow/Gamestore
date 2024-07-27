using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.IO;
using System.Net;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using System.Configuration;
using Gamestore.Classes;

namespace Gamestore
{
    public partial class PasswordOublie : System.Web.UI.Page
    {
        DALGamestore objDal = new DALGamestore();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnChangePassword_Click(object sender, EventArgs e)
        {
            String mailDest = TxtBoxMail.Text;
            String recupTokenUsers = null;
            recupTokenUsers = objDal.RecupTokenUsers(mailDest);

            if (recupTokenUsers != null)
            {
                String textMail = "Bonjour,\r\n \r\n Vous avez demandé la réinitialisation de votre Mot de Passe. \r\n \r\n Veuillez cliquer sur le lien suivant pour effectuer le changement.\r\n \r\n";
                String lien = "https://localhost:44368/RedefinirPassword?" + recupTokenUsers;

                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();

                //Champ Destinataire
                message.To.Add(mailDest);

                //Champs Expéditeur
                message.From = new System.Net.Mail.MailAddress("thomas59.lesage@gmail.com");
                //Sujet du mail
                message.Subject = "Réinitialisation de Mot de Passe";
                //Corps du mail
                message.Body = textMail + "Lien de réinitialisation :" + " " + lien;


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

                Alert.Show("Un E-Mail vient de vous être envoyé.");
            }
            else
            {
                Alert.Show("Envoie du mail impossible. Assurez-vous d'avoir correctement entré votre Adresse E-Mail.");
            }
        }
    }
}