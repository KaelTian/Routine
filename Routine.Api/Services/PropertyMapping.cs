using System;
using System.Collections.Generic;

namespace Routine.Api.Services
{
    /// <summary>
    /// 只有通过继承标记接口才能定义TSource,TDestination,否则无法解析
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TDestination"></typeparam>
    public class PropertyMapping<TSource, TDestination>:IPropertyMapping
    {
        public Dictionary<string, PropertyMappingValue> MappingDictionary { get; private set; }
        public PropertyMapping(Dictionary<string, PropertyMappingValue> mappingDictionary)
        {
            MappingDictionary = mappingDictionary
                                ?? throw new ArgumentNullException(nameof(mappingDictionary));
        }
    }
}
