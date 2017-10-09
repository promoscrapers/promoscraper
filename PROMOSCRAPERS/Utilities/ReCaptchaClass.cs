using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.Serialization;
using System.Net;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Web;


namespace Utilities
{
    public static class ReCaptchaClass
    {
        [DataContract]
        public class RecaptchaApiResponse
        {
            [DataMember(Name = "success")]
            public bool Success;

            [DataMember(Name = "error-codes")]
            public List<string> ErrorCodes;
        }

        public static bool isValid
        {
            get
            {
                //start building recaptch api call
                bool blnReturn = false;
                var sb = new StringBuilder();
                sb.Append("https://www.google.com/recaptcha/api/siteverify?secret=");

                //our secret key
                var secretKey = "6Lemti4UAAAAAKLEj9iQnacffaN593SwqvCETjV1";
                sb.Append(secretKey);

                //response from recaptch control 
                sb.Append("&");
                sb.Append("response=");
                var reCaptchaResponse = HttpContext.Current.Request["g-recaptcha-response"];
                sb.Append(reCaptchaResponse);

                //client ip address
                //---- This Ip address part is optional. If you donot want to send IP address you can
                //---- Skip(Remove below 4 lines)
                sb.Append("&");
                sb.Append("remoteip=");
                var clientIpAddress = CommonLibrary.IP;
                sb.Append(clientIpAddress);

                //make the api call and determine validity
                using (var client = new WebClient())
                {
                    var uri = sb.ToString();
                    var json = client.DownloadString(uri);
                    var serializer = new DataContractJsonSerializer(typeof(RecaptchaApiResponse));
                    var ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
                    var result = serializer.ReadObject(ms) as RecaptchaApiResponse;

                    //--- Check if we are able to call api or not.
                    if (result == null)
                    {
                        CommonLibrary.LogError("Captcha was unable to make the api call");

                    }
                    else // If Yes
                    {
                        //api call contains errors
                        if (result.ErrorCodes != null)
                        {
                            if (result.ErrorCodes.Count > 0)
                            {
                                foreach (var error in result.ErrorCodes)
                                {
                                    CommonLibrary.LogError("reCAPTCHA Error: " + error);
                                }
                            }
                        }
                        else //api does not contain errors
                        {
                            if (result.Success) //captcha was successful for some reason
                            {
                                blnReturn = true;
                            }
                        }

                    }

                }

                return blnReturn;
            }
        }
    }
}
