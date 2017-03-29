using AutoMapper;
using Putidms.Common;
using Putidms.Domain.Models;
using Putidms.Domain.Provider;
using Putidms.Filters;
using Putidms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Putidms.Controllers
{
    [Auth]
    public class DivisionController : Controller
    {
        DivisionProvider  divProvider = new DivisionProvider();       

        public ActionResult Index(string searchString)
        {
            IEnumerable<Division> divs = divProvider.FindList().OrderByDescending(x => x.CreateTime);
            User authUser = (User)Session[SystemConfig.AuthUserKey];
            if ((int)authUser.RoleType != (int)Role.Admin && (int)authUser.RoleType != (int)Role.SuperAdmin)
            {
                divs = divs.Where(x => x.Id == authUser.DivisionId);
            }
                
            
            if (!String.IsNullOrEmpty(searchString))
            {
                divs = divs.Where(s => s.Name.Contains(searchString));
            }
            return View(divs);
        }


        public ActionResult Add()
        {
            return View("Edit", new DivisionViewModel());
        }

        public ActionResult Edit(int Id)
        {
            Division div = divProvider.Find(Id);
            if (div != null)
            {
                return View(Mapper.Map<Division, DivisionViewModel>(div));
            }
            TempData.Flash("danger", "该修学处不存在");
            return RedirectToAction("index");

        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(DivisionViewModel vm)
        {
            if (vm.Id == 0 && divProvider.HasName(vm.Name)) ModelState.AddModelError("Name", "修学处名称已经存在");
            if (ModelState.IsValid)
            {
                Division div = Mapper.Map<DivisionViewModel, Division>(vm);
                Respond respond = divProvider.Save(div);
                if (respond.Code == 1)
                {
                    TempData.Flash("success", string.Format("成功{0}修学处: {1}", vm.Id == 0 ? "新增" : "更新", vm.Name));
                    return RedirectToAction("index");
                }
                TempData.Flash("danger", respond.Message);
            }
            return View(vm);
        }

        public ActionResult Delete(int Id)
        {
            Respond respond = divProvider.Delete(Id);
            TempData.Flash("info", respond.Message);
            return RedirectToAction("index");
        }

        [HttpPost]
        public JsonResult ValidateDivisionName(string name, string initialName)
        {
            if (name != initialName)
            {
                return Json(!divProvider.HasName(name));
            }
            return Json(true);
        }

    }
}