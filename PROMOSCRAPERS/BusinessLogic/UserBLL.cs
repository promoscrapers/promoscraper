using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess;
using Entity;
using MarketplaceWebServiceProducts;
using MarketplaceWebServiceProducts.Model;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace BusinessLogic
{
    public class UserBLL
    {

        //public static CouponEnt InsertBuyer(Buyer br)
        //{
        //    return new UserDAL().InsertBuyer(br);            
        //}


        //public static Buyer GetBuyer(int iBuyerID)
        //{
        //    return new UserDAL().GetBuyer(iBuyerID);
        //}


        //public static bool AuthenticateEmail(int iBuyerID, Guid gEmailID)
        //{           

        //    try
        //    {
        //        return new UserDAL().ConfirmEmail(iBuyerID, gEmailID);

        //    }
        //    catch(Exception ex)
        //    {
        //        return false;
        //    }


        //}



        public void CallToAmazon(string calltype)
        {
            string accessKey = "AKIAIQ3SPJWPOUYVECYQ";

            string secretKey = "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";
            
            string appName = "Promo Scrapers";
            
            string appVersion = "amzn1.devportal.apprelease.ae28aae240434fd7b86e94269c44a4c5";
            string serviceURL = "https://mws.amazonservices.com";

            MarketplaceWebServiceProductsConfig config = new MarketplaceWebServiceProductsConfig();
            config.ServiceURL = serviceURL;
            MarketplaceWebServiceProducts.MarketplaceWebServiceProducts client = new MarketplaceWebServiceProductsClient(appName, appVersion, accessKey, secretKey, config);
            

            try
            {
                IMWSResponse response = null;
                if(calltype =="GetProducts")
                { 
                response = InvokeListMatchingProducts(client);

                    string responseXml = response.ToXML();
                    XMLSerializationforProducts(responseXml);
                }
                else
                {
                    List<string> lstASINs = EntityDAL.GetASINWithNoCategoryName;

                    foreach (var asin in lstASINs)
                    {

                        if(asin != null)
                        {
                        response = InvokeGetProductCategoriesForASIN(client, asin);
                        string responseXml = response.ToXML();
                        XMLSerializationforCategories(responseXml);
                        System.Threading.Thread.Sleep(2000);
                        }
                    }

                }
                   
            }
            catch (MarketplaceWebServiceProductsException ex)
            {
                ResponseHeaderMetadata rhmd = ex.ResponseHeaderMetadata;
                throw ex;
            }
        }

        public ListMatchingProductsResponse InvokeListMatchingProducts(MarketplaceWebServiceProducts.MarketplaceWebServiceProducts client)
        {
            // Create a request.
            ListMatchingProductsRequest request = new ListMatchingProductsRequest();
            string sellerId = "A3H9ZT52H99B8J";
            request.SellerId = sellerId;
            string mwsAuthToken = "amzn.mws.06af8380-c09c-5d3b-8943-994cf89b99b5";
            request.MWSAuthToken = mwsAuthToken;
            string marketplaceId = "ATVPDKIKX0DER";
            request.MarketplaceId = marketplaceId;
            string query = "All";
            request.Query = query;
            string queryContextId = null;
            request.QueryContextId = queryContextId;
            return client.ListMatchingProducts(request);
        }

        public GetProductCategoriesForASINResponse InvokeGetProductCategoriesForASIN(MarketplaceWebServiceProducts.MarketplaceWebServiceProducts client,string ASIN)
        {
            GetProductCategoriesForASINRequest request = new GetProductCategoriesForASINRequest();
            string sellerId = "A3H9ZT52H99B8J";
            request.SellerId = sellerId;
            string mwsAuthToken = "amzn.mws.06af8380-c09c-5d3b-8943-994cf89b99b5";
            request.MWSAuthToken = mwsAuthToken;
            string marketplaceId = "ATVPDKIKX0DER";
            request.MarketplaceId = marketplaceId;
            string asin = ASIN;
            request.ASIN = asin;
            return client.GetProductCategoriesForASIN(request);

        }



        public void XMLSerializationforProducts(string XML)
        {
            try
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(XML);
                XmlNodeList allNodes = xDoc.GetElementsByTagName("Product");
                List<Product> productlist = new List<Product>();
                foreach (XmlNode node in allNodes)
                {
                    Product objproduct = new Product();
                    XmlDocument xDoc1 = new XmlDocument();
                    xDoc1.LoadXml(node.OuterXml);

                    var Marketplace = node["Identifiers"];
                    if (Marketplace != null)
                    {
                        objproduct.MarketplaceId = Marketplace.ChildNodes.Item(0).ChildNodes.Item(0).InnerText;
                        objproduct.ASIN = Marketplace.ChildNodes.Item(0).ChildNodes.Item(1).InnerText;
                    }

                    var ProductCategory = node["SalesRankings"];
                    if (ProductCategory != null)
                    {
                        objproduct.ProductCategory = ProductCategory.ChildNodes.Item(0).ChildNodes.Item(0).InnerText;
                        objproduct.ProductCategoryId = ProductCategory.ChildNodes.Item(1).ChildNodes.Item(0).InnerText;
                    }
                    
                    XmlNodeList allAttributes = xDoc1.GetElementsByTagName("ns2:ItemAttributes");

                    foreach (XmlNode attributenode in allAttributes)
                    {
                        var ProductGroup = attributenode["ns2:ProductGroup"];
                        if (ProductGroup != null)
                        {
                            objproduct.ProductGroup = ProductGroup.InnerText;
                        }
                        var ProductName = attributenode["ns2:Title"];
                        if(ProductName != null)
                        {
                            objproduct.ProductName = ProductName.InnerText;
                        }

                        var PublicationDate = attributenode["ns2:PublicationDate"];
                        if (PublicationDate != null)
                        {
                            objproduct.PublicationDate = PublicationDate.InnerText;
                        }
                        var ReleaseDate = attributenode["ns2:ReleaseDate"];
                        if (ReleaseDate != null)
                        {
                            objproduct.ReleaseDate = ReleaseDate.InnerText;
                        }
                        var imgeNode = attributenode["ns2:SmallImage"];
                        if(imgeNode != null)
                        {
                          objproduct.URL = imgeNode.ChildNodes.Item(0).InnerText;
                        }
                        

                        var priceNode = attributenode["ns2:ListPrice"];
                        if (priceNode != null)
                        {
                            objproduct.Amount = priceNode.ChildNodes.Item(0).InnerText;
                            objproduct.CurrencyCode = priceNode.ChildNodes.Item(1).InnerText;
                        }
                           
                    }

                    productlist.Add(objproduct);
                }
                foreach(var product in productlist)
                {
                    KotletaProduct objkotalac = new KotletaProduct();
                    objkotalac.AmazonASIN = product.ASIN;
                    objkotalac.Category = product.ProductCategoryId;
                    objkotalac.Name = product.ProductName;
                    objkotalac.ImageURL = product.URL;
                    objkotalac.Price =  product.Amount==null ? 0:decimal.Parse(product.Amount);
                    objkotalac.CurrencyCode = product.CurrencyCode;
                    objkotalac.ProductGroup = product.ProductGroup;
                    objkotalac.DateTimeWhen = product.PublicationDate==null?DateTime.Today: DateTime.Parse(product.PublicationDate);
                    EntityDAL.InsertNewProduct(objkotalac);
                }

            }
            catch (Exception exp)
            {
                throw exp;
               
            }

        }


        public void XMLSerializationforCategories(string XML)
        {
            try
            {
                string CategoryID="";
                string CategoryName="";
                string ParentID="";
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(XML);
                XmlNodeList allNodes = xDoc.GetElementsByTagName("GetProductCategoriesForASINResult");
                foreach (XmlNode node in allNodes)
                {

                    var Category = node["Self"];
                    if (Category != null)
                    {
                         CategoryID = Category.ChildNodes.Item(0).InnerText;
                         CategoryName = Category.ChildNodes.Item(1).InnerText;
                    }
                    
                    XmlDocument xDoc1 = new XmlDocument();
                    xDoc1.LoadXml(node.OuterXml);

                    XmlNodeList allAttributes = xDoc1.GetElementsByTagName("Parent");
                    int count = allAttributes.Count; 
                    int counter = 0;
                    foreach (XmlNode attributenode in allAttributes)
                    { 
                        if(count== counter + 1)
                        { 
                        ParentID = attributenode.ChildNodes.Item(0).InnerText;
                        }
                        counter++;
                       
                    }
                 }
                EntityDAL.InsertProductCategories(CategoryID, CategoryName, ParentID);

            }
            catch (Exception exp)
            {
                throw exp;

            }

        }

    }
    public class Product
    {
        public string ASIN { get; set; }
        public string ProductCategoryId { get; set; }
        public string ProductCategory { get; set; }
        public string ProductGroup { get; set; }
        public string ProductName { get; set; }
        public string PublicationDate { get; set; }
        public string ReleaseDate { get; set; }
        public string URL { get; set; }
        public string MarketplaceId { get; set; }
        public string Amount { get; set; }
        public string CurrencyCode { get; set; }

    }

}
