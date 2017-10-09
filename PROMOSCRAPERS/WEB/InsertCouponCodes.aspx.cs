using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity;
using BusinessLogic;
using System.Text;
using System.IO;

public partial class InsertCouponCodes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            cboProducts.DataSource = EntityBLL.ProductList;
            cboProducts.DataTextField = "Name";
            cboProducts.DataValueField = "ID";
            cboProducts.DataBind();

            cboProducts.Items.Insert(0, new ListItem("", "0")); 
        }
        

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        CouponEntity ce = new CouponEntity();

        int iProductID = int.Parse(cboProducts.SelectedItem.Value);    



        List<CouponEntity> lCE = new List<CouponEntity>();
        CouponEntity cee = new CouponEntity();

        string txt = txtCouponCodes.Text;
        string[] lst = txt.Split(new Char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

        for(int i=0;i<lst.Length;i++)
        {
            cee.CouponCode = lst[i].Trim();
            lCE.Add(cee);
            cee = new CouponEntity();
        }

        List<KeywordEntity> lke = new List<KeywordEntity>();
        KeywordEntity ke = new KeywordEntity();

        if(txtKeyword1.Text != string.Empty && txtPercentage1.Text != string.Empty)
        {
            ke.KeyWord = txtKeyword1.Text.Trim();
            ke.Percent = int.Parse(txtPercentage1.Text);
            lke.Add(ke);           
        }

        if (txtKeyword2.Text != string.Empty && txtPercentage2.Text != string.Empty)
        {
            ke = new KeywordEntity();
            ke.KeyWord = txtKeyword2.Text.Trim();
            ke.Percent = int.Parse(txtPercentage2.Text);
            lke.Add(ke);           
        }


        if (txtKeyword3.Text != string.Empty && txtPercentage3.Text != string.Empty)
        {
            ke = new KeywordEntity();
            ke.KeyWord = txtKeyword3.Text.Trim();
            ke.Percent = int.Parse(txtPercentage3.Text);
            lke.Add(ke);            
        }

        if (txtKeyword4.Text != string.Empty && txtPercentage4.Text != string.Empty)
        {
            ke = new KeywordEntity();
            ke.KeyWord = txtKeyword4.Text.Trim();
            ke.Percent = int.Parse(txtPercentage4.Text);
            lke.Add(ke);            
        }

        if (txtKeyword5.Text != string.Empty && txtPercentage5.Text != string.Empty)
        {
            ke = new KeywordEntity();
            ke.KeyWord = txtKeyword5.Text.Trim();
            ke.Percent = int.Parse(txtPercentage5.Text);
            lke.Add(ke);            
        }


        //do validation, total % must be exactly 100 and keyword may not repeat more than once

        lblConfirm.Visible = EntityBLL.InsertCouponCodesAndKeywords(iProductID, lCE, lke);

    }
}