using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Sockets;

namespace Sorak.Web.Utils
{
    public class NetUtilities
    {
        public static string GetMachineName(string ipAddress)
        {
            var resolvedAddress = ResolveIP(ipAddress);

            if (string.IsNullOrEmpty(resolvedAddress))
                return resolvedAddress;

            return resolvedAddress.Replace("sorak.com.tr", "").Replace("sorak.com.tr", "");
        }

        public static string ResolveIP(string ipAddress)
        {
            IPHostEntry iphostEntry;
            string clientHostName;
            try
            {
                iphostEntry = Dns.GetHostEntry(ipAddress);
                clientHostName = iphostEntry != null ? iphostEntry.HostName : ipAddress;
            }
            catch (SocketException e)
            {
                iphostEntry = Dns.Resolve(ipAddress);
                clientHostName = iphostEntry != null ? iphostEntry.HostName : ipAddress;
            }

            return clientHostName;
        }
    }
}