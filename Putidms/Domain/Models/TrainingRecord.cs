using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Putidms.Domain.Models
{
    public class TrainingRecord
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }


        [MaxLength(500)]
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "培训名称")]
        public string Name { get; set; }


        [MaxLength(500)]
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
        public DateTime FromDate { get; set; }


        [Required(ErrorMessage = "{0}不能为空")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "结束日期")]
        public DateTime ToDate { get; set; }



        [Display(Name = "辅导员")]
        public int CounselorId { get; set; }
        public virtual Counselor Counselor { get; set; }

        public int UpdateUser { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }






    }
}