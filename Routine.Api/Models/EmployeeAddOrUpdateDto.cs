using Routine.Api.Entities;
using Routine.Api.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Emit;

namespace Routine.Api.Models
{

    //IValidatableObject
    /// <summary>
    /// 当System.ComponentModel.DataAnnotations的验证无法通过时,错误信息显示DataAnnotations定义的错误信息,
    /// DataAnnotation验证通过时,IValidatableObject验证无法通过显示IValidatableObject的错误信息,有优先级
    /// </summary>
    [EmployeeNoMustDifferentFromFirstNameAttribute(ErrorMessage = "员工编号必须和姓不一样!!!!!!!!")]
    public abstract class EmployeeAddOrUpdateDto : IValidatableObject
    {
        [Display(Name = "员工号")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "{0}的长度是{1}")]
        public abstract string EmployeeNo { get; set; }

        [Display(Name = "名")]
        [Required(ErrorMessage = "{0}是必填项")]
        [MaxLength(50, ErrorMessage = "{0}的长度不能超过{1}")]
        public string FirstName { get; set; }

        [Display(Name = "姓"), Required(ErrorMessage = "{0}是必填项"), MaxLength(50, ErrorMessage = "{0}的长度不能超过{1}")]
        public string LastName { get; set; }

        [Display(Name = "性别")]
        [EnumDataType(typeof(Gender),ErrorMessage ="请输入正确的枚举值(1:男,2:女)")]
        public Gender Gender { get; set; }

        [Display(Name = "出生日期")]
        [Range(typeof(DateTime), "1900-01-01", "2020-08-03", ErrorMessage = "不合理的出生日期")]
        public DateTime DateOfBirth { get; set; }

        //ValidationContext 上下文
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (FirstName == LastName)
            {
                yield return new ValidationResult("姓和名不能一样",
                    new[] { nameof(EmployeeAddOrUpdateDto) });
            }
        }
    }
}
