<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Jeuxvideo.aspx.cs" Inherits="Gamestore.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <link href="/Content/ContentCSS/Jeuxvideo.css" type="text/css" rel="stylesheet" />

    <main aria-labelledby="title">
        <br />
        <br />
        <h3 class="title_default">Voici tout nos jeux vidéos !</h3>
        <br />
        <br />
        <asp:DropDownList ID="DDLGenre" runat="server" CssClass="DDLFilterGenre"></asp:DropDownList>
        <asp:DropDownList ID="DDLPrice" runat="server" CssClass="DDLFilterPrice"></asp:DropDownList>
        <asp:DropDownList ID="DDLPlateforme" runat="server" CssClass="DDLFilterPlateforme"></asp:DropDownList>
        <div class="grid">
            <div class="info"><img src="Content/ContentIMG/got.jpg" class="img_game" /></div>
            <div class="info"><img src="Content/ContentIMG/ml.jpg" class="img_game" /></div>
            <div class="info"><img src="Content/ContentIMG/vr.jpg" class="img_game" /></div>
            <div class="info"><img src="Content/ContentIMG/wayfinder.jpg" class="img_game" /></div>
            <div class="info"><a runat="server" href="~/DetailJeuxVideo"><img src="Content/ContentIMG/aoe4.jpg" class="img_game" /></a></div>
            <div class="info"><img src="Content/ContentIMG/roboquest.jpg" class="img_game" /></div> 
        </div>
        <br />
        <br />
    </main>
</asp:Content>
