using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Putidms.Common
{
    public static class Extension
    {
        public static TempDataDictionary Flash(this TempDataDictionary tempData, string key, object value)
        {
            tempData.Remove(key);
            tempData.Add(key, value);
            return tempData;
        }


        public static IMappingExpression<TSource, TDestination> IgnoreReadOnly<TSource, TDestination>(
                   this IMappingExpression<TSource, TDestination> expression)
        {
            var sourceType = typeof(TSource);

            foreach (var property in sourceType.GetProperties())
            {
                PropertyDescriptor descriptor = TypeDescriptor.GetProperties(sourceType)[property.Name];
                ReadOnlyAttribute attribute = (ReadOnlyAttribute)descriptor.Attributes[typeof(ReadOnlyAttribute)];
                if (attribute.IsReadOnly == true)
                    expression.ForMember(property.Name, opt => opt.Ignore());
            }
            return expression;
        }

        public static IMappingExpression<TSource, TDest> IgnoreAllUnmapped<TSource, TDest>(this IMappingExpression<TSource, TDest> expression)
        {
            expression.ForAllMembers(opt => opt.Ignore());
            return expression;
        }

    }
}