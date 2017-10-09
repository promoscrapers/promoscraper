using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Text;


namespace Utilities
{
    public static class ConfigValues
    {
        public static string ServerAddress { get { return HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute("~/"); } }        
        public static string Encrypt_Key { get { return "OiOsjkvys348sSY"; } }
    }
}
