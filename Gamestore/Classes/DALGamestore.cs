using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Windows.Forms;
using MongoDB.Driver;
using MySql.Data.MySqlClient;

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
            string verifDoublonMail = "SELECT (email) FROM users WHERE email =" + "'" + prmMail + "'";

            Classes.Users objUsers = null;
            bool isConnected = false;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {

                    command = new MySqlCommand(verifDoublonMail, connexion);
                    reader = command.ExecuteReader();
                    reader.Read();
                    objUsers = new Classes.Users();

                    objUsers.email = Convert.ToString(reader["email"]);
                };
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
            string usersInscription =
            "INSERT INTO users (Nom, Prenom, email, password, postal_adress, role_users, token_users) VALUES ('" + prmNom + "'," + "'" + prmPrenom + "'," + "'" + prmMail + "'," + "'" + prmPassword + "'," + "'" + prmPostalAdress + "'," + "'" + prmRolesUsers + "'," + "'" + prmTokenUsers + "'" + ")";

            bool isConnected = false;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(usersInscription, connexion);
                    reader = command.ExecuteReader();
                    estInscrit = true;
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

            String requete = "SELECT * FROM users WHERE email = '" + prmEmail + "' AND password= '" + prmPassword + "'";
            bool isConnected = false;
            Classes.Users objUsers = null;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(requete, connexion);
                    reader = command.ExecuteReader();
                    objUsers = new Classes.Users();

                    if (reader.Read())
                    {
                        objUsers.nom = Convert.ToString(reader["Nom"]);
                        objUsers.prenom = Convert.ToString(reader["Prenom"]);
                        objUsers.email = Convert.ToString(reader["email"]);
                        objUsers.password = Convert.ToString(reader["password"]);
                        objUsers.roleUsers = Convert.ToString(reader["role_users"]);
                        objUsers.postalAdress = Convert.ToString(reader["postal_adress"]);
                    }
                }
            }
            catch (Exception ex)
            {
                //Erreur de récupération
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

            String requete = "SELECT password FROM users WHERE email = '" + prmEmail + "'";
            bool isConnected = false;
            Classes.Users objUsers = null;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(requete, connexion);
                    reader = command.ExecuteReader();
                    objUsers = new Classes.Users();

                    if (reader.Read())
                    {
                        objUsers.password = Convert.ToString(reader["password"]);
                    }
                }
            }
            catch (Exception ex)
            {
                //Erreur de récupération
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

            String requete = "SELECT token_users FROM users WHERE email = '" + prmEmail + "'";
            bool isConnected = false;
            String tokenUsers = null;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(requete, connexion);
                    reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        tokenUsers = Convert.ToString(reader["token_users"]);
                    }
                }
            }
            catch (Exception ex)
            {
                //Erreur de récupération
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

            String requete = "SELECT email FROM users WHERE token_users = '" + prmTokenUsers + "'";
            bool isConnected = false;
            String mailUsers = null;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(requete, connexion);
                    reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        mailUsers = Convert.ToString(reader["email"]);
                    }
                }
            }
            catch (Exception ex)
            {
                //Erreur de récupération
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
            String requete = "UPDATE users SET password = '" + prmNewPassword + "'" + "WHERE email = '" + prmEmailUsers + "'";
            bool isConnected = false;
            bool isOk = false;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(requete, connexion);
                    reader = command.ExecuteReader();
                    isOk = true;
                }
            }
            catch (Exception ex)
            {
                //Erreur de récupération
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
        public bool InscriptionEmploye(string prmNom, string prmPrenom, string prmMail, string prmPassword, string prmRolesUsers)
        {
            bool estInscrit = false;
            string usersInscription =
            "INSERT INTO users (Nom, Prenom, email, password, role_users) VALUES ('" + prmNom + "'," + "'" + prmPrenom + "'," + "'" + prmMail + "'," + "'" + prmPassword + "'," + "'" + prmRolesUsers + "'" + ")";

            bool isConnected = false;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(usersInscription, connexion);
                    reader = command.ExecuteReader();
                    estInscrit = true;
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

            String requete = "SELECT id_game FROM jeux_video ORDER BY ID DESC LIMIT 1;";
            bool isConnected = false;
            int idGame = 0;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(requete, connexion);
                    reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        idGame = Convert.ToInt32(reader["id_game"]);
                    }
                }
            }
            catch (Exception ex)
            {
                //Erreur de récupération
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
            string usersInscription =
            "INSERT INTO jeux_video (image, title, price, pegi, quantity, genre, description, id_game) VALUES ('" + prmImage + "'," + "'" + prmTitre.Replace("'", " ") + "'," + "'" + prmPrix + "'," + "'" + prmPEGI + "'," + "'" + prmQuantity + "'," + "'" + prmGenre + "'," + "'" + prmDescription.Replace("'", "''") + "'," + "'" + prmIdGame + "'" + ")";

            bool isConnected = false;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(usersInscription, connexion);
                    reader = command.ExecuteReader();
                    estInscrit = true;
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

        //Création de jeux vidéo dans la bdd pour vente avec promotion
        public bool CreateGameWithPromotion(string prmImage, string prmTitre, string prmPrix, string prmPEGI, string prmQuantity, string prmGenre, string prmDescription, int prmIdGame, string prmDiscount, string prmPriceDiscount)
        {
            bool estInscrit = false;
            string usersInscription =
            "INSERT INTO jeux_video (image, title, price, pegi, quantity, genre, description, discount, price_discount, id_game) VALUES ('" + prmImage + "'," + "'" + prmTitre.Replace("'", " ") + "'," + "'" + prmPrix + "'," + "'" + prmPEGI + "'," + "'" + prmQuantity + "'," + "'" + prmGenre + "'," + "'" + prmDescription.Replace("'", "''") + "'," + "'" + prmDiscount + "'," + "'" + prmPriceDiscount.Replace(",", ".") + "'," + "'" + prmIdGame + "'" + ")";

            bool isConnected = false;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(usersInscription, connexion);
                    reader = command.ExecuteReader();
                    estInscrit = true;
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

        public Classes.JeuxVideo RécupJeuxVideo(string prmTitle)
        {
            string requete = "SELECT * FROM jeux_video WHERE title= '" + prmTitle + "'";

            bool isConnected = false;
            Classes.JeuxVideo objJeuxVideo = null;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(requete, connexion);
                    reader = command.ExecuteReader();
                    objJeuxVideo = new Classes.JeuxVideo();

                    if (reader.Read())
                    {
                        objJeuxVideo.urlImage = Convert.ToString(reader["image"]);
                        objJeuxVideo.prix = Convert.ToSingle(reader["price"]);
                        objJeuxVideo.description = Convert.ToString(reader["description"]);
                        objJeuxVideo.quantite = Convert.ToInt32(reader["quantity"]);
                        objJeuxVideo.pegi = Convert.ToInt32(reader["pegi"]);
                        objJeuxVideo.title = Convert.ToString(reader["title"]);
                        objJeuxVideo.genre = Convert.ToString(reader["genre"]);
                    }
                }
            }
            catch (Exception ex)
            {
                //Erreur de récupération
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
                    command = new MySqlCommand(requete, connexion);
                    reader = command.ExecuteReader();
                    reader.Read();

                    do
                    {
                        listTitleJeuxVideo.Add(Convert.ToString(reader["title"]));

                    } while (reader.Read());
                }
            }
            catch (Exception ex)
            {
                //Erreur de récupération
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
                    command = new MySqlCommand(requete, connexion);
                    reader = command.ExecuteReader();
                    reader.Read();

                    do
                    {
                        listTitleJeuxVideo.Add(Convert.ToString(reader["title"]));

                    } while (reader.Read());
                }
            }
            catch (Exception ex)
            {
                //Erreur de récupération
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
                    command = new MySqlCommand(requete, connexion);
                    reader = command.ExecuteReader();
                    reader.Read();

                    do
                    {
                        listTitleJeuxVideo.Add(Convert.ToString(reader["title"]));

                    } while (reader.Read());
                }
            }
            catch (Exception ex)
            {
                //Erreur de récupération
                listTitleJeuxVideo = null;
            }
            finally
            {
                Deconnecter();
            }
            return listTitleJeuxVideo;

        }

        public List<String> RécupJeuxVideoForPromotion()
        {
            string requete = "SELECT DISTINCT title, price, image FROM jeux_video";

            bool isConnected = false;
            List<String> listTitleJeuxVideo = new List<string>();

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(requete, connexion);
                    reader = command.ExecuteReader();
                    reader.Read();

                    do
                    {
                        listTitleJeuxVideo.Add(Convert.ToString(reader["title"]));

                    } while (reader.Read());
                }
            }
            catch (Exception ex)
            {
                //Erreur de récupération
                listTitleJeuxVideo = null;
            }

            Deconnecter();
            return listTitleJeuxVideo;

        }

        public bool UpdateQuantiteStock(int prmQuantite, String prmTitle)
        {
            String requete = "UPDATE jeux_video SET quantity = '" + prmQuantite + "'" + "WHERE title = '" + prmTitle + "'";
            bool isConnected = false;
            bool isOk = false;
            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(requete, connexion);
                    reader = command.ExecuteReader();

                    isOk = true;
                }

            }
            catch (InvalidOperationException)
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
            string requete = "SELECT quantity FROM jeux_video WHERE title = '" + prmTitle + "'";

            bool isConnected = false;
            int quantiteJeuxVideo = 0;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(requete, connexion);
                    reader = command.ExecuteReader();
                    reader.Read();

                    quantiteJeuxVideo = Convert.ToInt32(reader["quantity"]);

                }
            }
            catch (Exception ex)
            {
                //Erreur de récupération
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

            string requete = "SELECT id_client FROM users WHERE email = '" + prmEmail + "'";

            bool isConnected = false;
            int clientID = 0;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(requete, connexion);
                    reader = command.ExecuteReader();
                    reader.Read();

                    clientID = Convert.ToInt32(reader["id_client"]);
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

            string requete = "SELECT id_game FROM jeux_video WHERE title = '" + prmTitle + "'";

            bool isConnected = false;
            int clientID = 0;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(requete, connexion);
                    reader = command.ExecuteReader();
                    reader.Read();

                    clientID = Convert.ToInt32(reader["id_game"]);
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

        public bool AddToCart(string prmTitle, int prmClientID, int prmGameId)
        {
            bool estInscrit = false;
            string addCart =
            "INSERT INTO panier (titre_jeux, id_client, id_game) VALUES ('" + prmTitle + "'," + "'" + prmClientID + "'," + "'" + prmGameId + "'" + ")";

            bool isConnected = false;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(addCart, connexion);
                    reader = command.ExecuteReader();
                    estInscrit = true;
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

        public List<String> RécupCart(int prmIdClient)
        {
            string requete = "SELECT DISTINCT jeux_video.image, jeux_video.title, jeux_video.price FROM jeux_video, panier WHERE panier.id_game = jeux_video.id_game AND panier.id_client = '" + prmIdClient + "'";

            bool isConnected = false;
            List<String> listJeuxVideo = new List<String>();

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(requete, connexion);
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        do
                        {
                            listJeuxVideo.Add(Convert.ToString(reader["image"]));
                            listJeuxVideo.Add(Convert.ToString(reader["title"]));
                            listJeuxVideo.Add(Convert.ToString(reader["price"]));

                        } while (reader.Read());
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

        public List<String> RecupImageJeuxVideo()
        {
            string requete = "SELECT image FROM jeux_video ORDER BY id";

            bool isConnected = false;
            List<String> listJeuxVideo = new List<String>();

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(requete, connexion);
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        do
                        {
                            listJeuxVideo.Add(Convert.ToString(reader["image"]));

                        } while (reader.Read());
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

        //Recupération des prix de chaque jeux video
        public List<float> RecupPriceJeuxVideo()
        {
            string requete = "SELECT price FROM jeux_video ORDER BY id";

            bool isConnected = false;
            List<float> listPriceJeuxVideo = new List<float>();

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(requete, connexion);
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        do
                        {
                            listPriceJeuxVideo.Add(Convert.ToSingle(reader["price"]));

                        } while (reader.Read());
                    }
                }
            }
            catch (Exception ex)
            {
                //Erreur de récupération
                listPriceJeuxVideo.Add(-1);
            }
            finally
            {
                Deconnecter();
            }
            return listPriceJeuxVideo;

        }

        //Recupération du PEGI de chaque jeux video
        public List<int> RecupPegiJeuxVideo()
        {
            string requete = "SELECT pegi FROM jeux_video ORDER BY id";

            bool isConnected = false;
            List<int> listPegiJeuxVideo = new List<int>();

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(requete, connexion);
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        do
                        {
                            listPegiJeuxVideo.Add(Convert.ToInt32(reader["pegi"]));

                        } while (reader.Read());
                    }
                }
            }
            catch (Exception ex)
            {
                //Erreur de récupération
                listPegiJeuxVideo.Add(-1);
            }
            finally
            {
                Deconnecter();
            }
            return listPegiJeuxVideo;

        }

        public List<String> RecupTitreJeuxVideo()
        {
            string requete = "SELECT title FROM jeux_video ORDER BY id";

            bool isConnected = false;
            List<String> listJeuxVideo = new List<String>();

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(requete, connexion);
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        do
                        {
                            listJeuxVideo.Add(Convert.ToString(reader["title"]));

                        } while (reader.Read());
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

        //Recupération de la réduction de chaque jeux video en fonction du titre du jeu
        public int RecupDiscountJeuxVideo(string prmTitleGame)
        {
            string requete = "SELECT discount FROM jeux_video WHERE title = '" + prmTitleGame + "'";

            bool isConnected = false;
            int discountJeuxVideo = 0;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(requete, connexion);
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        do
                        {
                            if (!reader.IsDBNull(reader.GetOrdinal("discount")))
                            {
                                discountJeuxVideo = Convert.ToInt32(reader["discount"]);
                            }

                        } while (reader.Read());
                    }
                }
            }
            catch (Exception ex)
            {
                //Erreur de récupération
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
            string requete = "SELECT price_discount FROM jeux_video WHERE title = '" + prmTitleGame + "'";

            bool isConnected = false;
            float priceDiscountJeuxVideo = 0;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(requete, connexion);
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {

                        if (!reader.IsDBNull(reader.GetOrdinal("price_discount")))
                        {
                            priceDiscountJeuxVideo = Convert.ToSingle(reader["price_discount"]);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                //Erreur de récupération
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

            bool isConnected = false;
            List<String> listJeuxVideo = new List<String>();

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(requete, connexion);
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        do
                        {
                            listJeuxVideo.Add(Convert.ToString(reader["image"]));

                        } while (reader.Read());
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
        
        public List<String> RecupLastTitleJeuxVideo()
        {
            string requete = "SELECT title FROM jeux_video ORDER BY id DESC LIMIT 10";

            bool isConnected = false;
            List<String> listJeuxVideo = new List<String>();

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(requete, connexion);
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        do
                        {
                            listJeuxVideo.Add(Convert.ToString(reader["title"]));

                        } while (reader.Read());
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

            bool isConnected = false;
            List<String> listJeuxVideo = new List<String>();

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(requete, connexion);
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        do
                        {
                            listJeuxVideo.Add(Convert.ToString(reader["image"]));

                        } while (reader.Read());
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

        public List<String> RecupLastTitlePromotedJeuxVideo()
        {
            string requete = "SELECT title FROM jeux_video WHERE discount IS NOT NULL ORDER BY discount DESC LIMIT 10";

            bool isConnected = false;
            List<String> listJeuxVideo = new List<String>();

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(requete, connexion);
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        do
                        {
                            listJeuxVideo.Add(Convert.ToString(reader["title"]));

                        } while (reader.Read());
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

        public List<float> RecupDiscountPromotedJeuxVideo()
        {
            string requete = "SELECT discount FROM jeux_video WHERE discount IS NOT NULL ORDER BY discount DESC LIMIT 10";

            bool isConnected = false;
            List<float> listJeuxVideo = new List<float>();

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(requete, connexion);
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        do
                        {
                            listJeuxVideo.Add(Convert.ToSingle(reader["discount"]));

                        } while (reader.Read());
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Deconnecter();
            }
            return listJeuxVideo;
        }

        public List<float> RécupPriceInCart(int prmIdClient)
        {
            string requete = "SELECT DISTINCT jeux_video.price FROM jeux_video, panier WHERE panier.id_game = jeux_video.id_game AND panier.id_client = '" + prmIdClient + "'" + " ORDER BY panier.id";

            bool isConnected = false;
            List<float> listPriceJeuxVideo = new List<float>();

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(requete, connexion);
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        do
                        {

                            listPriceJeuxVideo.Add(Convert.ToSingle(reader["price"]));

                        } while (reader.Read());
                    }
                }
            }
            catch (Exception ex)
            {
                //Erreur de récupération
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
            string requete = "SELECT DISTINCT panier.titre_jeux, panier.id_client, panier.id_game FROM panier WHERE panier.id_client = '" + prmIdClient + "'";

            bool isConnected = false;
            List<String> listJeuxVideo = new List<String>();

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(requete, connexion);
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        do
                        {
                            listJeuxVideo.Add(Convert.ToString(reader["titre_jeux"]));
                            listJeuxVideo.Add(Convert.ToString(reader["id_client"]));
                            listJeuxVideo.Add(Convert.ToString(reader["id_game"]));

                        } while (reader.Read());
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

        //Création de jeux vidéo dans la bdd pour vente
        public bool CreateCommand(string prmStatutCommande, string prmTitre, string prmGenre, string prmDateRetrait, string prmNameStore, string prmIdGame, string prmIdClient)
        {
            bool estInscrit = false;
            string usersInscription =
            "INSERT INTO command (statut_commande, titre_jeux, genre, date_retrait, Name_Store, id_game, id_client) VALUES ('" + prmStatutCommande + "'," + "'" + prmTitre.Replace("'", " ") + "'," + "'" + prmGenre + "'," + "'" + prmDateRetrait + "'," + "'" + prmNameStore + "'," + "'" + prmIdGame + "'," + "'" + prmIdClient + "'" + ")";

            bool isConnected = false;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(usersInscription, connexion);
                    reader = command.ExecuteReader();
                    estInscrit = true;
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

        //Suppression d'un jeu du panier
        public bool DeleteInCart(String prmTitreJeux, int prmIdClient)
        {
            String requete = "DELETE FROM panier WHERE titre_jeux = '" + prmTitreJeux + "'" + " AND id_client = '" + prmIdClient + "'";
            bool isConnected = false;
            bool estInscrit = false;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(requete, connexion);
                    reader = command.ExecuteReader();
                    estInscrit = true;
                }
            }
            catch (Exception ex)
            {
                //Erreur de récupération
                isConnected = false;
                estInscrit = false;
            }
            finally
            {
                Deconnecter();
            }
            return estInscrit;
        }

        public bool DeleteAllInCart(int prmIdClient)
        {
            String requete = "DELETE FROM panier WHERE id_client = '" + prmIdClient + "'";
            bool isConnected = false;
            bool isDelete = false;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(requete, connexion);
                    reader = command.ExecuteReader();
                    isDelete = true;
                }
            }
            catch (Exception ex)
            {
                //Erreur de récupération
                isConnected = false;
                isDelete = false;
            }
            finally
            {
                Deconnecter();
            }
            return isDelete;
        }

        //Vérification des stocks
        public int VerifStocksGame(String prmTitreJeux)
        {
            String requete = "SELECT quantity FROM jeux_video WHERE title = '" + prmTitreJeux + "'";
            bool isConnected = false;
            int stockGame = -1;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(requete, connexion);
                    reader = command.ExecuteReader();
                    reader.Read();

                    stockGame = Convert.ToInt32(reader["quantity"]);
                }
            }
            catch (Exception ex)
            {
                //Erreur de récupération
                isConnected = false;
            }
            finally
            {
                Deconnecter();
            }
            return stockGame;
        }

        public String VerifDoublonInCart(String prmTitreJeux, int prmIdClient)
        {
            String requete = "SELECT titre_jeux FROM panier WHERE titre_jeux = '" + prmTitreJeux + "'" + " AND id_client = '" + prmIdClient + "'";
            bool isConnected = false;
            String verifDoublon = null;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(requete, connexion);
                    reader = command.ExecuteReader();
                    reader.Read();

                    verifDoublon = Convert.ToString(reader["titre_jeux"]);
                }
            }
            catch (Exception ex)
            {
                //Erreur de récupération
                isConnected = false;
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
            string requete = "SELECT DISTINCT jeux_video.image, jeux_video.title, jeux_video.price, jeux_video.genre, command.statut_commande, command.date_retrait FROM jeux_video, command WHERE command.id_game = jeux_video.id_game AND command.statut_commande = 'Livré' AND command.id_client = '" + prmIdClient + "'";

            bool isConnected = false;
            List<String> listJeuxVideo = new List<String>();

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(requete, connexion);
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        do
                        {
                            listJeuxVideo.Add(Convert.ToString(reader["image"]));
                            listJeuxVideo.Add(Convert.ToString(reader["title"]));
                            listJeuxVideo.Add(Convert.ToString(reader["price"]));
                            listJeuxVideo.Add(Convert.ToString(reader["genre"]));
                            listJeuxVideo.Add(Convert.ToString(reader["statut_commande"]));
                            listJeuxVideo.Add(Convert.ToString(reader["date_retrait"]));

                        } while (reader.Read());
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

        public List<String> RécupCommandNonLivre(int prmIdClient)
        {
            string requete = "SELECT DISTINCT jeux_video.image, jeux_video.title, jeux_video.price, jeux_video.genre, command.statut_commande, command.date_retrait FROM jeux_video, command WHERE command.id_game = jeux_video.id_game AND command.statut_commande = 'Validé' AND command.id_client = '" + prmIdClient + "'";

            bool isConnected = false;
            List<String> listJeuxVideo = new List<String>();

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(requete, connexion);
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        do
                        {
                            listJeuxVideo.Add(Convert.ToString(reader["image"]));
                            listJeuxVideo.Add(Convert.ToString(reader["title"]));
                            listJeuxVideo.Add(Convert.ToString(reader["price"]));
                            listJeuxVideo.Add(Convert.ToString(reader["genre"]));
                            listJeuxVideo.Add(Convert.ToString(reader["statut_commande"]));
                            listJeuxVideo.Add(Convert.ToString(reader["date_retrait"]));

                        } while (reader.Read());
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

        //Changement des informations de l'utilisateur
        public bool InfosUserChanged(int prmidClient, String prmNewNom, String prmNewPrenom, String prmNewEmail, String prmNewPostalAdresse)
        {
            String requete = "UPDATE users SET Nom = '" + prmNewNom + "'," + "Prenom = '" + prmNewPrenom + "'," + "email = '" + prmNewEmail + "'," + "postal_adress = '" + prmNewPostalAdresse + "'" + "WHERE id_client = '" + prmidClient + "'";
            bool isConnected = false;
            bool isOK = false;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(requete, connexion);
                    reader = command.ExecuteReader();
                    isOK = true;
                }
            }
            catch (Exception ex)
            {
                //Erreur de récupération
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
            bool isConnected = false;
            List<String> recupGenre = new List<String>();

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(requete, connexion);
                    reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        do
                        {
                            recupGenre.Add(Convert.ToString(reader["genre"]));

                        } while (reader.Read());
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Deconnecter();
            }
            return recupGenre;

        }

        public List<String> RecupEmailClientWithCommand()
        {

            String requete = "SELECT DISTINCT users.email FROM users, command WHERE command.id_client = users.id_client AND command.statut_commande = 'Validé'";
            bool isConnected = false;
            List<String> recupNom = new List<String>();

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(requete, connexion);
                    reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        do
                        {
                            recupNom.Add(Convert.ToString(reader["email"]));

                        } while (reader.Read());
                    }
                }
            }
            catch (Exception ex)
            {

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
            String requete = "UPDATE command SET statut_commande = '" + prmStatutCommand + "'" + "WHERE id_client = '" + prmidClient + "'" + " AND titre_jeux = '" + prmNameGame + "'";
            bool isConnected = false;
            bool isOK = false;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(requete, connexion);
                    reader = command.ExecuteReader();
                    isOK = true;
                }
            }
            catch (Exception ex)
            {
                //Erreur de récupération
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
            string requete = "SELECT DISTINCT jeux_video.image, jeux_video.title, jeux_video.price, jeux_video.genre, command.statut_commande, command.date_retrait FROM jeux_video, command WHERE command.id_game = jeux_video.id_game AND command.statut_commande = 'Validé' AND command.id_client = '" + prmIdClient + "'";

            bool isConnected = false;
            List<String> listJeuxVideo = new List<String>();

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(requete, connexion);
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        do
                        {
                            listJeuxVideo.Add(Convert.ToString(reader["image"]));
                            listJeuxVideo.Add(Convert.ToString(reader["title"]));
                            listJeuxVideo.Add(Convert.ToString(reader["price"]));
                            listJeuxVideo.Add(Convert.ToString(reader["genre"]));
                            listJeuxVideo.Add(Convert.ToString(reader["statut_commande"]));
                            listJeuxVideo.Add(Convert.ToString(reader["date_retrait"]));

                        } while (reader.Read());
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

        public String RecupEmailWithClientID(int prmIdClient)
        {
            string requete = "SELECT users.email FROM users WHERE users.id_client = '" + prmIdClient + "'";

            bool isConnected = false;
            String emailClient = "";

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(requete, connexion);
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        do
                        {
                            emailClient = Convert.ToString(reader["email"]);

                        } while (reader.Read());
                    }
                }
            }
            catch (Exception ex)
            {
                //Erreur de récupération
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
            string requete = "UPDATE jeux_video SET discount = '" + prmDiscount + "'," + "price_discount = '" + prmPriceDiscount.Replace(",", ".") + "'" + "WHERE title = '" + prmTitleGame + "'";

            bool isConnected = false;
            bool isOk = false;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(requete, connexion);
                    reader = command.ExecuteReader();
                    isOk = true;
                }
            }
            catch (Exception ex)
            {
                //Erreur de récupération
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
            string requete = "UPDATE jeux_video SET discount = NULL, price_discount = NULL WHERE title = '" + prmTitleGame + "'";

            bool isConnected = false;
            bool isOk = false;

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(requete, connexion);
                    reader = command.ExecuteReader();
                    isOk = true;
                }
            }
            catch (Exception ex)
            {
                //Erreur de récupération
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
            string requete = "SELECT pegi FROM pegi WHERE description = '" + prmPegiGame.Replace(" ", "") + "'";

            bool isConnected = false;
            String emailClient = "";

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(requete, connexion);
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        do
                        {
                            emailClient = Convert.ToString(reader["pegi"]);

                        } while (reader.Read());
                    }
                }
            }
            catch (Exception ex)
            {
                //Erreur de récupération
                emailClient = null;
            }
            finally
            {
                Deconnecter();
            }

            return emailClient;
        }

        public List<String> RecupGenreCommand()
        {
            string requete = "SELECT genre FROM command WHERE statut_commande = 'Livré'";

            bool isConnected = false;
            List<String> listGenreJV = new List<String>();

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(requete, connexion);
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        do
                        {
                            listGenreJV.Add(Convert.ToString(reader["genre"]));

                        } while (reader.Read());
                    }
                }
            }
            catch (Exception ex)
            {
                //Erreur de récupération
                listGenreJV = null;
            }
            finally
            {
                Deconnecter();
            }

            return listGenreJV;
        }

        public Dictionary<string, Dictionary<string, int>> GetSalesByGenre()
        {
            string requete = "SELECT genre, date_retrait, COUNT(*) as nombre_ventes FROM command WHERE statut_commande = 'Livré' GROUP BY genre";

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
            string requete = "SELECT genre FROM jeux_video WHERE title = '" + prmTitle + "'";

            bool isConnected = false;
            String genreJV = "";

            try
            {
                isConnected = Connecter();
                if (isConnected)
                {
                    command = new MySqlCommand(requete, connexion);
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        do
                        {
                            genreJV = Convert.ToString(reader["genre"]);

                        } while (reader.Read());
                    }
                }
            }
            catch (Exception ex)
            {
                //Erreur de récupération
                genreJV = null;
            }
            finally
            {
                Deconnecter();
            }

            return genreJV;
        }
    }
}