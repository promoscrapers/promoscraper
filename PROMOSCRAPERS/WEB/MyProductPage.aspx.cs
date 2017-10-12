using BusinessLogic;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public partial class MyProductPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            UserBLL dd = new UserBLL();
           var lst = dd.getProductListfromDB();

            foreach (var obj in lst)
            {
                System.Web.UI.HtmlControls.HtmlGenericControl createDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                createDiv.InnerHtml = "<div style=\"height:200px;width:200px;border-color:black\"><img src=\"" + obj + "\" alt=\"No Image Available\" style=\"display: inline;\"></div>";
                this.Controls.Add(createDiv);
            }

        }


    }

    [WebMethod]
    public static string GetProducts(int pageIndex)
    {
        return EntityDAL.ProductPageList(pageIndex).GetXml();
    }

}