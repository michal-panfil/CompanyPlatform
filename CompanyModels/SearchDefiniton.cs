using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyDomain
{
    public class SearchDefiniton
    {
        public string Keyword { get; set; }
        public DateTime? EmployeeDateOfBirthFrom { get; set; }
        public DateTime? EmployeeDateOfBirthTo { get; set; }
        public List<string> EmployeeJobTitle { get; set; }

    }
}
