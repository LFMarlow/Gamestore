using MySqlX.XDevAPI;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;


namespace Gamestore
{
    /// <summary>
    /// Description résumée de ProcessLocation
    /// </summary>
    public class ProcessLocation : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        //Récupération de la latitude et longitude de l'utilisateur et stockage dans 2 variables de sessions
        public void ProcessRequest(HttpContext context)
        {
            string jsonContent;
            using (var reader = new StreamReader(context.Request.InputStream))
            {
                jsonContent = reader.ReadToEnd();
            }

            JObject data = JObject.Parse(jsonContent);
            double latitude = (double)data["latitude"];
            double longitude = (double)data["longitude"];

            context.Session["latitude"] = latitude;
            context.Session["longitude"] = longitude;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}