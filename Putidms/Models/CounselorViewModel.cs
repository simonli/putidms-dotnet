using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Putidms.Models
{
    public class CounselorViewModel
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Remote("ValidateUserName", "Counselor",HttpMethod ="Post", ErrorMessage = "{0}已存在", AdditionalFields ="InitialUserName")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "{0}长度为{2}-{1}个字符")]
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "姓名")]
        public string UserName { get; set; }


        /// <summary>
        /// 法名
        /// </summary>
        [StringLength(100, MinimumLength = 2, ErrorMessage = "{0}长度为{2}-{1}个字符")]
        [Display(Name = "法名")]
        public string ReligiousName { get; set; }


        /// <summary>
        /// 性别【0-女，1-男】
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空")]
        [Range(0, 1, ErrorMessage = "{0}范围{1}-{2}")]
        [Display(Name = "性别")]
        public int Sex { get; set; }


        /// <summary>
        /// 生日
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "生日")]
        public DateTime? Birthday { get; set; }


        /// <summary>
        /// 手机
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空")]
        [Phone(ErrorMessage = "请输入正确的{0}")]
        [Display(Name = "手机号码")]
        public string Mobile { get; set; }


        /// <summary>
        /// Email
        /// </summary>
        [Remote("ValidateEmail", "Counselor", HttpMethod = "Post", ErrorMessage = "{0}已存在", AdditionalFields = "InitialEmail")]
        [EmailAddress(ErrorMessage = "请输入格式正确的{0}")]
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "Email")]
        public string Email { get; set; }


        /// <summary>
        /// 所属班级
        /// </summary>
        [Required(ErrorMessage ="{0}不能为空")]
        [Display(Name ="所带班级")]
        public int KlassId { get; set; }


        /// <summary>
        /// 岗位
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "带班岗位")]
        public int DutyId { get; set; }



        /// <summary>
        /// 所属修学处
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "带班所属修学处")]
        public int DivisionId { get; set; }

        /// <summary>
        /// 所属修学点
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "带班所属修学点")]
        public int DepartmentId { get; set; }
    }
}