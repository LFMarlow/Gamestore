using Gamestore.Classes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gamestore
{
    public partial class DetailsChart : System.Web.UI.Page
    {
        DALMongo objMongo = new DALMongo();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(Session["EstConnecte"]))
            {
                if (Session["RoleUtilisateur"].ToString() == "Administrateur" || Session["RoleUtilisateur"].ToString() == "Employé")
                {
                    if (!IsPostBack)
                    {
                        //Ajout des mois dans le DropDownList
                        var saleMonths = objMongo.GetDistinctSaleMonths();
                        DdlSaleMonth.DataSource = saleMonths;
                        DdlSaleMonth.DataBind();
                        DdlSaleMonth.Items.Insert(0, new ListItem("-- Sélectionner un mois --", ""));

                        DdlSaleDates.Visible = false;
                        DdlSaleGames.Visible = false;
                        LblFiltrageDate.Visible = false;
                        LblFiltrageGame.Visible = false;
                    }

                    //Ajout de toutes les informations dans le DateGridView
                    GridViewSalesSummary.DataSource = objMongo.GetSalesSummary();
                    GridViewSalesSummary.DataBind();



                }
                else
                {
                    Response.Redirect("~/Connexion");
                }
            }
            else
            {
                Response.Redirect("~/Connexion");
            }
        }

        protected void ddlSaleDates_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Vérifiez si une date valide est sélectionnée (ignorer l'option "-- Sélectionner une date --")
            if (!string.IsNullOrEmpty(DdlSaleDates.SelectedValue))
            {
                // Filtre les ventes par la date sélectionnée
                GridViewSalesSummary.DataSource = objMongo.GetSalesSummaryByDate(DdlSaleDates.SelectedValue);
                GridViewSalesSummary.DataBind();

                DdlSaleGames.SelectedIndex = 0;
            }
            else
            {
                // Si aucune date n'est sélectionnée, affichez toutes les ventes
                GridViewSalesSummary.DataSource = objMongo.GetSalesSummary();
                GridViewSalesSummary.DataBind();
            }
        }

        protected void ddlSaleGames_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Vérifiez si une date valide est sélectionnée (ignorer l'option "-- Sélectionner un jeu --")
            if (!string.IsNullOrEmpty(DdlSaleGames.SelectedValue))
            {
                string selectedMonthYear = DdlSaleMonth.SelectedValue;
                string[] parts = selectedMonthYear.Split(' ');
                if (parts.Length == 2)
                {
                    int year1 = int.Parse(parts[1]);
                    String month0 = parts[0];

                    // Filtre les ventes en fonction du jeu sélectionné
                    GridViewSalesSummary.DataSource = objMongo.GetSalesSummaryByGames(year1, month0, DdlSaleGames.SelectedValue);
                    GridViewSalesSummary.DataBind();

                    DdlSaleDates.SelectedIndex = 0;
                }
            }
            else
            {
                string selectedMonthYear = DdlSaleMonth.SelectedValue;
                int year = int.Parse(selectedMonthYear.Split(' ')[1]);
                int month = DateTime.ParseExact(selectedMonthYear.Split(' ')[0], "MMMM", CultureInfo.CurrentCulture).Month;
                // Si aucun jeu n'est sélectionné, affichez toutes les ventes
                GridViewSalesSummary.DataSource = objMongo.GetSalesSummaryByMonth(year, month);
                GridViewSalesSummary.DataBind();

            }
        }

        protected void ddlSaleMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DdlSaleMonth.SelectedIndex > 0)
            {
                string selectedMonthYear = DdlSaleMonth.SelectedValue;
                int year = int.Parse(selectedMonthYear.Split(' ')[1]);
                int month = DateTime.ParseExact(selectedMonthYear.Split(' ')[0], "MMMM", CultureInfo.CurrentCulture).Month;

                GridViewSalesSummary.DataSource = objMongo.GetSalesSummaryByMonth(year, month);
                GridViewSalesSummary.DataBind();

                //Ajout des dates de ventes dans le DropDownList
                string[] parts = selectedMonthYear.Split(' ');
                if (parts.Length == 2)
                {
                    int year1 = int.Parse(parts[1]);
                    String month0 = parts[0];
                    var saleDates = objMongo.GetDistinctSaleDates(year1, month0);
                    DdlSaleDates.DataSource = saleDates;
                    DdlSaleDates.DataBind();
                    DdlSaleDates.Items.Insert(0, new ListItem("-- Sélectionner une date --", ""));

                    //Ajout des jeux présent dans la Base dans le DropDownList
                    var saleGames = objMongo.GetDistinctSaleGames(year1, month0);
                    DdlSaleGames.DataSource = saleGames;
                    DdlSaleGames.DataBind();
                    DdlSaleGames.Items.Insert(0, new ListItem("-- Sélectionner un jeu --", ""));

                    var salesData = objMongo.GetSalesComparison(selectedMonthYear);

                    List<Tuple<Vente, string>> dataSource = new List<Tuple<Vente, string>>();

                    // Vérifie si la clé "CurrentMonth" existe dans le dictionnaire avant d'accéder aux données
                    if (salesData.ContainsKey("CurrentMonth"))
                    {
                        dataSource.Add(salesData["CurrentMonth"]);
                    }

                    // Vérifie si la clé "PreviousMonth" existe dans le dictionnaire avant d'accéder aux données
                    if (salesData.ContainsKey("PreviousMonth"))
                    {
                        dataSource.Add(salesData["PreviousMonth"]);
                    }

                    GridViewComparedSalesSummary.DataSource = dataSource;
                    GridViewComparedSalesSummary.DataBind();

                    DdlSaleDates.Visible = true;
                    DdlSaleGames.Visible = true;
                    LblFiltrageDate.Visible = true;
                    LblFiltrageGame.Visible = true;
                }
                else
                {
                    DdlSaleDates.Visible = false;
                    DdlSaleGames.Visible = false;
                    LblFiltrageDate.Visible = false;
                    LblFiltrageGame.Visible = false;
                    GridViewComparedSalesSummary.Visible = false;
                }
            }
            else
            {
                GridViewSalesSummary.DataSource = objMongo.GetSalesSummary();
                GridViewSalesSummary.DataBind();

                DdlSaleDates.Visible = false;
                DdlSaleGames.Visible = false;
                LblFiltrageDate.Visible = false;
                LblFiltrageGame.Visible = false;
            }
        }


    }
}