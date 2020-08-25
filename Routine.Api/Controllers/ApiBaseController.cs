using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Routine.Api.Services;

namespace Routine.Api.Controllers
{
    //[Route("api/[controller]")]//不建议这么写,应该保证uri不变
    //当controller使用[ApiController]属性进行注解时,遇到验证错误,name就会自动返回400错误,此时后台不需要再验证ModelState.IsValid
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        protected readonly ICompanyRepository _companyRepository;
        protected readonly IMapper _mapper;
        public ApiBaseController(ICompanyRepository companyRepository, IMapper mapper)
        {
            this._companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
    }
}
