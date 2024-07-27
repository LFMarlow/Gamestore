<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Panier.aspx.cs" Inherits="Gamestore.Panier" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link type="text/css" rel="stylesheet" href="Content/ContentCSS/Panier.css" />   
    <script type="text/javascript" src="Scripts/ContentJS/Location.js"></script>

    <div class="title_cart">
        <h2>Résumé de votre panier :</h2>
    </div>
    <br />
    <asp:Panel ID="PnlCart" CssClass="PnlCart" runat="server">
        <h3><asp:Label ID="LblCartEmpty" runat="server" CssClass="LblEmpty" Text="Votre Panier est vide"></asp:Label></h3>
    </asp:Panel>
    <asp:Panel ID="PnlSubmitCart" CssClass="PnlSubmitCart" runat="server">
        <asp:Label ID="Label1" runat="server" Text="Veuillez Choisir une date de retrait :"></asp:Label>
        <div id="calendarContainer">
            <asp:Calendar ID="CalendarCart" AutoPostBack="true" CssClass="Calendar_cart" runat="server" ForeColor="White" OnDayRender="CalendarCart_DayRender"></asp:Calendar>
        </div>
        <br />
        <asp:Label ID="LblStoreNearUser" runat="server" Text="L'agence la plus proche de chez vous est : "></asp:Label>
        <asp:Label ID="LblStoreNearUserReal" runat="server" Text="" Font-Bold="True"></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label3" runat="server" Text="Voius pouvez choisir une autre agence si vous le souhaitez : "></asp:Label>
        <asp:DropDownList ID="DdlNameStore" runat="server" AutoPostBack="true"></asp:DropDownList>
        <br />
        <br />
        <asp:Label ID="LblTotal" runat="server" Text="Total Estimé : " Font-Bold="True"></asp:Label>
        <asp:Label ID="LblTotalPrice" runat="server" Text="" Font-Bold="True"></asp:Label>
        <br />
        <br />
        <asp:Button ID="BtnValiderCart" CssClass="btn btn-primary btn-lg" runat="server" Text="Valider le Panier" OnClick="BtnValiderCart_Click" />
    </asp:Panel>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
</asp:Content>
