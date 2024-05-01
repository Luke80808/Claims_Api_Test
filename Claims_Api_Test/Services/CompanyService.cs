namespace Claims_Api.Services;

public class CompanyService
{
    public static bool CheckCompanyHasActivePolicy(DateTime policyEndDate)
    {
        if (policyEndDate < DateTime.UtcNow)
        {
            return false;
        }
        return true;
    }
}
