using System;
using System.Collections.Generic;

namespace AuthenDemo.Models.Entities;

public partial class Customer
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public int? Age { get; set; }

    public string? Gender { get; set; }

    public string? Address { get; set; }

    public string? Salt { get; set; }

    public string Username { get; set; } = null!;

    public string? Password { get; set; }

    public int Status { get; set; }

    public decimal? Debit { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedDate { get; set; }

    public string? UpdatedBy { get; set; }
}
