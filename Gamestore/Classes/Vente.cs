using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gamestore.Classes
{
    public class Vente
    {
        public string Id { get; set; }
        public string Mois { get; set; }
        public string DateVente { get; set; }
        public string NomArticle { get; set; }
        public int NombreVentes { get; set; }
        public decimal PrixTotal { get; set; }
    }
}