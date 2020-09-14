using CompanyDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace CompanyWebApi.DAL
{
    public class BaseRepo : IRepository
    {
        public CompanyContext Ctx;

        public BaseRepo(CompanyContext ctx)
        {
            Ctx = ctx;
        }
        public long AddCompanyAndEmpoyees(Company newCompany)
        {
            var company = Ctx.Companies.Add(newCompany);
            Ctx.SaveChanges();
            return company.Entity.Id;
        }

        public bool DeleteCompany(long companyID)
        {
            var companyToDelete = Ctx.Companies.Include(c=>c.Employees).FirstOrDefault(c => c.Id == companyID);

            if (companyToDelete != null)
            {
                if (companyToDelete?.Employees?.Count > 0) Ctx.Employees.RemoveRange(companyToDelete.Employees);

                Ctx.Companies.Remove(companyToDelete);

                Ctx.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Company> FindCompany(SearchDefiniton search)
        {
            var result = new List<Company>();
            var byKeyword = search.Keyword != null ? Ctx.Companies.Include(c=> c.Employees).Where(c => c.Name.Contains(search.Keyword)
                                                       || c.Employees.Any(e => e.FirstName.Contains(search.Keyword) || e.LastName.Contains(search.Keyword))).ToList()
                                                        : null;
            if(byKeyword != null) result.AddRange(byKeyword);
            
            if(search.EmployeeDateOfBirthFrom != null && search.EmployeeDateOfBirthTo != null)
            {
                var byBirthRange = Ctx.Companies.Include(c => c.Employees).Where(c => c.Employees.Any(e => e.DateOfBirth > search.EmployeeDateOfBirthFrom && e.DateOfBirth < search.EmployeeDateOfBirthTo));
                if (byBirthRange != null) result.AddRange(byBirthRange);          
            }
            else if(search.EmployeeDateOfBirthFrom != null || search.EmployeeDateOfBirthTo != null)
            {

                var byBirthDate = Ctx.Companies.Include(c => c.Employees).Where(c => c.Employees.Any(e => e.DateOfBirth > search.EmployeeDateOfBirthFrom || e.DateOfBirth < search.EmployeeDateOfBirthTo));
                if (byBirthDate != null) result.AddRange(byBirthDate);
            }
            var byJobTitle = search.EmployeeJobTitle != null ? Ctx.Companies.Include(c => c.Employees).Where(c => c.Employees.Any(e =>  search.EmployeeJobTitle.Contains(e.JobTitle.ToString()))) : null ;
            if (byJobTitle != null) result.AddRange(byJobTitle);

            return result.Distinct().ToList();
        }

        public bool UpdateCompany(Company updatedCompany)
        {
            var update = Ctx.Update(updatedCompany);
            if (update == null) return false;
                
            Ctx.SaveChanges();
            return true;
        }
    }
}
