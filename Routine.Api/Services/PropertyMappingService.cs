using Routine.Api.Entities;
using Routine.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Routine.Api.Services
{

    public class PropertyMappingService : IPropertyMappingService
    {
        private readonly Dictionary<string, PropertyMappingValue> _companyPropertyMapping =
    new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
{
            {"Id",new PropertyMappingValue(new List<string>{ "Id"}) },
            {"CompanyName",new PropertyMappingValue(new List<string>{ "Name"}) },
            {"Intruction",new PropertyMappingValue(new List<string>{ "Intruction"}) },
            {"Industry",new PropertyMappingValue(new List<string>{ "Industry"}) },
            {"Product",new PropertyMappingValue(new List<string>{ "Product"}) },
            {"Country",new PropertyMappingValue(new List<string>{ "Country"}) },
};

        private readonly Dictionary<string, PropertyMappingValue> _employeePropertyMapping =
            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
        {
            {"Id",new PropertyMappingValue(new List<string>{ "Id"}) },
            {"CompanyId",new PropertyMappingValue(new List<string>{ "CompanyId"}) },
            {"EmployeeNo",new PropertyMappingValue(new List<string>{ "EmployeeNo"}) },
            {"Name",new PropertyMappingValue(new List<string>{ "FirstName","LastName"}) },
            {"GenderDisplay",new PropertyMappingValue(new List<string>{ "Gender"}) },
            {"Age",new PropertyMappingValue(new List<string>{ "DateOfBirth"},revert:true) },
        };

        private readonly IList<IPropertyMapping> _propertyMappings = new List<IPropertyMapping>();

        public PropertyMappingService()
        {
            _propertyMappings.Add(new PropertyMapping<EmployeeDto, Employee>(_employeePropertyMapping));
            _propertyMappings.Add(new PropertyMapping<CompanyDto, Company>(_companyPropertyMapping));
        }

        public Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>()
        {
            var matchingMapping = _propertyMappings.OfType<PropertyMapping<TSource, TDestination>>();
            var matchingMappings = matchingMapping.ToList();
            if (matchingMappings.Count == 1)
            {
                return matchingMappings.First().MappingDictionary;
            }
            throw new Exception($"Can not find the unique mapping relation: {typeof(TSource)},{typeof(TDestination)}");
        }

        public bool ValidMappingExistFor<TSource, TDestination>(string fields)
        {
            var propertyMapping = GetPropertyMapping<TSource, TDestination>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                return true;
            }

            var fieldAfterSplit = fields.Split(",");
            foreach (var field in fieldAfterSplit)
            {
                var trimmedField = field.Trim();

                var indexOfFirstSpace = trimmedField.IndexOf(" ", StringComparison.OrdinalIgnoreCase);

                var propertyName = indexOfFirstSpace == -1
                    ? trimmedField
                    : trimmedField.Remove(indexOfFirstSpace);

                if (!propertyMapping.Keys.Contains(field))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
