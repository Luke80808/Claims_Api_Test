using Claims_Api.Interfaces;
using Claims_Api.Models;

namespace Claims_Api.Repositories;

public class ClaimRepository : IRepositoryBase<Claim>
{
    public readonly List<Claim> _claims = [];

    public void Add(Claim claim)
    {
        _claims.Add(claim);
    }

    public Claim Get(string ucr)
    {
        return _claims.FirstOrDefault(x => x.UCR == ucr) ?? throw new NullReferenceException("Claim not found");
    }

    public List<Claim> GetCompanyClaims(string companyId)
    {
        return _claims.Where(x => x.CompanyId.ToString() == companyId).ToList();
    }
}
