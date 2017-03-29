using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Putidms.Domain.Models
{
    public class Counselor
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [MaxLength(500)]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "{0}长度为{2}-{1}个字符")]
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name ="姓名")]
        public string UserName { get; set; }


        /// <summary>
        /// 法名
        /// </summary>
        [MaxLength(500)]
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
        [Required(ErrorMessage ="{0}不能为空")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name ="生日")]
        public DateTime Birthday { get; set; }


        /// <summary>
        /// 手机
        /// </summary>
        [MaxLength(100)]
        [Required(ErrorMessage = "{0}不能为空")]
        [Phone(ErrorMessage ="请输入正确的{0}")]
        [Display(Name ="手机号码")]
        public string Mobile { get; set; }


        /// <summary>
        /// Email
        /// </summary>
        [MaxLength(100)]
        [EmailAddress(ErrorMessage ="请输入格式正确的{0}")]
        [Required(ErrorMessage ="{0}不能为空")]
        [Display(Name ="Email")]
        public string Email { get; set; }


        /// <summary>
        /// 所属班级
        /// </summary> 
        public int KlassId { get; set; }
        public virtual Klass Klass { get; set; }


        /// <summary>
        /// 岗位
        /// </summary>
        public int DutyId { get; set; }
        public virtual Duty Duty { get; set; }


        /// <summary>
        /// 更新者
        /// </summary>
        [HiddenInput(DisplayValue = false)]
        public int UpdateUser { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [HiddenInput(DisplayValue = false)]
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [HiddenInput(DisplayValue = false)]
        public DateTime? UpdateTime { get; set; }
        
    }
}