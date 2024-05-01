using Claims_Api.Interfaces;
using Claims_Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Claims_Api.Controllers
{
    public class CompanyResponse
    {
        public required Company Company { get; set; }
        public bool HasActivePolicy { get; set; }
    }

    [ApiController]
    [Route("api/[controller]/companies")]
    public class CompanyController : ControllerBase
    {
        private readonly IRepositoryBase<Company> _companies;
        private readonly IRepositoryBase<ClaimType> _claimTypes;
        private readonly IRepositoryBase<Claim> _claims;

        public CompanyController(IRepositoryBase<Company> companies, IRepositoryBase<ClaimType> claimTypes, IRepositoryBase<Claim> claims)
        {
            _companies = companies;
            _claimTypes = claimTypes;
            _claims = claims;
        }

        [HttpGet("{id}", Name = "GetCompany")]
        public async Task<IActionResult> GetCompanyAsync(int id)
        {
            return await Task.FromResult(GetCompany(id));
        }

        private IActionResult GetCompany(int id)
        {
            var company = _companies.Get(id.ToString());
            if (company == null)
            {
                return NotFound();
            }
            var hasActivePolicy = CheckCompanyHasActivePolicy(company.InsuranceEndDate);
            return Ok(new CompanyResponse { Company = company, HasActivePolicy = hasActivePolicy });
        }

        private static bool CheckCompanyHasActivePolicy(DateTime policyEndDate)
        {
            if (policyEndDate < DateTime.UtcNow)
            {
                return false;
            }
            return true;
        }
    }
}
