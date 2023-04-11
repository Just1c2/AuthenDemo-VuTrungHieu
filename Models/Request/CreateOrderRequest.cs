using Microsoft.Identity.Client;

namespace AuthenDemo.Models.Request
{
    public class CreateOrderRequest
    {
        public long CustomerId { get; set; }
        public long ProductId { get; set; }

        public int Amount { get; set; }
    }
}
