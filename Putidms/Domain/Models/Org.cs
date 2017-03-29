using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Putidms.Domain.Models
{
    public class Org
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(1000)]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "{0}长度为{2}-{1}个字符")]
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "名称")]
        public string Name { get; set; }


        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(5000)]
        [StringLength(1000, ErrorMessage = "{0}不能超过1000个字符")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "简要描述")]
        public string Desc { get; set; }

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