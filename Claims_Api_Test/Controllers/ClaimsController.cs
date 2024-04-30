using Claims_Api_Test.Models;
using Microsoft.AspNetCore.Mvc;

namespace Claims_Api_Test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClaimsController : ControllerBase
    {
        public List<Company> companies = [];
        public List<ClaimType> claimTypes = [];
        public List<Claim> claims = [];

        public ClaimsController() { }

        public ClaimsController(List<Company> companies, List<ClaimType> claimTypes, List<Claim> claims)
        {
            this.companies = companies;
            this.claimTypes = claimTypes;
            this.claims = claims;
        }

        [HttpGet(Name = "GetCompany")]
        public async Task<Company> GetCompany()
        {
            throw new NotImplementedException();
        }
    }
}
