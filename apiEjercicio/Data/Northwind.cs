using apiEjercicio.Models;
using Dapper;
using System.Data.SqlClient;

namespace apiEjercicio.Data
{
    public class Northwind
    {
        public List<Product> GetAllProduct()
        {
            using var cnn = new SqlConnection("Server=LUCAS-NOTEBOOK;Database=Northwind;Integrated Security=true;");
            cnn.Open();
            var query = "SELECT * FROM Products";
            var lisProduct = cnn.Query<Product>(query).ToList();
            return lisProduct;
        }

        public Product GetProductById(int id)
        {
            using var cnn = new SqlConnection("Server=LUCAS-NOTEBOOK;Database=Northwind;Integrated Security=true;");
            cnn.Open();
            var query = "SELECT * From Products WHERE ProductId = @id";
            var product = cnn.QueryFirstOrDefault<Product>(query, new { id });
            return product;
        }

        public List<Order> GetAllOrders()
        {
            using var cnn = new SqlConnection("Server=LUCAS-NOTEBOOK;Database=Northwind;Integrated Security=true;");
            cnn.Open();
            var query = "SELECT * FROM Orders o INNER JOIN [Order Details] od ON o.OrderID = od.OrderID";

            var lookup = new Dictionary<int, Order>();

            cnn.Query<Order,OrderDetail, Order>(query, (o, d) =>
            {
                if (!lookup.TryGetValue(o.OrderID, out Order order))
                    lookup.Add(o.OrderID, order = o);
                if (order.Details == null)
                    order.Details = new List<OrderDetail>();
                order.Details.Add(d);
                return order;
            }, splitOn: "OrderId").AsQueryable();

            var orders = lookup.Values.ToList();

            return orders;
        }

        public int DeleteOrderById(int orderId)
        {
            using var cnn = new SqlConnection("Server=LUCAS-NOTEBOOK;Database=Northwind;Integrated Security=true;");
            cnn.Open();
            var tran = cnn.BeginTransaction();
            try
            {
                var queryDelete = "DELETE FROM [Order Details] WHERE OrderID = @orderId";
                var cant = cnn.Execute(queryDelete, new { orderId }, tran);

                queryDelete = "DELETE FROM Orders WHERE OrderId = @orderId";
                cant = cnn.Execute(queryDelete, new { orderId }, tran);
                tran.Commit();

                return cant;
            }
            catch
            {
                tran.Rollback();
                throw;
            }
            finally
            {
                cnn.Close();
            }
        }
    }
}
