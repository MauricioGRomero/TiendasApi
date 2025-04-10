namespace TiendasAPI.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public string Estado { get; set; }
    }
}
