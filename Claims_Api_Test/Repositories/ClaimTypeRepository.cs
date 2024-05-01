using Claims_Api.Interfaces;
using Claims_Api.Models;

namespace Claims_Api.Repositories;

public class ClaimTypeRepository : IRepositoryBase<ClaimType>
{
    public readonly List<ClaimType> _claimTypes = [];

    public void Add(ClaimType claimType)
    {
        _claimTypes.Add(claimType);
    }

    public ClaimType Get(string id)
    {
        return _claimTypes.FirstOrDefault(x => x.Id.ToString() == id) ?? throw new NullReferenceException("Claim type not found");
    }
}
