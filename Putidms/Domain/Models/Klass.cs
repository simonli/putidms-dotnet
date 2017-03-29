using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Putidms.Domain.Models
{
    public class Klass : Org
    {
        /// <summary>
        /// 班级编号
        /// </summary>
        [Display(Name = "班级编号")]
        public string Number { get; set; }


        /// <summary>
        /// 所属修学点
        /// </summary>
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<Counselor> Counselors { get; set; }
    }
}