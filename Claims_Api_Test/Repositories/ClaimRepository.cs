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

    public Claim? Get(string ucr)
    {
        return _claims.FirstOrDefault(x => x.UCR == ucr) ?? null;
    }

    public List<Claim> GetCompanyClaims(string companyId)
    {
        return _claims.Where(x => x.CompanyId.ToString() == companyId).ToList();
    }

    public Claim? Update(Claim claim)
    {
        var result = _claims.FirstOrDefault(x => x.UCR == claim.UCR) ?? throw new NullReferenceException("Claim not found");

        if (result != null)
        {
            result.UCR = claim.UCR;
            result.CompanyId = claim.CompanyId;
            result.ClaimDate = claim.ClaimDate;
            result.LossDate = claim.LossDate;
            result.AssuredName = claim.AssuredName;
            result.IncurredLoss = claim.IncurredLoss;
            result.Closed = claim.Closed;

            return result;
        }

        return null;
    }
}
