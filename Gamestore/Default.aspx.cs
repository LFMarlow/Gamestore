using Gamestore.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Gamestore
{
    public partial class _Default : Page
    {
        DALGamestore objDal = new DALGamestore();
        protected void Page_Load(object sender, EventArgs e)
        {
            //Initialisation du 1er carousel pour les derniers jeux enregistré
            List<String> listLastGame = new List<String>();
            List<String> listLastTitleGame = new List<String>();

            listLastGame = objDal.RecupLastJeuxVideo();
            listLastTitleGame = objDal.RecupLastTitleJeuxVideo();

            for (int i = 0; i < listLastGame.Count; i++)
            {
                if (Image0.ImageUrl == "")
                {
                    Image0.ImageUrl = listLastGame[i];
                    img0.HRef = "~/DetailJeuxVideo?" + listLastTitleGame[i].Replace(" ", "_");

                }
                else if (Image1.ImageUrl == "")
                {
                    Image1.ImageUrl = listLastGame[i];
                    img1.HRef = "~/DetailJeuxVideo?" + listLastTitleGame[i].Replace(" ", "_");
                }
                else if (Image2.ImageUrl == "")
                {
                    Image2.ImageUrl = listLastGame[i];
                    img2.HRef = "~/DetailJeuxVideo?" + listLastTitleGame[i].Replace(" ", "_");
                }
                else if (Image3.ImageUrl == "")
                {
                    Image3.ImageUrl = listLastGame[i];
                    img3.HRef = "~/DetailJeuxVideo?" + listLastTitleGame[i].Replace(" ", "_");
                }
                else if (Image4.ImageUrl == "")
                {
                    Image4.ImageUrl = listLastGame[i];
                    img4.HRef = "~/DetailJeuxVideo?" + listLastTitleGame[i].Replace(" ", "_");
                }
                else if (Image5.ImageUrl == "")
                {
                    Image5.ImageUrl = listLastGame[i];
                    img5.HRef = "~/DetailJeuxVideo?" + listLastTitleGame[i].Replace(" ", "_");
                }
                else if (Image6.ImageUrl == "")
                {
                    Image6.ImageUrl = listLastGame[i];
                    img6.HRef = "~/DetailJeuxVideo?" + listLastTitleGame[i].Replace(" ", "_");
                }
                else if (Image7.ImageUrl == "")
                {
                    Image7.ImageUrl = listLastGame[i];
                    img7.HRef = "~/DetailJeuxVideo?" + listLastTitleGame[i].Replace(" ", "_");
                }
                else if (Image8.ImageUrl == "")
                {
                    Image8.ImageUrl = listLastGame[i];
                    img8.HRef = "~/DetailJeuxVideo?" + listLastTitleGame[i].Replace(" ", "_");
                }
                else if (Image9.ImageUrl == "")
                {
                    Image9.ImageUrl = listLastGame[i];
                    img9.HRef = "~/DetailJeuxVideo?" + listLastTitleGame[i].Replace(" ", "_");
                }
            }

            //Initialisation du 2ème carousel pour les promotions
            List<String> listReductionGame = new List<String>();
            List<String> listReductionTitleGame = new List<String>();
            List<float> listDiscountReduction = new List<float>();

            listReductionGame = objDal.RecupLastPromotedJeuxVideo();
            listReductionTitleGame = objDal.RecupLastTitlePromotedJeuxVideo();
            listDiscountReduction = objDal.RecupDiscountPromotedJeuxVideo();

            if (listReductionGame.Count > 0)
            {
                for (int j = 0; j < listReductionGame.Count; j++)
                {
                    if (ImgReduc0.ImageUrl == "")
                    {
                        ImgReduc0.ImageUrl = listReductionGame[j];
                        A0.HRef = "~/DetailJeuxVideo?" + listReductionTitleGame[j].Replace(" ", "_");
                        LblDiscount0.Text = "-" + listDiscountReduction[j].ToString() + "%";

                    }
                    else if (ImgReduc1.ImageUrl == "")
                    {
                        ImgReduc1.ImageUrl = listReductionGame[j];
                        A1.HRef = "~/DetailJeuxVideo?" + listReductionTitleGame[j].Replace(" ", "_");
                        LblDiscount1.Text = "-" + listDiscountReduction[j].ToString() + "%";

                    }
                    else if (ImgReduc2.ImageUrl == "")
                    {
                        ImgReduc2.ImageUrl = listReductionGame[j];
                        A2.HRef = "~/DetailJeuxVideo?" + listReductionTitleGame[j].Replace(" ", "_");
                        LblDiscount2.Text = "-" + listDiscountReduction[j].ToString() + "%";

                    }
                    else if (ImgReduc3.ImageUrl == "")
                    {
                        ImgReduc3.ImageUrl = listReductionGame[j];
                        A3.HRef = "~/DetailJeuxVideo?" + listReductionTitleGame[j].Replace(" ", "_");
                        LblDiscount3.Text = "-" + listDiscountReduction[j].ToString() + "%";

                    }
                    else if (ImgReduc4.ImageUrl == "")
                    {
                        ImgReduc4.ImageUrl = listReductionGame[j];
                        A4.HRef = "~/DetailJeuxVideo?" + listReductionTitleGame[j].Replace(" ", "_");
                        LblDiscount4.Text = "-" + listDiscountReduction[j].ToString() + "%";
                    }
                    else if (ImgReduc5.ImageUrl == "")
                    {
                        ImgReduc5.ImageUrl = listReductionGame[j];
                        A5.HRef = "~/DetailJeuxVideo?" + listReductionTitleGame[j].Replace(" ", "_");
                        LblDiscount5.Text = "-" + listDiscountReduction[j].ToString() + "%";
                    }
                    else if (ImgReduc6.ImageUrl == "")
                    {
                        ImgReduc6.ImageUrl = listReductionGame[j];
                        A6.HRef = "~/DetailJeuxVideo?" + listReductionTitleGame[j].Replace(" ", "_");
                        LblDiscount6.Text = "-" + listDiscountReduction[j].ToString() + "%";
                    }
                    else if (ImgReduc7.ImageUrl == "")
                    {
                        ImgReduc7.ImageUrl = listReductionGame[j];
                        A7.HRef = "~/DetailJeuxVideo?" + listReductionTitleGame[j].Replace(" ", "_");
                        LblDiscount7.Text = "-" + listDiscountReduction[j].ToString() + "%";
                    }
                    else if (ImgReduc8.ImageUrl == "")
                    {
                        ImgReduc8.ImageUrl = listReductionGame[j];
                        A8.HRef = "~/DetailJeuxVideo?" + listReductionTitleGame[j].Replace(" ", "_");
                        LblDiscount8.Text = "-" + listDiscountReduction[j].ToString() + "%";
                    }
                    else if (ImgReduc9.ImageUrl == "")
                    {
                        ImgReduc9.ImageUrl = listReductionGame[j];
                        A9.HRef = "~/DetailJeuxVideo?" + listReductionTitleGame[j].Replace(" ", "_");
                        LblDiscount9.Text = "-" + listDiscountReduction[j].ToString() + "%";
                    }
                }
            }
            else
            {
                title_promo.Visible = false;
            }
        }
    }
}