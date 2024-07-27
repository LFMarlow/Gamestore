using Gamestore.Classes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Gamestore
{
    /// <summary>
    /// Description résumée de GeolocationHandler
    /// </summary>
    public class GeolocationHandler : IHttpHandler
    {
        DALGamestore objDal = new DALGamestore();
        private string connectionString = ConfigurationManager.ConnectionStrings["ChaineBdd"].ConnectionString;

        public void ProcessRequest(HttpContext context)
        {
            string latitude = context.Request.Form["latitude"];
            string longitude = context.Request.Form["longitude"];

            if (double.TryParse(latitude, out double userLat) && double.TryParse(longitude, out double userLon))
            {
                Store nearestStore = GetNearestStore(userLat, userLon);
                context.Response.ContentType = "text/plain";
                context.Response.Write(nearestStore.StoreID);
            }
        }

        //Calcul pour récupéré le magasin le plus proche de l'utilisateur
        private Store GetNearestStore(double userLat, double userLon)
        {
            List<Store> stores = GetStores();
            Store nearestStore = null;
            double shortestDistance = double.MaxValue;

            foreach (var store in stores)
            {
                double distance = GetDistance(userLat, userLon, store.Latitude, store.Longitude);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    nearestStore = store;
                }
            }
            return nearestStore;
        }

        private double GetDistance(double lat1, double lon1, double lat2, double lon2)
        {
            var R = 6371e3; // Earth radius in meters
            var φ1 = lat1 * Math.PI / 180;
            var φ2 = lat2 * Math.PI / 180;
            var Δφ = (lat2 - lat1) * Math.PI / 180;
            var Δλ = (lon2 - lon1) * Math.PI / 180;

            var a = Math.Sin(Δφ / 2) * Math.Sin(Δφ / 2) +
                    Math.Cos(φ1) * Math.Cos(φ2) *
                    Math.Sin(Δλ / 2) * Math.Sin(Δλ / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            var d = R * c;
            return d; // Distance in meters
        }

        public bool IsReusable => false;

        public List<Store> GetStores()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT id, Name, Latitude, Longitude FROM magasins";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                MySqlDataReader reader = cmd.ExecuteReader();
                List<Store> stores = new List<Store>();

                while (reader.Read())
                {
                    Store store = new Store
                    {
                        StoreID = reader.GetInt32("id"),
                        StoreName = reader.GetString("Name"),
                        Latitude = reader.GetDouble("Latitude"),
                        Longitude = reader.GetDouble("Longitude")
                    };
                    stores.Add(store);
                }
                return stores;
            }
        }

        public Store NearestStore(double userLat, double userLon, List<Store> magasins)
        {
            Store magasinProche = null;
            double minDistance = double.MaxValue;

            foreach (var magasin in magasins)
            {
                double distance = Math.Sqrt(Math.Pow(magasin.Latitude - userLat, 2) + Math.Pow(magasin.Longitude - userLon, 2));
                if (distance < minDistance)
                {
                    minDistance = distance;
                    magasinProche = magasin;
                }
            }

            return magasinProche;
        }
    }

    

    public class Store
    {
        public int StoreID { get; set; }
        public string StoreName { get; set; }
        public string Address { get; set; }
        public string Ville { get; set; }
        public int PostalCode { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
