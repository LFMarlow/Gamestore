using MongoDB.Driver;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using Gamestore.Classes;
using System.Collections;
using MongoDB.Driver.Core.Configuration;
using System.Globalization;
using MySqlX.XDevAPI.Common;

namespace Gamestore
{
    public class DALMongo
    {
        private IMongoCollection<Vente> _collection;

        public static IMongoDatabase GetDatabase()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ChaineMongoDb"].ConnectionString;

            var client = new MongoClient(connectionString);
            return client.GetDatabase("Gamestore");
        }

        public Dictionary<string, Dictionary<string, int>> GetSalesDataByItem()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ChaineMongoDb"].ConnectionString;
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Gamestore");
            var collection = database.GetCollection<BsonDocument>("video_games");

            // Définir les limites du mois en cours
            var now = DateTime.Now;
            var firstDayOfMonth = new DateTime(now.Year, now.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            var pipeline = new BsonDocument[]
            {
                new BsonDocument("$match", new BsonDocument
                {
                    { "date", new BsonDocument
                        {
                            { "$gte", firstDayOfMonth },
                            { "$lte", lastDayOfMonth }
                        }
                    }
                }),

                new BsonDocument("$group",
                    new BsonDocument
                    {
                        { "_id", new BsonDocument
                                {
                                    { "name", "$name" },
                                    { "date", new BsonDocument("$dateToString", new BsonDocument
                                        {
                                            { "format", "%Y-%m-%d" },
                                            { "date", "$date" }
                                        })
                                    }
                                }
                        },
                        { "nombre_ventes", new BsonDocument("$sum", 1) }
                    }),

                new BsonDocument("$sort", new BsonDocument
                {
                    { "_id.name", 1 },
                    { "_id.date", 1 }
                })
            };

            var results = collection.Aggregate<BsonDocument>(pipeline).ToList();

            Dictionary<string, Dictionary<string, int>> salesData = new Dictionary<string, Dictionary<string, int>>();
            foreach (var result in results)
            {
                string gameName = result["_id"]["name"].AsString;
                string saleDate = result["_id"]["date"].AsString;
                int salesCount = result["nombre_ventes"].AsInt32;

                if (!salesData.ContainsKey(gameName))
                {
                    salesData[gameName] = new Dictionary<string, int>();
                }

                salesData[gameName][saleDate] = salesCount;
            }

            return salesData;
        }

        //Récupération de toutes les informations dans la Base MongoDB pour Page "DétailsChart"
        public List<Vente> GetSalesSummary()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ChaineMongoDb"].ConnectionString;
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Gamestore");
            var collection = database.GetCollection<BsonDocument>("video_games");

            // Définir les limites du mois en cours
            var now = DateTime.Now;
            var firstDayOfMonth = new DateTime(now.Year, now.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            var filter = Builders<BsonDocument>.Filter.And(
                Builders<BsonDocument>.Filter.Gte("date", firstDayOfMonth),
                Builders<BsonDocument>.Filter.Lte("date", lastDayOfMonth)
            );

            //Configure un pipeline d'agrégation qui filtre les données, les groupe par date et nom, 
            //calcule le nombre total des ventes et le prix total pour chaque groupe, puis trie les résultats
            var aggregate = collection.Aggregate()
                .Match(filter)
                .Group(new BsonDocument
                {
                    //Configuration de l'identifiant de groupe utilisant date et nom
                    { "_id", new BsonDocument
                        {
                            //Conversion de la date du document en format spécifié
                            { "date", new BsonDocument("$dateToString", new BsonDocument { { "format", "%d-%m-%Y" }, { "date", "$date" } }) },
                            { "name", "$name" }
                        }
                    },
                    //Calcul du nombre total de ventes pour le groupe
                    { "nombre_ventes", new BsonDocument("$sum", 1) },

                    //Calcul du prix total des ventes pour le groupe en sommant les prix
                    { "prix_total", new BsonDocument("$sum", "$price") }
                })

                //Tri des résultats par date et par nom pour une présentation ordonnée
                .Sort(new BsonDocument { { "_id.date", 1 }, { "_id.name", 1 } })

                //Conversion du résultat en liste pour le traitement ultérieur
                .ToList();

            //Initialisation de la liste des résultats à retourner
            List<Vente> result = new List<Vente>();

            foreach (var doc in aggregate)
            {
                decimal prixTotal = 0m;
                if (doc["prix_total"].IsDouble)
                    prixTotal = (decimal)doc["prix_total"].AsDouble;
                else if (doc["prix_total"].IsInt32)
                    prixTotal = doc["prix_total"].AsInt32;
                else if (doc["prix_total"].IsInt64)
                    prixTotal = doc["prix_total"].AsInt64;

                result.Add(new Vente
                {
                    DateVente = doc["_id"]["date"].AsString,
                    NomArticle = doc["_id"]["name"].AsString,
                    NombreVentes = doc["nombre_ventes"].AsInt32,
                    PrixTotal = prixTotal
                });
            }

            return result;
        }

        //Récupération des dates dans la Base MongoDB
        public List<String> GetDistinctSaleDates(int year, String month)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ChaineMongoDb"].ConnectionString;
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Gamestore");
            var collection = database.GetCollection<BsonDocument>("video_games");

            // Convertir le nom du mois en numéro du mois
            int numberMonth = DateTime.ParseExact(month, "MMMM", CultureInfo.CurrentCulture).Month;

            // Définir les limites du mois sélectionné
            var firstDayOfMonth = new DateTime(year, numberMonth, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            var filter = Builders<BsonDocument>.Filter.And(
                Builders<BsonDocument>.Filter.Gte("date", firstDayOfMonth),
                Builders<BsonDocument>.Filter.Lte("date", lastDayOfMonth)
            );

            // Récupération des dates distinctes directement en tant que DateTime
            var dates = collection.Distinct<DateTime>("date", filter).ToList();

            // Conversion en HashSet pour éliminer tout doublon potentiel
            var dateSet = new HashSet<String>(dates.Select(d => d.ToString("dd-MM-yyyy")));

            // Conversion du HashSet en List et tri
            var formattedDates = dateSet.ToList();
            formattedDates.Sort();

            return formattedDates;
        }

        //Récupération des jeux dans la Base MongoDB
        public List<String> GetDistinctSaleGames(int year, String month)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ChaineMongoDb"].ConnectionString;
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Gamestore");
            var collection = database.GetCollection<BsonDocument>("video_games");

            // Convertir le nom du mois en numéro du mois
            int numberMonth = DateTime.ParseExact(month, "MMMM", CultureInfo.CurrentCulture).Month;

            // Définir les limites du mois sélectionné
            var firstDayOfMonth = new DateTime(year, numberMonth, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            var filter = Builders<BsonDocument>.Filter.And(
                Builders<BsonDocument>.Filter.Gte("date", firstDayOfMonth),
                Builders<BsonDocument>.Filter.Lte("date", lastDayOfMonth)
            );

            // Récupération des jeux distinctes directement en tant que String
            var games = collection.Distinct<String>("name", filter).ToList();

            // Conversion en HashSet pour éliminer tout doublon potentiel
            var gamesSet = new HashSet<String>(games.Select(d => d));

            // Conversion du HashSet en List et tri
            var formattedGames = gamesSet.ToList();
            formattedGames.Sort();

            return formattedGames;
        }

        //Récupération de données en fonction d'une date choisie
        public List<Vente> GetSalesSummaryByDate(String selectedDate)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ChaineMongoDb"].ConnectionString;
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Gamestore");
            var collection = database.GetCollection<BsonDocument>("video_games");

            //Parse la date sélectionnée du format string en DateTime, et définit le début du jour
            var startOfDay = DateTime.ParseExact(selectedDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            //Calcule la fin de la journée pour capturer toute activité de cette date
            var endOfDay = startOfDay.AddDays(1).AddTicks(-1);

            //Crée un filtre pour récupérer les documents où la date est entre le début et la fin du jour spécifié
            var filter = Builders<BsonDocument>.Filter.Gte("date", startOfDay) & Builders<BsonDocument>.Filter.Lte("date", endOfDay);

            //Configure un pipeline d'agrégation qui filtre les données, les groupe par date et nom, 
            //calcule le nombre total des ventes et le prix total pour chaque groupe, puis trie les résultats
            var aggregate = collection.Aggregate()
                .Match(filter)
                .Group(new BsonDocument
                {
                    //Configuration de l'identifiant de groupe utilisant date et nom
                    { "_id", new BsonDocument
                        {
                            //Conversion de la date du document en format spécifié
                            { "date", new BsonDocument("$dateToString", new BsonDocument { { "format", "%d-%m-%Y" }, { "date", "$date" } }) },
                            { "name", "$name" }
                        }
                    },
                    { "nombre_ventes", new BsonDocument("$sum", 1) },
                    { "prix_total", new BsonDocument("$sum", "$price") }
                })
                .Sort(new BsonDocument { { "_id.date", 1 }, { "_id.name", 1 } })
                .ToList();

            //Initialisation de la liste des résultats à retourner
            List<Vente> result = new List<Vente>();


            foreach (var doc in aggregate)
            {
                decimal prixTotal = 0m;

                if (doc["prix_total"].IsDouble)
                {
                    prixTotal = (decimal)doc["prix_total"].AsDouble;
                }
                else if (doc["prix_total"].IsInt32)
                {
                    prixTotal = doc["prix_total"].AsInt32;
                }
                else if (doc["prix_total"].IsInt64)
                {
                    prixTotal = doc["prix_total"].AsInt64;
                }

                result.Add(new Vente
                {
                    DateVente = doc["_id"]["date"].AsString,
                    NomArticle = doc["_id"]["name"].AsString,
                    NombreVentes = doc["nombre_ventes"].AsInt32,
                    PrixTotal = prixTotal
                });
            }

            return result;
        }

        //Récupération de données en fonction d'un jeu choisi
        public List<Vente> GetSalesSummaryByGames(int year, String month, String selectedGame)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ChaineMongoDb"].ConnectionString;
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Gamestore");
            var collection = database.GetCollection<BsonDocument>("video_games");

            // Convertir le nom du mois en numéro du mois
            int monthNumber = DateTime.ParseExact(month, "MMMM", CultureInfo.CurrentCulture).Month;

            // Définir les limites du mois sélectionné
            var firstDayOfMonth = new DateTime(year, monthNumber, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            // Crée un filtre pour récupérer les documents où la date est entre le début et la fin du mois spécifié et le nom du jeu est celui sélectionné
            var filter = Builders<BsonDocument>.Filter.And(
                Builders<BsonDocument>.Filter.Gte("date", firstDayOfMonth),
                Builders<BsonDocument>.Filter.Lte("date", lastDayOfMonth),
                Builders<BsonDocument>.Filter.Eq("name", selectedGame)
            );

            // Configure un pipeline d'agrégation qui filtre les données, les groupe par date et nom, calcule le nombre total des ventes et le prix total pour chaque groupe, puis trie les résultats
            var aggregate = collection.Aggregate()
                .Match(filter)
                .Group(new BsonDocument
                {
                { "_id", new BsonDocument
                    {
                        { "date", new BsonDocument("$dateToString", new BsonDocument { { "format", "%d-%m-%Y" }, { "date", "$date" } }) },
                        { "name", "$name" }
                    }
                },
                { "nombre_ventes", new BsonDocument("$sum", 1) },
                { "prix_total", new BsonDocument("$sum", "$price") }
                })
                .Sort(new BsonDocument { { "_id.date", 1 }, { "_id.name", 1 } })
                .ToList();

            // Initialisation de la liste des résultats à retourner
            List<Vente> result = new List<Vente>();
            foreach (var doc in aggregate)
            {
                decimal prixTotal = 0m;
                if (doc["prix_total"].IsDouble)
                    prixTotal = (decimal)doc["prix_total"].AsDouble;
                else if (doc["prix_total"].IsInt32)
                    prixTotal = doc["prix_total"].AsInt32;
                else if (doc["prix_total"].IsInt64)
                    prixTotal = doc["prix_total"].AsInt64;

                result.Add(new Vente
                {
                    DateVente = doc["_id"]["date"].AsString,
                    NomArticle = doc["_id"]["name"].AsString,
                    NombreVentes = doc["nombre_ventes"].AsInt32,
                    PrixTotal = prixTotal
                });
            }

            return result;
        }

        //Récupération des mois + année contenue dans la base
        public List<string> GetDistinctSaleMonths()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ChaineMongoDb"].ConnectionString;
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Gamestore");
            var collection = database.GetCollection<BsonDocument>("video_games");

            var pipeline = new BsonDocument[]
            {
                new BsonDocument("$project", new BsonDocument
                    {
                        { "month", new BsonDocument("$month", "$date") },
                        { "year", new BsonDocument("$year", "$date") }
                    }),

                new BsonDocument("$group", new BsonDocument
                    {
                        { "_id", new BsonDocument
                            {
                                { "month", "$month" },
                                { "year", "$year" }
                            }
                        }
                    }),

                new BsonDocument("$sort", new BsonDocument
                    {
                        { "_id.year", 1 },
                        { "_id.month", 1 }
                    })
            };

            var results = collection.Aggregate<BsonDocument>(pipeline).ToList();
            List<string> months = new List<string>();
            foreach (var result in results)
            {
                int year = result["_id"]["year"].AsInt32;
                int month = result["_id"]["month"].AsInt32;

                // Crée une chaîne représentant le mois et l'année (par exemple, "Juillet 2024")
                months.Add(new DateTime(year, month, 1).ToString("MMMM yyyy").ToUpper());
            }

            return months;
        }

        public List<Vente> GetSalesSummaryByMonth(int year, int month)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ChaineMongoDb"].ConnectionString;
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Gamestore");
            var collection = database.GetCollection<BsonDocument>("video_games");

            // Définir les limites du mois sélectionné
            var firstDayOfMonth = new DateTime(year, month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            var filter = Builders<BsonDocument>.Filter.And(
                Builders<BsonDocument>.Filter.Gte("date", firstDayOfMonth),
                Builders<BsonDocument>.Filter.Lte("date", lastDayOfMonth)
            );

            var aggregate = collection.Aggregate()
                .Match(filter)
                .Group(new BsonDocument
                {
                    { "_id", new BsonDocument
                        {
                            { "date", new BsonDocument("$dateToString", new BsonDocument { { "format", "%d-%m-%Y" }, { "date", "$date" } }) },
                            { "name", "$name" }
                        }
                    },
                    { "nombre_ventes", new BsonDocument("$sum", 1) },
                    { "prix_total", new BsonDocument("$sum", "$price") }
                })
                .Sort(new BsonDocument { { "_id.date", 1 }, { "_id.name", 1 } })
                .ToList();

            List<Vente> result = new List<Vente>();
            foreach (var doc in aggregate)
            {
                result.Add(new Vente
                {
                    DateVente = doc["_id"]["date"].AsString,
                    NomArticle = doc["_id"]["name"].AsString,
                    NombreVentes = doc["nombre_ventes"].AsInt32,
                    PrixTotal = Convert.ToDecimal(doc["prix_total"])
                });
            }

            return result;
        }

        public Dictionary<string, Tuple<Vente, string>> GetSalesComparison(string selectedMonthYear)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ChaineMongoDb"].ConnectionString;
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("Gamestore");
            var collection = database.GetCollection<BsonDocument>("video_games");

            if (!DateTime.TryParseExact(selectedMonthYear, "MMMM yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime selectedMonth))
            {
                return new Dictionary<string, Tuple<Vente, string>>();
            }

            DateTime previousMonth = selectedMonth.AddMonths(-1);

            List<Vente> GetSalesDataForMonth(DateTime month)
            {
                var firstDayOfMonth = new DateTime(month.Year, month.Month, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                var filter = Builders<BsonDocument>.Filter.And(
                    Builders<BsonDocument>.Filter.Gte("date", firstDayOfMonth),
                    Builders<BsonDocument>.Filter.Lte("date", lastDayOfMonth)
                );

                var aggregation = collection.Aggregate()
                    .Match(filter)
                    .Group(new BsonDocument
                    {
                { "_id", new BsonDocument("$dateToString", new BsonDocument { { "format", "%m" }, { "date", "$date" } }) },
                { "nombre_ventes", new BsonDocument("$sum", 1) },
                { "prix_total", new BsonDocument("$sum", "$price") }
                    })
                    .Project(new BsonDocument
                    {
                { "Mois", "$_id" },
                { "Nombre Total de Ventes", "$nombre_ventes" },
                { "Prix Total des Ventes", "$prix_total" }
                    })
                    .ToList();

                return aggregation.Select(a => new Vente
                {
                    Mois = FormatMonth(a["Mois"].AsString),
                    NombreVentes = a["Nombre Total de Ventes"].AsInt32,
                    PrixTotal = Convert.ToDecimal(a["Prix Total des Ventes"]) 
                }).ToList();
            }

            var salesThisMonth = GetSalesDataForMonth(selectedMonth);
            var salesLastMonth = GetSalesDataForMonth(previousMonth);

            //Fonction pour déterminer la classe CSS en fonction de la comparaison des valeurs
            string DetermineStyle(int current, int? previous)
            {
                if (previous == null)
                {
                    return ""; //Pas d'enregistrement pour le mois précédent
                }

                return current > previous ? "text-green" : current < previous ? "text-red" : "";
            }

            
            Vente lastMonthFirst = salesLastMonth.FirstOrDefault();
            int lastMonthSales = lastMonthFirst != null ? lastMonthFirst.NombreVentes : 0;

            var result = new Dictionary<string, Tuple<Vente, string>>();
            result.Add("CurrentMonth", new Tuple<Vente, string>(
                salesThisMonth.FirstOrDefault(),
                DetermineStyle(salesThisMonth.FirstOrDefault()?.NombreVentes ?? 0, lastMonthSales)
            ));

            if (lastMonthFirst != null)
            {
                result.Add("PreviousMonth", new Tuple<Vente, string>(lastMonthFirst, ""));
            }

            return result;
        }

        public string FormatMonth(string month)
        {
            var date = DateTime.ParseExact(month, "MM", CultureInfo.InvariantCulture);

            var formattedDate = date.ToString("MMMM", CultureInfo.CurrentCulture);

            if (formattedDate.Length > 1)
            {
                formattedDate = char.ToUpper(formattedDate[0]) + formattedDate.Substring(1);
            }

            return formattedDate;
        }
    }
}