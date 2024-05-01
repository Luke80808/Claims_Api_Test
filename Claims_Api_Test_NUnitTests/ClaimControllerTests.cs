using Claims_Api.Controllers;
using Claims_Api.Models;
using Claims_Api.Repositories;
using Claims_Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Claims_Api_Tests
{
    public class ClaimControllerTests
    {
        private CompanyRepository _companyRepository;
        private ClaimTypeRepository _claimTypeRepository;
        private ClaimRepository _claimRepository;

        [SetUp]
        public void Setup()
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

            _companyRepository = new CompanyRepository();
            _companyRepository.Add(company1);
            _companyRepository.Add(company2);


            var claimType1 = new ClaimType
            {
                Id = 1,
                Name = "General"
            };
            var claimType2 = new ClaimType
            {
                Id = 2,
                Name = "Theft"
            };
            var claimType3 = new ClaimType
            {
                Id = 3,
                Name = "Fire"
            };
            var claimType4 = new ClaimType
            {
                Id = 4,
                Name = "Natural disaster"
            };

            _claimTypeRepository = new ClaimTypeRepository();
            _claimTypeRepository.Add(claimType1);
            _claimTypeRepository.Add(claimType2);
            _claimTypeRepository.Add(claimType3);
            _claimTypeRepository.Add(claimType4);


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

            _claimRepository = new ClaimRepository();
            _claimRepository.Add(claim1);
            _claimRepository.Add(claim2);
            _claimRepository.Add(claim3);
        }

        [Test]
        public void GetClaimsForCompany_ReturnsClaims()
        {
            //arrange

            //act
            var controller = new ClaimController(_claimRepository, _companyRepository);

            var result = controller.GetClaimsForCompanyAsync(1).Result;
            var okResult = result as OkObjectResult;
            var listResult = okResult?.Value as List<Claim>;

            //assert
            Assert.That(listResult, Is.EqualTo(_claimRepository._claims.Where(x => x.CompanyId == 1)));
        }

        [Test]
        public void GetClaimsForCompany_ReturnsEmptyList_WhenNoClaims()
        {
            //arrange

            //act
            var controller = new ClaimController(_claimRepository, _companyRepository);

            var result = controller.GetClaimsForCompanyAsync(100).Result;
            var okResult = result as OkObjectResult;
            var listResult = okResult?.Value as List<Claim>;

            //assert
            Assert.That(listResult, Is.EqualTo(null));
        }

        [Test]
        public void GetClaimDetails_RetrievesClaim()
        {
            //arrange

            //act
            var controller = new ClaimController(_claimRepository, _companyRepository);

            var result = controller.GetClaimDetailsAsync("Company1Claim001").Result;
            var okResult = result as OkObjectResult;
            var claimResponse = okResult?.Value as ClaimResponse;
            var actualCompany = claimResponse?.Claim;

            //assert
            Assert.That(actualCompany, Is.EqualTo(_claimRepository._claims.FirstOrDefault(x => x.UCR == "Company1Claim001")));
        }

        [Test]
        public void GetClaimDetails_GivesCorrectClaimAge()
        {
            //arrange

            //act
            var controller = new ClaimController(_claimRepository, _companyRepository);

            var result = controller.GetClaimDetailsAsync("Company1Claim001").Result;
            var okResult = result as OkObjectResult;
            var claimResponse = okResult?.Value as ClaimResponse;
            var claimAge = claimResponse?.ClaimAgeInDays;

            //assert
            Assert.That(claimAge, Is.EqualTo(ClaimService.GetClaimAgeInDays(DateTime.Parse("2024-02-06"))));
        }

        [Test]
        public void GetClaimDetails_GivesCorrectClaimAge_ForClaimMadeToday()
        {
            //arrange

            //act
            var controller = new ClaimController(_claimRepository, _companyRepository);

            var result = controller.GetClaimDetailsAsync("Company1Claim002").Result;
            var okResult = result as OkObjectResult;
            var claimResponse = okResult?.Value as ClaimResponse;
            var claimAge = claimResponse?.ClaimAgeInDays;

            //assert
            Assert.That(claimAge, Is.EqualTo(ClaimService.GetClaimAgeInDays(DateTime.Now)));
        }

        [Test]
        public void UpdateClaim_CorrectlyUpdatesClosed()
        {
            //arrange
            var updatedClaim = new Claim
            {
                UCR = "Company1Claim002",
                CompanyId = 1,
                ClaimDate = DateTime.Now,
                LossDate = DateTime.Parse("2024-02-28"),
                AssuredName = "Company1",
                IncurredLoss = 500.00m,
                Closed = true
            };

            //act
            var controller = new ClaimController(_claimRepository, _companyRepository);

            var result = controller.UpdateClaimAsync("Company1Claim002", updatedClaim).Result;
            var okResult = result as OkObjectResult;
            var claim = okResult?.Value as Claim;

            //assert
            Assert.That(claim?.Closed, Is.EqualTo(true));

        }

        [Test]
        public void UpdateClaim_Returns404_WhenNoClaimExists()
        {
            //arrange
            var updatedClaim = new Claim
            {
                UCR = "Company1Claim1000",
                CompanyId = 1,
                ClaimDate = DateTime.Now,
                LossDate = DateTime.Parse("2024-02-28"),
                AssuredName = "Company1",
                IncurredLoss = 500.00m,
                Closed = true
            };

            //act
            var controller = new ClaimController(_claimRepository, _companyRepository);

            var result = controller.UpdateClaimAsync("Company1Claim1000", updatedClaim).Result;
            var notFoundResult = result as NotFoundObjectResult;

            //assert
            Assert.That(notFoundResult?.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public void UpdateClaim_ReturnsBadRequest_WhenUCRsDoNotMatch()
        {
            //arrange
            var updatedClaim = new Claim
            {
                UCR = "Company1Claim1000",
                CompanyId = 1,
                ClaimDate = DateTime.Now,
                LossDate = DateTime.Parse("2024-02-28"),
                AssuredName = "Company1",
                IncurredLoss = 500.00m,
                Closed = true
            };

            //act
            var controller = new ClaimController(_claimRepository, _companyRepository);

            var result = controller.UpdateClaimAsync("Company1Claim002", updatedClaim).Result;
            var badRequestResult = result as BadRequestObjectResult;

            //assert
            Assert.That(badRequestResult?.StatusCode, Is.EqualTo(400));
        }
    }
}
