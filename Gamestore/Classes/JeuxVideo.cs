using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;

namespace Gamestore.Classes
{
    public class JeuxVideo
    {
        public String title { get; set; }
        public String description { get; set; }
        public int pegi { get; set; }
        public String genre { get; set; }
        public int quantite { get; set; }
        public String urlImage { get; set; }
        public String editeur { get; set; }
        public String developpeur { get; set; }
        public float prix { get; set; }

        public JeuxVideo()
        {

        }
    }
}