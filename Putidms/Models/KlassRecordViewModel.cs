using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Putidms.Models
{
    public class KlassRecordViewModel
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }


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
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "辅导员ID")]
        public int CounselorId { get; set; }

        [Display(Name = "所属修学处")]
        public int DivisionId { get; set; }


        
        [Display(Name = "所属修学点")]
        public int DepartmentId { get; set; }

        //[Remote("ValidateKlassId", "KlassRecord", HttpMethod = "Post", ErrorMessage = "{0}已存在", AdditionalFields = "CounselorId,InitialKlassId")]
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "所带班级")]
        public int KlassId { get; set; }



        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "从事岗位")]
        public int DutyId { get; set; }
    }
}