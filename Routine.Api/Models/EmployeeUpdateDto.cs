

namespace Routine.Api.Models
{
    public class EmployeeUpdateDto : EmployeeAddOrUpdateDto
    {
        //非必填项
        public override string EmployeeNo
        { get; set; }
    }
}
