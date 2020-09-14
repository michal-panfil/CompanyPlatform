using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyDomain;
using CompanyWebApi.DAL;
using CompanyWebApi.Helpers;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyWebApi.Controllers
{
   

    [ApiController]
    [Route("[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly IRepository Db;
        public CompanyController(IRepository repository)
        {
            Db  = repository;
            

        }

        [HttpPost]
        [Route("[action]")]
        [Authorize]
        public IActionResult Create(Company company)
        {
            string errorString;
            var isValid = Validator.ValdiateCompany(company, out errorString);

            if (!isValid)
            {
                return ValidationProblem(errorString);
            }

            var companyId = Db.AddCompanyAndEmpoyees(company);
            return Ok(companyId);
        }


        [HttpPost]
        [Route("[action]")]
        [AllowAnonymous]
        public  IActionResult Search(SearchDefiniton search)
        {
            var result = Db.FindCompany(search);

            return Ok(result);
        }

        [HttpPut]
        [Route("[action]/{id}")]
        [Authorize]
        public  IActionResult Update(long id, Company company)
        {
            string errorString;
            var isValid = Validator.ValdiateCompany(company, out errorString);

            if (!isValid){
                return ValidationProblem(errorString);
            }
            
            company.Id = id;
            var result = Db.UpdateCompany(company);

            if (!result) return NotFound($"Company with id : {id} does not exist");
            return Ok();
        }

        [HttpDelete]
        [Route("[action]/{id}")]
        [Authorize]
        public IActionResult Delete(long id)
        {
            var result = Db.DeleteCompany(id);
            
            if (!result) return NotFound($"Company with id : {id} does not exist");
            return Ok($"Company with id : {id} was removed");
        }

    }
}
