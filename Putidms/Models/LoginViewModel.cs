using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Putidms.Models
{
    public class LoginViewModel
    {
        [HiddenInput(DisplayValue =true)]
        public int Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Remote("CheckUserName", "User", HttpMethod = "Post", ErrorMessage = "{0}不存在！")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "{0}长度为{2}-{1}个字符")]
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空")]
        [DataType(DataType.Password)]
        [StringLength(1000, ErrorMessage = "{0}长度少于{1}个字符")]
        [Display(Name = "密码")]
        public string Password { get; set; }


        [HiddenInput(DisplayValue = true)]
        public string ReturlUrl { get; set; }
    }
}