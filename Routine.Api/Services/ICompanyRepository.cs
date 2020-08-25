using Routine.Api.Entities;
using Routine.Api.Helpers;
using Routine.Api.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routine.Api.Services
{
   public interface ICompanyRepository
    {
        Task<PagedList<Company>> GetCompaniesAsync(CompanyDtoParameters parameters);

        Task<Company> GetCompanyAsync(Guid companyId);

        Task<IEnumerable<Company>> GetCompaniesAsync(IEnumerable<Guid> companyIds);

        void AddCompany(Company company);

        void UpdateCompany(Company company);

        Task<bool> CompanyExistAsync(Guid companyId);

        Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId, EmployeeDtoParameters parameters);

        Task<Employee> GetEmployeeAsync(Guid companyId,Guid employeeId);

        void AddEmployee(Guid companyId, Employee employee);

        void UpdateEmployee(Employee employee);

        void DeleteEmployee(Employee employee);

        Task<bool> SaveAsync();

        void AddCompanies(IEnumerable<Company> companies);

        void DeleteCompany(Company company);
    }
}
