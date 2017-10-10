﻿using System;
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



        public void CallToAmazon()
        {
            // TODO: Set the below configuration variables before attempting to run

            // Developer AWS access key
            string accessKey = "AKIAIQ3SPJWPOUYVECYQ";

            // Developer AWS secret key
            string secretKey = "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";

            // The client application name
            string appName = "Promo Scrapers";

            // The client application version
            string appVersion = "amzn1.devportal.apprelease.ae28aae240434fd7b86e94269c44a4c5";

            // The endpoint for region service and version (see developer guide)
            // ex: https://mws.amazonservices.com
            string serviceURL = "https://mws.amazonservices.com";

            // Create a configuration object
            MarketplaceWebServiceProductsConfig config = new MarketplaceWebServiceProductsConfig();
            config.ServiceURL = serviceURL;
            // Set other client connection configurations here if needed
            // Create the client itself
            MarketplaceWebServiceProducts.MarketplaceWebServiceProducts client = new MarketplaceWebServiceProductsClient(appName, appVersion, accessKey, secretKey, config);

            //MarketplaceWebServiceProductsSample sample = new MarketplaceWebServiceProductsSample(client);

            // Uncomment the operation you'd like to test here
            // TODO: Modify the request created in the Invoke method to be valid

            try
            {
                IMWSResponse response = null;
                // response = sample.InvokeGetCompetitivePricingForASIN();
                // response = sample.InvokeGetCompetitivePricingForSKU();
                // response = sample.InvokeGetLowestOfferListingsForASIN();
                // response = sample.InvokeGetLowestOfferListingsForSKU();
                // response = sample.InvokeGetLowestPricedOffersForASIN();
                // response = sample.InvokeGetLowestPricedOffersForSKU();
                //response = InvokeGetMatchingProduct(client);
                response = InvokeListMatchingProducts(client);
                // response = sample.InvokeGetMatchingProductForId();
                // response = sample.InvokeGetMyFeesEstimate();
                // response = sample.InvokeGetMyPriceForASIN();
                // response = sample.InvokeGetMyPriceForSKU();
                // response = sample.InvokeGetProductCategoriesForASIN();
                // response = sample.InvokeGetProductCategoriesForSKU();
                // response = sample.InvokeGetServiceStatus();
                // response = sample.InvokeListMatchingProducts();
                //Console.WriteLine("Response:");
                ResponseHeaderMetadata rhmd = response.ResponseHeaderMetadata;
                // We recommend logging the request id and timestamp of every call.
                //Console.WriteLine("RequestId: " + rhmd.RequestId);
                //Console.WriteLine("Timestamp: " + rhmd.Timestamp);
                string responseXml = response.ToXML();
                //Product obj = new Product();
                XMLSerialization(responseXml);
                //Console.WriteLine(responseXml);
            }
            catch (MarketplaceWebServiceProductsException ex)
            {
                // Exception properties are important for diagnostics.
                ResponseHeaderMetadata rhmd = ex.ResponseHeaderMetadata;
                //Console.WriteLine("Service Exception:");
                //if (rhmd != null)
                //{
                //    Console.WriteLine("RequestId: " + rhmd.RequestId);
                //    Console.WriteLine("Timestamp: " + rhmd.Timestamp);
                //}
                //Console.WriteLine("Message: " + ex.Message);
                //Console.WriteLine("StatusCode: " + ex.StatusCode);
                //Console.WriteLine("ErrorCode: " + ex.ErrorCode);
                //Console.WriteLine("ErrorType: " + ex.ErrorType);
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

        public void XMLSerialization(string XML)
        {
            try
            {
                //string xmlFile = File.ReadAllText(@"D:\Work_Time_Calculator\10-07-2013.xml");
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.LoadXml(XML);
                XmlNodeList nodeList = xmldoc.GetElementsByTagName("Product");
                string Short_Fall = string.Empty;
                foreach (XmlNode node in nodeList)
                {
                    Short_Fall = node.InnerXml;
                    xmldoc.LoadXml(Short_Fall);

                    XmlNodeList ASIN = xmldoc.GetElementsByTagName("ASIN");
                    XmlNodeList ProductCategoryId = xmldoc.GetElementsByTagName("ProductCategoryId");
                    XmlNodeList ProductGroup = xmldoc.GetElementsByTagName("ns2:ProductGroup");
                    XmlNodeList ProductTypeName = xmldoc.GetElementsByTagName("ns2:ProductTypeName");
                    XmlNodeList PublicationDate = xmldoc.GetElementsByTagName("ns2:PublicationDate");
                    XmlNodeList ReleaseDate = xmldoc.GetElementsByTagName("ns2:ReleaseDate");

                }
            }
            catch (Exception exp)
            {
                throw exp;
                //Handle Exception Code
            }
         
        }
    }
    public class Product
    {
        public string ASIN { get; set; }
        public long ProductCategoryId { get; set; }
        public string ProductCategory { get; set; }
        public string ProductGroup { get; set; }
        public string ProductTypeName { get; set; }
        public string PublicationDate { get; set; }
        public string ReleaseDate { get; set; }
        public string URL { get; set; }
        public string MarketplaceId { get; set; }
        public string Amount { get; set; }
        public string CurrencyCode { get; set; }

    }
}
