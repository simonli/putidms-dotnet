using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Putidms.Common
{
    public class EntityUtil
    {

        /// <summary>
        /// 用TSourceEntity的属性值覆盖TDestinationEntity的属性值，并返回TDestinationEntity
        /// </summary>
        /// <typeparam name="TSourceEntity"></typeparam>
        /// <typeparam name="TDestinationEntity"></typeparam>
        /// <param name="sourceEntity"></param>
        /// <param name="destinationEntity"></param>
        /// <returns></returns>
        public static TDestinationEntity GetEntity<TSourceEntity, TDestinationEntity>(TSourceEntity sourceEntity, TDestinationEntity destinationEntity)
        {
            foreach (PropertyInfo p in sourceEntity.GetType().GetProperties())
            {
                if (p.GetValue(sourceEntity) != null)
                {
                    destinationEntity.GetType().GetProperty(p.Name).SetValue(destinationEntity, p.GetValue(sourceEntity));
                }
            }
            return destinationEntity;
        }


    }
}
