<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Jeuxvideo.aspx.cs" Inherits="Gamestore.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <link href="/Content/ContentCSS/Jeuxvideo.css" type="text/css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script type="text/javascript" src="Scripts/ContentJS/FilterJV.js"></script> 

    <main aria-labelledby="title">
        <br />
        <br />
        <h3 class="title_default">Voici tout nos jeux vidéos !</h3>
        <br />
        <br />
        <div id="filters-container" class="filters_container">
            <div class="title_filter">
                <p>Filtre par Genre</p>
            </div>
            <div class="content_genre_filter">
                <asp:CheckBoxList ID="CheckBoxGenre" CssClass="check_filter" runat="server"></asp:CheckBoxList>
            </div>
        </div>
        <br />
        
        <div class="price_filter">
            <div class="title_filter">
                <p>Filtre par Prix</p>
            </div>
            <div class="content_genre_filter">
                <asp:CheckBoxList ID="CheckBoxPrice" runat="server" CssClass="checkbox-list">
                    <asp:ListItem Value="0-20">0-20€</asp:ListItem>
                    <asp:ListItem Value="20-40">20-40€</asp:ListItem>
                    <asp:ListItem Value="40-60">40-60€</asp:ListItem>
                    <asp:ListItem Value="60+">Plus de 60€</asp:ListItem>
                </asp:CheckBoxList>
            </div>
        </div>
        <asp:Panel ID="PnlGame" runat="server">
        </asp:Panel>
        <br />
        <br />
    </main>

</asp:Content>