using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Putidms.Domain.Models
{
    public class Department : Org
    {
        /// <summary>
        /// 所属修学处
        /// </summary>
        public int DivisionId { get; set; }

        [ForeignKey("DivisionId")]
        public virtual Division Division { get; set; }
        public virtual ICollection<Klass> Klasses { get; set; }
    }
}