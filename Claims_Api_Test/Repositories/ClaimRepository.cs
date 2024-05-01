using Claims_Api_Test.Interfaces;
using Claims_Api_Test.Models;

namespace Claims_Api_Test.Repositories;

public class ClaimRepository : IRepositoryBase<Claim>
{
    private readonly List<Claim> _claims = [];

    public void Add(Claim claim)
    {
        _claims.Add(claim);
    }

    public Claim Get(string ucr)
    {
        return _claims.FirstOrDefault(x => x.UCR == ucr) ?? throw new NullReferenceException("Claim not found");
    }
}
