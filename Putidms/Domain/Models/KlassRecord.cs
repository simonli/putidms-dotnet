using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Putidms.Domain.Models
{
    public class KlassRecord
    {
        [Key]
        [HiddenInput(DisplayValue =false)]
        public int Id { get; set; }


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

        
        
        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name ="辅导员")]
        [ForeignKey("Counselor")]
        public int CounselorId { get; set; }
        public virtual Counselor Counselor { get; set; }

        [Display(Name = "带班班级")]       
        public int KlassId { get; set; }
        public virtual Klass Klass { get; set; }

        [Display(Name = "带班岗位")]
        public int DutyId { get; set; }
        public virtual Duty Duty { get; set; }


        public int UpdateUser { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }


        
        
        

    }
}