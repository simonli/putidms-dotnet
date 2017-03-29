using AutoMapper;
using Putidms.Common;
using Putidms.Domain.Models;
using Putidms.Domain.Provider;
using Putidms.Filters;
using Putidms.Models;
using Putidms.Web.Utility;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Putidms.Controllers
{
    [Auth]
    public class CounselorController : Controller
    {
        CounselorProvider counselorProvider = new CounselorProvider();
        public ActionResult Index(string searchString)
        {
            IEnumerable<Counselor> counselors = counselorProvider.FindList();

            User authUser = (User)Session[SystemConfig.AuthUserKey];
            if ((int)authUser.RoleType != (int)Role.Admin && (int)authUser.RoleType != (int)Role.SuperAdmin)
            {
                if ((int)authUser.PermissionInType == (int)PermissionIn.Department)
                {
                    counselors = counselors.Where(x => x.Klass.DepartmentId == authUser.DepartmentId);
                }
                if ((int)authUser.PermissionInType == (int)PermissionIn.Division)
                {
                    counselors = counselors.Where(x => x.Klass.Department.DivisionId == authUser.DivisionId);
                }
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                counselors = counselors.Where(s => s.UserName.Contains(searchString) || s.ReligiousName.Contains(searchString) ||
                s.Email.Contains(searchString) || s.Klass.Name.Contains(searchString) || s.Klass.Department.Name.Contains(searchString) ||
                s.Klass.Department.Division.Name.Contains(searchString) || s.Mobile.Contains(searchString) || s.Duty.Name.Contains(searchString));
            }
            return View(counselors);
        }
        public ActionResult Add()
        {
            User authUser = (User)Session[SystemConfig.AuthUserKey];
            ViewBag.Divisions = WebUtil.GetDivisionList(authUser);
            ViewBag.Departments = WebUtil.GetDepartmentList(authUser);
            ViewBag.Klasses = WebUtil.GetKlassList(authUser);
            ViewBag.Duties = WebUtil.GetDutyList();
            return View("Edit", new CounselorViewModel());
        }

        public ActionResult Edit(int Id)
        {
            Counselor c = counselorProvider.Find(Id);
            if (c != null)
            {
                User authUser = (User)Session[SystemConfig.AuthUserKey];
                ViewBag.Divisions = WebUtil.GetDivisionList(authUser);
                ViewBag.Departments = WebUtil.GetDepartmentList(authUser);
                ViewBag.Klasses = WebUtil.GetKlassList(authUser);
                ViewBag.Duties = WebUtil.GetDutyList();
                CounselorViewModel vm = Mapper.Map<Counselor, CounselorViewModel>(c);
                vm.DepartmentId = c.Klass.DepartmentId;
                vm.DivisionId = c.Klass.Department.DivisionId;
                return View(vm);
            }
            TempData.Flash("danger", "该辅导员不存在");
            return RedirectToAction("Index");

        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(CounselorViewModel vm)
        {
            if (vm.Id == 0 && counselorProvider.HasUserName(vm.UserName)) ModelState.AddModelError("UserName", string.Format("用户名 {0} 已经存在", vm.UserName));
            if (vm.Id == 0 && counselorProvider.HasEmail(vm.Email)) ModelState.AddModelError("Email", string.Format("Email {0}已经存在", vm.Email));
            if (ModelState.IsValid)
            {
                Respond respond = counselorProvider.Save(Mapper.Map<CounselorViewModel, Counselor>(vm));
                TempData.Flash("success", respond.Message);
                return RedirectToAction("Index");
            }
            User authUser = (User)Session[SystemConfig.AuthUserKey];
            ViewBag.Divisions = WebUtil.GetDivisionList(authUser);
            ViewBag.Departments = WebUtil.GetDepartmentList(authUser);
            ViewBag.Klasses = WebUtil.GetKlassList(authUser);
            ViewBag.Duties = WebUtil.GetDutyList();
            return View(vm);
        }

        [HttpPost]
        public JsonResult ValidateUserName(string username, string initialUserName)
        {
            if (username != initialUserName)
            {
                return Json(!counselorProvider.HasUserName(username));
            }
            return Json(true);

        }

        [HttpPost]
        public JsonResult ValidateEmail(string email, string initialEmail)
        {
            if (email != initialEmail)
            {
                return Json(!counselorProvider.HasEmail(email));
            }
            return Json(true);

        }
    }
}