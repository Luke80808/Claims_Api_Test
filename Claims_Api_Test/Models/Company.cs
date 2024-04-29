using System.ComponentModel.DataAnnotations;

namespace Claims_Api_Test.Models;

public class Company
{
    public int Id { get; set; }

    [MaxLength(200)]
    public required string Name { get; set; }

    [MaxLength(100)]
    public required string Address1 { get; set; }

    [MaxLength(100)]
    public string? Address2 { get; set; }

    [MaxLength(100)]
    public string? Address3 { get; set; }

    [MaxLength(20)]
    public required string Postcode { get; set; }

    [MaxLength(50)]
    public required string Country { get; set; }

    public bool Active { get; set; }

    public DateTime InsuranceEndDate { get; set; }
}
