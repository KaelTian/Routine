using Routine.Api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Routine.Api.ValidationAttributes
{
    public class EmployeeNoMustDifferentFromFirstNameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //validationContext.ObjectInstance始终是被访问的对象实体
            //value如果作用于属性它就是属性,作用于类就是类对象.
            //所以用validationContext.ObjectInstance
            var employeeAddOrUpdateDto = (EmployeeAddOrUpdateDto)validationContext.ObjectInstance;
            if (employeeAddOrUpdateDto.EmployeeNo == employeeAddOrUpdateDto.FirstName)
            {
                return new ValidationResult(ErrorMessage,
                    new[] { nameof(EmployeeAddOrUpdateDto) });
            }
            return ValidationResult.Success;
        }
    }
}
