using FluentValidation;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyDomain
{
    public class Employee
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
     [JsonConverter(typeof(StringEnumConverter))]
        [Column(TypeName = "TEXT")]
        public JobTitles JobTitle { get; set; }

    }

    [JsonConverter(typeof(StringEnumConverter))]

    public enum JobTitles
    {
        Administrator,
        Developer,
        Architect,
        Manager
    }

    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(employee => employee.FirstName).NotEmpty();
            RuleFor(employee => employee.LastName).NotEmpty();
            RuleFor(employee => employee.DateOfBirth).NotNull();
            RuleFor(employee => employee.DateOfBirth).NotNull();

        }
    }
}
