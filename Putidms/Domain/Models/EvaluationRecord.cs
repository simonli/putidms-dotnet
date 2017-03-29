using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Putidms.Domain.Models
{
    public class EvaluationRecord
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }


        [MaxLength(500)]
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "考核项目")]
        public string Item { get; set; }


        [Required(ErrorMessage = "{0}不能为空")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "考核日期")]
        public DateTime ShiftDate { get; set; }


        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "考核得分")]
        public int Score { get; set; }
        

        public int CounselorId { get; set; }

        public virtual Counselor Counselor { get; set; }

        public int UpdateUser { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }

    }
}