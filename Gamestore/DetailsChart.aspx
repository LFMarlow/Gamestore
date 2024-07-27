<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetailsChart.aspx.cs" Inherits="Gamestore.DetailsChart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" type="text/css" href="Content/ContentCSS/DetailsChart.css" />

    <br />
    <h2 class="title_menu">Détails de toutes les ventes</h2>
    <br />
    <br />
    <div class="filter_month">
        <asp:Label ID="LblFilterMonth" runat="server" CssClass="Lbl" Text="Filtrage par Mois"></asp:Label>
        <br />
        <asp:DropDownList ID="DdlSaleMonth" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSaleMonth_SelectedIndexChanged"></asp:DropDownList>
    </div>
    <div class="filter_date">
        <asp:Label ID="LblFiltrageDate" runat="server" CssClass="Lbl" Text="Filtrage par Date"></asp:Label>
        <br />
        <asp:DropDownList ID="DdlSaleDates" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSaleDates_SelectedIndexChanged"></asp:DropDownList>
    </div>
    <div class="filter_game">
        <asp:Label ID="LblFiltrageGame" runat="server" CssClass="Lbl" Text="Filtrage par Jeu"></asp:Label>
        <br />
        <asp:DropDownList ID="DdlSaleGames" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSaleGames_SelectedIndexChanged"></asp:DropDownList>
    </div>
    <br />
    <br />
    <br />
    <div class="grid_summary">
        <asp:GridView ID="GridViewSalesSummary" CssClass="GridViewSalesSummary" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="DateVente" HeaderText="Date de Vente" SortExpression="DateVente" />
                <asp:BoundField DataField="NomArticle" HeaderText="Nom du Jeu" SortExpression="NomArticle" />
                <asp:BoundField DataField="NombreVentes" HeaderText="Nombre de Ventes" SortExpression="NombreVentes" />
                <asp:BoundField DataField="PrixTotal" HeaderText="Prix Total" SortExpression="PrixTotal" DataFormatString="{0:C}" />
            </Columns>
        </asp:GridView>
    </div>
    <br />
    <br />
    <div class="grid_summary_compared">
        <asp:GridView ID="GridViewComparedSalesSummary" runat="server" AutoGenerateColumns="false" CssClass="gridview-container">
            <Columns>
                <asp:TemplateField HeaderText="Mois">
                    <ItemTemplate>
                        <div class="gridview-item">
                            <div class="gridview-header">Mois</div>
                            <div class="gridview-data"><span class='<%# Eval("Item2") %>'><%# Eval("Item1.Mois") %></span></div>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Nombre Total de Ventes">
                    <ItemTemplate>
                        <div class="gridview-item">
                            <div class="gridview-header">Ventes</div>
                            <div class="gridview-data"><span class='<%# Eval("Item2") %>'><%# Eval("Item1.NombreVentes") %></div>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Prix Total des Ventes">
                    <ItemTemplate>
                        <div class="gridview-item">
                            <div class="gridview-header">Prix Total</div>
                            <div class="gridview-data"><span class='<%# Eval("Item2") %>'>€<%# Eval("Item1.PrixTotal", "{0:N2}") %></div>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
