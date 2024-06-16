using Gamestore.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gamestore
{
    public partial class DetailJeuxVideo : System.Web.UI.Page
    {
        DALGamestore objDal = new DALGamestore();
        protected void Page_Load(object sender, EventArgs e)
        {
            DDLPlateforme.Items.Add("PC");
            DDLPlateforme.Items.Add("XBOX");

            //On affiche toutes les informations retenues dans la BDD
            Classes.JeuxVideo objJeuxVideo = new Classes.JeuxVideo();
            objJeuxVideo = objDal.RécupJeuxVideo("Age of Empire IV");

            IMGGame.ImageUrl = objJeuxVideo.urlImage;
            LblTitleGame.Text = objJeuxVideo.title;
            LblStock.Text = objJeuxVideo.quantite + " " + "en Stock";
            LblPEGI.Text = "PEGI" + " " + objJeuxVideo.pegi;
            LblGenre.Text = objJeuxVideo.genre;
            LblPrice.Text = objJeuxVideo.prix + "€";

            if (objJeuxVideo.quantite == 0)
            {
                bi_check.Visible = false;
                LblStock.Visible = false;
                bi_cross.Visible = true;
                LblStockVide.Text = objJeuxVideo.quantite + " " + "en Stock";
            }
            else
            {
                bi_check.Visible = true;
                LblStock.Visible = true;
                bi_cross.Visible = false;
                LblStockVide.Visible = false;
            }

            //On Split la description des jeux par rapport au fin de ligne.
            List<String> DescriptionJeuxVideoSplit = new List<string>(objJeuxVideo.description.Split('.'));
            LiteralControl l = new LiteralControl("<br/>");
            String test = ".";
            int i = 0;

            foreach(var desc in DescriptionJeuxVideoSplit)
            {
                BulletListDescription.Items.Add(desc + ".");
                i++;
            }

            
            //On supprime la dernière ligne qui ne contient aucun caractère (Split au .)
            BulletListDescription.Items.RemoveAt(i-1);
        }
    }
}