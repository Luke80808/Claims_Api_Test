using System.ComponentModel.DataAnnotations;

namespace Claims_Api_Test.Models;

public class Claim
{
	[MaxLength(20)]
	public required string UCR {  get; set; }

	public int CompanyId { get; set; }

	public DateTime ClaimDate { get; set; }

	public DateTime LossDate { get; set; }

	[MaxLength(100)]
	public required string AssuredName { get; set; }

	public decimal IncurredLoss { get; set; }

	public bool Closed { get; set; }
}
