using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Drawing;

namespace Sorak.Web.Utils
{
    public class PhotoUtilities
    {
        public static string GetPhotoUriByWindowsName(string windowsName)
        {
            string defaultUrl = "/Content/images/employee_photos/unisex_profile.png";

            return defaultUrl;

        }

    }
}