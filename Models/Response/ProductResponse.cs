namespace AuthenDemo.Models.Response
{
    public class ProductResponse
    {
        public string Name { get; set; } = null!;

        public DateTime? ExpDate { get; set; }

        public decimal Price { get; set; }

        public int Amount { get; set; }

    }
}
