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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(Session["EstConnecte"]))
            {
                nomUsers = Session["Nom"].ToString();
                prenomUsers = Session["Prenom"].ToString();
            }
        }

        protected void BtnDeconnexion_Click(object sender, EventArgs e)
        {
            Session["EstConnecte"] = false;
            Session["LoginUtilisateur"] = "";
            Session["PassUtilisateur"] = "";
            Session["MailUtilisateur"] = "";
            Session["Nom"] = "";
            Session["Prenom"] = "";
            Session["RoleUtilisateur"] = "";
            Response.Redirect(Request.RawUrl);
        }
    }
}