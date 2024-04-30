using Claims_Api_Test.Models;
using Claims_Api_Test.Seeding;
using Microsoft.AspNetCore.Mvc;

namespace Claims_Api_Test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClaimsController : ControllerBase
    {
        private readonly ClaimsContext? _context;

        public List<Company> companies = [];
        public List<ClaimType> claimTypes = [];
        public List<Claim> claims = [];

        public ClaimsController(ClaimsContext context) 
        {
            _context = context;
        }

        public ClaimsController(List<Company> companies, List<ClaimType> claimTypes, List<Claim> claims)
        {
            this.companies = companies;
            this.claimTypes = claimTypes;
            this.claims = claims;
        }

        [HttpGet(Name = "GetCompany")]
        public async Task<IActionResult> GetCompanyAsync(int companyId)
        {
            return await Task.FromResult(GetCompany(companyId));
        }

        private IActionResult GetCompany(int id)
        {
            var company = companies.FirstOrDefault(c => c.Id == id);
            if (company == null)
            {
                return NotFound();
            }
            return Ok(company);
        }
    }
}
