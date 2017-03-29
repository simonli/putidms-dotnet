﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Putidms.Models
{
    public class UserViewModel
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }


        /// <summary>
        /// 用户名
        /// </summary>
        [Remote("ValidateUserName", "User", HttpMethod = "Post", ErrorMessage = "{0}已存在",AdditionalFields = "InitialUserName")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "{0}长度为{2}-{1}个字符")]
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        /// <summary>
        /// 姓名【可用作昵称、真实姓名等】
        /// </summary>
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
        [Required(ErrorMessage = "{0}不能为空")]
        [DataType(DataType.Password)]
        [StringLength(1000, ErrorMessage = "{0}长度少于{1}个字符")]
        [Display(Name = "密码")]
        public string Password { get; set; }

        /// <summary>
        /// 确认密码
        /// </summary>
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "两次输入密码不一致")]
        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [Remote("ValidateEmail", "User", HttpMethod = "Post", ErrorMessage = "{0}已经存在", AdditionalFields = "InitialEmail")]
        [Required(ErrorMessage = "{0}不能为空")]       
        [StringLength(100, MinimumLength = 4, ErrorMessage = "{0}长度为{2}-{1}个字符")]
        [DataType(DataType.EmailAddress, ErrorMessage = "请输入正确的{0}地址")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "角色权限")]
        public int RoleType { get; set; }

        /// <summary>
        /// 所属修学处
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "所属修学处")]
        public int DivisionId { get; set; }

        /// <summary>
        /// 所属修学点
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "所属修学点")]
        public int DepartmentId { get; set; }

        /// <summary>
        /// 权限范围类型
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "权限范围")]
        public int PermissionInType { get; set; }
    }
    
}