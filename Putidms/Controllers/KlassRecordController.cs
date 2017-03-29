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
    public class KlassRecordController : Controller
    {
        KlassRecordProvider provider = new KlassRecordProvider();
        
        public ActionResult Index(int counselorId = 0)
        {            
            if (counselorId > 0)
            {
                Counselor counselor = new CounselorProvider().Find(counselorId);
                if (counselor != null)
                {
                    IEnumerable<KlassRecord> records = provider.FindList().Where(x => x.Counselor.Id == counselorId).OrderByDescending(x => x.CreateTime);
                    ViewBag.Counselor = counselor;
                    return View(records);
                }
                else
                {
                    TempData.Flash("danger", "辅导员不存在");
                    return RedirectToAction("Index", "Counselor");
                }
            }
            else
            {
                TempData.Flash("danger", "请先选择辅导员，然后点击查看该辅导员的带班级记录");
                return RedirectToAction("Index", "Counselor");
            }
           
        }

        public ActionResult Add(int counselorId = 0)
        {
            if (counselorId > 0)
            {
                Counselor counselor = new CounselorProvider().Find(counselorId);
                if (counselor != null)
                {
                    ViewBag.Counselor = counselor;
                    User authUser = (User)Session[SystemConfig.AuthUserKey];
                    ViewBag.Divisions = WebUtil.GetDivisionList(authUser);
                    ViewBag.Departments = WebUtil.GetDepartmentList(authUser);
                    ViewBag.Klasses = WebUtil.GetKlassList(authUser);
                    ViewBag.Duties = WebUtil.GetDutyList();
                    return View("Edit", new KlassRecordViewModel() { CounselorId = counselorId });
                }
                else
                {
                    TempData.Flash("danger", "辅导员不存在");
                    return RedirectToAction("Index", "Counselor");
                }
            }
            else
            {
                TempData.Flash("danger", "请先选择辅导员，然后点击右侧的添加带班记录栏位的“+”进行添加");
                return RedirectToAction("Index", "Counselor");
            }
        }

        public ActionResult Edit(int Id)
        {
            KlassRecord record = provider.Find(Id);
            if (record != null)
            {
                Counselor counselor = record.Counselor;
                if (counselor != null)
                {
                    KlassRecordViewModel vm = Mapper.Map<KlassRecord,KlassRecordViewModel>(record);
                    vm.CounselorId = record.CounselorId;
                    vm.DepartmentId = record.Klass.DepartmentId;
                    vm.DivisionId = record.Klass.Department.DivisionId;                   
                    ViewBag.Counselor = counselor;
                    User authUser = (User)Session[SystemConfig.AuthUserKey];
                    ViewBag.Divisions = WebUtil.GetDivisionList(authUser);
                    ViewBag.Departments = WebUtil.GetDepartmentList(authUser);
                    ViewBag.Klasses = WebUtil.GetKlassList(authUser);
                    ViewBag.Duties = WebUtil.GetDutyList();
                    return View(vm);
                }
                else
                {
                    TempData.Flash("danger", "辅导员不存在");
                    return RedirectToAction("Index", "Counselor");
                }
            }
            TempData.Flash("danger", "该带班记录不存在");
            return RedirectToAction("Index", "Counselor");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(KlassRecordViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Counselor counselor = new CounselorProvider().Find(vm.CounselorId);
                if (counselor != null)
                {
                    Respond respond = new Respond();
                    respond = provider.Save(Mapper.Map<KlassRecordViewModel, KlassRecord>(vm));
                    if (respond.Code == 1)
                    {
                        TempData.Flash("success", string.Format("成功{0}带班记录", vm.Id == 0 ? "新增" : "更新"));
                        return RedirectToAction("Index", new { counselorId = counselor.Id });
                    }
                    TempData.Flash("danger", respond.Message);
                    return View(vm);
                }
                else
                {
                    TempData.Flash("danger", "辅导员不存在");
                    return RedirectToAction("Index", "Counselor");
                }
            }
            return View(vm);
        }

        public ActionResult Delete(int Id)
        {
            KlassRecord record = provider.Find(Id);
            if (record != null)
            {
                Counselor counselor = record.Counselor;
                Respond respond = provider.Delete(record);
                TempData.Flash("success", respond.Message);
                return RedirectToAction("index", new { counselorId = counselor.Id });
            }
            else
            {
                TempData.Flash("danger", "该带班记录不存在");
                return RedirectToAction("Index", "Counselor");
            }
            
        }


        [HttpPost]
        public JsonResult ValidateKlassId(int klassId, int counselorId, int initialKlassId)
        {
            if (klassId != initialKlassId)
            {
                return Json(!provider.HasRecord(counselorId, klassId));
            }
            return Json(true);
        }
    }
}