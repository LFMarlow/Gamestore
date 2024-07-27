using Gamestore.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gamestore
{
    public partial class RedefinirPassword : System.Web.UI.Page
    {
        DALGamestore objDal = new DALGamestore();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnNewPassword_Click(object sender, EventArgs e)
        {
            if (TxtBoxFirstMDP.Text == TxtBoxSecondMDP.Text)
            {
                String tokenUsers = null;
                tokenUsers = QueryURL();

                String newPassword = TxtBoxSecondMDP.Text;
                String mailUsers = objDal.RecupMailUsersComparedWithToken(tokenUsers);

                if (mailUsers != null)
                {
                    bool isOk = false;
                    var newPasswordHash = SecurePassword.Hash(newPassword);
                    isOk = objDal.PasswordChanged(mailUsers, newPasswordHash);

                    if (isOk == true)
                    {
                        Alert.Show("Mot de passe changé avec succès !");
                        Response.Redirect("~/Connexion");
                    }
                    else
                    {
                        Alert.Show("Impossible de modifier le mot de passe.");
                    }
                }
                else
                {
                    Alert.Show("Impossible de changer le mot de passe. Veuillez contacter un Administrateur.");
                }

            }
            else
            {
                Alert.Show("Les deux mot de passe doivent-être identiques");
            }
        }

        //Récupéré le token de l'utilisateur pour changer le mot de passe
        public String QueryURL()
        {
            String str = Request.ServerVariables["HTTP_REFERER"];
            String splitFinal = null;
            string[] strSplit = null;

            strSplit = str.Split('?');
            splitFinal = strSplit[1];

            return splitFinal;
        }
    }
}
