using System.ComponentModel.DataAnnotations;

namespace Claims_Api.Models;

public class ClaimType
{
    public int Id { get; set; }

    [MaxLength(20)]
    public required string Name { get; set; }
}
