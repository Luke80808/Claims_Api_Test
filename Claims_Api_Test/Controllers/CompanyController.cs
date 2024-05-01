using Claims_Api.Interfaces;
using Claims_Api.Models;
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
        private readonly IRepositoryBase<Company> _companies;

        public CompanyController(IRepositoryBase<Company> companies)
        {
            _companies = companies;
        }

        [HttpGet("company-details", Name = "GetCompany")]
        public async Task<IActionResult> GetCompanyAsync(int id)
        {
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
    }
}
