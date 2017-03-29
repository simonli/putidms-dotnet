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
    public class DepartmentController : Controller
    {
        DepartmentProvider deptProvider = new DepartmentProvider();
        public ActionResult Index(string searchString)
        {
            IEnumerable<Department> depts = deptProvider.FindList().OrderByDescending(x => x.CreateTime);

            User authUser = (User)Session[SystemConfig.AuthUserKey];
            if ((int)authUser.RoleType != (int)Role.Admin && (int)authUser.RoleType != (int)Role.SuperAdmin)
            {
                if ((int)authUser.PermissionInType == (int)PermissionIn.Department)
                {
                    depts = depts.Where(x => x.Id == authUser.DepartmentId);
                }
                if ((int)authUser.PermissionInType == (int)PermissionIn.Division)
                {
                    depts = depts.Where(x => x.DivisionId == authUser.DivisionId);
                }
            }


            if (!String.IsNullOrEmpty(searchString))
            {
                depts = depts.Where(s => s.Name.Contains(searchString) || s.Division.Name.Contains(searchString));
            }
            return View(depts);
        }
        public ActionResult Add()
        {
            User authUser = (User)Session[SystemConfig.AuthUserKey];
            ViewBag.Divisions = WebUtil.GetDivisionList(authUser);
            return View("Edit", new DepartmentViewModel());
        }

        public ActionResult Edit(int Id)
        {
            Department dept = deptProvider.Find(Id);
            if (dept != null)
            {
                User authUser = (User)Session[SystemConfig.AuthUserKey];
                ViewBag.Divisions = WebUtil.GetDivisionList(authUser, dept.DivisionId.ToString());
                return View(Mapper.Map<Department, DepartmentViewModel>(dept));
            }
            TempData.Flash("danger", "该修学点不存在");
            return RedirectToAction("index");

        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(DepartmentViewModel vm)
        {
            if (vm.Id == 0 && deptProvider.HasName(vm.Name)) ModelState.AddModelError("Name", "修学点名称已经存在");
            if (ModelState.IsValid)
            {
                Respond respond = new Respond();
                respond = deptProvider.Save(Mapper.Map<DepartmentViewModel, Department>(vm));
                if (respond.Code == 1)
                {
                    TempData.Flash("success", string.Format("成功{0}修学点: {1}", vm.Id == 0 ? "新增" : "更新", vm.Name));
                    return RedirectToAction("index");
                }
                TempData.Flash("danger", respond.Message);
            }
            User authUser = (User)Session[SystemConfig.AuthUserKey];
            ViewBag.Divisions = WebUtil.GetDivisionList(authUser);
            return View(vm);
        }

        [HttpPost]
        public JsonResult ValidateDepartmentName(string name, string initialName)
        {
            if (name != initialName)
            {
                return Json(!deptProvider.HasName(name));
            }
            return Json(true);
        }

        [HttpPost]
        public JsonResult GetDepartmentList(string divisionId)
        {
            if (!string.IsNullOrEmpty(divisionId))
            {
                int divId = Convert.ToInt32(divisionId);
                IEnumerable<Department> depts = deptProvider.FindList().Where(x => x.Division.Id == divId);
                var res = depts.Select(x => new { Id = x.Id, Name = x.Name });
                return Json(res);
            }else
            {
                return Json(null);
            }
        }
    }
}