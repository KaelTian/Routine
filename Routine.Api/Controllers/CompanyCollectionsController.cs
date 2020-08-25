using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Routine.Api.Entities;
using Routine.Api.Helpers;
using Routine.Api.Models;
using Routine.Api.Services;

namespace Routine.Api.Controllers
{
    [Route("api/companycollections")]
    public class CompanyCollectionsController : ApiBaseController
    {
        public CompanyCollectionsController(ICompanyRepository companyRepository, IMapper mapper) : base(companyRepository, mapper)
        {

        }


        [HttpGet("({ids})", Name = nameof(GetCompanyCollection))]
        public async Task<IActionResult> GetCompanyCollection(
            [FromRoute]
            [ModelBinder(BinderType =typeof(ArrayModelBinder))]
            IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                return BadRequest();
            }
            var entities = await _companyRepository.GetCompaniesAsync(ids);
            if (ids.Count() != entities.Count())
            {
                return NotFound();
            }

            var returnDtos = _mapper.Map<IEnumerable<CompanyDto>>(entities);
            return Ok(returnDtos);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> CreateCompanyCollection(
            IEnumerable<CompanyAddDto> companyCollection)
        {
            var companyEntities = _mapper.Map<IEnumerable<Company>>(companyCollection);
            _companyRepository.AddCompanies(companyEntities);
            var count = await _companyRepository.SaveAsync();
            var returnDtos = _mapper.Map<IEnumerable<CompanyDto>>(companyEntities);
            var idsString = string.Join(",", returnDtos.Select(a => a.Id));
            return CreatedAtRoute(nameof(GetCompanyCollection),
                new
                {
                    ids = idsString
                }, 
                returnDtos);
        }

    }
}
