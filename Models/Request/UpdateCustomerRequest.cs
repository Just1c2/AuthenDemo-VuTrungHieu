namespace AuthenDemo.Models.Request
{
    public class UpdateCustomerRequest
    {
        public string Name { get; set; } = null!;

        public int? Age { get; set; }

        public string? Gender { get; set; }

        public string? Address { get; set; }

        public decimal? Debit { get; set; }

        public string? Description { get; set; }
    }
}
