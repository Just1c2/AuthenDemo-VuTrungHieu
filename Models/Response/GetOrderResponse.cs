using AuthenDemo.Models.Entities;

namespace AuthenDemo.Models.Response
{
    public class GetOrderResponse
    {
        public string Name { get; set; } = null!;

        public List<OrderProductResponse> Orders { get; set; }

  
    }
}
