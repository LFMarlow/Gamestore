using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Gamestore.Classes
{
    public class GeolocationHub : Hub
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ChaineBdd"].ConnectionString;

        public string GetNearestStore(double latitude, double longitude)
        {
            GeolocationHandler geolocationHandler = new GeolocationHandler();
            Store nearestStore = geolocationHandler.GetNearestStore(latitude, longitude);
            return nearestStore.StoreID.ToString();
        }
    }
}