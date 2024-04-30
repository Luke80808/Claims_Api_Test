using Claims_Api_Test.Controllers;
using Claims_Api_Test.Models;
using Microsoft.AspNetCore.Mvc;

namespace Claims_Api_Test_Tests
{
    public class ClaimsControllerTests
    {
        [Fact]
        public void GetCompany_RetrievesCompany()
        {
            var testCompanies = GetTestCompanies();
            var testClaimTypes = GetTestClaimTypes();
            var testClaims = GetTestClaims();
            var controller = new ClaimsController(testCompanies, testClaimTypes, testClaims);

            var result = controller.GetCompanyAsync(1).Result;
            var okResult = result as OkObjectResult;
            var actualCompany = okResult?.Value as Company;

            Assert.Equal(testCompanies.FirstOrDefault(x => x.Id == 1), actualCompany);
        }

        private static List<Company> GetTestCompanies()
        {
            return new List<Company>
            {
                new()
                {
                    Id = 1,
                    Name = "Company1",
                    Address1 = "123 Fake Street",
                    Postcode = "AB1 2CD",
                    Country = "United Kingdom",
                    Active = true,
                    InsuranceEndDate = DateTime.Parse("2024-05-31")
                },
                new()
                {
                    Id = 2,
                    Name = "Company2",
                    Address1 = "456 False Lane",
                    Address2 = "The Lane",
                    Postcode = "EF3 4GH",
                    Country = "United Kingdom",
                    Active = false,
                    InsuranceEndDate = DateTime.Parse("2023-12-31")
                },
            };
        }

        private static List<ClaimType> GetTestClaimTypes()
        {
            return new List<ClaimType>
            {
                new()
                {
                    Id = 1,
                    Name = "General"
                },
                new()
                {
                    Id = 2,
                    Name = "Theft"
                },
                new()
                {
                    Id = 3,
                    Name = "Fire"
                },
                new()
                {
                    Id = 4,
                    Name = "Natural disaster"
                },
            };
        }

        private static List<Claim> GetTestClaims()
        {
            return new List<Claim>
            {
                new()
                {
                    UCR = "Company1Claim001",
                    CompanyId = 1,
                    ClaimDate = DateTime.Parse("2024-02-06"),
                    LossDate = DateTime.Parse("2024-02-02"),
                    AssuredName = "Company1",
                    IncurredLoss = 200.00m,
                    Closed = true
                },
                new()
                {
                    UCR = "Company1Claim002",
                    CompanyId = 1,
                    ClaimDate = DateTime.Parse("2024-03-11"),
                    LossDate = DateTime.Parse("2024-02-28"),
                    AssuredName = "Company1",
                    IncurredLoss = 500.00m,
                    Closed = false
                },
                new()
                {
                    UCR = "Company2Claim001",
                    CompanyId = 2,
                    ClaimDate = DateTime.Parse("2023-11-06"),
                    LossDate = DateTime.Parse("2023-11-01"),
                    AssuredName = "Company2",
                    IncurredLoss = 100.00m,
                    Closed = true
                },
            };
        }
    }
}