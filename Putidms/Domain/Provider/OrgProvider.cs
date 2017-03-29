using Putidms.Common;
using Putidms.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Putidms.Domain.Provider
{
    public class DivisionProvider : BaseProvider<Division>
    {
        public bool HasName(string name)
        {
            return base.Repos.IsContains(u => u.Name.ToUpper() == name.ToUpper());
        }
    }

    public class DepartmentProvider : BaseProvider<Department>
    {
        public bool HasName(string name)
        {
            return base.Repos.IsContains(u => u.Name.ToUpper() == name.ToUpper());
        }
    }

    public class KlassProvider : BaseProvider<Klass>
    {
        public bool HasName(string name)
        {
            return base.Repos.IsContains(u => u.Name.ToUpper() == name.ToUpper());
        }

        public bool HasNumber(string number)
        {
            return base.Repos.IsContains(u => u.Number.ToUpper() == number.ToUpper());
        }
    }

    public class DutyProvider : BaseProvider<Duty>
    {
        public bool HasName(string name)
        {
            return base.Repos.IsContains(u => u.Name.ToUpper() == name.ToUpper());
        }
        
    }
}
