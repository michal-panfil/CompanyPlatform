using CompanyDomain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyWebApi.DAL
{
    public interface IRepository
    {
        long AddCompanyAndEmpoyees(Company newCompany);
        List<Company> FindCompany(SearchDefiniton searchDefiniton);
        bool UpdateCompany(Company updatedCompany);
        bool DeleteCompany(long companyID);
    }
}
