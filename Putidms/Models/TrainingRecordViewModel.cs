using Putidms.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Putidms.Models
{
    public class TrainingRecordViewModel
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }


        
        [StringLength(500, MinimumLength = 2, ErrorMessage = "{0}长度为{2}-{1}个字符")]
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "培训名称")]
        public string Name { get; set; }


        [StringLength(500, MinimumLength = 2, ErrorMessage = "{0}长度为{2}-{1}个字符")]
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "培训地点")]
        public string Location { get; set; }



        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "培训内容")]
        public string Content { get; set; }



        [DataType(DataType.MultilineText)]
        [Display(Name = "备注")]
        public string Remark { get; set; }


        [Required(ErrorMessage = "{0}不能为空")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "开始日期")]
        public DateTime? FromDate { get; set; }


        [Required(ErrorMessage = "{0}不能为空")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "结束日期")]
        public DateTime? ToDate { get; set; }


        [HiddenInput(DisplayValue = false)]
        [Display(Name = "辅导员ID")]
        public int CounselorId { get; set; }

        [HiddenInput(DisplayValue = false)]
        [Display(Name = "辅导员")]
        public Counselor Counselor { get; set; }
    }
}