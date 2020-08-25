

using System.ComponentModel.DataAnnotations;

namespace Routine.Api.Models
{
    public class EmployeeAddDto : EmployeeAddOrUpdateDto
    {
        //必填项
        [Required(ErrorMessage = "{0}是必填项")]
        public override string EmployeeNo { get; set; }
    }
}
