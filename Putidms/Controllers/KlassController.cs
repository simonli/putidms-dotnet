using AutoMapper;
using Putidms.Common;
using Putidms.Domain.Models;
using Putidms.Domain.Provider;
using Putidms.Filters;
using Putidms.Models;
using Putidms.Web.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Putidms.Controllers
{
    [Auth]
    public class KlassController : Controller
    {
        KlassProvider klassProvider = new KlassProvider();
       
        public ActionResult Index(string searchString)
        {
            IEnumerable<Klass> klasses = klassProvider.FindList();
            User authUser = (User)Session[SystemConfig.AuthUserKey];

            if ((int)authUser.RoleType != (int)Role.Admin && (int)authUser.RoleType != (int)Role.SuperAdmin)
            {
                if ((int)authUser.PermissionInType == (int)PermissionIn.Department)
                {
                    klasses = klasses.Where(x => x.DepartmentId == authUser.DepartmentId);
                }
                if ((int)authUser.PermissionInType == (int)PermissionIn.Division)
                {
                    klasses = klasses.Where(x => x.Department.DivisionId == authUser.DivisionId);
                }
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                klasses = klasses.Where(s => s.Name.Contains(searchString) || s.Number.Contains(searchString) ||
                s.Department.Name.Contains(searchString) || s.Department.Division.Name.Contains(searchString));
            }
            return View(klasses);
        }
        public ActionResult Add()
        {
            User authUser = (User)Session[SystemConfig.AuthUserKey];
            ViewBag.Divisions = WebUtil.GetDivisionList(authUser);
            ViewBag.Departments = WebUtil.GetDepartmentList(authUser);
            return View("Edit", new KlassViewModel());
        }

        public ActionResult Edit(int Id)
        {
            Klass klass = klassProvider.Find(Id);
            if (klass != null)
            {
                User authUser = (User)Session[SystemConfig.AuthUserKey];
                ViewBag.Divisions = WebUtil.GetDivisionList(authUser); ;
                ViewBag.Departments = WebUtil.GetDepartmentList(authUser);
                KlassViewModel vm = Mapper.Map<Klass, KlassViewModel>(klass);
                vm.DivisionId = klass.Department.DivisionId;
                return View(vm);
            }
            TempData.Flash("danger", "该班级不存在");
            return RedirectToAction("index");

        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(KlassViewModel vm)
        {
            if (vm.Id == 0 && klassProvider.HasName(vm.Name)) ModelState.AddModelError("Name", "班级名称已经存在");
            if (vm.Id == 0 && klassProvider.HasNumber(vm.Number)) ModelState.AddModelError("Number", "班级编号已经存在.");
            if (ModelState.IsValid)
            {
                Respond respond = klassProvider.Save(Mapper.Map<KlassViewModel, Klass>(vm));
                if (respond.Code == 1)
                {
                    TempData.Flash("success", string.Format("成功{0}班级: {1}", vm.Id == 0 ? "新增" : "更新", vm.Name));
                    return RedirectToAction("index");
                }
                TempData.Flash("danger", respond.Message);
            }
            User authUser = (User)Session[SystemConfig.AuthUserKey];
            ViewBag.Divisions = WebUtil.GetDivisionList(authUser);
            ViewBag.Departments = WebUtil.GetDepartmentList(authUser);
            return View(vm);
        }

        [HttpPost]
        public JsonResult ValidateKlassName(string name, string initialName)
        {
            if (name != initialName)
            {
                return Json(!klassProvider.HasName(name));
            }
            return Json(true);
        }


        [HttpPost]
        public JsonResult ValidateKlassNumber(string number, string initialNumber)
        {
            if (number != initialNumber)
            {
                return Json(!klassProvider.HasNumber(number));
            }
            return Json(true);
        }

        [HttpPost]
        public JsonResult GetKlasstList(string departmentId)
        {
            if (!string.IsNullOrEmpty(departmentId))
            {
                int deptId = Convert.ToInt32(departmentId);
                IEnumerable<Klass> klasses = klassProvider.FindList().Where(x => x.Department.Id == deptId);
                var res = klasses.Select(x => new { Id = x.Id, Name = x.Name });
                return Json(res);
            }
            else
            {
                return Json(null);
            }
        }

    }
}