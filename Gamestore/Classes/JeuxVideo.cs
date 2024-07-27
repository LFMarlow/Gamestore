using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;

namespace Gamestore.Classes
{
    public class JeuxVideo
    {
        public String title;
        public String description;
        public int pegi;
        public String genre;
        public int quantite;
        public String urlImage;
        public String editeur;
        public String developpeur;
        public float prix;

        public JeuxVideo()
        {

        }

        public JeuxVideo(string prmTitle, string prmDescription, int prmPegi, string prmGenre, int prmQuantite, string prmURL, string prmEditeur, string prmDeveloppeur, float prmPrix)
        {
            title = prmTitle;
            description = prmDescription;
            pegi = prmPegi;
            genre = prmGenre;
            quantite = prmQuantite;
            urlImage = prmURL;
            editeur = prmEditeur;
            developpeur = prmDeveloppeur;
            prix = prmPrix;
        }

        public JeuxVideo(string prmTitle, string prmDescription, int prmPegi, string prmGenre, int prmQuantite, string prmURL, float prmPrix)
        {
            title = prmTitle;
            description = prmDescription;
            pegi = prmPegi;
            genre = prmGenre;
            quantite = prmQuantite;
            urlImage = prmURL;
            prix = prmPrix;
        }
    }
}