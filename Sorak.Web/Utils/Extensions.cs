using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sorak.Model.ClientModel;

namespace Sorak.Web.Utils
{
    public static class Extensions
    {
        public static void UpdatePhotoUri(this SorakUser videoUser)
        {
            try
            {
                videoUser.PhotoUri = PhotoUtilities.GetPhotoUriByWindowsName(videoUser.WindowsUserName);
            }
            catch { }
        }
    }
}