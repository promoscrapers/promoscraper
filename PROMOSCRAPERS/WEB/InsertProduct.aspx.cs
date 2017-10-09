using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity;
using BusinessLogic;

public partial class InsertProduct : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        KotletaProduct prd = new KotletaProduct();
        prd.AmazonASIN = txtAmazonASIN.Text.Trim();
        prd.Name = txtProductName.Text.Trim();
        prd.ImageURL = txtImageURL.Text.Trim();
        prd.Price = decimal.Parse(txtPrice.Text.Trim());
        lblConfirm.Visible = EntityBLL.InsertNewProduct(prd);

    }
}