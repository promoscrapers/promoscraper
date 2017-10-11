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
        //Utilities.AmazonGetProducts objamzongetproducts = new Utilities.AmazonGetProducts();
        //string xml =objamzongetproducts.CallToAmazon();

        UserBLL dd = new UserBLL();
        dd.CallToAmazon("GetProducts");



        //XmlSerializer serializer = new XmlSerializer(typeof(List<MyClass>));

        //using (FileStream stream = File.OpenWrite("filename"))
        //{
        //    List<MyClass> list = new List<MyClass>();
        //    serializer.Serialize(stream, list);
        //}

        //using (FileStream stream = File.OpenRead("filename"))
        //{
        //    List<MyClass> dezerializedList = (List<MyClass>)serializer.Deserialize(stream);
        //}
    }

    protected void btngetCategories_Click(object sender, EventArgs e)
    {
        UserBLL dd = new UserBLL();
        dd.CallToAmazon("GetCategories");
    }
}