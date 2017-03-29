using System.Collections.Generic;

namespace Putidms.Domain.Models
{
    public class Duty : Org
    {
        public virtual ICollection<Counselor> Counselors { get; set; }
    }
}