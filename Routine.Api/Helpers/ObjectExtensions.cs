﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace Routine.Api.Helpers
{
    public static class ObjectExtensions
    {
        public static ExpandoObject ShapedData<TSource>(this TSource source, string fields)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            var expandoObj = new ExpandoObject();
            var propertyInfos = new List<PropertyInfo>();
            if (string.IsNullOrWhiteSpace(fields))
            {
                propertyInfos =
                    typeof(TSource).GetProperties(
                    BindingFlags.IgnoreCase |
                    BindingFlags.Public |
                    BindingFlags.Instance).ToList();
            }
            else
            {
                var fieldsAfterSplit = fields.Split(",");
                foreach (var field in fieldsAfterSplit)
                {
                    var propertyName = field.Trim();
                    var propertyInfo = typeof(TSource).GetProperty(
                        propertyName,
                        BindingFlags.IgnoreCase |
                        BindingFlags.Public |
                        BindingFlags.Instance);
                    if (propertyInfo == null)
                    {
                        throw new Exception($"Property: {propertyName} can not be found:{typeof(TSource)}");
                    }
                    propertyInfos.Add(propertyInfo);
                }
            }
            foreach (var propertyInfo in propertyInfos)
            {
                var propertyValue = propertyInfo.GetValue(source);
                ((IDictionary<string, object>)expandoObj).Add(propertyInfo.Name, propertyValue);
            }
            return expandoObj;
        }
    }
}
