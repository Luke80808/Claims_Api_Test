using Claims_Api_Test.Models;

namespace Claims_Api_Test.Seeding;

public class DbInitializer
{
    public static void Initialize(ClaimsContext context)
    {
        context.Database.EnsureCreated();

        if (context.Companies.Any())
        {
            return;
        }

        var companies = new Company[]
        {
            new() {
                Id = 1,
                Name = "Company1",
                Address1 = "123 Fake Street",
                Postcode = "AB1 2CD",
                Country = "United Kingdom",
                Active = true,
                InsuranceEndDate = DateTime.Parse("2024-05-31")
            },
            new() {
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
        foreach (Company c in companies)
        {
            context.Companies.Add(c);
        }
        context.SaveChanges();

        var claimTypes = new ClaimType[]
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
        foreach (ClaimType ct in claimTypes)
        {
            context.ClaimTypes.Add(ct);
        }
        context.SaveChanges();

        var claims = new Claim[]
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
        foreach (Claim c in claims)
        {
            context.Claims.Add(c);
        }
        context.SaveChanges();
    }
}
