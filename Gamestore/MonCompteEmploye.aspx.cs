using Amazon.Runtime.Documents;
using Gamestore.Classes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gamestore
{
    public partial class MonCompteEmploye : System.Web.UI.Page
    {
        DALGamestore objDal = new DALGamestore();
        DALMongo objMongo = new DALMongo();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(Session["EstConnecte"]) && Session["RoleUtilisateur"].ToString() == "Employé")
            {
                //Variable pour récupéré la date du jour
                String dateRetraitFinal;

                //Variable pour récupéré le prix du jeu
                String substringLblPrice = "";

                LblUsers.Text = Session["Prenom"].ToString() + " " + Session["Nom"].ToString();
                if (!IsPostBack)
                {
                    PnlHistCommandClient.Visible = false;
                    PnlReadSale.Visible = false;
                    PnlSaleByGenre.Visible = false;
                    PnlSaleByItem.Visible = false;
                    DdlCmdClient.Visible = false;
                    BtnDetailsChart.Visible = false;
                }
                else
                {
                    if (DdlCmdClient.Text != "")
                    {
                        //Récupération de l'id client via son email contenu dans le dropdownlist
                        String emailUsers = DdlCmdClient.Text;
                        int idClient = objDal.RecupClientID(emailUsers);

                        if (idClient != 0)
                        {
                            //Récupération des commmandes passés par l'utilisateur
                            List<String> listCommand = new List<String>();
                            listCommand = objDal.RécupCommandValide(idClient);

                            if (listCommand.Count > 0)
                            {
                                StringBuilder sb0 = new StringBuilder();
                                StringBuilder sb1 = new StringBuilder();
                                StringBuilder sb2 = new StringBuilder();
                                StringBuilder sb3 = new StringBuilder();
                                StringBuilder sb4 = new StringBuilder();
                                StringBuilder sb5 = new StringBuilder();
                                StringBuilder sb6 = new StringBuilder();

                                sb0.Append(@"<div class='hc'>");
                                sb1.Append(@"<div class='hc_menu'>");
                                sb2.Append(@"<br />");
                                sb3.Append(@"<div class='contenu_game'>");
                                sb4.Append(@"<hr class='separate_content' />");
                                sb5.Append(@"<div class='contenu3_game'>");
                                sb6.Append(@"</div>");

                                int i = 0;

                                for (int j = 0; j < listCommand.Count; j++)
                                {

                                    Image ImgGame = new Image();
                                    Label LblTitle = new Label();
                                    Button BtnUpdateStatut = new Button();
                                    Label LblPrice = new Label();
                                    Label LblStatutCommand = new Label();

                                    ImgGame.ID = "ImgGame" + i.ToString();
                                    ImgGame.ImageUrl = listCommand[i];
                                    ImgGame.CssClass = "img_game";
                                    i++;
                                    j++;

                                    LblTitle.ID = "LblTitle" + i.ToString();
                                    LblTitle.Text = listCommand[i].ToString();
                                    i++;
                                    j++;

                                    float priceDiscount = objDal.RecupPriceDiscountJeuxVideo(LblTitle.Text);

                                    if (priceDiscount <= 0)
                                    {
                                        LblPrice.ID = "LblPrice" + i.ToString();
                                        LblPrice.Text = "Prix : " + listCommand[i].ToString() + "€";

                                        if(LblPrice.Text.Contains(","))
                                        {
                                            substringLblPrice = LblPrice.Text.Substring(7, 5);
                                        }
                                        else
                                        {
                                            substringLblPrice = listCommand[i].ToString();
                                        }
                                        

                                        i = i + 2;
                                        j = j + 2;
                                    }
                                    else
                                    {
                                        LblPrice.ID = "LblPrice" + i.ToString();
                                        LblPrice.Text = "Prix : " + priceDiscount.ToString() + "€";

                                        i = i + 2;
                                        j = j + 2;
                                    }


                                    LblStatutCommand.ID = "LblStatutCommand" + i.ToString();
                                    LblStatutCommand.Text = listCommand[i].ToString();
                                    LblStatutCommand.CssClass = "statut_command";
                                    i++;
                                    j++;

                                    if (LblStatutCommand.Text == "Validé")
                                    {
                                        String dateRetrait = listCommand[i].ToString();
                                        dateRetraitFinal = dateRetrait.Substring(0, 10);

                                        LblStatutCommand.Text = "Commande en attente de retrait le " + dateRetraitFinal;
                                    }
                                    i++;

                                    BtnUpdateStatut.ID = "BtnUpdateStatut" + i.ToString();
                                    BtnUpdateStatut.Text = "Valider la commande";
                                    BtnUpdateStatut.Visible = true;
                                    BtnUpdateStatut.CssClass = "BtnUpdateStatut";
                                    BtnUpdateStatut.Click += (source, args) =>
                                    {
                                        //Lien vers la DB MongoDB
                                        var database = DALMongo.GetDatabase();
                                        //Lien vers la Collection de la DB
                                        var collection = database.GetCollection<BsonDocument>("video_games");

                                        //Statut de la commande quand le client est venu la récupérer en magasin
                                        String statutCommand = "Livré";

                                        //Récupération du nom du jeu pour update le statut de la commande
                                        String titleGame = LblTitle.Text;

                                        //Récupération de la date du jour pour la date de retrait de commande
                                        var retraitCommand = DateTime.Now;

                                        //Update du statut des commandes par jeux
                                        bool isOk = false;
                                        isOk = objDal.UpdateStatutCommand(idClient, titleGame, statutCommand);

                                        if (isOk == true)
                                        {
                                            if (priceDiscount <= 0)
                                            {
                                                if (double.TryParse(substringLblPrice, out double price))
                                                {
                                                    // Pour insérer un document
                                                    var document = new BsonDocument
                                                    {
                                                        { "name", titleGame },
                                                        { "price", price },
                                                        { "date", retraitCommand }
                                                    };

                                                    collection.InsertOne(document);
                                                    Alert.Show("Commande validé avec succès");
                                                    MaJCommand();
                                                }
                                                else
                                                {
                                                    String priceGame = substringLblPrice;
                                                    // Pour insérer un document
                                                    var document = new BsonDocument
                                                    {
                                                        { "name", titleGame },
                                                        { "price", priceGame },
                                                        { "date", retraitCommand }
                                                    };

                                                    collection.InsertOne(document);
                                                    Alert.Show("Commande validé avec succès");
                                                    MaJCommand();
                                                }
                                            }
                                            else
                                            {
                                                //On convertit le prix en String pour substring
                                                String lastPrice;
                                                lastPrice = priceDiscount.ToString();

                                                //On récupére le nombre de caractère total
                                                int tailleLastPrice = 0;
                                                tailleLastPrice = lastPrice.Length;


                                                if (lastPrice.Contains(","))
                                                {
                                                    if (tailleLastPrice > 5 || tailleLastPrice == 4)
                                                    {
                                                        if (tailleLastPrice == 4)
                                                        {
                                                            lastPrice = lastPrice + "0";
                                                            lastPrice = lastPrice.Substring(0, 5);

                                                            //Insertion dans DB MongoDB
                                                            var document = new BsonDocument
                                                            {
                                                                { "name", titleGame },
                                                                { "price", lastPrice },
                                                                { "date", retraitCommand }
                                                            };

                                                            collection.InsertOne(document);
                                                            Alert.Show("Commande validé avec succès");
                                                            MaJCommand();
                                                        }
                                                        else
                                                        {
                                                            lastPrice = lastPrice.Substring(0, 5);
                                                            double lastDoublePrice = Convert.ToDouble(lastPrice);
                                                            //Insertion dans DB MongoDB
                                                            var document = new BsonDocument
                                                            {
                                                                { "name", titleGame },
                                                                { "price", lastDoublePrice },
                                                                { "date", retraitCommand }
                                                            };

                                                            collection.InsertOne(document);
                                                            Alert.Show("Commande validé avec succès");
                                                            MaJCommand();
                                                        }
                                                    }
                                                    else if (tailleLastPrice == 5)
                                                    {
                                                        lastPrice = lastPrice.Substring(0, 5);
                                                        double lastDoublePrice = Convert.ToDouble(lastPrice);
                                                        //Insertion dans DB MongoDB
                                                        var document = new BsonDocument
                                                            {
                                                                { "name", titleGame },
                                                                { "price", lastDoublePrice },
                                                                { "date", retraitCommand }
                                                            };

                                                        collection.InsertOne(document);
                                                        Alert.Show("Commande validé avec succès");
                                                        MaJCommand();
                                                    }
                                                    else if (tailleLastPrice == 2)
                                                    {
                                                        lastPrice = lastPrice.Substring(0, 2);
                                                        double lastDoublePrice = Convert.ToDouble(lastPrice);
                                                        //Insertion dans DB MongoDB
                                                        var document = new BsonDocument
                                                            {
                                                                { "name", titleGame },
                                                                { "price", lastDoublePrice },
                                                                { "date", retraitCommand }
                                                            };

                                                        collection.InsertOne(document);
                                                        Alert.Show("Commande validé avec succès");
                                                        MaJCommand();
                                                    }
                                                    else if (tailleLastPrice == 1)
                                                    {
                                                        lastPrice = lastPrice.Substring(0, 1);
                                                        double lastDoublePrice = Convert.ToDouble(lastPrice);
                                                        //Insertion dans DB MongoDB
                                                        var document = new BsonDocument
                                                            {
                                                                { "name", titleGame },
                                                                { "price", lastDoublePrice },
                                                                { "date", retraitCommand }
                                                            };

                                                        collection.InsertOne(document);
                                                        Alert.Show("Commande validé avec succès");
                                                        MaJCommand();
                                                    }
                                                }
                                            }
                                        }
                                    };

                                    //Affichage dynamique
                                    PnlHistCommandClient.Controls.Add(new LiteralControl(sb0.ToString()));
                                    PnlHistCommandClient.Controls.Add(new LiteralControl(sb1.ToString()));
                                    PnlHistCommandClient.Controls.Add(LblStatutCommand);
                                    PnlHistCommandClient.Controls.Add(new LiteralControl(sb2.ToString()));
                                    PnlHistCommandClient.Controls.Add(new LiteralControl(sb2.ToString()));
                                    PnlHistCommandClient.Controls.Add(ImgGame);
                                    PnlHistCommandClient.Controls.Add(new LiteralControl("&nbsp;"));
                                    PnlHistCommandClient.Controls.Add(new LiteralControl("&nbsp;"));
                                    PnlHistCommandClient.Controls.Add(new LiteralControl(sb3.ToString()));
                                    PnlHistCommandClient.Controls.Add(LblTitle);
                                    PnlHistCommandClient.Controls.Add(new LiteralControl(sb2.ToString()));
                                    PnlHistCommandClient.Controls.Add(new LiteralControl(sb2.ToString()));
                                    PnlHistCommandClient.Controls.Add(LblPrice);
                                    PnlHistCommandClient.Controls.Add(new LiteralControl(sb4.ToString()));
                                    PnlHistCommandClient.Controls.Add(new LiteralControl(sb5.ToString()));
                                    PnlHistCommandClient.Controls.Add(BtnUpdateStatut);
                                    PnlHistCommandClient.Controls.Add(new LiteralControl(sb6.ToString()));
                                    PnlHistCommandClient.Controls.Add(new LiteralControl(sb6.ToString()));
                                    PnlHistCommandClient.Controls.Add(new LiteralControl(sb2.ToString()));
                                    PnlHistCommandClient.Controls.Add(new LiteralControl(sb2.ToString()));
                                    PnlHistCommandClient.Controls.Add(new LiteralControl(sb6.ToString()));
                                    PnlHistCommandClient.Controls.Add(new LiteralControl(sb6.ToString()));
                                    PnlHistCommandClient.Controls.Add(new LiteralControl(sb2.ToString()));
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                Response.Redirect("~/Connexion");
            }
        }

        protected void BtnDisconnect_Click(object sender, EventArgs e)
        {
            Session["EstConnecte"] = false;
            Session["MailUtilisateur"] = "";
            Session["Nom"] = "";
            Session["Prenom"] = "";
            Session["RoleUtilisateur"] = "";
            Session["PostalAdress"] = "";
            Response.Redirect(Request.RawUrl);
        }

        protected void BtnValidateCmd_Click(object sender, EventArgs e)
        {

            DdlCmdClient.Text = "";
            PnlHistCommandClient.Visible = true;
            PnlReadSale.Visible = false;
            DdlCmdClient.Visible = true;
            title_menu.Visible = true;
            title_menu.InnerText = "Validation des commandes";

            //Récupération des emails des utilisateurs ayant des commandes à retirer
            List<String> listEmailUsers = new List<String>();
            listEmailUsers = objDal.RecupEmailClientWithCommand();

            if (listEmailUsers.Count != 0)
            {
                //On vide le DropDownList avant de mettre les données dedans
                DdlCmdClient.Items.Clear();

                //Ajout des emails dans le DropDownList
                DdlCmdClient.Items.Add("");
                foreach (var email in listEmailUsers)
                {
                    DdlCmdClient.Items.Add(email);
                }
            }
            else
            {
                DdlCmdClient.Visible = false;
                title_menu.InnerText = "Pas de commande à valider";
            }

        }

        protected void MaJCommand()
        {
            if (DdlCmdClient.Text != "")
            {
                String dateRetraitFinal;

                //Récupération de l'id client via son email contenu dans le dropdownlist
                String emailUsers = DdlCmdClient.Text;
                int idClient = objDal.RecupClientID(emailUsers);

                if (idClient != 0)
                {
                    PnlHistCommandClient.Controls.Clear();
                    //Récupération des commmandes passés par l'utilisateur
                    List<String> listCommand = new List<String>();
                    listCommand = objDal.RécupCommandValide(idClient);

                    if (listCommand.Count > 0)
                    {
                        StringBuilder sb0 = new StringBuilder();
                        StringBuilder sb1 = new StringBuilder();
                        StringBuilder sb2 = new StringBuilder();
                        StringBuilder sb3 = new StringBuilder();
                        StringBuilder sb4 = new StringBuilder();
                        StringBuilder sb5 = new StringBuilder();
                        StringBuilder sb6 = new StringBuilder();

                        sb0.Append(@"<div class='hc'>");
                        sb1.Append(@"<div class='hc_menu'>");
                        sb2.Append(@"<br />");
                        sb3.Append(@"<div class='contenu_game'>");
                        sb4.Append(@"<hr class='separate_content' />");
                        sb5.Append(@"<div class='contenu3_game'>");
                        sb6.Append(@"</div>");

                        int i = 0;

                        for (int j = 0; j < listCommand.Count; j++)
                        {

                            Image ImgGame = new Image();
                            Label LblTitle = new Label();
                            Button BtnUpdateStatut = new Button();
                            Label LblPrice = new Label();
                            Label LblStatutCommand = new Label();

                            ImgGame.ID = "ImgGame" + i.ToString();
                            ImgGame.ImageUrl = listCommand[i];
                            ImgGame.CssClass = "img_game";
                            i++;
                            j++;

                            LblTitle.ID = "LblTitle" + i.ToString();
                            LblTitle.Text = listCommand[i].ToString();
                            i++;
                            j++;

                            float priceDiscount = objDal.RecupPriceDiscountJeuxVideo(LblTitle.Text);

                            if (priceDiscount <= 0)
                            {
                                LblPrice.ID = "LblPrice" + i.ToString();
                                LblPrice.Text = "Prix : " + listCommand[i].ToString() + "€";

                                i = i + 2;
                                j = j + 2;
                            }
                            else
                            {
                                LblPrice.ID = "LblPrice" + i.ToString();
                                LblPrice.Text = "Prix : " + priceDiscount.ToString() + "€";

                                i = i + 2;
                                j = j + 2;
                            }


                            LblStatutCommand.ID = "LblStatutCommand" + i.ToString();
                            LblStatutCommand.Text = listCommand[i].ToString();
                            LblStatutCommand.CssClass = "statut_command";
                            i++;
                            j++;

                            if (LblStatutCommand.Text == "Validé")
                            {
                                String dateRetrait = listCommand[i].ToString();
                                dateRetraitFinal = dateRetrait.Substring(0, 10);

                                LblStatutCommand.Text = "Commande en attente de retrait le " + dateRetraitFinal;
                            }
                            i++;

                            BtnUpdateStatut.ID = "BtnUpdateStatut" + i.ToString();
                            BtnUpdateStatut.Text = "Valider la commande";
                            BtnUpdateStatut.Visible = true;
                            BtnUpdateStatut.CssClass = "BtnUpdateStatut";
                            BtnUpdateStatut.Click += (source, args) =>
                            {
                                //Lien vers la DB MongoDB
                                var database = DALMongo.GetDatabase();
                                //Lien vers la Collection de la DB
                                var collection = database.GetCollection<BsonDocument>("video_games");

                                //Statut de la commande quand le client est venu la récupérer en magasin
                                String statutCommand = "Livré";

                                //Récupération du nom du jeu pour update le statut de la commande
                                String titleGame = LblTitle.Text;

                                //Récupération de la date du jour pour la date de retrait de commande
                                var retraitCommand = DateTime.Now;

                                //Update du statut des commandes par jeux
                                bool isOk = false;
                                isOk = objDal.UpdateStatutCommand(idClient, titleGame, statutCommand);

                                if (isOk == true)
                                {
                                    if (priceDiscount <= 0)
                                    {
                                        if (double.TryParse(LblPrice.Text, out double price))
                                        {
                                            // Pour insérer un document
                                            var document = new BsonDocument
                                                    {
                                                        { "name", titleGame },
                                                        { "price", price },
                                                        { "date", retraitCommand }
                                                    };

                                            collection.InsertOne(document);
                                            Alert.Show("Commande validé avec succès");
                                            MaJCommand();
                                        }
                                        else
                                        {

                                        }
                                    }
                                    else
                                    {
                                        //On convertit le prix en String pour substring
                                        String lastPrice;
                                        lastPrice = priceDiscount.ToString();

                                        //On récupére le nombre de caractère total
                                        int tailleLastPrice = 0;
                                        tailleLastPrice = lastPrice.Length;


                                        if (lastPrice.Contains(","))
                                        {
                                            if (tailleLastPrice > 5 || tailleLastPrice == 4)
                                            {
                                                if (tailleLastPrice == 4)
                                                {
                                                    lastPrice = lastPrice + "0";
                                                    lastPrice = lastPrice.Substring(0, 5);

                                                    //Insertion dans DB MongoDB
                                                    var document = new BsonDocument
                                                            {
                                                                { "name", titleGame },
                                                                { "price", lastPrice },
                                                                { "date", retraitCommand }
                                                            };

                                                    collection.InsertOne(document);
                                                    Alert.Show("Commande validé avec succès");
                                                    MaJCommand();
                                                }
                                                else
                                                {
                                                    lastPrice = lastPrice.Substring(0, 5);

                                                    //Insertion dans DB MongoDB
                                                    var document = new BsonDocument
                                                            {
                                                                { "name", titleGame },
                                                                { "price", lastPrice },
                                                                { "date", retraitCommand }
                                                            };

                                                    collection.InsertOne(document);
                                                    Alert.Show("Commande validé avec succès");
                                                    MaJCommand();
                                                }
                                            }
                                            else if (tailleLastPrice == 5)
                                            {
                                                lastPrice = lastPrice.Substring(0, 5);

                                                //Insertion dans DB MongoDB
                                                var document = new BsonDocument
                                                            {
                                                                { "name", titleGame },
                                                                { "price", lastPrice },
                                                                { "date", retraitCommand }
                                                            };

                                                collection.InsertOne(document);
                                                Alert.Show("Commande validé avec succès");
                                                MaJCommand();
                                            }
                                            else if (tailleLastPrice == 2)
                                            {
                                                lastPrice = lastPrice.Substring(0, 2);

                                                //Insertion dans DB MongoDB
                                                var document = new BsonDocument
                                                            {
                                                                { "name", titleGame },
                                                                { "price", lastPrice },
                                                                { "date", retraitCommand }
                                                            };

                                                collection.InsertOne(document);
                                                Alert.Show("Commande validé avec succès");
                                                MaJCommand();
                                            }
                                            else if (tailleLastPrice == 1)
                                            {
                                                lastPrice = lastPrice.Substring(0, 1);

                                                //Insertion dans DB MongoDB
                                                var document = new BsonDocument
                                                            {
                                                                { "name", titleGame },
                                                                { "price", lastPrice },
                                                                { "date", retraitCommand }
                                                            };

                                                collection.InsertOne(document);
                                                Alert.Show("Commande validé avec succès");
                                                MaJCommand();
                                            }
                                        }
                                    }
                                }
                            };

                            //Affichage dynamique
                            PnlHistCommandClient.Controls.Add(new LiteralControl(sb0.ToString()));
                            PnlHistCommandClient.Controls.Add(new LiteralControl(sb1.ToString()));
                            PnlHistCommandClient.Controls.Add(LblStatutCommand);
                            PnlHistCommandClient.Controls.Add(new LiteralControl(sb2.ToString()));
                            PnlHistCommandClient.Controls.Add(new LiteralControl(sb2.ToString()));
                            PnlHistCommandClient.Controls.Add(ImgGame);
                            PnlHistCommandClient.Controls.Add(new LiteralControl("&nbsp;"));
                            PnlHistCommandClient.Controls.Add(new LiteralControl("&nbsp;"));
                            PnlHistCommandClient.Controls.Add(new LiteralControl(sb3.ToString()));
                            PnlHistCommandClient.Controls.Add(LblTitle);
                            PnlHistCommandClient.Controls.Add(new LiteralControl(sb2.ToString()));
                            PnlHistCommandClient.Controls.Add(new LiteralControl(sb2.ToString()));
                            PnlHistCommandClient.Controls.Add(LblPrice);
                            PnlHistCommandClient.Controls.Add(new LiteralControl(sb4.ToString()));
                            PnlHistCommandClient.Controls.Add(new LiteralControl(sb5.ToString()));
                            PnlHistCommandClient.Controls.Add(BtnUpdateStatut);
                            PnlHistCommandClient.Controls.Add(new LiteralControl(sb6.ToString()));
                            PnlHistCommandClient.Controls.Add(new LiteralControl(sb6.ToString()));
                            PnlHistCommandClient.Controls.Add(new LiteralControl(sb2.ToString()));
                            PnlHistCommandClient.Controls.Add(new LiteralControl(sb2.ToString()));
                            PnlHistCommandClient.Controls.Add(new LiteralControl(sb6.ToString()));
                            PnlHistCommandClient.Controls.Add(new LiteralControl(sb6.ToString()));
                            PnlHistCommandClient.Controls.Add(new LiteralControl(sb2.ToString()));
                        }
                        //Récupération des emails des utilisateurs ayant des commandes à retirer
                        List<String> listEmailUsers = new List<String>();
                        listEmailUsers = objDal.RecupEmailClientWithCommand();

                        //On vide le DropDownList avant de mettre les données dedans
                        DdlCmdClient.Items.Clear();

                        //Ajout des emails dans le DropDownList
                        DdlCmdClient.Items.Add("");
                        foreach (var email in listEmailUsers)
                        {
                            DdlCmdClient.Items.Add(email);
                        }

                        //On récupére l'email du client avec son id_client pour le laisser afficher dans le DropDownList
                        String emailClient;
                        emailClient = objDal.RecupEmailWithClientID(idClient);

                        if (DdlCmdClient.Items.Count > 0)
                        {
                            DdlCmdClient.Text = emailClient;
                        }
                    }
                    else
                    {
                        PnlHistCommandClient.Visible = false;
                        //Récupération des emails des utilisateurs ayant des commandes à retirer
                        List<String> listEmailUsers = new List<String>();
                        listEmailUsers = objDal.RecupEmailClientWithCommand();

                        //On vide le DropDownList avant de mettre les données dedans
                        DdlCmdClient.Items.Clear();

                        //Ajout des emails dans le DropDownList
                        DdlCmdClient.Items.Add("");
                        foreach (var email in listEmailUsers)
                        {
                            DdlCmdClient.Items.Add(email);
                        }

                        //On récupére l'email du client avec son id_client pour le laisser afficher dans le DropDownList
                        String emailClient;
                        emailClient = objDal.RecupEmailWithClientID(idClient);

                        if (DdlCmdClient.Items.Count > 1)
                        {
                            DdlCmdClient.Text = emailClient;
                        }
                    }
                }
            }
        }

        protected void BtnReadSale_Click(object sender, EventArgs e)
        {
            PnlHistCommandClient.Visible = false;
            PnlReadSale.Visible = true;
            PnlSaleByGenre.Visible = false;
            PnlSaleByItem.Visible = false;
            DdlCmdClient.Visible = false;
            DdlCmdClient.Text = "";
            BtnDetailsChart.Visible = false;
            title_menu.Visible = true;
            title_menu.InnerText = "Veuillez choisir le graphique que vous souhaitez voir";
        }

        protected void BtnDetailsChart_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/DetailsChart");
        }

        protected void BtnChartByItem_Click(object sender, EventArgs e)
        {
            PnlSaleByGenre.Visible = false;
            PnlSaleByItem.Visible = true;
            BtnDetailsChart.Visible = true;
            title_menu.Visible = true;
            title_menu.InnerText = "Ventes de jeu vidéo par Article";

            var salesData = GetSalesDataByItem();
            string jsonSalesData = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(salesData);
            ScriptManager.RegisterStartupScript(this, GetType(), "initializeChartItem", $"initializeChartItem({jsonSalesData});", true);
        }

        public Dictionary<string, Dictionary<string, int>> GetSalesDataByItem()
        {
            Dictionary<string, Dictionary<string, int>> salesData = new Dictionary<string, Dictionary<string, int>>();

            salesData = objMongo.GetSalesDataByItem();
            return salesData;
        }

        protected void BtnChartByGenre_Click(object sender, EventArgs e)
        {
            PnlSaleByGenre.Visible = true;
            PnlSaleByItem.Visible = false;
            BtnDetailsChart.Visible = true;
            title_menu.Visible = true;
            title_menu.InnerText = "Ventes de jeu vidéo par Genre";

            var salesData = GetSalesDataByGenre();
            string jsonSalesData = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(salesData);
            ScriptManager.RegisterStartupScript(this, GetType(), "initializeChartGenre", $"initializeChartGenre({jsonSalesData});", true);
        }

        public Dictionary<string, Dictionary<string, int>> GetSalesDataByGenre()
        {
            Dictionary<string, Dictionary<string, int>> salesData = new Dictionary<string, Dictionary<string, int>>();

            salesData = objDal.GetSalesByGenre();
            return salesData;
        }
    }
}