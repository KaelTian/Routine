using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Routine.Api.Entities;
using Routine.Api.Models;
using Routine.Api.ResourceParameters;
using Routine.Api.Services;

namespace Routine.Api.Controllers
{
    [Route("api/companies/{companyId}/employees")]
    [ResponseCache(CacheProfileName = "120sCacheProfile")]
    public class EmployeesController : ApiBaseController
    {
        public EmployeesController(ICompanyRepository companyRepository, IMapper mapper) : base(companyRepository, mapper)
        {

        }

        [HttpGet(Name = nameof(GetEmployeesForCompany))]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>>
            GetEmployeesForCompany(Guid companyId, [FromQuery] EmployeeDtoParameters parameters)
        {
            if (!await _companyRepository.CompanyExistAsync(companyId))
            {
                return NotFound();
            }
            var employees = await _companyRepository.GetEmployeesAsync(companyId, parameters);
            return Ok(_mapper.Map<IEnumerable<EmployeeDto>>(employees));
        }

        [HttpGet("{employeeId}", Name = nameof(GetEmployeeForCompany))]
        [ResponseCache(Duration = 60)]
        public async Task<ActionResult<EmployeeDto>>
            GetEmployeeForCompany(Guid companyId, Guid employeeId)
        {
            var employee = await _companyRepository.GetEmployeeAsync(companyId, employeeId);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<EmployeeDto>(employee));
        }

        [HttpPost(Name = nameof(CreateEmployeeForCompany))]
        public async Task<ActionResult<EmployeeDto>> CreateEmployeeForCompany(Guid companyId, EmployeeAddDto employee)
        {
            if (!await _companyRepository.CompanyExistAsync(companyId))
            {
                return NotFound();
            }

            var entity = _mapper.Map<Employee>(employee);
            _companyRepository.AddEmployee(companyId, entity);
            await _companyRepository.SaveAsync();
            var returnDto = _mapper.Map<EmployeeDto>(entity);
            return CreatedAtRoute(nameof(GetEmployeeForCompany),
                new
                {
                    companyId,
                    employeeId = returnDto.Id
                }, returnDto);

        }

        #region Http Put
        /// <summary>
        /// PUT 是整体替换,某些属性不想更新的话也要将原值写上,因此用的地方较少
        /// PUT 操作是幂等性
        /// PUT 操作可同时实现Update和Create
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="employeeId"></param>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPut("{employeeId}")]
        public async Task<ActionResult<EmployeeDto>> UpdateEmployeeForCompany(
            Guid companyId,
            Guid employeeId,
            EmployeeUpdateDto employee)
        {
            if (!await _companyRepository.CompanyExistAsync(companyId))
            {
                return NotFound();
            }
            var employeeEntity = await _companyRepository.GetEmployeeAsync(companyId, employeeId);
            if (employeeEntity == null)
            {
                //不存在就添加
                var employeeAddEntity = _mapper.Map<Employee>(employee);
                employeeAddEntity.Id = employeeId;
                employeeAddEntity.CompanyId = companyId;
                _companyRepository.AddEmployee(companyId, employeeAddEntity);
                await _companyRepository.SaveAsync();
                var returnDto = _mapper.Map<EmployeeDto>(employeeAddEntity);
                return CreatedAtRoute(nameof(GetEmployeeForCompany),
                new
                {
                    companyId,
                    employeeId = returnDto.Id
                }, returnDto);
            }
            else
            {
                _mapper.Map(employee, employeeEntity);
                //update 方法里并不需要实现,EF Core内部机制会对数据的改变进行跟踪.
                _companyRepository.UpdateEmployee(employeeEntity);
                await _companyRepository.SaveAsync();
                return NoContent();
            }

        }

        #endregion
        /// <summary>
        /// HTTP Patch 客户端数据需要遵循一定的数据规范
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="employeeId"></param>
        /// <param name="patchDocument"></param>
        /// <returns></returns>
        [HttpPatch("{employeeId}")]
        public async Task<IActionResult> PartiallyUpdateEmployeeForCompany(
            Guid companyId,
            Guid employeeId,
            JsonPatchDocument<EmployeeUpdateDto> patchDocument)
        {
            if (!await _companyRepository.CompanyExistAsync(companyId))
            {
                return NotFound();
            }
            var employeeEntity = await _companyRepository.GetEmployeeAsync(companyId, employeeId);
            if (employeeEntity == null)
            {
                var employeeDto = new EmployeeUpdateDto();
                patchDocument.ApplyTo(employeeDto, ModelState);
                if (!TryValidateModel(employeeDto))
                {
                    return ValidationProblem(ModelState);
                }
                var employeeToAdd = _mapper.Map<Employee>(employeeDto);
                employeeToAdd.Id = employeeId;
                employeeToAdd.CompanyId = companyId;
                _companyRepository.AddEmployee(companyId, employeeToAdd);
                await _companyRepository.SaveAsync();
                var returnDto = _mapper.Map<EmployeeDto>(employeeToAdd);
                return CreatedAtRoute(nameof(GetEmployeeForCompany),
                new
                {
                    companyId,
                    employeeId = returnDto.Id
                }, returnDto);
            }

            var dtoToPatch = _mapper.Map<EmployeeUpdateDto>(employeeEntity);
            //需要处理一些冲突.
            //处理验证错误
            patchDocument.ApplyTo(dtoToPatch, ModelState);
            if (!TryValidateModel(dtoToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(dtoToPatch, employeeEntity);
            _companyRepository.UpdateEmployee(employeeEntity);
            await _companyRepository.SaveAsync();
            return NoContent();
        }


        public override ActionResult ValidationProblem([ActionResultObjectValue] ModelStateDictionary modelStateDictionary)
        {
            var options = HttpContext.RequestServices.GetRequiredService<IOptions<ApiBehaviorOptions>>();

            return (ActionResult)options.Value.InvalidModelStateResponseFactory(ControllerContext);
        }

        [HttpDelete("{employeeId}")]
        public async Task<IActionResult> DeleteEmployeeForCompany(Guid companyId, Guid employeeId)
        {
            if (!await _companyRepository.CompanyExistAsync(companyId))
            {
                return NotFound();
            }
            var employee = await _companyRepository.GetEmployeeAsync(companyId, employeeId);
            if (employee == null)
            {
                return NotFound();
            }
            _companyRepository.DeleteEmployee(employee);
            await _companyRepository.SaveAsync();
            return NoContent();
        }
    }
}
