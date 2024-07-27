<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetailJeuxVideo.aspx.cs" Inherits="Gamestore.DetailJeuxVideo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" type="text/css" meedia="screen" href="/Content/ContentCSS/DetailJeuxVideo.css" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.5.0/font/bootstrap-icons.css">

    <div class="content_wrapper">
        <div class="photo_wrapper">
            <div class="img_wrapper">
                <asp:Image ID="IMGGame" CssClass="img_jv1" runat="server" />
            </div>
            <div class="panel_infos">
                <div class="info_wrapper">
                    <h1 class="title_container">
                        <asp:Label ID="LblTitleGame" runat="server" Text=""></asp:Label>
                    </h1>
                </div>
                <div class="subinfos">
                    <div class="stock">
                        <div runat="server" id="bi_check" class="bi bi-check h3"></div>
                        <asp:Label ID="LblStock" runat="server" Text=""></asp:Label>
                        <div runat="server" id="bi_cross" class="b bi-info-circle-fill h5"></div>
                        &nbsp;
                        <asp:Label ID="LblStockVide" runat="server" Text=""></asp:Label>
                        <div class="spacer"></div>
                    </div>
                    <div class="pegi">
                        <asp:Label ID="LblPEGI" runat="server" Text=""></asp:Label>
                        <div class="spacer"></div>
                    </div>
                    <div class="genre">
                        <asp:Label ID="LblGenre" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <br />
                <br />
                <div class="amount">
                    <br />
                    <asp:Label ID="LblPrice" CssClass="LblPrice" runat="server" Text=""></asp:Label>
                    &nbsp;
                    &nbsp;
                    <asp:Label ID="LblDiscount" CssClass="LblDiscount" runat="server" Text="" Visible="false"></asp:Label>
                    &nbsp;
                    &nbsp;
                    <asp:Label ID="LblPriceDiscount" CssClass="LblPriceDiscount" runat="server" Text="" Visible="false"></asp:Label>
                    <br />
                    <br />
                </div>
                <div class="action">
                    <div runat="server" id="div_check" class="bi bi-check h3"></div>
                    <asp:Label ID="LblAlreadyInCart" CssClass="already_cart" runat="server" Text="Le jeu est déjà dans votre Panier"></asp:Label>
                    <asp:Button ID="BtnAddCart" CssClass="btn btn-primary btn-lg" runat="server" Text="Ajouter au Panier" OnClick="BtnAddCart_Click" />
                </div>
            </div>
        </div>
    </div>
    <div class="details">
        <div class="description">
            <div class="head">
                <h2>Description du jeu :</h2>
            </div>
            <asp:BulletedList ID="BulletListDescription" runat="server" style="list-style-type: none;"></asp:BulletedList>
        </div>
    </div>
</asp:Content>
