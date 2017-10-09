using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Configuration;
using Utilities.Email;
using Utilities;
using Entity;

using System.IO;
using System.Net;
using System.Xml;
using BitlyDotNET.Interfaces;
using BitlyDotNET.Implementations;



namespace Utilities
{
    public static class CommonLibrary
    {

        public static string ShortURL(string sLongURL)
        {
            try
            {
                string shortUrl = string.Empty;
                BitlyService s = new BitlyService("o_6ejr93t5na", "R_3e96ecc9f3c545f48cd1c67a29e986a7");
                string shortened;
                StatusCode status = s.Shorten(sLongURL, out shortened);
                IBitlyResponse[] shortUrls = s.Shorten(new string[] { sLongURL }, out status);

                shortUrl = shortUrls[0].ShortUrl;

                return shortUrl;                
            }
            catch(Exception ex)
            {
                return sLongURL;
            }
        }

        public static double Percent(this double number, int percent)
        {           
            return ((double)number * percent) / 100;
        }

        public static bool CacheInsert(object ObjToCache, string sKey)
        {
            HttpRuntime.Cache.Insert(sKey, ObjToCache, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromDays(1));
            return true;
        }

        public static object CacheGet(string sKey)
        {
            return HttpRuntime.Cache.Get(sKey);
        }



        //public static string GetEmailConfirmURL(CouponEnt cp, Buyer br, int iAmazProdID)
        //{
        //    try
        //    {

        //        return ConfigValues.ServerAddress + "Coupon.aspx?" + CommonLibrary.Encrypted("r=" + CommonLibrary.RandomString(4, false) + "&j=" + IP.ToString().Trim() + "&P=" + iAmazProdID.ToString() + "&z=" + br.ID.ToString() + "&b=" + cp.BuyerEmailIdentifier.ToString() + "&c=" + cp.CouponCode.Trim()); ;
        //    }
        //    catch(Exception ex)
        //    {
        //        return "http://amzn.to/2iLwJfk";
        //    }

        //}

        //public static bool EmailConfirm(CouponEnt cp, Buyer br, int iAmazProdID)
        //{
        //    try
        //    {
        //        string sURL = GetEmailConfirmURL(cp, br, iAmazProdID);

        //        if (!ConfigValues.ServerAddress.ToLower().Contains("localhost"))
        //            sURL = ShortURL(sURL);

        //        EmailEntity em = new EmailEntity();
        //        em.FirstName = br.FirstName;
        //        em.LastName = br.LastName;
        //        em.Subject = "DEAR " + br.FirstName + " " + br.LastName + " Please click URL in this email to receive MIT NUTRA 20% coupon code for Amazon";
        //        em.EmaiLBody = "Hello " + br.FirstName + " " + br.LastName + ",<br><br>";
        //        em.EmaiLBody += "Please click the following URL and we will display MIT NUTRA 20% Amazon Coupon code:<br><br>";
        //        em.EmaiLBody += "<a href =\"" + sURL + "\">CLICK HERE TO CONFIRM YOUR EMAIL AND GET 20% AMAZON COUPON CODE DISPLAYED NOW!</a>";
        //        em.EmaiLBody += "<br><br><b>PLEASE NOTE, YOU MUST CLICK ON THE ABOVE LINK FROM THE SAME DEVICE OR PC.http://bit.ly/2eIRMdU</b>";
        //        em.EmaiLBody += "<br><br>If for some reason you are having troubles, copy the following URL (entire URL) and paste it in your browser: <br><br><b><font color='red'>" + sURL + "</font></b>";
        //        em.EmaiLBody += "<br><br>THANK YOU SO MUCH!!! WE HOPE TO SEE YOU PURCHASING MIT NUTRA PRODUCTS ON AMAZON!";
        //        em.EmaiLBody += "<br><br>IF YOU HAVE ANY QUESTIONS, PLEASE FEEL FREE TO EMAIL US AT SUPPORT@mitcoupons.com";
        //        em.EmaiLBody += "<br><br>MIT NUTRA";
        //        em.EmaiLBody += "<br>4023 KENNETT PIKE #50070";
        //        em.EmaiLBody += "<br>WILMINGTON, DE 19807";
        //        em.EmaiLBody += "<br>PH:+1 503 - 714 - 1204";
        //        em.EmaiLBody += "<br>E-MAIL:info @mitcoupons.com";
        //        em.EmaiLBody += "<br>WEB:www.mitnutra.com";

        //        bool bsuccess = new EmailClient().SendEmail("noreply@mitcoupons.com", br.Email, em.Subject, em.EmaiLBody, true);
        //        return bsuccess;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}


        public static string IP
        {
            get
            {
                var visitorsIpAddr = string.Empty;

                if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                {
                    visitorsIpAddr = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                }
                else if (!string.IsNullOrEmpty(HttpContext.Current.Request.UserHostAddress))
                {
                    visitorsIpAddr = HttpContext.Current.Request.UserHostAddress;
                }

                return visitorsIpAddr;
            }
        }

        public static string HTTPREFERRER
        {
            get
            {
                var strereturn = string.Empty;

                if (HttpContext.Current.Request.ServerVariables["HTTP_REFERER"] != null)
                {
                    strereturn = HttpContext.Current.Request.ServerVariables["HTTP_REFERER"];
                }
             
                return strereturn;
            }
        }


        public static string Decrypted(string Text)
        {
            try
            {
                try
                {
                    return Utilities.Cryptography.AESEncryption.Decrypt(System.Web.HttpUtility.UrlDecode(Text), ConfigValues.Encrypt_Key);
                }
                catch
                {
                    return Utilities.Cryptography.AESEncryption.Decrypt(Text, ConfigValues.Encrypt_Key);
                }
            }
            catch { return Text; }
        }

        public static string Encrypted(string Text)
        {
            return System.Web.HttpUtility.UrlEncode(Utilities.Cryptography.AESEncryption.Encrypt(Text, ConfigValues.Encrypt_Key));
        }

        public static string RandomString(int NumChars, bool NumbersOnly)
        {
            return new Utilities.KeyGenerator(NumChars, NumbersOnly).RandomString;
        }

        public static string GetQueryStringValueDecrypted(string QueryString, string Key)
        {
            string queryStringValue = string.Empty;

            if (!string.IsNullOrEmpty(QueryString))
            {
                string[] queryStringArray = QueryString.Split('&');
                int count = queryStringArray.Length;

                for (int index = 0; index < count; index++)
                {
                    if (queryStringArray[index].ToLower().StartsWith(Key.ToLower()))
                    {
                        string queryStringRow = queryStringArray[index].Trim();

                        int startIndex = (Key + "=").Length;
                        if (queryStringRow.Length > startIndex)
                        {
                            queryStringValue = queryStringRow.Substring(startIndex);
                        }
                        break;
                    }
                }
            }

            return queryStringValue;
        }

        public static string IP_HOST
        {
            get
            {
                var visitorsIpAddr = string.Empty;


                if (!string.IsNullOrEmpty(HttpContext.Current.Request.UserHostAddress))
                {
                    visitorsIpAddr = HttpContext.Current.Request.UserHostAddress;
                }

                return visitorsIpAddr;
            }
        }





        public static bool LogError(string sError)
        {
            return true;
        }


       
    }
}
