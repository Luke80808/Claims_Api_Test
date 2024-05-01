using Claims_Api_Test.Interfaces;
using Claims_Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Claims_Api_Test.Controllers
{
    [ApiController]
    [Route("api/[controller]/claims")]
    public class ClaimController : ControllerBase
    {
        private readonly IRepositoryBase<Company> _companies;
        private readonly IRepositoryBase<ClaimType> _claimTypes;
        private readonly IRepositoryBase<Claim> _claims;

        public ClaimController(IRepositoryBase<Company> companies, IRepositoryBase<ClaimType> claimTypes, IRepositoryBase<Claim> claims)
        {
            _companies = companies;
            _claimTypes = claimTypes;
            _claims = claims;
        }
    }
}
