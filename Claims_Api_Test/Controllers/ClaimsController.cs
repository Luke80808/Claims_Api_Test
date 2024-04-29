using Claims_Api_Test.Models;
using Microsoft.AspNetCore.Mvc;

namespace Claims_Api_Test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClaimsController : ControllerBase
    {
        private readonly ILogger<ClaimsController> _logger;

        public ClaimsController(ILogger<ClaimsController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetCompany")]
        public async Task<Company> GetCompany()
        {
            throw new NotImplementedException();
        }
    }
}
