namespace AuthenDemo.Models.Request
{
    public class ProductRequest
    {
        public string Name { get; set; } = null!;

        public DateTime? ExpDate { get; set; }

        public int Status { get; set; }

        public decimal Price { get; set; }

        public int Amount { get; set; }

        public string? Description { get; set; }
    }
}
