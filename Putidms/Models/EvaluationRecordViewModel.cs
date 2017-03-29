using Putidms.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Putidms.Models
{
    public class EvaluationRecordViewModel
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }


        [StringLength(500, MinimumLength = 2, ErrorMessage = "{0}长度为{2}-{1}个字符")]
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "考核项目")]
        public string Item { get; set; }


        [Required(ErrorMessage = "{0}不能为空")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "考核日期")]
        public DateTime? ShiftDate { get; set; }


        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "考核得分")]
        public int Score { get; set; }



        [HiddenInput(DisplayValue = false)]
        [Display(Name = "辅导员ID")]
        public int CounselorId { get; set; }

        [HiddenInput(DisplayValue = false)]
        [Display(Name = "辅导员")]
        public Counselor Counselor { get; set; }
    }
}