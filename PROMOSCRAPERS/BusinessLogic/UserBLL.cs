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
using MarketplaceWebServiceSellers.Model;
using MarketplaceWebServiceSellers;
using System.Data;

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

        public List<KotletaProduct> getProductListfromDB()
        {
            List<KotletaProduct> lst = new List<KotletaProduct>();

            
            DataSet ds = EntityDAL.ProductPageList(1);
            DataTable dt = new DataTable();
            dt = ds.Tables[0];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                KotletaProduct SD = new KotletaProduct();
                SD.AmazonASIN = dt.Rows[i]["AmazonASIN"].ToString();
                SD.Name = dt.Rows[i]["Name"].ToString();
                SD.Price = Convert.ToDecimal(dt.Rows[i]["OriginalPrice"]);
                SD.ImageURL = dt.Rows[i]["ImageURL"].ToString();
                lst.Add(SD);
            }
            return lst;
        }

        public void Callforsellers()
        {
            // TODO: Set the below configuration variables before attempting to run

            // Developer AWS access key
            string accessKey = null;

            string secretKey = null;
            string appName = null;

            string appVersion = null;
            string serviceURL = "https://mws.amazonservices.com";

            // Create a configuration object
            MarketplaceWebServiceSellersConfig config = new MarketplaceWebServiceSellersConfig();
            config.ServiceURL = serviceURL;
            // Set other client connection configurations here if needed
            // Create the client itself
            MarketplaceWebServiceSellers.MarketplaceWebServiceSellers client = new MarketplaceWebServiceSellersClient(accessKey, secretKey, appName, appVersion, config);

            //MarketplaceWebServiceSellersSample sample = new MarketplaceWebServiceSellersSample(client);

            // Uncomment the operation you'd like to test here
            // TODO: Modify the request created in the Invoke method to be valid

            try
            {
                MarketplaceWebServiceSellers.Model.IMWSResponse response = null;
                //response = InvokeGetServiceStatus(client);
                response = InvokeListMarketplaceParticipations(client);
                //response = InvokeListMarketplaceParticipationsByNextToken(client);
                Console.WriteLine("Response:");
                MarketplaceWebServiceSellers.Model.ResponseHeaderMetadata rhmd = response.ResponseHeaderMetadata;
                // We recommend logging the request id and timestamp of every call.
                Console.WriteLine("RequestId: " + rhmd.RequestId);
                Console.WriteLine("Timestamp: " + rhmd.Timestamp);
                string responseXml = response.ToXML();
                Console.WriteLine(responseXml);
            }
            catch (MarketplaceWebServiceSellersException ex)
            {
                // Exception properties are important for diagnostics.
                MarketplaceWebServiceSellers.Model.ResponseHeaderMetadata rhmd = ex.ResponseHeaderMetadata;
                Console.WriteLine("Service Exception:");
                if (rhmd != null)
                {
                    Console.WriteLine("RequestId: " + rhmd.RequestId);
                    Console.WriteLine("Timestamp: " + rhmd.Timestamp);
                }
                Console.WriteLine("Message: " + ex.Message);
                Console.WriteLine("StatusCode: " + ex.StatusCode);
                Console.WriteLine("ErrorCode: " + ex.ErrorCode);
                Console.WriteLine("ErrorType: " + ex.ErrorType);
                throw ex;
            }
        }

        public MarketplaceWebServiceSellers.Model.GetServiceStatusResponse InvokeGetServiceStatus(MarketplaceWebServiceSellers.MarketplaceWebServiceSellers client)
        {

            
            // Create a request.
            MarketplaceWebServiceSellers.Model.GetServiceStatusRequest request = new MarketplaceWebServiceSellers.Model.GetServiceStatusRequest();
            string sellerId = null;
            request.SellerId = sellerId;
            string mwsAuthToken = null;
            request.MWSAuthToken = mwsAuthToken;
            return client.GetServiceStatus(request);
        }

        public ListMarketplaceParticipationsResponse InvokeListMarketplaceParticipations(MarketplaceWebServiceSellers.MarketplaceWebServiceSellers client)
        {
            // Create a request.
            ListMarketplaceParticipationsRequest request = new ListMarketplaceParticipationsRequest();

            string sellerId = null;
            request.SellerId = sellerId;
            string mwsAuthToken = null;
            request.MWSAuthToken = mwsAuthToken;
            return client.ListMarketplaceParticipations(request);
        }

        public ListMarketplaceParticipationsByNextTokenResponse InvokeListMarketplaceParticipationsByNextToken(MarketplaceWebServiceSellers.MarketplaceWebServiceSellers client)
        {
            // Create a request.
            ListMarketplaceParticipationsByNextTokenRequest request = new ListMarketplaceParticipationsByNextTokenRequest();
            string sellerId = null;
            request.SellerId = sellerId;
            string mwsAuthToken = null;
            request.MWSAuthToken = mwsAuthToken;
            string nextToken = null;
            request.NextToken = nextToken;
            return client.ListMarketplaceParticipationsByNextToken(request);
        }

        public void CallToAmazon(string calltype)
        {
            string accessKey = null;

            string secretKey = null;

            string appName = null;
            
            string appVersion = null;

            string serviceURL = "https://mws.amazonservices.com";

            MarketplaceWebServiceProductsConfig config = new MarketplaceWebServiceProductsConfig();
            config.ServiceURL = serviceURL;
            MarketplaceWebServiceProducts.MarketplaceWebServiceProducts client = new MarketplaceWebServiceProductsClient(appName, appVersion, accessKey, secretKey, config);
            

            try
            {
                MarketplaceWebServiceProducts.Model.IMWSResponse response = null;
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
                MarketplaceWebServiceProducts.Model.ResponseHeaderMetadata rhmd = ex.ResponseHeaderMetadata;
                throw ex;
            }
        }

        public ListMatchingProductsResponse InvokeListMatchingProducts(MarketplaceWebServiceProducts.MarketplaceWebServiceProducts client)
        {
            // Create a request.
            ListMatchingProductsRequest request = new ListMatchingProductsRequest();
            string sellerId = null;
            request.SellerId = sellerId;
            string mwsAuthToken = null;
            request.MWSAuthToken = mwsAuthToken;
            string marketplaceId = null;
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
            string sellerId = null;
            request.SellerId = sellerId;
            string mwsAuthToken = null;
            request.MWSAuthToken = mwsAuthToken;
            string marketplaceId = null;
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
