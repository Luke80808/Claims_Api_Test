namespace Claims_Api.Services;

public class ClaimService
{
    public static double GetClaimAgeInDays(DateTime claimDate)
    {
        return (DateTime.Today.Date - claimDate.Date).Days;
    }
}
