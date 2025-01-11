using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web.Caching;
using System.Web;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;

namespace Gamestore.Classes
{
    public class DALGamestore
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ChaineBdd"].ConnectionString;

        MySqlConnection connexion;
        MySqlCommand command;
        MySqlDataReader reader;

        public DALGamestore()
        {
            connexion = new MySqlConnection(connectionString);
        }

        //Method de connexion à la BDD
        private bool Connecter()
        {
            bool isConnected = false;
            try
            {
                connexion.Open();
                if (connexion.State == System.Data.ConnectionState.Open)
                {
                    isConnected = true;
                }
            }
            catch (Exception ex)
            {
                isConnected = false;
            }
            return isConnected;
        }

        //Deconnexion de la BDD
        private void Deconnecter()
        {
            if ((reader != null) && (reader.IsClosed != true))
            {
                reader.Close();
            }
            connexion.Close();
        }

        //Vérification si adresse mail déjà renseigné
        public Classes.Users VerifDoublonMailInscription(string prmMail)
        {
            string verifDoublonMail = "SELECT email FROM users WHERE email = @Email";

            Classes.Users objUsers = null;
            bool isConnected = false;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    using (MySqlCommand command = new MySqlCommand(verifDoublonMail, connexion))
                    {
                        command.Parameters.AddWithValue("@Email", prmMail);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                objUsers = new Classes.Users();
                                objUsers.email = Convert.ToString(reader["email"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                objUsers = null;
            }
            finally
            {
                Deconnecter();
            }
            return objUsers;
        }

        //Inscription de l'utilisateur dans la BDD
        public bool Inscription(string prmNom, string prmPrenom, string prmMail, string prmPassword, string prmRolesUsers, string prmPostalAdress, string prmTokenUsers)
        {
            bool estInscrit = false;
            string usersInscription = "INSERT INTO users (Nom, Prenom, email, password, postal_adress, role_users, token_users) VALUES (@Nom, @Prenom, @Email, @Password, @PostalAdress, @RolesUsers, @TokenUsers)";

            bool isConnected = false;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    using (MySqlCommand command = new MySqlCommand(usersInscription, connexion))
                    {
                        command.Parameters.AddWithValue("@Nom", prmNom);
                        command.Parameters.AddWithValue("@Prenom", prmPrenom);
                        command.Parameters.AddWithValue("@Email", prmMail);
                        command.Parameters.AddWithValue("@Password", prmPassword);
                        command.Parameters.AddWithValue("@PostalAdress", prmPostalAdress);
                        command.Parameters.AddWithValue("@RolesUsers", prmRolesUsers);
                        command.Parameters.AddWithValue("@TokenUsers", prmTokenUsers);

                        command.ExecuteNonQuery();
                        estInscrit = true;
                    }
                }
            }
            catch (InvalidOperationException)
            {
                isConnected = false;
            }
            finally
            {
                Deconnecter();
            }
            return estInscrit;
        }

        //Authentification de l'utilisateur sur l'application
        public Classes.Users AuthentificationDAL(String prmEmail, String prmPassword)
        {
            String requete = "SELECT * FROM users WHERE email = @Email AND password = @Password";
            bool isConnected = false;
            Classes.Users objUsers = null;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    using (MySqlCommand command = new MySqlCommand(requete, connexion))
                    {
                        command.Parameters.AddWithValue("@Email", prmEmail);
                        command.Parameters.AddWithValue("@Password", prmPassword);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                objUsers = new Classes.Users
                                {
                                    nom = Convert.ToString(reader["Nom"]),
                                    prenom = Convert.ToString(reader["Prenom"]),
                                    email = Convert.ToString(reader["email"]),
                                    password = Convert.ToString(reader["password"]),
                                    roleUsers = Convert.ToString(reader["role_users"]),
                                    postalAdress = Convert.ToString(reader["postal_adress"])
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                objUsers = null;
            }
            finally
            {
                Deconnecter();
            }
            return objUsers;
        }

        //Récupération du mot de passe de l'utilisateur via son adress mail pour comparer le hachage
        public Classes.Users AuthentificationEmail(String prmEmail)
        {
            String requete = "SELECT password FROM users WHERE email = @Email";
            bool isConnected = false;
            Classes.Users objUsers = null;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    using (MySqlCommand command = new MySqlCommand(requete, connexion))
                    {
                        command.Parameters.AddWithValue("@Email", prmEmail);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                objUsers = new Classes.Users
                                {
                                    password = Convert.ToString(reader["password"])
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                objUsers = null;
            }
            finally
            {
                Deconnecter();
            }
            return objUsers;
        }

        //Récupére le token de l'utilisateur par rapport à l'adresse mail pour changement password
        public String RecupTokenUsers(String prmEmail)
        {
            String requete = "SELECT token_users FROM users WHERE email = @Email";
            bool isConnected = false;
            String tokenUsers = null;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    using (MySqlCommand command = new MySqlCommand(requete, connexion))
                    {
                        command.Parameters.AddWithValue("@Email", prmEmail);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                tokenUsers = Convert.ToString(reader["token_users"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                tokenUsers = null;
            }
            finally
            {
                Deconnecter();
            }
            return tokenUsers;
        }

        //Récupére le Mail de l'utilisateur par rapport au Token pour changement password
        public String RecupMailUsersComparedWithToken(String prmTokenUsers)
        {
            String requete = "SELECT email FROM users WHERE token_users = @TokenUsers";
            bool isConnected = false;
            String mailUsers = null;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    using (MySqlCommand command = new MySqlCommand(requete, connexion))
                    {
                        command.Parameters.AddWithValue("@TokenUsers", prmTokenUsers);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                mailUsers = Convert.ToString(reader["email"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                mailUsers = null;
            }
            finally
            {
                Deconnecter();
            }
            return mailUsers;
        }

        //Changement du mot de passe de l'utilisateur
        public bool PasswordChanged(String prmEmailUsers, String prmNewPassword)
        {
            String requete = "UPDATE users SET password = @NewPassword WHERE email = @Email";
            bool isConnected = false;
            bool isOk = false;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    using (MySqlCommand command = new MySqlCommand(requete, connexion))
                    {
                        command.Parameters.AddWithValue("@NewPassword", prmNewPassword);
                        command.Parameters.AddWithValue("@Email", prmEmailUsers);

                        command.ExecuteNonQuery();
                        isOk = true;
                    }
                }
            }
            catch (Exception ex)
            {
                isConnected = false;
                isOk = false;
            }
            finally
            {
                Deconnecter();
            }
            return isOk;
        }

        //Inscription des employé par l'administrateur
        public bool InscriptionEmploye(string prmNom, string prmPrenom, string prmMail, string prmPassword, string prmRolesUsers, string prmTokenUsers)
        {
            bool estInscrit = false;
            string usersInscription = "INSERT INTO users (Nom, Prenom, email, password, role_users, token_users) VALUES (@Nom, @Prenom, @Email, @Password, @RolesUsers, @TokenUsers)";

            bool isConnected = false;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    using (MySqlCommand command = new MySqlCommand(usersInscription, connexion))
                    {
                        command.Parameters.AddWithValue("@Nom", prmNom);
                        command.Parameters.AddWithValue("@Prenom", prmPrenom);
                        command.Parameters.AddWithValue("@Email", prmMail);
                        command.Parameters.AddWithValue("@Password", prmPassword);
                        command.Parameters.AddWithValue("@RolesUsers", prmRolesUsers);
                        command.Parameters.AddWithValue("@TokenUsers", prmTokenUsers);

                        command.ExecuteNonQuery();
                        estInscrit = true;
                    }
                }
            }
            catch (InvalidOperationException)
            {
                isConnected = false;
            }
            finally
            {
                Deconnecter();
            }
            return estInscrit;
        }

        public int RecupIdGame()
        {
            string requete = "SELECT id_game FROM jeux_video ORDER BY ID DESC LIMIT 1;";
            bool isConnected = false;
            int idGame = 0;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    using (MySqlCommand command = new MySqlCommand(requete, connexion))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                idGame = Convert.ToInt32(reader["id_game"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                idGame = -1;
            }
            finally
            {
                Deconnecter();
            }
            return idGame;
        }

        //Création de jeux vidéo dans la bdd pour vente sans promotion
        public bool CreateGameWithoutPromotion(string prmImage, string prmTitre, string prmPrix, string prmPEGI, string prmQuantity, string prmGenre, string prmDescription, int prmIdGame)
        {
            bool estInscrit = false;
            string usersInscription = "INSERT INTO jeux_video (image, title, price, pegi, quantity, genre, description, id_game) VALUES (@Image, @Title, @Price, @Pegi, @Quantity, @Genre, @Description, @IdGame)";

            bool isConnected = false;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    using (MySqlCommand command = new MySqlCommand(usersInscription, connexion))
                    {
                        command.Parameters.AddWithValue("@Image", prmImage);
                        command.Parameters.AddWithValue("@Title", prmTitre.Replace("'", " "));
                        command.Parameters.AddWithValue("@Price", prmPrix);
                        command.Parameters.AddWithValue("@Pegi", prmPEGI);
                        command.Parameters.AddWithValue("@Quantity", prmQuantity);
                        command.Parameters.AddWithValue("@Genre", prmGenre);
                        command.Parameters.AddWithValue("@Description", prmDescription.Replace("'", "''"));
                        command.Parameters.AddWithValue("@IdGame", prmIdGame);

                        command.ExecuteNonQuery(); 
                        estInscrit = true;
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                isConnected = false;
                
            }
            finally
            {
                Deconnecter();
            }
            return estInscrit;
        }

        //Création de jeux vidéo dans la bdd pour vente avec promotion
        public bool CreateGameWithPromotion(string prmImage, string prmTitre, string prmPrix, string prmPEGI, string prmQuantity, string prmGenre, string prmDescription, int prmIdGame, string prmDiscount, string prmPriceDiscount)
        {
            bool estInscrit = false;
            string usersInscription = "INSERT INTO jeux_video (image, title, price, pegi, quantity, genre, description, discount, price_discount, id_game) VALUES (@Image, @Title, @Price, @Pegi, @Quantity, @Genre, @Description, @Discount, @PriceDiscount, @IdGame)";

            bool isConnected = false;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    using (MySqlCommand command = new MySqlCommand(usersInscription, connexion))
                    {
                        command.Parameters.AddWithValue("@Image", prmImage);
                        command.Parameters.AddWithValue("@Title", prmTitre.Replace("'", " "));
                        command.Parameters.AddWithValue("@Price", prmPrix);
                        command.Parameters.AddWithValue("@Pegi", prmPEGI);
                        command.Parameters.AddWithValue("@Quantity", prmQuantity);
                        command.Parameters.AddWithValue("@Genre", prmGenre);
                        command.Parameters.AddWithValue("@Description", prmDescription.Replace("'", "''"));
                        command.Parameters.AddWithValue("@Discount", prmDiscount);
                        command.Parameters.AddWithValue("@PriceDiscount", prmPriceDiscount.Replace(",", "."));
                        command.Parameters.AddWithValue("@IdGame", prmIdGame);

                        command.ExecuteNonQuery();
                        estInscrit = true;
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                isConnected = false;
            }
            finally
            {
                Deconnecter();
            }
            return estInscrit;
        }

        public Classes.JeuxVideo RécupJeuxVideo(string prmTitle)
        {
            string requete = "SELECT * FROM jeux_video WHERE title = @Title";

            bool isConnected = false;
            Classes.JeuxVideo objJeuxVideo = null;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    using (MySqlCommand command = new MySqlCommand(requete, connexion))
                    {
                        command.Parameters.AddWithValue("@Title", prmTitle);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                objJeuxVideo = new Classes.JeuxVideo
                                {
                                    urlImage = Convert.ToString(reader["image"]),
                                    prix = Convert.ToSingle(reader["price"]),
                                    description = Convert.ToString(reader["description"]),
                                    quantite = Convert.ToInt32(reader["quantity"]),
                                    pegi = Convert.ToInt32(reader["pegi"]),
                                    title = Convert.ToString(reader["title"]),
                                    genre = Convert.ToString(reader["genre"])
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                objJeuxVideo = null;
            }
            finally
            {
                Deconnecter();
            }
            return objJeuxVideo;
        }

        public List<String> RécupTitleJeuxVideo()
        {
            string requete = "SELECT DISTINCT title FROM jeux_video";
            bool isConnected = false;
            List<String> listTitleJeuxVideo = new List<string>();

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    using (MySqlCommand command = new MySqlCommand(requete, connexion))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listTitleJeuxVideo.Add(Convert.ToString(reader["title"]));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Gestion des erreurs
                listTitleJeuxVideo = null;
            }
            finally
            {
                Deconnecter();
            }
            return listTitleJeuxVideo;
        }

        public List<String> RécupTitleJeuxVideoWithoutPromotions()
        {
            string requete = "SELECT DISTINCT title FROM jeux_video WHERE discount IS NULL";
            bool isConnected = false;
            List<String> listTitleJeuxVideo = new List<string>();

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    using (MySqlCommand command = new MySqlCommand(requete, connexion))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listTitleJeuxVideo.Add(Convert.ToString(reader["title"]));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                listTitleJeuxVideo = null;
            }
            finally
            {
                Deconnecter();
            }
            return listTitleJeuxVideo;
        }

        public List<String> RécupTitleJeuxVideoWithPromotions()
        {
            string requete = "SELECT DISTINCT title FROM jeux_video WHERE discount IS NOT NULL";
            bool isConnected = false;
            List<String> listTitleJeuxVideo = new List<string>();

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    using (MySqlCommand command = new MySqlCommand(requete, connexion))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listTitleJeuxVideo.Add(Convert.ToString(reader["title"]));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                listTitleJeuxVideo = null;
            }
            finally
            {
                Deconnecter();
            }
            return listTitleJeuxVideo;
        }

        public bool UpdateQuantiteStock(int prmQuantite, String prmTitle)
        {
            String requete = "UPDATE jeux_video SET quantity = @Quantite WHERE title = @Title";
            bool isConnected = false;
            bool isOk = false;
            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    using (MySqlCommand command = new MySqlCommand(requete, connexion))
                    {
                        command.Parameters.AddWithValue("@Quantite", prmQuantite);
                        command.Parameters.AddWithValue("@Title", prmTitle);

                        command.ExecuteNonQuery();
                        isOk = true;
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                isConnected = false;
                isOk = false;
            }
            finally
            {
                Deconnecter();
            }
            return isOk;
        }

        public int RécupQuantiteJeuxVideo(string prmTitle)
        {
            string requete = "SELECT quantity FROM jeux_video WHERE title = @Title";
            bool isConnected = false;
            int quantiteJeuxVideo = 0;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    using (MySqlCommand command = new MySqlCommand(requete, connexion))
                    {
                        command.Parameters.AddWithValue("@Title", prmTitle);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                quantiteJeuxVideo = Convert.ToInt32(reader["quantity"]);
                            }
                            else
                            {
                                quantiteJeuxVideo = -1;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                quantiteJeuxVideo = -1;
            }
            finally
            {
                Deconnecter();
            }
            return quantiteJeuxVideo;
        }

        public int RecupClientID(string prmEmail)
        {
            string requete = "SELECT id_client FROM users WHERE email = @Email";
            bool isConnected = false;
            int clientID = 0;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    using (MySqlCommand command = new MySqlCommand(requete, connexion))
                    {
                        command.Parameters.AddWithValue("@Email", prmEmail);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                clientID = Convert.ToInt32(reader["id_client"]);
                            }
                            else
                            {
                                clientID = -1; 
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                isConnected = false;
                clientID = -1;
            }
            finally
            {
                Deconnecter();
            }
            return clientID;
        }

        public int RecupGameID(string prmTitle)
        {
            string requete = "SELECT id_game FROM jeux_video WHERE title = @Title";
            bool isConnected = false;
            int gameId = 0;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    using (MySqlCommand command = new MySqlCommand(requete, connexion))
                    {
                        command.Parameters.AddWithValue("@Title", prmTitle);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                gameId = Convert.ToInt32(reader["id_game"]);
                            }
                            else
                            {
                                gameId = -1;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                isConnected = false;
                gameId = -1;
            }
            finally
            {
                Deconnecter();
            }
            return gameId;
        }

        public bool AddToCart(string prmTitle, int prmClientID, int prmGameId)
        {
            bool estInscrit = false;
            string addCart = "INSERT INTO panier (titre_jeux, id_client, id_game) VALUES (@Title, @ClientID, @GameID)";

            bool isConnected = false;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    using (MySqlCommand command = new MySqlCommand(addCart, connexion))
                    {
                        command.Parameters.AddWithValue("@Title", prmTitle);
                        command.Parameters.AddWithValue("@ClientID", prmClientID);
                        command.Parameters.AddWithValue("@GameID", prmGameId);

                        command.ExecuteNonQuery();
                        estInscrit = true;
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                isConnected = false;
                estInscrit = false;
            }
            finally
            {
                Deconnecter();
            }
            return estInscrit;
        }

        public List<String> RécupCart(int prmIdClient)
        {
            string requete = "SELECT DISTINCT jeux_video.image, jeux_video.title, jeux_video.price FROM jeux_video, panier WHERE panier.id_game = jeux_video.id_game AND panier.id_client = @ClientID";

            bool isConnected = false;
            List<String> listJeuxVideo = new List<String>();

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    using (MySqlCommand command = new MySqlCommand(requete, connexion))
                    {
                        command.Parameters.AddWithValue("@ClientID", prmIdClient);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                listJeuxVideo.Add(Convert.ToString(reader["image"]));
                                listJeuxVideo.Add(Convert.ToString(reader["title"]));
                                listJeuxVideo.Add(Convert.ToString(reader["price"]));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                listJeuxVideo = null;
            }
            finally
            {
                Deconnecter();
            }
            return listJeuxVideo;

        }

        public async Task<List<JeuxVideo>> RecupAllJeuxVideoAsync()
        {
            var cacheKey = "AllGamesCache";
            var cachedData = HttpContext.Current.Cache[cacheKey] as List<JeuxVideo>;

            if (cachedData != null)
            {
                return cachedData;
            }

            List<JeuxVideo> games = new List<JeuxVideo>();
            string requete = "SELECT title, description, pegi, genre, quantity, image, price, discount, price_discount FROM jeux_video";

            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new MySqlCommand(requete, connection))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var game = new JeuxVideo
                                {
                                    title = reader["title"].ToString(),
                                    description = reader["description"].ToString(),
                                    pegi = int.Parse(reader["pegi"].ToString()),
                                    genre = reader["genre"].ToString(),
                                    quantite = int.Parse(reader["quantity"].ToString()),
                                    urlImage = reader["image"].ToString(),
                                    prix = float.Parse(reader["price"].ToString()),
                                    discount = reader["discount"] != DBNull.Value ? Convert.ToInt32(reader["discount"]) : 0,
                                    price_discount = reader["price_discount"] != DBNull.Value ? Convert.ToDecimal(reader["price_discount"]) : (decimal?)null
                                };
                                games.Add(game);
                            }
                        }
                    }
                }

                HttpContext.Current.Cache.Insert(cacheKey, games, null, DateTime.Now.AddMinutes(30), Cache.NoSlidingExpiration);
            }
            catch (Exception ex)
            {
                return new List<JeuxVideo>();
            }
            finally
            {
                Deconnecter();
            }

            return games;
        }

        //Recupération de la réduction de chaque jeux video en fonction du titre du jeu
        public int RecupDiscountJeuxVideo(string prmTitleGame)
        {
            string requete = "SELECT discount FROM jeux_video WHERE title = @Title";
            bool isConnected = false;
            int discountJeuxVideo = 0;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    using (MySqlCommand command = new MySqlCommand(requete, connexion))
                    {
                        command.Parameters.AddWithValue("@Title", prmTitleGame);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (!reader.IsDBNull(reader.GetOrdinal("discount")))
                                {
                                    discountJeuxVideo = Convert.ToInt32(reader["discount"]);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                discountJeuxVideo = -1;
            }
            finally
            {
                Deconnecter();
            }
            return discountJeuxVideo;
        }

        //Recupération du prix réduit de chaque jeux video en fonction du titre du jeu
        public float RecupPriceDiscountJeuxVideo(string prmTitleGame)
        {
            string requete = "SELECT price_discount FROM jeux_video WHERE title = @Title";
            bool isConnected = false;
            float priceDiscountJeuxVideo = 0;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    using (MySqlCommand command = new MySqlCommand(requete, connexion))
                    {
                        command.Parameters.AddWithValue("@Title", prmTitleGame);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                if (!reader.IsDBNull(reader.GetOrdinal("price_discount")))
                                {
                                    priceDiscountJeuxVideo = Convert.ToSingle(reader["price_discount"]);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                priceDiscountJeuxVideo = -1;
            }
            finally
            {
                Deconnecter();
            }
            return priceDiscountJeuxVideo;
        }

        public List<String> RecupLastJeuxVideo()
        {
            string requete = "SELECT image FROM jeux_video ORDER BY id DESC LIMIT 10";
            List<String> listJeuxVideo = new List<String>();
            bool isConnected = false;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    using (MySqlCommand command = new MySqlCommand(requete, connexion))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                listJeuxVideo.Add(Convert.ToString(reader["image"]));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                listJeuxVideo = null; 
            }
            finally
            {
                Deconnecter();
            }

            return listJeuxVideo;
        }

        public List<String> RecupLastTitleJeuxVideo()
        {
            string requete = "SELECT title FROM jeux_video ORDER BY id DESC LIMIT 10";
            List<String> listJeuxVideo = new List<String>();
            bool isConnected = false;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    using (MySqlCommand command = new MySqlCommand(requete, connexion))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                listJeuxVideo.Add(Convert.ToString(reader["title"]));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Erreur de récupération
                listJeuxVideo = null;
            }
            finally
            {
                Deconnecter();
            }
            return listJeuxVideo;
        }

        public List<String> RecupLastPromotedJeuxVideo()
        {
            string requete = "SELECT image FROM jeux_video WHERE discount IS NOT NULL ORDER BY discount DESC LIMIT 10";
            List<String> listJeuxVideo = new List<String>();
            bool isConnected = false;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    using (MySqlCommand command = new MySqlCommand(requete, connexion))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                listJeuxVideo.Add(Convert.ToString(reader["image"]));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                listJeuxVideo = null;
            }
            finally
            {
                Deconnecter();
            }
            return listJeuxVideo;
        }

        public List<String> RecupLastTitlePromotedJeuxVideo()
        {
            string requete = "SELECT title FROM jeux_video WHERE discount IS NOT NULL ORDER BY discount DESC LIMIT 10";
            List<String> listJeuxVideo = new List<String>();
            bool isConnected = false;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    using (MySqlCommand command = new MySqlCommand(requete, connexion))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                listJeuxVideo.Add(Convert.ToString(reader["title"]));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                listJeuxVideo = null;
            }
            finally
            {
                Deconnecter();
            }
            return listJeuxVideo;
        }

        public List<int> RecupDiscountPromotedJeuxVideo()
        {
            string requete = "SELECT discount FROM jeux_video WHERE discount IS NOT NULL ORDER BY discount DESC LIMIT 10";
            List<int> listDiscounts = new List<int>();
            bool isConnected = false;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    using (MySqlCommand command = new MySqlCommand(requete, connexion))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                listDiscounts.Add(Convert.ToInt32(reader["discount"]));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                listDiscounts = null;
            }
            finally
            {
                Deconnecter();
            }
            return listDiscounts;
        }

        public List<float> RécupPriceInCart(int prmIdClient)
        {
            string requete = "SELECT DISTINCT jeux_video.price, panier.id FROM jeux_video, panier WHERE panier.id_game = jeux_video.id_game AND panier.id_client = '" + prmIdClient + "' ORDER BY panier.id";
            List<float> listPriceJeuxVideo = new List<float>();
            bool isConnected = false;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    using (MySqlCommand command = new MySqlCommand(requete, connexion))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                listPriceJeuxVideo.Add(Convert.ToSingle(reader["price"]));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                listPriceJeuxVideo = null;
            }
            finally
            {
                Deconnecter();
            }
            return listPriceJeuxVideo;
        }

        public List<String> RécupCartForCommand(int prmIdClient)
        {
            string requete = "SELECT DISTINCT panier.titre_jeux, panier.id_client, panier.id_game FROM panier WHERE panier.id_client = @IdClient";
            List<String> listJeuxVideo = new List<String>();
            bool isConnected = false;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    using (MySqlCommand command = new MySqlCommand(requete, connexion))
                    {
                        command.Parameters.AddWithValue("@IdClient", prmIdClient);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                listJeuxVideo.Add(Convert.ToString(reader["titre_jeux"]));
                                listJeuxVideo.Add(Convert.ToString(reader["id_client"]));
                                listJeuxVideo.Add(Convert.ToString(reader["id_game"]));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                listJeuxVideo = null;
            }
            finally
            {
                Deconnecter();
            }
            return listJeuxVideo;
        }

        public bool CreateCommand(string prmStatutCommande, string prmTitre, string prmGenre, string prmDateRetrait, string prmNameStore, string prmIdGame, string prmIdClient)
        {
            bool estInscrit = false;
            string usersInscription = "INSERT INTO command (statut_commande, titre_jeux, genre, date_retrait, Name_Store, id_game, id_client) VALUES (@StatutCommande, @Titre, @Genre, @DateRetrait, @NameStore, @IdGame, @IdClient)";

            bool isConnected = false;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    using (MySqlCommand command = new MySqlCommand(usersInscription, connexion))
                    {
                        command.Parameters.AddWithValue("@StatutCommande", prmStatutCommande);
                        command.Parameters.AddWithValue("@Titre", prmTitre.Replace("'", " "));
                        command.Parameters.AddWithValue("@Genre", prmGenre);
                        command.Parameters.AddWithValue("@DateRetrait", prmDateRetrait);
                        command.Parameters.AddWithValue("@NameStore", prmNameStore);
                        command.Parameters.AddWithValue("@IdGame", prmIdGame);
                        command.Parameters.AddWithValue("@IdClient", prmIdClient);

                        command.ExecuteNonQuery();
                        estInscrit = true;
                    }
                }
            }
            catch (Exception ex)
            {
                estInscrit = false;
            }
            finally
            {
                Deconnecter();
            }
            return estInscrit;
        }

        //Suppression d'un jeu du panier
        public bool DeleteInCart(String prmTitreJeux, int prmIdClient)
        {
            String requete = "DELETE FROM panier WHERE titre_jeux = @TitreJeux AND id_client = @IdClient";
            bool isConnected = false;
            bool isDeleted = false;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    using (MySqlCommand command = new MySqlCommand(requete, connexion))
                    {
                        command.Parameters.AddWithValue("@TitreJeux", prmTitreJeux);
                        command.Parameters.AddWithValue("@IdClient", prmIdClient);

                        int affectedRows = command.ExecuteNonQuery();
                        isDeleted = affectedRows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                isConnected = false;
                isDeleted = false;
            }
            finally
            {
                Deconnecter();
            }
            return isDeleted;
        }

        public bool DeleteAllInCart(int prmIdClient)
        {
            String requete = "DELETE FROM panier WHERE id_client = @IdClient";
            bool isConnected = false;
            bool isDeleted = false;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    using (MySqlCommand command = new MySqlCommand(requete, connexion))
                    {
                        command.Parameters.AddWithValue("@IdClient", prmIdClient);

                        int affectedRows = command.ExecuteNonQuery();
                        isDeleted = affectedRows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                isConnected = false;
                isDeleted = false;
            }
            finally
            {
                Deconnecter();
            }
            return isDeleted;
        }

        public String VerifDoublonInCart(String prmTitreJeux, int prmIdClient)
        {
            String requete = "SELECT titre_jeux FROM panier WHERE titre_jeux = @TitreJeux AND id_client = @IdClient";
            bool isConnected = false;
            String verifDoublon = null;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    using (MySqlCommand command = new MySqlCommand(requete, connexion))
                    {
                        command.Parameters.AddWithValue("@TitreJeux", prmTitreJeux);
                        command.Parameters.AddWithValue("@IdClient", prmIdClient);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                verifDoublon = Convert.ToString(reader["titre_jeux"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                verifDoublon = null;
            }
            finally
            {
                Deconnecter();
            }
            return verifDoublon;
        }

        public List<String> RécupCommandLivré(int prmIdClient)
        {
            string requete = "SELECT DISTINCT jeux_video.image, jeux_video.title, jeux_video.price, jeux_video.genre, command.statut_commande, command.date_retrait FROM jeux_video, command WHERE command.id_game = jeux_video.id_game AND command.statut_commande = 'Livré' AND command.id_client = @IdClient";

            bool isConnected = false;
            List<String> listJeuxVideo = new List<String>();

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    using (MySqlCommand command = new MySqlCommand(requete, connexion))
                    {
                        command.Parameters.AddWithValue("@IdClient", prmIdClient);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                listJeuxVideo.Add(Convert.ToString(reader["image"]));
                                listJeuxVideo.Add(Convert.ToString(reader["title"]));
                                listJeuxVideo.Add(Convert.ToString(reader["price"]));
                                listJeuxVideo.Add(Convert.ToString(reader["genre"]));
                                listJeuxVideo.Add(Convert.ToString(reader["statut_commande"]));
                                listJeuxVideo.Add(Convert.ToString(reader["date_retrait"]));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                listJeuxVideo = null;
            }
            finally
            {
                Deconnecter();
            }
            return listJeuxVideo;
        }

        public List<String> RécupCommandNonLivre(int prmIdClient)
        {
            string requete = "SELECT DISTINCT jeux_video.image, jeux_video.title, jeux_video.price, jeux_video.genre, command.statut_commande, command.date_retrait FROM jeux_video, command WHERE command.id_game = jeux_video.id_game AND command.statut_commande = 'Validé' AND command.id_client = @IdClient";

            bool isConnected = false;
            List<String> listJeuxVideo = new List<String>();

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    using (MySqlCommand command = new MySqlCommand(requete, connexion))
                    {
                        command.Parameters.AddWithValue("@IdClient", prmIdClient);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                listJeuxVideo.Add(Convert.ToString(reader["image"]));
                                listJeuxVideo.Add(Convert.ToString(reader["title"]));
                                listJeuxVideo.Add(Convert.ToString(reader["price"]));
                                listJeuxVideo.Add(Convert.ToString(reader["genre"]));
                                listJeuxVideo.Add(Convert.ToString(reader["statut_commande"]));
                                listJeuxVideo.Add(Convert.ToString(reader["date_retrait"]));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                listJeuxVideo = null;
            }
            finally
            {
                Deconnecter();
            }
            return listJeuxVideo;
        }

        //Changement des informations de l'utilisateur
        public bool InfosUserChanged(int prmidClient, String prmNewNom, String prmNewPrenom, String prmNewEmail, String prmNewPostalAdresse)
        {
            String requete = "UPDATE users SET Nom = @NewNom, Prenom = @NewPrenom, email = @NewEmail, postal_adress = @NewPostalAdresse WHERE id_client = @IdClient";
            bool isConnected = false;
            bool isOK = false;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    using (MySqlCommand command = new MySqlCommand(requete, connexion))
                    {
                        command.Parameters.AddWithValue("@NewNom", prmNewNom);
                        command.Parameters.AddWithValue("@NewPrenom", prmNewPrenom);
                        command.Parameters.AddWithValue("@NewEmail", prmNewEmail);
                        command.Parameters.AddWithValue("@NewPostalAdresse", prmNewPostalAdresse);
                        command.Parameters.AddWithValue("@IdClient", prmidClient);

                        int affectedRows = command.ExecuteNonQuery();
                        isOK = affectedRows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                isConnected = false;
                isOK = false;
            }
            finally
            {
                Deconnecter();
            }
            return isOK;
        }

        public List<String> RecupGenreJeuxVideo()
        {
            String requete = "SELECT jeux_video.genre FROM jeux_video";
            List<String> recupGenre = new List<String>();
            bool isConnected = false;
            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    using (MySqlCommand command = new MySqlCommand(requete, connexion))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            recupGenre.Add(reader["genre"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                recupGenre = null;
            }
            finally
            {
                Deconnecter();
            }
            return recupGenre;
        }

        public List<String> RecupEmailClientWithCommand()
        {
            String requete = "SELECT DISTINCT users.email FROM users JOIN command ON command.id_client = users.id_client WHERE command.statut_commande = 'Validé'";
            List<String> recupNom = new List<String>();

            bool isConnected = false;
            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    using (MySqlCommand command = new MySqlCommand(requete, connexion))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            recupNom.Add(reader["email"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                recupNom = null;
            }
            finally
            {
                Deconnecter();
            }
            return recupNom;
        }

        //Changement des informations de l'utilisateur
        public bool UpdateStatutCommand(int prmidClient, String prmNameGame, String prmStatutCommand)
        {
            String requete = "UPDATE command SET statut_commande = @StatutCommand WHERE id_client = @IdClient AND titre_jeux = @NameGame";
            bool isConnected = false;
            bool isOK = false;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    using (MySqlCommand command = new MySqlCommand(requete, connexion))
                    {
                        command.Parameters.AddWithValue("@StatutCommand", prmStatutCommand);
                        command.Parameters.AddWithValue("@IdClient", prmidClient);
                        command.Parameters.AddWithValue("@NameGame", prmNameGame);

                        int affectedRows = command.ExecuteNonQuery();
                        isOK = affectedRows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                isConnected = false;
                isOK = false;
            }
            finally
            {
                Deconnecter();
            }
            return isOK;
        }

        public List<String> RécupCommandValide(int prmIdClient)
        {
            string requete = "SELECT DISTINCT jeux_video.image, jeux_video.title, jeux_video.price, jeux_video.genre, command.statut_commande, command.date_retrait FROM jeux_video JOIN command ON command.id_game = jeux_video.id_game WHERE command.statut_commande = 'Validé' AND command.id_client = @IdClient";
            List<String> listJeuxVideo = new List<String>();

            bool isConnected = false;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    using (MySqlCommand command = new MySqlCommand(requete, connexion))
                    {
                        command.Parameters.AddWithValue("@IdClient", prmIdClient);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                listJeuxVideo.Add(reader["image"].ToString());
                                listJeuxVideo.Add(reader["title"].ToString());
                                listJeuxVideo.Add(reader["price"].ToString());
                                listJeuxVideo.Add(reader["genre"].ToString());
                                listJeuxVideo.Add(reader["statut_commande"].ToString());
                                listJeuxVideo.Add(reader["date_retrait"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                listJeuxVideo = null;
            }
            finally
            {
                Deconnecter();
            }
            return listJeuxVideo;
        }

        public String RecupEmailWithClientID(int prmIdClient)
        {
            string requete = "SELECT email FROM users WHERE id_client = @IdClient";
            String emailClient = "";

            try
            {
                if (Connecter())
                {
                    using (MySqlCommand command = new MySqlCommand(requete, connexion))
                    {
                        command.Parameters.AddWithValue("@IdClient", prmIdClient);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                emailClient = reader["email"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                emailClient = null;
            }
            finally
            {
                Deconnecter();
            }
            return emailClient;
        }

        public bool UpdateDiscountVideoGame(String prmTitleGame, float prmDiscount, string prmPriceDiscount)
        {
            string requete = "UPDATE jeux_video SET discount = @Discount, price_discount = @PriceDiscount WHERE title = @TitleGame";
            bool isConnected = false;
            bool isOk = false;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    using (MySqlCommand command = new MySqlCommand(requete, connexion))
                    {
                        command.Parameters.AddWithValue("@Discount", prmDiscount);
                        command.Parameters.AddWithValue("@PriceDiscount", prmPriceDiscount.Replace(",", "."));
                        command.Parameters.AddWithValue("@TitleGame", prmTitleGame);

                        int result = command.ExecuteNonQuery();
                        isOk = result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                isOk = false;
            }
            finally
            {
                Deconnecter();
            }

            return isOk;
        }

        //Supression d'une promotions d'un jeu
        public bool SupprDiscountVideoGame(String prmTitleGame)
        {
            string requete = "UPDATE jeux_video SET discount = NULL, price_discount = NULL WHERE title = @TitleGame";
            bool isConnected = false;
            bool isOk = false;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    using (MySqlCommand command = new MySqlCommand(requete, connexion))
                    {
                        command.Parameters.AddWithValue("@TitleGame", prmTitleGame);

                        int result = command.ExecuteNonQuery();
                        isOk = result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                isOk = false;
            }
            finally
            {
                Deconnecter();
            }

            return isOk;
        }

        //Récuépration des PEGI pour affichage image
        public String RecupPegi(String prmPegiGame)
        {
            string requete = "SELECT pegi FROM pegi WHERE description = @PegiGame";
            bool isConnected = false;
            String pegiValue = "";

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    using (MySqlCommand command = new MySqlCommand(requete, connexion))
                    {
                        command.Parameters.AddWithValue("@PegiGame", prmPegiGame.Replace(" ", ""));

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                pegiValue = Convert.ToString(reader["pegi"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception if needed
                pegiValue = null;
            }
            finally
            {
                Deconnecter();
            }

            return pegiValue;
        }

        public Dictionary<string, Dictionary<string, int>> GetSalesByGenre()
        {
            string requete = "SELECT date_retrait, genre, COUNT(*) as nombre_ventes FROM command WHERE statut_commande = 'Livré' GROUP BY date_retrait, genre ORDER BY date_retrait, genre";

            Dictionary<string, Dictionary<string, int>> salesData = new Dictionary<string, Dictionary<string, int>>();
            bool isConnected = false;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    MySqlCommand command = new MySqlCommand(requete, connexion);
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string combinedGenres = reader.GetString("genre");
                        string dateVente = reader.GetDateTime("date_retrait").ToString("yyyy-MM-dd");

                        //Diviser le nombre de ventes également entre les genres
                        int nombreVentes = reader.GetInt32("nombre_ventes") / combinedGenres.Split(',').Length;

                        //Split et traiter chaque genre séparément
                        foreach (var genre in combinedGenres.Split(','))  
                        {
                            //Nettoyer les espaces blancs
                            string trimmedGenre = genre.Trim();

                            // Vérifier si le genre traité existe déjà dans le dictionnaire principal
                            if (!salesData.ContainsKey(trimmedGenre))
                            {
                                // Si le genre n'existe pas, initialiser un nouveau dictionnaire pour ce genre,
                                // où les clés seront des dates et les valeurs seront des nombres de ventes
                                salesData[trimmedGenre] = new Dictionary<string, int>();

                            }

                            // Vérifier si la date actuelle existe déjà dans le dictionnaire du genre traité
                            if (!salesData[trimmedGenre].ContainsKey(dateVente))
                            {
                                // Si la date n'existe pas, initialiser le nombre de ventes pour cette date à 0
                                salesData[trimmedGenre][dateVente] = 0;

                            }
                            salesData[trimmedGenre][dateVente] += nombreVentes;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Erreur de récupération
                salesData = null;
            }
            finally
            {
                Deconnecter();
            }

            return salesData;
        }

        public String RecupGenreForCart(String prmTitle)
        {
            string requete = "SELECT genre FROM jeux_video WHERE title = @Title";
            String genreJV = "";

            try
            {
                bool isConnected = Connecter();
                if (isConnected)
                {
                    using (MySqlCommand command = new MySqlCommand(requete, connexion))
                    {
                        command.Parameters.AddWithValue("@Title", prmTitle);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                genreJV = reader["genre"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                genreJV = null;
            }
            finally
            {
                Deconnecter();
            }

            return genreJV;
        }

        public List<string> RecupTitleInCart(int prmUserId)
        {
            string requete = "SELECT titre_jeux FROM panier WHERE id_client = @UserId";
            List<string> titleGames = new List<string>();

            try
            {
                bool isConnected = Connecter();
                if (isConnected)
                {
                    using (MySqlCommand command = new MySqlCommand(requete, connexion))
                    {
                        command.Parameters.AddWithValue("@UserId", prmUserId);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                titleGames.Add(reader["titre_jeux"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                titleGames = null;
            }
            finally
            {
                Deconnecter();
            }

            return titleGames;
        }

    }
}