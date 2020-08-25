using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Routine.Api.ActionConstraints;
using Routine.Api.Entities;
using Routine.Api.Helpers;
using Routine.Api.Models;
using Routine.Api.ResourceParameters;
using Routine.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;


namespace Routine.Api.Controllers
{
    [Route("/api/companies")]  //建议这么写,保证即使controller名字变化,api 的uri 也不会发生变化
    public class CompaniesController : ApiBaseController
    {
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly IPropertyCheckerService _propertyCheckerService;

        public CompaniesController(
            ICompanyRepository companyRepository,
            IMapper mapper,
            IPropertyMappingService propertyMappingService,
            IPropertyCheckerService propertyCheckerService) : base(companyRepository, mapper)
        {
            this._propertyMappingService = propertyMappingService
                                           ?? throw new ArgumentNullException(nameof(propertyMappingService));
            this._propertyCheckerService = propertyCheckerService
                                           ?? throw new ArgumentNullException(nameof(propertyCheckerService));
        }

        [HttpGet(Name = nameof(GetCompanies))]
        [HttpHead]
        //FromQuery是重点,正常如果不指定fromquery就会报错
        public async Task<IActionResult> GetCompanies([FromQuery] CompanyDtoParameters parameters)
        {
            if (!_propertyMappingService.ValidMappingExistFor<CompanyDto, Company>(parameters.OrderBy) ||
                !_propertyCheckerService.TypeHasProperties<CompanyDto>(parameters.Fields))
            {
                return BadRequest();
            }

            var companies = await _companyRepository.GetCompaniesAsync(parameters);

            //var previousPageLink = companies.HasPrevious ?
            //    CreateCompaniesResourceUri(parameters, ResourceUriType.PreviousPage)
            //    : null;
            //var nextPageLink = companies.HasNext ?
            //    CreateCompaniesResourceUri(parameters, ResourceUriType.NextPage)
            //    : null;

            var oaginationMetadata = new
            {
                totalCount = companies.TotalCount,
                totalPages = companies.TotalPages,
                pageSize = companies.PageSize,
                currentPage = companies.CurrentPage,
                //previousPageLink,
                //nextPageLink
            };
            //自定义header 返回分页数据, 因为如果在OK体内返回其他信息不符合Restful api的设计原则.
            //方法是GetCompanies,应该只返回companies的数据,而不应该包含其他额外数据,额外数据应该通过自定义header返回.
            Response.Headers.Add("X-Pagination", System.Text.Json.JsonSerializer.Serialize(oaginationMetadata, new System.Text.Json.JsonSerializerOptions
            {
                //http web 默认会将一些字符转义,保护url,添加此属性不会将&符号转义.
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            }));

            var companyDtos = _mapper.Map<IEnumerable<CompanyDto>>(companies);
            var shapedData = companyDtos.ShapeData(parameters.Fields);

            var links = CreateLinksForCompany(parameters, companies.HasPrevious, companies.HasNext);

            var shapedCompaniesWithLinks = shapedData.Select(a =>
            {
                var companyDic = a as IDictionary<string, object>;
                var companyLinks = CreateLinksForCompany((Guid)companyDic["Id"], null);
                companyDic.Add("links", companyLinks);
                return companyDic;
            });

            var linkedCollectionResource = new
            {
                value = shapedCompaniesWithLinks,
                links
            };

            return Ok(linkedCollectionResource);
        }

        [Produces("application/json",
            "application/vnd.company.hateoas+json",
            "application/vnd.company.company.friendly+json",
            "application/vnd.company.company.friendly.hateoas+json",
            "application/vnd.company.company.full+json",
            "application/vnd.company.company.full.hateoas+json")]
        [HttpGet("{companyId:Guid}", Name = nameof(GetCompany))]
        public async Task<IActionResult> GetCompany(Guid companyId, string fields,
            [FromHeader(Name = "Accept")] string mediaType)
        {

            if (!MediaTypeHeaderValue.TryParse(mediaType, out MediaTypeHeaderValue parsedMediaType))
            {
                return BadRequest();
            }

            if (!_propertyCheckerService.TypeHasProperties<CompanyDto>(fields))
            {
                return BadRequest();
            }

            var company = await _companyRepository.GetCompanyAsync(companyId);

            if (company == null)
            {
                return NotFound();
            }


            //  application/vnd.company.company.full+json

            var includeLinks =
                parsedMediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);

            IEnumerable<LinkDto> myLinks = new List<LinkDto>();

            if (includeLinks)
            {
                myLinks = CreateLinksForCompany(companyId, fields);
            }

            var primaryMediaType = includeLinks ?
                parsedMediaType.SubTypeWithoutSuffix.Substring(0, parsedMediaType.SubTypeWithoutSuffix.Length - 8) :
                parsedMediaType.SubTypeWithoutSuffix;

            if (primaryMediaType == "vnd.company.company.full")
            {
                var full = _mapper.Map<CompanyFullDto>(company)
                    .ShapedData(fields) as IDictionary<string, object>;
                if (includeLinks)
                {
                    full.Add("links", myLinks);
                }

                return Ok(full);
            }

            var friendly = _mapper.Map<CompanyDto>(company).ShapedData(fields)
                    as IDictionary<string, object>;

            if (includeLinks)
            {
                friendly.Add("links", myLinks);
            }

            return Ok(friendly);
        }

        [HttpPost(Name = nameof(CreateCompanyWithBankruptTime))]
        //默认用这个HttpPost方法时,就是from body
        [RequestHeaderMatchesMediaType("Content-Type", "application/vnd.company.companyforcreationwithbankrupttime+json")]
        [Consumes("application/vnd.company.companyforcreationwithbankrupttime+json")]
        public async Task<IActionResult> CreateCompanyWithBankruptTime([FromBody] CompanyAddWithBankruptTimeDto company)
        {
            //这段代码现在可以去掉,最早版本Asp.Net Core不行
            //if (company == null)
            //{
            //    return BadRequest();
            //}

            var entity = _mapper.Map<Company>(company);
            _companyRepository.AddCompany(entity);
            var result = await _companyRepository.SaveAsync();
            var returnDto = _mapper.Map<CompanyDto>(entity);

            var links = CreateLinksForCompany(returnDto.Id, null);
            var linkedDict = returnDto.ShapedData(null)
                             as IDictionary<string, object>;
            linkedDict.Add("links", links);
            return CreatedAtRoute(nameof(GetCompany), new { companyId = linkedDict["Id"] }, linkedDict);
        }


        [HttpPost(Name = nameof(CreateCompany))]
        //默认用这个HttpPost方法时,就是from body
        [RequestHeaderMatchesMediaType("Content-Type", "application/json",
            "application/vnd.company.companyforcreation+json")]
        [Consumes("application/json",
            "application/vnd.company.companyforcreation+json")]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyAddDto company)
        {
            //这段代码现在可以去掉,最早版本Asp.Net Core不行
            //if (company == null)
            //{
            //    return BadRequest();
            //}

            var entity = _mapper.Map<Company>(company);
            _companyRepository.AddCompany(entity);
            var result = await _companyRepository.SaveAsync();
            var returnDto = _mapper.Map<CompanyDto>(entity);

            var links = CreateLinksForCompany(returnDto.Id, null);
            var linkedDict = returnDto.ShapedData(null)
                             as IDictionary<string, object>;
            linkedDict.Add("links", links);
            return CreatedAtRoute(nameof(GetCompany), new { companyId = linkedDict["Id"] }, linkedDict);
        }




        [HttpOptions]
        public IActionResult GetCompaniesOptions()
        {
            Response.Headers.Add("Allow", "GET,POST,OPTIONS");
            return Ok();
        }

        [HttpDelete("{companyId}", Name = nameof(DeleteCompany))]
        public async Task<IActionResult> DeleteCompany(Guid companyId)
        {
            var companyEntity = await _companyRepository.GetCompanyAsync(companyId);
            if (companyEntity == null)
            {
                return NotFound();
            }
            await _companyRepository.GetEmployeesAsync(companyId, null);
            _companyRepository.DeleteCompany(companyEntity);
            await _companyRepository.SaveAsync();
            return NoContent();
        }


        string CreateCompaniesResourceUri(CompanyDtoParameters parameters, ResourceUriType type)
        {
            switch (type)
            {
                //其他过滤条件都带上
                case ResourceUriType.PreviousPage:
                    return Url.Link(nameof(GetCompanies), new
                    {
                        fields = parameters.Fields,
                        pageNumber = parameters.PageNumber - 1,
                        pageSize = parameters.PageSize,
                        companyName = parameters.CompanyName,
                        searchTerm = parameters.SearchTerm,
                        orderBy = parameters.OrderBy
                    });
                case ResourceUriType.NextPage:
                    return Url.Link(nameof(GetCompanies), new
                    {
                        fields = parameters.Fields,
                        pageNumber = parameters.PageNumber + 1,
                        pageSize = parameters.PageSize,
                        companyName = parameters.CompanyName,
                        searchTerm = parameters.SearchTerm,
                        orderBy = parameters.OrderBy
                    });
                case ResourceUriType.CurrentPage:
                default:
                    return Url.Link(nameof(GetCompanies), new
                    {
                        fields = parameters.Fields,
                        pageNumber = parameters.PageNumber,
                        pageSize = parameters.PageSize,
                        companyName = parameters.CompanyName,
                        searchTerm = parameters.SearchTerm,
                        orderBy = parameters.OrderBy
                    });
            }
        }

        private IEnumerable<LinkDto> CreateLinksForCompany(Guid companyId, string fields)
        {
            var links = new List<LinkDto>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                links.Add(new LinkDto(
                    Url.Link(nameof(GetCompany), new { companyId }),
                    rel: "self",
                    method: "GET"));
            }
            else
            {
                links.Add(new LinkDto(
                    Url.Link(nameof(GetCompany), new { companyId, fields }),
                    rel: "self",
                    method: "GET"));
            }

            links.Add(new LinkDto(
                    Url.Link(nameof(DeleteCompany), new { companyId }),
                    rel: "delete_company",
                    method: "DELETE"));
            links.Add(new LinkDto(
                    Url.Link(nameof(EmployeesController.CreateEmployeeForCompany), new { companyId }),
                    rel: "create_employee_for_company",
                    method: "POST"));
            links.Add(new LinkDto(
                    Url.Link(nameof(EmployeesController.GetEmployeesForCompany), new { companyId }),
                    rel: "employees",
                    method: "GET"));
            return links;
        }

        private IEnumerable<LinkDto> CreateLinksForCompany(
            CompanyDtoParameters parameters,
            bool hasPrevious = false,
            bool hasNext = false)
        {
            var links = new List<LinkDto>();

            links.Add(new LinkDto(
                CreateCompaniesResourceUri(parameters, ResourceUriType.CurrentPage),
                "self",
                "GET"
                ));

            if (hasPrevious)
            {
                links.Add(new LinkDto(
                         CreateCompaniesResourceUri(parameters, ResourceUriType.PreviousPage),
                         "previous_page",
                         "GET"
                         ));
            }
            if (hasNext)
            {
                links.Add(new LinkDto(
                         CreateCompaniesResourceUri(parameters, ResourceUriType.NextPage),
                         "next_page",
                         "GET"
                         ));
            }
            return links;
        }
    }
}
