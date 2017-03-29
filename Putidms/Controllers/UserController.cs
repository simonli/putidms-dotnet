using AutoMapper;
using Putidms.Common;
using Putidms.Domain.Models;
using Putidms.Domain.Provider;
using Putidms.Models;
using Putidms.Web.Utility;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Putidms.Utility;
using Putidms.Filters;

namespace Putidms.Controllers
{
    [Auth]
    public class UserController : Controller
    {
        UserProvider provider = new UserProvider();
        public ActionResult Index(string searchString)
        {
            IEnumerable<User> users = provider.FindList().OrderByDescending(x => x.CreateTime).Where(x => x.RoleType != Role.SuperAdmin);
            if (!string.IsNullOrEmpty(searchString))
            {
                users = users.Where(x => x.UserName.Contains(searchString) || x.Name.Contains(searchString)
                || x.Email.Contains(searchString) || x.Division.Name.Contains(searchString));
            }
            return View(users);
        }

        public ActionResult Details(int Id)
        {
            User user = provider.Find(Id);
            if (user != null)
            {               
                return View(user);
            }
            TempData.Flash("danger", "该用户不存在");
            return RedirectToAction("Index");
        }

        [AdminAuth]
        public ActionResult Add()
        {
            User authUser = (User)Session[SystemConfig.AuthUserKey];
            ViewBag.Divisions = WebUtil.GetDivisionList(authUser);
            ViewBag.Departments = WebUtil.GetDepartmentList(authUser);
            ViewBag.Roles = WebUtil.GetRoleList();
            return View("Edit", new UserViewModel());
        }

        [AdminAuth]
        public ActionResult Edit(int Id)
        {
            User user = provider.Find(Id);
            if (user != null)
            {
                User authUser = (User)Session[SystemConfig.AuthUserKey];
                ViewBag.Divisions = WebUtil.GetDivisionList(authUser);
                ViewBag.Departments = WebUtil.GetDepartmentList(authUser);
                ViewBag.Roles = WebUtil.GetRoleList();
                UserViewModel vm = Mapper.Map<User, UserViewModel>(user);
                return View(vm);
            }
            TempData.Flash("danger", "该用户不存在");
            return RedirectToAction("Index");
        }

        [ValidateAntiForgeryToken]
        [AdminAuth]
        [HttpPost]
        public ActionResult Edit(UserViewModel vm)
        {
            if (vm.Id == 0 && provider.HasUserName(vm.UserName)) ModelState.AddModelError("UserName", "用户名已经存在");
            if (vm.Id == 0 && provider.HasEmail(vm.Email)) ModelState.AddModelError("Email", "Email已经存在");
            if (ModelState.IsValid)
            {
                vm.Password = CommonUtil.MD5Encrypt64(vm.Password);
                Respond respond = provider.Save(Mapper.Map<UserViewModel, User>(vm));
                if (respond.Code == 1)
                {
                    TempData.Flash("success", string.Format("成功{0}用户: {1}", vm.Id == 0 ? "新增" : "更新", vm.UserName));
                    return RedirectToAction("Index");
                }
                TempData.Flash("danger", respond.Message);
            }
            User authUser = (User)Session[SystemConfig.AuthUserKey];
            ViewBag.Divisions = WebUtil.GetDivisionList(authUser);
            ViewBag.Departments = WebUtil.GetDepartmentList(authUser);
            ViewBag.Roles = WebUtil.GetRoleList();
            return View(vm);
        }

        public ActionResult EditUserInfo(int Id)
        {
            User user = provider.Find(Id);
            if (user != null)
            {
                UserInfoViewModel vm = new UserInfoViewModel();
                vm.Id = user.Id;
                vm.Email = user.Email;
                vm.UserName = user.UserName;
                vm.Name = user.Name;
                return View(vm);
            }
            TempData.Flash("danger", "该用户不存在");
            return RedirectToAction("Index","Home");
        }

        [HttpPost]
        public ActionResult EditUserInfo(UserInfoViewModel vm)
        {
            if (ModelState.IsValid)
            {
                User user = provider.Find(vm.Id);
                user.Password = CommonUtil.MD5Encrypt64(vm.Password);
                user.Email = vm.Email;
                Respond respond = provider.Save(user);
                if (respond.Code == 1)
                {
                    TempData.Flash("success", string.Format("成功更新用户信息: {0}", user.UserName));
                    return RedirectToAction("Index","Home");
                }
                TempData.Flash("danger", respond.Message);
            }
            return View(vm);
        }

        [AdminAuth]
        public ActionResult Delete(int Id)
        {
            User user = provider.Find(Id);
            if (user != null)
            {
                if ((int)user.RoleType == (int)Role.SuperAdmin)
                {
                    TempData.Flash("danger", "权限不足，不能删除超级管理员.");
                    return RedirectToAction("index");
                }
                User sessionUser = (User)Session[SystemConfig.AuthUserKey];
                if (user.Id == sessionUser.Id)
                {
                    TempData.Flash("danger", "自己不能删除自己.");
                    return RedirectToAction("index");
                }
                Respond respond = provider.Delete(user);
                TempData.Flash("info", respond.Message);
                return RedirectToAction("index");
            }
            TempData.Flash("danger", "该用户不存在");
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public string MakePass(string p)
        {
            string x = CommonUtil.MD5Encrypt64(p);
            return x;
        }

        #region Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Login(LoginViewModel vm, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                Respond respond = provider.VerifyLogin(vm.UserName, vm.Password);
                if (respond.Code == 1)
                {
                    User user = respond.Data;
                    TempData.Flash("success", string.Format("{0}({1}),欢迎你， 登录成功", user.UserName, user.Name));
                    Session.Add(SystemConfig.AuthSaveKey, user.UserName);
                    Session.Add(SystemConfig.AuthUserKey, user);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            TempData.Flash("danger", "登录失败，请重试！");
            return RedirectToAction("login");

        }

        public ActionResult Logout()
        {
            Session.Remove(SystemConfig.AuthSaveKey);
            TempData.Flash("success", "成功登出！");
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult CheckUserName(string username)
        {
            return Json(provider.HasUserName(username));
        }
        #endregion


        [HttpPost]
        public JsonResult ValidateUserName(string username, string initialUserName)
        {
            if (username != initialUserName)
            {
                return Json(!provider.HasUserName(username));
            }
            return Json(true);

        }

        [HttpPost]
        public JsonResult ValidateEmail(string email, string initialEmail)
        {
            if (email != initialEmail)
            {
                return Json(!provider.HasEmail(email));
            }
            return Json(true);

        }
    }
}