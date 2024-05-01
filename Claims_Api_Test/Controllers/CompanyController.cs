using Claims_Api.Models;
using Claims_Api.Repositories;
using Claims_Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Claims_Api.Controllers
{
    public class CompanyResponse
    {
        public required Company Company { get; set; }
        public bool HasActivePolicy { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly CompanyRepository _companies;

        public CompanyController(CompanyRepository companies)
        {
            _companies = companies;
        }

        [HttpGet("company-details", Name = "GetCompany")]
        public async Task<IActionResult> GetCompanyAsync(int id)
        {
            if (_companies._companies.Count == 0)
            {
                CreateCompanyData();
            }

            return await Task.FromResult(GetCompany(id));
        }

        private IActionResult GetCompany(int id)
        {
            var company = _companies.Get(id.ToString());
            if (company == null)
            {
                return NotFound($"Company with Id {id} not found");
            }
            var hasActivePolicy = CompanyService.CheckCompanyHasActivePolicy(company.InsuranceEndDate);
            return Ok(new CompanyResponse { Company = company, HasActivePolicy = hasActivePolicy });
        }

        private void CreateCompanyData()
        {
            var company1 = new Company
            {
                Id = 1,
                Name = "Company1",
                Address1 = "123 Fake Street",
                Postcode = "AB1 2CD",
                Country = "United Kingdom",
                Active = true,
                InsuranceEndDate = DateTime.Parse("2024-05-31")
            };
            var company2 = new Company
            {
                Id = 2,
                Name = "Company2",
                Address1 = "456 False Lane",
                Address2 = "The Lane",
                Postcode = "EF3 4GH",
                Country = "United Kingdom",
                Active = false,
                InsuranceEndDate = DateTime.Parse("2023-12-31")
            };

            _companies.Add(company1);
            _companies.Add(company2);
        }
    }
}
