using System.Collections.Generic;

namespace Putidms.Domain.Models
{
    public class Division : Org
    {
        public virtual ICollection<Department> Departments { get; set; }
    }
}