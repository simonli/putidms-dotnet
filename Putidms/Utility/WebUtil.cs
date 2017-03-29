using Putidms.Domain.Models;
using Putidms.Domain.Provider;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Putidms.Web.Utility
{
    public class WebUtil
    {
        public static IEnumerable<SelectListItem> GetDivisionList(User authUser,string selectedItem = "")
        {
            var divs = new DivisionProvider().FindList();

            if ((int)authUser.RoleType != (int)Role.Admin && (int)authUser.RoleType != (int)Role.SuperAdmin)
            {

                if ((int)authUser.PermissionInType == (int)PermissionIn.Department)
                {
                    divs = divs.Where(x => x.Id == authUser.Department.DivisionId);
                    selectedItem = authUser.DivisionId.ToString();
                }
                if ((int)authUser.PermissionInType == (int)PermissionIn.Division)
                {
                    divs = divs.Where(x => x.Id == authUser.DivisionId);
                    selectedItem = authUser.DivisionId.ToString();
                }
            }
            List<SelectListItem> listItems = new List<SelectListItem>();
            foreach (var div in divs)
            {
                listItems.Add(new SelectListItem
                {
                    Text = div.Name,
                    Value = div.Id.ToString(),
                    Selected = div.Id.ToString() == selectedItem
                });
            }
            return listItems;
        }

        public static IEnumerable<SelectListItem> GetDepartmentList(User authUser, string selectedItem = "")
        {
            var depts = new DepartmentProvider().FindList();

            if ((int)authUser.RoleType != (int)Role.Admin && (int)authUser.RoleType != (int)Role.SuperAdmin)
            {
                if ((int)authUser.PermissionInType == (int)PermissionIn.Department)
                {
                    depts = depts.Where(x => x.Id == authUser.DepartmentId);
                    selectedItem = authUser.DepartmentId.ToString();
                }
                if ((int)authUser.PermissionInType == (int)PermissionIn.Division)
                {
                    depts = depts.Where(x => x.DivisionId == authUser.DivisionId);
                    selectedItem = authUser.DepartmentId.ToString();
                }
            }
            List<SelectListItem> listItems = new List<SelectListItem>();
            foreach (var dept in depts)
            {
                listItems.Add(new SelectListItem
                {
                    Text = dept.Name,
                    Value = dept.Id.ToString(),
                    Selected = dept.Id.ToString() == selectedItem
                });
            }
            return listItems;
        }


        public static IEnumerable<SelectListItem> GetKlassList(User authUser,string selectedItem = "")
        {
            var klasses = new KlassProvider().FindList();

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

            List<SelectListItem> listItems = new List<SelectListItem>();
            foreach (var klass in klasses)
            {
                listItems.Add(new SelectListItem
                {
                    Text = klass.Name,
                    Value = klass.Id.ToString(),
                    Selected = klass.Id.ToString() == selectedItem
                });
            }
            return listItems;
        }

        public static IEnumerable<SelectListItem> GetDutyList(string selectedItem = "")
        {
            var duties = new DutyProvider().FindList();
            List<SelectListItem> listItems = new List<SelectListItem>();
            foreach (var duty in duties)
            {
                listItems.Add(new SelectListItem
                {
                    Text = duty.Name,
                    Value = duty.Id.ToString(),
                    Selected = duty.Id.ToString() == selectedItem
                });
            }
            return listItems;
        }


        public static IEnumerable<SelectListItem> GetRoleList(string selectedItem = "")
        {
            List<SelectListItem> list = new List<SelectListItem>();
            List<SelectListItem> roleList= EnumCreateSelectList(typeof(Role), selectedItem);
            foreach(var x in roleList)
            {
                if (x.Value!= Convert.ToString((int)Role.SuperAdmin)) //排除超级管理员出现在界面的下拉选项中
                {
                    list.Add(x);
                }
            }
            return list;
        }

        public static List<SelectListItem> EnumCreateSelectList(Type enumType, string selectedItem)
        {
            return (from object item in Enum.GetValues(enumType)
                    let fi = enumType.GetField(item.ToString())
                    let attribute = fi.GetCustomAttributes(typeof(DescriptionAttribute), true).FirstOrDefault()
                    let title = attribute == null ? item.ToString() : ((DescriptionAttribute)attribute).Description
                    select new SelectListItem
                    {
                        Value = ((int)item).ToString(),
                        Text = title,
                        Selected = selectedItem == item.ToString()
                    }).ToList();
        }

        public static string GetEnumDescription(Enum enumValue)
        {
            string str = enumValue.ToString();
            System.Reflection.FieldInfo field = enumValue.GetType().GetField(str);
            object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (objs == null || objs.Length == 0) return str;
            DescriptionAttribute da = (DescriptionAttribute)objs[0];
            return da.Description;
        }

       
    }
}