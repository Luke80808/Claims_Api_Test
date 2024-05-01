using Claims_Api.Interfaces;
using Claims_Api.Models;

namespace Claims_Api.Repositories;

public class CompanyRepository : IRepositoryBase<Company>
{
    public readonly List<Company> _companies = [];

    public void Add(Company company)
    {
        _companies.Add(company);
    }

    public Company? Get(string id)
    {
        return _companies.FirstOrDefault(x => x.Id.ToString() == id) ?? null;
    }
}
