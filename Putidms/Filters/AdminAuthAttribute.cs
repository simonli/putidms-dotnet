using Putidms.Common;
using Putidms.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Putidms.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class AdminAuthAttribute : FilterAttribute, IAuthorizationFilter
    {
        private string _PermissionDeniedUrl = string.Empty;
        public AdminAuthAttribute()
        {
            string permissionDeniedUrl = SystemConfig.PermissionDeniedToUrl;
            if (String.IsNullOrEmpty(permissionDeniedUrl))
            {
                this._PermissionDeniedUrl = "/home/index";
            }
            else
            {
                this._PermissionDeniedUrl = permissionDeniedUrl;
            }
        }
        public AdminAuthAttribute(String permissionDeniedUrl) : this()
        {
            this._PermissionDeniedUrl = permissionDeniedUrl;
        }



        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext == null)
            {
                throw new Exception("此特性只适合于Web应用程序使用！");
            }
            else
            {
                if (filterContext.HttpContext.Session == null)
                {
                    throw new Exception("服务器Session不可用！");
                }
                else
                {
                    User user = (User)filterContext.HttpContext.Session[SystemConfig.AuthUserKey];
                    if ((int)user.RoleType<(int)Role.Admin)
                    {
                        TempDataDictionary TempData = new TempDataDictionary();
                        TempData.Flash("danger", "权限不足，需要管理员权限！");
                        filterContext.Result = new RedirectResult(_PermissionDeniedUrl);
                    }
                }
                
            }
        }
    }
}