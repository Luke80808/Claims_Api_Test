using Claims_Api.Interfaces;
using Claims_Api.Models;
using Claims_Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Claims_Api.Controllers
{
    public class ClaimResponse
    {
        public required Claim Claim { get; set; }
        public double ClaimAgeInDays { get; set; }
    }

    [ApiController]
    [Route("api/[controller]/claims")]
    public class ClaimController : ControllerBase
    {
        private readonly CompanyRepository _companies;
        private readonly IRepositoryBase<ClaimType> _claimTypes;
        private readonly ClaimRepository _claims;

        public ClaimController(CompanyRepository companies, IRepositoryBase<ClaimType> claimTypes, ClaimRepository claims)
        {
            _companies = companies;
            _claimTypes = claimTypes;
            _claims = claims;
        }

        [HttpGet("/{companyId}", Name = "GetCompanyClaims")]
        public async Task<IActionResult> GetClaimsForCompanyAsync(int companyId)
        {
            return await Task.FromResult(GetClaimsForCompany(companyId));
        }

        [HttpGet("{claimUCR}", Name = "GetClaimDetails")]
        public async Task<IActionResult> GetClaimDetailsAsync(string claimUCR)
        {
            return await Task.FromResult(GetClaimDetails(claimUCR));
        }

        private IActionResult GetClaimsForCompany(int companyId)
        {
            var claims = _claims.GetCompanyClaims(companyId.ToString());
            if (claims == null)
            {
                return NotFound();
            }
            return Ok(claims);
        }

        private IActionResult GetClaimDetails(string claimUCR)
        {
            var claim = _claims.Get(claimUCR);
            if (claim == null)
            {
                return NotFound();
            }
            var claimAge = GetClaimAgeInDays(claim.ClaimDate);
            return Ok(new ClaimResponse { Claim = claim, ClaimAgeInDays = claimAge });;
        }

        private static double GetClaimAgeInDays(DateTime claimDate)
        {
            return (DateTime.Today - claimDate).TotalDays;
        }
    }
}
