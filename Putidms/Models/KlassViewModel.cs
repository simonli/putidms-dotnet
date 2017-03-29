using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Putidms.Models
{
    public class KlassViewModel
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }


        /// <summary>
        /// 名称
        /// </summary>
        [Remote("ValidateKlassName", "Klass", HttpMethod = "Post", ErrorMessage = "{0}已存在", AdditionalFields = "InitialName")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "{0}长度为{2}-{1}个字符")]
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "班级名称")]
        public string Name { get; set; }


        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(1000, ErrorMessage = "{0}不能超过1000个字符")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "简要描述")]
        public string Desc { get; set; }

        /// <summary>
        /// 班级编号
        /// </summary>
        [Remote("ValidateKlassNumber", "Klass", HttpMethod = "Post", ErrorMessage = "{0}已存在", AdditionalFields = "InitialNumber")]
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "班级编号")]
        public string Number { get; set; }


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


    }
}