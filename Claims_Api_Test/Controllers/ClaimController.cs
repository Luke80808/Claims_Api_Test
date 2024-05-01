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
        private readonly CompanyRepository _companies;

        public ClaimController(ClaimRepository claims, CompanyRepository companies)
        {
            _claims = claims;
            _companies = companies;
        }

        [HttpGet("company-claims", Name = "GetCompanyClaims")]
        public async Task<IActionResult> GetClaimsForCompanyAsync(int companyId)
        {
            if (_claims._claims.Count == 0)
            {
                CreateCompanyAndClaimData();
            }

            return await Task.FromResult(GetClaimsForCompany(companyId));
        }

        [HttpGet("claim-details", Name = "GetClaimDetails")]
        public async Task<IActionResult> GetClaimDetailsAsync(string claimUCR)
        {
            if (_claims._claims.Count == 0)
            {
                CreateCompanyAndClaimData();
            }

            return await Task.FromResult(GetClaimDetails(claimUCR));
        }

        [HttpPut("update-claim", Name = "UpdateClaim")]
        public async Task<IActionResult> UpdateClaimAsync(string claimUCR, Claim updatedClaim)
        {
            if (_claims._claims.Count == 0)
            {
                CreateCompanyAndClaimData();
            }

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

        private void CreateCompanyAndClaimData()
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


            var claim1 = new Claim
            {
                UCR = "Company1Claim001",
                CompanyId = 1,
                ClaimDate = DateTime.Parse("2024-02-06"),
                LossDate = DateTime.Parse("2024-02-02"),
                AssuredName = "Company1",
                IncurredLoss = 200.00m,
                Closed = true
            };
            var claim2 = new Claim
            {
                UCR = "Company1Claim002",
                CompanyId = 1,
                ClaimDate = DateTime.Now,
                LossDate = DateTime.Parse("2024-02-28"),
                AssuredName = "Company1",
                IncurredLoss = 500.00m,
                Closed = false
            };
            var claim3 = new Claim
            {
                UCR = "Company2Claim001",
                CompanyId = 2,
                ClaimDate = DateTime.Parse("2023-11-06"),
                LossDate = DateTime.Parse("2023-11-01"),
                AssuredName = "Company2",
                IncurredLoss = 100.00m,
                Closed = true
            };

            _claims.Add(claim1);
            _claims.Add(claim2);
            _claims.Add(claim3);
        }
    }
}
