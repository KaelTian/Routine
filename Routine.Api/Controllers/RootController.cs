using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Routine.Api.Models;
using Routine.Api.Services;
using System.Collections.Generic;

namespace Routine.Api.Controllers
{
    [Route("api")]
    public class RootController : ApiBaseController
    {
        public RootController(ICompanyRepository companyRepository, IMapper mapper) : base(companyRepository, mapper)
        {

        }

        [HttpGet(Name = nameof(GetRoot))]
        public IActionResult GetRoot()
        {
            var links = new List<LinkDto>();

            links.Add(new LinkDto(Url.Link(nameof(GetRoot), new { }), "self", "GET"));

            links.Add(new LinkDto(Url.Link(nameof(CompaniesController.GetCompanies), new { }), "companies", "GET"));

            links.Add(new LinkDto(Url.Link(nameof(CompaniesController.CreateCompany), new { }), "create_company", "POST"));

            return Ok(links);
        }
    }
}
