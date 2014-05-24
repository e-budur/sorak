using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;

namespace Sorak.Web.Utils
{
    public class WebUtilities
    {

        public static string ResolvePath(string relativePath)
        {
            return HttpContext.Current.Server.MapPath(relativePath);
        }

        public static string ClientIP
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            }
        }

        public static string ClientMachineName
        {
            get
            {
                var resolvedAddress = NetUtilities.GetMachineName(WebUtilities.ClientIP);

                if (string.IsNullOrEmpty(resolvedAddress))
                    return resolvedAddress;

                return resolvedAddress.Replace("sorak.com.tr", "").Replace("sorak.com.tr", "");
            }
        }

        public static NetworkCredential CommonCredentials
        {
            get
            {
                return new System.Net.NetworkCredential()
                {
                    Domain = "teknoloji",
                    UserName = @"teknoloji\FormPro",
                    Password = "TaSnIf2006"
                };
            }
        }
    }
}