namespace apiEjercicio.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public string? CustomerID { get; set; }
        public List<OrderDetail> Details { get; set; }
        
    }
}
