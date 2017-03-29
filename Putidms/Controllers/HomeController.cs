using Putidms.Common;
using Putidms.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Putidms.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        [ChildActionOnly]
        public ActionResult MenuNavUser()
        {
            User authUser = (User)Session[SystemConfig.AuthUserKey];
            if (authUser != null)
            {
                if ((int)authUser.RoleType == (int)Role.Admin || (int)authUser.RoleType == (int)Role.SuperAdmin)
                {
                    return PartialView(true);
                }
            }
            return PartialView(false);
        }


        [ChildActionOnly]
        public ActionResult MenuNavLogin()
        {
            User authUser = (User)Session[SystemConfig.AuthUserKey];
            if (authUser != null)
            {
                return PartialView(authUser);
            }
            else
            {
                return PartialView(null);
            }
        }
    }
}