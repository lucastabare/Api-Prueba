using apiEjercicio.Models;
using apiEjercicio.Rules;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace apiEjercicio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        [HttpGet("/api/products")]
        public List<Product> GetAllProducts()
        {
            var rule = new ProductRule();
            return rule.GetAllProducts();
        }

        [HttpGet("/api/products/{id}")]
        public Product GetProduct(int id)
        {
            var rule = new ProductRule();
            return rule.GetProductById(id);
        }

        [HttpGet("/api/Orders")]
        public List<Order> GetAllOrders()
        {
            var rule = new OrderRule();
            return rule.GetAllOrders();
        }

        [HttpDelete("/api/Orders/")]
        public RespuestaDelete DeleteOrderById(int orderId)
        {
            var rule = new OrderRule();
            return rule.DeleteOrderById(orderId); ;
        }
    }
}
