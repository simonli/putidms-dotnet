using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Putidms.Common
{
    public class SystemConfig
    {
        public static string AuthUrl = ConfigUtil.GetAppSettings("AuthUrl");
        public static string AuthSaveKey = ConfigUtil.GetAppSettings("AuthSaveKey");
        public static string AuthSaveType = ConfigUtil.GetAppSettings("AuthSaveType");
        public static string AuthUserKey = ConfigUtil.GetAppSettings("AuthUserKey");
        public static string PermissionDeniedToUrl = ConfigUtil.GetAppSettings("PermissionDeniedToUrl");
    }
}