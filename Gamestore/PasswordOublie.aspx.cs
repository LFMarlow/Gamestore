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

namespace Gamestore
{
    public partial class PasswordOublie : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public static byte[] Encrypt_AES(string plainText, byte[] key, byte[] iv)
        {
            byte[] encrypted;

            using (AesManaged aes = new AesManaged())
            {

                ICryptoTransform encryptor = aes.CreateEncryptor(key, iv);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(plainText);
                        encrypted = ms.ToArray();
                    }
                }
            }
            // Return encrypted data
            return encrypted;
        }

        protected void BtnChangePassword_Click(object sender, EventArgs e)
        {
            String mailDest = TxtBoxMail.Text;
            String lien = "https://localhost:44368/RedefinirPassword";

            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();

            //Champ Destinataire
            message.To.Add(mailDest);

            //Champs Expéditeur
            message.From = new System.Net.Mail.MailAddress("thomas59.lesage@gmail.com");
            //Sujet du mail
            message.Subject = "Réinitialisation de Mot de Passe";
            //Corps du mail
            message.Body = "Lien de réinitialisation :" + lien;


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
        }
    }
}