using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Putidms.Utility
{
    public class Config
    {
        public static string pageSize = ConfigUtil.GetAppSettings("pagesize");
    }
}