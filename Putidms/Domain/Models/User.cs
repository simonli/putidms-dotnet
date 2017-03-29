using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Putidms.Domain.Models
{
    public class User
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary
        [MaxLength(100)]
        [Remote("CanUserName", "User", HttpMethod = "Post", ErrorMessage = "{0}已存在")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "{0}长度为{2}-{1}个字符")]
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        /// <summary>
        /// 姓名【可用作昵称、真实姓名等】
        /// </summary>
        [MaxLength(100)]
        [StringLength(20, ErrorMessage = "{0}必须少于{1}个字符")]
        [Display(Name = "姓名")]
        public string Name { get; set; }

        /// <summary>
        /// 性别【0-女，1-男】
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空")]
        [Range(0, 1, ErrorMessage = "{0}范围{1}-{2}")]
        [Display(Name = "性别")]
        public int Sex { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [MaxLength(2000)]
        [Required(ErrorMessage = "{0}不能为空")]
        [DataType(DataType.Password)]
        [StringLength(1000, ErrorMessage = "{0}长度少于{1}个字符")]
        [Display(Name = "密码")]
        public string Password { get; set; }


        /// <summary>
        /// Email
        /// </summary>
        [MaxLength(1000)]
        [Remote("CanEmail", "User", HttpMethod = "Post", ErrorMessage = "{0}已经存在")]
        [Required(ErrorMessage = "{0}不能为空")]
        [DataType(DataType.EmailAddress, ErrorMessage = "请输入正确的{0}地址")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "{0}长度为{2}-{1}个字符")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// 上一次登录IP
        /// </summary>
        [MaxLength(100)]
        [Display(Name = "上次登录IP")]
        public string LastLoginIP { get; set; }

        /// <summary>
        /// 上一次登录时间
        /// </summary>
        [Display(Name = "上次登录日期")]
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 登录次数
        /// </summary>
        [Display(Name ="登录次数")]
        public int LoginCount { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "角色权限")]
        public Role RoleType { get; set; }

        /// <summary>
        /// 所属修学处
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "所属修学处")]        
        public int DivisionId { get; set; }
        public virtual Division Division { get; set; }

        /// <summary>
        /// 所属修学点
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "所属修学点")]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        /// <summary>
        /// 权限范围类型
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "权限范围")]
        public PermissionIn PermissionInType { get; set; }

        /// <summary>
        /// 最后更新用户
        /// </summary>
        [Display(Name = "更新者")]
        public int UpdateUser { get; set; }


        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime? CreateTime { get; set; }


        /// <summary>
        /// 更新时间
        /// </summary>
        [Display(Name = "更新时间")]
        public DateTime? UpdateTime { get; set; }
    }


    /// <summary>
    /// 角色
    /// </summary>
    public enum Role
    {
        [Description("修学处档案义工")]
        StudyDivisionVolunteer = 100,


        [Description("辅导委档案义工")]
        CounsellingVolunteer = 300,


        [Description("管理员")]
        Admin = 300,

        [Description("超级管理员")]
        SuperAdmin = 999



    }

    public enum PermissionIn
    {
        [Description("修学处")]
        Division = 1,


        [Description("修学点")]
        Department = 2,

    }
}