using Gamestore.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gamestore
{
    public partial class SiteMaster : MasterPage
    {
        String nomUsers;
        String prenomUsers;
        DALGamestore objDal = new DALGamestore();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(Session["EstConnecte"]))
            {
                nomUsers = Session["Nom"].ToString();
                prenomUsers = Session["Prenom"].ToString();

                if (Session["RoleUtilisateur"].ToString() == "Administrateur")
                {
                    link_compte.HRef = "~/MonCompteAdministrateur";
                    ImgCart.Visible = false;

                }else if (Session["RoleUtilisateur"].ToString() == "Employé")
                {
                    link_compte.HRef = "~/MonCompteEmploye";
                    ImgCart.Visible = false;

                }
                else if (Session["RoleUtilisateur"].ToString() == "Utilisateur")
                {
                    //Récupération de l'email de l'utilisateur pour avoir l'ID Client
                    String emailUsers = Convert.ToString(Session["MailUtilisateur"]);
                    int idClient = objDal.RecupClientID(emailUsers);

                    //Récupération du contenu du panier avec l'idClient
                    List<String> listCart = new List<String>();
                    listCart = objDal.RécupCart(idClient);

                    //Si le panier est vide, le symbole ne s'affiche pas, sinon oui
                    if (listCart.Count <= 0)
                    {
                        ImgCart.Visible = false;
                    }
                    else 
                    {
                        ImgCart.Visible = true;
                    }

                    link_compte.HRef = "~/MonCompteUtilisateur";
                }
            }
        }
    }
}