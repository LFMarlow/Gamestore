<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MonCompteEmploye.aspx.cs" Inherits="Gamestore.MonCompteEmploye" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" type="text/css" href="Content/ContentCSS/MonCompteEmploye.css" />
    <script type="text/javascript" src="Scripts/ContentJS/GraphParArticle.js"></script>
    <script type="text/javascript" src="Scripts/ContentJS/GraphParGenre.js"></script>

    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/chartjs-adapter-date-fns"></script>

    <section class="main_account">
        <div class="div_aside">
            <aside class="left_aside">
                <div class="hello">
                    <p>
                        <strong>Bonjour</strong>
                        <br />
                        <em>
                            <asp:Label ID="LblUsers" runat="server" Text=""></asp:Label>
                        </em>
                    </p>
                    <asp:Button ID="BtnDisconnect" runat="server" CssClass="btnDisconnect" Text="Me Deconnecter" OnClick="BtnDisconnect_Click" />
                </div>
            </aside>
            <aside class="right_aside">
                <div class="menu_interact">
                    <asp:Button ID="BtnValidateCmd" runat="server" CssClass="" Text="Validation des commandes" OnClick="BtnValidateCmd_Click" />
                    <asp:Button ID="BtnReadSale" runat="server" CssClass="" Text="Affichage des ventes" OnClick="BtnReadSale_Click" />
                </div>
            </aside>
        </div>
        <br />
        <br />
        <h2 id="title_menu" class="title_menu" runat="server"></h2>
        <br />
        <asp:DropDownList ID="DdlCmdClient" runat="server" AutoPostBack="true"></asp:DropDownList>
        <br />
        <br />
        <asp:Panel ID="PnlHistCommandClient" runat="server">
        </asp:Panel>
        <asp:Panel ID="PnlReadSale" runat="server">
            <asp:Button ID="BtnChartByGenre" runat="server" Text="Vente par Genre" OnClick="BtnChartByGenre_Click" OnClientClick="return graphByGenreClick();" />
            <asp:Button ID="BtnChartByItem" runat="server" Text="Vente par Article" OnClick="BtnChartByItem_Click" OnClientClick="return graphByItemClick();" />
        </asp:Panel>
        <br />
        <br />
        <asp:Panel ID="PnlSaleByGenre" runat="server">
            <div id="GraphVenteGenre" class="graph_vente">
                <canvas id="salesChart" width="400" height="400"></canvas>
            </div>
            <br />
        </asp:Panel>
        <asp:Panel ID="PnlSaleByItem" runat="server">
            <div id="GraphVenteArticle" class="graph_vente">
                <canvas id="salesChartArticle" width="400" height="400"></canvas>
            </div>
            <br />
        </asp:Panel>
        <asp:Button ID="BtnDetailsChart" runat="server" Text="Détails" OnClick="BtnDetailsChart_Click" />
    </section>
</asp:Content>
