using Claims_Api.Repositories;
using Claims_Api_Test.Controllers;
using Claims_Api_Test.Models;
using Claims_Api_Test.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Claims_Api_Test_NUnitTests
{
    public class Tests
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
                ClaimDate = DateTime.Parse("2024-03-11"),
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
        public void GetCompany_RetrievesCompany()
        {
            //arrange

            //act
            var controller = new ClaimsController(_companyRepository, _claimRepository);

            var result = controller.GetCompanyAsync(1).Result;
            var okResult = result as OkObjectResult;
            var companyResponse = okResult?.Value as CompanyResponse;
            var actualCompany = companyResponse?.Company;

            //assert
            Assert.That(actualCompany, Is.EqualTo(_companyRepository._companies.FirstOrDefault(x => x.Id == 1)));
        }

        [Test]
        public void GetCompany_ReturnsNullReferenceException_WhenNoMatch()
        {
            //arrange

            //act
            var controller = new ClaimsController(_companyRepository, _claimRepository);

            //assert
            Assert.ThrowsAsync<NullReferenceException>(() => controller.GetCompanyAsync(100));
        }
    }
}