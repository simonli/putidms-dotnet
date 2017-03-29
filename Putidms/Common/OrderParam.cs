using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Putidms.Common
{
    public class OrderParam
    {
        public string PropertyName { get; set; }
        public OrderMethod Method { get; set; }

    }
    public enum OrderMethod
    {
        ASC,
        DESC
    }
}
