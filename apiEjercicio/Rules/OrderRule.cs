using apiEjercicio.Data;
using apiEjercicio.Models;

namespace apiEjercicio.Rules
{
    public class OrderRule
    {
        public List<Order> GetAllOrders()
        {
            var data = new Northwind();
            return data.GetAllOrders();
        }
        public RespuestaDelete DeleteOrderById(int orderId)
        {
            try
            {
                var data = new Northwind();
                var cantidad = data.DeleteOrderById(orderId);
                return new RespuestaDelete()
                {
                    CantidadEliminados = cantidad,
                    Result = true,
                    MensajeDelete = cantidad + " Registros eliminados con exito",
                };
            }
            catch (Exception ex)
            {
                return new RespuestaDelete()
                {
                    CantidadEliminados = 0,
                    Result = false,
                    MensajeDelete = ex.Message,
                };
            }

        }
    }

}
