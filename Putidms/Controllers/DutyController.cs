using AutoMapper;
using Putidms.Common;
using Putidms.Domain.Models;
using Putidms.Domain.Provider;
using Putidms.Filters;
using Putidms.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Putidms.Controllers
{
    [Auth]
    public class DutyController : Controller
    {
        DutyProvider dutyProvider = new DutyProvider();
        public ActionResult Index(string searchString)
        {
            IEnumerable<Duty> duties = dutyProvider.FindList();
            if (!string.IsNullOrEmpty(searchString))
            {
                duties = duties.Where(s => s.Name.Contains(searchString));
            }
            return View(duties);
        }

        public ActionResult Add()
        {
            return View("Edit", new DutyViewModel());
        }

        public ActionResult Edit(int Id)
        {
            Duty duty = dutyProvider.Find(Id);
            if (duty != null)
            {
                return View(Mapper.Map<Duty, DutyViewModel>(duty));
            }
            TempData.Flash("danger", "岗位不存在");
            return RedirectToAction("index");

        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(DutyViewModel vm)
        {
            if (vm.Id == 0 && dutyProvider.HasName(vm.Name)) ModelState.AddModelError("Name", "岗位已存在");
            if (ModelState.IsValid)
            {
                Respond respond = dutyProvider.Save(Mapper.Map<DutyViewModel, Duty>(vm));
                if (respond.Code == 1)
                {
                    TempData.Flash("success", string.Format("成功{0}岗位: {1}", vm.Id == 0 ? "新增" : "更新", vm.Name));
                    return RedirectToAction("index");
                }
                TempData.Flash("danger", respond.Message);
            }
            return View(vm);
        }

        [HttpPost]
        public JsonResult ValidateDutyName(string name, string initialName)
        {
            if (name != initialName)
            {
                return Json(!dutyProvider.HasName(name));
            }
            return Json(true);
        }
    }
}