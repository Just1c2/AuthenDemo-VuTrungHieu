using System;
using System.Collections.Generic;

namespace AuthenDemo.Models.Entities;

public partial class Order
{
    public long ProductId { get; set; }

    public long CustomerId { get; set; }

    public int Amount { get; set; }

    public decimal Price { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public long Id { get; set; }
}
