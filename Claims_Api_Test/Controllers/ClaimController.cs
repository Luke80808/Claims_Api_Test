using Claims_Api.Models;
using Claims_Api.Repositories;
using Claims_Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Claims_Api.Controllers
{
    public class ClaimResponse
    {
        public required Claim Claim { get; set; }
        public double ClaimAgeInDays { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class ClaimController : ControllerBase
    {
        private readonly ClaimRepository _claims;

        public ClaimController(ClaimRepository claims)
        {
            _claims = claims;
        }

        [HttpGet("company-claims", Name = "GetCompanyClaims")]
        public async Task<IActionResult> GetClaimsForCompanyAsync(int companyId)
        {
            return await Task.FromResult(GetClaimsForCompany(companyId));
        }

        [HttpGet("claim-details", Name = "GetClaimDetails")]
        public async Task<IActionResult> GetClaimDetailsAsync(string claimUCR)
        {
            return await Task.FromResult(GetClaimDetails(claimUCR));
        }

        [HttpPut("update-claim", Name = "UpdateClaim")]
        public async Task<IActionResult> UpdateClaimAsync(string claimUCR, Claim updatedClaim)
        {
            return await Task.FromResult(UpdateClaim(claimUCR, updatedClaim));
        }

        private IActionResult GetClaimsForCompany(int companyId)
        {
            var claims = _claims.GetCompanyClaims(companyId.ToString());
            if (claims == null)
            {
                return NotFound();
            }
            if (claims.Count == 0)
            {
                return Ok("No claims found for the given company.");
            }
            return Ok(claims);
        }

        private IActionResult GetClaimDetails(string claimUCR)
        {
            var claim = _claims.Get(claimUCR);
            if (claim == null)
            {
                return NotFound($"Claim with UCR {claimUCR} not found");
            }
            var claimAge = ClaimService.GetClaimAgeInDays(claim.ClaimDate);
            return Ok(new ClaimResponse { Claim = claim, ClaimAgeInDays = claimAge });;
        }

        private IActionResult UpdateClaim(string claimUCR, Claim updatedClaim)
        {
            if (claimUCR != updatedClaim.UCR)
            {
                return BadRequest("Claim UCR mismatch");
            }

            var claimToUpdate = _claims.Get(claimUCR);
            if (claimToUpdate == null)
            {
                return NotFound($"Claim with UCR {claimUCR} not found");
            }

            var updater = _claims.Update(updatedClaim);

            if (updater == null)
            {
                return Problem();
            }

            return Ok(updater);
        }
    }
}
