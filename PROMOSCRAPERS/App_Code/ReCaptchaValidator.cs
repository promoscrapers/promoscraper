using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;

/// <summary>
/// Summary description for ReCaptchaValidator
/// </summary>
/// 
public static class ReCaptchaValidator
{
    public static string success { get; set; }

    public static string ReCaptcha_Key = "6LdpsS4UAAAAAGL3Yj47_vCQAfPYQADLrtLHwgAc";
    public static string ReCaptcha_Secret = "6LdpsS4UAAAAAHzEGkGg-ilTwgJ8MjB6etyct9Zf";

    public static string ValidateReCaptcha()
    {
        string sReturn = "OK";

        //start building recaptch api call
        var sb = new StringBuilder();

        //Getting Response String Append to Post Method
        string Response = System.Web.HttpContext.Current.Request["g-recaptcha-response"];

        string url = "https://www.google.com/recaptcha/api/siteverify?secret=" + ReCaptcha_Secret + "&response=" + Response;
        sb.Append(url);

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
                sReturn = "Captcha was unable to make the api call";
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
                            sReturn += "reCAPTCHA Error: " + error;
                        }
                    }
                }
                else //api does not contain errors
                {
                    if (!result.Success) //captcha was unsuccessful for some reason
                    {
                        sReturn = "Captcha did not pass, please try again.";
                    }
                    else //---- If successfully verified. Do your rest of logic.
                    {
                        sReturn = "OK";

                    }
                }
            }
        }
        return sReturn;
    }



    public class RecaptchaApiResponse
    {

        public bool Success;


        public List<string> ErrorCodes;
    }
}

//public bool temp = true;
//protected void SendMessageButton_Click(object sender, EventArgs e)
//{
//    temp = ValidateReCaptcha();
//    if (temp == false)
//    {
//        lblmsg.Text = "Not Valid Recaptcha";
//        lblmsg.ForeColor = System.Drawing.Color.Red;
//    }
//    else
//    {
//        lblmsg.Text = "Successful";
//        lblmsg.ForeColor = System.Drawing.Color.Green;
//    }

//    Page.Validate();

//    if (this.Page.IsValid == true && temp == true)
//    { //Page and invisible recaptcha is valid  }
// }
//}

//public class ReCaptchaValidator
//{
//    private readonly string _ReCaptchaSecret="6LdpsS4UAAAAAHzEGkGg-ilTwgJ8MjB6etyct9Zf";
//    private readonly string _ReCaptchaSiteKey = "6LdpsS4UAAAAAGL3Yj47_vCQAfPYQADLrtLHwgAc";
//    public List<string> ErrorCodes { get; set; }

//    public ReCaptchaValidator(string reCaptchaSecret)
//    {
//        _ReCaptchaSecret = reCaptchaSecret;
//        this.ErrorCodes = new List<string>();
//    }
//    public ReCaptchaValidator(string reCaptchaSecret, string reCaptchaSiteKey)
//    {
//        _ReCaptchaSecret = reCaptchaSecret;
//        _ReCaptchaSiteKey = reCaptchaSiteKey;
//        this.ErrorCodes = new List<string>();
//    }

//    public bool ValidateCaptcha(HttpRequest request)
//    {
//        var sb = new StringBuilder();
//        sb.Append("https://www.google.com/recaptcha/api/siteverify?secret=");
//        sb.Append(_ReCaptchaSecret);
//        sb.Append("&response=");
//        sb.Append(request.Form["g-recaptcha-response"]);

//        //client ip address
//        sb.Append("&remoteip=");
//        sb.Append(GetUserIp(request));

//        //make the api call and determine validity
//        using (var client = new WebClient())
//        {
//            var uri = sb.ToString();
//            var json = client.DownloadString(uri);
//            var serializer = new DataContractJsonSerializer(typeof(RecaptchaApiResponse));
//            var ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
//            var result = serializer.ReadObject(ms) as RecaptchaApiResponse;

//            if (result == null)
//            {
//                return false;
//            }
//            else if (result.ErrorCodes != null)
//            {
//                foreach (var code in result.ErrorCodes)
//                {
//                    this.ErrorCodes.Add(code.ToString());
//                }
//                return false;
//            }
//            else if (!result.Success)
//            {
//                return false;
//            }
//            else //-- If successfully verified.
//            {
//                return true;
//            }
//        }
//    }

//    //--- To get user IP(Optional)
//    private string GetUserIp(HttpRequest request)
//    {
//        var visitorsIpAddr = string.Empty;

//        if (request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
//        {
//            visitorsIpAddr = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
//        }
//        else if (!string.IsNullOrEmpty(request.UserHostAddress))
//        {
//            visitorsIpAddr = request.UserHostAddress;
//        }
//        return visitorsIpAddr;
//    }

//}


//public class RecaptchaApiResponse
//{
//    public bool Success;

   
//    public List<string> ErrorCodes;
//}
