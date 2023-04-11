using AuthenDemo.Models;
using AuthenDemo.Models.Entities;
using AuthenDemo.Models.Request;
using AuthenDemo.Models.Response;
using AuthenDemo.Models.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenDemo.Controllers
{
    public class CustomerController : BaseController<CustomerController>
    {
        public CustomerController(PracticeDbContext context, ILogger<CustomerController> logger, IConfiguration configuration) : base(context, logger, configuration) { }

        [HttpGet]
        public IActionResult GetList([FromQuery] CustomerFilter filter)
        {
            var res = _context.Customers.Where(m
            => (m.Name.ToLower().Contains(filter.Name.ToLower()) || filter.Name == "")
            && (m.Status == filter.Status || filter.Status == 0)
            && (m.Gender.ToLower().Contains(filter.Gender.ToLower()) || filter.Gender == ""));

            return Ok(res);
        }

        [HttpGet]
        public IActionResult GetDetails(int id)
        {
            var customer = _context.Customers.Find(id);

            if (customer == null) { return BadRequest("Customer is not found"); }

            return Ok(customer);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register([FromBody] CustomerRequest request)
        {
            var customer = new Customer();

            customer.Username = request.Username;
            customer.Password = request.Password;
            customer.Name = request.Name;
            customer.Age = request.Age;
            customer.Gender = request.Gender;
            customer.CreatedDate = DateTime.Now;
            customer.CreatedBy = "system";

            if (request.rePassword != request.Password)
            {
                return BadRequest("Password and rePassword are not the same");
            }

            _context.Customers.Add(customer);

            var ef = _context.SaveChanges();

            return ef > 0 ? Ok(request) : BadRequest("Insert failed");
        }

        [HttpPut]
        public IActionResult Update(int id, UpdateCustomerRequest request)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null) return BadRequest("Customer is not found");

            customer.Name = request.Name;
            customer.Age = request.Age;
            customer.Gender = request.Gender;
            customer.Description = request.Description;
            customer.Address = request.Address;
            customer.Debit = request.Debit;

            _context.Customers.Update(customer);

            var ef = _context.SaveChanges();

            return ef > 0 ? Ok(request) : BadRequest("Update Failed");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null) return BadRequest("Customer is not found");

            _context.Customers.Remove(customer);

            return Ok("Delete Successfully");
        }

        [HttpGet]
        public IActionResult GetOrder(long customerId) 
        {
            var customer = _context.Customers.Find(customerId);
            if (customer == null) return BadRequest("Customer is not found");

            var productOrder = from products in _context.Products
                               join orders in _context.Orders
                               on products.Id equals orders.ProductId
                               where orders.CustomerId == customerId
                               select new ProductResponse
                               {
                                   Name = products.Name,
                                   Price = products.Price,
                                   Amount = products.Amount,
                                   ExpDate = products.ExpDate
                               };

            var orderResponse = new OrderProductResponse();

            orderResponse.Products.Add((ProductResponse)productOrder);

            return Ok(new GetOrderResponse
            {
                Name = customer.Name,
                Orders = new List<OrderProductResponse> { orderResponse }
            }) ;
        }

        
        [HttpPost]
        public IActionResult CreateOrder(CreateOrderRequest request)
        {
            var product = _context.Products.Find(request.ProductId);

            if (product == null) return NotFound("Product is not found");

            if (_context.Customers.Find(request.CustomerId) == null) return NotFound("Customer is not found");

            var productPrice = product.Price;

            var order = new Order
            {
                CustomerId = request.CustomerId,
                ProductId = request.ProductId,
                Amount = request.Amount,
                Price = request.Amount * productPrice
            };

            _context.Orders.Add(order);

            var ef = _context.SaveChanges();

            return ef > 0 ? Ok("Create Order Successfully") : BadRequest("Create Order Failed");
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.Username == request.Username);
            if (customer == null) return BadRequest("Username/Password is incorrect");

            var checkPass = request.Username.ValidPassword(request.Password, customer.Salt, customer.Password);
            if (!checkPass) return BadRequest("Username/Password is incorrect");

            var accessToken = GenerateToken(request.Username);

            return Ok(accessToken);
        }
    }
}
