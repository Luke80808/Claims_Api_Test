using Claims_Api_Test.Interfaces;
using Claims_Api_Test.Models;

namespace Claims_Api_Test.Repositories;

public class CompanyRepository : IRepositoryBase<Company>
{
    public readonly List<Company> _companies = [];

    public void Add(Company company)
    {
        _companies.Add(company);
    }

    public Company Get(string id)
    {
        return _companies.FirstOrDefault(x => x.Id.ToString() == id) ?? throw new NullReferenceException("Company not found");
    }
}
