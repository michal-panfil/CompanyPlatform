using CompanyDomain;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyWebApi.Helpers
{
    public class Validator
    {

        public static bool  ValdiateCompany(Company company, out string errorString)
        {
            var validators = new List<ValidationResult>();
            validators.Add(new CompanyValidator().Validate(company));
            if(company.Employees.Count > 0)  company.Employees.ForEach(e => validators.Add(new EmployeeValidator().Validate(e)));

            var errors = validators.Where(v => v.IsValid == false).SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
            var isValid = validators.All(v => v.IsValid);
            
            errorString = (String.Join('\n', errors));
            return isValid;
        }
    }
}
