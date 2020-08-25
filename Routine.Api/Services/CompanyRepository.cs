using Microsoft.EntityFrameworkCore;
using Routine.Api.Data;
using Routine.Api.Entities;
using Routine.Api.Helpers;
using Routine.Api.Models;
using Routine.Api.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace Routine.Api.Services
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly RoutineDbContext _context;
        private readonly IPropertyMappingService _propertyMappingService;

        public CompanyRepository(RoutineDbContext context, IPropertyMappingService propertyMappingService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _propertyMappingService = propertyMappingService ?? throw new ArgumentNullException(nameof(propertyMappingService));
        }

        public void AddCompany(Company company)
        {
            if (company == null)
            {
                throw new ArgumentNullException(nameof(company));
            }
            company.Id = System.Guid.NewGuid();
            if (company.Employees?.Count > 0)
            {
                foreach (var employee in company.Employees)
                {
                    employee.Id = System.Guid.NewGuid();
                }
            }
            _context.Companies.Add(company);
        }

        public void AddEmployee(Guid companyId, Employee employee)
        {
            if (_context.Companies.FirstOrDefault(a => a.Id == companyId) != null &&
                employee != null)
            {
                employee.CompanyId = companyId;
                _context.Employees.Add(employee);
            }
        }

        public async Task<bool> CompanyExistAsync(Guid companyId)
        {
            return await _context.Companies
                .AnyAsync(a => a.Id == companyId);
        }

        public void DeleteEmployee(Employee employee)
        {
            if (employee != null &&
                employee.Id != Guid.Empty &&
                _context.Employees.FirstOrDefault(a => a.Id == employee.Id) != null)
            {
                _context.Employees.Remove(employee);
            }
        }

        public async Task<PagedList<Company>> GetCompaniesAsync(CompanyDtoParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }
            var queryExpression = _context.Companies as IQueryable<Company>;
            if (!string.IsNullOrWhiteSpace(parameters.CompanyName))
            {
                parameters.CompanyName = parameters.CompanyName.Trim();
                queryExpression = queryExpression.Where(a => a.Name == parameters.CompanyName);
            }
            if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
            {
                parameters.SearchTerm = parameters.SearchTerm.Trim();
                queryExpression = queryExpression.Where(a => a.Name.Contains(parameters.SearchTerm) ||
                a.Intruction.Contains(parameters.SearchTerm));
            }
            var mappingDictionary = _propertyMappingService.GetPropertyMapping<CompanyDto, Company>();
            queryExpression = queryExpression.ApplySort(parameters.OrderBy, mappingDictionary);
            return await PagedList<Company>.CreateAsync(queryExpression, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<IEnumerable<Company>> GetCompaniesAsync(IEnumerable<Guid> companyIds)
        {
            if (companyIds?.Count() > 0)
            {
                return await _context.Companies
                    .Where(a => companyIds.Contains(a.Id))
                    .OrderBy(a => a.Name)
                    .ToListAsync();
            }
            throw new ArgumentNullException(nameof(companyIds));
        }

        public async Task<Company> GetCompanyAsync(Guid companyId)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }

            return await _context.Companies
                .FirstOrDefaultAsync(a => a.Id == companyId);
        }

        public async Task<Employee> GetEmployeeAsync(Guid companyId, Guid employeeId)
        {

            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }

            if (employeeId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(employeeId));
            }

            return await _context.Employees
                .FirstOrDefaultAsync(a => a.CompanyId == companyId &&
                a.Id == employeeId);
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId, EmployeeDtoParameters parameters)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }
            //先转化成IQueryable类型,方便后续搜索或者过滤.
            var items = _context.Employees.Where(a => a.CompanyId == companyId) as IQueryable<Employee>;
            if (parameters != null)
            {
                if (!string.IsNullOrEmpty(parameters.Gender))
                {
                    parameters.Gender = parameters.Gender.Trim();
                    var gender = Enum.Parse<Gender>(parameters.Gender);
                    items = items.Where(a => a.Gender == gender);
                }
                if (!string.IsNullOrEmpty(parameters.Q))
                {
                    parameters.Q = parameters.Q.Trim();
                    items = items.Where(a => a.EmployeeNo.Contains(parameters.Q) ||
                      a.FirstName.Contains(parameters.Q) ||
                      a.LastName.Contains(parameters.Q));
                }
                var mappingDictionary = _propertyMappingService.GetPropertyMapping<EmployeeDto, Employee>();
                items = items.ApplySort(parameters.OrderBy, mappingDictionary);
            }
            return await items.ToListAsync();
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void UpdateCompany(Company company)
        {
            //if (company!=null&&
            //    _context.Companies.FirstOrDefault(a => a.Id == company.Id) != null)
            //{
            //    _context.Companies.Update(company);
            //}
        }

        public void UpdateEmployee(Employee employee)
        {
            // if (employee != null &&
            //_context.Employees.FirstOrDefault(a => a.Id == employee.Id) != null)
            // {
            //     _context.Employees.Update(employee);
            // }
        }


        public void AddCompanies(IEnumerable<Company> companies)
        {
            if (companies?.Count() > 0)
            {
                foreach (var company in companies)
                {
                    company.Id = System.Guid.NewGuid();
                }

                _context.Companies.AddRange(companies);
            }
        }

        public void DeleteCompany(Company company)
        {
            if (company != null &&
                company.Id != Guid.Empty &&
                _context.Companies.FirstOrDefault(a => a.Id == company.Id) != null)
            {
                _context.Companies.Remove(company);
            }
        }
    }
}
