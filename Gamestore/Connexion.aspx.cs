using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gamestore
{
    public partial class Connexion : System.Web.UI.Page
    {
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
            String mail;      //Mail de l'utilisateur
            String password;       //Password de l'utilisateur
            String roleUser = " "; //Roles de l'utilisateur (Visiteurs, Patient, Admin)
            String allergenes = " "; //Allergene de l'utilisateur

            bool isOk = false;        //True si l'authentification à réussis
        }
    }
}