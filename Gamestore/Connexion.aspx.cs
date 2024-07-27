using Gamestore.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gamestore
{
    public partial class Connexion : System.Web.UI.Page
    {

        WBServiceGamestore objProxy = new WBServiceGamestore();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(Session["EstConnecte"]) == true)
            {
                Response.Redirect("~/", false);
            }
        }
        
        protected void BtnConnexion_Click(object sender, EventArgs e)
        {

            String nom = " ";  //Nom de l'utilisateur
            String prenom = " ";   //Prenom de l'utilisateur
            String mail = " ";      //Mail de l'utilisateur
            String password = " ";       //Password de l'utilisateur
            String passwordHach = " "; //Password Hacher de l'utilisateur
            String roleUser = " "; //Roles de l'utilisateur (Utilisateur, Employé, Administrateur)
            String postalAdress = " "; //Adresse Postale de l'utilisateur

            bool isOk = false;        //True si l'authentification à réussi

            mail = TxtBoxMailConnexion.Text;
            password = TxtBoxMDPConnexion.Text;

            if (!String.IsNullOrWhiteSpace(mail) && (!String.IsNullOrWhiteSpace(password)))
            {

                passwordHach = objProxy.RecupPasswordHash(mail);

                if (passwordHach != null)
                {
                    //Vérification entre le mot de passe entré par l'utilisateur et le hachage
                    var resultPassword = SecurePassword.Verify(password, passwordHach);

                    //Si le hachage est correct
                    if (resultPassword == true)
                    {
                        //Authentification de l'utilisateur
                        isOk = objProxy.Authentification(ref mail, ref passwordHach, ref nom, ref prenom, ref roleUser, ref postalAdress);

                        //Si l'authentification à réussi, on enregistre les informations
                        if (isOk)
                        {
                            Session["EstConnecte"] = true;
                            Session["MailUtilisateur"] = mail;
                            Session["Nom"] = nom;
                            Session["Prenom"] = prenom;
                            Session["RoleUtilisateur"] = roleUser;
                            Session["PostalAdress"] = postalAdress;
                            Response.Redirect("~/", false);
                        }
                        else
                        {
                            Alert.Show("E-mail ou mot de passe incorrect");
                        }
                    }
                    else
                    {
                        Alert.Show("Mot de Passe incorrect");
                    }
                }
                else
                {
                    Alert.Show("Adresse Email incorrect.");
                }


            }
            else
            {
                Alert.Show("Connexion Impossible");
            }
        }
    }
}