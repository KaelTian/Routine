using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Routine.Api.Models
{
    //View model 和 EF实体类,还有Add/Update dto不应该用相同的,因为业务逻辑会有区别,方便扩展和重构
    //client数据的验证应该定义在这里,虽然EF实体对类的属性进行了相应的限制,但是如果client端数据不符合数据要求,错误应该发生在client数据端,因此应该对这些从client
    //端输入的数据进行类型验证.
    public class CompanyAddDto
    {
        [Display(Name ="公司名")]
        [Required(ErrorMessage ="{0}这个字段是必填的!!!")]
        [MaxLength(100,ErrorMessage ="{0}的最大长度不可以超过{1}")]
        public string Name { get; set; }

        [Display(Name="简介")]
        [StringLength(500,MinimumLength =10,ErrorMessage ="{0}的长度范围从{2}到{1}")]
        public string Intruction { get; set; }

        public ICollection<EmployeeAddDto> Employees { get; set; } = new List<EmployeeAddDto>();
    }
}
