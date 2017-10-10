using MarketplaceWebServiceProducts;
using MarketplaceWebServiceProducts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilities
{
    public class AmazonGetProducts
    {
        public string CallToAmazon()
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
                return responseXml;
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
        //private readonly MarketplaceWebServiceProducts.MarketplaceWebServiceProducts client;

        //public AmazonGetProducts(MarketplaceWebServiceProducts.MarketplaceWebServiceProducts client)
        //{
        //    this.client = client;
        //}
        public GetMatchingProductResponse InvokeGetMatchingProduct(MarketplaceWebServiceProducts.MarketplaceWebServiceProducts client)
        {
            // Create a request.
            GetMatchingProductRequest request = new GetMatchingProductRequest();
            string sellerId = "A3H9ZT52H99B8J";
            request.SellerId = sellerId;
            string mwsAuthToken = "amzn.mws.06af8380-c09c-5d3b-8943-994cf89b99b5";
            request.MWSAuthToken = mwsAuthToken;
            string marketplaceId = "ATVPDKIKX0DER";
            request.MarketplaceId = marketplaceId;
            ASINListType asinList = new ASINListType();
            request.ASINList = asinList;
            return client.GetMatchingProduct(request);
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
    }
}
