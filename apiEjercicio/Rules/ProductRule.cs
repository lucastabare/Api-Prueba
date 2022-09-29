using apiEjercicio.Data;
using apiEjercicio.Models;

namespace apiEjercicio.Rules
{
    public class ProductRule
    {
        public List<Product> GetAllProducts()
        {
            var data = new Northwind();
            return data.GetAllProduct();
        }

        public Product GetProductById(int id)
        {
            var data = new Northwind();
            return data.GetProductById(id);
        }

    }
}
