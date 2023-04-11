using AuthenDemo.Models;
using AuthenDemo.Models.Entities;
using AuthenDemo.Models.Request;
using AuthenDemo.Models.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenDemo.Controllers
{
    public class ProductController : BaseController<ProductController>
    {
        public ProductController(PracticeDbContext context, ILogger<ProductController> logger, IConfiguration configuration) : base(context, logger, configuration)
        {
        }

        [HttpGet]
        public IActionResult GetList([FromQuery] ProductFilter filter)
        {
            var res = _context.Products.Where(m
            => (m.Name.ToLower().Contains(filter.Name.ToLower()) || filter.Name == "")
            && (m.Status == filter.Status || filter.Status == 0)
            && (m.Price == filter.Price || filter.Price == 0));

            return Ok(res);
        }

        [HttpGet]
        public IActionResult GetDetails(int id)
        {
            var res = _context.Products.Find(id);

            if (res == null) { return BadRequest("Product is not found"); }

            return Ok(res);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProductRequest request)
        {
            var product = new Product();

            product.Amount = request.Amount;
            product.Name = request.Name;
            product.Price = request.Price;
            product.Description = request.Description;
            product.ExpDate = request.ExpDate;

            product.CreatedDate = DateTime.Now;
            product.CreatedBy = "system";

            _context.Products.Add(product);

            var ef = _context.SaveChanges();

            return ef > 0 ? Ok(request) : BadRequest("Insert failed");
        }

        [HttpPut]
        public IActionResult Update(int id, ProductRequest request)
        {
            var product = _context.Products.Find(id);
            if (product == null) return BadRequest("Product is not found");

            product.Amount = request.Amount;
            product.Name = request.Name;
            product.Price = request.Price;
            product.Description = request.Description;
            product.ExpDate = request.ExpDate;

            _context.Products.Update(product);

            var ef = _context.SaveChanges();

            return ef > 0 ? Ok(request) : BadRequest("Update Failed");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return BadRequest("Product is not found");

            _context.Products.Remove(product);

            return Ok("Delete Successfully");
        }
    }
}
