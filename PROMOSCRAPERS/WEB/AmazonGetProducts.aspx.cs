using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;
using Utilities;
using BusinessLogic;


public partial class AmazonGetProducts : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnGetProducts_Click(object sender, EventArgs e)
    {

        UserBLL dd = new UserBLL();
        dd.CallToAmazon("GetProducts");


    }

    protected  void btngetCategories_Click(object sender, EventArgs e)
    {
        UserBLL dd = new UserBLL();
        dd.CallToAmazon("GetCategories");
    }
}