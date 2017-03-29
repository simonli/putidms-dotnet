using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Putidms.Models
{
    public class DivisionViewModel
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Remote("ValidateDivisionName", "Division", HttpMethod = "Post", ErrorMessage = "{0}已存在", AdditionalFields = "InitialName")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "{0}长度为{2}-{1}个字符")]
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "修学处名称")]
        public string Name { get; set; }


        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(1000, ErrorMessage = "{0}不能超过1000个字符")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "简要描述")]
        public string Desc { get; set; }
    }
}