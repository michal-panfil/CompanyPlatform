using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyDomain
{
    public class Company
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int EstablishmentYear { get; set; }
        public List<Employee> Employees { get; set; }
    }

    public class CompanyValidator : AbstractValidator<Company>
    {
        public CompanyValidator()
        {
            RuleFor(company => company.Name).NotEmpty();
            RuleFor(company => company.EstablishmentYear).GreaterThan(0);
        }
    }
}
